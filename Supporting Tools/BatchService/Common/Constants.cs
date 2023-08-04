using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adobe.OogwayBatch.Common
{
    /// <summary>
    /// Constants
    /// </summary>
    public static class Constants
    {
        #region

        public const string SERVICE_NAME = "RequestXmlPath";

        #endregion

        #region APPSETTINGS

        public const string LOCATION_REQUEST_XML = "RequestXmlPath";
        public const string LOCATION_GRAPH_XML = "GraphXmlPath";
        public const string WATSON_WEB_SERVER = "WatsonWebServer";
        public const string WATSON_APP_GUID = "WatsonAPPGUID";

        public const string SETTING_EMAIL_ID = "DefaultEmailID";
        public const string SETTING_EMAIL_NAME = "DefaultEmailName";
        public const string SETTING_SMTP_SERVER = "SMTPServer";
        public const string SETTING_SMTP_PORT = "SMTPPort";

        public const string SETTING_LOGGER = "Logger";
        public const string SETTING_LOG_LOCATION = "LogLocation";

        #endregion

        #region Queries

        public const string DATA_TYPE_ALL = "ALL";
        public const string DATA_TYPE_CORE = "CORE";
        public const string DATA_TYPE_LOC = "LOC";

        public const string FILTER_CRITERIA_FAMILY = "FAMILY_NAME";
        public const string FILTER_CRITERIA_PRODUCT = "PRODUCT_NAME";
        public const string FILTER_CRITERIA_VERSIONS = "VERSION_NAME";
        public const string FILTER_CRITERIA_KEYWORDS = "KEYWORDS";

        public const string FILTER_OPERATOR_EQUALS = "Equals";
        public const string FILTER_OPERATOR_IN = "In";

        #endregion

        #region Column Names

        public const string COL_PRODUCT_DETAIL_ID = "WatsonProductDetailID";
        public const string COL_PRODUCT = "PRODUCT";
        public const string COL_VERSION = "VERSION";
        public const string COL_LAST_EXECUTION_DATE = "LastExecutionDate";

        #endregion

        #region StoredProcedures

        public const string BATCH_STORED_PROCEDURE = "SP_BATCH_PROCESSWATSONDATA";

        #endregion

        #region Error Messages

        public const string STATUS_SUCCESS = "Success";
        public const string STATUS_FAILED = "Failes";

        public const string ERROR_PROCESSING_FAILED = "\tProcessing failed for Product:{0}, Version:{1}\n";
        public const string ERROR_DATABASE_DUMP_FAILED = "\tDatabase Dump Operation failed for Product:{0}, Version:{1} and Type:{2}\n";
        public const string ERROR_WATSON_DATA_PROCESSING_FAILED = "\tWatson Data processing failed for Product:{0}, Version:{1}\n";
        public const string ERROR_GRAPH_GENERATION_FAILED = "\tGraph XMLs Generation for Product failed\n";
 
        #endregion

    }
}
