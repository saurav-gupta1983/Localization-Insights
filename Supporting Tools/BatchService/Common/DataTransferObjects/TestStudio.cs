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
        private string productTSKeyID;
        private string productVersionTSID;
        private string status;
        private string localeTSKeyID;
        private string platformTSKeyID;
        private int testCasesCount = 0;
        private int testCasesExecuted = 0;
        private int testCasesNA = 0;
        private bool priorityDistributionCriteria = true;
        private bool productAreaDistributionCriteria = true;
        private DataTable testSuiteDetails;
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
        /// productTSKeyID
        /// </summary>
        public string ProductTSKeyID
        {
            get
            {
                return productTSKeyID;
            }
            set
            {
                productTSKeyID = value;
            }
        }

        /// <summary>
        /// productVersionTSID
        /// </summary>
        public string ProductVersionTSID
        {
            get
            {
                return productVersionTSID;
            }
            set
            {
                productVersionTSID = value;
            }
        }

        /// <summary>
        /// localeTSKeyID
        /// </summary>
        public string LocaleTSKeyID
        {
            get
            {
                return localeTSKeyID;
            }
            set
            {
                localeTSKeyID = value;
            }
        }

        /// <summary>
        /// platformTSKeyID
        /// </summary>
        public string PlatformTSKeyID
        {
            get
            {
                return platformTSKeyID;
            }
            set
            {
                platformTSKeyID = value;
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
        /// TestSuiteDetails
        /// </summary>
        public DataTable TestSuiteDetails
        {
            get
            {
                return testSuiteDetails;
            }
            set
            {
                testSuiteDetails = value;
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
