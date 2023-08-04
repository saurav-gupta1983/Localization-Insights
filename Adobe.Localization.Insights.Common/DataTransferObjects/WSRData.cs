using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Adobe.Localization.Insights.Common.DataTransferObjects
{
    [Serializable]
    public class WSRData
    {
        #region Variables

        private Header dataHeader;
        private string wsrDataID;
        private string wsrSectionID;
        private string productID;
        private string productVersionID;
        private string productYear;
        private string wsrParameterID;
        private string userProductID;
        private string projectPhaseID;
        private string phaseTypeID;
        private string productSprintID;
        private string userVendorID;
        private string userID;
        private string vendorID;
        private string testingTypeID;
        private string reportingType;
        private string weekID;
        private string weekStartDate;
        private string redIssues;
        private string yellowIssues;
        private string greenAccom;
        private string featuresTested;
        private string notes;
        private string resourceCount;
        private string resourceNames;
        private string testCasesStatsID;
        private int testCasesExecuted;
        private string bugsID;
        private int totalBugs;
        private int bugsRegressed;
        private int bugsPending;
        private string effortsID;
        private int execHours;
        private int regressionHours;
        private int meetingHours;
        private int machineSetupHours;
        private int wsrHours;
        private string tcRemarks;
        private string bugRemarks;
        private string effortRemarks;
        private string newDeliverables;
        private DataSet prevWeekDeliverables;
        private bool reporting = false;
        private ArrayList wsrProductParameterCollection;
        private ArrayList vendorEffortsCollection;
        private bool isSelectedWSRParameters = true;

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
        /// ProjectID
        /// </summary>
        public string WsrDataID
        {
            get
            {
                if (wsrDataID == null || wsrDataID == "0")
                    return "";
                return wsrDataID;
            }
            set
            {
                wsrDataID = value;
            }
        }

        /// <summary>
        /// wsrSectionID
        /// </summary>
        public string WsrSectionID
        {
            get
            {
                if (wsrSectionID == null || wsrSectionID == "0")
                    return "";
                return wsrSectionID;
            }
            set
            {
                wsrSectionID = value;
            }
        }

        /// <summary>
        /// ProductID
        /// </summary>
        public string ProductID
        {
            get
            {
                if (productID == null || productID == "0")
                    return "";
                return productID;
            }
            set
            {
                productID = value;
            }
        }

        /// <summary>
        /// ProductVersionID
        /// </summary>
        public string ProductVersionID
        {
            get
            {
                if (productVersionID == null || productVersionID == "0")
                    return "";
                return productVersionID;
            }
            set
            {
                productVersionID = value;
            }
        }

        /// <summary>
        /// ProductYear
        /// </summary>
        public string ProductYear
        {
            get
            {
                return productYear;
            }
            set
            {
                productYear = value;
            }
        }

        /// <summary>
        /// projectPhaseID
        /// </summary>
        public string ProjectPhaseID
        {
            get
            {
                if (projectPhaseID == null || projectPhaseID == "0")
                    return "";
                return projectPhaseID;
            }
            set
            {
                projectPhaseID = value;
            }
        }

        /// <summary>
        /// PhaseTypeID
        /// </summary>
        public string PhaseTypeID
        {
            get
            {
                if (phaseTypeID == null || phaseTypeID == "0")
                    return "";
                return phaseTypeID;
            }
            set
            {
                phaseTypeID = value;
            }
        }

        /// <summary>
        /// productSprintID
        /// </summary>
        public string ProductSprintID
        {
            get
            {
                if (productSprintID == null || productSprintID == "0")
                    return "";
                return productSprintID;
            }
            set
            {
                productSprintID = value;
            }
        }

        /// <summary>
        /// userProductID
        /// </summary>
        public string UserProductID
        {
            get
            {
                if (userProductID == null || userProductID == "0")
                    return "";
                return userProductID;
            }
            set
            {
                userProductID = value;
            }
        }

        /// <summary>
        /// userVendorID
        /// </summary>
        public string UserVendorID
        {
            get
            {
                if (userVendorID == null || userVendorID == "0")
                    return "";
                return userVendorID;
            }
            set
            {
                userVendorID = value;
            }
        }

        /// <summary>
        /// wsrParameterID
        /// </summary>
        public string WsrParameterID
        {
            get
            {
                if (wsrParameterID == null || wsrParameterID == "0")
                    return "";
                return wsrParameterID;
            }
            set
            {
                wsrParameterID = value;
            }
        }

        /// <summary>
        /// userID
        /// </summary>
        public string UserID
        {
            get
            {
                if (userID == null || userID == "0")
                    return "";
                return userID;
            }
            set
            {
                userID = value;
            }
        }

        /// <summary>
        /// VendorID
        /// </summary>
        public string VendorID
        {
            get
            {
                if (vendorID == null || vendorID == "0")
                    return "";
                return vendorID;
            }
            set
            {
                vendorID = value;
            }
        }

        /// <summary>
        /// TestingTypeID
        /// </summary>
        public string TestingTypeID
        {
            get
            {
                if (testingTypeID == null || testingTypeID == "0")
                    return "";
                return testingTypeID;
            }
            set
            {
                testingTypeID = value;
            }
        }

        /// <summary>
        /// ReportingType
        /// </summary>
        public string ReportingType
        {
            get
            {
                if (reportingType == null || reportingType == "0")
                    return "";
                return reportingType;
            }
            set
            {
                reportingType = value;
            }
        }

        /// <summary>
        /// WeekID
        /// </summary>
        public string WeekID
        {
            get
            {
                if (weekID == null || weekID == "0")
                    return "";
                return weekID;
            }
            set
            {
                weekID = value;
            }
        }

        /// <summary>
        /// WeekStartDate
        /// </summary>
        public string WeekStartDate
        {
            get
            {
                if (weekStartDate == null || weekStartDate == "0")
                    return "";
                return weekStartDate;
            }
            set
            {
                weekStartDate = value;
            }
        }

        /// <summary>
        /// RedIssues
        /// </summary>
        public string RedIssues
        {
            get
            {
                return redIssues;
            }
            set
            {
                redIssues = value;
            }
        }

        /// <summary>
        /// YellowIssues
        /// </summary>
        public string YellowIssues
        {
            get
            {
                return yellowIssues;
            }
            set
            {
                yellowIssues = value;
            }
        }

        /// <summary>
        /// GreenAccom
        /// </summary>
        public string GreenAccom
        {
            get
            {
                return greenAccom;
            }
            set
            {
                greenAccom = value;
            }
        }

        /// <summary>
        /// FeaturesTested
        /// </summary>
        public string FeaturesTested
        {
            get
            {
                return featuresTested;
            }
            set
            {
                featuresTested = value;
            }
        }

        /// <summary>
        /// Notes
        /// </summary>
        public string Notes
        {
            get
            {
                return notes;
            }
            set
            {
                notes = value;
            }
        }

        /// <summary>
        /// ResourceCount
        /// </summary>
        public string ResourceCount
        {
            get
            {
                if (resourceCount == null || resourceCount == "")
                    return "0";
                return resourceCount;
            }
            set
            {
                resourceCount = value;
            }
        }

        /// <summary>
        /// ResourceNames
        /// </summary>
        public string ResourceNames
        {
            get
            {
                return resourceNames;
            }
            set
            {
                resourceNames = value;
            }
        }

        /// <summary>
        /// TestCasesStatsID
        /// </summary>
        public string TestCasesStatsID
        {
            get
            {
                if (testCasesStatsID == null || testCasesStatsID == "0")
                    return "";
                return testCasesStatsID;
            }
            set
            {
                testCasesStatsID = value;
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
        /// BugsID
        /// </summary>
        public string BugsID
        {
            get
            {
                if (bugsID == null || bugsID == "0")
                    return "";
                return bugsID;
            }
            set
            {
                bugsID = value;
            }
        }

        /// <summary>
        /// TotalBugs
        /// </summary>
        public int TotalBugs
        {
            get
            {
                return totalBugs;
            }
            set
            {
                totalBugs = value;
            }
        }

        /// <summary>
        /// BugsRegressed
        /// </summary>
        public int BugsRegressed
        {
            get
            {
                return bugsRegressed;
            }
            set
            {
                bugsRegressed = value;
            }
        }

        /// <summary>
        /// BugsPending
        /// </summary>
        public int BugsPending
        {
            get
            {
                return bugsPending;
            }
            set
            {
                bugsPending = value;
            }
        }

        /// <summary>
        /// EffortsID
        /// </summary>
        public string EffortsID
        {
            get
            {
                if (effortsID == null || effortsID == "0")
                    return "";
                return effortsID;
            }
            set
            {
                effortsID = value;
            }
        }

        /// <summary>
        /// ExecHours
        /// </summary>
        public int ExecHours
        {
            get
            {
                return execHours;
            }
            set
            {
                execHours = value;
            }
        }

        /// <summary>
        /// RegressionHours
        /// </summary>
        public int RegressionHours
        {
            get
            {
                return regressionHours;
            }
            set
            {
                regressionHours = value;
            }
        }

        /// <summary>
        /// MeetingHours
        /// </summary>
        public int MeetingHours
        {
            get
            {
                return meetingHours;
            }
            set
            {
                meetingHours = value;
            }
        }

        /// <summary>
        /// MachineSetupHours
        /// </summary>
        public int MachineSetupHours
        {
            get
            {
                return machineSetupHours;
            }
            set
            {
                machineSetupHours = value;
            }
        }

        /// <summary>
        /// WSRHours
        /// </summary>
        public int WSRHours
        {
            get
            {
                return wsrHours;
            }
            set
            {
                wsrHours = value;
            }
        }

        /// <summary>
        /// TcRemarks
        /// </summary>
        public string TcRemarks
        {
            get
            {
                return tcRemarks;
            }
            set
            {
                tcRemarks = value;
            }
        }

        /// <summary>
        /// BugRemarks
        /// </summary>
        public string BugRemarks
        {
            get
            {
                return bugRemarks;
            }
            set
            {
                bugRemarks = value;
            }
        }

        /// <summary>
        /// EffortRemarks
        /// </summary>
        public string EffortRemarks
        {
            get
            {
                return effortRemarks;
            }
            set
            {
                effortRemarks = value;
            }
        }

        /// <summary>
        /// NewDeliverables
        /// </summary>
        public string NewDeliverables
        {
            get
            {
                return newDeliverables;
            }
            set
            {
                newDeliverables = value;
            }
        }

        /// <summary>
        /// PrevWeekDeliverables
        /// </summary>
        public DataSet PrevWeekDeliverables
        {
            get
            {
                return prevWeekDeliverables;
            }
            set
            {
                prevWeekDeliverables = value;
            }
        }

        /// <summary>
        /// Reporting
        /// </summary>
        public bool Reporting
        {
            get
            {
                return reporting;
            }
            set
            {
                reporting = value;
            }
        }

        /// <summary>
        /// WsrProductParameterCollection
        /// </summary>
        public ArrayList WsrProductParameterCollection
        {
            get
            {
                if (wsrProductParameterCollection == null)
                    return new ArrayList();
                return wsrProductParameterCollection;
            }
            set
            {
                wsrProductParameterCollection = value;
            }
        }

        /// <summary>
        /// VendorEffortsCollection
        /// </summary>
        public ArrayList VendorEffortsCollection
        {
            get
            {
                if (vendorEffortsCollection == null)
                    return new ArrayList();
                return vendorEffortsCollection;
            }
            set
            {
                vendorEffortsCollection = value;
            }
        }

        /// <summary>
        /// isSelectedWSRParameters
        /// </summary>
        public bool IsSelectedWSRParameters
        {
            get
            {
                return isSelectedWSRParameters;
            }
            set
            {
                isSelectedWSRParameters = value;
            }
        }

        #endregion
    }
}
