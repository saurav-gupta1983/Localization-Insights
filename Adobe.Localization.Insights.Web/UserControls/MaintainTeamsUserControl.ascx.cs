using System;
using System.Data;
using System.Collections;
using System.Web.UI.WebControls;
using Adobe.Localization.Insights.Common;
using COM = Adobe.Localization.Insights.Common.Common;
using BO = Adobe.Localization.Insights.BusinessLayer;
using DTO = Adobe.Localization.Insights.Common.DataTransferObjects;

namespace Adobe.Localization.Insights.Web.UserControls
{
    public partial class MaintainTeamsUserControl : System.Web.UI.UserControl
    {
        #region Variables

        private bool isReadOnly = true;

        private DataTable dtScreenLabels;
        private DataTable dtMasterDetails = new DataTable();
        private DataTable dtTypeDetails = new DataTable();
        private DTO.Header dataHeader = new DTO.Header();

        private ArrayList columnNames;

        private const int CONST_ZERO = 0;
        private const int CONST_ONE = 1;
        private const int COL_SAVE_UPDATE_NO = 7;
        private const int COL_CANCEL_DELETE_NO = 8;
        private const int NUM_EACH_GRID_ROW_DISPLAY_HEIGHT = 20;

        private const string CONST_TRUE = "True";
        private const string STR_SPACE = "&nbsp;";

        #endregion

        #region Page Load Event

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            GetScreenAccess(Session[WebConstants.SESSION_IDENTIFIER].ToString());
            SetScreenLabels();

            if (!this.IsPostBack)
            {
                ViewState[WebConstants.COL_STR_COLUMN_NAMES] = GetColumnNames(WebConstants.TBL_VENDORS);
                PopulateData();
            }

            columnNames = (ArrayList)ViewState[WebConstants.COL_STR_COLUMN_NAMES];
            dtTypeDetails = (DataTable)ViewState[WebConstants.COL_STR_TYPE_TABLE_DETAILS];

            SetScreenAccess();
            AddFooterPadding(GridViewMasterDataDetails.Rows.Count);
        }

        #endregion

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

                if (e.Row.Cells[CONST_ZERO].Text.ToString() == CONST_ZERO.ToString() || e.Row.Cells[CONST_ZERO].Text.ToString() == STR_SPACE)
                {
                    e.Row.Cells[CONST_ZERO].Text = "";
                    setLinkButtons(e.Row, true);
                }
                else
                {
                    Label LabelType = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_TYPE);
                    if (LabelType.Text != "")
                    {
                        LabelType.Text = dtTypeDetails.Select(WebConstants.COL_VENDOR_TYPE_ID + " = " + LabelType.Text)[0][WebConstants.COL_VENDOR_TYPE].ToString();
                    }

                    Label LabelContractor = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_CONTRACTOR);
                    if (LabelContractor.Text == CONST_ONE.ToString())
                    {
                        LabelContractor.Text = CONST_TRUE;
                    }
                    else
                    {
                        LabelContractor.Text = "";
                    }
                }
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

            dataHeader.LoginID = Session[WebConstants.SESSION_LOGIN_ID].ToString();

            DTO.MasterData masterData = new DTO.MasterData();
            masterData.DataHeader = dataHeader;
            masterData.TableName = WebConstants.TBL_VENDORS;
            masterData.ColumnNames = columnNames;

            TextBox TextBoxCode = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_CODE);
            TextBox TextBoxDescription = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_DESCRIPTION);
            TextBox TextBoxLocation = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_LOCATION);
            DropDownList DropDownListType = (DropDownList)detailsGridViewRow.FindControl(WebConstants.CONTROL_DROPDOWNLIST_TYPE);
            CheckBox CheckBoxContractor = (CheckBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_CHECKBOX_CONTRACTOR);

            masterData.VendorLocation = TextBoxLocation.Text;
            if (TextBoxCode.Text == "")
            {
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.SCREEN_TEAM_CODE_MANDATORY); ;
                return;
            }
            masterData.Code = TextBoxCode.Text;

            if (TextBoxDescription.Text == "")
            {
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.SCREEN_TEAM_MANDATORY); ;
                return;
            }
            masterData.Description = TextBoxDescription.Text;

            masterData.Type = DropDownListType.SelectedValue;

            if (CheckBoxContractor.Checked)
                masterData.IsContractor = CONST_ONE.ToString();
            else
                masterData.IsContractor = CONST_ZERO.ToString();

            if (detailsGridViewRow.Cells[CONST_ZERO].Text != "")
            {
                Label LabelID = (Label)detailsGridViewRow.FindControl(WebConstants.CONTROL_LABEL_ID);
                masterData.MasterDataID = LabelID.Text;
            }

            if (BO.BusinessObjects.AddUpdateVendorDetailsforMasterData(masterData))
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_SUCCESS_SAVE_MESSAGE);
            else
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_SAVE_MESSAGE);

            PopulateData();
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

            if (detailsGridViewRow.Cells[CONST_ZERO].Text == "")
            {
                TextBox TextBoxDescription = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_DESCRIPTION);
                TextBoxDescription.Text = "";
                TextBox TextBoxCode = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_CODE);
                TextBoxCode.Text = "";
                TextBox TextBoxLocation = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_LOCATION);
                TextBoxLocation.Text = "";
                CheckBox CheckBoxContractor = (CheckBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_CHECKBOX_CONTRACTOR);
                CheckBoxContractor.Checked = false;
            }
            else
            {
                setLinkButtons(detailsGridViewRow, false);
            }
        }

        /// <summary>
        /// LinkButtonUpdateDetails_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonUpdateDetails_Click(object sender, CommandEventArgs e)
        {
            setLinkButtons(GridViewMasterDataDetails.Rows[Convert.ToInt32(e.CommandArgument)], true);
        }

        /// <summary>
        /// LinkButtonDeleteDetails_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonDeleteDetails_Click(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow detailsGridViewRow = GridViewMasterDataDetails.Rows[index];

            DTO.MasterData masterData = new DTO.MasterData();
            masterData.TableName = WebConstants.TBL_VENDORS;
            masterData.ColumnNames = columnNames;

            Label LabelID = (Label)detailsGridViewRow.FindControl(WebConstants.CONTROL_LABEL_ID);
            masterData.MasterDataID = LabelID.Text;

            if (BO.BusinessObjects.AddUpdateDetailsforMasterData(masterData))
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_SUCCESS_DELETE_MESSAGE);
            else
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_DELETE_MESSAGE); ;

            PopulateData();
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// PopulateData
        /// </summary>
        private void PopulateData()
        {
            GetMasterDetails();
            PopulateTypeDetails();

            GridViewMasterDataDetails.DataSource = dtMasterDetails;
            GridViewMasterDataDetails.DataBind();
        }

        /// <summary>
        /// PopulateTypeDetails
        /// </summary>
        private void PopulateTypeDetails()
        {
            DTO.MasterData masterData = new DTO.MasterData();
            masterData.TableName = WebConstants.TBL_VENDOR_TYPES;

            dtTypeDetails = BO.BusinessObjects.GetDetailsforMasterData(masterData).Tables[0];
            dtTypeDetails.Rows.RemoveAt(0);

            ViewState[WebConstants.COL_STR_TYPE_TABLE_DETAILS] = dtTypeDetails;
        }

        /// <summary>
        /// GetMasterDetails
        /// </summary>
        private void GetMasterDetails()
        {
            DTO.MasterData masterData = new DTO.MasterData();
            masterData.TableName = WebConstants.TBL_VENDORS;
            masterData.ColumnNames = columnNames;

            dtMasterDetails = BO.BusinessObjects.GetDetailsforMasterData(masterData).Tables[0];

            if (isReadOnly)
                dtMasterDetails.Rows.RemoveAt(CONST_ZERO);
        }

        /// <summary>
        /// setLinkButtons
        /// </summary>
        /// <param name="gridViewRow"></param>
        /// <param name="isSave"></param>
        private void setLinkButtons(GridViewRow gridViewRow, bool isSave)
        {
            LinkButton LinkButtonSave = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_SAVE);
            LinkButtonSave.Visible = isSave;
            LinkButton LinkButtonCancel = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_CANCEL);
            LinkButtonCancel.Visible = isSave;
            LinkButton LinkButtonUpdate = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_UPDATE);
            LinkButtonUpdate.Visible = (!isSave);
            LinkButton LinkButtonDelete = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_DELETE);
            LinkButtonDelete.Visible = (!isSave);

            //TextBoxDescription
            Label LabelDescription = (Label)gridViewRow.FindControl(WebConstants.CONTROL_LABEL_DESCRIPTION);
            LabelDescription.Visible = (!isSave);
            TextBox TextBoxDescription = (TextBox)gridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_DESCRIPTION);
            TextBoxDescription.Visible = isSave;
            TextBoxDescription.Text = LabelDescription.Text;

            //Code
            Label LabelCode = (Label)gridViewRow.FindControl(WebConstants.CONTROL_LABEL_CODE);
            LabelCode.Visible = (!isSave);
            TextBox TextBoxCode = (TextBox)gridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_CODE);
            TextBoxCode.Visible = isSave;
            TextBoxCode.Text = LabelCode.Text;

            //Type
            Label LabelType = (Label)gridViewRow.FindControl(WebConstants.CONTROL_LABEL_TYPE);
            LabelType.Visible = (!isSave);
            DropDownList DropDownListType = (DropDownList)gridViewRow.FindControl(WebConstants.CONTROL_DROPDOWNLIST_TYPE);
            DropDownListType.Visible = isSave;
            if (isSave)
            {
                DropDownListType.DataSource = dtTypeDetails;
                DropDownListType.DataValueField = WebConstants.COL_VENDOR_TYPE_ID;
                DropDownListType.DataTextField = WebConstants.COL_VENDOR_TYPE;
                DropDownListType.DataBind();
                if (LabelType.Text != "")
                    DropDownListType.SelectedIndex = DropDownListType.Items.IndexOf(DropDownListType.Items.FindByText(LabelType.Text));
            }

            //Vendor Location
            Label LabelLocation = (Label)gridViewRow.FindControl(WebConstants.CONTROL_LABEL_LOCATION);
            LabelLocation.Visible = (!isSave);
            TextBox TextBoxLocation = (TextBox)gridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_LOCATION);
            TextBoxLocation.Visible = isSave;
            TextBoxLocation.Text = LabelLocation.Text;

            //IS Contractor
            Label LabelContractor = (Label)gridViewRow.FindControl(WebConstants.CONTROL_LABEL_CONTRACTOR);
            LabelContractor.Visible = (!isSave);
            CheckBox CheckBoxContractor = (CheckBox)gridViewRow.FindControl(WebConstants.CONTROL_CHECKBOX_CONTRACTOR);
            CheckBoxContractor.Visible = isSave;
            if (isSave)
            {
                CheckBoxContractor.Checked = false;
                if (LabelContractor.Text == CONST_ONE.ToString() || LabelContractor.Text == CONST_TRUE)
                    CheckBoxContractor.Checked = true;
            }
        }

        /// <summary>
        /// GetScreenAccess
        /// </summary>
        private void GetScreenAccess(string screenIdentifier)
        {
            if (Session[WebConstants.SESSION_CACHE_SCREEN_ACCESS_DATA_TABLE] != null)
            {
                DataTable dtScreenAccess = (DataTable)Session[WebConstants.SESSION_CACHE_SCREEN_ACCESS_DATA_TABLE];

                DataRow drScreenAccess = dtScreenAccess.Select(WebConstants.COL_SCREEN_IDENTIFIER + " = '" + screenIdentifier + "'")[0];

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
                GridViewMasterDataDetails.Columns[COL_SAVE_UPDATE_NO].Visible = false;
                GridViewMasterDataDetails.Columns[COL_CANCEL_DELETE_NO].Visible = false;
            }
        }

        /// <summary>
        /// SetScreenLabels
        /// </summary>
        /// <param name="dtScreenLabels"></param>
        private void SetScreenLabels()
        {
            PopulateScreenLabels();

            if (Session[WebConstants.SESSION_LOCALE_ID] != null && Session[WebConstants.SESSION_LOCALE_ID].ToString() != WebConstants.DEF_VAL_LOCALE_ID)
            {
                LabelHeader.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelHeader.Text);
                GridViewMasterDataDetails.Columns[CONST_ZERO].HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.SCREEN_SNO);

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
                if (gridViewRow.Cells[CONST_ZERO].Text.ToString() == "0" || gridViewRow.Cells[CONST_ZERO].Text.ToString() == STR_SPACE)
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
        /// GetColumnNames
        /// </summary>
        private ArrayList GetColumnNames(string tableName)
        {
            return BO.BusinessObjects.GetColumnNames(tableName);
        }

        /// <summary>
        /// AddFooterPadding
        /// </summary>
        private void AddFooterPadding(int productCount)
        {
            int spaceCount = (productCount * NUM_EACH_GRID_ROW_DISPLAY_HEIGHT);

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