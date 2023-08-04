using System;
using System.Web;
using System.Collections.Generic;

namespace Adobe.Localization.Insights.Common
{
    /// <summary>
    /// WebConstants
    /// </summary>
    public class WebConstants
    {
        #region TABLE NAMES

        public const string TBL_COVERAGE_DETAILS = "PhaseCoverageDetails";
        public const string TBL_LOCALE_TIERS = "LocaleTiers";
        public const string TBL_LOCALES = "Locales";
        public const string TBL_PLATFORMS = "Platforms";
        public const string TBL_PHASE_COVERAGE_DETAILS = "PhaseCoverageDetails";
        public const string TBL_PHASE_TYPES = "PhaseTypes";
        public const string TBL_PRODUCT = "Products";
        public const string TBL_PROJECT_BUILD_DETAILS = "ProjectBuildDetails";
        public const string TBL_PRODUCT_FEATURES = "ProductFeatures";
        public const string TBL_PRODUCT_LINKS = "ProductLinks";
        public const string TBL_PRODUCT_LOCALES = "ProductLocales";
        public const string TBL_PRODUCT_PLATFORMS = "ProductPlatforms";
        public const string TBL_PRODUCT_SPRINTS = "ProductSprints";
        public const string TBL_PRODUCT_VERSIONS = "ProductVersions";
        public const string TBL_PROJECT_ROLES = "ProjectRoles";
        public const string TBL_PROJECT_PHASE_LOCALES = "ProjectLocales";
        public const string TBL_PROJECT_PHASE_PLATFORMS = "ProjectPlatforms";
        public const string TBL_PROJECT_PHASES = "ProjectPhases";
        public const string TBL_RELEASE_TYPES = "ReleaseTypes";
        public const string TBL_SCREEN_ACCESS_LEVELS = "ScreenAccessLevels";
        public const string TBL_SCREEN_LABELS = "ScreenLabels";
        public const string TBL_SCREENS = "Screens";
        public const string TBL_STATUS = "Status";
        public const string TBL_USER_PROJECT_ROLES = "UserProjectRoles";
        public const string TBL_USERS = "Users";
        public const string TBL_VENDORS = "Vendors";
        public const string TBL_VENDOR_TYPES = "VendorTypes";
        public const string TBL_WSR_DATA = "WSRData";
        public const string TBL_WSR_RESOURCE_DATA = "WSRResourceData";
        public const string TBL_WSR_PARAMETERS = "WSRParameters";
        public const string TBL_WSR_OUTSTANDING_DELIVERABLES = "OutstandingDeliverables";

        public const string DTO_WSR_DATA = "WSRData";
        public const string TBL_TEMP_GRID_HEADER = "GridHeader";
        public const string TBL_TEMP_COMPILED_EFFORTS = "CompiledEfforts";
        public const string TBL_TEMP_ACTUAL_EFFORTS = "ActualEfforts";
        public const string TBL_TEMP_REVISED_EFFORTS = "RevisedEfforts";
        public const string TBL_TEMP_VENDOR_LOCALES = "VendorLocales";
        public const string TBL_TEMP_ACTUAL_TC_EXECUTED = "ActualTestCasesExecuted";

        #endregion

        #region COLUMN NAMES

        public const string COL_STR_COLUMN_NAMES = "ColumnNames";
        public const string COL_STR_TYPE_TABLE_DETAILS = "TypeDetailsDataTable";

        public const string COL_ID = "ID";
        public const string COL_CODE = "Code";
        public const string COL_TYPE = "Type";
        public const string COL_DESCRIPTION = "Description";

        public const string COL_LOCALE_ID = "LocaleID";
        public const string COL_LOCALE_CODE = "LocaleCode";
        public const string COL_LOCALE = "Locale";
        public const string COL_LOCALE_WEIGHT = "LocaleWeight";
        public const string COL_FALLBACK_LOCALE_ID = "FallBackLocaleID";
        public const string COL_PRODUCT_LOCALE_ID = "ProductLocaleID";

        public const string COL_PLATFORM_ID = "PlatformID";
        public const string COL_PLATFORM = "Platform";
        public const string COL_PLATFORM_TYPE_ID = "PlatformTypeID";
        public const string COL_PRODUCT_PLATFORM_ID = "ProductPlatformID";
        public const string COL_PRIORITY = "Priority";

        public const string COL_VENDOR_ID = "VendorID";
        public const string COL_VENDOR = "Vendor";
        public const string COL_VENDOR_IS_CONTRACTOR = "IsContractor";
        public const string COL_VENDOR_TYPE_ID = "VendorTypeID";
        public const string COL_VENDOR_TYPE = "VendorType";

        public const string COL_USER_ID = "UserID";
        public const string COL_LOGIN_ID = "LoginID";
        public const string COL_FIRST_NAME = "FirstName";
        public const string COL_LAST_NAME = "LastName";
        public const string COL_NICK_NAME = "NickName";
        public const string COL_EMAIL_ID = "EmailID";
        public const string COL_ALTERNATE_EMAIL_ID = "AlternateEmailID";
        public const string COL_CONTACT_NO = "ContactNo";
        public const string COL_MANAGER_LOGIN_ID = "ManagerLoginID";
        public const string COL_USER_NAME = "UserName";
        public const string COL_SUPER_USER = "SuperUser";

        public const string COL_SCREEN_ID = "ScreenID";
        public const string COL_SCREEN = "Screen";
        public const string COL_SCREEN_PARENT_ID = "ParentScreenID";
        public const string COL_SCREEN_IDENTIFIER = "ScreenIdentifier";
        public const string COL_PAGE_NAME = "PageName";
        public const string COL_SCREEN_VALUE = "ScreenValue";
        public const string COL_SCREEN_LABEL = "ScreenLabel";
        public const string COL_SCREEN_LOCALIZED_VALUE = "LocalizedValue";
        public const string COL_SCREEN_IS_READ = "IsRead";
        public const string COL_SCREEN_IS_READ_WRITE = "IsReadWrite";
        public const string COL_SCREEN_IS_REPORT = "IsReport";
        public const string COL_SCREEN_SEQUENCE = "Sequence";
        public const string COL_SCREEN_IS_PRODUCT_TYPE = "IsProductType";

        public const string COL_PRODUCT_ID = "ProductID";
        public const string COL_PRODUCT = "Product";
        public const string COL_PRODUCT_VERSION_ID = "ProductVersionID";
        public const string COL_PRODUCT_VERSION = "ProductVersion";
        public const string COL_PRODUCT_VERSION_CODE = "ProductCodeName";
        public const string COL_PRODUCT_YEAR = "ProductYear";
        public const string COL_PRODUCT_ABOUT = "AboutProduct";
        public const string COL_PRODUCT_VERSION_ACTIVE = "IsActive";
        public const string COL_PRODUCT_IS_ENABLED = "IsEnabled";
        public const string COL_USER_PRODUCT_PREFERENCE_ID = "UserProductPreferenceID";

        public const string COL_PROJECT_PHASE_ID = "ProjectPhaseID";
        public const string COL_PROJECT_PHASE = "ProjectPhase";
        public const string COL_PROJECT_PHASE_ABOUT = "AboutProjectPhase";
        public const string COL_PROJECT_PHASE_STATUS_ID = "StatusID";
        public const string COL_PROJECT_PHASE_START_DATE = "PhaseStartDate";
        public const string COL_PROJECT_PHASE_END_DATE = "PhaseEndDate";
        public const string COL_PHASE_COVERAGE_DETAIL_ID = "PhaseCoverageDetailID";
        public const string COL_PHASE_COVERAGE_DETAILS = "PhaseCoverageDetails";
        public const string COL_PHASE_COVERAGE_TC_REPOSITORY = "TestCasesRepository";
        public const string COL_PHASE_COVERAGE_TC_DISTRIBUTION = "TestCasesDistribution";
        public const string COL_PHASE_COVERAGE_TC_MATRIX = "AcrossLocalesandPlatforms";

        public const string COL_PHASE_TYPE_ID = "PhaseTypeID";
        public const string COL_PHASE_TYPE = "PhaseType";

        public const string COL_PRODUCT_SPRINT_ID = "ProductSprintID";
        public const string COL_PRODUCT_SPRINT = "Sprint";
        public const string COL_PRODUCT_SPRINT_DETAILS = "SprintDetails";

        public const string COL_PROJECT_DATA_ID = "ProjectDataID";
        public const string COL_PROJECT_LOCALE_ID = "ProjectLocaleID";
        public const string COL_PROJECT_PLATFORM_ID = "ProjectPlatformID";
        public const string COL_TEST_STUDIO_KEY_ID = "TestStudioKeyID";
        public const string COL_PROJECT_PHASE_TOTAL_TC_COUNT = "TestCasesCount";
        public const string COL_PROJECT_PHASE_TOTAL_REM_COUNT = "TestCasesRemaining";

        public const string COL_PROJECT_PHASE_TOTAL_COUNT = "TotalCount";
        public const string COL_PROJECT_PHASE_TOTAL_EXECUTED = "TotalExecuted";
        public const string COL_PROJECT_PHASE_TOTAL_NA = "TotalNA";
        public const string COL_PROJECT_PHASE_TOTAL_REMAINING = "TotalRemaining";

        public const string COL_PROJECT_BUILD_DETAIL_ID = "ProjectBuildDetailID";
        public const string COL_PROJECT_BUILD_CODE = "ProjectBuildCode";
        public const string COL_PROJECT_BUILD = "ProjectBuild";
        public const string COL_PROJECT_BUILD_IS_RELEASE = "IsReleaseBuild";
        public const string COL_PROJECT_BUILD_LOCALE_ID = "ProjectBuildLocaleID";

        public const string COL_PROJECT_WIND_UP_ID = "ProjectWindUpID";
        public const string COL_PROJECT_PST_MORTEM_ANALYSIS_COMMENTS = "PostMortemAnalysis";
        public const string COL_PROJECT_LEARNINGS = "Learnings";
        public const string COL_PROJECT_BEST_PRACTICES = "BestPractices";

        public const string COL_PROJECT_ROLE_ID = "ProjectRoleID";
        public const string COL_PROJECT_ROLE = "ProjectRole";
        public const string COL_PROJECT_ROLE_CODE = "ProjectRoleCode";

        public const string COL_USER_PROJECT_ROLE_ID = "UserProjectRoleID";
        public const string COL_USER_PRODUCT_ID = "UserProductID";
        public const string COL_USER_PRODUCT_IS_OWNER = "IsOwner";

        public const string COL_WSR_PARAMETER_ID = "WSRParameterID";
        public const string COL_WSR_PARAMETER = "WSRParameter";
        public const string COL_WSR_SECTION = "WSRSection";
        public const string COL_WSR_PARAMETER_SELECTED_ID = "WSRParamSelectedID";
        public const string COL_WSR_PARAMETER_IS_SELECTED = "IsSelected";
        public const string COL_WSR_REMARKS = "Remarks";
        public const string COL_WSR_EFFORTS_TRACK_ID = "EffortsTrackID";
        public const string COL_WSR_EFFORTS = "Efforts";
        public const string COL_WSR_QUANTITY = "Quantity";
        public const string COL_WSR_DATA_ID = "WSRDataID";
        public const string COL_WSR_DETAIL_ID = "WSRDetailID";
        public const string COL_WSR_ACTUAL_QUANTITY = "ActualQuantity";
        public const string COL_WSR_REVISED_QUANTITY = "RevisedQuantity";
        public const string COL_WSR_ACTUAL_EFFORTS = "ActualEfforts";
        public const string COL_WSR_REVISED_EFFORTS = "RevisedEfforts";
        public const string COL_WSR_RED_ISSUES = "RedIssues";
        public const string COL_WSR_YELLOW_ISSUES = "YellowIssues";
        public const string COL_WSR_GREEN_ACCOMPLISHMENTS = "GreenAccom";
        public const string COL_WSR_NEXT_WEEK_DELIVERABLES = "NextWeekDeliverables";
        public const string COL_WSR_FEATURES_TESTED = "FeaturesTested";
        public const string COL_WSR_NOTES = "Notes";
        public const string COL_WSR_RESOURCE_COUNT = "ResourceCount";
        public const string COL_WSR_RESOURCE_NAMES = "ResourceNames";
        public const string COL_WSR_MIN_RESOURCE_COUNT = "MinResourceCount";
        public const string COL_WSR_MAX_RESOURCE_COUNT = "MaxResourceCount";
        public const string COL_WSR_AVG_RESOURCE_COUNT = "AvgResourceCount";

        public const string COL_REPORTING_DATE = "ReportingDate";
        public const string COL_REPORTING_MIN_DATE = "MinDate";
        public const string COL_REPORTING_MAX_DATE = "MaxDate";
        public const string COL_WEEK_ID = "WeekID";
        public const string COL_WEEK = "Week";
        public const string COL_MONTH = "Month";
        public const string COL_WEEK_START_DATE = "WeekStartDate";
        public const string COL_WEEK_END_DATE = "WeekEndDate";

        public const string COL_TEST_CASES = "TestCases";
        public const string COL_TEST_CASES_ID = "TestCasesID";
        public const string COL_TEST_CASES_PRODUCT_AREA = "ProductArea";
        public const string COL_TEST_CASES_SUB_AREA = "SubArea";
        public const string COL_TEST_CASES_PRIORITY = "Priority";
        public const string COL_TEST_CASES_VERSION = "Version";
        public const string COL_TEST_SUITE_NO = "TestSuiteNo";
        public const string COL_TEST_SUITE_RUN_IDS = "SIDs";
        public const string COL_TEST_SUITE_ID = "SuiteID";
        public const string COL_TESTCASES_COUNT = "TestCasesCount";
        public const string COL_SUITE_COUNTER = "SuiteCounter";
        public const string COL_SUITE_LAST_SET_VALUE = "SuiteLastSetValue";
        public const string COL_TESTCASES_EXECUTED = "TestCasesExecuted";
        public const string COL_TESTCASES_NA = "TestCasesNA";
        public const string COL_TESTCASES_REMAINING = "TestCasesRemaining";
        public const string COL_TESTCASES_REPOSITORY = "TestCasesRepository";
        public const string COL_TESTCASES_DISTRIBUTION = "TestCasesDistribution";
        public const string COL_TESTCASES_ACROSSBLOCKS = "AcrossLocalesAndPlatforms";
        public const string COL_TESTCASES_MATRIX_DATA_COUNT = "MatrixDataCount";

        public const string COL_WSR_PREV_WEEK_DELIVERABLES = "PrevWeekDeliverables";
        public const string COL_WSR_ORIG_SCHEDULE_DATE = "OriginalScheduleDate";
        public const string COL_WSR_REASON = "Reason";

        public const string COL_TABLE_NAME = "TableName";
        public const string COL_IS_CODE = "IsCode";
        public const string COL_IS_TYPE = "IsType";
        public const string COL_TYPE_TABLE_NAME = "TypeTableName";
        public const string COL_IS_TYPE_CODE = "IsTypeCode";
        public const string COL_IS_SUB_SCREEN = "IsSubScreen";
        public const string COL_MASTER_DATA_FILTER = "Filter";

        public const string COL_TEMP_GRID_HEADER = "GridHeader";
        public const string COL_TEMP_GRID_HEADER_DISPLAY = "GridHeaderDisplay";
        public const string COL_TEMP_TOTAL = "Total";
        public const string COL_TEMP_TOTAL_LOCALES = "Total Locales";
        public const string COL_TEMP_TOTAL_TEAM_COUNT = "Team Members Count";
        public const string COL_TEMP_PRODUCTIVITY = "Productivity (TCs Executed Quantity / Hours)";
        public const string COL_TEMP_PRODUCTIVITY_OVERALL = "Overall Productivity ((Total Quantity - Misc Quantity) / Total Hours)";
        public const string COL_TEMP_SECTION_VALUE_MISCELLANEOUS = "MISCELLANEOUS";
        public const string COL_TEMP_DATE = "DATE";
        public const string COL_TEMP_ROW = "Row";
        public const string COL_TEMP_EFFORTS_TRACK_ID_DAY = "EffortsTrackIDDay";
        public const string COL_TEMP_EFFORTS_DAY = "EffortsDay";

        public const string COL_TEMP_GRID_PROJECT_LOCALE_ID = "ProjectLocaleID";
        public const string COL_TEMP_GRID_LOCALE = "Locale";
        public const string COL_TEMP_GRID_SELECTED = "Select";
        public const string COL_TEMP_GRID_TEST_SUITE = "GridTestSuite";
        public const string COL_TEMP_GRID_TESTCASES_COUNT = "GridTestCasesCount";
        public const string COL_TEMP_GRID_SID = "GridSID";
        public const string COL_TEMP_GRID_SID_INFORMATION = "GridInformation";

        #endregion

        #region SESSIONS

        public const string SESSION_USER_ID = "UserID";
        public const string SESSION_LOGIN_ID = "LoginID";
        public const string SESSION_PASSWORD = "Password";
        public const string SESSION_LOCALE_ID = "LocaleID";
        public const string SESSION_PROJECT_ROLE_ID = "ProjectRoleID";
        public const string SESSION_PRODUCT_ID = "ProductID";
        public const string SESSION_PRODUCT_VERSION_ID = "ProductVersionID";
        public const string SESSION_PROJECT_PHASE_ID = "ProjectPhaseID";
        public const string SESSION_PHASE_TYPE_ID = "PhaseTypeID";
        public const string SESSION_PRODUCT_SPRINT_ID = "ProductSprintID";

        public const string SESSION_VIEW_PRODUCT_VERSION = "ViewProductVersions";

        public const string SESSION_MATRIX_PROJECT_PHASE_ID = "MatrixprojectPhaseID";
        public const string SESSION_MATRIX_PHASE_COVERAGE_DETAIL_ID = "MatrixPhaseCoverageDetailID";
        public const string SESSION_MATRIX_PLATFORM_TYPE_ID = "MatrixPlatformTypeID";
        public const string SESSION_MATRIX_BUILD_DETAIL_ID = "MatrixBuildDetailID";
        public const string SESSION_MATRIX_PRODUCT_LOCALE_ID = "MatrixProductLocaleID";

        public const string SESSION_VENDOR_ID = "VendorID";
        public const string SESSION_VENDOR_IS_CONTRACTOR = "ISCONTRACTOR";
        public const string SESSION_IS_ADMIN = "ISADMIN";
        public const string SESSION_RPM_TRUE = "IsRPMTrue";
        public const string SESSION_MATRIX_SELECTED_VENDOR_ID = "MatrixSelectedVendorID";
        public const string SESSION_MATRIX_SELECTED_PRODUCT_VERSION_ID = "MatrixSelectedProductVersionID";
        public const string SESSION_MATRIX_SELECTED_PROJECT_PHASE_ID = "MatrixSelectedProjectPhaseID";

        public const string SESSION_IDENTIFIER = "ControlIdentifier";
        public const string SESSION_IDENTIFIER_TEMP = "ControlIdentifierTemp";
        public const string SESSION_MATRIX_USER_ACCESS = "MatrixUserAccess";
        public const string SESSION_DATA_TREEVIEW_NODE_COLLECTION = "DataTreeViewNodeCollection";
        public const string SESSION_CACHE_SCREEN_ACCESS_DATA_TABLE = "DataTableScreenAccess";

        #endregion

        #region PAGES

        public const string PAGE_HOME = "Home.aspx";
        public const string PAGE_INDEX = "Index.aspx";
        public const string PAGE_LOCALIZATION_INSIGHT = "LocalizationInsight.aspx";
        public const string PAGE_REPORT = "Report.aspx";
        public const string PAGE_CONTACTUS = "ContactUs.aspx";
        public const string PAGE_ABOUT_US = "AboutUs.aspx";
        public const string PAGE_TEAM = "Team.aspx";
        public const string PAGE_LOGOUT = "Logout.aspx";
        public const string PAGE_LOGON = "Logon.aspx";
        public const string PAGE_UPDATE_MATRIX = "UpdateLocalesvsPlatformMatrixForm.aspx";

        #endregion

        #region SCREEN IDENTIFIERS

        public const string SCREEN_SITE = "Site";
        public const string SCREEN_LOGOUT = "Logout";
        public const string SCREEN_LOGON = "Logon";
        public const string SCREEN_ABOUTUS = "About Us";
        public const string SCREEN_CONTACTUS = "Contact Us";
        public const string SCREEN_COMMON = "Common";
        public const string SCREEN_HOME = "Home";
        public const string SCREEN_MAINTAIN_PRODUCTS = "Maintain Products";

        #endregion

        #region SCREEN HEADERS

        public const string SCREEN_SNO = "SNO";
        public const string SCREEN_HEADER_CODE = "Code";
        public const string SCREEN_HEADER_TYPE = "Type";
        public const string SCREEN_CODE_MANDATORY = "Code Mandatory";
        public const string SCREEN_DESCRIPTION_MANDATORY = "Description Mandatory";
        public const string SCREEN_DOCUMENT_NAME_MANDATORY = "Document Name Mandatory";
        public const string SCREEN_DOCUMENT_LINK_MANDATORY = "Document Link Mandatory";
        public const string SCREEN_HEADER_MASTER_DATA = "Header";
        public const string SCREEN_HEADER_DEFINITION = "Definition";
        public const string SCREEN_GRID_HEADER_MASTER_DATA = "Grid Header";

        public const string SCREEN_TEAM_CODE_MANDATORY = "Team Code Mandatory";
        public const string SCREEN_TEAM_MANDATORY = "Team Mandatory";

        public const string SCREEN_HOLIDAY_REASON_MANDATORY = "Holiday Reason Mandatory";
        public const string SCREEN_HOLIDAY_DATE_CHECK = "Date Check";

        public const string SCREEN_LOGINID_MANDATORY = "Login ID Mandatory";
        public const string SCREEN_FIRSTNAME_MANDATORY = "First Name Mandatory";
        public const string SCREEN_INCIDENT_DETAILS_MANDATORY = "Incident Details Mandatory";

        public const string SCREEN_SCREEN_IDENTIFIER_MANDATORY = "Screen Identifier Mandatory";

        public const string SCREEN_PRODUCT_CODE_MANDATORY = "Product Code Mandatory";
        public const string SCREEN_PRODUCT_VERSION_MANDATORY = "Product Version Mandatory";

        public const string SCREEN_PROJECT_PHASE_MANDATORY = "Phase Mandatory";
        public const string SCREEN_PROJECT_PHASE_COVERAGE_MANDATORY = "Phase Coverage Mandatory";
        public const string SCREEN_PRODUCT_SPRINT_MANDATORY = "Product Sprint Mandatory";
        public const string SCREEN_START_DATE = "StartDate";
        public const string SCREEN_END_DATE = "EndDate";
        public const string SCREEN_DATE_CHECK = "Date Check";
        public const string SCREEN_PRODUCT_BUILD_CODE_MANDATORY = "Build Code Mandatory";

        public const string SCREEN_MATRIX_SUITE_ID_MANDATORY = "Suite ID Mandatory";

        public const string SCREEN_BUILD_NO_MANDATORY = "Build No. Mandatory";

        public const string SCREEN_DEFINITION = "Definition";

        #endregion

        #region APPSETTINGS

        public const string SETTING_LOCALE_ID = "DefaultLocaleID";

        public const string STR_DOMAIN = "ADOBENET";
        public const string SETTING_ENVIRONMENT = "Environment";
        public const string SETTING_LOGGER = "Logger";
        public const string SETTING_LOG_LOCATION = "LogLocation";
        public const string SETTING_ENVIRONMENT_DEV = "DEV";
        public const string SETTING_ENVIRONMENT_PROD = "PROD";
        public const string SETTING_CHANGE_LOCALE = "ChangeLocale";
        public const string SETTING_EMAIL_ID = "DefaultEmailID";
        public const string SETTING_EMAIL_NAME = "DefaultEmailName";
        public const string SETTING_SMTP_SERVER = "SMTPServer";
        public const string SETTING_SMTP_PORT = "SMTPPort";
        public const string STR_ADOBE_COM = "@adobe.com";
        public const string DEF_EMAIL_SUBJECT_FIRST_TIME_USER = "First Time User Subject Email";
        public const string DEF_EMAIL_BODY_FIRST_TIME_USER = "First Time User Body Email";

        public const string DEF_CONTROL_LINKBUTTON_CLEAR = "CLEAR";

        #endregion

        #region DEFAULTS

        public const string STR_IS_PRODUCT_VERSION_ACTIVE = "IsProductVersionActive";

        public const string STR_ATTRIBUTE_RUNAT = "runat";
        public const string STR_ATTRIBUTE_STYLE = "style";
        public const string STR_ATTRIBUTE_STYLE_VALUE_ALIGN_CENTER = "Text-Align: Center";
        public const string STR_SERVER = "server";
        public const string STR_PROJECT_ROLE_REPORT_MANAGER_CODE = "RPM";

        public const string DEF_VAL_BUILDS_ALL = "All Builds";
        public const string DEF_VAL_LANGUAGE_GROUPS_ALL = "All Locale Groups";
        public const string DEF_VAL_LOCALES_ALL = "All Locales";
        public const string DEF_VAL_TEAMS_ALL = "All Teams";
        public const string DEF_VAL_USERS_ALL = "All Users";
        public const string DEF_VAL_PRODUCTS_ALL = "All Products";
        public const string DEF_VAL_PRODUCT_VERSIONS_ALL = "All Product Versions";
        public const string DEF_VAL_PROJECT_PHASES_ALL = "All Phases";
        public const string DEF_VAL_PHASE_COVERAGES_ALL = "All Coverages";
        public const string DEF_VAL_PHASE_TYPE_ALL = "All Phase Types";
        public const string DEF_VAL_PRODUCT_SPRINT_ALL = "All Product Sprints";

        public const string DEF_VAL_REPORTING_TYPE_YEARLY = "Yearly";
        public const string DEF_VAL_REPORTING_TYPE_WEEKLY = "Weekly";
        public const string DEF_VAL_REPORTING_TYPE_MONTHLY = "Monthly";
        public const string DEF_VAL_REPORTING_TYPE_TOTAL = "Total";
        public const string DEF_VAL_DATE_MONTH_FORMAT = "MMMM";

        public const string STR_SYSTEM_STRING = "System.String";
        public const string STR_SYSTEM_INT32 = "System.Int32";

        public const string STR_OTMATRIX_ACCESS_DEFINE = "Define Matrix";
        public const string STR_OTMATRIX_ACCESS_UPDATE = "Update Status";
        public const string STR_OTMATRIX_ACCESS_DISPLAY_ONLY = "Display Only";
        public const string STR_COL_SPAN = "ColSpan";
        public const string STR_NOT_DEFINED = "Not Defined";
        public const string STR_CONST_SPRINTS = "Sprints";

        public const string DEF_VAL_LOCALE_ID = "1";
        public const string DEF_VAL_ACCESS_CHECKED = "1";
        public const string DEF_VAL_LOGIN_ID = "ADMIN";
        public const string VAL_ROLE_ADMIN = "ADMINISTRATOR";
        public const string DEF_VALUE = "DEFAULT";
        public const string DEF_VAL_WIN_PLATFORM_TYPE_ID = "1";
        public const string DEF_VAL_MAC_PLATFORM_TYPE_ID = "2";

        public const string TS_SESSION_ID = "sessionid";

        public const string CONFIRM_MESSAGE = "return confirm('{0}')";

        public const int VAL_FOOTER_MAXIMUM_PIXEL = 330;

        #endregion

        #region COMMON LABELS

        public const string COM_FAILURE_EMAIL_MESSAGE = "Email Failure";
        public const string COM_SUCCESS_EMAIL_MESSAGE = "Email Success";
        public const string COM_FAILURE_SAVE_MESSAGE = "Save Failure";
        public const string COM_SUCCESS_SAVE_MESSAGE = "Save Success";
        public const string COM_FAILURE_DELETE_MESSAGE = "Delete Failure";
        public const string COM_SUCCESS_DELETE_MESSAGE = "Delete Success";

        #endregion

        #region USER CONTROLS

        public const string USER_CONTROL_PRODUCT_VERSION_SCREEN_IDENTIFIER = "Maintain Product Versions";
        public const string USER_CONTROL_PROJECT_PHASE_SCREEN_IDENTIFIER = "Maintain Project Phases";
        public const string USER_CONTROL_REPORTING_SCREEN_IDENTIFIER = "Efforts Tracker and Status Reporting";
        public const string USER_CONTROL_DAILY_SCREEN_IDENTIFIER = "Daily Efforts Tracker";
        public const string USER_CONTROL_DAILY_CONSOLIDATION_SCREEN_IDENTIFIER = "View Consolidated Efforts";
        public const string USER_CONTROL_WEEKLY_SCREEN_IDENTIFIER = "Submit Weekly Status";
        public const string USER_CONTROL_WEEKLY_CONSOLIDATION_SCREEN_IDENTIFIER = "Consolidated Status Reports";
        public const string USER_CONTROL_UPLOAD_REPORT_SCREEN_IDENTIFIER = "Upload Weekly Status Report";

        public const string USER_CONTROL_ROOT_PATH = "~/UserControls/";
        public const string USER_CONTROL_SHOW_PRODUCTS = "ShowProductsUserControl.ascx";
        public const string USER_CONTROL_PRODUCT_SUMMARY = "ShowProductSummaryUserControl.ascx";
        public const string USER_CONTROL_MESSAGE = "MessageUserControl.ascx";
        public const string USER_CONTROL_MAINTAIN_PRODUCT_VERSION = "MaintainProductVersionsUserControl.ascx";
        public const string USER_CONTROL_VIEW_PRODUCT_VERSION = "ViewProductVersionDetailsUserControl.ascx";
        public const string USER_CONTROL_MAINTAIN_PROJECT_PHASE = "MaintainProjectPhasesUserControl.ascx";
        public const string USER_CONTROL_LOCALES_PLATFORM_MATRIX_DISPLAY = "LocalesVsPlatformsCombinedMatrixDataUserControl.ascx";
        public const string USER_CONTROL_MASTER_DATA = "MasterDataUserControl.ascx";
        public const string USER_CONTROL_SUBMIT_DAILY_EFFORT_DATA = "MaintainVendorsDailyEffortsUserControl.ascx";
        public const string USER_CONTROL_VIEW_DAILY_CONSOLIDATION = "SubmitWSRDataUserControl.ascx";
        public const string USER_CONTROL_SUBMIT_WEEKLY_DATA = "ConsolidatedVendorsEffortsUserControl.ascx";
        public const string USER_CONTROL_VIEW_WEEKLY_CONSOLIDATION = "ConsolidatedStatusReportUserControl.ascx";
        public const string PAGE_UPLOAD_REPORT = "UploadReport.aspx";

        #endregion

        #region WEB CONTROLS

        public const string CONTROL_LABEL_ID = "LabelID";
        public const string CONTROL_LABEL_CODE = "LabelCode";
        public const string CONTROL_LABEL_TYPE = "LabelType";
        public const string CONTROL_LABEL_DESCRIPTION = "LabelDescription";
        public const string CONTROL_LABEL_CONTRACTOR = "LabelContractor";
        public const string CONTROL_LABEL_LOCATION = "LabelLocation";
        public const string CONTROL_LABEL_HOLIDAY_ID = "LabelHolidayID";
        public const string CONTROL_LABEL_VENDOR = "LabelVendor";
        public const string CONTROL_LABEL_HOLIDAY_REASON = "LabelHolidayReason";
        public const string CONTROL_LABEL_START_DATE = "LabelStartDate";
        public const string CONTROL_LABEL_END_DATE = "LabelEndDate";
        public const string CONTROL_LABEL_LOGIN_ID = "LabelLoginID";
        public const string CONTROL_LABEL_USER_ID = "LabelUserID";
        public const string CONTROL_LABEL_FIRSTNAME = "LabelFirstName";
        public const string CONTROL_LABEL_LASTNAME = "LabelLastName";
        public const string CONTROL_LABEL_PROJECT_ROLE_ID = "LabelProjectRoleID";
        public const string CONTROL_LABEL_PROJECT_ROLE = "LabelProjectRole";
        public const string CONTROL_LABEL_PRODUCT_ID = "LabelProductID";
        public const string CONTROL_LABEL_PRODUCT = "LabelProduct";
        public const string CONTROL_LABEL_PRODUCT_VERSION_ID = "LabelProductVersionID";
        public const string CONTROL_LABEL_PRODUCT_VERSION = "LabelProductVersion";
        public const string CONTROL_LABEL_USER_PROJECT_ROLE_ID = "LabelUserProjectRoleID";
        public const string CONTROL_LABEL_USER_PRODUCT_ID = "LabelUserProductID";
        public const string CONTROL_LABEL_USER_PRODUCT_PREFERENCE_ID = "LabelUserProductPreferenceID";
        public const string CONTROL_LABEL_SCREENID = "LabelScreenID";
        public const string CONTROL_LABEL_SCREEN_ACCESS_EXISTS = "LabelScreenAccessExists";
        public const string CONTROL_LABEL_PARENT_SCREENID = "LabelParentScreenID";
        public const string CONTROL_LABEL_SCREEN_IDENTIFIER = "LabelScreenIdentifier";
        public const string CONTROL_LABEL_SEQUENCE = "LabelSequence";
        public const string CONTROL_LABEL_PAGE_NAME = "LabelPageName";
        public const string CONTROL_LABEL_SCREEN_LOCALIZED_LABELID = "LabelScreenLocalizedLabelID";
        public const string CONTROL_LABEL_SCREEN_LABELID = "LabelScreenLabelID";
        public const string CONTROL_LABEL_WSR_PARAMETER_ID = "LabelWSRParameterID";
        public const string CONTROL_LABEL_WSR_SECTION = "LabelWSRSection";
        public const string CONTROL_LABEL_REVISED_QUANTITY = "LabelRevisedQuantity";
        public const string CONTROL_LABEL_REVISED_EFFORTS = "LabelRevisedEfforts";
        public const string CONTROL_LABEL_PRODUCT_WSR_PARAMETER_ID = "LabelProductWSRParameterID";
        public const string CONTROL_LABEL_EFFORTS_DAY = "LabelEffortsDay";
        public const string CONTROL_LABEL_EFFORTS_TRACK_ID_DAY = "LabelEffortsTrackIDDay";
        public const string CONTROL_LABEL_REMARKS = "LabelRemarks";
        public const string CONTROL_LABEL_WSR_DETAIL_ID = "LabelWSRDetailID";
        public const string CONTROL_LABEL_ORIG_SCHEDULE_DATE = "LabelOriginalScheduleDate";
        public const string CONTROL_LABEL_PROJECT_GM_DETAIL_ID = "LabelProjectGMDetailID";
        public const string CONTROL_LABEL_BUILD_NO = "LabelBuildNo";
        public const string CONTROL_LABEL_BUILD_PATH = "LabelBuildPath";
        public const string CONTROL_LABEL_GEO = "LabelGeo";
        public const string CONTROL_LABEL_LOCALE_ID = "LabelLocaleID";
        public const string CONTROL_LABEL_PLATFORM_ID = "LabelPlatformID";
        public const string CONTROL_LABEL_PRODUCT_LOCALE_ID = "LabelProductLocaleID";
        public const string CONTROL_LABEL_PRODUCT_PLATFORM_ID = "LabelProductPlatformID";
        public const string CONTROL_LABEL_ACTIVE = "LabelActive";
        public const string CONTROL_LABEL_CLOSED = "LabelClosed";
        public const string CONTROL_LABEL_PHASE_START_DATE = "LabelPhaseStartDate";
        public const string CONTROL_LABEL_PHASE_END_DATE = "LabelPhaseEndDate";
        public const string CONTROL_LABEL_PHASE_TYPE_ID = "LabelPhaseTypeID";
        public const string CONTROL_LABEL_PHASE_TYPE = "LabelPhaseType";
        public const string CONTROL_LABEL_PRODUCT_SPRINT_ID = "LabelProductSprintID";
        public const string CONTROL_LABEL_PRODUCT_SPRINT = "LabelProductSprint";
        public const string CONTROL_LABEL_PRODUCT_SPRINT_DETAILS = "LabelProductSprintDetails";
        public const string CONTROL_LABEL_SPRINT_START_DATE = "LabelSprintStartDate";
        public const string CONTROL_LABEL_SPRINT_END_DATE = "LabelSprintEndDate";
        public const string CONTROL_LABEL_TESTING_TYPE_ID = "LabelTestingTypeID";
        public const string CONTROL_LABEL_RELEASE_TYPE_ID = "LabelReleaseTypeID";
        public const string CONTROL_LABEL_STATUS_ID = "LabelStatusID";
        public const string CONTROL_LABEL_ABOUT_PRODUCT = "LabelAboutProduct";
        public const string CONTROL_LABEL_PRODUCT_CODE = "LabelProductCodeName";
        public const string CONTROL_LABEL_PRODUCT_YEAR = "LabelProductYear";
        public const string CONTROL_LABEL_PROJECT_PHASE_ID = "LabelProjectPhaseID";
        public const string CONTROL_LABEL_PROJECT_PHASE = "LabelProjectPhase";
        public const string CONTROL_LABEL_PROJECT_PHASE_COVERAGE_DETAIL_ID = "LabelPhaseCoverageDetailID";
        public const string CONTROL_LABEL_PROJECT_LOCALE_ID = "LabelProjectLocaleID";
        public const string CONTROL_LABEL_PROJECT_PLATFORM_ID = "LabelProjectPlatformID";
        public const string CONTROL_LABEL_PROJECT_PHASE_COVERAGE_DETAILS = "LabelCoverageDetails";
        public const string CONTROL_LABEL_PROJECT_BUILD_LOCALE_ID = "LabelProjectBuildLocaleID";
        public const string CONTROL_LABEL_PROJECT_BUILD_DETAIL_ID = "LabelProjectBuildDetailID";
        public const string CONTROL_LABEL_PROJECT_BUILD_CODE = "LabelProjectBuildCode";
        public const string CONTROL_LABEL_PROJECT_BUILD_DETAILS = "LabelProjectBuildDetails";
        public const string CONTROL_LABEL_TEST_SUITE_ID = "LabelSuiteID";
        public const string CONTROL_LABEL_TEST_CASES_COUNT = "LabelTestCasesCount";
        public const string CONTROL_LABEL_TEST_CASES_PLANNED = "LabelTestCasesPlanned";
        public const string CONTROL_LABEL_ABOUT_PROJECT_PHASE = "LabelAboutProjectPhase";
        public const string CONTROL_LABEL_LOCALE_WEIGHT = "LabelLocaleWeight";
        public const string CONTROL_LABEL_DOCUMENT_LINK_ID = "LabelProductLinkID";
        public const string CONTROL_LABEL_DOCUMENT_NAME = "LabelDocumentName";
        public const string CONTROL_LABEL_DOCUMENT_LINK = "LabelDocumentLink";
        public const string CONTROL_LABEL_COVERAGES_NOT_DEFINED = "LabelNoCoveragesDefined";
        public const string CONTROL_LABEL_TEAM_FEEDBACK_ID = "LabelTeamFeedbackID";
        public const string CONTROL_LABEL_FEEDBACK_INCIDENT_DETAILS = "LabelIncidentDetails";
        public const string CONTROL_LABEL_FEEDBACK_SEVERITY = "LabelSeverity";
        public const string CONTROL_LABEL_FEEDBACK_LOGGED_BY = "LabelLoggedBy";

        public const string CONTROL_LABEL_GM_DATE = "LabelGMDate";

        public const string CONTROL_TEXTBOX_CODE = "TextBoxCode";
        public const string CONTROL_TEXTBOX_DESCRIPTION = "TextBoxDescription";
        public const string CONTROL_TEXTBOX_LOCATION = "TextBoxLocation";
        public const string CONTROL_TEXTBOX_HOLIDAY_REASON = "TextBoxHolidayReason";
        public const string CONTROL_TEXTBOX_LOGIN_ID = "TextBoxLoginID";
        public const string CONTROL_TEXTBOX_FIRSTNAME = "TextBoxFirstName";
        public const string CONTROL_TEXTBOX_LASTNAME = "TextBoxLastName";
        public const string CONTROL_TEXTBOX_PARENT_SCREENID = "TextBoxParentScreenID";
        public const string CONTROL_TEXTBOX_SCREEN_IDENTIFIER = "TextBoxScreenIdentifier";
        public const string CONTROL_TEXTBOX_SEQUENCE = "TextBoxSequence";
        public const string CONTROL_TEXTBOX_PAGE_NAME = "TextBoxPageName";
        public const string CONTROL_TEXTBOX_LOCALIZED_VALUE = "TextBoxLocalizedValue";
        public const string CONTROL_TEXTBOX_REMARKS = "TextBoxRemarks";
        public const string CONTROL_TEXTBOX_EFFORTS_DAY = "TextBoxEffortsDay";
        public const string CONTROL_TEXTBOX_REASON = "TextBoxReason";
        public const string CONTROL_TEXTBOX_PREVIOUS_WEEK_DELIVERABLES = "TextBoxPrevWeekDeliverables";
        public const string CONTROL_TEXTBOX_REVISED_QUANTITY = "TextBoxRevisedQuantity";
        public const string CONTROL_TEXTBOX_REVISED_EFFORTS = "TextBoxRevisedEfforts";
        public const string CONTROL_TEXTBOX_BUILD_NO = "TextBoxBuildNo";
        public const string CONTROL_TEXTBOX_BUILD_PATH = "TextBoxBuildPath";
        public const string CONTROL_TEXTBOX_PLATFORM_PRIORITY = "TextBoxPlatformPriority";
        public const string CONTROL_TEXTBOX_PRODUCT_CODE = "TextBoxProductCodeName";
        public const string CONTROL_TEXTBOX_PRODUCT_YEAR = "TextBoxProductYear";
        public const string CONTROL_TEXTBOX_PRODUCT_VERSION = "TextBoxProductVersion";
        public const string CONTROL_TEXTBOX_PROJECT_PHASE = "TextBoxProjectPhase";
        public const string CONTROL_TEXTBOX_PROJECT_PHASE_COVERAGE_DETAILS = "TextBoxCoverageDetails";
        public const string CONTROL_TEXTBOX_PRODUCT_SPRINT = "TextBoxProductSprint";
        public const string CONTROL_TEXTBOX_PRODUCT_SPRINT_DETAILS = "TextBoxProductSprintDetails";
        public const string CONTROL_TEXTBOX_PROJECT_BUILD_CODE = "TextBoxProjectBuildCode";
        public const string CONTROL_TEXTBOX_PROJECT_BUILD_DETAILS = "TextBoxProjectBuildDetails";
        public const string CONTROL_TEXTBOX_TEST_SUITE_ID = "TextBoxSuiteID";
        public const string CONTROL_TEXTBOX_TEST_CASES_COUNT = "TextBoxTestCasesCount";
        public const string CONTROL_TEXTBOX_TEST_CASES_PLANNED = "TextBoxTestCasesPlanned";
        public const string CONTROL_TEXTBOX_LOCALE_WEIGHT = "TextBoxLocaleWeight";
        public const string CONTROL_TEXTBOX_DOCUMENT_NAME = "TextBoxDocumentName";
        public const string CONTROL_TEXTBOX_DOCUMENT_LINK = "TextBoxDocumentLink";
        public const string CONTROL_TEXTBOX_FEEDBACK_INCIDENT_DETAILS = "TextBoxIncidentDetails";

        public const string CONTROL_DROPDOWNLIST_TYPE = "DropDownListType";
        public const string CONTROL_DROPDOWNLIST_SELECT_VENDOR = "DropDownListSelectVendor";
        public const string CONTROL_DROPDOWNLIST_PROJECT_ROLES = "DropDownListProjectRoles";
        public const string CONTROL_DROPDOWNLIST_PRODUCT = "DropDownListProduct";
        public const string CONTROL_DROPDOWNLIST_PRODUCT_VERSION = "DropDownListProductVersion";
        public const string CONTROL_DROPDOWNLIST_PROJECT_BUILD = "DropDownListProjectBuild";
        public const string CONTROL_DROPDOWNLIST_PHASE_TYPE = "DropDownListPhaseType";
        public const string CONTROL_DROPDOWNLIST_PRODUCT_SPRINT = "DropDownListProductSprint";
        public const string CONTROL_DROPDOWNLIST_TESTING_TYPE = "DropDownListTestingType";
        public const string CONTROL_DROPDOWNLIST_RELEASE_TYPE = "DropDownListReleaseType";
        public const string CONTROL_DROPDOWNLIST_STATUS = "DropDownListStatus";
        public const string CONTROL_DROPDOWNLIST_FEEDBACK_SEVERITY = "DropDownListSeverity";

        public const string CONTROL_CHECKBOX_CONTRACTOR = "CheckBoxContractor";
        public const string CONTROL_CHECKBOX_ROLES_SELECTED = "CheckBoxRolesSelected";
        public const string CONTROL_CHECKBOX_PRODUCTS_SELECTED = "CheckBoxProductSelected";
        public const string CONTROL_CHECKBOX_ACTIVE = "CheckBoxActive";
        public const string CONTROL_CHECKBOX_WSR_PARAMETERS_SELECTED = "CheckBoxSelectedWSRParameters";
        public const string CONTROL_CHECKBOX_LOCALES_SELECTED = "CheckBoxLocalesSelected";
        public const string CONTROL_CHECKBOX_PLATFORMS_SELECTED = "CheckBoxPlatformsSelected";
        public const string CONTROL_CHECKBOX_RELEASE_BUILD_SELECTED = "CheckBoxReleaseBuildSelected";

        public const string CONTROL_DATECALENDAR_START_DATE = "DateCalendarStartDate";
        public const string CONTROL_DATECALENDAR_END_DATE = "DateCalendarEndDate";
        public const string CONTROL_DATECALENDAR_ORIG_SCHEDULE_DATE = "DateCalendarOrigScheduleDate";
        public const string CONTROL_DATECALENDAR_GM_DATE = "DateCalendarGMDate";
        public const string CONTROL_DATECALENDAR_PHASE_START_DATE = "DateCalendarPhaseStartDate";
        public const string CONTROL_DATECALENDAR_PHASE_END_DATE = "DateCalendarPhaseEndDate";
        public const string CONTROL_DATECALENDAR_SPRINT_START_DATE = "DateCalendarSprintStartDate";
        public const string CONTROL_DATECALENDAR_SPRINT_END_DATE = "DateCalendarSprintEndDate";

        public const string CONTROL_BUTTON_DELETE = "ButtonDelete";

        public const string CONTROL_RADIOBUTTON_READ = "RadioButtonRead";
        public const string CONTROL_RADIOBUTTON_READ_WRITE = "RadioButtonReadWrite";
        public const string CONTROL_RADIOBUTTON_REPORT = "RadioButtonReport";
        public const string CONTROL_RADIOBUTTON_CLEAR = "RadiobuttonClear";

        public const string CONTROL_LINKBUTTON_SAVE = "LinkButtonSave";
        public const string CONTROL_LINKBUTTON_CANCEL = "LinkButtonCancel";
        public const string CONTROL_LINKBUTTON_UPDATE = "LinkButtonUpdate";
        public const string CONTROL_LINKBUTTON_DELETE = "LinkButtonDelete";
        public const string CONTROL_LINKBUTTON_VIEW_USER_ROLES = "LinkButtonViewUserRoles";
        public const string CONTROL_LINKBUTTON_VIEW_VERSION_DETAILS = "LinkButtonViewVersionDetails";
        public const string CONTROL_LINKBUTTON_VIEW_PHASE_DETAILS = "LinkButtonViewPhaseDetails";

        public const string CONTROL_DATECALENDAR_BUTTON = "button";
        public const string CONTROL_DATECALENDAR_DIVCALENDAR = "DivCalendar";
        public const string CONTROL_DATECALENDAR_ONCLICK = "onclick";

        public const string CONTROL_GRIDVIEW_TEST_STRATEGY = "GridViewTestStrategy";

        #endregion

        #region LABEL CATEGORIES

        public const string MENU_TITLE = "1";
        public const string HEADER = "2";
        public const string SCREEN_TITLE = "3";
        public const string GRID = "4";
        public const string LABEL = "5";
        public const string CHECKBOX = "6";
        public const string DROPDOWNLIST = "7";
        public const string RADIOBUTTON = "8";
        public const string GRID_HEADER = "9";
        public const string MESSAGE = "10";
        public const string TAB = "11";
        public const string FIELD = "12";
        public const string BUTTON = "13";

        #endregion

        #region Site Master Links

        //public const string SAVED_SUCCESS = "Record saved successfully.";
        //public const string DELETED_SUCCESS = "Record deleted successfully.";
        //public const string FAILURE = "Operation Failed. Please try again later!!!";

        //public const string PROJECT = "Project";
        //public const string VIEWDATA = "ViewData";
        //public const string LANGUAGE = "Language";
        //public const string HOME = "MainPage";
        //public const string CLASS = "Classification";
        //public const string SUB_CLASS = "SubClassification";
        //public const string ASS_CLASS = "AssociateClassification";
        //public const string AWT_CIRC = "AWTCircluar";
        //public const string AAW_CIRC = "AAWCircluar";
        //public const string UPLOAD = "Upload";

        //public const string WSR = "WeeklyStatusReport";
        //public const string METRICS = "Metrics";
        //public const string STATISTICS = "Statistics";
        //public const string TEST_CASES = "TestCases";
        //public const string BUGS = "Bugs";
        //public const string Efforts = "Efforts";
        //public const string DEFINE_USER_ACCESS = "DefineUserAccess";
        //public const string ADD_UPDATE_USERS = "AddUpdateUsers";
        //public const string USER_PROFILE_PREFRENCES = "UserProfileandPreferences";
        //public const string DEFINE_VENDOR_ASSOCIATIONS = "DefineVendorAssociations";
        //public const string DEFINE_PRODUCT_OWNER = "DefineProductOwner";
        //public const string MAINTAIN_PRODUCT_VERSION = "MaintainProductVersions";
        //public const string MAINTAIN_PROJECT_PHASES = "MaintainProjectPhases";
        //public const string MAINTAIN_PRODUCT_LOCALES_PLATFORMS = "UpdateLocalesandPlatforms";
        //public const string MAINTAIN_PROJECT_LOCALES_VS_PLATFORMS_MATRIX = "UpdateLocalesVsPlatformsMatrix";
        //public const string MAINTAIN_PRODUCT_OWNERS_USERS = "UpdateOwnersandUsers";
        //public const string MAINTAIN_DEFINE_WSR_PARAMETERS = "DefineWSRParameters";
        //public const string MAINTAIN_DEFINE_PROJECT_CLOSURE = "ProjectWind-up/Closure";
        //public const string MAINTAIN_EFFORTS_ENTRY = "EffortsEntry";
        //public const string MAINTAIN_VIEW_TEAM_EFFORTS = "ViewEffortsbyTeam";
        //public const string SUBMIT_WEEKLY_STATUS_REPORT = "UpdateWeeklyStatus";
        //public const string IMPORT_STATUS_REPORT = "ImportStatusReport";
        //public const string VIEW_REPORTS = "ConsolidateStatusReports";
        //public const string MAINTAIN_HOLIDAYS_LIST = "MaintainHolidaysList";
        //public const string MAINTAIN_SCREEN_ACCESS = "DefineScreenAccess";
        //public const string MAINTAIN_SCREEN_LABELS = "DefineScreenLabels";

        //public const string MASTER_DEFINE_PRODUCTS = "MasterDefineProducts";
        //public const string PRODUCT_HEADER = "Product";
        //public const string PRODUCT_TABLENAME = "Products";

        //public const string MASTER_DEFINE_LOCBUILDGEOS = "MasterDefineLOCBuildsGeos";

        //public const string MASTER_DEFINE_LOCALES = "MasterDefineLocales";
        //public const string LOCALE_HEADER = "Locale";
        //public const string LOCALE_TABLENAME = "Locales";

        //public const string MASTER_DEFINE_PLATFORM_TYPES = "MasterDefinePlatformTypes";
        //public const string PLATFORM_TYPE_HEADER = "Platform Type";
        //public const string PLATFORM_TYPE_TABLENAME = "PlatformTypes";

        //public const string MASTER_DEFINE_PLATFORMS = "MasterDefinePlatforms";
        //public const string PLATFORM_HEADER = "Platform";
        //public const string PLATFORM_TABLENAME = "Platforms";

        //public const string MASTER_DEFINE_VENDOR_TYPES = "MasterDefineVendorTypes";
        //public const string VENDOR_TYPE_HEADER = "Vendor Type";
        //public const string VENDOR_TYPE_TABLENAME = "VendorTypes";

        //public const string MASTER_DEFINE_VENDORS = "DefineVendors";
        //public const string VENDOR_HEADER = "Vendor";
        //public const string VENDOR_TABLENAME = "Vendors";

        //public const string MASTER_DEFINE_PROJECT_ROLES = "MasterDefineProjectRoles";
        //public const string PROJECT_ROLE_HEADER = "Project Role";
        //public const string PROJECT_ROLE_TABLENAME = "ProjectRoles";

        //public const string MASTER_DEFINE_PHASES_TYPES = "MasterDefinePhasesTypes";
        //public const string PHASES_TYPE_HEADER = "Phase Type";
        //public const string PHASES_TYPES_TABLENAME = "PhaseTypes";

        //public const string MASTER_DEFINE_PHASES_STATUS = "MasterDefinePhaseStatus";
        //public const string PHASES_STATUS_HEADER = "Status";
        //public const string PHASES_STATUS_TABLENAME = "STatus";

        //public const string MASTER_DEFINE_WSRSECTIONS = "MasterDefineWSRSections";
        //public const string WSRSECTION_HEADER = "WSR Section";
        //public const string WSRSECTION_TABLENAME = "WSRSections";

        //public const string MASTER_DEFINE_WSRPARAMTERS = "MasterDefineWSRParameters";
        //public const string WSRPARAMTER_HEADER = "WSR Parameter";
        //public const string WSRPARAMTER_TABLENAME = "WSRParameters";

        //public const string MASTER_DEFINE_PRODUCT_FEATURES = "MasterDefineProductFeatures";
        //public const string PRODUCT_FEATURES_HEADER = "Feature";
        //public const string PRODUCT_FEATURES_TABLENAME = "ProductFeatures";

        //public const string MASTER_DEFINE_COVERAGE_DETAILS = "MasterDefineCoverageDetails";
        //public const string COVERAGE_DETAILS_HEADER = "";
        //public const string COVERAGE_DETAILS_TABLENAME = "PhaseCoverageDetails";

        #endregion
    }
}
