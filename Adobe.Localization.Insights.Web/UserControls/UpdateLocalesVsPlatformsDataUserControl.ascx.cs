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
    /// UpdateLocalesVsPlatformsDataUserControl
    /// </summary>
    public partial class UpdateLocalesVsPlatformsDataUserControl : System.Web.UI.UserControl
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

        private string projectPhaseID;
        private string phaseCoverageDetailID;
        private string platformTypeID;
        private string projectBuildDetailID;
        private string productLocaleID;
        private string vendorID;
        private string userView;

        private const string TC_TS = "TS#";
        private const string TC_SIDS = "SIDs";
        private const string TC_COUNT = "Count";
        private const string TC_NA = "NA";
        private const string TC_EXECUTED = "Executed";
        private const string TC_PERCENT = "Percent";
        private const string TC_REM = "Remaining";
        private const string ARLIST_DISPLAY_LABLES = "DisplayLables";
        private const string ARLIST_DISPLAY_TEXTBOXES = "DisplayTextBoxes";
        //private const string CONTENT_PLACEHOLDER_TEXTBOX = "ctl00$ContentPlaceHolder$ctl00$TextBox";       
        private const string CONTENT_PLACEHOLDER_TEXTBOX = "TextBox";
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
        private const string STR_ZERO = "0";
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
            phaseCoverageDetailID = Session[WebConstants.SESSION_MATRIX_PHASE_COVERAGE_DETAIL_ID].ToString();
            platformTypeID = Session[WebConstants.SESSION_MATRIX_PLATFORM_TYPE_ID].ToString();
            projectBuildDetailID = Session[WebConstants.SESSION_MATRIX_BUILD_DETAIL_ID].ToString();
            productLocaleID = Session[WebConstants.SESSION_MATRIX_PRODUCT_LOCALE_ID].ToString();
            vendorID = Session[WebConstants.SESSION_MATRIX_SELECTED_VENDOR_ID].ToString();
            userView = Session[WebConstants.SESSION_MATRIX_USER_ACCESS].ToString();
            projectPhaseID = Session[WebConstants.SESSION_MATRIX_PROJECT_PHASE_ID].ToString();

            GetAndSetScreenAccess();
            SetScreenLabels();

            if (!IsPostBack)
            {
                PopulateData(true);
                //PopulateVendor();
                //PopulateLocaleVsPlatformData();
            }
            else
            {
                PopulateData(false);
            }
            dtprojectPhaseLocales = (DataTable)ViewState[WebConstants.TBL_PROJECT_PHASE_LOCALES];
            dtprojectPhasePlatforms = (DataTable)ViewState[WebConstants.TBL_PROJECT_PHASE_PLATFORMS];
            alDisplayLables = (ArrayList)ViewState[ARLIST_DISPLAY_LABLES];
            alDisplayTextBoxes = (ArrayList)ViewState[ARLIST_DISPLAY_TEXTBOXES];
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
            projectPhaseData.PhaseCoverageDetailID = phaseCoverageDetailID;
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
                    {
                        matrixData.ProjectDataID = drMatrixDataRows[0][WebConstants.COL_PROJECT_DATA_ID].ToString();

                        //matrixData.ProjectLocaleID = drLocale[WebConstants.COL_PROJECT_LOCALE_ID].ToString();
                        //matrixData.ProjectPlatformID = drPlatform[WebConstants.COL_PROJECT_PLATFORM_ID].ToString();
                        matrixData.UserAccess = userView;

                        if (alDisplayTextBoxes.Contains(TC_COUNT))
                            //matrixData.TC_Count = Request.Form[String.Format(textBoxID, TC_COUNT)];
                            matrixData.TC_Count = ((TextBox)WrapperPanelMatrix.FindControl(String.Format(textBoxID, TC_COUNT))).Text;
                        if (alDisplayTextBoxes.Contains(TC_TS))
                        {
                            //matrixData.TsNo = Request.Form[String.Format(textBoxID, TC_TS)];
                            matrixData.TsNo = ((TextBox)WrapperPanelMatrix.FindControl(String.Format(textBoxID, TC_TS))).Text;
                            if (matrixData.TsNo == "")
                                if (!(matrixData.TC_Count == "" || matrixData.TC_Count == "0"))
                                {
                                    isTSInfoMissing = true;
                                    break;
                                }
                        }

                        if (drMatrixDataRows[0][WebConstants.COL_TEST_SUITE_NO].ToString() != "")
                        {
                            if (alDisplayTextBoxes.Contains(TC_SIDS))
                                matrixData.SIDs = ((TextBox)WrapperPanelMatrix.FindControl(String.Format(textBoxID, TC_SIDS))).Text;
                            if (alDisplayTextBoxes.Contains(TC_NA))
                                matrixData.TC_NA = ((TextBox)WrapperPanelMatrix.FindControl(String.Format(textBoxID, TC_NA))).Text;
                            if (alDisplayTextBoxes.Contains(TC_EXECUTED))
                                matrixData.TC_Executed = ((TextBox)WrapperPanelMatrix.FindControl(String.Format(textBoxID, TC_EXECUTED))).Text;
                        }

                        projectPhaseData.ProjectLocaleVsPlatformMatrix.Add(matrixData);
                    }
                }
            }

            bool isSuccess = false;
            if (!isTSInfoMissing)
                isSuccess = BO.BusinessObjects.UpdateProjectLocalesVsPlatformsMatrix(projectPhaseData);

            if (isSuccess)
            {
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_SUCCESS_SAVE_MESSAGE);
                PopulateData(false);
            }
            else
            {
                if (isTSInfoMissing)
                    LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.SCREEN_MATRIX_SUITE_ID_MANDATORY);
                else
                    LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_SAVE_MESSAGE);
            }
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// PopulateDetails
        /// </summary>
        private void PopulateDetails(DTO.ProjectPhases projectPhaseData)
        {
            DataTable dtPhaseCoverageDetails = BO.BusinessObjects.GetPhaseCoverageAllDetails(projectPhaseData).Tables[0];

            LabelProductValue.Text = dtPhaseCoverageDetails.Rows[0][WebConstants.COL_PRODUCT].ToString();
            LabelProductVersionValue.Text = String.Format(" ( {0} ) ", dtPhaseCoverageDetails.Rows[0][WebConstants.COL_PRODUCT_VERSION].ToString());
            LabelProjectPhaseValue.Text = dtPhaseCoverageDetails.Rows[0][WebConstants.COL_PROJECT_PHASE].ToString();
            LabelCoveragesValue.Text = dtPhaseCoverageDetails.Rows[0][WebConstants.COL_PHASE_COVERAGE_DETAILS].ToString();

            if (vendorID == "0" || vendorID == "")
                LabelVendorName.Text = WebConstants.DEF_VAL_TEAMS_ALL;
            else
            {
                DTO.Users userData = new DTO.Users();
                userData.VendorID = vendorID;
                LabelVendorName.Text = BO.BusinessObjects.GetVendorDetails(userData).Tables[0].Rows[0][WebConstants.COL_VENDOR].ToString();
            }

            LabelLocalesFilterValue.Text = "";
            if (projectBuildDetailID != STR_ZERO && projectBuildDetailID != "")
            {
                DTO.Product productData = new DTO.Product();
                productData.ProductVersionID = dtPhaseCoverageDetails.Rows[0][WebConstants.COL_PRODUCT_VERSION_ID].ToString();
                productData.ProjectBuildDetailID = projectBuildDetailID;
                LabelLocalesFilterValue.Text = BO.BusinessObjects.GetProjectBuildDetails(productData).Tables[0].Rows[0][WebConstants.COL_PROJECT_BUILD].ToString() + " -> ";
            }

            if (productLocaleID == STR_ZERO || productLocaleID == "")
                LabelLocalesFilterValue.Text += WebConstants.DEF_VAL_LOCALES_ALL;
            else
            {
                DTO.Product productData = new DTO.Product();
                productData.ProductVersionID += dtPhaseCoverageDetails.Rows[0][WebConstants.COL_PRODUCT_VERSION_ID].ToString();
                productData.ProjectBuildDetailID = projectBuildDetailID;

                LabelLocalesFilterValue.Text = BO.BusinessObjects.GetProjectBuildLocales(productData).Tables[0].Select(WebConstants.COL_PRODUCT_LOCALE_ID + "=" + productLocaleID)[0][WebConstants.COL_LOCALE].ToString();
            }

            platformTypeID = Session[WebConstants.SESSION_MATRIX_PLATFORM_TYPE_ID].ToString();
            if (platformTypeID == WebConstants.DEF_VAL_WIN_PLATFORM_TYPE_ID)
                LabelPlatformsFilterValue.Text = "WIN";
            else if (platformTypeID == WebConstants.DEF_VAL_MAC_PLATFORM_TYPE_ID)
                LabelPlatformsFilterValue.Text = "MAC";
            else
                LabelPlatformsFilterValue.Text = "WIN / MAC";
        }

        /// <summary>
        /// PopulateLocaleVsPlatformData
        /// </summary>
        private void PopulateLocaleVsPlatformData(DTO.ProjectPhases projectPhaseData)
        {
            WrapperPanelMatrix.Controls.Clear();
            PopulateProjectPhaseLocales(projectPhaseData);
            PopulateProjectPhasePlatforms(projectPhaseData);

            SetDisplayLabelAndTextboxes();

            Panel panelMatrix = new Panel();
            panelMatrix.ID = PANEL_MATRIX;
            panelMatrix.Controls.Add(CreateLocaleVsPlatformMatrixDynamically());
            WrapperPanelMatrix.Controls.Add(panelMatrix);
        }

        /// <summary>
        /// PopulateData
        /// </summary>
        private void PopulateData(bool isAll)
        {
            DTO.ProjectPhases projectPhaseData = new DTO.ProjectPhases();
            projectPhaseData.ProjectPhaseID = projectPhaseID;
            projectPhaseData.PhaseCoverageDetailID = phaseCoverageDetailID;
            projectPhaseData.VendorID = vendorID;
            projectPhaseData.PlatformTypeID = platformTypeID;
            projectPhaseData.ProjectBuildDetailID = projectBuildDetailID;
            projectPhaseData.ProductLocaleID = productLocaleID;

            if (isAll)
                PopulateDetails(projectPhaseData);

            PopulateLocaleVsPlatformData(projectPhaseData);
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
        /// SetDisplayLabelAndTextboxes
        /// </summary>
        private void SetDisplayLabelAndTextboxes()
        {
            alDisplayTextBoxes = new ArrayList();
            alDisplayLables = new ArrayList();

            if (userView == WebConstants.STR_OTMATRIX_ACCESS_DEFINE)
            {
                alDisplayTextBoxes.Add(TC_TS);
                alDisplayLables.Add(TC_SIDS);
                alDisplayTextBoxes.Add(TC_COUNT);
                alDisplayLables.Add(TC_EXECUTED);
                alDisplayLables.Add(TC_NA);
                alDisplayLables.Add(TC_REM);

            }
            else if (userView == WebConstants.STR_OTMATRIX_ACCESS_UPDATE)
            {
                alDisplayLables.Add(TC_TS);
                alDisplayTextBoxes.Add(TC_SIDS);
                alDisplayLables.Add(TC_COUNT);
                alDisplayTextBoxes.Add(TC_EXECUTED);
                alDisplayTextBoxes.Add(TC_NA);
                alDisplayLables.Add(TC_REM);
            }
            else if (userView == WebConstants.STR_OTMATRIX_ACCESS_DISPLAY_ONLY)
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

            DTO.ProjectLocaleVsPlatformMatrix matrixData = new DTO.ProjectLocaleVsPlatformMatrix();
            matrixData.VendorID = vendorID;
            matrixData.PhaseCoverageDetailID = phaseCoverageDetailID;
            matrixData.PlatformTypeID = platformTypeID;
            matrixData.ProjectBuildDetailID = projectBuildDetailID;
            matrixData.ProductLocaleID = productLocaleID;

            dtMatrixData = BO.BusinessObjects.GetLocaleVsPlatformMatrixData(matrixData).Tables[0];
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
                    //if (drMatrixDataRows.Length == 0)
                    //{
                    //    drMatrixDataRow = dtMatrixData.NewRow();
                    //    drMatrixDataRow[WebConstants.COL_PROJECT_LOCALE_ID] = drLocale[WebConstants.COL_PROJECT_LOCALE_ID].ToString();
                    //    drMatrixDataRow[WebConstants.COL_PROJECT_PLATFORM_ID] = drPlatform[WebConstants.COL_PROJECT_PLATFORM_ID].ToString();
                    //}
                    //else
                    if (drMatrixDataRows.Length != 0)
                    {
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
                    else
                        tr.Controls.Add(CreateTableCellForProjectLocaleVsPlatformData(null));
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

            if (dr != null)
            {
                if (dr[WebConstants.COL_TEST_SUITE_NO].ToString() != "")
                {
                    tb.BackColor = COLOR_SELECTED_CELL;
                }

                string id = WebConstants.COL_PROJECT_LOCALE_ID + "_" + dr[WebConstants.COL_PROJECT_LOCALE_ID] + "_" + WebConstants.COL_PROJECT_PLATFORM_ID + "_" + dr[WebConstants.COL_PROJECT_PLATFORM_ID];

                if ((userView == WebConstants.STR_OTMATRIX_ACCESS_DEFINE) || dr[WebConstants.COL_TEST_SUITE_NO].ToString() != "")
                {
                    tb.Controls.Add(CreateTableCellForData(id, TC_TS, dr[WebConstants.COL_TEST_SUITE_NO].ToString()));
                    tb.Controls.Add(CreateTableCellForData(id, TC_COUNT, dr[WebConstants.COL_TESTCASES_COUNT].ToString()));
                    tb.Controls.Add(CreateTableCellForData(id, TC_SIDS, dr[WebConstants.COL_TEST_SUITE_RUN_IDS].ToString()));
                    // tb.Controls.Add(CreateTableCellForTCCount(id, TC_COUNT, dr["TestCasesCount"].ToString(), dr["TestCasesPercent"].ToString()));
                    tb.Controls.Add(CreateTableCellForData(id, TC_EXECUTED, dr[WebConstants.COL_TESTCASES_EXECUTED].ToString()));
                    tb.Controls.Add(CreateTableCellForData(id, TC_NA, dr[WebConstants.COL_TESTCASES_NA].ToString()));
                    tb.Controls.Add(CreateTableCellForData(id, TC_REM, dr[WebConstants.COL_TESTCASES_REMAINING].ToString()));

                    return SetTableCellPropertiesWithBorders(tb);
                }
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
            textBox.Attributes.Add(WebConstants.STR_ATTRIBUTE_RUNAT, WebConstants.STR_SERVER);

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
            lbl.Attributes.Add(WebConstants.STR_ATTRIBUTE_RUNAT, WebConstants.STR_SERVER);
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

        #region Screen Access and Labels

        /// <summary>
        /// GetAndSetScreenAccess
        /// </summary>
        private void GetAndSetScreenAccess()
        {
            dataHeader.LoginID = Session[WebConstants.SESSION_LOGIN_ID].ToString();

            //if (Session[WebConstants.SESSION_PRODUCT_ID] != null)
            //    productID = Session[WebConstants.SESSION_PRODUCT_ID].ToString();

            //isContractor = (bool)Session[WebConstants.SESSION_VENDOR_IS_CONTRACTOR];

            //if (Session[WebConstants.SESSION_CACHE_SCREEN_ACCESS_DATA_TABLE] != null)
            //{
            //    DataTable dtScreenAccess = (DataTable)Session[WebConstants.SESSION_CACHE_SCREEN_ACCESS_DATA_TABLE];

            //    DataRow drScreenAccess = dtScreenAccess.Select(WebConstants.COL_SCREEN_IDENTIFIER + " = '" + Session[WebConstants.SESSION_IDENTIFIER] + "'")[0];

            //    if (drScreenAccess[WebConstants.COL_SCREEN_IS_READ_WRITE].ToString() == WebConstants.DEF_VAL_ACCESS_CHECKED)
            //        isReadOnly = false;
            //    if (drScreenAccess[WebConstants.COL_SCREEN_IS_REPORT].ToString() == WebConstants.DEF_VAL_ACCESS_CHECKED)
            //        isReport = true;
            //}

            //if (isReport && !isReadOnly)
            //{
            //    PanelSelectUserType.Visible = true;

            //    if (Session[WebConstants.SESSION_MATRIX_USER_ACCESS] != null)
            //        DropDownListUserType.SelectedIndex = DropDownListUserType.Items.IndexOf(DropDownListUserType.Items.FindByValue(Session[WebConstants.SESSION_MATRIX_USER_ACCESS].ToString()));
            //    else
            //        DropDownListUserType.SelectedIndex = DropDownListUserType.Items.IndexOf(DropDownListUserType.Items.FindByValue(WebConstants.STR_OTMATRIX_ACCESS_DISPLAY_ONLY));
            //}
            //else if (isReport)
            //    DropDownListUserType.SelectedIndex = DropDownListUserType.Items.IndexOf(DropDownListUserType.Items.FindByValue(WebConstants.STR_OTMATRIX_ACCESS_OWNER));
            //else if (!isReadOnly)
            //    DropDownListUserType.SelectedIndex = DropDownListUserType.Items.IndexOf(DropDownListUserType.Items.FindByValue(WebConstants.STR_OTMATRIX_ACCESS_UPDATE));
            //else
            //    DropDownListUserType.SelectedIndex = DropDownListUserType.Items.IndexOf(DropDownListUserType.Items.FindByValue(WebConstants.STR_OTMATRIX_ACCESS_DISPLAY_ONLY));

            //if (DropDownListUserType.SelectedValue == WebConstants.STR_OTMATRIX_ACCESS_OWNER)
            //{
            //    PanelDistributionType.Visible = true;
            //    ButtonDistributeTestCases.Visible = true;
            //}
            //else
            //{
            //    PanelDistributionType.Visible = false;
            //    ButtonDistributeTestCases.Visible = false;
            //}

            //if (DropDownListUserType.SelectedValue == WebConstants.STR_OTMATRIX_ACCESS_DISPLAY_ONLY)
            //    ButtonUpdateLocalesVsPlatformsMatrix.Visible = false;
            //else
            //    ButtonUpdateLocalesVsPlatformsMatrix.Visible = true;
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
                LabelProduct.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelProduct.Text);
                LabelProjectPhases.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelProjectPhases.Text);
                LabelPlatformsFilter.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelPlatformsFilter.Text);
                LabelCoverages.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelCoverages.Text);
                LabelLocalesFilter.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelLocalesFilter.Text);
                LabelSelectVendor.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelSelectVendor.Text);

                ButtonUpdateLocalesVsPlatformsMatrix.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, ButtonUpdateLocalesVsPlatformsMatrix.Text);
            }
        }

        #endregion
    }
}