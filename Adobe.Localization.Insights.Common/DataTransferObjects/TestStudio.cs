using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Adobe.Localization.Insights.Common.DataTransferObjects
{
    [Serializable]
    public class TestSudio
    {
        #region Variables

        private Header dataHeader;
        private string sessionID;
        private string testSuiteID;
        private string testSuiteRunID;
        private string testSuiteTitle;
        private string errorCode;
        private string errorMessage;
        private string product;
        private string productVersion;
        private string status;
        private string locale;
        private string platform;
        private string buildNo;
        private string milestone;
        private int testCasesCount = 0;
        private int testCasesExecuted = 0;
        private int testCasesNA = 0;
        private bool priorityDistributionCriteria = true;
        private bool productAreaDistributionCriteria = true;
        private DataSet dsConfigurations;
        private DataTable dtVersions;
        private DataTable dtTestSuiteDetails;
        private DataRow[] drTestCasesCol;

        #endregion

        #region Public Properties

        /// <summary>
        /// DataHeader
        /// </summary>
        public Header DataHeader
        {
            get
            {
                return dataHeader;
            }
            set
            {
                dataHeader = value;
            }
        }

        /// <summary>
        /// SessionID
        /// </summary>
        public string SessionID
        {
            get
            {
                if (sessionID == null || sessionID == "0")
                    return "";
                return sessionID;
            }
            set
            {
                sessionID = value;
            }
        }

        /// <summary>
        /// TestSuiteID
        /// </summary>
        public string TestSuiteID
        {
            get
            {
                return testSuiteID;
            }
            set
            {
                testSuiteID = value;
            }
        }

        /// <summary>
        /// TestSuiteRunID
        /// </summary>
        public string TestSuiteRunID
        {
            get
            {
                return testSuiteRunID;
            }
            set
            {
                testSuiteRunID = value;
            }
        }

        /// <summary>
        /// TestSuiteTitle
        /// </summary>
        public string TestSuiteTitle
        {
            get
            {
                return testSuiteTitle;
            }
            set
            {
                testSuiteTitle = value;
            }
        }

        /// <summary>
        /// ErrorCode
        /// </summary>
        public string ErrorCode
        {
            get
            {
                return errorCode;
            }
            set
            {
                errorCode = value;
            }
        }

        /// <summary>
        /// ErrorMessage
        /// </summary>
        public string ErrorMessage
        {
            get
            {
                return errorMessage;
            }
            set
            {
                errorMessage = value;
            }
        }

        /// <summary>
        /// product
        /// </summary>
        public string Product
        {
            get
            {
                return product;
            }
            set
            {
                product = value;
            }
        }

        /// <summary>
        /// productVersion
        /// </summary>
        public string ProductVersion
        {
            get
            {
                return productVersion;
            }
            set
            {
                productVersion = value;
            }
        }

        /// <summary>
        /// locale
        /// </summary>
        public string Locale
        {
            get
            {
                return locale;
            }
            set
            {
                locale = value;
            }
        }

        /// <summary>
        /// platform
        /// </summary>
        public string Platform
        {
            get
            {
                return platform;
            }
            set
            {
                platform = value;
            }
        }

        /// <summary>
        /// buildNo
        /// </summary>
        public string BuildNo
        {
            get
            {
                return buildNo;
            }
            set
            {
                buildNo = value;
            }
        }

        /// <summary>
        /// Milestone
        /// </summary>
        public string Milestone
        {
            get
            {
                return milestone;
            }
            set
            {
                milestone = value;
            }
        }

        /// <summary>
        /// status
        /// </summary>
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }

        /// <summary>
        /// TestCasesCount
        /// </summary>
        public int TestCasesCount
        {
            get
            {
                return testCasesCount;
            }
            set
            {
                testCasesCount = value;
            }
        }

        /// <summary>
        /// TestCasesExecuted
        /// </summary>
        public int TestCasesExecuted
        {
            get
            {
                return testCasesExecuted;
            }
            set
            {
                testCasesExecuted = value;
            }
        }

        /// <summary>
        /// TestCasesNA
        /// </summary>
        public int TestCasesNA
        {
            get
            {
                return testCasesNA;
            }
            set
            {
                testCasesNA = value;
            }
        }

        /// <summary>
        /// productAreaDistributionCriteria
        /// </summary>
        public bool ProductAreaDistributionCriteria
        {
            get
            {
                return productAreaDistributionCriteria;
            }
            set
            {
                productAreaDistributionCriteria = value;
            }
        }

        /// <summary>
        /// priorityDistributionCriteria
        /// </summary>
        public bool PriorityDistributionCriteria
        {
            get
            {
                return priorityDistributionCriteria;
            }
            set
            {
                priorityDistributionCriteria = value;
            }
        }

        /// <summary>
        /// Configurations
        /// </summary>
        public DataSet Configurations
        {
            get
            {
                return dsConfigurations;
            }
            set
            {
                dsConfigurations = value;
            }
        }

        /// <summary>
        /// VersionList
        /// </summary>
        public DataTable VersionList
        {
            get
            {
                return dtVersions;
            }
            set
            {
                dtVersions = value;
            }
        }

        /// <summary>
        /// TestSuiteDetails
        /// </summary>
        public DataTable TestSuiteDetails
        {
            get
            {
                return dtTestSuiteDetails;
            }
            set
            {
                dtTestSuiteDetails = value;
            }
        }

        /// <summary>
        /// TestCasesCollection
        /// </summary>
        public DataRow[] TestCasesCollection
        {
            get
            {
                return drTestCasesCol;
            }
            set
            {
                drTestCasesCol = value;
            }
        }

        #endregion
    }
}
