using System;
using System.Data;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Adobe.Localization.Insights.Common;
using COM = Adobe.Localization.Insights.Common.Common;
using BO = Adobe.Localization.Insights.BusinessLayer;
using DTO = Adobe.Localization.Insights.Common.DataTransferObjects;

namespace Adobe.Localization.Insights.Web.UserControls
{
    /// <summary>
    /// ProvideTeamsFeedbackUserControl
    /// </summary>
    public partial class ProvideTeamsFeedbackUserControl : System.Web.UI.UserControl
    {
        #region Variables

        private DataTable dtTeamFeedbackLogged;
        private DataTable dtTeamFeedbackLoggedFor;
        private DataTable dtVendors;
        private DataTable dtProducts;
        private DataTable dtProductVersions;
        private DataTable dtScreenLabels;
        private DTO.Header dataHeader = new DTO.Header();

        private int colID = 0;

        private bool isReadOnly = true;
        private bool isContractor = false;

        private const string STR_SPACE = "&nbsp;";
        private const string STR_ZERO = "0";

        private const int PAGE_INDEX = 10;
        private const int TAB_INDEX_USER_ROLES = 1;
        private const int NUM_EACH_GRID_ROW_DISPLAY_HEIGHT = 20;

        private const int COL_VIEW_USER_SAVE_UPDATE_NO = 7;
        private const int COL_VIEW_USER_CANCEL_DELETE_NO = 8;

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
                PopulateDropDownListsData();
                PopulateFeedbackDetails();
            }

            dtVendors = (DataTable)ViewState[WebConstants.TBL_VENDORS];
            dtProducts = (DataTable)ViewState[WebConstants.TBL_PRODUCT];
            dtProductVersions = (DataTable)ViewState[WebConstants.TBL_PRODUCT_VERSIONS];

            SetScreenAccess();
        }

        #endregion

        #region Grid Events

        /// <summary>
        /// GridViewFeedback_PageIndexChanging
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewFeedback_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewFeedback.PageIndex = e.NewPageIndex;
            if (!(dtTeamFeedbackLogged.Rows[e.NewPageIndex * 10][colID].ToString() == STR_ZERO || dtTeamFeedbackLogged.Rows[e.NewPageIndex * 10][colID].ToString() == ""))
            {
                DataRow dr = dtTeamFeedbackLogged.NewRow();
                dr[colID] = 0;
                dtTeamFeedbackLogged.Rows.InsertAt(dr, e.NewPageIndex * PAGE_INDEX);
            }
            GridViewFeedback.DataSource = dtTeamFeedbackLogged;
            GridViewFeedback.DataBind();
        }

        /// <summary>
        /// GridViewFeedback_RowDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewFeedback_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                SetGridLinkButtonsDisplayText(e.Row);

                DropDownList DropDownListProduct = (DropDownList)e.Row.FindControl(WebConstants.CONTROL_DROPDOWNLIST_PRODUCT);
                DropDownListProduct.DataSource = dtProducts;
                DropDownListProduct.DataValueField = WebConstants.COL_PRODUCT_ID;
                DropDownListProduct.DataTextField = WebConstants.COL_PRODUCT;
                DropDownListProduct.DataBind();

                Label LabelLoggedBy = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_FEEDBACK_LOGGED_BY);

                if (e.Row.Cells[colID].Text.ToString() == STR_ZERO || e.Row.Cells[colID].Text.ToString() == STR_SPACE)
                {
                    e.Row.Cells[colID].Text = "";
                    SetLinkButtons(e.Row, true);

                    LabelLoggedBy.Text = Session[WebConstants.SESSION_LOGIN_ID].ToString();
                }
                else
                {
                    Label LabelSeverity = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_FEEDBACK_SEVERITY);
                    DropDownList DropDownListSeverity = (DropDownList)e.Row.FindControl(WebConstants.CONTROL_DROPDOWNLIST_FEEDBACK_SEVERITY);
                    DropDownListSeverity.SelectedIndex = DropDownListSeverity.Items.IndexOf(DropDownListSeverity.Items.FindByValue(LabelSeverity.Text));
                    LabelSeverity.Text = DropDownListSeverity.SelectedItem.Text;

                    Label LabelVendor = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_VENDOR);
                    DropDownList DropDownListSelectVendor = (DropDownList)e.Row.FindControl(WebConstants.CONTROL_DROPDOWNLIST_SELECT_VENDOR);
                    DropDownListSelectVendor.DataSource = dtVendors;
                    DropDownListSelectVendor.DataValueField = WebConstants.COL_ID;
                    DropDownListSelectVendor.DataTextField = WebConstants.COL_DESCRIPTION;
                    DropDownListSelectVendor.DataBind();
                    DropDownListSelectVendor.SelectedIndex = DropDownListSelectVendor.Items.IndexOf(DropDownListSelectVendor.Items.FindByValue(LabelVendor.Text));
                    LabelVendor.Text = DropDownListSelectVendor.SelectedItem.Text;

                    Label LabelProductVersion = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_PRODUCT_VERSION);
                    if (LabelProductVersion.Text != "")
                    {
                        string productID = dtProductVersions.Select(WebConstants.COL_PRODUCT_VERSION_ID + "=" + LabelProductVersion.Text)[0][WebConstants.COL_PRODUCT_ID].ToString();
                        DropDownListProduct.SelectedIndex = DropDownListProduct.Items.IndexOf(DropDownListProduct.Items.FindByValue(productID));

                        DropDownList DropDownListProductVersion = (DropDownList)e.Row.FindControl(WebConstants.CONTROL_DROPDOWNLIST_PRODUCT_VERSION);
                        PopulateProductVersionDropDownList(productID, DropDownListProductVersion);

                        DropDownListProductVersion.SelectedIndex = DropDownListProductVersion.Items.IndexOf(DropDownListProductVersion.Items.FindByValue(LabelProductVersion.Text));
                        LabelProductVersion.Text = DropDownListProduct.SelectedItem.Text + " - " + DropDownListProductVersion.SelectedItem.Text;
                        DropDownListProductVersion.Visible = false;
                    }

                    if (LabelLoggedBy.Text.ToUpper() != Session[WebConstants.SESSION_LOGIN_ID].ToString().ToUpper())
                    {
                        LinkButton LinkButtonUpdate = (LinkButton)e.Row.FindControl(WebConstants.CONTROL_LINKBUTTON_UPDATE);
                        LinkButtonUpdate.Enabled = false;
                        LinkButton LinkButtonDelete = (LinkButton)e.Row.FindControl(WebConstants.CONTROL_LINKBUTTON_DELETE);
                        LinkButtonDelete.Enabled = false;
                    }
                }
            }
        }

        /// <summary>
        /// GridViewFeedback_RowUpdating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewFeedback_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
        }

        /// <summary>
        /// GridViewFeedback_RowCancelingEdit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewFeedback_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
        }

        /// <summary>
        /// GridViewLoggedTeamFeedback_PageIndexChanging
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewLoggedTeamFeedback_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewFeedback.PageIndex = e.NewPageIndex;
            //if (!(dtTeamFeedback.Rows[e.NewPageIndex * 10][colID].ToString() == STR_ZERO || dtTeamFeedback.Rows[e.NewPageIndex * 10][colID].ToString() == ""))
            //{
            //    DataRow dr = dtTeamFeedback.NewRow();
            //    dr[colID] = 0;
            //    dtTeamFeedback.Rows.InsertAt(dr, e.NewPageIndex * PAGE_INDEX);
            //}
            GridViewFeedback.DataSource = dtTeamFeedbackLoggedFor;
            GridViewFeedback.DataBind();
        }

        /// <summary>
        /// GridViewLoggedTeamFeedback_RowDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewLoggedTeamFeedback_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label LabelSeverity = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_FEEDBACK_SEVERITY);
                DropDownList DropDownListSeverity = (DropDownList)e.Row.FindControl(WebConstants.CONTROL_DROPDOWNLIST_FEEDBACK_SEVERITY);
                DropDownListSeverity.SelectedIndex = DropDownListSeverity.Items.IndexOf(DropDownListSeverity.Items.FindByValue(LabelSeverity.Text));
                LabelSeverity.Text = DropDownListSeverity.SelectedItem.Text;

                Label LabelProductVersion = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_PRODUCT_VERSION);
                if (LabelProductVersion.Text != "")
                {
                    DataRow dr = dtProductVersions.Select(WebConstants.COL_PRODUCT_VERSION_ID + "=" + LabelProductVersion.Text)[0];
                    LabelProductVersion.Text = dr[WebConstants.COL_PRODUCT_ID].ToString() + " - " + dr[WebConstants.COL_PRODUCT_VERSION].ToString();
                }
            }
        }

        #endregion

        #region GridView Button Click Events

        /// <summary>
        /// SaveFeedbackDetailsLinkButton_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SaveFeedbackDetailsLinkButton_Click(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow detailsGridViewRow = GridViewFeedback.Rows[index];

            DTO.Users userData = new DTO.Users();
            userData.DataHeader = dataHeader;

            TextBox TextBoxIncidentDetails = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_FEEDBACK_INCIDENT_DETAILS);
            DropDownList DropDownListSelectVendor = (DropDownList)detailsGridViewRow.FindControl(WebConstants.CONTROL_DROPDOWNLIST_SELECT_VENDOR);
            DropDownList DropDownListProductVersion = (DropDownList)detailsGridViewRow.FindControl(WebConstants.CONTROL_DROPDOWNLIST_PRODUCT_VERSION);
            DropDownList DropDownListSeverity = (DropDownList)detailsGridViewRow.FindControl(WebConstants.CONTROL_DROPDOWNLIST_FEEDBACK_SEVERITY);

            if (TextBoxIncidentDetails.Text == "")
            {
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.SCREEN_INCIDENT_DETAILS_MANDATORY);
                return;
            }
            userData.IncidentDetails = TextBoxIncidentDetails.Text;

            if (DropDownListProductVersion.Visible)
                userData.ProductVersionID = DropDownListProductVersion.SelectedValue;
            userData.VendorID = DropDownListSelectVendor.SelectedValue;
            userData.Severity = DropDownListSeverity.SelectedValue;

            if (detailsGridViewRow.Cells[colID].Text != "")
            {
                Label LabelTeamFeedbackID = (Label)detailsGridViewRow.FindControl(WebConstants.CONTROL_LABEL_TEAM_FEEDBACK_ID);
                userData.TeamFeedbackID = LabelTeamFeedbackID.Text;
            }

            if (BO.BusinessObjects.AddUpdateTeamFeedback(userData))
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_SUCCESS_SAVE_MESSAGE);
            else
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_SAVE_MESSAGE);

            PopulateFeedbackDetails();
        }

        /// <summary>
        /// UpdateFeedbackDetailsLinkButton_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UpdateFeedbackDetailsLinkButton_Click(object sender, CommandEventArgs e)
        {
            SetLinkButtons(GridViewFeedback.Rows[Convert.ToInt32(e.CommandArgument)], true);
        }

        /// <summary>
        /// CancelFeedbackDetailsLinkButton_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CancelFeedbackDetailsLinkButton_Click(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow detailsGridViewRow = GridViewFeedback.Rows[index];

            if (detailsGridViewRow.Cells[colID].Text != "")
                SetLinkButtons(detailsGridViewRow, false);
            else
                SetLinkButtons(detailsGridViewRow, true);
        }

        /// <summary>
        /// DeleteFeedbackDetailsLinkButton_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DeleteFeedbackDetailsLinkButton_Click(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow detailsGridViewRow = GridViewFeedback.Rows[index];

            DTO.Users userData = new DTO.Users();

            Label LabelTeamFeedbackID = (Label)detailsGridViewRow.FindControl(WebConstants.CONTROL_LABEL_TEAM_FEEDBACK_ID);
            userData.TeamFeedbackID = LabelTeamFeedbackID.Text;

            if (BO.BusinessObjects.AddUpdateTeamFeedback(userData))
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_SUCCESS_DELETE_MESSAGE);
            else
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_DELETE_MESSAGE); ;

            PopulateFeedbackDetails();
        }

        #endregion

        #region DropDownList Events

        /// <summary>
        /// DropDownListLoggedForTeam_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListLoggedForTeam_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            LabelMessage.Text = "";
            PopulateFeedbackDetails();
        }

        /// <summary>
        /// DropDownListProducts_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListProducts_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateProductVersionDropDownList(DropDownListProducts.SelectedValue, DropDownListProductVersions);
            LabelMessage.Text = "";
            PopulateFeedbackDetails();
        }

        /// <summary>
        /// DropDownListProductVersions_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListProductVersions_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            LabelMessage.Text = "";
            PopulateFeedbackDetails();
        }

        /// <summary>
        /// DropDownListProduct_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListProduct_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList dropDownListProduct = (DropDownList)sender;
            GridViewRow gridViewRow = (GridViewRow)dropDownListProduct.NamingContainer;

            DropDownList dropDownListProductVersion = (DropDownList)gridViewRow.FindControl(WebConstants.CONTROL_DROPDOWNLIST_PRODUCT_VERSION);
            PopulateProductVersionDropDownList(dropDownListProduct.SelectedValue, dropDownListProductVersion);
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// PopulateDropDownListsData
        /// </summary>
        private void PopulateDropDownListsData()
        {
            dtVendors = PopulateTypeDetails(WebConstants.TBL_VENDORS, null, true);
            ViewState[WebConstants.TBL_VENDORS] = dtVendors;

            DataTable dtVendorDropDown = dtVendors.Copy();
            dtVendorDropDown = COM.AddEmptyRowtoDataTable(dtVendorDropDown);
            dtVendorDropDown.Rows[0][WebConstants.COL_DESCRIPTION] = WebConstants.DEF_VAL_TEAMS_ALL;
            DropDownListLoggedForTeam.DataSource = dtVendorDropDown;
            DropDownListLoggedForTeam.DataValueField = WebConstants.COL_ID;
            DropDownListLoggedForTeam.DataTextField = WebConstants.COL_DESCRIPTION;
            DropDownListLoggedForTeam.DataBind();

            DTO.Product productData = new DTO.Product();

            dtProductVersions = BO.BusinessObjects.GetProductVersion(productData).Tables[0];
            ViewState[WebConstants.TBL_PRODUCT_VERSIONS] = dtProductVersions;

            DataView dv = dtProductVersions.DefaultView;
            dtProducts = dv.ToTable(true, WebConstants.COL_PRODUCT_ID, WebConstants.COL_PRODUCT);
            dtProducts.Rows.InsertAt(dtProducts.NewRow(), 0);
            ViewState[WebConstants.TBL_PRODUCT] = dtProducts;

            DropDownListProducts.DataSource = dtProducts;
            DropDownListProducts.DataValueField = WebConstants.COL_PRODUCT_ID;
            DropDownListProducts.DataTextField = WebConstants.COL_PRODUCT;
            DropDownListProducts.DataBind();

            DropDownListProducts.SelectedIndex = DropDownListProducts.Items.IndexOf(DropDownListProducts.Items.FindByValue(Session[WebConstants.SESSION_PRODUCT_ID].ToString()));
            PopulateProductVersionDropDownList(DropDownListProducts.SelectedValue, DropDownListProductVersions);
        }

        /// <summary>
        /// PopulateProductVersionDropDownList
        /// </summary>
        private void PopulateProductVersionDropDownList(string productID, DropDownList dropDownList)
        {
            if (productID == "" || productID == "0")
            {
                dropDownList.Items.Clear();
                dropDownList.Visible = false;
            }
            else
            {
                dropDownList.Visible = true;

                DataView dvProductVersions = dtProductVersions.DefaultView;
                dvProductVersions.RowFilter = WebConstants.COL_PRODUCT_ID + "=" + productID;
                dropDownList.DataSource = dvProductVersions;
                dropDownList.DataValueField = WebConstants.COL_PRODUCT_VERSION_ID;
                dropDownList.DataTextField = WebConstants.COL_PRODUCT_VERSION;
                dropDownList.DataBind();

                DropDownListProductVersions.SelectedIndex = DropDownListProductVersions.Items.IndexOf(DropDownListProductVersions.Items.FindByValue(Session[WebConstants.SESSION_PRODUCT_VERSION_ID].ToString()));
            }
        }

        /// <summary>
        /// PopulateTypeDetails
        /// </summary>
        private DataTable PopulateTypeDetails(string tableName, DropDownList dropDownList, bool isCode)
        {
            DTO.MasterData masterData = new DTO.MasterData();
            masterData.TableName = tableName;

            DataTable typeDetailsDataTable = BO.BusinessObjects.GetDetailsforMasterData(masterData).Tables[0];

            typeDetailsDataTable.Rows.RemoveAt(0);

            if (isCode)
                typeDetailsDataTable.Columns.RemoveAt(1);

            typeDetailsDataTable.Columns[0].ColumnName = WebConstants.COL_ID;
            typeDetailsDataTable.Columns[1].ColumnName = WebConstants.COL_DESCRIPTION;

            if (dropDownList != null)
            {
                dropDownList.DataSource = typeDetailsDataTable;
                dropDownList.DataValueField = WebConstants.COL_ID;
                dropDownList.DataTextField = WebConstants.COL_DESCRIPTION;
                dropDownList.DataBind();
            }

            return typeDetailsDataTable;
        }

        /// <summary>
        /// PopulateFeedbackDetails
        /// </summary>
        private void PopulateFeedbackDetails()
        {
            DTO.Users userData = new DTO.Users();
            userData.DataHeader = dataHeader;
            userData.ProductVersionID = DropDownListProductVersions.SelectedValue;
            userData.VendorID = DropDownListLoggedForTeam.SelectedValue;
            userData.UserID = Session[WebConstants.SESSION_USER_ID].ToString();

            dtTeamFeedbackLogged = BO.BusinessObjects.GetTeamFeedbackLogged(userData).Tables[0];
            if (isReadOnly)
                dtTeamFeedbackLogged.Rows.RemoveAt(0);

            GridViewFeedback.DataSource = dtTeamFeedbackLogged;
            GridViewFeedback.DataBind();

            userData.VendorID = Session[WebConstants.SESSION_VENDOR_ID].ToString(); ;
            dtTeamFeedbackLoggedFor = BO.BusinessObjects.GetTeamFeedback(userData).Tables[0];
            dtTeamFeedbackLoggedFor = COM.AddSequenceColumnToDataTable(dtTeamFeedbackLoggedFor);
            GridViewLoggedTeamFeedback.DataSource = dtTeamFeedbackLoggedFor;
            GridViewLoggedTeamFeedback.DataBind();

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

            SetLinkButtonsForTextBoxes(gridViewRow, WebConstants.CONTROL_LABEL_FEEDBACK_INCIDENT_DETAILS, WebConstants.CONTROL_TEXTBOX_FEEDBACK_INCIDENT_DETAILS, isSave);
            SetLinkButtonsForDropDowns(gridViewRow, dtVendors, WebConstants.CONTROL_LABEL_VENDOR, WebConstants.CONTROL_DROPDOWNLIST_SELECT_VENDOR, isSave);
            SetLinkButtonsForDropDowns(gridViewRow, null, WebConstants.CONTROL_LABEL_FEEDBACK_SEVERITY, WebConstants.CONTROL_DROPDOWNLIST_FEEDBACK_SEVERITY, isSave);
            SetLinkButtonsForProductDropDowns(gridViewRow, dtProductVersions, WebConstants.CONTROL_LABEL_PRODUCT_VERSION_ID, WebConstants.CONTROL_DROPDOWNLIST_PRODUCT, WebConstants.CONTROL_DROPDOWNLIST_PRODUCT_VERSION, isSave);
        }

        /// <summary>
        /// SetLinkButtonsForProductDropDowns
        /// </summary>
        /// <param name="gridViewRow"></param>
        /// <param name="labelType"></param>
        /// <param name="dropDownListType"></param>
        /// <param name="isSave"></param>
        private void SetLinkButtonsForProductDropDowns(GridViewRow gridViewRow, DataTable dt, string labelType, string dropDownList, string dropDownListType, bool isSave)
        {
            Label LabelType = (Label)gridViewRow.FindControl(labelType);
            Label LabelProductVersion = (Label)gridViewRow.FindControl(WebConstants.CONTROL_LABEL_PRODUCT_VERSION);
            LabelProductVersion.Visible = (!isSave);
            DropDownList DropDownList = (DropDownList)gridViewRow.FindControl(dropDownList);
            DropDownList DropDownListType = (DropDownList)gridViewRow.FindControl(dropDownListType);

            DropDownList.SelectedIndex = DropDownList.Items.IndexOf(DropDownList.Items.FindByValue(Session[WebConstants.SESSION_PRODUCT_ID].ToString()));

            if (LabelType.Text != "")
            {
                DataRow dr = dt.Select(WebConstants.COL_PRODUCT_VERSION_ID + "=" + LabelType.Text)[0];
                DropDownList.SelectedIndex = DropDownList.Items.IndexOf(DropDownList.Items.FindByValue(dr[WebConstants.COL_PRODUCT_ID].ToString()));
            }

            PopulateProductVersionDropDownList(DropDownList.SelectedValue, DropDownListType);

            if (LabelType.Text != "")
                DropDownListType.SelectedIndex = DropDownListType.Items.IndexOf(DropDownListType.Items.FindByValue(LabelType.Text));

            DropDownList.Visible = isSave;
            DropDownListType.Visible = isSave;
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

            if (dt != null)
            {
                DropDownListType.DataSource = dt;
                DropDownListType.DataValueField = WebConstants.COL_ID;
                DropDownListType.DataTextField = WebConstants.COL_DESCRIPTION;
                DropDownListType.DataBind();
            }

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
            CalendarType.Date = LabelType.Text;
        }

        #endregion

        #region Screen Access and Labels

        /// <summary>
        /// GetScreenAccess
        /// </summary>
        private void GetScreenAccess()
        {
            dataHeader.LoginID = Session[WebConstants.SESSION_LOGIN_ID].ToString();
            isContractor = (bool)Session[WebConstants.SESSION_VENDOR_IS_CONTRACTOR];

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
                GridViewFeedback.Columns[COL_VIEW_USER_SAVE_UPDATE_NO].Visible = false;
                GridViewFeedback.Columns[COL_VIEW_USER_CANCEL_DELETE_NO].Visible = false;
            }

            if (GridViewFeedback.Visible)
                AddFooterPadding(GridViewFeedback.Rows.Count);
            else
                PanelFooterPadding.Visible = false;
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

            if (Session[WebConstants.SESSION_LOCALE_ID] != null && Session[WebConstants.SESSION_LOCALE_ID].ToString() != WebConstants.DEF_VAL_LOCALE_ID)
            {
                LabelHeader.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelHeader.Text);
                PanelSearchFilter.GroupingText = COM.GetScreenLocalizedLabel(dtScreenLabels, PanelSearchFilter.GroupingText);

                foreach (DataControlField field in GridViewFeedback.Columns)
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
            LinkButton LinkButtonViewUserRoles = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_VIEW_USER_ROLES);

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
            if (LinkButtonViewUserRoles != null)
                LinkButtonViewUserRoles.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LinkButtonViewUserRoles.Text);
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