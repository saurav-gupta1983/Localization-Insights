using System;
using System.Data;
using System.Collections;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Adobe.Localization.Insights.Common;
using COM = Adobe.Localization.Insights.Common.Common;
using BO = Adobe.Localization.Insights.BusinessLayer;
using DTO = Adobe.Localization.Insights.Common.DataTransferObjects;

namespace Adobe.Localization.Insights.Web.UserControls
{
    /// <summary>
    /// DefineProjectWindUpUserControl
    /// </summary>
    public partial class DefineProjectWindUpUserControl : System.Web.UI.UserControl
    {
        #region Variables

        private DataTable dtProjectBuildDetails;
        //private DataTable dtProductVersion;
        private DataTable dtScreenLabels;

        private const int colID = 0;
        private string productID = "";

        private DTO.Header dataHeader = new DTO.Header();

        private bool isReadOnly = true;
        private bool isProductVersionActive = false;

        private const string STR_NON_ACTIVE = "Not Active";
        private const string STR_NOT_DEFINED = "Not Defined";
        private const string STR_SPACE = "&nbsp;";
        private const string STR_ZERO = "0";
        private const string STR_VAL_ISACTIVE = "1";

        private const string CONFIRM_MESSAGE_PRODUCT_VERSION = "After submitting Wind-Up details, This Product will be closed and will not be available for updations.";

        private const int NUM_EACH_GRID_ROW_DISPLAY_HEIGHT = 20;
        private const int COL_GRID_SAVE_UPDATE_NO = 7;
        private const int COL_GRID_CANCEL_DELETE_NO = 8;

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
            {
                PopulateProductVersions();
                GetProjectWindUpDetails();
            }

            dtProjectBuildDetails = (DataTable)ViewState[WebConstants.TBL_PROJECT_BUILD_DETAILS];
            isProductVersionActive = (bool)ViewState[WebConstants.STR_IS_PRODUCT_VERSION_ACTIVE];

            SetScreenAccess();
        }

        #endregion

        #region GM Details

        #region Grid Events

        /// <summary>
        /// GridViewMasterDataDetails_RowDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewMasterDataDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                SetGridLinkButtonsDisplayText(e.Row);

                Label LabelGMDate = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_GM_DATE);
                Calendar DateCalendarGMDate = (Calendar)e.Row.FindControl(WebConstants.CONTROL_DATECALENDAR_GM_DATE);
                SetDateCalendar(DateCalendarGMDate);

                if (LabelGMDate.Text == "")
                    LabelGMDate.Text = STR_NOT_DEFINED;
                else
                    LabelGMDate.Text = DateTime.Parse(LabelGMDate.Text).ToShortDateString();

                Label LabelBuildNo = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_BUILD_NO);
                TextBox TextBoxBuildNo = (TextBox)e.Row.FindControl(WebConstants.CONTROL_TEXTBOX_BUILD_NO);

                if (TextBoxBuildNo.Text == "")
                    LabelBuildNo.Text = STR_NOT_DEFINED;

                Label LabelBuildPath = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_BUILD_PATH);
                TextBox TextBoxBuildPath = (TextBox)e.Row.FindControl(WebConstants.CONTROL_TEXTBOX_BUILD_PATH);
                if (TextBoxBuildPath.Text == "")
                    LabelBuildPath.Text = STR_NOT_DEFINED;
            }
        }

        /// <summary>
        /// GridViewMasterDataDetails_RowUpdating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewMasterDataDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
        }

        /// <summary>
        /// GridViewMasterDataDetails_RowCancelingEdit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewMasterDataDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
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
            GridViewRow detailsGridViewRow = GridViewMasterDataDetails.Rows[index];

            DTO.Product productData = new DTO.Product();
            productData.DataHeader = dataHeader;
            productData.ProductVersionID = DropDownListProductVersion.SelectedValue;
            productData.WindUpId = LabelWindUpID.Text;

            Label LabelProjectBuildDetailID = (Label)detailsGridViewRow.FindControl(WebConstants.CONTROL_LABEL_PROJECT_BUILD_DETAIL_ID);
            TextBox TextBoxBuildNo = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_BUILD_NO);
            TextBox TextBoxGMBuildPath = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_BUILD_PATH);
            Calendar DateCalendarGMDate = (Calendar)detailsGridViewRow.FindControl(WebConstants.CONTROL_DATECALENDAR_GM_DATE);

            productData.ProjectBuildDetailID = LabelProjectBuildDetailID.Text;
            if (TextBoxBuildNo.Text == "")
            {
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.SCREEN_BUILD_NO_MANDATORY);
                return;
            }

            productData.BuildNo = TextBoxBuildNo.Text;
            productData.GmDate = DateCalendarGMDate.Date;
            productData.BuildPath = TextBoxGMBuildPath.Text;

            productData.PostMortemDetails = TextBoxComments.Text;
            productData.Learnings = TextBoxLearnings.Text;
            productData.BestPractices = TextBoxBestPractices.Text;

            if (BO.BusinessObjects.UpdateProjectGMDetails(productData))
            {
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_SUCCESS_SAVE_MESSAGE);
                GetProjectWindUpDetails();
            }
            else
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_SAVE_MESSAGE);

        }

        /// <summary>
        /// LinkButtonCancelDetails_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonCancelDetails_Click(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow detailsGridViewRow = GridViewMasterDataDetails.Rows[index];

            if (detailsGridViewRow.Cells[colID].Text == "")
            {
                TextBox TextBoxBuildNo = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_BUILD_NO);
                TextBoxBuildNo.Text = "";
                TextBox TextBoxBuildPath = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_BUILD_PATH);
                TextBoxBuildPath.Text = "";
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
            SetLinkButtons(GridViewMasterDataDetails.Rows[Convert.ToInt32(e.CommandArgument)], true);
        }

        /// <summary>
        /// LinkButtonDeleteDetails_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonDeleteDetails_Click(object sender, CommandEventArgs e)
        {
            //int index = Convert.ToInt32(e.CommandArgument);
            //GridViewRow detailsGridViewRow = GridViewMasterDataDetails.Rows[index];

            //DTO.MasterData masterData = new DTO.MasterData();
            //masterData.TableName = WebConstants.TBL_PROJECT_GM_DETAILS;
            //masterData.ColumnNames = columnNames;

            //Label LabelProjectGMDetailID = (Label)detailsGridViewRow.FindControl(WebConstants.CONTROL_LABEL_PROJECT_GM_DETAIL_ID);
            //masterData.MasterDataID = LabelProjectGMDetailID.Text;

            //if (BO.BusinessObjects.AddUpdateDetailsforMasterData(masterData))
            //    LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_SUCCESS_DELETE_MESSAGE);
            //else
            //    LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_DELETE_MESSAGE); ;

            //PopulateGMDetailsData();
        }

        #endregion

        #region Button Click Events

        /// <summary>
        /// ButtonSaveProjectWindUpDetails_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSaveProjectWindUpDetails_Click(object sender, EventArgs e)
        {
            if (UpdateProjectWindUpDetails())
                GetProjectWindUpDetails();
        }

        /// <summary>
        /// ButtonSubmitWindUpDetails_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSubmitWindUpDetails_Click(object sender, EventArgs e)
        {
            if (UpdateProjectWindUpDetails())
            {
                GetProjectWindUpDetails();

                DTO.Product productData = new DTO.Product();
                productData.ProductVersionID = DropDownListProductVersion.SelectedValue;
                productData.DataHeader = dataHeader;
                if (BO.BusinessObjects.SubmitWindUpDetails(productData))
                    Response.Redirect(WebConstants.PAGE_LOCALIZATION_INSIGHT);
                else
                    LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_SAVE_MESSAGE);
            }
        }

        #endregion

        #region DropDownList events

        /// <summary>
        /// DropDownListProductVersion_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListProductVersion_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            GetProjectWindUpDetails();
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// GetProjectWindUpDetails
        /// </summary>
        /// <returns></returns>
        private void GetProjectWindUpDetails()
        {
            DTO.Product productData = new DTO.Product();
            productData.ProductVersionID = DropDownListProductVersion.SelectedValue;

            if (DropDownListProductVersion.Items.Count == 0)
                productData.ProductVersionID = "-1";

            DataTable dtProjectWindUpDetails = BO.BusinessObjects.GetProjectWindUpDetails(productData).Tables[0];

            if (dtProjectWindUpDetails.Rows.Count == 1)
            {
                TextBoxComments.Text = dtProjectWindUpDetails.Rows[0][WebConstants.COL_PROJECT_PST_MORTEM_ANALYSIS_COMMENTS].ToString();
                TextBoxLearnings.Text = dtProjectWindUpDetails.Rows[0][WebConstants.COL_PROJECT_LEARNINGS].ToString();
                TextBoxBestPractices.Text = dtProjectWindUpDetails.Rows[0][WebConstants.COL_PROJECT_BEST_PRACTICES].ToString();

                LabelWindUpID.Text = dtProjectWindUpDetails.Rows[0][WebConstants.COL_PROJECT_WIND_UP_ID].ToString();
            }

            PopulateGMDetailsData();
        }

        /// <summary>
        /// PopulateProductVersions
        /// </summary>
        private void PopulateProductVersions()
        {
            DTO.Product productData = new DTO.Product();
            productData.ProductID = Session[WebConstants.SESSION_PRODUCT_ID].ToString();
            productData.ProductVersionID = Session[WebConstants.SESSION_PRODUCT_VERSION_ID].ToString();
            //productData.IsActive = STR_VAL_ISACTIVE;

            DataTable dtProductVersion = BO.BusinessObjects.GetProductVersion(productData).Tables[0];

            LabelProductValue.Text = dtProductVersion.Rows[0][WebConstants.COL_PRODUCT].ToString();

            DropDownListProductVersion.DataSource = dtProductVersion;
            DropDownListProductVersion.DataValueField = WebConstants.COL_PRODUCT_VERSION_ID;
            DropDownListProductVersion.DataTextField = WebConstants.COL_PRODUCT_VERSION;
            DropDownListProductVersion.DataBind();

            //DropDownListProductVersion.SelectedIndex = DropDownListProductVersion.Items.IndexOf(DropDownListProductVersion.Items.FindByValue(Session[WebConstants.SESSION_PRODUCT_VERSION_ID].ToString()));

            if (dtProductVersion.Rows[0][WebConstants.COL_PRODUCT_VERSION_CODE].ToString() != "")
                LabelProductVersionValue.Text = dtProductVersion.Rows[0][WebConstants.COL_PRODUCT_VERSION_CODE].ToString() + " ( " + dtProductVersion.Rows[0][WebConstants.COL_PRODUCT_VERSION].ToString() + " ) ";
            else
                LabelProductVersionValue.Text = dtProductVersion.Rows[0][WebConstants.COL_PRODUCT_VERSION].ToString();

            if (dtProductVersion.Rows[0][WebConstants.COL_PRODUCT_VERSION_ACTIVE].ToString() == "0")
                LabelProductVersionValue.Text += " - " + COM.GetScreenLocalizedLabel(dtScreenLabels, STR_NON_ACTIVE);
            else
                isProductVersionActive = true;

            ViewState[WebConstants.STR_IS_PRODUCT_VERSION_ACTIVE] = isProductVersionActive;
        }

        /// <summary>
        /// PopulateData
        /// </summary>
        private void PopulateGMDetailsData()
        {
            DTO.Product productData = new DTO.Product();
            productData.ProductVersionID = DropDownListProductVersion.SelectedValue;
            productData.IsReleaseBuild = STR_VAL_ISACTIVE;

            dtProjectBuildDetails = BO.BusinessObjects.GetProjectBuildDetails(productData).Tables[0];
            dtProjectBuildDetails = COM.AddSequenceColumnToDataTable(dtProjectBuildDetails);

            if (dtProjectBuildDetails.Rows.Count == 0)
                LabelNoProjectBuildsAvailable.Visible = true;

            GridViewMasterDataDetails.DataSource = dtProjectBuildDetails;
            GridViewMasterDataDetails.DataBind();
        }

        /// <summary>
        /// SetDateCalendar
        /// </summary>
        private void SetDateCalendar(Calendar dateCalendar)
        {
            HtmlInputButton dateButton;
            HtmlGenericControl dateDiv;

            dateButton = (HtmlInputButton)dateCalendar.FindControl(WebConstants.CONTROL_DATECALENDAR_BUTTON);
            dateDiv = (HtmlGenericControl)dateCalendar.FindControl(WebConstants.CONTROL_DATECALENDAR_DIVCALENDAR);
            dateButton.Attributes.Add(WebConstants.CONTROL_DATECALENDAR_ONCLICK, "javascript:return OnClick('" + dateDiv.ClientID + "')");
        }

        /// <summary>
        /// UpdateProjectWindUpDetails
        /// </summary>
        private bool UpdateProjectWindUpDetails()
        {
            DTO.Product productData = new DTO.Product();
            productData.DataHeader = dataHeader;
            productData.WindUpId = LabelWindUpID.Text;
            productData.ProductVersionID = DropDownListProductVersion.SelectedValue;
            productData.PostMortemDetails = TextBoxComments.Text;
            productData.Learnings = TextBoxLearnings.Text;
            productData.BestPractices = TextBoxBestPractices.Text;

            if (BO.BusinessObjects.AddUpdateWindUpDetails(productData))
            {
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_SUCCESS_SAVE_MESSAGE);
                return true;
            }
            else
            {
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_SAVE_MESSAGE);
                return false;
            }
        }

        #endregion

        #endregion

        #region Private Functions

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

            //Build No
            SetLinkButtonsForTextBoxes(gridViewRow, WebConstants.CONTROL_LABEL_BUILD_NO, WebConstants.CONTROL_TEXTBOX_BUILD_NO, isSave);

            //GM Date
            SetLinkButtonsForDateCalendar(gridViewRow, WebConstants.CONTROL_LABEL_GM_DATE, WebConstants.CONTROL_DATECALENDAR_GM_DATE, isSave);

            //Build Path
            SetLinkButtonsForTextBoxes(gridViewRow, WebConstants.CONTROL_LABEL_BUILD_PATH, WebConstants.CONTROL_TEXTBOX_BUILD_PATH, isSave);
        }

        /// <summary>
        /// SetLinkButtonsForDropDowns
        /// </summary>
        /// <param name="gridViewRow"></param>
        /// <param name="labelType"></param>
        /// <param name="dropDownListType"></param>
        /// <param name="isSave"></param>
        private void SetLinkButtonsForDropDowns(GridViewRow gridViewRow, DataTable dt, string labelType, string dropDownListType, bool isSave)
        {
            Label LabelType = (Label)gridViewRow.FindControl(labelType);
            LabelType.Visible = (!isSave);
            DropDownList DropDownListType = (DropDownList)gridViewRow.FindControl(dropDownListType);
            DropDownListType.Visible = isSave;

            DropDownListType.DataSource = dt;
            DropDownListType.DataValueField = WebConstants.COL_ID;
            DropDownListType.DataTextField = WebConstants.COL_DESCRIPTION;
            DropDownListType.DataBind();
            DropDownListType.SelectedIndex = DropDownListType.Items.IndexOf(DropDownListType.Items.FindByText(LabelType.Text));
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

            if (LabelType.Text == STR_NOT_DEFINED)
                TextBoxType.Text = "";
            else
                TextBoxType.Text = LabelType.Text;
        }

        /// <summary>
        /// SetLinkButtonsForDateCalendar
        /// </summary>
        /// <param name="gridViewRow"></param>
        /// <param name="labelType"></param>
        /// <param name="dateCalendarType"></param>
        /// <param name="isSave"></param>
        private void SetLinkButtonsForDateCalendar(GridViewRow gridViewRow, string labelType, string dateCalendarType, bool isSave)
        {
            Label LabelType = (Label)gridViewRow.FindControl(labelType);
            LabelType.Visible = (!isSave);
            Calendar CalendarType = (Calendar)gridViewRow.FindControl(dateCalendarType);
            CalendarType.Visible = isSave;

            if (LabelType.Text != STR_NOT_DEFINED)
                CalendarType.Date = LabelType.Text;
            SetDateCalendar(CalendarType);
        }

        /// <summary>
        /// GetColumnNames
        /// </summary>
        private ArrayList GetColumnNames(string tableName)
        {
            return BO.BusinessObjects.GetColumnNames(tableName);
        }

        #endregion

        #region Screen Access and Labels

        /// <summary>
        /// GetScreenAccess
        /// </summary>
        private void GetScreenAccess()
        {
            dataHeader.LoginID = Session[WebConstants.SESSION_LOGIN_ID].ToString();

            if (Session[WebConstants.SESSION_PRODUCT_ID] != null)
                productID = Session[WebConstants.SESSION_PRODUCT_ID].ToString();

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
            ButtonSubmitWindUpDetails.OnClientClick = string.Format(WebConstants.CONFIRM_MESSAGE, CONFIRM_MESSAGE_PRODUCT_VERSION);

            if (!isProductVersionActive)
                isReadOnly = true;

            if (isReadOnly)
            {
                GridViewMasterDataDetails.Columns[COL_GRID_SAVE_UPDATE_NO].Visible = false;
                GridViewMasterDataDetails.Columns[COL_GRID_CANCEL_DELETE_NO].Visible = false;

                ButtonSaveProjectWindUpDetails.Enabled = false;
                ButtonSubmitWindUpDetails.Enabled = false;

                TextBoxComments.Visible = false;
                TextBoxLearnings.Visible = false;
                TextBoxBestPractices.Visible = false;

                LabelShowComments.Visible = true;
                LabelShowLearnings.Visible = true;
                LabelShowBestPractices.Visible = true;

                LabelShowComments.Text = TextBoxComments.Text;
                LabelShowLearnings.Text = TextBoxLearnings.Text;
                LabelShowBestPractices.Text = TextBoxBestPractices.Text;
            }

            AddFooterPadding(1000);

            //if (DropDownListProductVersion.Items.Count == 0)
            //{
            //    GridViewMasterDataDetails.Enabled = false;

            //    TextBoxComments.Enabled = false;
            //    TextBoxLearnings.Enabled = false;
            //    TextBoxBestPractices.Enabled = false;

            //    ButtonSaveProjectWindUpDetails.Enabled = false;
            //    ButtonSaveProjectWindUpDetails.Enabled = false;
            //}

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

            LabelNoProjectBuildsAvailable.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelNoProjectBuildsAvailable.Text);

            if (Session[WebConstants.SESSION_LOCALE_ID] != null && Session[WebConstants.SESSION_LOCALE_ID].ToString() != WebConstants.DEF_VAL_LOCALE_ID)
            {
                LabelHeader.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelHeader.Text);
                LabelProduct.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelProduct.Text);
                LabelProductVersion.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelProductVersion.Text);
                LabelComments.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelComments.Text);
                LabelLearnings.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelLearnings.Text);
                LabelBestPractices.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelBestPractices.Text);

                ButtonSaveProjectWindUpDetails.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, ButtonSaveProjectWindUpDetails.Text);

                foreach (DataControlField field in GridViewMasterDataDetails.Columns)
                    if (field.HeaderText != "")
                        field.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, field.HeaderText);
            }
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
                if (gridViewRow.Cells[colID].Text.ToString() == STR_ZERO || gridViewRow.Cells[colID].Text.ToString() == STR_SPACE)
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