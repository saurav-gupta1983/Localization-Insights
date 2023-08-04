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
    /// ViewProjectPhasesDetailsUserControl
    /// </summary>
    public partial class ViewProjectPhasesDetailsUserControl : System.Web.UI.UserControl
    {
        #region Variables

        private Control controlFired;

        private DataTable dtProjectLocales = new DataTable();
        private DataTable dtProjectPlatforms = new DataTable();
        private DataTable dtPhaseTypes;
        private DataTable dtProductSprints;
        private DataTable dtTestingTypes;
        private DataTable dtStatus;
        private DataTable dtVendors;
        private DataTable dtScreenLabels;

        private ArrayList columnNames;
        private DTO.Header dataHeader = new DTO.Header();

        private const int CONST_ZERO = 0;

        private bool isReadOnly = true;
        private bool isReport = false;
        private bool isContractor = false;
        private bool isProductVersionActive = false;

        private const string STR_NON_ACTIVE = "Not Active";
        private const string STR_SPACE = "&nbsp;";
        private const string STR_ZERO = "0";
        private const string STR_TAB_PANEL_LOCALES_VS_PLATFORMS_MATRIX = "TabPanelLocalesVsPlatforms";
        private const string STR_TAB_PANEL_LOCALES_AND_PLATFORMS = "TabPanelLocalesAndPlatforms";
        private const string STR_TAB_PANEL_ABOUT_PROJECT_PHASE = "TabPanelAboutProjectPhase";
        private const string STR_UPDATE_ABOUT_PHASE_FAILED = "Update About Phase Failed";
        private const string STR_ABOUT_PHASE_NOT_DEFINED = "No About Phase Defined";
        private const string STR_ABOUT_PHASE_NOT_DEFINED_READ_ONLY = "The Project Phase information is not defined.";
        private const string STR_DEFINE_ABOUT = "Define About";
        private const string STR_ABOUT_PHASE_EDIT_BUTTON = "Button Edit About Phase";
        private const string STR_VAL_ISACTIVE = "1";
        private const string STR_VAL_ISCLOSED = "1";
        private const string STR_VAL_YES = "Yes";
        private const string STR_VAL_TRUE = "True";
        private const string STR_NA = "NA";

        private const int TAB_INDEX_LOCALES_VS_PLATFORM_MATRIX = 0;
        private const int TAB_INDEX_LOCALES_AND_PLATFORMS = 1;
        private const int TAB_INDEX_ABOUT_PROJECT_PHASE = 2;
        private const int NUM_EACH_GRID_ROW_DISPLAY_HEIGHT = 20;

        private const int COL_GRID_PHASE_SAVE_UPDATE_NO = 11;
        private const int COL_GRID_PHASE_CANCEL_DELETE_NO = 12;
        private const int COL_GRID_COVERAGE_SAVE_UPDATE_NO = 5;
        private const int COL_GRID_COVERAGE_CANCEL_DELETE_NO = 6;
        private const int COL_GRID_LOCALES_VENDOR_NO = 5;

        private const string CONFIRM_MESSAGE_PROJECT_PHASE = "Are you certain you want to delete this Project Phase?";
        private const string CONFIRM_MESSAGE_LOCALES_PLATFORMS = "Are you certain you want to update Locales/Platforms as any removal will cause updations to OT Matrix?";

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
                ViewState[WebConstants.COL_STR_COLUMN_NAMES] = GetColumnNames(WebConstants.TBL_PROJECT_PHASES);
                PopulateData();
            }
            else
            {
                controlFired = GetPostBackControl(Page);
            }

            columnNames = (ArrayList)ViewState[WebConstants.COL_STR_COLUMN_NAMES];
            dtPhaseTypes = (DataTable)ViewState[WebConstants.TBL_PHASE_TYPES];
            dtTestingTypes = (DataTable)ViewState[WebConstants.TBL_VENDOR_TYPES];
            dtStatus = (DataTable)ViewState[WebConstants.TBL_STATUS];
            dtVendors = (DataTable)ViewState[WebConstants.TBL_VENDORS];
            dtProductSprints = (DataTable)ViewState[WebConstants.TBL_PRODUCT_SPRINTS];
            isProductVersionActive = (bool)ViewState[WebConstants.STR_IS_PRODUCT_VERSION_ACTIVE];

            if (LoadLocalesVsPlatformsMatrixControl.Controls.Count == 1 && PanelPopulateVersionDetailsData.Visible)
                PopulateLocalesVsPlatformsMatrix();

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
                if (controlFired.ClientID.Contains(STR_TAB_PANEL_LOCALES_VS_PLATFORMS_MATRIX))
                    TabContainerPopulateVersionDetailsData.ActiveTabIndex = TAB_INDEX_LOCALES_VS_PLATFORM_MATRIX;
                else if (controlFired.ClientID.Contains(STR_TAB_PANEL_LOCALES_AND_PLATFORMS))
                    TabContainerPopulateVersionDetailsData.ActiveTabIndex = TAB_INDEX_LOCALES_AND_PLATFORMS;
                else
                    TabContainerPopulateVersionDetailsData.ActiveTabIndex = TAB_INDEX_ABOUT_PROJECT_PHASE;
            }
        }

        #endregion

        #region Project Phases

        #region Button Click Events

        /// <summary>
        /// ButtonViewProjectPhases_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonViewProjectPhases_Click(object sender, EventArgs e)
        {
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// PopulateProductVersion
        /// </summary>
        private void PopulateProductVersion()
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
        private void PopulateData()
        {
            PopulateProductVersion();
            PopulateProjectPhases();
        }

        /// <summary>
        /// UpdateProjectPhaseDetails
        /// </summary>
        private bool UpdateProjectPhaseDetails()
        {
            DTO.ProjectPhases projectPhaseData = new DTO.ProjectPhases();
            projectPhaseData.IsUpdatePhaseDetails = true;

            projectPhaseData = UpdateAboutProjectPhaseDetails(projectPhaseData);
            projectPhaseData = UpdateProjectPhaseLocalesAndPlatforms(projectPhaseData);

            return BO.BusinessObjects.UpdateProjectPhaseDetails(projectPhaseData);
        }

        #endregion

        #endregion

        #region TabPanelAboutProjectPhase

        #region Grid Events

        /// <summary>
        /// GridViewPhaseCoverage_RowDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewPhaseCoverage_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                SetGridLinkButtonsDisplayText(e.Row);

                if (e.Row.Cells[CONST_ZERO].Text.ToString() == STR_ZERO || e.Row.Cells[CONST_ZERO].Text.ToString() == STR_SPACE)
                {
                    e.Row.Cells[CONST_ZERO].Text = "";
                    SetLinkButtons_PhaseCoverages(e.Row, true);
                }
            }
        }

        /// <summary>
        /// GridViewPhaseCoverage_RowUpdating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewPhaseCoverage_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
        }

        /// <summary>
        /// GridViewPhaseCoverage_RowCancelingEdit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewPhaseCoverage_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
        }

        #endregion

        #region Grid Button Click Events

        /// <summary>
        /// LinkButtonSaveCoverageDetails_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonSaveCoverageDetails_Click(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow detailsGridViewRow = GridViewPhaseCoverage.Rows[index];

            DTO.ProjectPhases projectPhaseData = new DTO.ProjectPhases();
            projectPhaseData.DataHeader = dataHeader;
            projectPhaseData.ProjectPhaseID = LabelProjectPhaseID.Text;

            TextBox TextBoxCoverageDetails = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_PROJECT_PHASE_COVERAGE_DETAILS);
            TextBox TextBoxSuiteID = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_TEST_SUITE_ID);
            TextBox TextBoxTestCasesCount = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_TEST_CASES_COUNT);

            if (TextBoxCoverageDetails.Text == "")
            {
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.SCREEN_PROJECT_PHASE_COVERAGE_MANDATORY);
                return;
            }
            projectPhaseData.ProjectPhaseCoverage = TextBoxCoverageDetails.Text;
            projectPhaseData.SuiteID = TextBoxSuiteID.Text;
            projectPhaseData.TestCasesCount = TextBoxTestCasesCount.Text;

            if (detailsGridViewRow.Cells[CONST_ZERO].Text != "")
            {
                Label LabelPhaseCoverageDetailID = (Label)detailsGridViewRow.FindControl(WebConstants.CONTROL_LABEL_PROJECT_PHASE_COVERAGE_DETAIL_ID);
                projectPhaseData.PhaseCoverageDetailID = LabelPhaseCoverageDetailID.Text;
            }

            if (BO.BusinessObjects.AddUpdateProjectPhaseCoverages(projectPhaseData))
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_SUCCESS_SAVE_MESSAGE);
            else
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_SAVE_MESSAGE);

            PopulateCoverageDetails();
        }

        /// <summary>
        /// LinkButtonCancelCoverageDetails_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonCancelCoverageDetails_Click(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow detailsGridViewRow = GridViewPhaseCoverage.Rows[index];
            LabelMessage.Text = "";
            if (detailsGridViewRow.Cells[CONST_ZERO].Text == "")
                SetLinkButtons_PhaseCoverages(detailsGridViewRow, true);
            else
                SetLinkButtons_PhaseCoverages(detailsGridViewRow, false);
        }

        /// <summary>
        /// LinkButtonUpdateCoverageDetails_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonUpdateCoverageDetails_Click(object sender, CommandEventArgs e)
        {
            LabelMessage.Text = "";
            SetLinkButtons_PhaseCoverages(GridViewPhaseCoverage.Rows[Convert.ToInt32(e.CommandArgument)], true);
        }

        /// <summary>
        /// LinkButtonDeleteCoverageDetails_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonDeleteCoverageDetails_Click(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow detailsGridViewRow = GridViewPhaseCoverage.Rows[index];

            DTO.ProjectPhases projectPhaseData = new DTO.ProjectPhases();
            Label LabelPhaseCoverageDetailID = (Label)detailsGridViewRow.FindControl(WebConstants.CONTROL_LABEL_PROJECT_PHASE_COVERAGE_DETAIL_ID);
            projectPhaseData.PhaseCoverageDetailID = LabelPhaseCoverageDetailID.Text;

            if (BO.BusinessObjects.AddUpdateProjectPhaseCoverages(projectPhaseData))
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_SUCCESS_DELETE_MESSAGE);
            else
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_DELETE_MESSAGE); ;

            PopulateCoverageDetails();
        }

        #endregion

        #region DropDownList Button Events

        /// <summary>
        /// DropDownListCoverages_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListCoverages_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateLocalesVsPlatformsMatrix();
        }

        #endregion

        #region Button Click Events

        /// <summary>
        /// ButtonEditAboutProjectPhase_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonEditAboutProjectPhase_Click(object sender, EventArgs e)
        {
            ButtonEditAboutProjectPhase.Visible = false;
            TextBoxAboutProjectPhase.Visible = true;
            ButtonSaveAboutPhaseDetails.Visible = true;
            LabelAboutProjectPhase.Visible = false;
            //TextBoxAboutProjectPhase.Text = LabelAboutProjectPhase.Text;
            LabelMessage.Text = "";
        }

        /// <summary>
        /// ButtonSavePhaseDetails_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSavePhaseDetails_Click(object sender, EventArgs e)
        {
            if (UpdateProjectPhaseDetails())
            {
                ButtonSaveAboutPhaseDetails.Visible = false;
                TextBoxAboutProjectPhase.Visible = false;
                ButtonEditAboutProjectPhase.Visible = true;
                LabelAboutProjectPhase.Visible = true;
                LabelAboutProjectPhase.Text = TextBoxAboutProjectPhase.Text;
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_SUCCESS_SAVE_MESSAGE);

                SetAboutProjectPhase();
                PopulateLocalesAndPlatforms();
                PopulateLocalesVsPlatformsMatrix();
            }
            else
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_SAVE_MESSAGE);

        }

        #endregion

        #region Private Functions

        /// <summary>
        /// PopulateProjectPhasesDetails
        /// </summary>
        /// <param name="detailsGridViewRow"></param>
        private void PopulateProjectPhases()
        {
            DTO.ProjectPhases projectPhaseData = new DTO.ProjectPhases();
            projectPhaseData.ProjectPhaseID = Session[WebConstants.SESSION_PROJECT_PHASE_ID].ToString();

            DataTable dtProjectPhase = BO.BusinessObjects.GetProjectPhases(projectPhaseData).Tables[0];

            LabelProjectPhaseID.Text = dtProjectPhase.Rows[0][WebConstants.COL_PROJECT_PHASE_ID].ToString();
            LabelAboutProjectPhase.Text = dtProjectPhase.Rows[0][WebConstants.COL_PROJECT_PHASE_ABOUT].ToString();
            TextBoxAboutProjectPhase.Text = LabelAboutProjectPhase.Text;
            SetAboutProjectPhase();

            LabelProjectPhasePopulateValue.Text = dtProjectPhase.Rows[0][WebConstants.COL_PROJECT_PHASE].ToString();

            PopulateProjectPhasesDetailsData(projectPhaseData);
        }

        /// <summary>
        /// PopulateProjectPhasesDetailsData
        /// </summary>
        private void PopulateProjectPhasesDetailsData(DTO.ProjectPhases projectPhaseData)
        {
            PopulateCoverageDetails();
            PopulateLocalesAndPlatforms();
            PopulateLocalesVsPlatformsMatrix();
        }

        /// <summary>
        /// UpdateAboutProjectPhaseDetails
        /// </summary>
        private DTO.ProjectPhases UpdateAboutProjectPhaseDetails(DTO.ProjectPhases projectPhaseData)
        {
            projectPhaseData.DataHeader = dataHeader;
            projectPhaseData.ProjectPhaseID = LabelProjectPhaseID.Text;
            projectPhaseData.AboutProjectPhase = TextBoxAboutProjectPhase.Text;

            return projectPhaseData;
        }

        /// <summary>
        /// PopulateCoverageDetails
        /// </summary>
        private void PopulateCoverageDetails()
        {
            DTO.ProjectPhases projectPhaseData = new DTO.ProjectPhases();
            projectPhaseData.ProjectPhaseID = LabelProjectPhaseID.Text;

            DataTable dtCoverageDetails = BO.BusinessObjects.GetPhaseCoverageDetails(projectPhaseData).Tables[0];

            DataColumn col = new DataColumn(WebConstants.COL_TEMP_ROW, Type.GetType(WebConstants.STR_SYSTEM_INT32));
            dtCoverageDetails.Columns.Add(col);

            for (int rowCount = 0; rowCount < dtCoverageDetails.Rows.Count; rowCount++)
                dtCoverageDetails.Rows[rowCount][WebConstants.COL_TEMP_ROW] = rowCount + 1;

            if (isReadOnly && dtCoverageDetails.Rows.Count == 0)
                PanelNoCoverageDetails.Visible = true;
            else if (!isReadOnly)
                dtCoverageDetails.Rows.InsertAt(dtCoverageDetails.NewRow(), 0);

            GridViewPhaseCoverage.DataSource = dtCoverageDetails;
            GridViewPhaseCoverage.DataBind();

            DropDownListCoverages.Items.Clear();

            if (dtCoverageDetails.Rows.Count > 0)
            {
                if (dtCoverageDetails.Rows[0][WebConstants.COL_PHASE_COVERAGE_DETAIL_ID].ToString() != "")
                {
                    DataRow drCoverage = dtCoverageDetails.NewRow();
                    drCoverage[WebConstants.COL_PHASE_COVERAGE_DETAIL_ID] = STR_ZERO;
                    drCoverage[WebConstants.COL_PHASE_COVERAGE_DETAILS] = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.DEF_VAL_PHASE_COVERAGES_ALL);
                    dtCoverageDetails.Rows.InsertAt(drCoverage, 0);
                }
                else
                    dtCoverageDetails.Rows[0][WebConstants.COL_PHASE_COVERAGE_DETAILS] = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.DEF_VAL_PHASE_COVERAGES_ALL);

                DropDownListCoverages.DataSource = dtCoverageDetails;
                DropDownListCoverages.DataValueField = WebConstants.COL_PHASE_COVERAGE_DETAIL_ID;
                DropDownListCoverages.DataTextField = WebConstants.COL_PHASE_COVERAGE_DETAILS;
                DropDownListCoverages.DataBind();
            }
            else
                DropDownListCoverages.Enabled = false;
        }

        #endregion

        #endregion

        #region TabPanelLocalesAndPlatforms

        #region Grid Events

        /// <summary>
        /// GridViewPhaseLocales_RowDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewPhaseLocales_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label LabelProductLocaleID = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_PRODUCT_LOCALE_ID);
                Label LabelVendor = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_VENDOR);
                TextBox TextBoxLocaleWeight = (TextBox)e.Row.FindControl(WebConstants.CONTROL_TEXTBOX_LOCALE_WEIGHT);
                CheckBox CheckBoxLocalesSelected = (CheckBox)e.Row.FindControl(WebConstants.CONTROL_CHECKBOX_LOCALES_SELECTED);
                DropDownList DropDownListVendor = (DropDownList)e.Row.FindControl(WebConstants.CONTROL_DROPDOWNLIST_SELECT_VENDOR);

                TextBoxLocaleWeight.Text = STR_ZERO;

                DataView dvVendors = new DataView(dtVendors);
                if (isContractor)
                    dvVendors.RowFilter = WebConstants.COL_ID + " = " + Session[WebConstants.SESSION_VENDOR_ID].ToString();

                DropDownListVendor.DataSource = dvVendors.ToTable();
                DropDownListVendor.DataValueField = WebConstants.COL_ID;
                DropDownListVendor.DataTextField = WebConstants.COL_DESCRIPTION;
                DropDownListVendor.DataBind();

                DataRow[] dr = dtProjectLocales.Select(WebConstants.COL_PRODUCT_LOCALE_ID + " = " + LabelProductLocaleID.Text);
                if (dr.Length == 1)
                {
                    Label LabelProjectLocaleID = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_PROJECT_LOCALE_ID);
                    LabelProjectLocaleID.Text = dr[0][WebConstants.COL_PROJECT_LOCALE_ID].ToString();

                    TextBoxLocaleWeight.Text = dr[0][WebConstants.COL_LOCALE_WEIGHT].ToString();
                    DropDownListVendor.SelectedIndex = DropDownListVendor.Items.IndexOf(DropDownListVendor.Items.FindByValue(dr[0][WebConstants.COL_VENDOR_ID].ToString()));
                    CheckBoxLocalesSelected.Checked = true;
                }

                if (isReadOnly)
                {
                    CheckBoxLocalesSelected.Enabled = false;

                    Label LabelLocaleWeight = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_LOCALE_WEIGHT);
                    LabelLocaleWeight.Text = TextBoxLocaleWeight.Text;
                    LabelLocaleWeight.Visible = true;
                    TextBoxLocaleWeight.Visible = false;

                    LabelVendor.Text = DropDownListVendor.SelectedItem.Text;
                    LabelVendor.Visible = true;
                    DropDownListVendor.Visible = false;
                }

                if (dr.Length == 1)
                {
                    if (isContractor && dr[0][WebConstants.COL_VENDOR_ID].ToString() != Session[WebConstants.SESSION_VENDOR_ID].ToString())
                    {
                        LabelVendor.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, STR_NA);
                        LabelVendor.Visible = true;
                        DropDownListVendor.Visible = false;
                    }
                }
            }
        }

        /// <summary>
        /// GridViewPhasePlatforms_RowDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewPhasePlatforms_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label LabelProductPlatformID = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_PRODUCT_PLATFORM_ID);
                CheckBox CheckBoxPlatformsSelected = (CheckBox)e.Row.FindControl(WebConstants.CONTROL_CHECKBOX_PLATFORMS_SELECTED);

                DataRow[] dr = dtProjectPlatforms.Select(WebConstants.COL_PRODUCT_PLATFORM_ID + " = " + LabelProductPlatformID.Text);
                if (dr.Length == 1)
                {
                    Label LabelProjectPlatformID = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_PROJECT_PLATFORM_ID);
                    LabelProjectPlatformID.Text = dr[0][WebConstants.COL_PROJECT_PLATFORM_ID].ToString();

                    CheckBoxPlatformsSelected.Checked = true;
                }

                if (isReadOnly)
                    CheckBoxPlatformsSelected.Enabled = false;
            }
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// PopulateLocalesAndPlatforms
        /// </summary>
        private void PopulateLocalesAndPlatforms()
        {
            DTO.Product productData = new DTO.Product();
            productData.ProductVersionID = DropDownListProductVersion.SelectedValue;

            PopulateLocales(productData);
            PopulatePlatforms(productData);
        }

        /// <summary>
        /// PopulateLocales
        /// </summary>
        private void PopulateLocales(DTO.Product productData)
        {
            DataTable dtProductVersionLocales = BO.BusinessObjects.GetProductVersionLocales(productData).Tables[0];
            dtProductVersionLocales.Columns.Add(WebConstants.COL_PROJECT_LOCALE_ID);
            dtProductVersionLocales.Columns.Add(WebConstants.COL_VENDOR);
            dtProductVersionLocales.Columns.Add(WebConstants.COL_LOCALE_WEIGHT);

            DTO.ProjectPhases projectPhaseData = new DTO.ProjectPhases();
            projectPhaseData.ProjectPhaseID = LabelProjectPhaseID.Text;

            dtProjectLocales = BO.BusinessObjects.GetProjectLocales(projectPhaseData).Tables[0];

            GridViewPhaseLocales.DataSource = dtProductVersionLocales;
            GridViewPhaseLocales.DataBind();

            if (dtProductVersionLocales.Rows.Count == 0)
                LabelNoLocalesAndPlatforms.Visible = true;
        }

        /// <summary>
        /// PopulatePlatforms
        /// </summary>
        /// <param name="productData"></param>
        private void PopulatePlatforms(DTO.Product productData)
        {
            DataTable dtPlatforms = BO.BusinessObjects.GetProductVersionPlatforms(productData).Tables[0];
            dtPlatforms.Columns.Add(WebConstants.COL_PROJECT_PLATFORM_ID);

            DTO.ProjectPhases projectPhaseData = new DTO.ProjectPhases();
            projectPhaseData.ProjectPhaseID = LabelProjectPhaseID.Text;

            dtProjectPlatforms = BO.BusinessObjects.GetProjectPlatforms(projectPhaseData).Tables[0];

            GridViewPhasePlatforms.DataSource = dtPlatforms;
            GridViewPhasePlatforms.DataBind();

            if (dtPlatforms.Rows.Count == 0)
                LabelNoLocalesAndPlatforms.Visible = true;
        }

        /// <summary>
        /// UpdateProjectPhaseLocalesAndPlatforms
        /// </summary>
        /// <param name="projectPhaseData"></param>
        protected DTO.ProjectPhases UpdateProjectPhaseLocalesAndPlatforms(DTO.ProjectPhases projectPhaseData)
        {
            projectPhaseData.DataHeader = dataHeader;
            projectPhaseData.ProjectLocaleIDCollection = new ArrayList();
            projectPhaseData.ProductLocaleIDCollection = new ArrayList();
            projectPhaseData.LocaleWeightCollection = new ArrayList();
            projectPhaseData.LocaleVendorIDCollection = new ArrayList();
            projectPhaseData.PlatformIDCollection = new ArrayList();

            //Project Locales
            for (int i = 0; i < GridViewPhaseLocales.Rows.Count; i++)
            {
                GridViewRow row = GridViewPhaseLocales.Rows[i];
                if (((CheckBox)row.FindControl(WebConstants.CONTROL_CHECKBOX_LOCALES_SELECTED)).Checked)
                {
                    Label LabelProjectLocaleID = (Label)row.FindControl(WebConstants.CONTROL_LABEL_PROJECT_LOCALE_ID);
                    projectPhaseData.ProjectLocaleIDCollection.Add(LabelProjectLocaleID.Text);

                    Label LabelProductLocaleID = (Label)row.FindControl(WebConstants.CONTROL_LABEL_PRODUCT_LOCALE_ID);
                    projectPhaseData.ProductLocaleIDCollection.Add(LabelProductLocaleID.Text);

                    TextBox TextBoxLocaleWeight = (TextBox)row.FindControl(WebConstants.CONTROL_TEXTBOX_LOCALE_WEIGHT);
                    projectPhaseData.LocaleWeightCollection.Add(TextBoxLocaleWeight.Text);

                    DropDownList DropDownListVendor = (DropDownList)row.FindControl(WebConstants.CONTROL_DROPDOWNLIST_SELECT_VENDOR);
                    projectPhaseData.LocaleVendorIDCollection.Add(DropDownListVendor.SelectedValue);
                }
            }

            //Project Platforms
            for (int i = 0; i < GridViewPhasePlatforms.Rows.Count; i++)
            {
                GridViewRow row = GridViewPhasePlatforms.Rows[i];
                if (((CheckBox)row.FindControl(WebConstants.CONTROL_CHECKBOX_PLATFORMS_SELECTED)).Checked)
                {
                    Label LabelProductPlatformID = (Label)row.FindControl(WebConstants.CONTROL_LABEL_PRODUCT_PLATFORM_ID);
                    projectPhaseData.PlatformIDCollection.Add(LabelProductPlatformID.Text);
                }
            }

            return projectPhaseData;
        }

        #endregion

        #endregion

        #region TabPanelLocalesVsPlatformMatrix

        /// <summary>
        /// PopulateLocalesVsPlatformsMatrix
        /// </summary>
        private void PopulateLocalesVsPlatformsMatrix()
        {
            LoadLocalesVsPlatformsMatrixControl.Controls.Clear();

            bool dataExists = false;
            dtVendors = (DataTable)ViewState[WebConstants.TBL_VENDORS];

            DataView dvVendors = new DataView(dtVendors);
            if (isContractor)
                dvVendors.RowFilter = WebConstants.COL_ID + " = " + Session[WebConstants.SESSION_VENDOR_ID].ToString();

            foreach (DataRow dr in dvVendors.ToTable().Rows)
            {
                DTO.ProjectPhases projectPhasesData = new DTO.ProjectPhases();
                projectPhasesData.ProjectPhaseID = LabelProjectPhaseID.Text;
                projectPhasesData.VendorID = dr[WebConstants.COL_ID].ToString();
                projectPhasesData.PhaseCoverageDetailID = DropDownListCoverages.SelectedValue;

                if (BO.BusinessObjects.GetProjectLocales(projectPhasesData).Tables[0].Rows.Count > 0)
                {
                    LocalesVsPlatformsCombinedMatrixDataUserControl localesVsPlatformsUserControl = (LocalesVsPlatformsCombinedMatrixDataUserControl)this.LoadControl(WebConstants.USER_CONTROL_LOCALES_PLATFORM_MATRIX_DISPLAY);

                    localesVsPlatformsUserControl.VendorID = dr[WebConstants.COL_ID].ToString();
                    localesVsPlatformsUserControl.Vendor = dr[WebConstants.COL_DESCRIPTION].ToString();
                    localesVsPlatformsUserControl.ProjectPhaseID = LabelProjectPhaseID.Text;
                    localesVsPlatformsUserControl.PhaseCoverageDetailID = DropDownListCoverages.SelectedValue;
                    LoadLocalesVsPlatformsMatrixControl.Controls.Add(localesVsPlatformsUserControl);

                    dataExists = true;
                }
            }

            if (!dataExists)
                LabelNoLocalesVsPlatformMatrixDefined.Visible = true;
        }

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
                LinkButton LinkButtonViewPhaseDetails = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_VIEW_PHASE_DETAILS);
                LinkButtonViewPhaseDetails.Visible = !isSave;
            }

            //ProjectPhase
            SetLinkButtonsForTextBoxes(gridViewRow, WebConstants.CONTROL_LABEL_PROJECT_PHASE, WebConstants.CONTROL_TEXTBOX_PROJECT_PHASE, isSave);

            ////TestCasesPlanned
            //SetLinkButtonsForTextBoxes(gridViewRow, WebConstants.CONTROL_LABEL_TEST_CASES_PLANNED, WebConstants.CONTROL_TEXTBOX_TEST_CASES_PLANNED, isSave);

            //PhaseTypeID
            SetLinkButtonsForDropDowns(gridViewRow, dtPhaseTypes, WebConstants.CONTROL_LABEL_PHASE_TYPE_ID, WebConstants.CONTROL_DROPDOWNLIST_PHASE_TYPE, isSave);

            //ProductSprintID
            Label LabelPhaseTypeID = (Label)gridViewRow.FindControl(WebConstants.CONTROL_LABEL_PHASE_TYPE_ID);
            Label LabelPhaseType = (Label)gridViewRow.FindControl(WebConstants.CONTROL_LABEL_PHASE_TYPE);
            DropDownList dropDownListPhaseType = (DropDownList)gridViewRow.FindControl(WebConstants.CONTROL_DROPDOWNLIST_PHASE_TYPE);

            if (isSave && dropDownListPhaseType.SelectedItem.Text == WebConstants.STR_CONST_SPRINTS)
                SetLinkButtonsForDropDowns(gridViewRow, dtProductSprints, WebConstants.CONTROL_LABEL_PRODUCT_SPRINT_ID, WebConstants.CONTROL_DROPDOWNLIST_PRODUCT_SPRINT, isSave);
            else
            {
                DropDownList dropDownListProductSprint = (DropDownList)gridViewRow.FindControl(WebConstants.CONTROL_DROPDOWNLIST_PRODUCT_SPRINT);
                dropDownListProductSprint.Visible = false;
            }

            LabelPhaseType.Visible = false;
            if (LabelPhaseTypeID.Visible)
            {
                LabelPhaseTypeID.Visible = false;
                LabelPhaseType.Visible = true;
            }

            //TestingTypeID
            SetLinkButtonsForDropDowns(gridViewRow, dtTestingTypes, WebConstants.CONTROL_LABEL_TESTING_TYPE_ID, WebConstants.CONTROL_DROPDOWNLIST_TESTING_TYPE, isSave);

            //StatusTypeID
            SetLinkButtonsForDropDowns(gridViewRow, dtStatus, WebConstants.CONTROL_LABEL_STATUS_ID, WebConstants.CONTROL_DROPDOWNLIST_STATUS, isSave);

            //Phase Start Date
            SetLinkButtonsForDateCalendar(gridViewRow, WebConstants.CONTROL_LABEL_PHASE_START_DATE, WebConstants.CONTROL_DATECALENDAR_PHASE_START_DATE, isSave);

            //Phase End Date
            SetLinkButtonsForDateCalendar(gridViewRow, WebConstants.CONTROL_LABEL_PHASE_END_DATE, WebConstants.CONTROL_DATECALENDAR_PHASE_END_DATE, isSave);

        }

        /// <summary>
        /// SetLinkButtons_PhaseCoverages
        /// </summary>
        /// <param name="gridViewRow"></param>
        /// <param name="isSave"></param>
        private void SetLinkButtons_PhaseCoverages(GridViewRow gridViewRow, bool isSave)
        {
            LinkButton LinkButtonSave = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_SAVE);
            LinkButtonSave.Visible = isSave;
            LinkButton LinkButtonCancel = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_CANCEL);
            LinkButtonCancel.Visible = isSave;
            LinkButton LinkButtonUpdate = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_UPDATE);
            LinkButtonUpdate.Visible = (!isSave);
            LinkButton LinkButtonDelete = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_DELETE);
            LinkButtonDelete.Visible = (!isSave);

            SetLinkButtonsForTextBoxes(gridViewRow, WebConstants.CONTROL_LABEL_PROJECT_PHASE_COVERAGE_DETAILS, WebConstants.CONTROL_TEXTBOX_PROJECT_PHASE_COVERAGE_DETAILS, isSave);
            SetLinkButtonsForTextBoxes(gridViewRow, WebConstants.CONTROL_LABEL_TEST_CASES_COUNT, WebConstants.CONTROL_TEXTBOX_TEST_CASES_COUNT, isSave);
            SetLinkButtonsForTextBoxes(gridViewRow, WebConstants.CONTROL_LABEL_TEST_SUITE_ID, WebConstants.CONTROL_TEXTBOX_TEST_SUITE_ID, isSave);
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

            DropDownListType.Items.Clear();

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

            isContractor = (bool)Session[WebConstants.SESSION_VENDOR_IS_CONTRACTOR];

            if (Session[WebConstants.SESSION_CACHE_SCREEN_ACCESS_DATA_TABLE] != null)
            {
                DataTable dtScreenAccess = (DataTable)Session[WebConstants.SESSION_CACHE_SCREEN_ACCESS_DATA_TABLE];
                DataRow[] drScreenAccess = dtScreenAccess.Select(WebConstants.COL_SCREEN_IDENTIFIER + " = '" + WebConstants.USER_CONTROL_PROJECT_PHASE_SCREEN_IDENTIFIER + "'");

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
            //if (Session[WebConstants.SESSION_IDENTIFIER] == null)
            //{
            //    ButtonViewProjectPhases.Visible = false;
            //}

            if (!isProductVersionActive)
                isReadOnly = true;

            if (isReadOnly)
            {
                GridViewPhaseCoverage.Columns[COL_GRID_COVERAGE_SAVE_UPDATE_NO].Visible = false;
                GridViewPhaseCoverage.Columns[COL_GRID_COVERAGE_CANCEL_DELETE_NO].Visible = false;

                ButtonEditAboutProjectPhase.Enabled = false;
                ButtonSaveAboutPhaseDetails.Enabled = false;
                ButtonSaveLocalesAndPlatforms.Enabled = false;
            }
            else
                ButtonSaveLocalesAndPlatforms.OnClientClick = string.Format(WebConstants.CONFIRM_MESSAGE, CONFIRM_MESSAGE_LOCALES_PLATFORMS);

            if (isReport)
            {
                //GridViewPhaseLocales.Columns[4].Visible = false;
                GridViewPhaseLocales.Columns[COL_GRID_LOCALES_VENDOR_NO].Visible = false;
            }

            SetAboutProjectPhase();

            if (GridViewPhaseLocales.Rows.Count < GridViewPhasePlatforms.Rows.Count)
                AddFooterPadding(GridViewPhaseLocales.Rows.Count + 5);
            else
                AddFooterPadding(GridViewPhasePlatforms.Rows.Count + 5);
        }

        /// <summary>
        /// PopulateScreenLabels
        /// </summary>
        /// <param name="userID"></param>
        private void PopulateScreenLabels()
        {
            DTO.Screens screenData = new DTO.Screens();
            screenData.ScreenIdentifier = WebConstants.USER_CONTROL_PROJECT_PHASE_SCREEN_IDENTIFIER;
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
                Session[WebConstants.SESSION_IDENTIFIER_TEMP] = WebConstants.USER_CONTROL_PROJECT_PHASE_SCREEN_IDENTIFIER;
        }

        /// <summary>
        /// SetScreenLabels
        /// </summary>
        /// <param name="dtScreenLabels"></param>
        private void SetScreenLabels()
        {
            PopulateScreenLabels();

            LabelNoCoverageDetails.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelNoCoverageDetails.Text);
            LabelNoLocalesAndPlatforms.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelNoLocalesAndPlatforms.Text);
            LabelNoLocalesVsPlatformMatrixDefined.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelNoLocalesVsPlatformMatrixDefined.Text);

            if (Session[WebConstants.SESSION_LOCALE_ID] != null && Session[WebConstants.SESSION_LOCALE_ID].ToString() != WebConstants.DEF_VAL_LOCALE_ID)
            {
                LabelHeader.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelHeader.Text);
                LabelProduct.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelProduct.Text);
                LabelProductVersion.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelProductVersion.Text);
                LabelProjectPhasePopulate.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelProjectPhasePopulate.Text);

                ButtonViewProjectPhases.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, ButtonViewProjectPhases.Text);
                ButtonSaveAboutPhaseDetails.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, ButtonSaveAboutPhaseDetails.Text);
                ButtonSaveLocalesAndPlatforms.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, ButtonSaveLocalesAndPlatforms.Text);
                ButtonEditAboutProjectPhase.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, ButtonEditAboutProjectPhase.Text);

                TabPanelAboutProjectPhase.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, TabPanelAboutProjectPhase.HeaderText);
                TabPanelLocalesAndPlatforms.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, TabPanelLocalesAndPlatforms.HeaderText);
                TabPanelLocalesVsPlatforms.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, TabPanelLocalesVsPlatforms.HeaderText);

                foreach (DataControlField field in GridViewPhaseLocales.Columns)
                    if (field.HeaderText != "")
                        field.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, field.HeaderText);

                foreach (DataControlField field in GridViewPhasePlatforms.Columns)
                    if (field.HeaderText != "")
                        field.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, field.HeaderText);

                foreach (DataControlField field in GridViewPhaseCoverage.Columns)
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
        /// SetAboutProjectPhase
        /// </summary>
        /// <param name="gridView"></param>
        private void SetAboutProjectPhase()
        {
            if (LabelAboutProjectPhase.Text == "")
            {
                if (isReadOnly)
                    LabelAboutProjectPhase.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, STR_ABOUT_PHASE_NOT_DEFINED_READ_ONLY);
                else
                    LabelAboutProjectPhase.Text = String.Format(COM.GetScreenLocalizedLabel(dtScreenLabels, STR_ABOUT_PHASE_NOT_DEFINED), COM.GetScreenLocalizedLabel(dtScreenLabels, STR_ABOUT_PHASE_EDIT_BUTTON));
            }
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
}