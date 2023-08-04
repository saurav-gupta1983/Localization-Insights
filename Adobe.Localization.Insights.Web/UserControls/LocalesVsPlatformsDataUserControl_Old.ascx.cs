using System;
using System.Web;
using System.Data;
using System.Text;
using System.Collections;
using System.Configuration;
using System.ComponentModel;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Drawing;
using Adobe.Localization.Insights.Common;
using COM = Adobe.Localization.Insights.Common.Common;
using BO = Adobe.Localization.Insights.BusinessLayer;
using DTO = Adobe.Localization.Insights.Common.DataTransferObjects;

namespace Adobe.Localization.Insights.Web.UserControls
{
    /// <summary>
    /// LocalesVsPlatformsDataUserControl
    /// </summary>
    public partial class LocalesVsPlatformsDataUserControl : System.Web.UI.UserControl
    {
        #region Variables

        private DataTable dtprojectPhaseLocales;
        private DataTable dtprojectPhasePlatforms;
        private DataTable dtScreenLabels;
        private DataTable dtMatrixData;

        private DTO.Header dataHeader = new DTO.Header();

        private ArrayList alDisplayLables;
        private ArrayList alDisplayTextBoxes;

        private bool displayOnlyLabels = false;
        private bool isReadOnly = true;
        private bool isReport = false;
        private bool isContractor = false;

        private string productID;
        private const string TC_TS = "TS#";
        private const string TC_SIDS = "SIDs";
        private const string TC_COUNT = "Count";
        private const string TC_NA = "NA";
        private const string TC_EXECUTED = "Executed";
        private const string TC_PERCENT = "Percent";
        private const string TC_REM = "Remaining";
        private const string ARLIST_DISPLAY_LABLES = "DisplayLables";
        private const string ARLIST_DISPLAY_TEXTBOXES = "DisplayTextBoxes";
        private const string CONTENT_PLACEHOLDER_TEXTBOX = "ctl00$ContentPlaceHolder$ctl00$TextBox";
        private const string VIEW_STATE_MATRIX_DATA = "MatrixData";
        private const string PANEL_MATRIX = "PanelMatrix";
        private const string MATRIX_OUTER_TABLE = "MatrixOuterTable";
        private const string HEADING_MATRIX_HEADER = "MatrixHeader";
        private const string HEADING_TEST_CASES = "TestCases";
        private const string HEADING_TOTAL_RUNS = "TotalRuns";

        private const string STR_NOT_SET = "Not Set";
        private const string STR_LABEL_ID_LABELS = "Labels";
        private const string STR_LABEL = "Label";
        private const string STR_TEXTBOX = "TextBox";
        private const string STR_VAL_ISACTIVE = "1";

        private int tdWidthCount;
        private int minWidthCount = 3;
        private const int VAL_MAX_PIXEL_PER = 100;
        private const int VAL_TEXTBOX_PIXEL_PER = 90;
        private const int VAL_GAP_PIXEL_10 = 10;
        private const int VAL_GAP_PIXEL_20 = 20;
        private const int NUM_EACH_GRID_ROW_DISPLAY_HEIGHT = 20;

        private Color COLOR_SELECTED_CELL = Color.LightBlue;
        private Color COLOR_NOT_SELECTED_CELL = Color.LightGray;
        private Color COLOR_BLANK = Color.White;
        private Color tdBgColor = System.Drawing.Color.Black;
        private Color COLOR_TEXT_DISPLAY_TEXT_BLACK = System.Drawing.Color.Black;
        private Color COLOR_TEXT_DISPLAY_TEXT_WHITE = System.Drawing.Color.AntiqueWhite;

        #endregion

        #region Page Load

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            GetAndSetScreenAccess();
            SetScreenLabels();

            if (!IsPostBack)
            {
                PopulateProductVersion();
                //PopulateVendor();
                //PopulateLocaleVsPlatformData();
            }

            dtprojectPhaseLocales = (DataTable)ViewState[WebConstants.TBL_PROJECT_PHASE_LOCALES];
            dtprojectPhasePlatforms = (DataTable)ViewState[WebConstants.TBL_PROJECT_PHASE_PLATFORMS];
            alDisplayLables = (ArrayList)ViewState[ARLIST_DISPLAY_LABLES];
            alDisplayTextBoxes = (ArrayList)ViewState[ARLIST_DISPLAY_TEXTBOXES];

            SetFooter();
        }

        #endregion

        #region Button Events

        /// <summary>
        /// ButtonUpdateLocalesVsPlatformsMatrix_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonUpdateLocalesVsPlatformsMatrix_Click(object sender, EventArgs e)
        {
            if (dtMatrixData == null)
                dtMatrixData = (DataTable)ViewState[VIEW_STATE_MATRIX_DATA];

            DTO.ProjectPhases projectPhaseData = new DTO.ProjectPhases();
            projectPhaseData.DataHeader = dataHeader;
            projectPhaseData.ProjectPhaseID = DropDownListProjectPhases.SelectedValue;
            projectPhaseData.ProjectLocaleVsPlatformMatrix = new ArrayList();

            bool isTSInfoMissing = false;

            foreach (DataRow drLocale in dtprojectPhaseLocales.Rows)
            {
                foreach (DataRow drPlatform in dtprojectPhasePlatforms.Rows)
                {
                    DTO.ProjectLocaleVsPlatformMatrix matrixData = new DTO.ProjectLocaleVsPlatformMatrix();

                    string textBoxID = CONTENT_PLACEHOLDER_TEXTBOX + "_{0}_" + WebConstants.COL_PROJECT_LOCALE_ID + "_" + drLocale[WebConstants.COL_PROJECT_LOCALE_ID] + "_" + WebConstants.COL_PROJECT_PLATFORM_ID + "_" + drPlatform[WebConstants.COL_PROJECT_PLATFORM_ID];

                    DataRow[] drMatrixDataRows = dtMatrixData.Select(WebConstants.COL_PROJECT_LOCALE_ID + " = " + drLocale[WebConstants.COL_PROJECT_LOCALE_ID].ToString() + " AND " + WebConstants.COL_PROJECT_PLATFORM_ID + " = " + drPlatform[WebConstants.COL_PROJECT_PLATFORM_ID].ToString());

                    if (drMatrixDataRows.Length > 0)
                        matrixData.ProjectDataID = drMatrixDataRows[0][WebConstants.COL_PROJECT_DATA_ID].ToString();

                    matrixData.ProjectLocaleID = drLocale[WebConstants.COL_PROJECT_LOCALE_ID].ToString();
                    matrixData.ProjectPlatformID = drPlatform[WebConstants.COL_PROJECT_PLATFORM_ID].ToString();
                    matrixData.UserAccess = DropDownListUserType.SelectedItem.Value;

                    if (alDisplayTextBoxes.Contains(TC_COUNT))
                        matrixData.TC_Count = Request.Form[String.Format(textBoxID, TC_COUNT)];
                    if (alDisplayTextBoxes.Contains(TC_TS))
                    {
                        matrixData.TsNo = Request.Form[String.Format(textBoxID, TC_TS)];
                        if (matrixData.TsNo == "")
                            if (!(matrixData.TC_Count == "" || matrixData.TC_Count == "0"))
                            {
                                isTSInfoMissing = true;
                                break;
                            }
                    }
                    if (alDisplayTextBoxes.Contains(TC_SIDS))
                        matrixData.SIDs = Request.Form[String.Format(textBoxID, TC_SIDS)];
                    if (alDisplayTextBoxes.Contains(TC_PERCENT))
                        matrixData.TC_Percent = Request.Form[String.Format(textBoxID, TC_PERCENT + "_" + TC_COUNT)];
                    if (alDisplayTextBoxes.Contains(TC_NA))
                        matrixData.TC_NA = Request.Form[String.Format(textBoxID, TC_NA)];
                    if (alDisplayTextBoxes.Contains(TC_EXECUTED))
                        matrixData.TC_Executed = Request.Form[String.Format(textBoxID, TC_EXECUTED)];

                    projectPhaseData.ProjectLocaleVsPlatformMatrix.Add(matrixData);
                }
            }

            bool isSuccess = false;
            if (!isTSInfoMissing)
                isSuccess = BO.BusinessObjects.UpdateProjectLocalesVsPlatformsMatrix(projectPhaseData);
            PopulateLocaleVsPlatformData();

            if (isSuccess)
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_SUCCESS_SAVE_MESSAGE);
            else
            {
                if (isTSInfoMissing)
                    LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.SCREEN_MATRIX_SUITE_ID_MANDATORY);
                else
                    LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_SAVE_MESSAGE);

                foreach (DataRow drLocale in dtprojectPhaseLocales.Rows)
                {
                    foreach (DataRow drPlatform in dtprojectPhasePlatforms.Rows)
                    {
                        DTO.ProjectLocaleVsPlatformMatrix matrixData = new DTO.ProjectLocaleVsPlatformMatrix();

                        string textBoxID = CONTENT_PLACEHOLDER_TEXTBOX + "_{0}_" + WebConstants.COL_PROJECT_LOCALE_ID + "_" + drLocale[WebConstants.COL_PROJECT_LOCALE_ID] + "_" + WebConstants.COL_PROJECT_PLATFORM_ID + "_" + drPlatform[WebConstants.COL_PROJECT_PLATFORM_ID];

                        TextBox textBoxControl;

                        if (alDisplayTextBoxes.Contains(TC_TS))
                        {
                            textBoxControl = (TextBox)this.Page.FindControl(String.Format(textBoxID, TC_TS));
                            textBoxControl.Text = Request.Form[String.Format(textBoxID, TC_TS)];
                        }
                        if (alDisplayTextBoxes.Contains(TC_SIDS))
                        {
                            textBoxControl = (TextBox)this.Page.FindControl(String.Format(textBoxID, TC_SIDS));
                            textBoxControl.Text = Request.Form[String.Format(textBoxID, TC_SIDS)];
                        }
                        if (alDisplayTextBoxes.Contains(TC_COUNT))
                        {
                            textBoxControl = (TextBox)this.Page.FindControl(String.Format(textBoxID, TC_COUNT));
                            textBoxControl.Text = Request.Form[String.Format(textBoxID, TC_COUNT)];
                        }
                        if (alDisplayTextBoxes.Contains(TC_PERCENT))
                        {
                            textBoxControl = (TextBox)this.Page.FindControl(String.Format(textBoxID, TC_PERCENT));
                            textBoxControl.Text = Request.Form[String.Format(textBoxID, TC_PERCENT)];
                        }
                        if (alDisplayTextBoxes.Contains(TC_NA))
                        {
                            textBoxControl = (TextBox)this.Page.FindControl(String.Format(textBoxID, TC_NA));
                            textBoxControl.Text = Request.Form[String.Format(textBoxID, TC_NA)];
                        }
                        if (alDisplayTextBoxes.Contains(TC_EXECUTED))
                        {
                            textBoxControl = (TextBox)this.Page.FindControl(String.Format(textBoxID, TC_EXECUTED));
                            textBoxControl.Text = Request.Form[String.Format(textBoxID, TC_EXECUTED)];
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ButtonDistributeTestCases_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonDistributeTestCases_Click(object sender, EventArgs e)
        {
            PopulateLocaleVsPlatformData();
            //int tsCount = 1;
            //if (RadioButtonEqual.Checked)
            //    tsCount = 2;

            //foreach (DataRow drLocale in dtprojectPhaseLocales.Rows)
            //{
            //    foreach (DataRow drPlatform in dtprojectPhasePlatforms.Rows)
            //    {
            //        DTO.ProjectLocaleVsPlatformMatrix matrixData = new DTO.ProjectLocaleVsPlatformMatrix();

            //        string textBoxID = CONTENT_PLACEHOLDER_TEXTBOX + "_{0}_" + WebConstants.COL_PROJECT_LOCALE_ID + "_" + drLocale[WebConstants.COL_PROJECT_LOCALE_ID] + "_" + WebConstants.COL_PROJECT_PLATFORM_ID + "_" + drPlatform[WebConstants.COL_PROJECT_PLATFORM_ID];

            //        TextBox textBoxControl;

            //        if (alDisplayTextBoxes.Contains(TC_TS))
            //        {
            //            textBoxControl = (TextBox)this.Page.FindControl(String.Format(textBoxID, TC_TS));
            //            textBoxControl.Text = Request.Form[String.Format(textBoxID, TC_TS)];
            //            tsCount++;
            //        }
            //        if (alDisplayTextBoxes.Contains(TC_SIDS))
            //        {
            //            textBoxControl = (TextBox)this.Page.FindControl(String.Format(textBoxID, TC_SIDS));
            //            textBoxControl.Text = Request.Form[String.Format(textBoxID, TC_SIDS)];
            //        }
            //        if (alDisplayTextBoxes.Contains(TC_COUNT))
            //        {
            //            textBoxControl = (TextBox)this.Page.FindControl(String.Format(textBoxID, TC_COUNT));
            //            textBoxControl.Text = Request.Form[String.Format(textBoxID, TC_COUNT)];
            //        }
            //        if (alDisplayTextBoxes.Contains(TC_PERCENT))
            //        {
            //            textBoxControl = (TextBox)this.Page.FindControl(String.Format(textBoxID, TC_PERCENT));
            //            textBoxControl.Text = Request.Form[String.Format(textBoxID, TC_PERCENT)];
            //        }
            //        if (alDisplayTextBoxes.Contains(TC_NA))
            //        {
            //            textBoxControl = (TextBox)this.Page.FindControl(String.Format(textBoxID, TC_NA));
            //            textBoxControl.Text = Request.Form[String.Format(textBoxID, TC_NA)];
            //        }
            //        if (alDisplayTextBoxes.Contains(TC_EXECUTED))
            //        {
            //            textBoxControl = (TextBox)this.Page.FindControl(String.Format(textBoxID, TC_EXECUTED));
            //            textBoxControl.Text = Request.Form[String.Format(textBoxID, TC_EXECUTED)];
            //        }
            //    }
            //}
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
            Session[WebConstants.SESSION_MATRIX_SELECTED_PRODUCT_VERSION_ID] = DropDownListProductVersion.SelectedValue;
            Session.Remove(WebConstants.SESSION_MATRIX_SELECTED_PROJECT_PHASE_ID);
            Response.Redirect(WebConstants.PAGE_LOCALIZATION_INSIGHT);
        }

        /// <summary>
        /// DropDownListProjectPhases_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListProjectPhases_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Session[WebConstants.SESSION_MATRIX_SELECTED_PROJECT_PHASE_ID] = DropDownListProjectPhases.SelectedValue;
            Response.Redirect(WebConstants.PAGE_LOCALIZATION_INSIGHT);
        }

        /// <summary>
        /// DropDownListUserType_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListUserType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Session[WebConstants.SESSION_MATRIX_USER_ACCESS] = DropDownListUserType.SelectedValue;
            Response.Redirect(WebConstants.PAGE_LOCALIZATION_INSIGHT);
        }

        /// <summary>
        /// DropDownListSelectVendor_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListSelectVendor_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Session[WebConstants.SESSION_MATRIX_SELECTED_VENDOR_ID] = DropDownListSelectVendor.SelectedValue;
            Response.Redirect(WebConstants.PAGE_LOCALIZATION_INSIGHT);
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
            productData.IsActive = STR_VAL_ISACTIVE;

            DataTable dtProductVersion = BO.BusinessObjects.GetProductVersion(productData).Tables[0];

            if (dtProductVersion.Rows.Count > 0)
            {
                LabelProductValue.Text = dtProductVersion.Rows[0][WebConstants.COL_PRODUCT].ToString();

                DropDownListProductVersion.DataSource = dtProductVersion;
                DropDownListProductVersion.DataValueField = WebConstants.COL_PRODUCT_VERSION_ID;
                DropDownListProductVersion.DataTextField = WebConstants.COL_PRODUCT_VERSION;
                DropDownListProductVersion.DataBind();

                // For Selected Product version
                if (Session[WebConstants.SESSION_MATRIX_SELECTED_PRODUCT_VERSION_ID] != null)
                    DropDownListProductVersion.SelectedIndex = DropDownListProductVersion.Items.IndexOf(DropDownListProductVersion.Items.FindByValue(Session[WebConstants.SESSION_MATRIX_SELECTED_PRODUCT_VERSION_ID].ToString()));
                else if (Session[WebConstants.SESSION_PRODUCT_VERSION_ID] != null)
                    DropDownListProductVersion.SelectedIndex = DropDownListProductVersion.Items.IndexOf(DropDownListProductVersion.Items.FindByValue(Session[WebConstants.SESSION_PRODUCT_VERSION_ID].ToString()));

                PopulateProjectPhases();
                return;
            }
            else
                LabelProductValue.Text = BO.BusinessObjects.GetProducts(productData).Tables[0].Rows[0][WebConstants.COL_PRODUCT].ToString();

            PopulateData();
        }

        /// <summary>
        /// PopulateProjectPhases
        /// </summary>
        private void PopulateProjectPhases()
        {
            DTO.ProjectPhases projectPhaseData = new DTO.ProjectPhases();
            projectPhaseData.ProductVersionID = DropDownListProductVersion.SelectedValue;
            projectPhaseData.IsActive = true;

            DataSet projectPhases = BO.BusinessObjects.GetProjectPhases(projectPhaseData);

            DropDownListProjectPhases.DataSource = projectPhases;
            DropDownListProjectPhases.DataValueField = WebConstants.COL_PROJECT_PHASE_ID;
            DropDownListProjectPhases.DataTextField = WebConstants.COL_PROJECT_PHASE;
            DropDownListProjectPhases.DataBind();

            // For Selected Project Phase
            if (Session[WebConstants.SESSION_MATRIX_SELECTED_PROJECT_PHASE_ID] != null)
                DropDownListProjectPhases.SelectedIndex = DropDownListProjectPhases.Items.IndexOf(DropDownListProjectPhases.Items.FindByValue(Session[WebConstants.SESSION_MATRIX_SELECTED_PROJECT_PHASE_ID].ToString()));
            else if (Session[WebConstants.SESSION_PROJECT_PHASE_ID] != null)
                DropDownListProjectPhases.SelectedIndex = DropDownListProjectPhases.Items.IndexOf(DropDownListProjectPhases.Items.FindByValue(Session[WebConstants.SESSION_PROJECT_PHASE_ID].ToString()));

            if (DropDownListSelectVendor.Items.Count == 0)
                PopulateVendor();
            else
                PopulateLocaleVsPlatformData();
        }

        /// <summary>
        /// PopulateVendor
        /// </summary>
        private void PopulateVendor()
        {
            DTO.ProjectPhases projectPhaseData = new DTO.ProjectPhases();
            projectPhaseData.ProjectPhaseID = DropDownListProjectPhases.SelectedValue;

            DataTable dtVendors = BO.BusinessObjects.GetProjectLocales(projectPhaseData).Tables[0];

            DataView dv = new DataView(dtVendors);
            dtVendors = dv.ToTable(true, WebConstants.COL_VENDOR_ID, WebConstants.COL_VENDOR);

            if (dtVendors.Rows.Count > 1)
            {
                dtVendors.Rows.InsertAt(dtVendors.NewRow(), 0);
                dtVendors.Rows[0][WebConstants.COL_VENDOR_ID] = 0;
                dtVendors.Rows[0][WebConstants.COL_VENDOR] = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.DEF_VAL_VENDOR_ALL);
            }

            DropDownListSelectVendor.DataSource = dtVendors;
            DropDownListSelectVendor.DataValueField = WebConstants.COL_VENDOR_ID;
            DropDownListSelectVendor.DataTextField = WebConstants.COL_VENDOR;
            DropDownListSelectVendor.DataBind();

            // For Selected vendor
            if (Session[WebConstants.SESSION_MATRIX_SELECTED_VENDOR_ID] != null)
                DropDownListSelectVendor.SelectedIndex = DropDownListSelectVendor.Items.IndexOf(DropDownListSelectVendor.Items.FindByValue(Session[WebConstants.SESSION_MATRIX_SELECTED_VENDOR_ID].ToString()));
            else
                DropDownListSelectVendor.SelectedIndex = DropDownListSelectVendor.Items.IndexOf(DropDownListSelectVendor.Items.FindByValue(Session[WebConstants.SESSION_VENDOR_ID].ToString()));

            if (isContractor)
            {
                LabelVendorName.Text = DropDownListSelectVendor.SelectedItem.Text;
                LabelVendorName.Visible = true;
                DropDownListSelectVendor.Visible = false;
            }

            PopulateLocaleVsPlatformData();
        }

        /// <summary>
        /// PopulateTotalTestCases
        /// </summary>
        private void PopulateTotalTestCases()
        {
            DTO.ProjectPhases projectPhaseData = new DTO.ProjectPhases();
            projectPhaseData.ProjectPhaseID = GetSelectedProjectPhaseID();
            projectPhaseData.IsActive = true;

            string tcCount = BO.BusinessObjects.GetPhaseExecutableTestCases(projectPhaseData).Tables[0].Rows[0][0].ToString();

            if (tcCount != "0" && tcCount != "")
                LabelTestCasesValue.Text = tcCount;
            else
                LabelTestCasesValue.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, STR_NOT_SET);
        }

        /// <summary>
        /// PopulateLocaleVsPlatformData
        /// </summary>
        private void PopulateLocaleVsPlatformData()
        {
            PopulateTotalTestCases();
            PopulateData();
        }

        /// <summary>
        /// PopulateProjectPhaseLocales
        /// </summary>
        private void PopulateProjectPhaseLocales(DTO.ProjectPhases projectPhaseData)
        {
            dtprojectPhaseLocales = BO.BusinessObjects.GetProjectLocales(projectPhaseData).Tables[0];

            dtprojectPhaseLocales.Columns.Add(WebConstants.COL_PROJECT_PHASE_TOTAL_COUNT, WebConstants.STR_SYSTEM_INT32.GetType());
            dtprojectPhaseLocales.Columns.Add(WebConstants.COL_PROJECT_PHASE_TOTAL_EXECUTED, WebConstants.STR_SYSTEM_INT32.GetType());
            dtprojectPhaseLocales.Columns.Add(WebConstants.COL_PROJECT_PHASE_TOTAL_NA, WebConstants.STR_SYSTEM_INT32.GetType());
            dtprojectPhaseLocales.Columns.Add(WebConstants.COL_PROJECT_PHASE_TOTAL_REMAINING, WebConstants.STR_SYSTEM_INT32.GetType());

            foreach (DataRow dr in dtprojectPhaseLocales.Rows)
            {
                dr[WebConstants.COL_PROJECT_PHASE_TOTAL_COUNT] = 0;
                dr[WebConstants.COL_PROJECT_PHASE_TOTAL_EXECUTED] = 0;
                dr[WebConstants.COL_PROJECT_PHASE_TOTAL_NA] = 0;
                dr[WebConstants.COL_PROJECT_PHASE_TOTAL_REMAINING] = 0;
            }

            ViewState[WebConstants.TBL_PROJECT_PHASE_LOCALES] = dtprojectPhaseLocales;
        }

        /// <summary>
        /// PopulateProjectPhasePlatforms
        /// </summary>
        private void PopulateProjectPhasePlatforms(DTO.ProjectPhases projectPhaseData)
        {
            dtprojectPhasePlatforms = BO.BusinessObjects.GetProjectPlatforms(projectPhaseData).Tables[0];

            dtprojectPhasePlatforms.Columns.Add(WebConstants.COL_PROJECT_PHASE_TOTAL_COUNT, WebConstants.STR_SYSTEM_INT32.GetType());
            dtprojectPhasePlatforms.Columns.Add(WebConstants.COL_PROJECT_PHASE_TOTAL_EXECUTED, WebConstants.STR_SYSTEM_INT32.GetType());
            dtprojectPhasePlatforms.Columns.Add(WebConstants.COL_PROJECT_PHASE_TOTAL_NA, WebConstants.STR_SYSTEM_INT32.GetType());
            dtprojectPhasePlatforms.Columns.Add(WebConstants.COL_PROJECT_PHASE_TOTAL_REMAINING, WebConstants.STR_SYSTEM_INT32.GetType());

            foreach (DataRow dr in dtprojectPhasePlatforms.Rows)
            {
                dr[WebConstants.COL_PROJECT_PHASE_TOTAL_COUNT] = 0;
                dr[WebConstants.COL_PROJECT_PHASE_TOTAL_EXECUTED] = 0;
                dr[WebConstants.COL_PROJECT_PHASE_TOTAL_NA] = 0;
                dr[WebConstants.COL_PROJECT_PHASE_TOTAL_REMAINING] = 0;
            }

            ViewState[WebConstants.TBL_PROJECT_PHASE_PLATFORMS] = dtprojectPhasePlatforms;
        }

        /// <summary>
        /// PopulateData
        /// </summary>
        private void PopulateData()
        {
            DTO.ProjectPhases projectPhaseData = new DTO.ProjectPhases();
            projectPhaseData.ProjectPhaseID = GetSelectedProjectPhaseID();
            projectPhaseData.VendorID = DropDownListSelectVendor.SelectedValue;

            PopulateProjectPhaseLocales(projectPhaseData);
            PopulateProjectPhasePlatforms(projectPhaseData);

            SetDisplayLabelAndTextboxes();

            Panel panelMatrix = new Panel();
            panelMatrix.ID = PANEL_MATRIX;
            panelMatrix.EnableViewState = true;
            panelMatrix.Controls.Add(CreateLocaleVsPlatformMatrixDynamically());
            WrapperPanelMatrix.Controls.Add(panelMatrix);
            WrapperPanelMatrix.EnableViewState = true;

            if (dtprojectPhaseLocales.Rows.Count == 0 || dtprojectPhasePlatforms.Rows.Count == 0 || DropDownListProductVersion.Items.Count == 0 || DropDownListProjectPhases.Items.Count == 0)
            {
                ButtonUpdateLocalesVsPlatformsMatrix.Enabled = false;
                ButtonDistributeTestCases.Enabled = false;
            }
        }

        /// <summary>
        /// GetSelectedProjectPhaseID
        /// </summary>
        /// <returns></returns>
        private string GetSelectedProjectPhaseID()
        {
            if (DropDownListProjectPhases.SelectedValue == "")
                return "-1";
            else
                return DropDownListProjectPhases.SelectedValue;
        }

        /// <summary>
        /// SetDisplayLabelAndTextboxes
        /// </summary>
        private void SetDisplayLabelAndTextboxes()
        {
            alDisplayTextBoxes = new ArrayList();
            alDisplayLables = new ArrayList();

            if (DropDownListUserType.SelectedItem.Value == WebConstants.STR_OTMATRIX_ACCESS_OWNER)
            {
                alDisplayTextBoxes.Add(TC_TS);
                alDisplayLables.Add(TC_SIDS);
                alDisplayTextBoxes.Add(TC_COUNT);
                alDisplayLables.Add(TC_EXECUTED);
                alDisplayLables.Add(TC_NA);
                alDisplayLables.Add(TC_REM);

            }
            else if (DropDownListUserType.SelectedItem.Value == WebConstants.STR_OTMATRIX_ACCESS_USER)
            {
                alDisplayLables.Add(TC_TS);
                alDisplayTextBoxes.Add(TC_SIDS);
                alDisplayLables.Add(TC_COUNT);
                alDisplayTextBoxes.Add(TC_EXECUTED);
                alDisplayTextBoxes.Add(TC_NA);
                alDisplayLables.Add(TC_REM);
            }
            else if (DropDownListUserType.SelectedItem.Value == WebConstants.STR_OTMATRIX_ACCESS_DISPLAY_ONLY)
            {
                alDisplayLables.Add(TC_TS);
                alDisplayLables.Add(TC_SIDS);
                alDisplayLables.Add(TC_COUNT);
                alDisplayLables.Add(TC_EXECUTED);
                alDisplayLables.Add(TC_NA);
                alDisplayLables.Add(TC_REM);
            }

            ViewState[ARLIST_DISPLAY_LABLES] = alDisplayLables;
            ViewState[ARLIST_DISPLAY_TEXTBOXES] = alDisplayTextBoxes;

        }

        #endregion

        #region Create Locale Vs Platform Natrix

        /// <summary>
        /// CreateTestCasesMatrixDynamically
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        private Table CreateLocaleVsPlatformMatrixDynamically()
        {
            tdWidthCount = dtprojectPhasePlatforms.Rows.Count + minWidthCount;
            tdWidthCount = VAL_MAX_PIXEL_PER / tdWidthCount;

            DTO.ProjectPhases projectPhasesData = new DTO.ProjectPhases();
            projectPhasesData.ProjectPhaseID = DropDownListProjectPhases.SelectedValue;
            projectPhasesData.VendorID = DropDownListSelectVendor.SelectedValue;

            dtMatrixData = BO.BusinessObjects.GetLocaleVsPlatformMatrixData(projectPhasesData).Tables[0];
            ViewState[VIEW_STATE_MATRIX_DATA] = dtMatrixData;

            foreach (DataRow dr in dtMatrixData.Rows)
            {
                dr[WebConstants.COL_TESTCASES_REMAINING] = GetIntegerValue(dr[WebConstants.COL_TESTCASES_COUNT].ToString()) - GetIntegerValue(dr[WebConstants.COL_TESTCASES_EXECUTED].ToString()) - GetIntegerValue(dr[WebConstants.COL_TESTCASES_NA].ToString());
            }

            Table tb = SetTablePropertiesWithBorders();
            tb.ID = MATRIX_OUTER_TABLE;
            TableRow tr = SetTableRowProperties();
            TableCell tdLabel;

            tdLabel = SetTableCellPropertiesWithBorders(AddLabel(WebConstants.COL_PLATFORM_ID + "_-1", COM.GetScreenLocalizedLabel(dtScreenLabels, HEADING_MATRIX_HEADER)));
            tdLabel.BackColor = tdBgColor;
            tdLabel.Height = Unit.Pixel(VAL_GAP_PIXEL_20 * 2);
            tr.Controls.Add(tdLabel);

            tdLabel = SetTableCellPropertiesWithBorders(AddLabel(STR_LABEL_ID_LABELS, COM.GetScreenLocalizedLabel(dtScreenLabels, HEADING_TEST_CASES)));
            tdLabel.BackColor = tdBgColor;
            tr.Controls.Add(tdLabel);

            #region Add Platforms

            foreach (DataRow drPlatform in dtprojectPhasePlatforms.Rows)
            {
                tdLabel = SetTableCellPropertiesWithBorders(AddLabel(WebConstants.COL_PLATFORM_ID + "_" + drPlatform[WebConstants.COL_PROJECT_PLATFORM_ID].ToString(), drPlatform[WebConstants.COL_PLATFORM].ToString()), 1);
                tdLabel.HorizontalAlign = HorizontalAlign.Center;
                tdLabel.BackColor = tdBgColor;
                tr.Controls.Add(tdLabel);
            }
            tb.Controls.Add(tr);

            #endregion

            tdLabel = SetTableCellPropertiesWithBorders(AddLabel(WebConstants.COL_PLATFORM_ID + "_N", COM.GetScreenLocalizedLabel(dtScreenLabels, HEADING_TOTAL_RUNS)));
            tdLabel.BackColor = tdBgColor;
            tdLabel.HorizontalAlign = HorizontalAlign.Center;
            tr.Controls.Add(tdLabel);

            #region Add Locales

            foreach (DataRow drLocale in dtprojectPhaseLocales.Rows)
            {
                tr = SetTableRowProperties();
                string localeIDtext = WebConstants.COL_LOCALE_ID + "_" + drLocale[WebConstants.COL_PROJECT_LOCALE_ID].ToString();
                tdLabel = SetTableCellPropertiesWithBorders(AddLabel(localeIDtext, drLocale[WebConstants.COL_LOCALE].ToString()));
                tdLabel.BackColor = tdBgColor;
                tdLabel.HorizontalAlign = HorizontalAlign.Center;
                tr.Controls.Add(tdLabel);
                //displayLabelcounter = 0;

                tr.Controls.Add(CreateTableCellForDataLabels(localeIDtext, false));
                foreach (DataRow drPlatform in dtprojectPhasePlatforms.Rows)
                {
                    //displayLabelcounter++;
                    DataRow[] drMatrixDataRows = dtMatrixData.Select(WebConstants.COL_PROJECT_LOCALE_ID + " = " + drLocale[WebConstants.COL_PROJECT_LOCALE_ID].ToString() + " AND " + WebConstants.COL_PROJECT_PLATFORM_ID + " = " + drPlatform[WebConstants.COL_PROJECT_PLATFORM_ID].ToString());

                    DataRow drMatrixDataRow;
                    if (drMatrixDataRows.Length == 0)
                    {
                        drMatrixDataRow = dtMatrixData.NewRow();
                        drMatrixDataRow[WebConstants.COL_PROJECT_LOCALE_ID] = drLocale[WebConstants.COL_PROJECT_LOCALE_ID].ToString();
                        drMatrixDataRow[WebConstants.COL_PROJECT_PLATFORM_ID] = drPlatform[WebConstants.COL_PROJECT_PLATFORM_ID].ToString();
                    }
                    else
                        drMatrixDataRow = drMatrixDataRows[0];

                    tr.Controls.Add(CreateTableCellForProjectLocaleVsPlatformData(drMatrixDataRow));

                    drPlatform[WebConstants.COL_PROJECT_PHASE_TOTAL_COUNT] = GetIntegerValue(drPlatform[WebConstants.COL_PROJECT_PHASE_TOTAL_COUNT].ToString()) + GetIntegerValue(drMatrixDataRow[WebConstants.COL_TESTCASES_COUNT].ToString());
                    drPlatform[WebConstants.COL_PROJECT_PHASE_TOTAL_EXECUTED] = GetIntegerValue(drPlatform[WebConstants.COL_PROJECT_PHASE_TOTAL_EXECUTED].ToString()) + GetIntegerValue(drMatrixDataRow[WebConstants.COL_TESTCASES_EXECUTED].ToString());
                    drPlatform[WebConstants.COL_PROJECT_PHASE_TOTAL_NA] = GetIntegerValue(drPlatform[WebConstants.COL_PROJECT_PHASE_TOTAL_NA].ToString()) + GetIntegerValue(drMatrixDataRow[WebConstants.COL_TESTCASES_NA].ToString());
                    drPlatform[WebConstants.COL_PROJECT_PHASE_TOTAL_REMAINING] = GetIntegerValue(drPlatform[WebConstants.COL_PROJECT_PHASE_TOTAL_REMAINING].ToString()) + GetIntegerValue(drMatrixDataRow[WebConstants.COL_TESTCASES_REMAINING].ToString());

                    drLocale[WebConstants.COL_PROJECT_PHASE_TOTAL_COUNT] = GetIntegerValue(drLocale[WebConstants.COL_PROJECT_PHASE_TOTAL_COUNT].ToString()) + GetIntegerValue(drMatrixDataRow[WebConstants.COL_TESTCASES_COUNT].ToString());
                    drLocale[WebConstants.COL_PROJECT_PHASE_TOTAL_EXECUTED] = GetIntegerValue(drLocale[WebConstants.COL_PROJECT_PHASE_TOTAL_EXECUTED].ToString()) + GetIntegerValue(drMatrixDataRow[WebConstants.COL_TESTCASES_EXECUTED].ToString());
                    drLocale[WebConstants.COL_PROJECT_PHASE_TOTAL_NA] = GetIntegerValue(drLocale[WebConstants.COL_PROJECT_PHASE_TOTAL_NA].ToString()) + GetIntegerValue(drMatrixDataRow[WebConstants.COL_TESTCASES_NA].ToString());
                    drLocale[WebConstants.COL_PROJECT_PHASE_TOTAL_REMAINING] = GetIntegerValue(drLocale[WebConstants.COL_PROJECT_PHASE_TOTAL_REMAINING].ToString()) + GetIntegerValue(drMatrixDataRow[WebConstants.COL_TESTCASES_REMAINING].ToString());
                }

                tr.Controls.Add(CreateTableCellForGrandTotalTestCases(drLocale[WebConstants.COL_PROJECT_LOCALE_ID].ToString(), "", drLocale[WebConstants.COL_PROJECT_PHASE_TOTAL_COUNT].ToString(), drLocale[WebConstants.COL_PROJECT_PHASE_TOTAL_EXECUTED].ToString(), drLocale[WebConstants.COL_PROJECT_PHASE_TOTAL_NA].ToString(), drLocale[WebConstants.COL_PROJECT_PHASE_TOTAL_REMAINING].ToString()));

                //tr = CreateTableCellForProjectLocaleVsPlatformData(dtprojectPhasePlatforms, drLocale["ProjectLocaleID"].ToString());
                tb.Controls.Add(tr);
            }

            #endregion

            //displayLabelcounter = 0;
            tr = SetTableRowProperties();
            tdLabel = SetTableCellPropertiesWithBorders(AddLabel(WebConstants.COL_LOCALE_ID + "_N", COM.GetScreenLocalizedLabel(dtScreenLabels, HEADING_TOTAL_RUNS)));
            tdLabel.BackColor = tdBgColor;
            tdLabel.HorizontalAlign = HorizontalAlign.Center;
            tr.Controls.Add(tdLabel);

            //displayLabelcounter = 0;
            int grandTotalCount = 0;
            int grandTotalExecuted = 0;
            int grandTotalNA = 0;
            int grandTotallRemaining = 0;

            tr.Controls.Add(CreateTableCellForDataLabels(WebConstants.COL_PROJECT_LOCALE_ID + "_N_" + WebConstants.COL_PLATFORM, true));
            foreach (DataRow drPlatform in dtprojectPhasePlatforms.Rows)
            {
                //displayLabelcounter++;
                tr.Controls.Add(CreateTableCellForGrandTotalTestCases("", drPlatform[WebConstants.COL_PROJECT_PLATFORM_ID].ToString(), drPlatform[WebConstants.COL_PROJECT_PHASE_TOTAL_COUNT].ToString(), drPlatform[WebConstants.COL_PROJECT_PHASE_TOTAL_EXECUTED].ToString(), drPlatform[WebConstants.COL_PROJECT_PHASE_TOTAL_NA].ToString(), drPlatform[WebConstants.COL_PROJECT_PHASE_TOTAL_REMAINING].ToString()));

                grandTotalCount += GetIntegerValue(drPlatform[WebConstants.COL_PROJECT_PHASE_TOTAL_COUNT].ToString());
                grandTotalExecuted += GetIntegerValue(drPlatform[WebConstants.COL_PROJECT_PHASE_TOTAL_EXECUTED].ToString());
                grandTotalNA += GetIntegerValue(drPlatform[WebConstants.COL_PROJECT_PHASE_TOTAL_NA].ToString());
                grandTotallRemaining += GetIntegerValue(drPlatform[WebConstants.COL_PROJECT_PHASE_TOTAL_REMAINING].ToString());
            }

            tr.Controls.Add(CreateTableCellForGrandTotalTestCases("", "", grandTotalCount.ToString(), grandTotalExecuted.ToString(), grandTotalNA.ToString(), grandTotallRemaining.ToString()));
            tb.Controls.Add(tr);

            return tb;
        }

        /// <summary>
        /// GetIntegerValue
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private int GetIntegerValue(string value)
        {
            if (String.IsNullOrEmpty(value))
                return 0;
            return Convert.ToInt32(value);

        }

        /// <summary>
        /// CreateTableCellForDataLabels
        /// </summary>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        private TableCell CreateTableCellForDataLabels(string localeIDtext, bool IsSkip)
        {
            Table tb = SetTablePropertiesWithBorders();
            TableRow tr;

            if (!IsSkip)
            {
                tr = SetTableRowProperties();
                tr.Controls.Add(SetTableCellProperties(AddLabel(localeIDtext + "_" + TC_TS, TC_TS)));
                //tr.Controls.Add(SetTableCellProperties(AddTextBoxSetVisibility(localeIDtext + "_" + TC_TS, TC_TS, false)));
                tb.Controls.Add(tr);
            }

            tr = SetTableRowProperties();
            tr.Controls.Add(SetTableCellProperties(AddLabel(localeIDtext + "_" + TC_COUNT, TC_COUNT)));
            //tr.Controls.Add(SetTableCellProperties(AddTextBoxSetVisibility(localeIDtext + "_" + TC_COUNT, TC_COUNT, false)));
            tb.Controls.Add(tr);

            if (!IsSkip)
            {
                tr = SetTableRowProperties();
                tr.Controls.Add(SetTableCellProperties(AddLabel(localeIDtext + "_" + TC_SIDS, TC_SIDS)));
                //tr.Controls.Add(SetTableCellProperties(AddTextBoxSetVisibility(localeIDtext + "_" + TC_SIDS, TC_SIDS, false)));
                tb.Controls.Add(tr);
            }

            tr = SetTableRowProperties();
            tr.Controls.Add(SetTableCellProperties(AddLabel(localeIDtext + "_" + TC_EXECUTED, TC_EXECUTED)));
            //tr.Controls.Add(SetTableCellProperties(AddTextBoxSetVisibility(localeIDtext + "_" + TC_EXECUTED, TC_EXECUTED, false)));
            tb.Controls.Add(tr);

            tr = SetTableRowProperties();
            tr.Controls.Add(SetTableCellProperties(AddLabel(localeIDtext + "_" + TC_NA, TC_NA)));
            //tr.Controls.Add(SetTableCellProperties(AddTextBoxSetVisibility(localeIDtext + "_" + TC_NA, TC_NA, false)));
            tb.Controls.Add(tr);

            tr = SetTableRowProperties();
            tr.Controls.Add(SetTableCellProperties(AddLabel(localeIDtext + "_" + TC_REM, TC_REM)));
            //tr.Controls.Add(SetTableCellProperties(AddTextBoxSetVisibility(localeIDtext + "_" + TC_REM, TC_REM, false)));
            tb.Controls.Add(tr);

            return SetTableCellPropertiesWithBorders(tb);
        }

        /// <summary>
        /// CreateTableCellForProjectLocaleVsPlatformData
        /// </summary>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        private TableCell CreateTableCellForProjectLocaleVsPlatformData(DataRow dr)
        {
            Table tb = SetTablePropertiesWithBorders();

            if (dr[WebConstants.COL_TEST_SUITE_NO].ToString() != "")
            {
                tb.BackColor = COLOR_SELECTED_CELL;
            }

            string id = WebConstants.COL_PROJECT_LOCALE_ID + "_" + dr[WebConstants.COL_PROJECT_LOCALE_ID] + "_" + WebConstants.COL_PROJECT_PLATFORM_ID + "_" + dr[WebConstants.COL_PROJECT_PLATFORM_ID];

            if ((DropDownListUserType.SelectedItem.Value == WebConstants.STR_OTMATRIX_ACCESS_OWNER) || dr[WebConstants.COL_TEST_SUITE_NO].ToString() != "")
            {
                tb.Controls.Add(CreateTableCellForData(id, TC_TS, dr[WebConstants.COL_TEST_SUITE_NO].ToString()));
                tb.Controls.Add(CreateTableCellForData(id, TC_COUNT, dr[WebConstants.COL_TESTCASES_COUNT].ToString()));
                tb.Controls.Add(CreateTableCellForData(id, TC_SIDS, dr[WebConstants.COL_TEST_SUITE_IDS].ToString()));
                // tb.Controls.Add(CreateTableCellForTCCount(id, TC_COUNT, dr["TestCasesCount"].ToString(), dr["TestCasesPercent"].ToString()));
                tb.Controls.Add(CreateTableCellForData(id, TC_EXECUTED, dr[WebConstants.COL_TESTCASES_EXECUTED].ToString()));
                tb.Controls.Add(CreateTableCellForData(id, TC_NA, dr[WebConstants.COL_TESTCASES_NA].ToString()));
                tb.Controls.Add(CreateTableCellForData(id, TC_REM, dr[WebConstants.COL_TESTCASES_REMAINING].ToString()));
                return SetTableCellPropertiesWithBorders(tb);
            }
            return ShowBlankCells();
        }

        /// <summary>
        /// CreateTableCellForGrandTotalTestCases
        /// </summary>
        /// <param name="localeID"></param>
        /// <param name="platformID"></param>
        /// <param name="tcCount"></param>
        /// <param name="tcExecuted"></param>
        /// <param name="tcNA"></param>
        /// <param name="tcRemaining"></param>
        /// <returns></returns>
        private TableCell CreateTableCellForGrandTotalTestCases(string localeID, string platformID, string tcCount, string tcExecuted, string tcNA, string tcRemaining)
        {
            Table tb = SetTablePropertiesWithBorders();
            tb.BackColor = COLOR_SELECTED_CELL;

            if (Convert.ToInt64(tcCount) != 0)
            {
                displayOnlyLabels = true;
                string id = "";
                if (localeID != "")
                    id = WebConstants.COL_PROJECT_LOCALE_ID + "_" + localeID;
                else if (platformID != "")
                    id = WebConstants.COL_PROJECT_PLATFORM_ID + "_" + platformID;
                else
                    id = WebConstants.COL_PROJECT_LOCALE_ID + "_0_" + WebConstants.COL_PROJECT_PLATFORM_ID + "_0";

                if (localeID != "")
                {
                    tb.Controls.Add(CreateTableCellForData(id, TC_TS, ""));
                }
                tb.Controls.Add(CreateTableCellForData(id, TC_COUNT, tcCount));
                if (localeID != "")
                {
                    tb.Controls.Add(CreateTableCellForData(id, TC_SIDS, ""));
                }
                tb.Controls.Add(CreateTableCellForData(id, TC_EXECUTED, tcExecuted));
                tb.Controls.Add(CreateTableCellForData(id, TC_NA, tcNA));
                tb.Controls.Add(CreateTableCellForData(id, TC_REM, tcRemaining));

                tb.Enabled = true;
                displayOnlyLabels = false;

                return SetTableCellPropertiesWithBorders(tb);
            }
            return ShowBlankCells();
        }

        /// <summary>
        /// ShowBlankCells
        /// </summary>
        /// <param name="backColor"></param>
        /// <returns></returns>
        private TableCell ShowBlankCells(Color backColor)
        {
            Table tb = SetTableProperties();

            if (backColor != COLOR_BLANK)
            {
                tb.BackColor = backColor;
            }
            return SetTableCellPropertiesWithBorders(tb);
        }

        /// <summary>
        /// ShowBlankCells
        /// </summary>
        /// <returns></returns>
        private TableCell ShowBlankCells()
        {
            return ShowBlankCells(COLOR_BLANK);
        }

        /// <summary>
        /// CreateTableCellForData
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private TableRow CreateTableCellForData(string id, string text, string value)
        {
            Color displayColor = COLOR_TEXT_DISPLAY_TEXT_BLACK;
            TableRow tr = SetTableRowProperties();

            if (alDisplayLables.Contains(text) || id.Contains(WebConstants.COL_PROJECT_LOCALE_ID + "_0_" + WebConstants.COL_PROJECT_PLATFORM_ID + "_0") || displayOnlyLabels)
            {
                tr.Controls.Add(SetTableCellProperties(AddLabel(text + "_" + id, value, displayColor), 1));
            }
            else if (alDisplayTextBoxes.Contains(text))
            {
                tr.Controls.Add(SetTableCellProperties(AddTextBox(text + "_" + id, value), 1));
            }

            return tr;
        }

        ///// <summary>
        ///// CreateTableCellForTCCount
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="text"></param>
        ///// <param name="value"></param>
        ///// <param name="percenValue"></param>
        ///// <returns></returns>
        //private TableRow CreateTableCellForTCCount(string id, string text, string value, string percenValue)
        //{
        //    TableRow tr = SetTableRowProperties();
        //    //if (displayLabelcounter == 1)
        //    //{
        //    //    tr.Controls.Add(SetTableCellProperties(AddLabel(id, text)));
        //    //}

        //    Table tb = SetTableProperties();
        //    TableRow tr1 = SetTableRowProperties();
        //    tr1.Controls.Add(SetTableCellProperties(AddTextBox(text + "_" + id, value)));

        //    Label lbl = AddLabel("/", "/");
        //    lbl.Width = System.Web.UI.WebControls.Unit.Pixel(1);
        //    tr1.Controls.Add(SetTableCellProperties(lbl));
        //    tr1.Controls.Add(SetTableCellProperties(AddTextBox(TC_PERCENT + "_" + text + "_" + id, percenValue)));
        //    tr1.Controls.Add(SetTableCellProperties(AddLabel("%", "%")));
        //    tb.Controls.Add(tr1);

        //    tr.Controls.Add(SetTableCellProperties(tb, 1));
        //    return tr;
        //}

        ///// <summary>
        ///// AddCheckBox
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="text"></param>
        ///// <returns></returns>
        //private CheckBox AddCheckBox(string id, string text)
        //{
        //    CheckBox chk = new CheckBox();

        //    if (text == "")
        //    {
        //        chk.Visible = false;
        //    }
        //    chk.ID = id;
        //    chk.Text = text;
        //    chk.AutoPostBack = true;
        //    chk.ForeColor = System.Drawing.Color.AntiqueWhite;
        //    chk.EnableViewState = true;
        //    chk.Width = System.Web.UI.WebControls.Unit.Percentage(100);
        //    return chk;
        //}

        /// <summary>
        /// AddTextBox
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        private TextBox AddTextBox(string id, string text)
        {
            TextBox textBox = new TextBox();

            textBox.ID = STR_TEXTBOX + "_" + id;
            textBox.Text = text;
            textBox.EnableViewState = true;
            textBox.Width = System.Web.UI.WebControls.Unit.Percentage(VAL_TEXTBOX_PIXEL_PER);
            //txtBox.Height = System.Web.UI.WebControls.Unit.Pixel(18);
            textBox.Attributes.Add(WebConstants.STR_RUNAT, WebConstants.STR_SERVER);

            if (id.Contains(TC_COUNT) || id.Contains(TC_EXECUTED) || id.Contains(TC_NA) || id.Contains(TC_REM))
                textBox.Attributes.Add("onkeydown", "javascript:NumericOnly()");

            return textBox;
        }

        ///// <summary>
        ///// AddTextBoxSetVisibility
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="text"></param>
        ///// <returns></returns>
        //private TextBox AddTextBoxSetVisibility(string id, string text, bool isVisible)
        //{
        //    TextBox txtBox = AddTextBox(id, text);
        //    txtBox.Visible = true;
        //    if (!isVisible)
        //    {
        //        txtBox.Width = System.Web.UI.WebControls.Unit.Pixel(0);
        //    }
        //    return txtBox;
        //}

        /// <summary>
        /// AddLabel
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        private Label AddLabel(string id, string text)
        {
            Label lbl = new Label();

            lbl.Visible = true;
            lbl.ForeColor = COLOR_TEXT_DISPLAY_TEXT_WHITE;
            lbl.Font.Size = FontUnit.Small;
            lbl.ID = STR_LABEL + "_" + id;
            lbl.Text = text;
            lbl.EnableViewState = true;
            lbl.Width = System.Web.UI.WebControls.Unit.Percentage(VAL_MAX_PIXEL_PER);
            lbl.Height = System.Web.UI.WebControls.Unit.Percentage(VAL_MAX_PIXEL_PER);
            lbl.Attributes.Add(WebConstants.STR_RUNAT, WebConstants.STR_SERVER);
            return lbl;
        }

        /// <summary>
        /// AddLabel
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <param name="displayColor"></param>
        /// <returns></returns>
        private Label AddLabel(string id, string text, Color displayColor)
        {
            Label lbl = AddLabel(id, text);

            lbl.ForeColor = displayColor;
            return lbl;
        }

        /// <summary>
        /// SetTableProperties
        /// </summary>
        /// <param name="width"></param>
        /// <returns></returns>
        private Table SetTablePropertiesWithBorders()
        {
            Table tb = SetTableProperties();
            tb.BorderWidth = 2;
            tb.BorderColor = System.Drawing.Color.Blue;
            return tb;
        }

        /// <summary>
        /// SetTablePropertiesWithoutBorders
        /// </summary>
        /// <param name="width"></param>
        /// <returns></returns>
        private Table SetTableProperties()
        {
            Table tb = new Table();
            tb.Width = System.Web.UI.WebControls.Unit.Percentage(VAL_MAX_PIXEL_PER);
            tb.Height = System.Web.UI.WebControls.Unit.Percentage(VAL_MAX_PIXEL_PER);
            tb.EnableViewState = true;
            //tb.CellSpacing = 0;
            //tb.CellPadding = 0;
            return tb;
        }

        /// <summary>
        /// SetTableRowProperties
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        private TableRow SetTableRowProperties()
        {
            TableRow tr = new TableRow();
            tr.Width = System.Web.UI.WebControls.Unit.Percentage(VAL_MAX_PIXEL_PER);
            tr.Height = System.Web.UI.WebControls.Unit.Percentage(VAL_MAX_PIXEL_PER);
            tr.EnableViewState = true;
            return tr;
        }

        /// <summary>
        /// SetTableCellProperties
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        private TableCell SetTableCellProperties(Control control)
        {
            TableCell td = new TableCell();
            td.EnableViewState = true;
            if (control != null)
            {
                td.Controls.Add(control);
                int width = tdWidthCount;
                td.Width = System.Web.UI.WebControls.Unit.Percentage(width);
            }
            else
            {
                td.Width = System.Web.UI.WebControls.Unit.Pixel(VAL_GAP_PIXEL_10);
            }
            td.HorizontalAlign = HorizontalAlign.Left;
            td.Height = System.Web.UI.WebControls.Unit.Pixel(NUM_EACH_GRID_ROW_DISPLAY_HEIGHT);
            return td;
        }

        /// <summary>
        /// SetTableCellProperties
        /// </summary>
        /// <param name="control"></param>
        /// <param name="isCountTrue"></param>
        /// <returns></returns>
        private TableCell SetTableCellProperties(Control control, int isWidth)
        {
            TableCell td = SetTableCellProperties(control);
            int width = tdWidthCount;
            //if (displayLabelcounter == 1)
            //{
            //    width = width * 2;
            //}
            td.Width = System.Web.UI.WebControls.Unit.Percentage(width);
            return td;
        }

        /// <summary>
        /// SetTableCellProperties
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        private TableCell SetTableCellPropertiesWithBorders(Control control)
        {
            TableCell td = SetTableCellProperties(control);
            td.BorderWidth = 1;
            td.BorderColor = System.Drawing.Color.Blue;
            return td;
        }

        /// <summary>
        /// SetTableCellPropertiesWithBorders
        /// </summary>
        /// <param name="control"></param>
        /// <param name="isCountTrue"></param>
        /// <returns></returns>
        private TableCell SetTableCellPropertiesWithBorders(Control control, int isWidth)
        {
            TableCell td = SetTableCellPropertiesWithBorders(control);
            td.Width = System.Web.UI.WebControls.Unit.Percentage(tdWidthCount);
            return td;
        }

        #endregion

        //#region Set Properties and Access

        //#region Properties

        ///// <summary>
        ///// ProductID
        ///// </summary>
        //[Browsable(true)]
        //[Category("ProductID")]
        //[Description("Set the Product")]
        //public string ProductID
        //{
        //    set
        //    {
        //        productID = value;
        //        PanelProduct.Visible = false;
        //    }
        //}

        ///// <summary>
        ///// Product VersionID
        ///// </summary>
        //[Browsable(true)]
        //[Category("ProductVersionID")]
        //[Description("Set the Product Version ID")]
        //public string ProductVersionID
        //{
        //    set
        //    {
        //        DropDownListProductVersion.SelectedIndex = DropDownListProductVersion.Items.IndexOf(DropDownListProductVersion.Items.FindByValue(value));
        //        PanelProductVersion.Visible = false;
        //    }
        //}

        ///// <summary>
        ///// Project Phases ID
        ///// </summary>
        //[Browsable(true)]
        //[Category("ProjectPhaseID")]
        //[Description("Set the ProductPhaseID")]
        //public string ProjectPhaseID
        //{
        //    set
        //    {
        //        DropDownListProjectPhases.SelectedIndex = DropDownListProjectPhases.Items.IndexOf(DropDownListProjectPhases.Items.FindByValue(value));
        //        PanelProjectPhase.Visible = false;
        //    }
        //}

        ///// <summary>
        ///// Set User Access
        ///// </summary>
        //[Browsable(true)]
        //[Category("SetUserAccess")]
        //[Description("Set the UserAccess")]
        //public string SetUserAccess
        //{
        //    set
        //    {
        //        if (value == "User")
        //        {
        //        }
        //        else if (value == "Owner")
        //        {
        //        }
        //        else if (value == "ADMIN")
        //        {
        //        }
        //    }
        //}

        ///// <summary>
        ///// Set User Access
        ///// </summary>
        //[Browsable(true)]
        //[Category("PopulateMatrix")]
        //[Description("Populate Locale Vs Platform Matrix")]
        //public void PopulateMatrix()
        //{
        //    PopulateLocaleVsPlatformData();
        //}

        //#endregion

        //#endregion

        #region Screen Access and Labels

        /// <summary>
        /// GetAndSetScreenAccess
        /// </summary>
        private void GetAndSetScreenAccess()
        {
            dataHeader.LoginID = Session[WebConstants.SESSION_LOGIN_ID].ToString();

            if (Session[WebConstants.SESSION_PRODUCT_ID] != null)
                productID = Session[WebConstants.SESSION_PRODUCT_ID].ToString();

            isContractor = (bool)Session[WebConstants.SESSION_VENDOR_IS_CONTRACTOR];

            if (Session[WebConstants.SESSION_CACHE_SCREEN_ACCESS_DATA_TABLE] != null)
            {
                DataTable dtScreenAccess = (DataTable)Session[WebConstants.SESSION_CACHE_SCREEN_ACCESS_DATA_TABLE];

                DataRow drScreenAccess = dtScreenAccess.Select(WebConstants.COL_SCREEN_IDENTIFIER + " = '" + Session[WebConstants.SESSION_IDENTIFIER] + "'")[0];

                if (drScreenAccess[WebConstants.COL_SCREEN_IS_READ_WRITE].ToString() == WebConstants.DEF_VAL_ACCESS_CHECKED)
                    isReadOnly = false;
                if (drScreenAccess[WebConstants.COL_SCREEN_IS_REPORT].ToString() == WebConstants.DEF_VAL_ACCESS_CHECKED)
                    isReport = true;
            }

            if (isReport && !isReadOnly)
            {
                PanelSelectUserType.Visible = true;

                if (Session[WebConstants.SESSION_MATRIX_USER_ACCESS] != null)
                    DropDownListUserType.SelectedIndex = DropDownListUserType.Items.IndexOf(DropDownListUserType.Items.FindByValue(Session[WebConstants.SESSION_MATRIX_USER_ACCESS].ToString()));
                else
                    DropDownListUserType.SelectedIndex = DropDownListUserType.Items.IndexOf(DropDownListUserType.Items.FindByValue(WebConstants.STR_OTMATRIX_ACCESS_DISPLAY_ONLY));
            }
            else if (isReport)
                DropDownListUserType.SelectedIndex = DropDownListUserType.Items.IndexOf(DropDownListUserType.Items.FindByValue(WebConstants.STR_OTMATRIX_ACCESS_OWNER));
            else if (!isReadOnly)
                DropDownListUserType.SelectedIndex = DropDownListUserType.Items.IndexOf(DropDownListUserType.Items.FindByValue(WebConstants.STR_OTMATRIX_ACCESS_USER));
            else
                DropDownListUserType.SelectedIndex = DropDownListUserType.Items.IndexOf(DropDownListUserType.Items.FindByValue(WebConstants.STR_OTMATRIX_ACCESS_DISPLAY_ONLY));

            if (DropDownListUserType.SelectedValue == WebConstants.STR_OTMATRIX_ACCESS_OWNER)
            {
                PanelDistributionType.Visible = true;
                ButtonDistributeTestCases.Visible = true;
            }
            else
            {
                PanelDistributionType.Visible = false;
                ButtonDistributeTestCases.Visible = false;
            }

            if (DropDownListUserType.SelectedValue == WebConstants.STR_OTMATRIX_ACCESS_DISPLAY_ONLY)
                ButtonUpdateLocalesVsPlatformsMatrix.Visible = false;
            else
                ButtonUpdateLocalesVsPlatformsMatrix.Visible = true;
        }

        /// <summary>
        /// SetFooter
        /// </summary>
        private void SetFooter()
        {
            ButtonDistributeTestCases.Enabled = false;

            if (dtprojectPhaseLocales != null)
                AddFooterPadding(dtprojectPhaseLocales.Rows.Count);
            else
                AddFooterPadding(0);
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

            LabelHeader.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelHeader.Text, false);
            LabelProduct.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelProduct.Text, false);
            LabelProductVersion.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelProductVersion.Text, false);
            LabelProjectPhases.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelProjectPhases.Text, false);
            LabelTotalTestCases.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelTotalTestCases.Text, false);
            LabelSelectVendor.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelSelectVendor.Text, false);
            LabelSelectUserView.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelSelectUserView.Text, false);
            LabelDistributionType.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelDistributionType.Text, false);
            LabelTestCasesDistribution.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelTestCasesDistribution.Text, false);
            LabelAcrossLocalesandPlatforms.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelAcrossLocalesandPlatforms.Text, false);

            RadioBtnManual.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, RadioBtnManual.Text, false);
            RadioButtonEqual.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, RadioButtonEqual.Text, false);
            RadioButtonLocaleWeight.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, RadioButtonLocaleWeight.Text, false);
            RadioButtonNone.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, RadioButtonNone.Text, false);
            RadioButtonLocales.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, RadioButtonLocales.Text, false);
            RadioButtonBoth.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, RadioButtonBoth.Text, false);

            ButtonDistributeTestCases.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, ButtonDistributeTestCases.Text, false);

            foreach (ListItem item in DropDownListUserType.Items)
                item.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, item.Text);
        }

        /// <summary>
        /// AddFooterPadding
        /// </summary>
        private void AddFooterPadding(int rowsCount)
        {
            int spaceCount = (rowsCount * NUM_EACH_GRID_ROW_DISPLAY_HEIGHT) + 10 * NUM_EACH_GRID_ROW_DISPLAY_HEIGHT;

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