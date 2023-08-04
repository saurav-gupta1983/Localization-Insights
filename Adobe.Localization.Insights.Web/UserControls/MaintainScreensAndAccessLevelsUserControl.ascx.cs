using System;
using System.Data;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adobe.Localization.Insights.Common;
using COM = Adobe.Localization.Insights.Common.Common;
using BO = Adobe.Localization.Insights.BusinessLayer;
using DTO = Adobe.Localization.Insights.Common.DataTransferObjects;

namespace Adobe.Localization.Insights.Web.UserControls
{
    /// <summary>
    /// MaintainScreensAndAccessLevelsUserControl
    /// </summary>
    public partial class MaintainScreensAndAccessLevelsUserControl : System.Web.UI.UserControl
    {
        #region Variables

        private Control controlFired;

        private DataSet dsScreenDetails;
        private DataTable dtScreenLabels;
        private DTO.Header dataHeader = new DTO.Header();

        //private int colID = 0;
        private const int CONST_ZERO = 0;

        private bool isReadOnly = true;
        //private bool isContractor = false;
        //private bool isAdmin = true;

        private const string STR_SPACE = "&nbsp;";
        private const string STR_ZERO = "0";
        private const string STR_ONE = "1";
        private const string STR_PRODUCT_SCREEN_ACCESS = "GridViewProductScreenAccess";
        private const string STR_GROUP_SCREEN_ACCESS = "GridViewGroupScreenAccess";
        private const string STR_PRODUCT_FILTER_CRITERIA = "Role = 'Product'";
        private const string STR_NON_PRODUCT_FILTER_CRITERIA = "Role <> 'Product'";

        private const int TAB_INDEX_PRODUCT_SCREEN_ACCESS = 1;
        private const int TAB_INDEX_GROUP_SCREEN_ACCESS = 2;
        private const int NUM_EACH_GRID_ROW_DISPLAY_HEIGHT = 20;

        private const int COL_VIEW_USER_SAVE_UPDATE_NO = 6;
        private const int COL_VIEW_USER_CANCEL_DELETE_NO = 7;

        #endregion

        #region Page Load Event

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            GetScreenAccess();
            SetScreenLabels();

            if (!this.IsPostBack)
                PopulateScreensData();
            else
            {
                dsScreenDetails = (DataSet)ViewState[WebConstants.TBL_SCREENS];
                controlFired = GetPostBackControl(Page);
            }

            SetScreenAccess();
        }

        /// <summary>
        /// GetPostBackControl
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static Control GetPostBackControl(Page page)
        {
            Control postbackControlInstance = null;

            string postbackControlName = page.Request.Params.Get("__EVENTTARGET");
            if (postbackControlName != null && postbackControlName != string.Empty)
            {
                postbackControlInstance = page.FindControl(postbackControlName);
            }
            else
            {
                // handle the Button control postbacks
                for (int i = 0; i < page.Request.Form.Keys.Count; i++)
                {
                    postbackControlInstance = page.FindControl(page.Request.Form.Keys[i]);
                    if (postbackControlInstance is System.Web.UI.WebControls.Button)
                    {
                        return postbackControlInstance;
                    }
                }
            }
            // handle the ImageButton postbacks
            if (postbackControlInstance == null)
            {
                for (int i = 0; i < page.Request.Form.Count; i++)
                {
                    if ((page.Request.Form.Keys[i].EndsWith(".x")) || (page.Request.Form.Keys[i].EndsWith(".y")))
                    {
                        postbackControlInstance = page.FindControl(page.Request.Form.Keys[i].Substring(0, page.Request.Form.Keys[i].Length - 2));
                        return postbackControlInstance;
                    }
                }
            }
            return postbackControlInstance;
        }

        /// <summary>
        /// OnPreRender
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            if (controlFired != null)
            {
                if (controlFired.ClientID.Contains(STR_PRODUCT_SCREEN_ACCESS))
                    TabContainerPopulateScreensData.ActiveTabIndex = TAB_INDEX_PRODUCT_SCREEN_ACCESS;
                else if (controlFired.ClientID.Contains(STR_GROUP_SCREEN_ACCESS))
                    TabContainerPopulateScreensData.ActiveTabIndex = TAB_INDEX_GROUP_SCREEN_ACCESS;
                else
                    TabContainerPopulateScreensData.ActiveTabIndex = CONST_ZERO;
            }
        }

        #endregion

        #region GridViewScreenDetails

        #region Grid Events

        /// <summary>
        /// GridViewScreenDetails_RowDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewScreenDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                SetGridLinkButtonsDisplayText(e.Row);

                if (e.Row.Cells[CONST_ZERO].Text.ToString() == STR_ZERO || e.Row.Cells[CONST_ZERO].Text.ToString() == STR_SPACE)
                {
                    e.Row.Cells[CONST_ZERO].Text = "";
                    SetLinkButtons(e.Row, true);
                }
            }
        }

        /// <summary>
        /// GridViewScreenDetails_RowUpdating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewScreenDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
        }

        /// <summary>
        /// GridViewScreenDetails_RowCancelingEdit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewScreenDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
        }

        #endregion

        #region GridView Button Click Events

        /// <summary>
        /// LinkButtonSaveDetails_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonSaveDetails_Click(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow detailsGridViewRow = GridViewScreenDetails.Rows[index];

            DTO.Screens screensData = new DTO.Screens();
            screensData.DataHeader = dataHeader;
            TextBox TextBoxParentScreenID = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_PARENT_SCREENID);
            TextBox TextBoxScreenIdentifier = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_SCREEN_IDENTIFIER);
            TextBox TextBoxSequence = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_SEQUENCE);
            TextBox TextBoxPageName = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_PAGE_NAME);

            screensData.ParentScreenID = TextBoxParentScreenID.Text;

            if (TextBoxScreenIdentifier.Text == "")
            {
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.SCREEN_SCREEN_IDENTIFIER_MANDATORY);
                return;
            }
            screensData.ScreenIdentifier = TextBoxScreenIdentifier.Text;
            screensData.Sequence = TextBoxSequence.Text;
            screensData.IsPage = STR_ZERO;
            screensData.PageName = TextBoxPageName.Text;

            if (detailsGridViewRow.Cells[CONST_ZERO].Text != "")
            {
                Label LabelScreenID = (Label)detailsGridViewRow.FindControl(WebConstants.CONTROL_LABEL_SCREENID);
                screensData.ScreenID = LabelScreenID.Text;
            }

            if (BO.BusinessObjects.AddUpdateScreensDetails(screensData))
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_SUCCESS_SAVE_MESSAGE);
            else
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_SAVE_MESSAGE);

            PopulateScreensData();
        }

        /// <summary>
        /// LinkButtonCancelDetails_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonCancelDetails_Click(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow detailsGridViewRow = GridViewScreenDetails.Rows[index];

            if (detailsGridViewRow.Cells[CONST_ZERO].Text == "")
            {
                TextBox TextBoxParentScreenID = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_PARENT_SCREENID);
                TextBoxParentScreenID.Text = "";
                TextBox TextBoxScreenIdentifier = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_SCREEN_IDENTIFIER);
                TextBoxScreenIdentifier.Text = "";
                TextBox TextBoxSequence = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_SEQUENCE);
                TextBoxSequence.Text = "";
                TextBox TextBoxPageName = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_PAGE_NAME);
                TextBoxPageName.Text = "";
            }
            else
            {
                SetLinkButtons(detailsGridViewRow, false);
            }
        }

        /// <summary>
        /// LinkButtonUpdateDetails_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonUpdateDetails_Click(object sender, CommandEventArgs e)
        {
            SetLinkButtons(GridViewScreenDetails.Rows[Convert.ToInt32(e.CommandArgument)], true);
        }

        /// <summary>
        /// LinkButtonDeleteDetails_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonDeleteDetails_Click(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow detailsGridViewRow = GridViewScreenDetails.Rows[index];
            Label LabelScreenID = (Label)detailsGridViewRow.FindControl(WebConstants.CONTROL_LABEL_SCREENID);

            DTO.Screens screenData = new DTO.Screens();
            screenData.ScreenID = LabelScreenID.Text;

            if (BO.BusinessObjects.AddUpdateScreensDetails(screenData))
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_SUCCESS_DELETE_MESSAGE);
            else
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_DELETE_MESSAGE); ;

            PopulateScreensData();
        }

        #endregion

        #endregion

        #region GridViewProductScreenAccess

        #region Grid Events

        /// <summary>
        /// GridViewProductScreenAccess_RowDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewProductScreenAccess_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                SetGridLinkButtonsDisplayText(e.Row);
                GridViewScreenAccessRowDataBound(e.Row, STR_PRODUCT_FILTER_CRITERIA);
            }
        }

        /// <summary>
        /// GridViewProductScreenAccess_PageIndexChanging
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewProductScreenAccess_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewProductScreenAccess.PageIndex = e.NewPageIndex;

            GridViewProductScreenAccess.DataSource = (DataTable)ViewState[WebConstants.TBL_SCREEN_ACCESS_LEVELS];
            GridViewProductScreenAccess.DataBind();
        }



        #endregion

        #region GridViewButtonClickEvents

        /// <summary>
        /// LinkButtonSaveProductAccessDetails_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonSaveProductAccessDetails_Click(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow detailsGridViewRow = GridViewProductScreenAccess.Rows[index];

            SaveScreenAccessDetails(detailsGridViewRow, STR_PRODUCT_FILTER_CRITERIA);
        }

        #endregion

        #endregion

        #region GridViewGroupScreenAccess

        #region Grid Events

        /// <summary>
        /// GridViewGroupScreenAccess_RowDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewGroupScreenAccess_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                SetGridLinkButtonsDisplayText(e.Row);
                GridViewScreenAccessRowDataBound(e.Row, STR_NON_PRODUCT_FILTER_CRITERIA);
            }
        }

        /// <summary>
        /// GridViewGroupScreenAccess_PageIndexChanging
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewGroupScreenAccess_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewGroupScreenAccess.PageIndex = e.NewPageIndex;

            GridViewGroupScreenAccess.DataSource = (DataTable)ViewState[WebConstants.TBL_SCREEN_ACCESS_LEVELS];
            GridViewGroupScreenAccess.DataBind();
        }

        #endregion

        #region GridView Button Click Events

        /// <summary>
        /// LinkButtonSaveGroupAccessDetails_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonSaveGroupAccessDetails_Click(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow detailsGridViewRow = GridViewGroupScreenAccess.Rows[index];

            SaveScreenAccessDetails(detailsGridViewRow, STR_NON_PRODUCT_FILTER_CRITERIA);
        }

        #endregion

        #endregion

        #region Private Functions

        /// <summary>
        /// PopulateProductVersion
        /// </summary>
        private void PopulateScreensData()
        {
            dsScreenDetails = BO.BusinessObjects.GetScreensData();
            ViewState[WebConstants.TBL_PROJECT_ROLES] = dsScreenDetails.Tables[WebConstants.TBL_PROJECT_ROLES];
            ViewState[WebConstants.TBL_SCREENS] = dsScreenDetails;

            DataTable dtScreenAccess = dsScreenDetails.Tables[0].Copy();
            ViewState[WebConstants.TBL_SCREEN_ACCESS_LEVELS] = dtScreenAccess;

            if (isReadOnly)
                dsScreenDetails.Tables[0].Rows.RemoveAt(0);

            GridViewScreenDetails.DataSource = dsScreenDetails.Tables[0];
            GridViewScreenDetails.DataBind();

            dtScreenAccess.Rows.RemoveAt(0);

            DataView dvScreens = new DataView(dtScreenAccess);
            dvScreens.RowFilter = WebConstants.COL_SCREEN_SEQUENCE + " <> " + STR_ZERO;
            dtScreenAccess = dvScreens.ToTable();

            GridViewProductScreenAccess.DataSource = dtScreenAccess;
            GridViewProductScreenAccess.DataBind();
            GridViewGroupScreenAccess.DataSource = dtScreenAccess;
            GridViewGroupScreenAccess.DataBind();
        }

        /// <summary>
        /// GridViewScreenAccessRowDataBound
        /// </summary>
        /// <param name="row"></param>
        private void GridViewScreenAccessRowDataBound(GridViewRow row, string filterCriteria)
        {
            DataTable dtProjectRoles = (DataTable)ViewState[WebConstants.TBL_PROJECT_ROLES];
            foreach (DataRow dr in dtProjectRoles.Select(filterCriteria))
            {
                Label LabelScreenID = (Label)row.FindControl(WebConstants.CONTROL_LABEL_SCREENID);
                Label LabelScreenAccessExists = (Label)row.FindControl(WebConstants.CONTROL_LABEL_SCREEN_ACCESS_EXISTS);

                string projectRoleID = dr[WebConstants.COL_PROJECT_ROLE_ID].ToString();
                string screenID = LabelScreenID.Text;

                DataRow[] drData = dsScreenDetails.Tables[WebConstants.TBL_SCREEN_ACCESS_LEVELS].Select(WebConstants.COL_PROJECT_ROLE_ID + " = " + projectRoleID + " AND " + WebConstants.COL_SCREEN_ID + " = " + screenID);

                if (drData.Length > 0)
                {
                    LabelScreenAccessExists.Text = STR_ONE;

                    RadioButton radioBtnRead = (RadioButton)row.FindControl(WebConstants.CONTROL_RADIOBUTTON_READ + projectRoleID);
                    RadioButton radioBtnReadWrite = (RadioButton)row.FindControl(WebConstants.CONTROL_RADIOBUTTON_READ_WRITE + projectRoleID);
                    RadioButton radioBtnReadReport = (RadioButton)row.FindControl(WebConstants.CONTROL_RADIOBUTTON_REPORT + projectRoleID);
                    RadioButton radioBtnClear = (RadioButton)row.FindControl(WebConstants.CONTROL_RADIOBUTTON_CLEAR + projectRoleID);

                    radioBtnRead.Checked = drData[0][WebConstants.COL_SCREEN_IS_READ].ToString() == STR_ONE ? true : false;
                    radioBtnReadWrite.Checked = drData[0][WebConstants.COL_SCREEN_IS_READ_WRITE].ToString() == STR_ONE ? true : false;
                    radioBtnReadReport.Checked = drData[0][WebConstants.COL_SCREEN_IS_REPORT].ToString() == STR_ONE ? true : false;

                    if (radioBtnRead.Checked == false && radioBtnReadWrite.Checked == false && radioBtnReadReport.Checked == false)
                        radioBtnClear.Checked = true;

                    if (isReadOnly)
                    {
                        radioBtnRead.Enabled = false;
                        radioBtnReadWrite.Enabled = false;
                        radioBtnReadReport.Enabled = false;
                        radioBtnClear.Enabled = false;
                    }
                }
            }
        }

        /// <summary>
        /// SaveScreenAccessDetails
        /// </summary>
        /// <param name="GridViewProductScreenAccess"></param>
        private void SaveScreenAccessDetails(GridViewRow row, string filterCriteria)
        {
            ArrayList accessList = new ArrayList();
            Label LabelScreenID = (Label)row.FindControl(WebConstants.CONTROL_LABEL_SCREENID);
            Label LabelScreenAccessExists = (Label)row.FindControl(WebConstants.CONTROL_LABEL_SCREEN_ACCESS_EXISTS);

            foreach (DataRow dr in ((DataTable)ViewState[WebConstants.TBL_PROJECT_ROLES]).Select(filterCriteria))
            {
                DTO.Screens screenData = new DTO.Screens();
                screenData.DataHeader = dataHeader;
                screenData.ScreenID = LabelScreenID.Text;
                screenData.ScreenAccessExists = LabelScreenAccessExists.Text;
                screenData.ProjectRoleID = dr.ItemArray[CONST_ZERO].ToString();

                RadioButton radioBtnRead = (RadioButton)row.FindControl(WebConstants.CONTROL_RADIOBUTTON_READ + screenData.ProjectRoleID);
                RadioButton radioBtnReadWrite = (RadioButton)row.FindControl(WebConstants.CONTROL_RADIOBUTTON_READ_WRITE + screenData.ProjectRoleID);
                RadioButton radioBtnReport = (RadioButton)row.FindControl(WebConstants.CONTROL_RADIOBUTTON_REPORT + screenData.ProjectRoleID);

                screenData.IsRead = radioBtnRead.Checked.ToString();
                screenData.IsReadWrite = radioBtnReadWrite.Checked.ToString();
                screenData.IsReport = radioBtnReport.Checked.ToString();

                accessList.Add(screenData);
            }

            if (BO.BusinessObjects.AddUpdateScreensAccessDetails(accessList))
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_SUCCESS_SAVE_MESSAGE);
            else
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_SAVE_MESSAGE);

            PopulateScreensData();
        }

        /// <summary>
        /// SetLinkButtons
        /// </summary>
        /// <param name="gridViewRow"></param>
        /// <param name="isSave"></param>
        private void SetLinkButtons(GridViewRow gridViewRow, bool isSave)
        {
            LinkButton LinkButtonSave = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_SAVE);
            LinkButtonSave.Visible = isSave;
            LinkButton LinkButtonCancel = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_CANCEL);
            LinkButtonCancel.Visible = isSave;
            LinkButton LinkButtonUpdate = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_UPDATE);
            LinkButtonUpdate.Visible = (!isSave);
            LinkButton LinkButtonDelete = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_DELETE);
            LinkButtonDelete.Visible = (!isSave);

            SetLinkButtonsForTextBoxes(gridViewRow, WebConstants.CONTROL_LABEL_PARENT_SCREENID, WebConstants.CONTROL_TEXTBOX_PARENT_SCREENID, isSave);
            SetLinkButtonsForTextBoxes(gridViewRow, WebConstants.CONTROL_LABEL_SCREEN_IDENTIFIER, WebConstants.CONTROL_TEXTBOX_SCREEN_IDENTIFIER, isSave);
            SetLinkButtonsForTextBoxes(gridViewRow, WebConstants.CONTROL_LABEL_SEQUENCE, WebConstants.CONTROL_TEXTBOX_SEQUENCE, isSave);
            SetLinkButtonsForTextBoxes(gridViewRow, WebConstants.CONTROL_LABEL_PAGE_NAME, WebConstants.CONTROL_TEXTBOX_PAGE_NAME, isSave);
        }

        /// <summary>
        /// SetLinkButtonsForTextBoxes
        /// </summary>
        /// <param name="gridViewRow"></param>
        /// <param name="labelType"></param>
        /// <param name="textBoxType"></param>
        /// <param name="isSave"></param>
        private void SetLinkButtonsForTextBoxes(GridViewRow gridViewRow, string labelType, string textBoxType, bool isSave)
        {
            Label LabelType = (Label)gridViewRow.FindControl(labelType);
            LabelType.Visible = (!isSave);
            TextBox TextBoxType = (TextBox)gridViewRow.FindControl(textBoxType);
            TextBoxType.Visible = isSave;
            TextBoxType.Text = LabelType.Text;
        }

        #endregion

        #region Screen Access and Labels

        /// <summary>
        /// GetScreenAccess
        /// </summary>
        private void GetScreenAccess()
        {
            dataHeader.LoginID = Session[WebConstants.SESSION_LOGIN_ID].ToString();

            if (Session[WebConstants.SESSION_CACHE_SCREEN_ACCESS_DATA_TABLE] != null)
            {
                DataTable dtScreenAccess = (DataTable)Session[WebConstants.SESSION_CACHE_SCREEN_ACCESS_DATA_TABLE];

                DataRow drScreenAccess = dtScreenAccess.Select(WebConstants.COL_SCREEN_IDENTIFIER + " = '" + Session[WebConstants.SESSION_IDENTIFIER] + "'")[0];

                if (drScreenAccess[WebConstants.COL_SCREEN_IS_READ_WRITE].ToString() == WebConstants.DEF_VAL_ACCESS_CHECKED)
                    isReadOnly = false;
            }
        }

        /// <summary>
        /// SetScreenAccess
        /// </summary>
        private void SetScreenAccess()
        {
            if (isReadOnly)
            {
                GridViewScreenDetails.Columns[COL_VIEW_USER_SAVE_UPDATE_NO].Visible = false;
                GridViewScreenDetails.Columns[COL_VIEW_USER_CANCEL_DELETE_NO].Visible = false;

                GridViewProductScreenAccess.Columns[GridViewProductScreenAccess.Columns.Count - 1].Visible = false;
                GridViewGroupScreenAccess.Columns[GridViewGroupScreenAccess.Columns.Count - 1].Visible = false;
            }

            AddFooterPadding(GridViewScreenDetails.Rows.Count * 2);
        }

        /// <summary>
        /// PopulateScreenLabels
        /// </summary>
        /// <param name="userID"></param>
        private void PopulateScreenLabels()
        {
            DTO.Screens screenData = new DTO.Screens();
            screenData.ScreenIdentifier = Session[WebConstants.SESSION_IDENTIFIER].ToString();
            screenData.IsIncludeCommon = true;

            if (Session[WebConstants.SESSION_LOCALE_ID] != null)
                screenData.LocaleID = Session[WebConstants.SESSION_LOCALE_ID].ToString();
            else
                screenData.LocaleID = WebConstants.DEF_VAL_LOCALE_ID;

            DataTable dtLocales = (DataTable)ViewState[WebConstants.TBL_LOCALES];
            if (dtLocales == null)
            {
                dtLocales = BO.BusinessObjects.GetAllLocales().Tables[0];
                ViewState[WebConstants.TBL_LOCALES] = dtLocales;
            }

            screenData.LocaleID = COM.GetLocaleForScreenLabels(screenData.LocaleID, dtLocales)[WebConstants.COL_LOCALE_ID].ToString();

            dtScreenLabels = BO.BusinessObjects.GetScreenLocalizedLabels(screenData).Tables[0];
        }

        /// <summary>
        /// SetScreenLabels
        /// </summary>
        /// <param name="dtScreenLabels"></param>
        private void SetScreenLabels()
        {
            PopulateScreenLabels();

            LabelHeading.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelHeading.Text);
            TabPanelSetScreensDetails.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, TabPanelSetScreensDetails.HeaderText);
            TabPanelSetProductScreenAccess.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, TabPanelSetProductScreenAccess.HeaderText);
            TabPanelSetGroupScreenAccess.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, TabPanelSetGroupScreenAccess.HeaderText);

            foreach (DataControlField field in GridViewScreenDetails.Columns)
                if (field.HeaderText != "")
                    field.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, field.HeaderText);

            foreach (DataControlField field in GridViewProductScreenAccess.Columns)
                if (field.HeaderText != "")
                    field.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, field.HeaderText);

            foreach (DataControlField field in GridViewGroupScreenAccess.Columns)
                if (field.HeaderText != "")
                    field.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, field.HeaderText);
        }

        /// <summary>
        /// SetGridLinkButtonsDisplayText
        /// </summary>
        /// <param name="gridView"></param>
        private void SetGridLinkButtonsDisplayText(GridViewRow gridViewRow)
        {
            LinkButton LinkButtonSave = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_SAVE);
            LinkButton LinkButtonCancel = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_CANCEL);
            LinkButton LinkButtonUpdate = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_UPDATE);
            LinkButton LinkButtonDelete = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_DELETE);

            if (LinkButtonSave != null)
                LinkButtonSave.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LinkButtonSave.Text);
            if (LinkButtonCancel != null)
            {
                if (gridViewRow.Cells[CONST_ZERO].Text.ToString() == STR_ZERO || gridViewRow.Cells[CONST_ZERO].Text.ToString() == STR_SPACE)
                    LinkButtonCancel.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.DEF_CONTROL_LINKBUTTON_CLEAR);
                else
                    LinkButtonCancel.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LinkButtonCancel.Text);
            }
            if (LinkButtonUpdate != null)
                LinkButtonUpdate.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LinkButtonUpdate.Text);
            if (LinkButtonDelete != null)
                LinkButtonDelete.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LinkButtonDelete.Text);
        }

        /// <summary>
        /// AddFooterPadding
        /// </summary>
        private void AddFooterPadding(int rowsCount)
        {
            int spaceCount = (rowsCount * NUM_EACH_GRID_ROW_DISPLAY_HEIGHT) + 4 * NUM_EACH_GRID_ROW_DISPLAY_HEIGHT;

            if (spaceCount < WebConstants.VAL_FOOTER_MAXIMUM_PIXEL)
            {
                Table tb = new Table();
                TableRow tr = new TableRow();
                TableCell tc = new TableCell();
                tc.Height = Unit.Pixel(WebConstants.VAL_FOOTER_MAXIMUM_PIXEL - spaceCount);
                tr.Controls.Add(tc);
                tb.Controls.Add(tr);

                PanelFooterPadding.Controls.Add(tb);
                PanelFooterPadding.Visible = true;
            }
        }

        #endregion
    }
}