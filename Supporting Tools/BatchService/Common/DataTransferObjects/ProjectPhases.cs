using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Generic;

namespace Adobe.Localization.Insights.Common.DataTransferObjects
{
    [Serializable]
    public class ProjectPhases
    {
        #region Variables

        private Header dataHeader;
        private string projectPhaseID;
        private string projectPhase;
        private string productVersionID;
        private string productID;
        private string vendorID;
        private string platformTypeID;
        private string projectBuildDetailID;
        private string productLocaleID;
        private string aboutProjectPhase;
        private string phaseTypeID;
        private string productSprintID;
        private string statusID;
        private string testingTypeID;
        private string startDate;
        private string endDate;
        private string testCasesPlanned;
        private string testCasesCount;
        private string suiteID;
        private string phaseCoverageDetailID;
        private string projectPhaseCoverage;
        private string tcRepositorySetting;
        private string tcDistributeSetting;
        private string acrossBlockSetting;
        private ArrayList projectLocaleIDCollection;
        private ArrayList productLocaleIDCollection;
        private ArrayList platformIDCollection;
        private ArrayList localeVendorIDCollection;
        private ArrayList localeWeightCollection;
        private bool isUpdatePhaseDetails = false;
        private bool isActive = false;
        private ArrayList projectLocaleVsPlatformMatrix;
        private string copyProjectPhaseID;
        private bool isCopyLocales = false;
        private bool isCopyPlatforms = false;
        private bool isCopyMatrix = false;

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
        /// ProjectPhaseID
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
        /// ProjectPhase
        /// </summary>
        public string ProjectPhase
        {
            get
            {
                if (projectPhase == null)
                    return "";
                return projectPhase;
            }
            set
            {
                projectPhase = value;
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
        /// AboutProjectPhase
        /// </summary>
        public string AboutProjectPhase
        {
            get
            {
                if (aboutProjectPhase == null)
                    return "";
                return aboutProjectPhase;
            }
            set
            {
                aboutProjectPhase = value;
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
        /// StatusID
        /// </summary>
        public string StatusID
        {
            get
            {
                if (statusID == null || statusID == "0")
                    return "";
                return statusID;
            }
            set
            {
                statusID = value;
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
        /// StartDate
        /// </summary>
        public string StartDate
        {
            get
            {
                if (startDate == null || startDate == "0")
                    return "";
                return startDate;
            }
            set
            {
                startDate = value;
            }
        }

        /// <summary>
        /// EndDate
        /// </summary>
        public string EndDate
        {
            get
            {
                if (endDate == null)
                    return "";
                return endDate;
            }
            set
            {
                endDate = value;
            }
        }

        /// <summary>
        /// TestCasesPlanned
        /// </summary>
        public string TestCasesPlanned
        {
            get
            {
                if (testCasesPlanned == null || testCasesPlanned == "")
                    return "0";
                return testCasesPlanned;
            }
            set
            {
                testCasesPlanned = value;
            }
        }

        /// <summary>
        /// ProjectPhaseCoverage
        /// </summary>
        public string ProjectPhaseCoverage
        {
            get
            {
                if (projectPhaseCoverage == null)
                    return "";
                return projectPhaseCoverage;
            }
            set
            {
                projectPhaseCoverage = value;
            }
        }

        /// <summary>
        /// suiteID
        /// </summary>
        public string SuiteID
        {
            get
            {
                if (suiteID == null)
                    return "";
                return suiteID;
            }
            set
            {
                suiteID = value;
            }
        }

        /// <summary>
        /// PhaseCoverageDetailID
        /// </summary>
        public string PhaseCoverageDetailID
        {
            get
            {
                if (phaseCoverageDetailID == null || phaseCoverageDetailID == "0")
                    return "";
                return phaseCoverageDetailID;
            }
            set
            {
                phaseCoverageDetailID = value;
            }
        }


        /// <summary>
        /// platformTypeID
        /// </summary>
        public string PlatformTypeID
        {
            get
            {
                if (platformTypeID == null || platformTypeID == "0")
                    return "";
                return platformTypeID;
            }
            set
            {
                platformTypeID = value;
            }
        }

        /// <summary>
        /// projectBuildDetailID
        /// </summary>
        public string ProjectBuildDetailID
        {
            get
            {
                if (projectBuildDetailID == null || projectBuildDetailID == "0")
                    return "";
                return projectBuildDetailID;
            }
            set
            {
                projectBuildDetailID = value;
            }
        }

        /// <summary>
        /// productLocaleID
        /// </summary>
        public string ProductLocaleID
        {
            get
            {
                if (productLocaleID == null || productLocaleID == "0")
                    return "";
                return productLocaleID;
            }
            set
            {
                productLocaleID = value;
            }
        }

        /// <summary>
        /// testCasesCount
        /// </summary>
        public string TestCasesCount
        {
            get
            {
                if (testCasesCount == null || testCasesCount == "")
                    return "0";
                return testCasesCount;
            }
            set
            {
                testCasesCount = value;
            }
        }

        /// <summary>
        /// TestCaseRepositorySetting
        /// </summary>
        public string TestCasesRepositorySetting
        {
            get
            {
                if (tcRepositorySetting == null || tcRepositorySetting == "")
                    return "0";
                return tcRepositorySetting;
            }
            set
            {
                tcRepositorySetting = value;
            }
        }

        /// <summary>
        /// TestCasesDistributeSetting
        /// </summary>
        public string TestCasesDistributeSetting
        {
            get
            {
                if (tcDistributeSetting == null || tcDistributeSetting == "")
                    return "0";
                return tcDistributeSetting;
            }
            set
            {
                tcDistributeSetting = value;
            }
        }

        /// <summary>
        /// AcrossBlockSetting
        /// </summary>
        public string AcrossBlockSetting
        {
            get
            {
                if (acrossBlockSetting == null || acrossBlockSetting == "")
                    return "0";
                return acrossBlockSetting;
            }
            set
            {
                acrossBlockSetting = value;
            }
        }

        /// <summary>
        /// ProjectLocaleIDCollection
        /// </summary>
        public ArrayList ProjectLocaleIDCollection
        {
            get
            {
                if (projectLocaleIDCollection == null)
                    return new ArrayList();
                return projectLocaleIDCollection;
            }
            set
            {
                projectLocaleIDCollection = value;
            }
        }

        /// <summary>
        /// ProductLocaleIDCollection
        /// </summary>
        public ArrayList ProductLocaleIDCollection
        {
            get
            {
                if (productLocaleIDCollection == null)
                    return new ArrayList();
                return productLocaleIDCollection;
            }
            set
            {
                productLocaleIDCollection = value;
            }
        }

        /// <summary>
        /// PlatformIDCollection
        /// </summary>
        public ArrayList PlatformIDCollection
        {
            get
            {
                if (platformIDCollection == null)
                    return new ArrayList();
                return platformIDCollection;
            }
            set
            {
                platformIDCollection = value;
            }
        }

        /// <summary>
        /// LocalevendorIDCollection
        /// </summary>
        public ArrayList LocaleVendorIDCollection
        {
            get
            {
                if (localeVendorIDCollection == null)
                    return new ArrayList();
                return localeVendorIDCollection;
            }
            set
            {
                localeVendorIDCollection = value;
            }
        }

        /// <summary>
        /// LocaleWeightCollection
        /// </summary>
        public ArrayList LocaleWeightCollection
        {
            get
            {
                if (localeWeightCollection == null)
                    return new ArrayList();
                return localeWeightCollection;
            }
            set
            {
                localeWeightCollection = value;
            }
        }

        /// <summary>
        /// IsUpdatePhaseDetails
        /// </summary>
        public bool IsUpdatePhaseDetails
        {
            get
            {
                return isUpdatePhaseDetails;
            }
            set
            {
                isUpdatePhaseDetails = value;
            }
        }

        /// <summary>
        /// IsActive
        /// </summary>
        public bool IsActive
        {
            get
            {
                return isActive;
            }
            set
            {
                isActive = value;
            }
        }

        /// <summary>
        /// ProjectLocaleVsPlatformMatrix
        /// </summary>
        public ArrayList ProjectLocaleVsPlatformMatrix
        {
            get
            {
                if (projectLocaleVsPlatformMatrix == null)
                    return new ArrayList();
                return projectLocaleVsPlatformMatrix;
            }
            set
            {
                projectLocaleVsPlatformMatrix = value;
            }
        }

        /// <summary>
        /// copyProjectPhaseID
        /// </summary>
        public string CopyProjectPhaseID
        {
            get
            {
                if (copyProjectPhaseID == null || copyProjectPhaseID == "0")
                    return "";
                return copyProjectPhaseID;
            }
            set
            {
                copyProjectPhaseID = value;
            }
        }

        /// <summary>
        /// IsCopyLocales
        /// </summary>
        public bool IsCopyLocales
        {
            get
            {
                return isCopyLocales;
            }
            set
            {
                isCopyLocales = value;
            }
        }

        /// <summary>
        /// isCopyPlatforms
        /// </summary>
        public bool IsCopyPlatforms
        {
            get
            {
                return isCopyPlatforms;
            }
            set
            {
                isCopyPlatforms = value;
            }
        }

        /// <summary>
        /// isCopyMatrix
        /// </summary>
        public bool IsCopyMatrix
        {
            get
            {
                return isCopyMatrix;
            }
            set
            {
                isCopyMatrix = value;
            }
        }

        #endregion
    }
}
