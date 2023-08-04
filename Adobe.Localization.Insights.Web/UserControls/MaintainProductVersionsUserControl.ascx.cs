using System;
using System.Data;
using System.Text;
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
    /// MaintainProductVersionsUserControl
    /// </summary>
    public partial class MaintainProductVersionsUserControl : System.Web.UI.UserControl
    {
        #region Variables

        private Control controlFired;

        private DataTable dtMasterDetails = new DataTable();
        private DataTable dtReleaseTypes;
        private DataTable dtScreenLabels;

        private DTO.Header dataHeader = new DTO.Header();
        private ArrayList columnNames;

        private bool isReadOnly = true;
        private bool isReport = false;
        private bool isContractor = false;

        private const string STR_SPACE = "&nbsp;";
        private const string STR_ZERO = "0";
        private const string STR_DOCUMENT_HYPERLINK = "<a href='{0}' target='_blank'>{0}</a>";
        private const string STR_TAB_PANEL_ABOUT_PRODUCT = "TabPanelAboutProduct";
        private const string STR_TAB_PANEL_PRODUCT_LINKS = "TabPanelImportantLinks";
        private const string STR_UPDATE_ABOUT_PRODUCT_FAILED = "Update About Product Failed";
        private const string STR_DEFINE_ABOUT = "Define About";
        private const string STR_ABOUT_PRODUCT_NOT_DEFINED = "No About Product Defined";
        private const string STR_ABOUT_PRODUCT_NOT_DEFINED_READ_ONLY = "The Product information is not defined.";
        private const string STR_ABOUT_PRODUCT_EDIT_BUTTON = "Button Edit About Product";
        private const string STR_COVERAGE_NOT_DEFINED = "Coverage Not Defined";
        private const string STR_VAL_ISACTIVE = "1";
        private const string STR_VAL_ISCLOSED = "1";
        private const string STR_VAL_YES = "Yes";
        private const string STR_VAL_TRUE = "True";
        private const string STR_USER_FILTER = " OR ProjectRoleCode = 'IQE' OR ProjectRoleCode = 'IPM'";

        private const string CONFIRM_MESSAGE_PRODUCT_VERSION = "Are you certain you want to delete this Product Version?";

        private const int CONST_ZERO = 0;
        private const int COL_GRID_PHASE_SAVE_UPDATE_NO = 9;
        private const int COL_GRID_PHASE_CANCEL_DELETE_NO = 10;
        private const int NUM_EACH_GRID_ROW_DISPLAY_HEIGHT = 20;

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

            if (ViewState[WebConstants.COL_STR_COLUMN_NAMES] != null)
                columnNames = (ArrayList)ViewState[WebConstants.COL_STR_COLUMN_NAMES];
            else
                ViewState[WebConstants.COL_STR_COLUMN_NAMES] = GetColumnNames(WebConstants.TBL_PRODUCT_VERSIONS);

            if (!this.IsPostBack)
            {
                PopulateData();
            }

            dtReleaseTypes = (DataTable)ViewState[WebConstants.TBL_RELEASE_TYPES];
            SetScreenAccess();
        }

        #endregion

        #region Product Version

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
                if (e.Row.Cells[CONST_ZERO].Text.ToString() == STR_ZERO || e.Row.Cells[CONST_ZERO].Text.ToString() == STR_SPACE)
                {
                    e.Row.Cells[CONST_ZERO].Text = "";
                    SetLinkButtons(e.Row, true);
                }
                else
                {
                    LinkButton LinkButtonDelete = (LinkButton)e.Row.FindControl(WebConstants.CONTROL_LINKBUTTON_DELETE);
                    LinkButtonDelete.OnClientClick = string.Format(WebConstants.CONFIRM_MESSAGE, CONFIRM_MESSAGE_PRODUCT_VERSION);

                    Label LabelActive = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_ACTIVE);
                    if (LabelActive.Text == STR_VAL_ISACTIVE)
                        LabelActive.Text = STR_VAL_YES;
                    else
                        LabelActive.Text = "";

                    Label LabelClosed = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_CLOSED);
                    if (LabelClosed.Text == STR_VAL_ISCLOSED)
                        LabelClosed.Text = STR_VAL_YES;
                    else
                        LabelClosed.Text = "";

                    Label LabelReleaseTypeID = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_RELEASE_TYPE_ID);
                    if (LabelReleaseTypeID.Text != "")
                        LabelReleaseTypeID.Text = dtReleaseTypes.Select(WebConstants.COL_ID + " = '" + LabelReleaseTypeID.Text + "'")[0][WebConstants.COL_DESCRIPTION].ToString();
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

            DTO.Product productData = new DTO.Product();
            productData.DataHeader = dataHeader;
            productData.ProductID = Session[WebConstants.SESSION_PRODUCT_ID].ToString();

            DropDownList DropDownListReleaseType = (DropDownList)detailsGridViewRow.FindControl(WebConstants.CONTROL_DROPDOWNLIST_RELEASE_TYPE);
            TextBox TextBoxProductCodeName = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_PRODUCT_CODE);
            TextBox TextBoxProductVersion = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_PRODUCT_VERSION);
            TextBox TextBoxProductYear = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_PRODUCT_YEAR);
            CheckBox CheckBoxActive = (CheckBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_CHECKBOX_ACTIVE);

            productData.ReleaseTypeID = DropDownListReleaseType.SelectedValue;
            if (TextBoxProductVersion.Text == "")
            {
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.SCREEN_PRODUCT_VERSION_MANDATORY);
                return;
            }
            productData.ProductVersion = TextBoxProductVersion.Text;

            if (TextBoxProductCodeName.Text == "")
            {
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.SCREEN_PRODUCT_CODE_MANDATORY);
                return;
            }
            productData.ProductCodeName = TextBoxProductCodeName.Text;

            productData.ProductYear = TextBoxProductYear.Text;
            if (productData.ProductYear == "")
            {
                productData.ProductYear = (System.DateTime.Now.Year + 1).ToString();
            }

            if (CheckBoxActive.Checked)
                productData.IsActive = STR_VAL_ISACTIVE;
            else
                productData.IsActive = STR_ZERO;

            if (detailsGridViewRow.Cells[CONST_ZERO].Text != "")
            {
                Label LabelProductVersionID = (Label)detailsGridViewRow.FindControl(WebConstants.CONTROL_LABEL_PRODUCT_VERSION_ID);
                productData.ProductVersionID = LabelProductVersionID.Text;
            }

            if (BO.BusinessObjects.AddUpdateProductVersion(productData))
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
            LabelMessage.Text = "";

            if (detailsGridViewRow.Cells[CONST_ZERO].Text == "")
            {
                TextBox TextBoxProductVersion = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_PRODUCT_VERSION);
                TextBoxProductVersion.Text = "";
                TextBox TextBoxProductCodeName = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_PRODUCT_CODE);
                TextBoxProductCodeName.Text = "";
                TextBox TextBoxProductYear = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_PRODUCT_YEAR);
                TextBoxProductYear.Text = "";
                CheckBox CheckBoxActive = (CheckBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_CHECKBOX_ACTIVE);
                CheckBoxActive.Checked = false;
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
            LabelMessage.Text = "";
            SetLinkButtons(GridViewMasterDataDetails.Rows[Convert.ToInt32(e.CommandArgument)], true);
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
            LabelMessage.Text = "";

            DTO.MasterData masterData = new DTO.MasterData();
            masterData.TableName = WebConstants.TBL_PRODUCT_VERSIONS;
            masterData.ColumnNames = columnNames;

            Label LabelProductVersionID = (Label)detailsGridViewRow.FindControl(WebConstants.CONTROL_LABEL_PRODUCT_VERSION_ID);
            masterData.MasterDataID = LabelProductVersionID.Text;

            if (BO.BusinessObjects.AddUpdateDetailsforMasterData(masterData))
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_SUCCESS_DELETE_MESSAGE);
            else
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_DELETE_MESSAGE); ;

            PopulateData();
        }

        /// <summary>
        /// UpdateVersionDetailsLinkButton_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonViewVersionDetails_Click(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow detailsGridViewRow = GridViewMasterDataDetails.Rows[index];

            Session[WebConstants.SESSION_PRODUCT_VERSION_ID] = ((Label)detailsGridViewRow.FindControl(WebConstants.CONTROL_LABEL_PRODUCT_VERSION_ID)).Text;

            Session.Remove(WebConstants.SESSION_PROJECT_PHASE_ID);
            Session.Remove(WebConstants.SESSION_PHASE_TYPE_ID);
            Session.Remove(WebConstants.SESSION_PRODUCT_SPRINT_ID);

            Session[WebConstants.SESSION_VIEW_PRODUCT_VERSION] = true;

            Session.Remove(WebConstants.SESSION_DATA_TREEVIEW_NODE_COLLECTION);
            Response.Redirect(WebConstants.PAGE_LOCALIZATION_INSIGHT);
            LabelMessage.Text = "";
        }

        #endregion

        #region Button Click Events

        /// <summary>
        /// ButtonCreateProductVersion_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonCreateProductVersion_Click(object sender, EventArgs e)
        {
            SetVisibility(true, false, true);
            LabelMessage.Text = "";
        }

        /// <summary>
        /// ButtonAddNewVersion_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonAddNewVersion_Click(object sender, EventArgs e)
        {
            DTO.Product productData = new DTO.Product();
            productData.DataHeader = dataHeader;
            productData.ProductID = Session[WebConstants.SESSION_PRODUCT_ID].ToString();
            productData.ReleaseTypeID = DropDownListReleaseTypeCreate.SelectedValue;

            if (TextBoxProductVersionCreate.Text == "")
            {
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.SCREEN_PRODUCT_VERSION_MANDATORY);
                return;
            }
            productData.ProductVersion = TextBoxProductVersionCreate.Text;

            if (TextBoxProductCodeNameCreate.Text == "")
            {
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.SCREEN_PRODUCT_CODE_MANDATORY);
                return;
            }
            productData.ProductCodeName = TextBoxProductCodeNameCreate.Text;

            productData.ProductYear = TextBoxProductYearCreate.Text;
            if (productData.ProductYear == "")
            {
                productData.ProductYear = (System.DateTime.Now.Year + 1).ToString();
            }

            if (CheckBoxActiveCreate.Checked)
                productData.IsActive = STR_VAL_ISACTIVE;
            else
                productData.IsActive = STR_ZERO;

            if (BO.BusinessObjects.AddUpdateProductVersion(productData))
            {
                productData.ProductVersionID = BO.BusinessObjects.GetProductVersionWithYear(productData, productData.ProductYear).Tables[0].Rows[0][WebConstants.COL_PRODUCT_VERSION_ID].ToString();

                if (DropDownListPopulateVersions.Items.Count > 0)
                    PopulateDataFromPreviousVersion(productData);

                Session[WebConstants.SESSION_PRODUCT_VERSION_ID] = productData.ProductVersionID;
                Response.Redirect(WebConstants.PAGE_LOCALIZATION_INSIGHT);
            }
            else
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_SAVE_MESSAGE);
        }

        /// <summary>
        /// ButtonCancel_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            SetVisibility(false, false, false);
        }

        /// <summary>
        /// PopulateDataFromPreviousVersion
        /// </summary>
        /// <param name="productData"></param>
        private void PopulateDataFromPreviousVersion(DTO.Product productData)
        {
            productData.CopyProductVersionID = DropDownListPopulateVersions.SelectedValue;

            if (CheckBoxCopyLocales.Checked || CheckBoxCopyPlatforms.Checked || CheckBoxCopyUsers.Checked || CheckBoxCopyPhases.Checked || CheckBoxCopySprints.Checked || CheckBoxCopyProjectBuilds.Checked)
            {
                productData.IsCopyLocales = CheckBoxCopyLocales.Checked;
                productData.IsCopyPlatforms = CheckBoxCopyPlatforms.Checked;
                productData.IsCopyUsers = CheckBoxCopyUsers.Checked;
                productData.IsCopyPhases = CheckBoxCopyPhases.Checked;
                productData.IsCopySprintDetails = CheckBoxCopySprints.Checked;
                productData.IsCopyProjectBuildDetails = CheckBoxCopyProjectBuilds.Checked;

                if (BO.BusinessObjects.CopyProductVersionData(productData))
                    LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_SUCCESS_SAVE_MESSAGE);
                else
                    LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_SAVE_MESSAGE);
            }
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// PopulateData
        /// </summary>
        private void PopulateDropDownLists()
        {
            dtReleaseTypes = BO.BusinessObjects.GetMasterDataWithTypeDetails(WebConstants.TBL_RELEASE_TYPES, true);
            ViewState[WebConstants.TBL_RELEASE_TYPES] = dtReleaseTypes;

            DropDownListReleaseTypeCreate.DataSource = dtReleaseTypes;
            DropDownListReleaseTypeCreate.DataValueField = WebConstants.COL_ID;
            DropDownListReleaseTypeCreate.DataTextField = WebConstants.COL_DESCRIPTION;
            DropDownListReleaseTypeCreate.DataBind();
        }

        /// <summary>
        /// GetMasterDetails
        /// </summary>
        private void GetProductVersions(string productID)
        {
            DTO.Product productData = new DTO.Product();
            productData.ProductID = productID;

            dtMasterDetails = BO.BusinessObjects.GetProductVersion(productData).Tables[0];
            DataColumn col = new DataColumn(WebConstants.COL_TEMP_ROW, Type.GetType(WebConstants.STR_SYSTEM_INT32));

            dtMasterDetails.Columns.Add(col);
            for (int rowCount = 0; rowCount < dtMasterDetails.Rows.Count; rowCount++)
            {
                dtMasterDetails.Rows[rowCount][WebConstants.COL_TEMP_ROW] = rowCount + 1;
            }

            if (dtMasterDetails.Rows.Count == 0)
            {
                dtMasterDetails.Rows.Add(dtMasterDetails.NewRow());
            }
        }

        /// <summary>
        /// PopulateData
        /// </summary>
        private void PopulateData()
        {
            PopulateDropDownLists();

            GetProductVersions(Session[WebConstants.SESSION_PRODUCT_ID].ToString());
            //GetProductUserAccess(Session[WebConstants.SESSION_PRODUCT_ID].ToString(), Session[WebConstants.SESSION_USER_ID].ToString());

            GridViewMasterDataDetails.DataSource = dtMasterDetails;
            GridViewMasterDataDetails.DataBind();

            PopulateProductsForCopy();
        }

        #endregion

        #endregion

        #region Populate Product Data for Copy

        #region DropDownList Events

        /// <summary>
        /// DropDownListPopulateProducts_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListPopulateProducts_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownListPopulateProducts.SelectedValue == "0")
            {
                DropDownListPopulateVersions.Enabled = false;
                DropDownListPopulateVersions.Items.Clear();
            }
            else
            {
                DropDownListPopulateVersions.Enabled = true;
                PopulateProductVersions();
            }

            if (DropDownListPopulateVersions.Items.Count == 0)
            {
                CheckBoxCopyLocales.Enabled = false;
                CheckBoxCopyPlatforms.Enabled = false;
                CheckBoxCopyUsers.Enabled = false;
                CheckBoxCopyPhases.Enabled = false;
                CheckBoxCopySprints.Enabled = false;
                CheckBoxCopyProjectBuilds.Enabled = false;

                CheckBoxCopyLocales.Checked = false;
                CheckBoxCopyPlatforms.Checked = false;
                CheckBoxCopyUsers.Checked = false;
                CheckBoxCopyPhases.Checked = false;
                CheckBoxCopySprints.Checked = false;
                CheckBoxCopyProjectBuilds.Checked = false;
            }
            else
            {
                CheckBoxCopyLocales.Enabled = true;
                CheckBoxCopyPlatforms.Enabled = true;
                CheckBoxCopyUsers.Enabled = true;
                CheckBoxCopyPhases.Enabled = true;
                CheckBoxCopySprints.Enabled = true;
                CheckBoxCopyProjectBuilds.Enabled = true;

                CheckBoxCopyLocales.Checked = true;
                CheckBoxCopyPlatforms.Checked = true;
                CheckBoxCopyUsers.Checked = true;
                CheckBoxCopyPhases.Checked = true;
                CheckBoxCopySprints.Checked = true;
                CheckBoxCopyProjectBuilds.Checked = true;
            }
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// PopulateProducts
        /// </summary>
        private void PopulateProductsForCopy()
        {
            DTO.MasterData masterData = new DTO.MasterData();
            masterData.TableName = WebConstants.TBL_PRODUCT;

            DataTable dtProducts = BO.BusinessObjects.GetDetailsforMasterData(masterData).Tables[0];
            DropDownListPopulateProducts.DataSource = dtProducts;
            DropDownListPopulateProducts.DataValueField = WebConstants.COL_PRODUCT_ID;
            DropDownListPopulateProducts.DataTextField = WebConstants.COL_PRODUCT;
            DropDownListPopulateProducts.DataBind();

            //Set Products
            DataRow dr = dtProducts.Select(WebConstants.COL_PRODUCT_ID + " = " + Session[WebConstants.COL_PRODUCT_ID].ToString())[0];

            LabelProductNamePopulate.Text = dr[WebConstants.COL_PRODUCT].ToString();
            LabelProductNameCreate.Text = LabelProductNamePopulate.Text;
            DropDownListPopulateProducts.SelectedIndex = DropDownListPopulateProducts.Items.IndexOf(DropDownListPopulateProducts.Items.FindByText(LabelProductNamePopulate.Text));

            GetProductVersions(DropDownListPopulateProducts.SelectedValue);

            DropDownListPopulateVersions.DataSource = dtMasterDetails;
            DropDownListPopulateVersions.DataValueField = WebConstants.COL_PRODUCT_VERSION_ID;
            DropDownListPopulateVersions.DataTextField = WebConstants.COL_PRODUCT_VERSION;
            DropDownListPopulateVersions.DataBind();
        }

        /// <summary>
        /// PopulateProductVersions
        /// </summary>
        private void PopulateProductVersions()
        {
            GetProductVersions(DropDownListPopulateProducts.SelectedValue);
            DropDownListPopulateVersions.DataSource = dtMasterDetails;
            DropDownListPopulateVersions.DataValueField = WebConstants.COL_PRODUCT_VERSION_ID;
            DropDownListPopulateVersions.DataTextField = WebConstants.COL_PRODUCT_VERSION;
            DropDownListPopulateVersions.DataBind();

            GetProductVersions(DropDownListPopulateProducts.SelectedValue);
        }

        #endregion

        #endregion

        #region Private Functions

        /// <summary>
        /// setLinkButtons
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

            if (gridViewRow.Cells[CONST_ZERO].Text != "")
            {
                LinkButton LinkButtonViewVersionDetails = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_VIEW_VERSION_DETAILS);
                LinkButtonViewVersionDetails.Visible = !isSave;
            }

            //Product Version
            Label LabelProductVersion = (Label)gridViewRow.FindControl(WebConstants.CONTROL_LABEL_PRODUCT_VERSION);
            LabelProductVersion.Visible = (!isSave);
            TextBox TextBoxProductVersion = (TextBox)gridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_PRODUCT_VERSION);
            TextBoxProductVersion.Visible = isSave;
            TextBoxProductVersion.Text = LabelProductVersion.Text;

            //Product Code Name
            Label LabelProductCodeName = (Label)gridViewRow.FindControl(WebConstants.CONTROL_LABEL_PRODUCT_CODE);
            LabelProductCodeName.Visible = (!isSave);
            TextBox TextBoxProductCodeName = (TextBox)gridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_PRODUCT_CODE);
            TextBoxProductCodeName.Visible = isSave;
            TextBoxProductCodeName.Text = LabelProductCodeName.Text;

            //Product Year
            Label LabelProductYear = (Label)gridViewRow.FindControl(WebConstants.CONTROL_LABEL_PRODUCT_YEAR);
            LabelProductYear.Visible = (!isSave);
            TextBox TextBoxProductYear = (TextBox)gridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_PRODUCT_YEAR);
            TextBoxProductYear.Visible = isSave;
            TextBoxProductYear.Text = LabelProductYear.Text;

            //ReleaseTypeID
            SetLinkButtonsForDropDowns(gridViewRow, dtReleaseTypes, WebConstants.CONTROL_LABEL_RELEASE_TYPE_ID, WebConstants.CONTROL_DROPDOWNLIST_RELEASE_TYPE, isSave);

            //Is Active
            Label LabelActive = (Label)gridViewRow.FindControl(WebConstants.CONTROL_LABEL_ACTIVE);
            LabelActive.Visible = (!isSave);
            CheckBox CheckBoxActive = (CheckBox)gridViewRow.FindControl(WebConstants.CONTROL_CHECKBOX_ACTIVE);
            CheckBoxActive.Visible = isSave;
            if (isSave)
            {
                CheckBoxActive.Checked = false;
                if (LabelActive.Text == STR_VAL_ISACTIVE || LabelActive.Text == STR_VAL_TRUE || LabelActive.Text == STR_VAL_YES)
                {
                    CheckBoxActive.Checked = true;
                }
            }
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
        /// GetColumnNames
        /// </summary>
        private ArrayList GetColumnNames(string tableName)
        {
            return BO.BusinessObjects.GetColumnNames(tableName);
        }

        /// <summary>
        /// SetVisibility
        /// </summary>
        /// <param name="isUpdate"></param>
        /// <param name="isPopulate"></param>
        private void SetVisibility(bool isCreate, bool isUpdate, bool isPopulate)
        {
            ButtonCreateProductVersion.Visible = !isCreate && !isUpdate;
            ButtonAddNewVersion.Visible = isCreate;
            ButtonCancel.Visible = isCreate;

            PanelUpdateProductVersions.Visible = !isCreate && !isUpdate;
            PanelCreateProductVersions.Visible = isCreate;
            PanelUpdateProductVersionDetails.Visible = isUpdate;
            PanelPopulateDataFromProducts.Visible = isPopulate;

            SetScreenAccess();
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
                DataRow[] drScreenAccess = dtScreenAccess.Select(WebConstants.COL_SCREEN_IDENTIFIER + " = '" + WebConstants.USER_CONTROL_PRODUCT_VERSION_SCREEN_IDENTIFIER + "'");

                if (drScreenAccess.Length == 1)
                {
                    if (drScreenAccess[0][WebConstants.COL_SCREEN_IS_READ_WRITE].ToString() == WebConstants.DEF_VAL_ACCESS_CHECKED)
                        isReadOnly = false;
                    else if (drScreenAccess[0][WebConstants.COL_SCREEN_IS_REPORT].ToString() == WebConstants.DEF_VAL_ACCESS_CHECKED)
                        isReport = true;
                    else
                        isReport = false;
                }
            }
        }

        /// <summary>
        /// SetScreenAccess
        /// </summary>
        private void SetScreenAccess()
        {
            if (isReadOnly)
            {
                GridViewMasterDataDetails.Columns[COL_GRID_PHASE_SAVE_UPDATE_NO].Visible = false;
                GridViewMasterDataDetails.Columns[COL_GRID_PHASE_CANCEL_DELETE_NO].Visible = false;

                ButtonCreateProductVersion.Visible = false;
                ButtonAddNewVersion.Visible = false;
                ButtonCancel.Visible = false;
            }

            if (ButtonCreateProductVersion.Visible)
                AddFooterPadding(GridViewMasterDataDetails.Rows.Count);
            else if (ButtonAddNewVersion.Visible)
                AddFooterPadding(16);
        }

        /// <summary>
        /// PopulateScreenLabels
        /// </summary>
        /// <param name="userID"></param>
        private void PopulateScreenLabels()
        {
            DTO.Screens screenData = new DTO.Screens();
            screenData.ScreenIdentifier = WebConstants.USER_CONTROL_PRODUCT_VERSION_SCREEN_IDENTIFIER;
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

            if (Session[WebConstants.SESSION_IDENTIFIER] == null)
                Session[WebConstants.SESSION_IDENTIFIER_TEMP] = WebConstants.USER_CONTROL_PRODUCT_VERSION_SCREEN_IDENTIFIER;
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
                LabelProductCreate.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelProductCreate.Text);
                LabelProductVersionCreate.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelProductVersionCreate.Text);
                LabelProductCodeNameCreate.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelProductCodeNameCreate.Text);
                LabelProductYearCreate.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelProductYearCreate.Text);
                LabelActiveCreate.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelActiveCreate.Text);
                LabelPopulateDataFrom.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelPopulateDataFrom.Text);
                LabelPopulateProducts.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelPopulateProducts.Text);
                LabelPopulateversions.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelPopulateversions.Text);
                LabelCopyData.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelCopyData.Text);

                ButtonCreateProductVersion.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, ButtonCreateProductVersion.Text);
                ButtonAddNewVersion.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, ButtonAddNewVersion.Text);
                ButtonCancel.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, ButtonCancel.Text);

                CheckBoxCopyLocales.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, CheckBoxCopyLocales.Text);
                CheckBoxCopyPlatforms.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, CheckBoxCopyPlatforms.Text);
                CheckBoxCopyUsers.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, CheckBoxCopyUsers.Text);
                CheckBoxCopyPhases.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, CheckBoxCopyPhases.Text);
                CheckBoxCopySprints.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, CheckBoxCopySprints.Text);
                CheckBoxCopyProjectBuilds.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, CheckBoxCopyProjectBuilds.Text);

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
            LinkButton LinkButtonViewVersionDetails = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_VIEW_VERSION_DETAILS);

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
            if (LinkButtonViewVersionDetails != null)
                LinkButtonViewVersionDetails.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LinkButtonViewVersionDetails.Text);
        }

        /// <summary>
        /// AddFooterPadding
        /// </summary>
        private void AddFooterPadding(int rowsCount)
        {
            PanelFooterPadding.Controls.Clear();

            int spaceCount = (rowsCount * NUM_EACH_GRID_ROW_DISPLAY_HEIGHT);

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

    ///// <summary>
    ///// GridViewPhaseTemplate
    ///// </summary>
    //public class GridViewPhaseTemplate : ITemplate
    //{
    //    ListItemType _templateType;
    //    DataRow _drtemplateField;

    //    /// <summary>
    //    /// GridViewTemplate
    //    /// </summary>
    //    /// <param name="type"></param>
    //    /// <param name="drtemplateField"></param>
    //    public GridViewPhaseTemplate(ListItemType type, DataRow drtemplateField)
    //    {
    //        _templateType = type;
    //        _drtemplateField = drtemplateField;
    //    }

    //    /// <summary>
    //    /// InstantiateIn
    //    /// </summary>
    //    /// <param name="container"></param>
    //    void ITemplate.InstantiateIn(System.Web.UI.Control container)
    //    {
    //        switch (_templateType)
    //        {
    //            case ListItemType.Header:
    //                Label lbCol = new Label();
    //                lbCol.ID = "Label" + _drtemplateField[WebConstants.COL_PROJECT_PHASE].ToString();
    //                //lbCol.Text = "'<%# Bind(\"" + _drtemplateField["GridHeader"].ToString() + "\") %>'";
    //                lbCol.DataBinding += new EventHandler(LabelDataBinding);

    //                container.Controls.Add(lbCol);        //Adds the newly created label control to the container.
    //                break;
    //        }
    //    }

    //    /// <summary>
    //    /// LabelDataBinding
    //    /// </summary>
    //    /// <param name="sender"></param>
    //    /// <param name="e"></param>
    //    private void LabelDataBinding(object sender, EventArgs e)
    //    {
    //        Label lbl = (Label)sender;
    //        GridViewRow container = (GridViewRow)lbl.NamingContainer;
    //        object dataValue = DataBinder.Eval(container.DataItem, _drtemplateField[WebConstants.COL_PROJECT_PHASE].ToString());
    //        if (dataValue != DBNull.Value)
    //        {
    //            lbl.Text = dataValue.ToString();
    //        }
    //    }
    //}
}