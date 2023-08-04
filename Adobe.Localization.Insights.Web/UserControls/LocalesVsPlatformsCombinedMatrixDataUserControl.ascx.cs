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
    /// LocalesVsPlatformsCombinedMatrixDataUserControl
    /// </summary>
    public partial class LocalesVsPlatformsCombinedMatrixDataUserControl : System.Web.UI.UserControl
    {
        #region Variables

        private DataTable dtProjectPhaseLocales;
        private DataTable dtProjectPhasePlatforms;

        private DataTable dtScreenLabels;
        private ArrayList alDisplayLables;

        private string productID = "";
        private string productVersionID = "";
        private string projectPhaseID = "";
        private string phaseCoverageDetailID = "";
        private string vendorID = "";

        private bool displayOnlyLabels = false;
        private bool isReport = false;

        private int tdWidthCount;
        private int minWidthCount = 3;
        private int totalTestCasesCount = 0;

        private const string TC_TS = "TS#";
        private const string TC_SIDS = "SIDs";
        private const string TC_COUNT = "Count";
        private const string TC_NA = "NA";
        private const string TC_EXECUTED = "Executed";
        private const string TC_PERCENT = "Percent";
        private const string TC_REM = "Remaining";
        private const string ARLIST_DISPLAY_LABLES = "DisplayLables";
        private const string PANEL_MATRIX = "PanelMatrix";
        private const string MATRIX_OUTER_TABLE = "MatrixOuterTable";
        private const string HEADING_MATRIX_HEADER = "MatrixHeader";
        private const string HEADING_TEST_CASES = "TestCases";
        private const string HEADING_TOTAL_RUNS = "TotalRuns";
        private const string STR_MESSAGE_MATRIX_NOT_DEFINED = "MatrixNotDefined";

        private const string STR_LABEL_ID_LABELS = "Labels";
        private const string STR_LABEL = "Label";
        private const string STR_ZERO = "0";
        private const string STR_PER = "%";
        private const string STR_NA = "NA";

        private const int VAL_MAX_PIXEL_PER = 100;
        private const int VAL_TEXTBOX_PIXEL_PER = 90;
        private const int VAL_GAP_PIXEL_10 = 10;
        private const int VAL_GAP_PIXEL_20 = 20;
        private const int NUM_EACH_GRID_ROW_DISPLAY_HEIGHT = 20;

        private Color COLOR_TOTAL = Color.LightGray;
        private Color COLOR_ERROR = Color.Red;
        private Color COLOR_NOT_PLANNED = Color.DarkKhaki;
        private Color COLOR_NOT_STARTED = Color.CornflowerBlue;
        private Color COLOR_IN_PROGRESS = Color.Yellow;
        private Color COLOR_COMPLETED = Color.ForestGreen;
        private Color COLOR_BLANK = Color.White;
        private Color COLOR_TD_BACK_BLACK = System.Drawing.Color.Black;
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
            GetScreenAccess();
            SetScreenLabels();
            PopulateData();
            SetScreenAccess();
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// PopulateProjectPhaseLocales
        /// </summary>
        private void PopulateProjectPhaseLocales(DTO.ProjectPhases projectPhaseData)
        {
            dtProjectPhaseLocales = BO.BusinessObjects.GetProjectLocales(projectPhaseData).Tables[0];

            if (productVersionID != "")
            {
                DataView dvProjectPhaseLocales = new DataView(dtProjectPhaseLocales);
                dtProjectPhaseLocales = dvProjectPhaseLocales.ToTable(true, WebConstants.COL_PRODUCT_LOCALE_ID, WebConstants.COL_LOCALE);
                dtProjectPhaseLocales.Columns[WebConstants.COL_PRODUCT_LOCALE_ID].ColumnName = WebConstants.COL_PROJECT_LOCALE_ID;
            }

            dtProjectPhaseLocales.Columns.Add(WebConstants.COL_PROJECT_PHASE_TOTAL_COUNT, WebConstants.STR_SYSTEM_INT32.GetType());
            dtProjectPhaseLocales.Columns.Add(WebConstants.COL_PROJECT_PHASE_TOTAL_EXECUTED, WebConstants.STR_SYSTEM_INT32.GetType());
            dtProjectPhaseLocales.Columns.Add(WebConstants.COL_PROJECT_PHASE_TOTAL_NA, WebConstants.STR_SYSTEM_INT32.GetType());
            dtProjectPhaseLocales.Columns.Add(WebConstants.COL_PROJECT_PHASE_TOTAL_REMAINING, WebConstants.STR_SYSTEM_INT32.GetType());

            foreach (DataRow dr in dtProjectPhaseLocales.Rows)
            {
                dr[WebConstants.COL_PROJECT_PHASE_TOTAL_COUNT] = 0;
                dr[WebConstants.COL_PROJECT_PHASE_TOTAL_EXECUTED] = 0;
                dr[WebConstants.COL_PROJECT_PHASE_TOTAL_NA] = 0;
                dr[WebConstants.COL_PROJECT_PHASE_TOTAL_REMAINING] = 0;
            }

            ViewState[WebConstants.TBL_PROJECT_PHASE_LOCALES] = dtProjectPhaseLocales;
        }

        /// <summary>
        /// PopulateProjectPhasePlatforms
        /// </summary>
        private void PopulateProjectPhasePlatforms(DTO.ProjectPhases projectPhaseData)
        {
            dtProjectPhasePlatforms = BO.BusinessObjects.GetProjectPlatforms(projectPhaseData).Tables[0];

            if (productVersionID != "")
            {
                DataView dvProjectPhasePlatforms = new DataView(dtProjectPhasePlatforms);
                dtProjectPhasePlatforms = dvProjectPhasePlatforms.ToTable(true, WebConstants.COL_PRODUCT_PLATFORM_ID, WebConstants.COL_PLATFORM);
                dtProjectPhasePlatforms.Columns[WebConstants.COL_PRODUCT_PLATFORM_ID].ColumnName = WebConstants.COL_PROJECT_PLATFORM_ID;
            }

            dtProjectPhasePlatforms.Columns.Add(WebConstants.COL_PROJECT_PHASE_TOTAL_COUNT, WebConstants.STR_SYSTEM_INT32.GetType());
            dtProjectPhasePlatforms.Columns.Add(WebConstants.COL_PROJECT_PHASE_TOTAL_EXECUTED, WebConstants.STR_SYSTEM_INT32.GetType());
            dtProjectPhasePlatforms.Columns.Add(WebConstants.COL_PROJECT_PHASE_TOTAL_NA, WebConstants.STR_SYSTEM_INT32.GetType());
            dtProjectPhasePlatforms.Columns.Add(WebConstants.COL_PROJECT_PHASE_TOTAL_REMAINING, WebConstants.STR_SYSTEM_INT32.GetType());

            foreach (DataRow dr in dtProjectPhasePlatforms.Rows)
            {
                dr[WebConstants.COL_PROJECT_PHASE_TOTAL_COUNT] = 0;
                dr[WebConstants.COL_PROJECT_PHASE_TOTAL_EXECUTED] = 0;
                dr[WebConstants.COL_PROJECT_PHASE_TOTAL_NA] = 0;
                dr[WebConstants.COL_PROJECT_PHASE_TOTAL_REMAINING] = 0;
            }

            ViewState[WebConstants.TBL_PROJECT_PHASE_PLATFORMS] = dtProjectPhasePlatforms;
        }

        /// <summary>
        /// PopulateData
        /// </summary>
        private void PopulateData()
        {
            DTO.ProjectPhases projectPhaseData = new DTO.ProjectPhases();
            projectPhaseData.ProjectPhaseID = projectPhaseID;
            projectPhaseData.ProductVersionID = productVersionID;
            projectPhaseData.VendorID = vendorID;

            PopulateProjectPhaseLocales(projectPhaseData);
            PopulateProjectPhasePlatforms(projectPhaseData);

            SetDisplayLabelAndTextboxes();

            Panel panelMatrix = new Panel();
            panelMatrix.ID = PANEL_MATRIX + vendorID;
            panelMatrix.EnableViewState = true;
            panelMatrix.Controls.Add(CreateLocaleVsPlatformMatrixDynamically());
            WrapperPanelMatrix.Controls.Add(panelMatrix);
            WrapperPanelMatrix.EnableViewState = true;

        }

        /// <summary>
        /// SetDisplayLabelAndTextboxes
        /// </summary>
        private void SetDisplayLabelAndTextboxes()
        {
            alDisplayLables = new ArrayList();

            alDisplayLables.Add(TC_TS);
            alDisplayLables.Add(TC_SIDS);
            alDisplayLables.Add(TC_COUNT);
            alDisplayLables.Add(TC_EXECUTED);
            alDisplayLables.Add(TC_NA);
            alDisplayLables.Add(TC_REM);

            ViewState[ARLIST_DISPLAY_LABLES] = alDisplayLables;
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
            tdWidthCount = dtProjectPhasePlatforms.Rows.Count + minWidthCount;
            tdWidthCount = 100 / tdWidthCount;

            DTO.ProjectLocaleVsPlatformMatrix matrixData = new DTO.ProjectLocaleVsPlatformMatrix();
            matrixData.ProjectPhaseID = projectPhaseID;
            matrixData.ProductVersionID = productVersionID;
            matrixData.VendorID = vendorID;
            matrixData.PhaseCoverageDetailID = phaseCoverageDetailID;

            DataTable dtMatrixData = BO.BusinessObjects.GetLocaleVsPlatformMatrixData(matrixData).Tables[0];

            if (dtMatrixData.Rows.Count == 0)
            {
                Table tbNUll = SetTableProperties();
                TableRow trNUll = SetTableRowProperties();
                TableCell tdLabelNUll;
                tdLabelNUll = SetTableCellProperties(AddLabel(STR_MESSAGE_MATRIX_NOT_DEFINED, COM.GetScreenLocalizedLabel(dtScreenLabels, STR_MESSAGE_MATRIX_NOT_DEFINED)));
                trNUll.Controls.Add(tdLabelNUll);
                tbNUll.Controls.Add(trNUll);

                //if (LabelVendor.Text == "")
                //    tbNUll.Visible = false;
                return tbNUll;
            }

            foreach (DataRow dr in dtMatrixData.Rows)
            {
                dr[WebConstants.COL_TESTCASES_REMAINING] = GetIntegerValue(dr[WebConstants.COL_TESTCASES_COUNT].ToString()) - GetIntegerValue(dr[WebConstants.COL_TESTCASES_EXECUTED].ToString()) - GetIntegerValue(dr[WebConstants.COL_TESTCASES_NA].ToString());
                totalTestCasesCount = totalTestCasesCount + GetIntegerValue(dr[WebConstants.COL_TESTCASES_COUNT].ToString());
            }

            Table tb = SetTablePropertiesWithBorders();
            tb.ID = MATRIX_OUTER_TABLE;
            TableRow tr = SetTableRowProperties();
            TableCell tdLabel;

            tdLabel = SetTableCellPropertiesWithBorders(AddLabel(WebConstants.COL_PLATFORM_ID + "_-1", COM.GetScreenLocalizedLabel(dtScreenLabels, HEADING_MATRIX_HEADER)));
            tdLabel.BackColor = COLOR_TD_BACK_BLACK;
            tr.Controls.Add(tdLabel);

            tdLabel = SetTableCellPropertiesWithBorders(AddLabel(STR_LABEL_ID_LABELS, COM.GetScreenLocalizedLabel(dtScreenLabels, HEADING_TEST_CASES)));
            tdLabel.BackColor = COLOR_TD_BACK_BLACK;
            tr.Controls.Add(tdLabel);

            #region Add Platforms

            foreach (DataRow drPlatform in dtProjectPhasePlatforms.Rows)
            {
                tdLabel = SetTableCellPropertiesWithBorders(AddLabel(WebConstants.COL_PLATFORM_ID + "_" + drPlatform[WebConstants.COL_PROJECT_PLATFORM_ID].ToString(), drPlatform[WebConstants.COL_PLATFORM].ToString()), 1);
                tdLabel.HorizontalAlign = HorizontalAlign.Center;
                tdLabel.BackColor = COLOR_TD_BACK_BLACK;
                tr.Controls.Add(tdLabel);
            }
            tb.Controls.Add(tr);

            #endregion

            tdLabel = SetTableCellPropertiesWithBorders(AddLabel(WebConstants.COL_PLATFORM_ID + "_N", COM.GetScreenLocalizedLabel(dtScreenLabels, HEADING_TOTAL_RUNS)));
            tdLabel.BackColor = COLOR_TD_BACK_BLACK;
            tdLabel.HorizontalAlign = HorizontalAlign.Center;
            tdLabel.Height = Unit.Pixel(VAL_GAP_PIXEL_20 * 2);
            tr.Controls.Add(tdLabel);

            #region Add Locales

            foreach (DataRow drLocale in dtProjectPhaseLocales.Rows)
            {
                tr = SetTableRowProperties();
                string localeIDtext = WebConstants.COL_LOCALE_ID + "_" + drLocale[WebConstants.COL_PROJECT_LOCALE_ID].ToString();
                tdLabel = SetTableCellPropertiesWithBorders(AddLabel(localeIDtext, drLocale[WebConstants.COL_LOCALE].ToString()));
                tdLabel.BackColor = COLOR_TD_BACK_BLACK;
                tdLabel.HorizontalAlign = HorizontalAlign.Center;
                tr.Controls.Add(tdLabel);
                //displayLabelcounter = 0;

                tr.Controls.Add(CreateTableCellForDataLabels(localeIDtext, false));
                foreach (DataRow drPlatform in dtProjectPhasePlatforms.Rows)
                {
                    //displayLabelcounter++;
                    DataRow[] drMatrixDataRows = dtMatrixData.Select(WebConstants.COL_PROJECT_LOCALE_ID + " = " + drLocale[WebConstants.COL_PROJECT_LOCALE_ID].ToString() + " AND " + WebConstants.COL_PROJECT_PLATFORM_ID + " = " + drPlatform[WebConstants.COL_PROJECT_PLATFORM_ID].ToString());
                    DataRow drMatrixDataRow;

                    if (drMatrixDataRows.Length > 0)
                    {
                        drMatrixDataRow = drMatrixDataRows[0];

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
                        drMatrixDataRow = dtMatrixData.NewRow();

                    tr.Controls.Add(CreateTableCellForProjectLocaleVsPlatformData(drMatrixDataRow));
                }

                tr.Controls.Add(CreateTableCellForGrandTotalTestCases(drLocale[WebConstants.COL_PROJECT_LOCALE_ID].ToString(), "", drLocale[WebConstants.COL_PROJECT_PHASE_TOTAL_COUNT].ToString(), drLocale[WebConstants.COL_PROJECT_PHASE_TOTAL_EXECUTED].ToString(), drLocale[WebConstants.COL_PROJECT_PHASE_TOTAL_NA].ToString(), drLocale[WebConstants.COL_PROJECT_PHASE_TOTAL_REMAINING].ToString()));

                //tr = CreateTableCellForProjectLocaleVsPlatformData(dtprojectPhasePlatforms, drLocale["ProjectLocaleID"].ToString());
                tb.Controls.Add(tr);
            }

            #endregion

            //displayLabelcounter = 0;
            tr = SetTableRowProperties();
            tdLabel = SetTableCellPropertiesWithBorders(AddLabel(WebConstants.COL_LOCALE_ID + "_N", COM.GetScreenLocalizedLabel(dtScreenLabels, HEADING_TOTAL_RUNS)));
            tdLabel.BackColor = COLOR_TD_BACK_BLACK;
            tdLabel.HorizontalAlign = HorizontalAlign.Center;
            tr.Controls.Add(tdLabel);

            //displayLabelcounter = 0;
            int grandTotalCount = 0;
            int grandTotalExecuted = 0;
            int grandTotalNA = 0;
            int grandTotallRemaining = 0;

            tr.Controls.Add(CreateTableCellForDataLabels(WebConstants.COL_PROJECT_LOCALE_ID + "_N_" + WebConstants.COL_PLATFORM, true));
            foreach (DataRow drPlatform in dtProjectPhasePlatforms.Rows)
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

            //if (productVersionID != "")
            //{
            tr = SetTableRowProperties();
            tr.Controls.Add(SetTableCellProperties(AddLabel(localeIDtext + "_" + TC_EXECUTED, TC_EXECUTED)));
            //tr.Controls.Add(SetTableCellProperties(AddTextBoxSetVisibility(localeIDtext + "_" + TC_EXECUTED, TC_EXECUTED, false)));
            tb.Controls.Add(tr);
            //}
            //else
            //{
            //    if (!IsSkip)
            //    {
            //        tr = SetTableRowProperties();
            //        tr.Controls.Add(SetTableCellProperties(AddLabel(localeIDtext + "_" + TC_TS, TC_TS)));
            //        //tr.Controls.Add(SetTableCellProperties(AddTextBoxSetVisibility(localeIDtext + "_" + TC_TS, TC_TS, false)));
            //        tb.Controls.Add(tr);
            //    }

            //    tr = SetTableRowProperties();
            //    tr.Controls.Add(SetTableCellProperties(AddLabel(localeIDtext + "_" + TC_COUNT, TC_COUNT)));
            //    //tr.Controls.Add(SetTableCellProperties(AddTextBoxSetVisibility(localeIDtext + "_" + TC_COUNT, TC_COUNT, false)));
            //    tb.Controls.Add(tr);

            //    if (!IsSkip)
            //    {
            //        tr = SetTableRowProperties();
            //        tr.Controls.Add(SetTableCellProperties(AddLabel(localeIDtext + "_" + TC_SIDS, TC_SIDS)));
            //        //tr.Controls.Add(SetTableCellProperties(AddTextBoxSetVisibility(localeIDtext + "_" + TC_SIDS, TC_SIDS, false)));
            //        tb.Controls.Add(tr);
            //    }

            //    tr = SetTableRowProperties();
            //    tr.Controls.Add(SetTableCellProperties(AddLabel(localeIDtext + "_" + TC_EXECUTED, TC_EXECUTED)));
            //    //tr.Controls.Add(SetTableCellProperties(AddTextBoxSetVisibility(localeIDtext + "_" + TC_EXECUTED, TC_EXECUTED, false)));
            //    tb.Controls.Add(tr);

            //    tr = SetTableRowProperties();
            //    tr.Controls.Add(SetTableCellProperties(AddLabel(localeIDtext + "_" + TC_NA, TC_NA)));
            //    //tr.Controls.Add(SetTableCellProperties(AddTextBoxSetVisibility(localeIDtext + "_" + TC_NA, TC_NA, false)));
            //    tb.Controls.Add(tr);

            //    tr = SetTableRowProperties();
            //    tr.Controls.Add(SetTableCellProperties(AddLabel(localeIDtext + "_" + TC_REM, TC_REM)));
            //    //tr.Controls.Add(SetTableCellProperties(AddTextBoxSetVisibility(localeIDtext + "_" + TC_REM, TC_REM, false)));
            //    tb.Controls.Add(tr);
            //}
            return SetTableCellPropertiesWithBorders(tb);
        }

        /// <summary>
        /// CreateTableCellForProjectLocaleVsPlatformData
        /// </summary>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        private TableCell CreateTableCellForProjectLocaleVsPlatformData(DataRow dr)
        {
            string id = WebConstants.COL_PROJECT_LOCALE_ID + "_" + dr[WebConstants.COL_PROJECT_LOCALE_ID] + "_" + WebConstants.COL_PROJECT_PLATFORM_ID + "_" + dr[WebConstants.COL_PROJECT_PLATFORM_ID];
            Table tb = SetTablePropertiesWithBorders();

            if (dr[WebConstants.COL_TESTCASES_COUNT].ToString() == "" || dr[WebConstants.COL_TESTCASES_COUNT].ToString() == STR_ZERO)
                tb.Controls.Add(CreateTableCellForData(id, TC_EXECUTED, STR_NA));
            else
            {
                if (productVersionID == "")
                {
                    double percent = Math.Round(((Convert.ToDouble(dr[WebConstants.COL_TESTCASES_COUNT]) - Convert.ToDouble(dr[WebConstants.COL_TESTCASES_REMAINING])) * 100) / Convert.ToDouble(dr[WebConstants.COL_TESTCASES_COUNT]), 2);
                    tb.Controls.Add(CreateTableCellForData(id, TC_EXECUTED, percent.ToString() + STR_PER));
                }
                else
                {
                    double percent = Math.Round(((Convert.ToDouble(dr[WebConstants.COL_TESTCASES_COUNT]) - Convert.ToDouble(dr[WebConstants.COL_TESTCASES_REMAINING])) * 100) / totalTestCasesCount, 2);
                    tb.Controls.Add(CreateTableCellForData(id, TC_EXECUTED, percent.ToString() + STR_PER));
                }
            }

            //if (productVersionID == "")
            //{
            //    if (dr[WebConstants.COL_TEST_SUITE_NO].ToString() != "")
            //    {
            //        tb.Controls.Add(CreateTableCellForData(id, TC_TS, dr[WebConstants.COL_TEST_SUITE_NO].ToString()));
            //        tb.Controls.Add(CreateTableCellForData(id, TC_COUNT, dr[WebConstants.COL_TESTCASES_COUNT].ToString()));
            //        tb.Controls.Add(CreateTableCellForData(id, TC_SIDS, dr[WebConstants.COL_TEST_SUITE_IDS].ToString()));
            //        // tb.Controls.Add(CreateTableCellForTCCount(id, TC_COUNT, dr["TestCasesCount"].ToString(), dr["TestCasesPercent"].ToString()));
            //        tb.Controls.Add(CreateTableCellForData(id, TC_EXECUTED, dr[WebConstants.COL_TESTCASES_EXECUTED].ToString()));
            //        tb.Controls.Add(CreateTableCellForData(id, TC_NA, dr[WebConstants.COL_TESTCASES_NA].ToString()));
            //        tb.Controls.Add(CreateTableCellForData(id, TC_REM, dr[WebConstants.COL_TESTCASES_REMAINING].ToString()));
            //    }
            //    else
            //    {
            //        tb.Controls.Add(CreateTableCellForData(id, TC_TS, COM.GetScreenLocalizedLabel(dtScreenLabels, STR_NA)));
            //        tb.Controls.Add(CreateTableCellForData(id, TC_COUNT, STR_ZERO));
            //        tb.Controls.Add(CreateTableCellForData(id, TC_SIDS, COM.GetScreenLocalizedLabel(dtScreenLabels, STR_NA)));
            //        tb.Controls.Add(CreateTableCellForData(id, TC_EXECUTED, STR_ZERO));
            //        tb.Controls.Add(CreateTableCellForData(id, TC_NA, STR_ZERO));
            //        tb.Controls.Add(CreateTableCellForData(id, TC_REM, STR_ZERO));
            //    }
            //}
            //else
            //{
            //}

            if (dr[WebConstants.COL_TESTCASES_COUNT].ToString() == "" || dr[WebConstants.COL_TESTCASES_COUNT].ToString() == STR_ZERO)
                tb.BackColor = COLOR_NOT_PLANNED;
            else if (dr[WebConstants.COL_TESTCASES_EXECUTED].ToString() == "" || dr[WebConstants.COL_TESTCASES_EXECUTED].ToString() == STR_ZERO)
                tb.BackColor = COLOR_NOT_STARTED;
            else if (dr[WebConstants.COL_TESTCASES_REMAINING].ToString() == "" || dr[WebConstants.COL_TESTCASES_REMAINING].ToString() == STR_ZERO)
                tb.BackColor = COLOR_COMPLETED;
            else if (Convert.ToInt32(dr[WebConstants.COL_TESTCASES_REMAINING]) < 0)
                tb.BackColor = COLOR_ERROR;
            else
                tb.BackColor = COLOR_IN_PROGRESS;

            return SetTableCellPropertiesWithBorders(tb);

            //return ShowBlankCells();
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
            tb.BackColor = COLOR_TOTAL;

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

                //if (productVersionID == "")
                //{
                //    if (localeID != "")
                //    {
                //        tb.Controls.Add(CreateTableCellForData(id, TC_TS, ""));
                //    }
                //    tb.Controls.Add(CreateTableCellForData(id, TC_COUNT, tcCount));
                //    if (localeID != "")
                //    {
                //        tb.Controls.Add(CreateTableCellForData(id, TC_SIDS, ""));
                //    }
                //    tb.Controls.Add(CreateTableCellForData(id, TC_EXECUTED, tcExecuted));
                //    tb.Controls.Add(CreateTableCellForData(id, TC_NA, tcNA));
                //    tb.Controls.Add(CreateTableCellForData(id, TC_REM, tcRemaining));
                //}
                //else
                //{
                tb.Controls.Add(CreateTableCellForData(id, TC_EXECUTED, tcExecuted));
                //}

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
            //else if (aldisplayTextBoxes.Contains(text))
            //{
            //    tr.Controls.Add(SetTableCellProperties(AddTextBox(text + "_" + id, value), 1));
            //}

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

        ///// <summary>
        ///// AddTextBox
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="text"></param>
        ///// <returns></returns>
        //private TextBox AddTextBox(string id, string text)
        //{
        //    TextBox textBox = new TextBox();

        //    textBox.ID = STR_TEXTBOX + "_" + id;
        //    textBox.Text = text;
        //    textBox.EnableViewState = true;
        //    textBox.Width = System.Web.UI.WebControls.Unit.Percentage(VAL_TEXTBOX_PIXEL_PER);
        //    //txtBox.Height = System.Web.UI.WebControls.Unit.Pixel(18);
        //    textBox.Attributes.Add(WebConstants.STR_RUNAT, WebConstants.STR_SERVER);

        //    if (id.Contains(TC_COUNT) || id.Contains(TC_EXECUTED) || id.Contains(TC_NA) || id.Contains(TC_REM))
        //        textBox.Attributes.Add("onkeydown", "javascript:if (event.keyCode < 37 && event.keyCode > 40) {if (event.keyCode != 116 && event.keyCode != 46 && event.keyCode != 190 && event.keyCode != 8 && event.keyCode != 9 && event.keyCode != 16) {if (event.keyCode < 48 || event.keyCode > 57) {alert('Please enter Numeric value.');event.returnValue = false;}}}");

        //    return textBox;
        //}

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
        /// SetTableProperties
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
                td.Width = System.Web.UI.WebControls.Unit.Percentage(tdWidthCount);
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
            //int width = tdWidthCount;
            //if (displayLabelcounter == 1)
            //{
            //    width = width * 2;
            //}
            td.Width = System.Web.UI.WebControls.Unit.Percentage(tdWidthCount);
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

        #region Set Properties and Access

        #region Properties

        /// <summary>
        /// ProductID
        /// </summary>
        [Browsable(true)]
        [Category(WebConstants.COL_PRODUCT_ID)]
        [Description("Set the Product")]
        public string ProductID
        {
            set
            {
                productID = value;
            }
        }

        /// <summary>
        /// Product VersionID
        /// </summary>
        [Browsable(true)]
        [Category(WebConstants.COL_PRODUCT_VERSION_ID)]
        [Description("Set the Product Version ID")]
        public string ProductVersionID
        {
            set
            {
                productVersionID = value;
            }
        }

        /// <summary>
        /// Project Phases ID
        /// </summary>
        [Browsable(true)]
        [Category(WebConstants.COL_PROJECT_PHASE_ID)]
        [Description("Set the ProjectPhaseID")]
        public string ProjectPhaseID
        {
            set
            {
                projectPhaseID = value;
            }
        }

        /// <summary>
        /// Phase Coverage Detail ID
        /// </summary>
        [Browsable(true)]
        [Category(WebConstants.COL_PHASE_COVERAGE_DETAIL_ID)]
        [Description("Set the PhaseCoverageDetailID")]
        public string PhaseCoverageDetailID
        {
            set
            {
                phaseCoverageDetailID = value;
            }
        }

        /// <summary>
        /// vendor ID
        /// </summary>
        [Browsable(true)]
        [Category(WebConstants.COL_VENDOR_ID)]
        [Description("Set the VendorID")]
        public string VendorID
        {
            set
            {
                vendorID = value;
            }
        }

        /// <summary>
        /// Vendor
        /// </summary>
        [Browsable(true)]
        [Category(WebConstants.COL_VENDOR)]
        [Description("Set the Vendor")]
        public string Vendor
        {
            set
            {
                LabelVendor.Text = value;
            }
        }

        #endregion

        #endregion

        #region Screen Access and Labels

        /// <summary>
        /// GetScreenAccess
        /// </summary>
        private void GetScreenAccess()
        {
            string screenIdentifier = "";
            if (Session[WebConstants.SESSION_IDENTIFIER] != null)
                screenIdentifier = Session[WebConstants.SESSION_IDENTIFIER].ToString();
            else if (Session[WebConstants.SESSION_IDENTIFIER_TEMP] != null)
                screenIdentifier = Session[WebConstants.SESSION_IDENTIFIER_TEMP].ToString();

            if (Session[WebConstants.SESSION_CACHE_SCREEN_ACCESS_DATA_TABLE] != null)
            {
                DataTable dtScreenAccess = (DataTable)Session[WebConstants.SESSION_CACHE_SCREEN_ACCESS_DATA_TABLE];
                DataRow[] drScreenAccess = dtScreenAccess.Select(WebConstants.COL_SCREEN_IDENTIFIER + " = '" + screenIdentifier + "'");

                if (drScreenAccess.Length == 1)
                {
                    if (drScreenAccess[0][WebConstants.COL_SCREEN_IS_REPORT].ToString() == WebConstants.DEF_VAL_ACCESS_CHECKED)
                        isReport = true;
                    if (drScreenAccess[0][WebConstants.COL_SCREEN_IS_READ].ToString() == WebConstants.DEF_VAL_ACCESS_CHECKED || drScreenAccess[0][WebConstants.COL_SCREEN_IS_READ_WRITE].ToString() == WebConstants.DEF_VAL_ACCESS_CHECKED)
                        isReport = false;
                }
            }
        }

        /// <summary>
        /// SetScreenAccess
        /// </summary>
        private void SetScreenAccess()
        {
            if (isReport || LabelVendor.Text == "")
                PanelVendorDetails.Visible = false;
        }

        /// <summary>
        /// PopulateScreenLabels
        /// </summary>
        /// <param name="userID"></param>
        private void PopulateScreenLabels()
        {
            DTO.Screens screenData = new DTO.Screens();
            screenData.ScreenIdentifier = WebConstants.USER_CONTROL_LOCALES_PLATFORM_MATRIX_DISPLAY;
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
        }

        #endregion
    }
}