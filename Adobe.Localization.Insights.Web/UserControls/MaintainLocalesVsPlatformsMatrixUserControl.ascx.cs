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
    /// MaintainLocalesVsPlatformsMatrixUserControl
    /// </summary>
    public partial class MaintainLocalesVsPlatformsMatrixUserControl : System.Web.UI.UserControl
    {
        #region Variables

        private DataTable dtProjectPhaseCoverages;
        private DataTable dtProjectPhaseLocales;
        private DataTable dtProjectPhasePlatforms;
        private DataTable dtScreenLabels;
        private DataTable dtMatrixData;

        private DTO.Header dataHeader = new DTO.Header();
        private DTO.TestSudio ts = new DTO.TestSudio();

        private ArrayList alDisplayLables;
        private ArrayList alDisplayTextBoxes;

        private string TBL_MATRIX_DATA = "MatrixData";
        //private string GRID_MATRIX_DATA = "GridViewMatrixData";

        private bool displayOnlyLabels = false;
        private bool isReadOnly = true;
        private bool isReport = false;
        private bool isContractor = false;
        private bool isProductVersionActive = false;

        private const string STR_NON_ACTIVE = "Not Active";
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
        private const string STR_ZERO = "0";

        private const string VAL_TC_REP_TS = "1";
        private const string VAL_TC_REP_DOCUMENT = "2";
        private const string VAL_TC_DIST_MANUAL = "1";
        private const string VAL_TC_DIST_EQUAL = "2";
        private const string VAL_TC_DIST_WEIGHT = "3";
        private const string VAL_TC_LOC_PLAT_NONE = "1";
        private const string VAL_TC_LOC_PLAT_LOC = "2";
        private const string VAL_TC_LOC_PLAT_PLAT = "3";
        private const string VAL_TC_LOC_PLAT_BOTH = "4";

        private const string CONFIRM_MESSAGE_MATRIX_SAVE = "Any modifications will delete any existing Blocks if un-selected. Are you certain that you would like to continue?";
        private const string CONFIRM_MESSAGE_MATRIX_RESET = "Are you certain that you would like to clear all settings and Optimization Matrix?";
        private const string CONFIRM_MESSAGE_CREATE_SUITE = "It would create both Test Suites and attach Test cases to Suites as per the selected options.";

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

        private const string COL_TEST_SUITE = "TestSuite_";
        private const string COL_SUITE_ID = "SuiteID_";
        private const string HYPERLINK_TEST_SUITE_SEARCH = "<a href='http://ts.corp.adobe.com/TSPRD/TS/TS.html#tcsearchcriterion=%3Csearchcriterion%3E%3Cname%3E%3C%2Fname%3E%3Cid%3E-1%3C%2Fid%3E%3Csearchtype%3E%3C%2Fsearchtype%3E%3Cassociationtype%3E1%3C%2Fassociationtype%3E%3Ccasesensitive%3Etrue%3C%2Fcasesensitive%3E%3Cpagesize%3E-1%3C%2Fpagesize%3E%3Cpagenumber%3E1%3C%2Fpagenumber%3E%3Cfilterlist%3E%3Cfilter%3E%3Cname%3ETS_RECURSIVE_DISPLAYNAME%3C%2Fname%3E%3Coperator%3E%3D%3C%2Foperator%3E%3Cid%3E0%3C%2Fid%3E%3Cvalues%3E%3Cvalue%3E{0}%3C%2Fvalue%3E%3C%2Fvalues%3E%3C%2Ffilter%3E%3Cfilter%3E%3Cname%3ESHOW_DELETED_TC%3C%2Fname%3E%3Coperator%3E%3D%3C%2Foperator%3E%3Cid%3E0%3C%2Fid%3E%3Cvalues%3E%3Cvalue%3EN%3C%2Fvalue%3E%3C%2Fvalues%3E%3C%2Ffilter%3E%3C%2Ffilterlist%3E%3Corderlist%3E%3C%2Forderlist%3E%3Cgrouplist%3E%3C%2Fgrouplist%3E%3Cgroupvalues%3E%3C%2Fgroupvalues%3E%3Csearchowner%3E%3Cid%3E-1%3C%2Fid%3E%3Cname%3E%3C%2Fname%3E%3C%2Fsearchowner%3E%3C%2Fsearchcriterion%3E' target='_blank'>{0}</a>";
        private const string HYPERLINK_SUITE_ID_SEARCH = "<a href='http://ts.corp.adobe.com/TSPRD/TS/TS.html#trsearchcriterion=%3Csearchcriterion%3E%3Cname%3E%3C%2Fname%3E%3Cid%3E-1%3C%2Fid%3E%3Csearchtype%3E%3C%2Fsearchtype%3E%3Cassociationtype%3E2%3C%2Fassociationtype%3E%3Ccasesensitive%3Etrue%3C%2Fcasesensitive%3E%3Cpagesize%3E-1%3C%2Fpagesize%3E%3Cpagenumber%3E1%3C%2Fpagenumber%3E%3Cfilterlist%3E%3Cfilter%3E%3Cname%3ETR_SUITE_RUN_DISPLAYNAME%3C%2Fname%3E%3Coperator%3E%3D%3C%2Foperator%3E%3Cid%3E0%3C%2Fid%3E%3Cvalues%3E%3Cvalue%3E{0}%3C%2Fvalue%3E%3C%2Fvalues%3E%3C%2Ffilter%3E%3C%2Ffilterlist%3E%3Corderlist%3E%3C%2Forderlist%3E%3Cgrouplist%3E%3C%2Fgrouplist%3E%3Cgroupvalues%3E%3C%2Fgroupvalues%3E%3Csearchowner%3E%3Cid%3E-1%3C%2Fid%3E%3Cname%3E%3C%2Fname%3E%3C%2Fsearchowner%3E%3C%2Fsearchcriterion%3E' target='_blank'>{0}</a>";

        private const int COL_TEST_SUITE_LENGTH = 8;
        private const int COL_SUITE_ID_LENGTH = 10;

        private System.Drawing.Color headerBackColor = System.Drawing.Color.LightGray;

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

            LabelMessage.Text = "";
            LabelProductVersionID.Text = Session[WebConstants.SESSION_PRODUCT_VERSION_ID].ToString();
            if (!IsPostBack)
            {
                PopulateProductVersion();
            }

            dtProjectPhaseLocales = (DataTable)ViewState[WebConstants.TBL_PROJECT_PHASE_LOCALES];
            dtProjectPhasePlatforms = (DataTable)ViewState[WebConstants.TBL_PROJECT_PHASE_PLATFORMS];
            dtProjectPhaseCoverages = (DataTable)ViewState[WebConstants.TBL_PHASE_COVERAGE_DETAILS];
            alDisplayLables = (ArrayList)ViewState[ARLIST_DISPLAY_LABLES];
            alDisplayTextBoxes = (ArrayList)ViewState[ARLIST_DISPLAY_TEXTBOXES];
            isProductVersionActive = (bool)ViewState[WebConstants.STR_IS_PRODUCT_VERSION_ACTIVE];
            dtMatrixData = (DataTable)ViewState[TBL_MATRIX_DATA];

            SetScreenAccess();
        }

        #endregion

        #region Button Events

        /// <summary>
        /// ButtonSaveMatrix_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSaveMatrix_Click(object sender, EventArgs e)
        {
            SaveMatrixDetails(false);
        }

        /// <summary>
        /// ButtonCreateTestSuite_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonCreateTestSuite_Click(object sender, EventArgs e)
        {
            ts.ErrorCode = STR_ZERO;
            ts.DataHeader = dataHeader;
            ts.DataHeader.Password = Session[WebConstants.SESSION_PASSWORD].ToString();

            //PopulateLocaleVsPlatformData();
            LabelMessage.Text = "Creating Test Suite...";
            if (RadioButtonNone.Checked)
            {
                foreach (DataRow drData in dtMatrixData.Rows)
                {
                    drData[WebConstants.COL_TEST_SUITE_NO] = LabelTestSuite.Text;
                    drData[WebConstants.COL_TESTCASES_COUNT] = LabelTestCasesCount.Text;
                }
            }
            else
            {

                ts = BO.BusinessObjects.GetLoginSessionID(ts);

                if (ts.ErrorCode == STR_ZERO)
                {
                    ts.TestSuiteID = LabelTestSuite.Text;
                    ts.TestCasesCount = Convert.ToInt32(LabelTestCasesCount.Text);
                    ts.ProductAreaDistributionCriteria = CheckBoxProductArea.Checked;
                    ts.PriorityDistributionCriteria = CheckBoxPriority.Checked;

                    ts = BO.BusinessObjects.GetTestSuiteDetails(ts);
                    LabelTestCasesCount.Text = ts.TestSuiteDetails.Rows.Count.ToString();

                    if (ts.ErrorCode == STR_ZERO)
                    {
                        DataTable dtTestCases = ts.TestSuiteDetails;
                        dtTestCases.Columns.Add(new DataColumn(WebConstants.COL_TEST_SUITE_NO, Type.GetType(WebConstants.STR_SYSTEM_STRING)));

                        DataTable dtSuiteSet = new DataTable();
                        dtSuiteSet.Columns.Add(new DataColumn(WebConstants.COL_TEST_SUITE_NO, Type.GetType(WebConstants.STR_SYSTEM_STRING)));
                        dtSuiteSet.Columns.Add(new DataColumn(WebConstants.COL_TESTCASES_COUNT, Type.GetType(WebConstants.STR_SYSTEM_STRING)));

                        if (RadioButtonLocales.Checked)
                            AssignSuitesForLocalesOnly(dtMatrixData, dtSuiteSet, dtTestCases);
                        else if (RadioButtonPlatforms.Checked)
                            AssignSuitesForPlatformsOnly(dtMatrixData, dtSuiteSet, dtTestCases);
                        else
                            AssignSuitesForLocalesAndPlatforms(dtMatrixData, dtSuiteSet, dtTestCases);
                    }
                }
            }

            if (ts.ErrorCode == STR_ZERO)
                SaveUpdatedMatrixDetails(dtMatrixData, WebConstants.STR_OTMATRIX_ACCESS_DEFINE);

            LabelMessage.Text = ts.ErrorMessage;
            PopulateLocaleVsPlatformData();
        }

        /// <summary>
        /// ButtonResetMatrix_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonResetMatrix_Click(object sender, EventArgs e)
        {
            SaveMatrixDetails(true);
        }

        /// <summary>
        /// ButtonRefreshSID_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonRefreshSID_Click(object sender, EventArgs e)
        {
            LabelMessage.Text = "Refreshing Suite Run IDs...";
            PopulateLocaleVsPlatformData();
            PopulateSuiteRunIDDetails(dtMatrixData);
            LabelMessage.Text = ts.ErrorMessage;
            PopulateLocaleVsPlatformData();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonGenerateSID_Click(object sender, EventArgs e)
        {
            ts.ErrorCode = STR_ZERO;
            ts.DataHeader = dataHeader;
            ts.DataHeader.Password = Session[WebConstants.SESSION_PASSWORD].ToString();

            ts = BO.BusinessObjects.GetLoginSessionID(ts);

            foreach (DataRow drMatrixData in dtMatrixData.Rows)
            {
            }

            if (ts.ErrorCode == STR_ZERO)
                SaveUpdatedMatrixDetails(dtMatrixData, WebConstants.STR_OTMATRIX_ACCESS_UPDATE);

            LabelMessage.Text = ts.ErrorMessage;
            PopulateLocaleVsPlatformData();
        }

        /// <summary>
        /// ButtonUpdateMatrixManually_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonUpdateMatrixManually_Click(object sender, EventArgs e)
        {
            Session[WebConstants.SESSION_MATRIX_PHASE_COVERAGE_DETAIL_ID] = DropDownListCoverages.SelectedValue;
            Session[WebConstants.SESSION_MATRIX_BUILD_DETAIL_ID] = DropDownListLocaleBuilds.SelectedValue;
            Session[WebConstants.SESSION_MATRIX_PRODUCT_LOCALE_ID] = DropDownListLocales.SelectedValue;
            Session[WebConstants.SESSION_MATRIX_SELECTED_VENDOR_ID] = DropDownListSelectVendor.SelectedValue;
            Session[WebConstants.SESSION_MATRIX_USER_ACCESS] = DropDownListUserView.SelectedValue;
            Session[WebConstants.SESSION_MATRIX_PROJECT_PHASE_ID] = DropDownListProjectPhases.SelectedValue;

            Session[WebConstants.SESSION_MATRIX_PLATFORM_TYPE_ID] = STR_ZERO;
            if (CheckBoxPlatformsWin.Checked && !CheckBoxPlatformsMac.Checked)
                Session[WebConstants.SESSION_MATRIX_PLATFORM_TYPE_ID] = WebConstants.DEF_VAL_WIN_PLATFORM_TYPE_ID;
            else if (!CheckBoxPlatformsWin.Checked && CheckBoxPlatformsMac.Checked)
                Session[WebConstants.SESSION_MATRIX_PLATFORM_TYPE_ID] = WebConstants.DEF_VAL_MAC_PLATFORM_TYPE_ID;


            //Response.Redirect((WebConstants.PAGE_UPDATE_MATRIX, "_blank", "menubar=0,scrollbars=1,width=780,height=900,top=10");

            //Response.Write("<script>");
            //Response.Write(String.Format("window.open('{0}','_blank')", WebConstants.PAGE_UPDATE_MATRIX));
            //Response.Write("</script>");

            Page.ClientScript.RegisterStartupScript(this.GetType(), "showPopup", String.Format("<script>window.open('{0}','_blank');</script>", WebConstants.PAGE_UPDATE_MATRIX));

            PopulateLocaleVsPlatformData();

            #region Leave Commented

            //bool isTSInfoMissing = false;

            //foreach (DataRow drLocale in dtprojectPhaseLocales.Rows)
            //{
            //    foreach (DataRow drPlatform in dtprojectPhasePlatforms.Rows)
            //    {
            //        DTO.ProjectLocaleVsPlatformMatrix matrixData = new DTO.ProjectLocaleVsPlatformMatrix();

            //        string textBoxID = CONTENT_PLACEHOLDER_TEXTBOX + "_{0}_" + WebConstants.COL_PROJECT_LOCALE_ID + "_" + drLocale[WebConstants.COL_PROJECT_LOCALE_ID] + "_" + WebConstants.COL_PROJECT_PLATFORM_ID + "_" + drPlatform[WebConstants.COL_PROJECT_PLATFORM_ID];


            //        if (drMatrixDataRows.Length > 0)
            //            matrixData.ProjectDataID = drMatrixDataRows[0][WebConstants.COL_PROJECT_DATA_ID].ToString();

            //        matrixData.ProjectLocaleID = drLocale[WebConstants.COL_PROJECT_LOCALE_ID].ToString();
            //        matrixData.ProjectPlatformID = drPlatform[WebConstants.COL_PROJECT_PLATFORM_ID].ToString();
            //        matrixData.UserAccess = DropDownListUserView.SelectedItem.Value;

            //        if (alDisplayTextBoxes.Contains(TC_COUNT))
            //            matrixData.TC_Count = Request.Form[String.Format(textBoxID, TC_COUNT)];
            //        if (alDisplayTextBoxes.Contains(TC_TS))
            //        {
            //            matrixData.TsNo = Request.Form[String.Format(textBoxID, TC_TS)];
            //            if (matrixData.TsNo == "")
            //                if (!(matrixData.TC_Count == "" || matrixData.TC_Count == "0"))
            //                {
            //                    isTSInfoMissing = true;
            //                    break;
            //                }
            //        }
            //        if (alDisplayTextBoxes.Contains(TC_SIDS))
            //            matrixData.SIDs = Request.Form[String.Format(textBoxID, TC_SIDS)];
            //        if (alDisplayTextBoxes.Contains(TC_PERCENT))
            //            matrixData.TC_Percent = Request.Form[String.Format(textBoxID, TC_PERCENT + "_" + TC_COUNT)];
            //        if (alDisplayTextBoxes.Contains(TC_NA))
            //            matrixData.TC_NA = Request.Form[String.Format(textBoxID, TC_NA)];
            //        if (alDisplayTextBoxes.Contains(TC_EXECUTED))
            //            matrixData.TC_Executed = Request.Form[String.Format(textBoxID, TC_EXECUTED)];

            //        projectPhaseData.ProjectLocaleVsPlatformMatrix.Add(matrixData);
            //    }
            //}

            //bool isSuccess = false;
            //if (!isTSInfoMissing)
            //    isSuccess = BO.BusinessObjects.UpdateProjectLocalesVsPlatformsMatrix(projectPhaseData);
            //PopulateLocaleVsPlatformData();

            //if (isSuccess)
            //    LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_SUCCESS_SAVE_MESSAGE);
            //else
            //{
            //    if (isTSInfoMissing)
            //        LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.SCREEN_MATRIX_SUITE_ID_MANDATORY);
            //    else
            //        LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_SAVE_MESSAGE);

            //    foreach (DataRow drLocale in dtprojectPhaseLocales.Rows)
            //    {
            //        foreach (DataRow drPlatform in dtprojectPhasePlatforms.Rows)
            //        {
            //            DTO.ProjectLocaleVsPlatformMatrix matrixData = new DTO.ProjectLocaleVsPlatformMatrix();

            //            string textBoxID = CONTENT_PLACEHOLDER_TEXTBOX + "_{0}_" + WebConstants.COL_PROJECT_LOCALE_ID + "_" + drLocale[WebConstants.COL_PROJECT_LOCALE_ID] + "_" + WebConstants.COL_PROJECT_PLATFORM_ID + "_" + drPlatform[WebConstants.COL_PROJECT_PLATFORM_ID];

            //            TextBox textBoxControl;

            //            if (alDisplayTextBoxes.Contains(TC_TS))
            //            {
            //                textBoxControl = (TextBox)this.Page.FindControl(String.Format(textBoxID, TC_TS));
            //                textBoxControl.Text = Request.Form[String.Format(textBoxID, TC_TS)];
            //            }
            //            if (alDisplayTextBoxes.Contains(TC_SIDS))
            //            {
            //                textBoxControl = (TextBox)this.Page.FindControl(String.Format(textBoxID, TC_SIDS));
            //                textBoxControl.Text = Request.Form[String.Format(textBoxID, TC_SIDS)];
            //            }
            //            if (alDisplayTextBoxes.Contains(TC_COUNT))
            //            {
            //                textBoxControl = (TextBox)this.Page.FindControl(String.Format(textBoxID, TC_COUNT));
            //                textBoxControl.Text = Request.Form[String.Format(textBoxID, TC_COUNT)];
            //            }
            //            if (alDisplayTextBoxes.Contains(TC_PERCENT))
            //            {
            //                textBoxControl = (TextBox)this.Page.FindControl(String.Format(textBoxID, TC_PERCENT));
            //                textBoxControl.Text = Request.Form[String.Format(textBoxID, TC_PERCENT)];
            //            }
            //            if (alDisplayTextBoxes.Contains(TC_NA))
            //            {
            //                textBoxControl = (TextBox)this.Page.FindControl(String.Format(textBoxID, TC_NA));
            //                textBoxControl.Text = Request.Form[String.Format(textBoxID, TC_NA)];
            //            }
            //            if (alDisplayTextBoxes.Contains(TC_EXECUTED))
            //            {
            //                textBoxControl = (TextBox)this.Page.FindControl(String.Format(textBoxID, TC_EXECUTED));
            //                textBoxControl.Text = Request.Form[String.Format(textBoxID, TC_EXECUTED)];
            //            }
            //        }
            //    }
            //}

            #endregion
        }

        ///// <summary>
        ///// ButtonUpdateSIDMatrixManually_Click
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void ButtonUpdateSIDMatrixManually_Click(object sender, EventArgs e)
        //{
        //    PopulateLocaleVsPlatformData();
        //    //int tsCount = 1;
        //    //if (RadioButtonEqual.Checked)
        //    //    tsCount = 2;

        //    //foreach (DataRow drLocale in dtprojectPhaseLocales.Rows)
        //    //{
        //    //    foreach (DataRow drPlatform in dtprojectPhasePlatforms.Rows)
        //    //    {
        //    //        DTO.ProjectLocaleVsPlatformMatrix matrixData = new DTO.ProjectLocaleVsPlatformMatrix();

        //    //        string textBoxID = CONTENT_PLACEHOLDER_TEXTBOX + "_{0}_" + WebConstants.COL_PROJECT_LOCALE_ID + "_" + drLocale[WebConstants.COL_PROJECT_LOCALE_ID] + "_" + WebConstants.COL_PROJECT_PLATFORM_ID + "_" + drPlatform[WebConstants.COL_PROJECT_PLATFORM_ID];

        //    //        TextBox textBoxControl;

        //    //        if (alDisplayTextBoxes.Contains(TC_TS))
        //    //        {
        //    //            textBoxControl = (TextBox)this.Page.FindControl(String.Format(textBoxID, TC_TS));
        //    //            textBoxControl.Text = Request.Form[String.Format(textBoxID, TC_TS)];
        //    //            tsCount++;
        //    //        }
        //    //        if (alDisplayTextBoxes.Contains(TC_SIDS))
        //    //        {
        //    //            textBoxControl = (TextBox)this.Page.FindControl(String.Format(textBoxID, TC_SIDS));
        //    //            textBoxControl.Text = Request.Form[String.Format(textBoxID, TC_SIDS)];
        //    //        }
        //    //        if (alDisplayTextBoxes.Contains(TC_COUNT))
        //    //        {
        //    //            textBoxControl = (TextBox)this.Page.FindControl(String.Format(textBoxID, TC_COUNT));
        //    //            textBoxControl.Text = Request.Form[String.Format(textBoxID, TC_COUNT)];
        //    //        }
        //    //        if (alDisplayTextBoxes.Contains(TC_PERCENT))
        //    //        {
        //    //            textBoxControl = (TextBox)this.Page.FindControl(String.Format(textBoxID, TC_PERCENT));
        //    //            textBoxControl.Text = Request.Form[String.Format(textBoxID, TC_PERCENT)];
        //    //        }
        //    //        if (alDisplayTextBoxes.Contains(TC_NA))
        //    //        {
        //    //            textBoxControl = (TextBox)this.Page.FindControl(String.Format(textBoxID, TC_NA));
        //    //            textBoxControl.Text = Request.Form[String.Format(textBoxID, TC_NA)];
        //    //        }
        //    //        if (alDisplayTextBoxes.Contains(TC_EXECUTED))
        //    //        {
        //    //            textBoxControl = (TextBox)this.Page.FindControl(String.Format(textBoxID, TC_EXECUTED));
        //    //            textBoxControl.Text = Request.Form[String.Format(textBoxID, TC_EXECUTED)];
        //    //        }
        //    //    }
        //    //}
        //}

        /// <summary>
        /// SaveMatrixDetails
        /// </summary>
        /// <param name="isReset"></param>
        private void SaveMatrixDetails(bool isReset)
        {
            bool isSuccess = false;

            DTO.ProjectPhases projectPhaseData = new DTO.ProjectPhases();
            projectPhaseData.DataHeader = dataHeader;
            projectPhaseData.ProjectPhaseID = DropDownListProjectPhases.SelectedValue;
            projectPhaseData.PhaseCoverageDetailID = DropDownListCoverages.SelectedValue;
            projectPhaseData.ProjectLocaleVsPlatformMatrix = new ArrayList();

            if (!isReset)
                GetSelectedRadioButtonInformation(projectPhaseData);

            int selectedCoverageCopyOTMatrix = 0;

            if (PanelCopyOTMatrix.Enabled && DropDownListSelectCoverage.SelectedValue != "")
                selectedCoverageCopyOTMatrix = Convert.ToInt32(DropDownListSelectCoverage.SelectedValue);

            if (selectedCoverageCopyOTMatrix > 0)
            {
                DTO.ProjectPhases oldProjectPhaseData = new DTO.ProjectPhases();
                oldProjectPhaseData.ProjectPhaseID = DropDownListSelectProjectPhase.SelectedValue;
                oldProjectPhaseData.PhaseCoverageDetailID = DropDownListSelectCoverage.SelectedValue;

                isSuccess = BO.BusinessObjects.CopyLocalesVsPlatformsMatrix(projectPhaseData, oldProjectPhaseData);
            }
            else
            {
                for (int rowCount = 0; rowCount < dtProjectPhaseLocales.Rows.Count; rowCount++)
                {
                    DataRow drLocale = dtProjectPhaseLocales.Rows[rowCount];
                    GridViewRow gridViewRow = GridViewLocalesVsPlatformMatrix.Rows[rowCount];
                    string clientID = GridViewLocalesVsPlatformMatrix.Rows[rowCount].ClientID.Replace("_", "$");

                    for (int colCount = 0; colCount < dtProjectPhasePlatforms.Rows.Count; colCount++)
                    {
                        DataRow drPlatform = dtProjectPhasePlatforms.Rows[colCount];
                        DTO.ProjectLocaleVsPlatformMatrix matrixData = new DTO.ProjectLocaleVsPlatformMatrix();
                        DataRow[] drMatrixDataRows = dtMatrixData.Select(WebConstants.COL_PROJECT_LOCALE_ID + " = " + drLocale[WebConstants.COL_PROJECT_LOCALE_ID].ToString() + " AND " + WebConstants.COL_PROJECT_PLATFORM_ID + " = " + drPlatform[WebConstants.COL_PROJECT_PLATFORM_ID].ToString());
                        if (drMatrixDataRows.Length > 0)
                            matrixData.ProjectDataID = drMatrixDataRows[0][WebConstants.COL_PROJECT_DATA_ID].ToString();

                        if (!isReset)
                        {
                            matrixData.ProjectLocaleID = drLocale[WebConstants.COL_PROJECT_LOCALE_ID].ToString();
                            matrixData.ProjectPlatformID = drPlatform[WebConstants.COL_PROJECT_PLATFORM_ID].ToString();

                            string requestForm = Request.Form[clientID + "$CheckBoxSelect_" + dtProjectPhasePlatforms.Rows[colCount][WebConstants.COL_PROJECT_PLATFORM_ID].ToString()];
                            if (requestForm == "on")
                                matrixData.IsSelected = true;
                            projectPhaseData.ProjectLocaleVsPlatformMatrix.Add(matrixData);
                        }
                        else if (matrixData.ProjectDataID != "")
                        {
                            projectPhaseData.ProjectLocaleVsPlatformMatrix.Add(matrixData);
                        }
                    }
                }

                isSuccess = BO.BusinessObjects.SaveProjectLocalesVsPlatformsMatrix(projectPhaseData);
            }

            PopulateCoverageDetails(true);
            PopulateLocaleVsPlatformData();

            if (isSuccess)
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_SUCCESS_SAVE_MESSAGE);
            else
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_SAVE_MESSAGE);
        }

        #region Create Suites

        /// <summary>
        /// AssignSuitesForLocalesOnly
        /// </summary>
        private void AssignSuitesForLocalesOnly(DataTable dtMatrixData, DataTable dtSuiteSet, DataTable dtTestCases)
        {
            DataView dvPlatformTypes = dtMatrixData.DefaultView;
            DataTable dtDistinctPlatformTypes = dvPlatformTypes.ToTable(true, WebConstants.COL_PLATFORM_TYPE_ID);

            int totalSets = 0;
            for (int colCount = 0; colCount < dtDistinctPlatformTypes.Rows.Count; colCount++)
            {
                DataView dvLocales = dtMatrixData.DefaultView;
                dvLocales.RowFilter = WebConstants.COL_PLATFORM_TYPE_ID + "=" + dtDistinctPlatformTypes.Rows[colCount][0];
                DataTable dtDistinctLocales = dvLocales.ToTable(true, WebConstants.COL_PROJECT_LOCALE_ID);

                if (dtSuiteSet.Rows.Count == 0 || totalSets != dtSuiteSet.Rows.Count)
                {
                    dtSuiteSet.Rows.Clear();
                    for (int rowCount = 1; rowCount <= dtDistinctLocales.Rows.Count; rowCount++)
                        dtSuiteSet.Rows.Add("SET" + rowCount.ToString());

                    totalSets = dtSuiteSet.Rows.Count;
                    CreateTestSuites(dtSuiteSet, dtTestCases, totalSets);

                    if (ts.ErrorCode != STR_ZERO)
                        return;
                }

                string filterCriteria = WebConstants.COL_PLATFORM_TYPE_ID + "={0} AND " + WebConstants.COL_PROJECT_LOCALE_ID + "={1}";

                for (int rowCount = 0; rowCount < dtDistinctLocales.Rows.Count; rowCount++)
                {
                    DataRow[] drMatrixData = dtMatrixData.Select(String.Format(filterCriteria, dtDistinctPlatformTypes.Rows[colCount][WebConstants.COL_PLATFORM_TYPE_ID], dtDistinctLocales.Rows[rowCount][WebConstants.COL_PROJECT_LOCALE_ID]));
                    if (drMatrixData.Length > 0)
                    {
                        drMatrixData[0][WebConstants.COL_TESTCASES_COUNT] = dtSuiteSet.Rows[(rowCount + colCount) % totalSets][WebConstants.COL_TESTCASES_COUNT];
                        drMatrixData[0][WebConstants.COL_TEST_SUITE_NO] = dtSuiteSet.Rows[(rowCount + colCount) % totalSets][WebConstants.COL_TEST_SUITE_NO];
                    }
                }
            }
        }

        /// <summary>
        /// AssignSuitesForPlatformsOnly
        /// </summary>
        private void AssignSuitesForPlatformsOnly(DataTable dtMatrixData, DataTable dtSuiteSet, DataTable dtTestCases)
        {
            DataView dvLocales = dtMatrixData.DefaultView;
            DataTable dtDistinctLocales = dvLocales.ToTable(true, WebConstants.COL_PROJECT_LOCALE_ID);

            int lastValue = 0;
            for (int rowCount = 0; rowCount < dtDistinctLocales.Rows.Count; rowCount++)
            {
                DataView dvPlatformTypes = dtMatrixData.DefaultView;
                dvPlatformTypes.RowFilter = WebConstants.COL_PROJECT_LOCALE_ID + "=" + dtDistinctLocales.Rows[rowCount][0];
                DataTable dtDistinctPlatformTypes = dvPlatformTypes.ToTable(true, WebConstants.COL_PLATFORM_TYPE_ID);

                string filterCriteria = WebConstants.COL_PLATFORM_TYPE_ID + "={0} AND " + WebConstants.COL_PROJECT_LOCALE_ID + "={1}";

                if (dtDistinctPlatformTypes.Rows.Count == 1)
                {
                    for (int colCount = 0; colCount < dtDistinctPlatformTypes.Rows.Count; colCount++)
                    {
                        DataRow[] drMatrixData = dtMatrixData.Select(String.Format(filterCriteria, dtDistinctPlatformTypes.Rows[colCount][WebConstants.COL_PLATFORM_TYPE_ID], dtDistinctLocales.Rows[rowCount][WebConstants.COL_PROJECT_LOCALE_ID]));
                        if (drMatrixData.Length > 0)
                        {
                            drMatrixData[0][WebConstants.COL_TEST_SUITE_NO] = LabelTestSuite.Text;
                            drMatrixData[0][WebConstants.COL_TESTCASES_COUNT] = LabelTestCasesCount.Text;
                        }
                    }
                }
                else
                {
                    lastValue++;
                    int totalSets = dtSuiteSet.Rows.Count;
                    if (dtSuiteSet.Rows.Count == 0)
                    {
                        for (int colCount = 1; colCount <= dtDistinctPlatformTypes.Rows.Count; colCount++)
                            dtSuiteSet.Rows.Add("SET" + colCount.ToString());

                        totalSets = dtSuiteSet.Rows.Count;
                        CreateTestSuites(dtSuiteSet, dtTestCases, totalSets);
                    }

                    if (ts.ErrorCode != STR_ZERO)
                        return;

                    for (int colCount = 0; colCount < dtDistinctPlatformTypes.Rows.Count; colCount++)
                    {
                        DataRow[] drMatrixData = dtMatrixData.Select(String.Format(filterCriteria, dtDistinctPlatformTypes.Rows[colCount][WebConstants.COL_PLATFORM_TYPE_ID], dtDistinctLocales.Rows[rowCount][WebConstants.COL_PROJECT_LOCALE_ID]));
                        if (drMatrixData.Length > 0)
                        {
                            drMatrixData[0][WebConstants.COL_TEST_SUITE_NO] = dtSuiteSet.Rows[(colCount + lastValue) % totalSets][WebConstants.COL_TEST_SUITE_NO];
                            drMatrixData[0][WebConstants.COL_TESTCASES_COUNT] = dtSuiteSet.Rows[(colCount + lastValue) % totalSets][WebConstants.COL_TESTCASES_COUNT];
                        }
                    }
                }
            }
        }

        /// <summary>
        /// AssignSuitesForPlatformsOnly
        /// </summary>
        private void AssignSuitesForLocalesAndPlatforms(DataTable dtMatrixData, DataTable dtSuiteSet, DataTable dtTestCases)
        {
            dtSuiteSet.Rows.Clear();

            DataView dvLocales = dtMatrixData.DefaultView;
            DataView dvPlatforms = dtMatrixData.DefaultView;

            DataTable dtDistinctLocales = dvLocales.ToTable(true, WebConstants.COL_PROJECT_LOCALE_ID);
            DataTable dtDistinctPlatforms = dvPlatforms.ToTable(true, WebConstants.COL_PROJECT_PLATFORM_ID);

            for (int rowCount = 0; rowCount < dtMatrixData.Rows.Count; rowCount++)
                dtSuiteSet.Rows.Add("SET" + rowCount.ToString());

            int totalSets = dtMatrixData.Rows.Count;
            CreateTestSuites(dtSuiteSet, dtTestCases, totalSets);

            if (ts.ErrorCode != STR_ZERO)
                return;

            for (int rowCount = 0; rowCount < dtMatrixData.Rows.Count; rowCount++)
            {
                dtMatrixData.Rows[rowCount][WebConstants.COL_TEST_SUITE_NO] = dtSuiteSet.Rows[rowCount % totalSets][WebConstants.COL_TEST_SUITE_NO];
                dtMatrixData.Rows[rowCount][WebConstants.COL_TESTCASES_COUNT] = dtSuiteSet.Rows[rowCount % totalSets][WebConstants.COL_TESTCASES_COUNT];
            }
        }

        /// <summary>
        /// CreateTestSuites
        /// </summary>
        private void CreateTestSuites(DataTable dtSuiteSet, DataTable dtTestCases, int totalSets)
        {
            for (int count = 0; count < dtTestCases.Rows.Count; count++)
                dtTestCases.Rows[count][WebConstants.COL_TEST_SUITE_NO] = dtSuiteSet.Rows[count % totalSets][WebConstants.COL_TEST_SUITE_NO];

            foreach (DataRow drSuite in dtSuiteSet.Rows)
            {
                string suiteDetails = LabelProductValue.Text + "-" + LabelProductVersionValue.Text + " : " + DropDownListProjectPhases.SelectedItem.Text + "(" + DropDownListCoverages.SelectedItem.Text + ") - " + drSuite[WebConstants.COL_TEST_SUITE_NO];
                drSuite[WebConstants.COL_TESTCASES_COUNT] = dtTestCases.Select(WebConstants.COL_TEST_SUITE_NO + " = '" + drSuite[WebConstants.COL_TEST_SUITE_NO] + "'").Length;

                ts.TestSuiteTitle = suiteDetails;
                ts.TestCasesCollection = dtTestCases.Select(WebConstants.COL_TEST_SUITE_NO + " = '" + drSuite[WebConstants.COL_TEST_SUITE_NO] + "'");
                drSuite[WebConstants.COL_TEST_SUITE_NO] = BO.BusinessObjects.CreateTestSuite(ts).TestSuiteID;
            }
        }

        #endregion

        #region Generate/Refresh SID

        /// <summary>
        /// PopulateSuiteRunIDDetails
        /// </summary>
        private void PopulateSuiteRunIDDetails(DataTable dtMatrixData)
        {
            ts.ErrorCode = STR_ZERO;
            ts.DataHeader = dataHeader;
            ts.DataHeader.Password = Session[WebConstants.SESSION_PASSWORD].ToString();
            ts.BuildNo = TextBoxBuildNo.Text;

            ts = BO.BusinessObjects.GetLoginSessionID(ts);

            foreach (DataRow drMatrixData in dtMatrixData.Rows)
            {
                string suiteID = drMatrixData[WebConstants.COL_TEST_SUITE_NO].ToString();
                if (suiteID != "" && suiteID.Contains("TS_"))
                {
                    ts.TestSuiteID = suiteID;
                    ts.TestCasesCount = Convert.ToInt32(drMatrixData[WebConstants.COL_TESTCASES_COUNT]);

                    if (drMatrixData[WebConstants.COL_TEST_SUITE_RUN_IDS].ToString() == "")
                        ts = CreateSuiteRunIDs(drMatrixData, ts);

                    if (drMatrixData[WebConstants.COL_TEST_SUITE_RUN_IDS].ToString().Length == COL_SUITE_ID_LENGTH)
                    {
                        ts.TestSuiteRunID = drMatrixData[WebConstants.COL_TEST_SUITE_RUN_IDS].ToString();
                        ts = BO.BusinessObjects.GetTestSuiteRunDetails(ts);
                        drMatrixData[WebConstants.COL_TESTCASES_EXECUTED] = ts.TestCasesExecuted;
                        drMatrixData[WebConstants.COL_TESTCASES_NA] = ts.TestCasesNA;
                    }
                }
            }

            SaveUpdatedMatrixDetails(dtMatrixData, WebConstants.STR_OTMATRIX_ACCESS_UPDATE);
        }

        /// <summary>
        /// CreateSuiteRunIDs
        /// </summary>
        /// <param name="drMatrixData"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        private DTO.TestSudio CreateSuiteRunIDs(DataRow drMatrixData, DTO.TestSudio ts)
        {
            ts = BO.BusinessObjects.GetTestStudioConfigurations(ts);
            if (ts.ErrorCode != STR_ZERO)
                return ts;

            ts.Locale = dtProjectPhaseLocales.Select(WebConstants.COL_PROJECT_LOCALE_ID + "=" + drMatrixData[WebConstants.COL_PROJECT_LOCALE_ID].ToString())[0][WebConstants.COL_LOCALE].ToString();

            if (ts.Configurations.Tables[WebConstants.TBL_LOCALES].Select(WebConstants.COL_LOCALE + "= '" + ts.Locale + "'").Length == 0)
            {
                if (ts.Locale.Contains("French"))
                    ts.Locale = "French";
                else if (ts.Locale.Contains("Spanish"))
                    ts.Locale = "Spanish";
                else
                    ts.Locale = "English";
            }

            ts.Platform = dtProjectPhasePlatforms.Select(WebConstants.COL_PROJECT_PLATFORM_ID + "=" + drMatrixData[WebConstants.COL_PROJECT_PLATFORM_ID].ToString())[0][WebConstants.COL_PLATFORM].ToString();
            if (ts.Configurations.Tables[WebConstants.TBL_PLATFORMS].Select(WebConstants.COL_PLATFORM + "= '" + ts.Platform + "'").Length == 0)
                ts.Platform = "Win All";

            ts.Product = ts.Configurations.Tables[WebConstants.TBL_PRODUCT].Rows[0][WebConstants.COL_PRODUCT].ToString();

            ts = BO.BusinessObjects.GetTestSuiteDetails(ts);
            if (ts.ErrorCode != STR_ZERO)
                return ts;

            DataTable dtTestCases = ts.TestSuiteDetails;
            DataView dvVersions = dtTestCases.DefaultView;
            ts.VersionList = dvVersions.ToTable(true, WebConstants.COL_TEST_CASES_VERSION);

            ts = BO.BusinessObjects.CreateTestSuiteRuns(ts);
            if (ts.ErrorCode != STR_ZERO)
                return ts;

            if (ts.TestSuiteRunID.Contains("SID_"))
                drMatrixData[WebConstants.COL_TEST_SUITE_RUN_IDS] = ts.TestSuiteRunID;

            return ts;
        }

        #endregion

        /// <summary>
        /// SaveUpdatedMatrixDetails
        /// </summary>
        private void SaveUpdatedMatrixDetails(DataTable dtMatrixData, string userView)
        {
            DTO.ProjectPhases projectPhaseData = new DTO.ProjectPhases();
            projectPhaseData.DataHeader = dataHeader;
            projectPhaseData.PhaseCoverageDetailID = DropDownListCoverages.SelectedValue;
            projectPhaseData.ProjectLocaleVsPlatformMatrix = new ArrayList();

            foreach (DataRow drMatrixData in dtMatrixData.Rows)
            {
                DTO.ProjectLocaleVsPlatformMatrix matrixData = new DTO.ProjectLocaleVsPlatformMatrix();
                matrixData.UserAccess = userView;
                matrixData.ProjectDataID = drMatrixData[WebConstants.COL_PROJECT_DATA_ID].ToString();

                if (userView == WebConstants.STR_OTMATRIX_ACCESS_DEFINE)
                {
                    matrixData.TsNo = drMatrixData[WebConstants.COL_TEST_SUITE_NO].ToString();
                    matrixData.TC_Count = drMatrixData[WebConstants.COL_TESTCASES_COUNT].ToString();
                }
                else
                {
                    matrixData.SIDs = drMatrixData[WebConstants.COL_TEST_SUITE_RUN_IDS].ToString();
                    matrixData.TC_Executed = drMatrixData[WebConstants.COL_TESTCASES_EXECUTED].ToString();
                    matrixData.TC_NA = drMatrixData[WebConstants.COL_TESTCASES_NA].ToString();
                }

                projectPhaseData.ProjectLocaleVsPlatformMatrix.Add(matrixData);
            }

            if (BO.BusinessObjects.UpdateProjectLocalesVsPlatformsMatrix(projectPhaseData))
                ts.ErrorMessage = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_SUCCESS_SAVE_MESSAGE);
            else
                ts.ErrorMessage = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_SAVE_MESSAGE);
        }

        #endregion

        #region GridView Events

        /// <summary>
        /// GridViewLocalesVsPlatformMatrix_RowCreated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewLocalesVsPlatformMatrix_RowCreated(object sender, GridViewRowEventArgs e)
        {
            //dtScreenLabels = (DataTable)ViewState[WebConstants.TBL_SCREEN_LABELS];

            //if (e.Row.RowType == DataControlRowType.Header)
            //{
            //    //Creating a gridview object            
            //    GridView objGridView = (GridView)sender;

            //    //Creating a gridview row object
            //    GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            //    //Creating a table cell object
            //    TableCell objtablecell = new TableCell();

            //    #region Merge cells

            //    //Add a blank cell at the first three cell headers
            //    //This can be achieved by making the colspan property of the table cell object as 3
            //    // and the text property of the table cell object will be blank
            //    //Henceforth, add the table cell object to the grid view row object
            //    AddMergedCells(objgridviewrow, objtablecell, 2, COM.GetScreenLocalizedLabel(dtScreenLabels, STR_MERGED_CELL_PARAMETERS), headerBackColor.Name);

            //    if (dtCompileVendorsData != null && vendorDisplayColorSet != null)
            //    {
            //        int counter = 1;
            //        foreach (DataRow dr in dtCompileVendorsData.Rows)
            //        {
            //            AddMergedCells(objgridviewrow, objtablecell, NUM_MERGED_CELL_DATA_VENDOR, dr[WebConstants.COL_VENDOR].ToString(), vendorDisplayColorSet[1 - (counter % 2)].ToString());
            //            counter++;
            //        }
            //    }
            //    //Lastly add the gridrow object to the gridview object at the 0th position
            //    //Because,the header row position is 0.
            //    objGridView.Controls[CONST_ZERO].Controls.AddAt(CONST_ZERO, objgridviewrow);

            //    #endregion
            //}
        }

        /// <summary>
        /// GridViewLocalesVsPlatformMatrix_RowDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewLocalesVsPlatformMatrix_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridViewRow gridRow = e.Row;
                e.Row.Cells[0].BackColor = headerBackColor;

                if (DropDownListUserView.SelectedValue == WebConstants.STR_OTMATRIX_ACCESS_DEFINE)
                {
                    DataRow drLocale = dtProjectPhaseLocales.Rows[gridRow.RowIndex];

                    foreach (DataRow drMatrixPlaform in dtMatrixData.Select(WebConstants.COL_PROJECT_LOCALE_ID + " = " + drLocale[WebConstants.COL_PROJECT_LOCALE_ID]))
                    {
                        //if (drMatrixPlaform[WebConstants.COL_TEST_SUITE_NO].ToString() != "")
                        //{
                        CheckBox CheckBoxSelected = (CheckBox)gridRow.FindControl("CheckBoxSelect_" + drMatrixPlaform[WebConstants.COL_PROJECT_PLATFORM_ID]);
                        CheckBoxSelected.Checked = true;
                        //}
                    }
                }
            }
        }

        /// <summary>
        /// AddMergedCells
        /// </summary>
        /// <param name="drGridView"></param>
        /// <param name="tbcell"></param>
        /// <param name="colspan"></param>
        /// <param name="celltxt"></param>
        /// <param name="backColor"></param>
        protected void AddMergedCells(GridViewRow drGridView, TableCell tbcell, int colspan, string celltxt, string backColor)
        {
            tbcell = new TableCell();
            tbcell.Text = celltxt;
            tbcell.ColumnSpan = colspan;
            tbcell.Style.Add("background-color", backColor);
            tbcell.HorizontalAlign = HorizontalAlign.Center;
            drGridView.Cells.Add(tbcell);
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
            //Session[WebConstants.SESSION_MATRIX_SELECTED_PRODUCT_VERSION_ID] = DropDownListProductVersion.SelectedValue;
            //Session.Remove(WebConstants.SESSION_MATRIX_SELECTED_PROJECT_PHASE_ID);
            //Response.Redirect(WebConstants.PAGE_LOCALIZATION_INSIGHT);
        }

        /// <summary>
        /// DropDownListProjectPhases_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListProjectPhases_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PopulatePhaseCoverages();
            //Session[WebConstants.SESSION_MATRIX_SELECTED_PROJECT_PHASE_ID] = DropDownListProjectPhases.SelectedValue;
            //Response.Redirect(WebConstants.PAGE_LOCALIZATION_INSIGHT);
        }

        /// <summary>
        /// DropDownListSelectProjectPhase_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListSelectProjectPhase_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateCopyOTMatrixPhaseCoverages();
            PopulateLocaleVsPlatformData();
            //Session[WebConstants.SESSION_MATRIX_SELECTED_PROJECT_PHASE_ID] = DropDownListProjectPhases.SelectedValue;
            //Response.Redirect(WebConstants.PAGE_LOCALIZATION_INSIGHT);
        }

        /// <summary>
        /// DropDownListCoverages_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListCoverages_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateCoverageDetails(false);
            PopulateLocaleVsPlatformData();
            //Session[WebConstants.SESSION_MATRIX_SELECTED_PROJECT_PHASE_ID] = DropDownListProjectPhases.SelectedValue;
            //Response.Redirect(WebConstants.PAGE_LOCALIZATION_INSIGHT);
        }

        /// <summary>
        /// DropDownListUserView_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListUserView_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            SetPanelDisplay();
            PopulateLocaleVsPlatformData();
            //Session[WebConstants.SESSION_MATRIX_USER_ACCESS] = DropDownListUserView.SelectedValue;
            //Response.Redirect(WebConstants.PAGE_LOCALIZATION_INSIGHT);
        }

        /// <summary>
        /// DropDownListSelectVendor_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListSelectVendor_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateProjectBuilds();
            PopulateLocaleVsPlatformData();
            //Session[WebConstants.SESSION_MATRIX_SELECTED_VENDOR_ID] = DropDownListSelectVendor.SelectedValue;
            //Response.Redirect(WebConstants.PAGE_LOCALIZATION_INSIGHT);
        }

        /// <summary>
        /// DropDownListLocaleBuilds_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListLocaleBuilds_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateProjectBuildLocales();
            PopulateLocaleVsPlatformData();
            //Session[WebConstants.SESSION_MATRIX_SELECTED_VENDOR_ID] = DropDownListSelectVendor.SelectedValue;
            //Response.Redirect(WebConstants.PAGE_LOCALIZATION_INSIGHT);
        }

        /// <summary>
        /// DropDownListLocales_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListLocales_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateLocaleVsPlatformData();
            //Session[WebConstants.SESSION_MATRIX_SELECTED_VENDOR_ID] = DropDownListSelectVendor.SelectedValue;
            //Response.Redirect(WebConstants.PAGE_LOCALIZATION_INSIGHT);
        }

        #endregion

        #region CheckBox Events

        /// <summary>
        /// CheckBoxPlatformsWin_OnCheckedChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CheckBoxPlatformsWin_OnCheckedChanged(object sender, EventArgs e)
        {
            PopulateLocaleVsPlatformData();
        }

        /// <summary>
        /// CheckBoxPlatformsMac_OnCheckedChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CheckBoxPlatformsMac_OnCheckedChanged(object sender, EventArgs e)
        {
            PopulateLocaleVsPlatformData();
        }

        #endregion

        #region RadioButton Events

        /// <summary>
        /// RadioButtonTestStudio_OnCheckedChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RadioButtonTestStudio_OnCheckedChanged(object sender, EventArgs e)
        {
            if (RadioButtonTestStudio.Checked)
                PanelTestCasesDistribution.Enabled = true;
            else
            {
                PanelTestCasesDistribution.Enabled = false;
                RadioButtonManual.Checked = true;
            }
        }

        /// <summary>
        /// RadioButtonManual_OnCheckedChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RadioButtonManual_OnCheckedChanged(object sender, EventArgs e)
        {
            if (!RadioButtonManual.Checked)
                PanelAcrossLocalesandPlatforms.Enabled = true;
            else
            {
                PanelAcrossLocalesandPlatforms.Enabled = false;
                RadioButtonNone.Checked = true;
            }
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

            DataTable dtProductVersion = BO.BusinessObjects.GetProductVersion(productData).Tables[0];

            LabelProductValue.Text = dtProductVersion.Rows[0][WebConstants.COL_PRODUCT].ToString();

            DropDownListProductVersion.DataSource = dtProductVersion;
            DropDownListProductVersion.DataValueField = WebConstants.COL_PRODUCT_VERSION_ID;
            DropDownListProductVersion.DataTextField = WebConstants.COL_PRODUCT_VERSION;
            DropDownListProductVersion.DataBind();

            if (dtProductVersion.Rows[0][WebConstants.COL_PRODUCT_VERSION_CODE].ToString() != "")
                LabelProductVersionValue.Text = dtProductVersion.Rows[0][WebConstants.COL_PRODUCT_VERSION_CODE].ToString() + " ( " + dtProductVersion.Rows[0][WebConstants.COL_PRODUCT_VERSION].ToString() + " ) ";
            else
                LabelProductVersionValue.Text = dtProductVersion.Rows[0][WebConstants.COL_PRODUCT_VERSION].ToString();

            if (dtProductVersion.Rows[0][WebConstants.COL_PRODUCT_VERSION_ACTIVE].ToString() == "0")
                LabelProductVersionValue.Text += " - " + COM.GetScreenLocalizedLabel(dtScreenLabels, STR_NON_ACTIVE);
            else
                isProductVersionActive = true;

            ViewState[WebConstants.STR_IS_PRODUCT_VERSION_ACTIVE] = isProductVersionActive;

            PopulateProjectPhases();
        }

        /// <summary>
        /// PopulateProjectPhases
        /// </summary>
        private void PopulateProjectPhases()
        {
            DTO.ProjectPhases projectPhaseData = new DTO.ProjectPhases();
            projectPhaseData.ProductVersionID = DropDownListProductVersion.SelectedValue;
            //projectPhaseData.IsActive = true;

            if (isContractor && Session[WebConstants.SESSION_VENDOR_ID].ToString() != null)
                projectPhaseData.VendorID = Session[WebConstants.SESSION_VENDOR_ID].ToString();

            DataTable dtProjectPhases = BO.BusinessObjects.GetProjectPhases(projectPhaseData).Tables[0];
            ViewState[WebConstants.TBL_PROJECT_PHASES] = dtProjectPhases;

            DropDownListProjectPhases.DataSource = dtProjectPhases;
            DropDownListProjectPhases.DataValueField = WebConstants.COL_PROJECT_PHASE_ID;
            DropDownListProjectPhases.DataTextField = WebConstants.COL_PROJECT_PHASE;
            DropDownListProjectPhases.DataBind();

            // For Selected Project Phase
            //if (Session[WebConstants.SESSION_MATRIX_SELECTED_PROJECT_PHASE_ID] != null)
            //    DropDownListProjectPhases.SelectedIndex = DropDownListProjectPhases.Items.IndexOf(DropDownListProjectPhases.Items.FindByValue(Session[WebConstants.SESSION_MATRIX_SELECTED_PROJECT_PHASE_ID].ToString()));
            //else 
            if (Session[WebConstants.SESSION_PROJECT_PHASE_ID] != null)
                DropDownListProjectPhases.SelectedIndex = DropDownListProjectPhases.Items.IndexOf(DropDownListProjectPhases.Items.FindByValue(Session[WebConstants.SESSION_PROJECT_PHASE_ID].ToString()));

            //if (DropDownListSelectVendor.Items.Count == 0)
            //    PopulateVendor();
            //else
            //    PopulateLocaleVsPlatformData();

            if (DropDownListProjectPhases.Items.Count == 0)
            {
                PanelDefineMatrix.Visible = false;
                PanelUpdateStatus.Visible = false;
                PanelCoverages.Enabled = false;
                PanelTestCases.Enabled = false;
                PanelPlatformsAndLocales.Enabled = false;
            }
            else
            {

                DropDownListSelectProjectPhase.DataSource = dtProjectPhases;
                DropDownListSelectProjectPhase.DataValueField = WebConstants.COL_PROJECT_PHASE_ID;
                DropDownListSelectProjectPhase.DataTextField = WebConstants.COL_PROJECT_PHASE;
                DropDownListSelectProjectPhase.DataBind();

                DropDownListSelectProjectPhase.Items.Insert(0, new ListItem("", "-1"));

                PopulatePhaseCoverages();
            }
        }

        /// <summary>
        /// PopulatePhaseCoverages
        /// </summary>
        private void PopulatePhaseCoverages()
        {
            DTO.ProjectPhases projectPhaseData = new DTO.ProjectPhases();
            projectPhaseData.ProjectPhaseID = DropDownListProjectPhases.SelectedValue;
            //projectPhaseData.IsActive = true;

            dtProjectPhaseCoverages = BO.BusinessObjects.GetPhaseCoverageDetails(projectPhaseData).Tables[0];
            ViewState[WebConstants.TBL_PHASE_COVERAGE_DETAILS] = dtProjectPhaseCoverages;

            DropDownListCoverages.DataSource = dtProjectPhaseCoverages;
            DropDownListCoverages.DataValueField = WebConstants.COL_PHASE_COVERAGE_DETAIL_ID;
            DropDownListCoverages.DataTextField = WebConstants.COL_PHASE_COVERAGE_DETAILS;
            DropDownListCoverages.DataBind();

            PopulateCoverageDetails(false);

            DropDownListUserView.Enabled = true;
            SetUserType();

            int phaseStatus = Convert.ToInt32(((DataTable)ViewState[WebConstants.TBL_PROJECT_PHASES]).Select(WebConstants.COL_PROJECT_PHASE_ID + "=" + DropDownListProjectPhases.SelectedValue)[0][WebConstants.COL_PROJECT_PHASE_STATUS_ID]);
            if (phaseStatus != 2)
            {
                if (phaseStatus == 1)
                    DropDownListUserView.SelectedIndex = DropDownListUserView.Items.IndexOf(DropDownListUserView.Items.FindByValue(WebConstants.STR_OTMATRIX_ACCESS_DEFINE));
                if (phaseStatus == 3)
                    DropDownListUserView.SelectedIndex = DropDownListUserView.Items.IndexOf(DropDownListUserView.Items.FindByValue(WebConstants.STR_OTMATRIX_ACCESS_DISPLAY_ONLY));

                DropDownListUserView.Enabled = false;
                SetPanelDisplay();
            }
            PopulateVendors();
        }

        /// <summary>
        /// PopulateCopyOTMatrixPhaseCoverages
        /// </summary>
        private void PopulateCopyOTMatrixPhaseCoverages()
        {
            DTO.ProjectPhases projectPhaseData = new DTO.ProjectPhases();
            projectPhaseData.ProjectPhaseID = DropDownListSelectProjectPhase.SelectedValue;

            DataTable dtCoverages = BO.BusinessObjects.GetPhaseCoverageDetails(projectPhaseData).Tables[0];

            DropDownListSelectCoverage.DataSource = dtCoverages;
            DropDownListSelectCoverage.DataValueField = WebConstants.COL_PHASE_COVERAGE_DETAIL_ID;
            DropDownListSelectCoverage.DataTextField = WebConstants.COL_PHASE_COVERAGE_DETAILS;
            DropDownListSelectCoverage.DataBind();
        }

        /// <summary>
        /// PopulateTotalTestCases
        /// </summary>
        private void PopulateCoverageDetails(bool isRefresh)
        {
            if (dtProjectPhaseCoverages.Rows.Count == 0)
            {
                PanelDistributionType.Enabled = false;
                PanelCopyOTMatrix.Enabled = false;
            }
            else
            {
                if (isRefresh)
                {
                    DTO.ProjectPhases projectPhaseData = new DTO.ProjectPhases();
                    projectPhaseData.ProjectPhaseID = DropDownListProjectPhases.SelectedValue;
                    projectPhaseData.IsActive = true;

                    dtProjectPhaseCoverages = BO.BusinessObjects.GetPhaseCoverageDetails(projectPhaseData).Tables[0];
                    ViewState[WebConstants.TBL_PHASE_COVERAGE_DETAILS] = dtProjectPhaseCoverages;
                }

                DataRow drCoverage = dtProjectPhaseCoverages.Select(WebConstants.COL_PHASE_COVERAGE_DETAIL_ID + " = " + DropDownListCoverages.SelectedValue)[0];

                string tcCount = drCoverage[WebConstants.COL_TESTCASES_COUNT].ToString();
                if (tcCount != "0" && tcCount != "")
                {
                    LabelTestCasesCount.Text = tcCount;
                    LabelTestCasesValue.Text = tcCount;
                    PanelDistributionType.Enabled = true;
                    PanelCopyOTMatrix.Enabled = true;

                    LabelTestSuite.Text = drCoverage[WebConstants.COL_TEST_SUITE_ID].ToString();

                    if (LabelTestSuite.Text != "")
                    {
                        if (LabelTestSuite.Text.Contains("TS_") && LabelTestSuite.Text.Length == COL_TEST_SUITE_LENGTH)
                            LabelTestCasesValue.Text += " (" + String.Format(HYPERLINK_TEST_SUITE_SEARCH, LabelTestSuite.Text) + ")";
                        else
                            LabelTestCasesValue.Text += " (" + LabelTestSuite.Text + ")";
                    }
                    SetRadioButtons(drCoverage[WebConstants.COL_PHASE_COVERAGE_TC_REPOSITORY].ToString(), drCoverage[WebConstants.COL_PHASE_COVERAGE_TC_DISTRIBUTION].ToString(), drCoverage[WebConstants.COL_PHASE_COVERAGE_TC_MATRIX].ToString());

                    DTO.ProjectPhases projectPhaseData = new DTO.ProjectPhases();
                    projectPhaseData.PhaseCoverageDetailID = DropDownListCoverages.SelectedValue;
                    DataTable dtCoverageMatrixDetails = BO.BusinessObjects.GetPhaseExecutableTestCases(projectPhaseData).Tables[0];

                    //if (dtCoverageDetails.Rows.Count > 0 && Convert.ToInt32(dtCoverageDetails.Rows[0][WebConstants.COL_TESTCASES_MATRIX_DATA_COUNT].ToString()) > 0)
                    if (dtCoverageMatrixDetails.Rows.Count > 0)
                    {
                        PanelDistributionType.Enabled = false;
                        PanelCopyOTMatrix.Enabled = false;
                    }
                    else
                    {
                        PanelDistributionType.Enabled = true;
                        PanelCopyOTMatrix.Enabled = true;
                    }
                }
                else
                {
                    LabelTestCasesValue.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, STR_NOT_SET);
                    PanelDistributionType.Enabled = false;
                    PanelCopyOTMatrix.Enabled = false;
                    SetRadioButtons(VAL_TC_REP_DOCUMENT, VAL_TC_DIST_MANUAL, VAL_TC_LOC_PLAT_NONE);
                }
            }
        }

        /// <summary>
        /// PopulateVendors
        /// </summary>
        private void PopulateVendors()
        {
            DTO.ProjectPhases projectPhaseData = new DTO.ProjectPhases();
            projectPhaseData.ProjectPhaseID = DropDownListProjectPhases.SelectedValue;

            DataTable dtVendors = BO.BusinessObjects.GetProjectPhaseVendors(projectPhaseData).Tables[0];

            DataView dv = new DataView(dtVendors);
            dtVendors = dv.ToTable(true, WebConstants.COL_VENDOR_ID, WebConstants.COL_VENDOR);

            if (dtVendors.Rows.Count > 1)
            {
                dtVendors.Rows.InsertAt(dtVendors.NewRow(), 0);
                dtVendors.Rows[0][WebConstants.COL_VENDOR_ID] = STR_ZERO;
                dtVendors.Rows[0][WebConstants.COL_VENDOR] = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.DEF_VAL_TEAMS_ALL);
            }

            DropDownListSelectVendor.DataSource = dtVendors;
            DropDownListSelectVendor.DataValueField = WebConstants.COL_VENDOR_ID;
            DropDownListSelectVendor.DataTextField = WebConstants.COL_VENDOR;
            DropDownListSelectVendor.DataBind();

            // For Selected vendor
            //if (Session[WebConstants.SESSION_MATRIX_SELECTED_VENDOR_ID] != null)
            //    DropDownListSelectVendor.SelectedIndex = DropDownListSelectVendor.Items.IndexOf(DropDownListSelectVendor.Items.FindByValue(Session[WebConstants.SESSION_MATRIX_SELECTED_VENDOR_ID].ToString()));
            //else
            //    DropDownListSelectVendor.SelectedIndex = DropDownListSelectVendor.Items.IndexOf(DropDownListSelectVendor.Items.FindByValue(Session[WebConstants.SESSION_VENDOR_ID].ToString()));

            if (isContractor)
            {
                ListItem item = DropDownListSelectVendor.Items.FindByValue(Session[WebConstants.SESSION_VENDOR_ID].ToString());
                LabelVendorName.Text = "Not Assigned";
                if (item == null)
                    DropDownListSelectVendor.Items.Clear();
                else
                {
                    DropDownListSelectVendor.SelectedIndex = DropDownListSelectVendor.Items.IndexOf(item);
                    LabelVendorName.Text = DropDownListSelectVendor.SelectedItem.Text;
                }
                LabelVendorName.Visible = true;
                DropDownListSelectVendor.Visible = false;
            }

            PopulateProjectBuilds();
            PopulateLocaleVsPlatformData();
        }

        /// <summary>
        /// SetUserType
        /// </summary>
        private void SetUserType()
        {
            PanelSelectUserType.Visible = true;

            //if (isReport && !isReadOnly)
            //{
            //    if (Session[WebConstants.SESSION_MATRIX_USER_ACCESS] != null)
            //        DropDownListUserView.SelectedIndex = DropDownListUserView.Items.IndexOf(DropDownListUserView.Items.FindByValue(Session[WebConstants.SESSION_MATRIX_USER_ACCESS].ToString()));
            //    else
            //        DropDownListUserView.SelectedIndex = DropDownListUserView.Items.IndexOf(DropDownListUserView.Items.FindByValue(WebConstants.STR_OTMATRIX_ACCESS_DISPLAY_ONLY));
            //}
            //else if (isReport)
            //    DropDownListUserView.SelectedIndex = DropDownListUserView.Items.IndexOf(DropDownListUserView.Items.FindByValue(WebConstants.STR_OTMATRIX_ACCESS_OWNER));

            if (isReport)
            {
                //Do nothing
            }
            else if (!isReadOnly)
            {
                ListItem item = DropDownListUserView.Items.FindByValue(WebConstants.STR_OTMATRIX_ACCESS_DEFINE);
                if (item != null)
                    DropDownListUserView.Items.RemoveAt(DropDownListUserView.Items.IndexOf(item));
            }
            else
            {
                ListItem item = DropDownListUserView.Items.FindByValue(WebConstants.STR_OTMATRIX_ACCESS_DEFINE);
                if (item != null)
                    DropDownListUserView.Items.RemoveAt(DropDownListUserView.Items.IndexOf(item));

                item = DropDownListUserView.Items.FindByValue(WebConstants.STR_OTMATRIX_ACCESS_UPDATE);
                if (item != null)
                    DropDownListUserView.Items.RemoveAt(DropDownListUserView.Items.IndexOf(item));
            }

            SetPanelDisplay();
        }

        /// <summary>
        /// PopulateProjectBuilds
        /// </summary>
        private void PopulateProjectBuilds()
        {
            DTO.Product productData = new DTO.Product();
            productData.ProductVersionID = LabelProductVersionID.Text;
            productData.VendorID = DropDownListSelectVendor.SelectedValue;

            DataTable dtProjectBuilds = BO.BusinessObjects.GetProjectBuildDetails(productData).Tables[0];
            if (dtProjectBuilds.Rows.Count > 0)
            {
                dtProjectBuilds = COM.AddEmptyRowtoDataTable(dtProjectBuilds);
                dtProjectBuilds.Rows[0][WebConstants.COL_PROJECT_BUILD] = STR_ZERO;
                dtProjectBuilds.Rows[0][WebConstants.COL_PROJECT_BUILD] = WebConstants.DEF_VAL_LANGUAGE_GROUPS_ALL;

                DropDownListLocaleBuilds.DataSource = dtProjectBuilds;
                DropDownListLocaleBuilds.DataValueField = WebConstants.COL_PROJECT_BUILD_DETAIL_ID;
                DropDownListLocaleBuilds.DataTextField = WebConstants.COL_PROJECT_BUILD;
                DropDownListLocaleBuilds.DataBind();
            }
            PopulateProjectBuildLocales();
        }

        /// <summary>
        /// PopulateProjectBuildLocales
        /// </summary>
        private void PopulateProjectBuildLocales()
        {
            DropDownListLocales.Items.Clear();

            if (DropDownListLocaleBuilds.Items.Count > 0)
            {
                DTO.Product productData = new DTO.Product();
                productData.ProductVersionID = LabelProductVersionID.Text;
                productData.ProjectBuildDetailID = DropDownListLocaleBuilds.SelectedValue;
                productData.VendorID = DropDownListSelectVendor.SelectedValue;
                productData.ProjectPhaseID = DropDownListProjectPhases.SelectedValue;

                DataTable dtProjectBuildLocales = BO.BusinessObjects.GetProjectBuildLocales(productData).Tables[0];

                if (dtProjectBuildLocales.Rows.Count > 0)
                {
                    dtProjectBuildLocales = COM.AddEmptyRowtoDataTable(dtProjectBuildLocales);
                    dtProjectBuildLocales.Rows[0][WebConstants.COL_PRODUCT_LOCALE_ID] = STR_ZERO;
                    dtProjectBuildLocales.Rows[0][WebConstants.COL_LOCALE] = WebConstants.DEF_VAL_LOCALES_ALL;

                    DropDownListLocales.DataSource = dtProjectBuildLocales;
                    DropDownListLocales.DataValueField = WebConstants.COL_PRODUCT_LOCALE_ID;
                    DropDownListLocales.DataTextField = WebConstants.COL_LOCALE;
                    DropDownListLocales.DataBind();
                }
            }
        }

        ///// <summary>
        ///// PopulateLocaleVsPlatformData
        ///// </summary>
        //private void PopulateLocaleVsPlatformData()
        //{
        //    PopulateData();
        //}

        ///// <summary>
        ///// PopulateData
        ///// </summary>
        //private void PopulateData()
        //{
        //    DTO.ProjectPhases projectPhaseData = new DTO.ProjectPhases();
        //    projectPhaseData.ProjectPhaseID = GetSelectedProjectPhaseID();
        //    projectPhaseData.VendorID = DropDownListSelectVendor.SelectedValue;

        //    PopulateProjectPhaseLocales(projectPhaseData);
        //    PopulateProjectPhasePlatforms(projectPhaseData);

        //    SetDisplayLabelAndTextboxes();

        //    Panel panelMatrix = new Panel();
        //    panelMatrix.ID = PANEL_MATRIX;
        //    panelMatrix.EnableViewState = true;
        //    panelMatrix.Controls.Add(CreateLocaleVsPlatformMatrixDynamically());
        //    WrapperPanelMatrix.Controls.Add(panelMatrix);
        //    WrapperPanelMatrix.EnableViewState = true;

        //    if (dtprojectPhaseLocales.Rows.Count == 0 || dtprojectPhasePlatforms.Rows.Count == 0 || DropDownListProductVersion.Items.Count == 0 || DropDownListProjectPhases.Items.Count == 0)
        //    {
        //        ButtonUpdateMatrixManually.Enabled = false;
        //        BuButtonResetMatrix.Enabled = false;
        //    }
        //}

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

            if (DropDownListUserView.SelectedItem.Value == WebConstants.STR_OTMATRIX_ACCESS_DEFINE)
            {
                alDisplayTextBoxes.Add(TC_TS);
                alDisplayLables.Add(TC_SIDS);
                alDisplayTextBoxes.Add(TC_COUNT);
                alDisplayLables.Add(TC_EXECUTED);
                alDisplayLables.Add(TC_NA);
                alDisplayLables.Add(TC_REM);

            }
            else if (DropDownListUserView.SelectedItem.Value == WebConstants.STR_OTMATRIX_ACCESS_UPDATE)
            {
                alDisplayLables.Add(TC_TS);
                alDisplayTextBoxes.Add(TC_SIDS);
                alDisplayLables.Add(TC_COUNT);
                alDisplayTextBoxes.Add(TC_EXECUTED);
                alDisplayTextBoxes.Add(TC_NA);
                alDisplayLables.Add(TC_REM);
            }
            else if (DropDownListUserView.SelectedItem.Value == WebConstants.STR_OTMATRIX_ACCESS_DISPLAY_ONLY)
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

        #region PopulateMatrix

        /// <summary>
        /// PopulateLocaleVsPlatformData
        /// </summary>
        private void PopulateLocaleVsPlatformData()
        {
            GridViewLocalesVsPlatformMatrix.Columns.Clear();

            if (DropDownListCoverages.Items.Count != 0)
            {
                DTO.ProjectLocaleVsPlatformMatrix matrixData = new DTO.ProjectLocaleVsPlatformMatrix();
                matrixData.VendorID = DropDownListSelectVendor.SelectedValue;

                matrixData.PhaseCoverageDetailID = DropDownListCoverages.SelectedValue;

                if (CheckBoxPlatformsWin.Checked && !CheckBoxPlatformsMac.Checked)
                    matrixData.PlatformTypeID = WebConstants.DEF_VAL_WIN_PLATFORM_TYPE_ID;
                else if (!CheckBoxPlatformsWin.Checked && CheckBoxPlatformsMac.Checked)
                    matrixData.PlatformTypeID = WebConstants.DEF_VAL_MAC_PLATFORM_TYPE_ID;

                matrixData.ProjectBuildDetailID = DropDownListLocaleBuilds.SelectedValue;
                matrixData.ProductLocaleID = DropDownListLocales.SelectedValue;

                PopulateProjectLocalesAndPlatforms(matrixData);

                //if (dtProjectPhaseLocales.Rows.Count > 0 && dtProjectPhasePlatforms.Rows.Count > 0)
                //{
                dtMatrixData = BO.BusinessObjects.GetLocaleVsPlatformMatrixData(matrixData).Tables[0];
                ViewState[TBL_MATRIX_DATA] = dtMatrixData;

                DataTable dtGridHeader = new DataTable();
                DataColumn headerCol = new DataColumn(WebConstants.COL_TEMP_GRID_HEADER, Type.GetType(WebConstants.STR_SYSTEM_STRING));
                dtGridHeader.Columns.Add(headerCol);
                headerCol = new DataColumn(WebConstants.COL_TEMP_GRID_HEADER_DISPLAY, Type.GetType(WebConstants.STR_SYSTEM_STRING));
                dtGridHeader.Columns.Add(headerCol);
                headerCol = new DataColumn(WebConstants.COL_TEMP_GRID_PROJECT_LOCALE_ID, Type.GetType(WebConstants.STR_SYSTEM_STRING));
                dtGridHeader.Columns.Add(headerCol);
                headerCol = new DataColumn(WebConstants.COL_TEMP_GRID_TEST_SUITE, Type.GetType(WebConstants.STR_SYSTEM_STRING));
                dtGridHeader.Columns.Add(headerCol);
                headerCol = new DataColumn(WebConstants.COL_TEMP_GRID_SID_INFORMATION, Type.GetType(WebConstants.STR_SYSTEM_STRING));
                dtGridHeader.Columns.Add(headerCol);

                dtGridHeader.Rows.Add(dtGridHeader.NewRow());
                dtGridHeader.Rows[0][WebConstants.COL_TEMP_GRID_HEADER] = WebConstants.COL_LOCALE;
                dtGridHeader.Rows[0][WebConstants.COL_TEMP_GRID_HEADER_DISPLAY] = WebConstants.COL_LOCALE;
                dtGridHeader.Rows[0][WebConstants.COL_TEMP_GRID_PROJECT_LOCALE_ID] = WebConstants.COL_PROJECT_LOCALE_ID;

                foreach (DataRow drPlatform in dtProjectPhasePlatforms.Rows)
                {
                    DataRow tempRow = dtGridHeader.NewRow();
                    tempRow[WebConstants.COL_TEMP_GRID_HEADER] = drPlatform[WebConstants.COL_PROJECT_PLATFORM_ID].ToString();
                    tempRow[WebConstants.COL_TEMP_GRID_HEADER_DISPLAY] = drPlatform[WebConstants.COL_PLATFORM].ToString();
                    dtGridHeader.Rows.Add(tempRow);
                }

                DataTable dtMatrixGridData = new DataTable();

                int counter = 0;
                foreach (DataRow drGridHeader in dtGridHeader.Rows)
                {
                    if (counter == 0)
                    {
                        GridViewLocalesVsPlatformMatrix.Columns.Add(CreateTemplateField(drGridHeader, ListItemType.Header));
                        DataColumn col = new DataColumn(drGridHeader[WebConstants.COL_TEMP_GRID_HEADER].ToString(), Type.GetType(WebConstants.STR_SYSTEM_STRING));
                        dtMatrixGridData.Columns.Add(col);
                    }
                    else
                    {
                        GridViewLocalesVsPlatformMatrix.Columns.Add(CreateTemplateField(drGridHeader, ListItemType.Item));

                        DataColumn col = new DataColumn("TestSuite_" + drGridHeader[WebConstants.COL_TEMP_GRID_HEADER].ToString(), Type.GetType(WebConstants.STR_SYSTEM_STRING));
                        dtMatrixGridData.Columns.Add(col);

                        col = new DataColumn("SuiteID_" + drGridHeader[WebConstants.COL_TEMP_GRID_HEADER].ToString(), Type.GetType(WebConstants.STR_SYSTEM_STRING));
                        dtMatrixGridData.Columns.Add(col);
                    }
                    counter++;
                }

                string suiteLabel = "";
                string suiteRunLabel = "";

                if (RadioButtonTestStudio.Checked)
                {
                    suiteLabel = "TS# ";
                    suiteRunLabel = "SID# ";
                }

                foreach (DataRow drLocale in dtProjectPhaseLocales.Rows)
                {
                    DataRow drData = dtMatrixGridData.NewRow();
                    drData[0] = drLocale[WebConstants.COL_LOCALE];

                    foreach (DataRow drPlatform in dtMatrixData.Select(WebConstants.COL_PROJECT_LOCALE_ID + " = " + drLocale[WebConstants.COL_PROJECT_LOCALE_ID]))
                    {
                        string testSuite = drPlatform[WebConstants.COL_TEST_SUITE_NO].ToString();

                        if (testSuite != "")
                        {
                            string platformID = drPlatform[WebConstants.COL_PROJECT_PLATFORM_ID].ToString();
                            string col_testSuite = COL_TEST_SUITE + platformID;
                            string col_suiteID = COL_SUITE_ID + platformID;

                            int testCasesCount = 0;
                            if (drPlatform[WebConstants.COL_TESTCASES_COUNT].ToString() != "")
                                testCasesCount = Convert.ToInt32(drPlatform[WebConstants.COL_TESTCASES_COUNT].ToString());

                            int testCasesExecuted = 0;
                            if (drPlatform[WebConstants.COL_TESTCASES_EXECUTED].ToString() != "")
                                testCasesExecuted = Convert.ToInt32(drPlatform[WebConstants.COL_TESTCASES_EXECUTED]);

                            int testCasesNA = 0;
                            if (drPlatform[WebConstants.COL_TESTCASES_NA].ToString() != "")
                                testCasesNA = Convert.ToInt32(drPlatform[WebConstants.COL_TESTCASES_NA]);

                            int testCasesRem = testCasesCount - testCasesExecuted - testCasesNA;

                            if (RadioButtonTestStudio.Checked && testSuite.Contains("TS_") && testSuite.Length == COL_TEST_SUITE_LENGTH)
                                testSuite = String.Format(HYPERLINK_TEST_SUITE_SEARCH, testSuite);

                            if (DropDownListUserView.SelectedValue == WebConstants.STR_OTMATRIX_ACCESS_DEFINE)
                                drData[col_testSuite] = "<BR>" + suiteLabel + testSuite + " (" + testCasesCount.ToString() + ") ";
                            else
                                drData[col_testSuite] = suiteLabel + testSuite + " (" + testCasesCount.ToString() + ") ";

                            string suiteRunID = drPlatform[WebConstants.COL_TEST_SUITE_RUN_IDS].ToString();
                            string suiteRunLabelHeader = suiteRunLabel;
                            if (suiteRunID != "" && RadioButtonTestStudio.Checked && suiteRunID.Contains("SID_") && suiteRunID.Length == COL_SUITE_ID_LENGTH)
                                suiteRunID = String.Format(HYPERLINK_SUITE_ID_SEARCH, suiteRunID);
                            else if (suiteRunID.Trim() == "")
                                suiteRunID = "TBD";
                            else if (RadioButtonTestStudio.Checked)
                                suiteRunLabelHeader = "";

                            drData[col_suiteID] = "<BR>" + suiteRunLabelHeader + suiteRunID + String.Format(" (Ex={0}, NA={1}, Rem={2})", testCasesExecuted.ToString(), testCasesNA.ToString(), testCasesRem.ToString());
                        }
                    }

                    dtMatrixGridData.Rows.Add(drData);
                }

                GridViewLocalesVsPlatformMatrix.DataSource = dtMatrixGridData;
                GridViewLocalesVsPlatformMatrix.DataBind();

                ViewState[WebConstants.TBL_COVERAGE_DETAILS] = dtProjectPhaseCoverages;

                EnableDisableButtonDisplay();
                //}
            }
        }

        /// <summary>
        /// PopulateProjectLocalesAndPlatforms
        /// </summary>
        private void PopulateProjectLocalesAndPlatforms(DTO.ProjectLocaleVsPlatformMatrix matrixData)
        {
            DTO.ProjectPhases projectPhaseData = new DTO.ProjectPhases();
            projectPhaseData.ProjectPhaseID = GetSelectedProjectPhaseID();
            projectPhaseData.VendorID = matrixData.VendorID;
            projectPhaseData.PlatformTypeID = matrixData.PlatformTypeID;
            projectPhaseData.ProjectBuildDetailID = matrixData.ProjectBuildDetailID;
            projectPhaseData.ProductLocaleID = matrixData.ProductLocaleID;

            PopulateProjectPhaseLocales(projectPhaseData);
            PopulateProjectPhasePlatforms(projectPhaseData);
        }

        /// <summary>
        /// PopulateProjectPhaseLocales
        /// </summary>
        private void PopulateProjectPhaseLocales(DTO.ProjectPhases projectPhaseData)
        {
            dtProjectPhaseLocales = BO.BusinessObjects.GetProjectLocales(projectPhaseData).Tables[0];

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
        /// CreateTemplateField
        /// </summary>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        private DataControlField CreateTemplateField(DataRow drtemplateField, ListItemType itemtype)
        {
            TemplateField tf = new TemplateField();
            tf.HeaderText = drtemplateField[WebConstants.COL_TEMP_GRID_HEADER_DISPLAY].ToString();
            tf.ItemTemplate = new GridViewMatrixTemplate(itemtype, drtemplateField, DropDownListUserView.SelectedValue);
            return tf;
        }

        /// <summary>
        /// SetPanelDisplay
        /// </summary>
        private void SetPanelDisplay()
        {
            PanelDefineMatrix.Visible = false;
            PanelUpdateStatus.Visible = false;

            if (DropDownListUserView.SelectedValue == WebConstants.STR_OTMATRIX_ACCESS_DEFINE)
                PanelDefineMatrix.Visible = true;
            else if (DropDownListUserView.SelectedValue == WebConstants.STR_OTMATRIX_ACCESS_UPDATE)
                PanelUpdateStatus.Visible = true;

            if (DropDownListCoverages.Items.Count > 0)
            {
                //if (DropDownListUserView.SelectedValue == WebConstants.STR_OTMATRIX_ACCESS_DISPLAY_ONLY)
                //{
                //    if (DropDownListCoverages.Items[0].Value != STR_ZERO)
                //        DropDownListCoverages.Items.Add(new ListItem(COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.DEF_VAL_PHASE_COVERAGES_ALL), STR_ZERO));
                //}
                //else 
                if (DropDownListCoverages.Items[0].Value == STR_ZERO)
                    DropDownListCoverages.Items.RemoveAt(0);
            }
        }

        /// <summary>
        /// SetRadioButtons
        /// </summary>
        /// <param name="tcRep"></param>
        /// <param name="tcDist"></param>
        /// <param name="pLap"></param>
        private void GetSelectedRadioButtonInformation(DTO.ProjectPhases projectPhaseData)
        {
            #region TestCases Repository

            if (RadioButtonTestStudio.Checked)
                projectPhaseData.TestCasesRepositorySetting = VAL_TC_REP_TS;
            else
                projectPhaseData.TestCasesRepositorySetting = VAL_TC_REP_DOCUMENT;

            #endregion

            #region TestCases Distribution

            if (RadioButtonManual.Checked)
                projectPhaseData.TestCasesDistributeSetting = VAL_TC_DIST_MANUAL;
            else if (RadioButtonEqual.Checked)
                projectPhaseData.TestCasesDistributeSetting = VAL_TC_DIST_EQUAL;
            else
                projectPhaseData.TestCasesDistributeSetting = VAL_TC_DIST_WEIGHT;

            #endregion

            #region Across Distribution

            if (RadioButtonNone.Checked)
                projectPhaseData.AcrossBlockSetting = VAL_TC_LOC_PLAT_NONE;
            else if (RadioButtonLocales.Checked)
                projectPhaseData.AcrossBlockSetting = VAL_TC_LOC_PLAT_LOC;
            else if (RadioButtonPlatforms.Checked)
                projectPhaseData.AcrossBlockSetting = VAL_TC_LOC_PLAT_PLAT;
            else
                projectPhaseData.AcrossBlockSetting = VAL_TC_LOC_PLAT_BOTH;

            #endregion
        }

        /// <summary>
        /// SetRadioButtons
        /// </summary>
        /// <param name="tcRep"></param>
        /// <param name="tcDist"></param>
        /// <param name="plap"></param>
        private void SetRadioButtons(string tcRep, string tcDist, string plap)
        {
            #region TestCases Repository

            if (tcRep == VAL_TC_REP_TS)
            {
                RadioButtonTestStudio.Checked = true;
                RadioButtonDocument.Checked = false;
            }
            else if (tcRep == VAL_TC_REP_DOCUMENT)
            {
                RadioButtonTestStudio.Checked = false;
                RadioButtonDocument.Checked = true;
            }

            #endregion

            #region TestCases Distribution

            if (tcDist == VAL_TC_DIST_MANUAL)
            {
                RadioButtonManual.Checked = true;
                RadioButtonEqual.Checked = false;
                RadioButtonLocaleWeight.Checked = false;

            }
            else if (tcDist == VAL_TC_DIST_EQUAL)
            {
                RadioButtonManual.Checked = false;
                RadioButtonEqual.Checked = true;
                RadioButtonLocaleWeight.Checked = false;
            }
            else if (tcDist == VAL_TC_DIST_WEIGHT)
            {
                RadioButtonManual.Checked = false;
                RadioButtonEqual.Checked = false;
                RadioButtonLocaleWeight.Checked = true;
            }

            #endregion

            #region Across Distribution

            if (plap == VAL_TC_LOC_PLAT_NONE)
            {
                RadioButtonNone.Checked = true;
                RadioButtonLocales.Checked = false;
                RadioButtonPlatforms.Checked = false;
                RadioButtonBoth.Checked = false;
            }
            else if (plap == VAL_TC_LOC_PLAT_LOC)
            {
                RadioButtonNone.Checked = false;
                RadioButtonLocales.Checked = true;
                RadioButtonPlatforms.Checked = false;
                RadioButtonBoth.Checked = false;
            }
            else if (plap == VAL_TC_LOC_PLAT_PLAT)
            {
                RadioButtonNone.Checked = false;
                RadioButtonLocales.Checked = false;
                RadioButtonPlatforms.Checked = true;
                RadioButtonBoth.Checked = false;
            }
            else if (plap == VAL_TC_LOC_PLAT_BOTH)
            {
                RadioButtonNone.Checked = false;
                RadioButtonLocales.Checked = false;
                RadioButtonPlatforms.Checked = false;
                RadioButtonBoth.Checked = true;
            }

            #endregion
        }

        /// <summary>
        /// EnableDisableButtonDisplay
        /// </summary>
        private void EnableDisableButtonDisplay()
        {
            bool isMatrixCreated = true;
            if (dtMatrixData.Rows.Count == 0)
                isMatrixCreated = false;
            //ButtonResetMatrix.OnClientClick = string.Format(WebConstants.CONFIRM_MESSAGE, CONFIRM_MESSAGE_MATRIX_RESET);
            //ButtonCreateTestSuite.OnClientClick = string.Format(WebConstants.CONFIRM_MESSAGE, CONFIRM_MESSAGE_CREATE_SUITE);

            //PanelDistributionType.Enabled = true;
            //if (GridViewLocalesVsPlatformMatrix.Rows.Count > 0)
            //{
            //    PanelDistributionType.Enabled = false;
            //}

            ButtonSaveMatrix.Enabled = true;
            ButtonCreateTestSuite.Enabled = isMatrixCreated;
            ButtonResetMatrix.Enabled = isMatrixCreated;
            ButtonUpdateMatrixManually.Enabled = isMatrixCreated;
            ButtonRefreshSID.Enabled = isMatrixCreated;
            TextBoxBuildNo.Visible = isMatrixCreated;
            ButtonUpdateSIDMatrixManually.Enabled = isMatrixCreated;

            if (isMatrixCreated)
            {
                ButtonResetMatrix.OnClientClick = string.Format(WebConstants.CONFIRM_MESSAGE, CONFIRM_MESSAGE_MATRIX_RESET);
                ButtonCreateTestSuite.OnClientClick = string.Format(WebConstants.CONFIRM_MESSAGE, CONFIRM_MESSAGE_CREATE_SUITE);
                ButtonSaveMatrix.OnClientClick = string.Format(WebConstants.CONFIRM_MESSAGE, CONFIRM_MESSAGE_MATRIX_SAVE);
            }
            else
            {
                ButtonSaveMatrix.OnClientClick = "";
            }

            if (RadioButtonDocument.Checked)
            {
                ButtonCreateTestSuite.Enabled = false;
                ButtonRefreshSID.Enabled = false;
                TextBoxBuildNo.Visible = false;
            }
            else
            {
                ButtonCreateTestSuite.Enabled = isMatrixCreated;
                ButtonRefreshSID.Enabled = true;
                TextBoxBuildNo.Visible = true;
            }
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
            tdWidthCount = VAL_MAX_PIXEL_PER / tdWidthCount;

            DTO.ProjectLocaleVsPlatformMatrix matrixData = new DTO.ProjectLocaleVsPlatformMatrix();
            matrixData.ProjectPhaseID = DropDownListProjectPhases.SelectedValue;
            matrixData.VendorID = DropDownListSelectVendor.SelectedValue;

            dtMatrixData = BO.BusinessObjects.GetLocaleVsPlatformMatrixData(matrixData).Tables[0];
            ViewState[TBL_MATRIX_DATA] = dtMatrixData;

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

            foreach (DataRow drPlatform in dtProjectPhasePlatforms.Rows)
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

            foreach (DataRow drLocale in dtProjectPhaseLocales.Rows)
            {
                tr = SetTableRowProperties();
                string localeIDtext = WebConstants.COL_LOCALE_ID + "_" + drLocale[WebConstants.COL_PROJECT_LOCALE_ID].ToString();
                tdLabel = SetTableCellPropertiesWithBorders(AddLabel(localeIDtext, drLocale[WebConstants.COL_LOCALE].ToString()));
                tdLabel.BackColor = tdBgColor;
                tdLabel.HorizontalAlign = HorizontalAlign.Center;
                tr.Controls.Add(tdLabel);
                //displayLabelcounter = 0;

                tr.Controls.Add(CreateTableCellForDataLabels(localeIDtext, false));
                foreach (DataRow drPlatform in dtProjectPhasePlatforms.Rows)
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

            if ((DropDownListUserView.SelectedItem.Value == WebConstants.STR_OTMATRIX_ACCESS_DEFINE) || dr[WebConstants.COL_TEST_SUITE_NO].ToString() != "")
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
                if (drScreenAccess[WebConstants.COL_SCREEN_IS_REPORT].ToString() == WebConstants.DEF_VAL_ACCESS_CHECKED)
                    isReport = true;
            }
        }

        /// <summary>
        /// SetScreenAccess
        /// </summary>
        private void SetScreenAccess()
        {
            if (!isProductVersionActive)
                isReadOnly = true;

            //if (isReadOnly)
            //{
            //    PanelDefineMatrix.Visible = false;
            //    PanelUpdateStatus.Visible = false;
            //}

            if (System.Configuration.ConfigurationManager.AppSettings[WebConstants.SETTING_ENVIRONMENT] != WebConstants.SETTING_ENVIRONMENT_PROD)
                LabelTestStudioIntegration.Visible = true;

            if (dtProjectPhaseLocales != null)
                AddFooterPadding(dtProjectPhaseLocales.Rows.Count);
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

            if (Session[WebConstants.SESSION_LOCALE_ID] != null && Session[WebConstants.SESSION_LOCALE_ID].ToString() != WebConstants.DEF_VAL_LOCALE_ID)
            {
                LabelHeader.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelHeader.Text);
                LabelProduct.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelProduct.Text);
                LabelProductVersion.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelProductVersion.Text);
                LabelProjectPhases.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelProjectPhases.Text);
                LabelTotalTestCases.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelTotalTestCases.Text);
                LabelSelectVendor.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelSelectVendor.Text);
                LabelSelectUserView.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelSelectUserView.Text);
                LabelDistributionType.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelDistributionType.Text);
                LabelTestCasesDistribution.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelTestCasesDistribution.Text);
                LabelAcrossLocalesandPlatforms.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelAcrossLocalesandPlatforms.Text);
                LabelCoverages.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelCoverages.Text);
                LabelPlatformsAndLocales.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelPlatformsAndLocales.Text);
                LabelTestCasesRepository.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelTestCasesRepository.Text);

                CheckBoxPlatformsWin.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, CheckBoxPlatformsWin.Text);
                CheckBoxPlatformsMac.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, CheckBoxPlatformsMac.Text);

                RadioButtonTestStudio.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, RadioButtonTestStudio.Text);
                RadioButtonDocument.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, RadioButtonDocument.Text);
                RadioButtonManual.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, RadioButtonManual.Text);
                RadioButtonEqual.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, RadioButtonEqual.Text);
                RadioButtonLocaleWeight.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, RadioButtonLocaleWeight.Text);
                RadioButtonNone.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, RadioButtonNone.Text);
                RadioButtonLocales.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, RadioButtonLocales.Text);
                RadioButtonPlatforms.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, RadioButtonPlatforms.Text);
                RadioButtonBoth.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, RadioButtonBoth.Text);

                ButtonResetMatrix.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, ButtonResetMatrix.Text);

                foreach (ListItem item in DropDownListUserView.Items)
                    item.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, item.Text);
            }

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

    #region GridViewMatrixTemplate Class

    /// <summary>
    /// GridViewMatrixTemplate
    /// </summary>
    public class GridViewMatrixTemplate : ITemplate
    {
        private ListItemType templateType;
        private DataRow drtemplateField;
        private string userView;
        private const string COL_TEST_SUITE = "TestSuite_";
        private const string COL_SUITE_ID = "SuiteID_";

        /// <summary>
        /// GridViewMatrixTemplate
        /// </summary>
        /// <param name="type"></param>
        /// <param name="drField"></param>
        public GridViewMatrixTemplate(ListItemType type, DataRow drField, string dataView)
        {
            templateType = type;
            drtemplateField = drField;
            userView = dataView;
        }

        /// <summary>
        /// InstantiateIn
        /// </summary>
        /// <param name="container"></param>
        void ITemplate.InstantiateIn(System.Web.UI.Control container)
        {
            switch (templateType)
            {
                case ListItemType.Item:

                    if (userView == WebConstants.STR_OTMATRIX_ACCESS_DEFINE)
                    {
                        CheckBox CheckBoxSelect = new CheckBox();
                        CheckBoxSelect.ID = "CheckBoxSelect_" + drtemplateField[WebConstants.COL_TEMP_GRID_HEADER].ToString();
                        container.Controls.Add(CheckBoxSelect);
                    }

                    Label LabelTSInformation = new Label();
                    LabelTSInformation.ID = "LabelTSInformation_" + drtemplateField[WebConstants.COL_TEMP_GRID_HEADER].ToString();
                    LabelTSInformation.DataBinding += new EventHandler(LabelTestSuiteDataBinding);
                    container.Controls.Add(LabelTSInformation);

                    Label LabelSIDInformation = new Label();
                    LabelSIDInformation.ID = "LabelSIDInformation_" + drtemplateField[WebConstants.COL_TEMP_GRID_HEADER].ToString();
                    LabelSIDInformation.DataBinding += new EventHandler(LabelSuiteIDDataBinding);
                    container.Controls.Add(LabelSIDInformation);

                    break;

                case ListItemType.Header:

                    Label LabelLocale = new Label();
                    LabelLocale.ID = "Label" + drtemplateField[WebConstants.COL_TEMP_GRID_HEADER].ToString();
                    LabelLocale.DataBinding += new EventHandler(LabelLocaleDataBinding);
                    container.Controls.Add(LabelLocale);

                    Label LabelProjectLocaleID = new Label();
                    LabelProjectLocaleID.ID = "Label" + drtemplateField[WebConstants.COL_TEMP_GRID_PROJECT_LOCALE_ID].ToString();
                    LabelProjectLocaleID.DataBinding += new EventHandler(LabelProjectLocaleIDDataBinding);
                    container.Controls.Add(LabelLocale);

                    break;
            }
        }

        /// <summary>
        /// LabelLocaleDataBinding
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LabelLocaleDataBinding(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            GridViewRow container = (GridViewRow)lbl.NamingContainer;
            object dataValue = DataBinder.Eval(container.DataItem, drtemplateField[WebConstants.COL_TEMP_GRID_HEADER].ToString());
            if (dataValue != DBNull.Value)
                lbl.Text = dataValue.ToString();
        }

        /// <summary>
        /// LabelLocaleDataBinding
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LabelProjectLocaleIDDataBinding(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            GridViewRow container = (GridViewRow)lbl.NamingContainer;
            object dataValue = DataBinder.Eval(container.DataItem, drtemplateField[WebConstants.COL_TEMP_GRID_PROJECT_LOCALE_ID].ToString());
            if (dataValue != DBNull.Value)
                lbl.Text = dataValue.ToString();
        }

        /// <summary>
        /// LabelTestSuiteDataBinding
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LabelTestSuiteDataBinding(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            GridViewRow container = (GridViewRow)lbl.NamingContainer;
            object dataValue = DataBinder.Eval(container.DataItem, COL_TEST_SUITE + drtemplateField[WebConstants.COL_TEMP_GRID_HEADER].ToString());
            if (dataValue != DBNull.Value)
                lbl.Text = dataValue.ToString();
        }

        /// <summary>
        /// LabelSuiteIDDataBinding
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LabelSuiteIDDataBinding(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            GridViewRow container = (GridViewRow)lbl.NamingContainer;
            object dataValue = DataBinder.Eval(container.DataItem, COL_SUITE_ID + drtemplateField[WebConstants.COL_TEMP_GRID_HEADER].ToString());
            if (dataValue != DBNull.Value)
                lbl.Text = dataValue.ToString();
        }
    }

    #endregion
}