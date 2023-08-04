using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Generic;

namespace Adobe.Localization.Insights.Common.DataTransferObjects
{
    [Serializable]
    public class ProjectLocaleVsPlatformMatrix
    {
        #region Variables

        private Header dataHeader;
        private string projectLocaleID;
        private string projectDataID;
        private string projectPlatformID;
        private string tcCount;
        private string tcPercent;
        private string tcNA;
        private string tcExecuted;
        private string sids;
        private string tsNo;
        private string userAccess;
        private string productVersionID;
        private string projectPhaseID;
        private string phaseCoverageDetailID;
        private string vendorID;
        private string platformTypeID;
        private string projectBuildDetailID;
        private string productLocaleID;
        private bool isSelected = false;

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
        /// ProjectDataID
        /// </summary>
        public string ProjectDataID
        {
            get
            {
                if (projectDataID == null || projectDataID == "0")
                    return "";
                return projectDataID;
            }
            set
            {
                projectDataID = value;
            }
        }

        /// <summary>
        /// ProjectLocaleID
        /// </summary>
        public string ProjectLocaleID
        {
            get
            {
                if (projectLocaleID == null || projectLocaleID == "0")
                    return "";
                return projectLocaleID;
            }
            set
            {
                projectLocaleID = value;
            }
        }

        /// <summary>
        /// ProjectPlatformID
        /// </summary>
        public string ProjectPlatformID
        {
            get
            {
                if (projectPlatformID == null)
                    return "";
                return projectPlatformID;
            }
            set
            {
                projectPlatformID = value;
            }
        }

        /// <summary>
        /// TCCount
        /// </summary>
        public string TC_Count
        {
            get
            {
                if (tcCount == null || tcCount == "")
                    return "0";
                return tcCount;
            }
            set
            {
                tcCount = value;
            }
        }

        /// <summary>
        /// TCPercent
        /// </summary>
        public string TC_Percent
        {
            get
            {
                if (tcPercent == null)
                    return "1";
                return tcPercent;
            }
            set
            {
                tcPercent = value;
            }
        }

        /// <summary>
        /// TCNA
        /// </summary>
        public string TC_NA
        {
            get
            {
                if (tcNA == null || tcNA == "")
                    return "0";
                return tcNA;
            }
            set
            {
                tcNA = value;
            }
        }

        /// <summary>
        /// TC_Executed
        /// </summary>
        public string TC_Executed
        {
            get
            {
                if (tcExecuted == null || tcExecuted == "")
                    return "0";
                return tcExecuted;
            }
            set
            {
                tcExecuted = value;
            }
        }

        /// <summary>
        /// SIDs
        /// </summary>
        public string SIDs
        {
            get
            {
                if (sids == null || sids == "0")
                    return "";
                return sids;
            }
            set
            {
                sids = value;
            }
        }

        /// <summary>
        /// TsNo
        /// </summary>
        public string TsNo
        {
            get
            {
                if (tsNo == null || tsNo == "0")
                    return "";
                return tsNo;
            }
            set
            {
                tsNo = value;
            }
        }

        /// <summary>
        /// UserAccess
        /// </summary>
        public string UserAccess
        {
            get
            {
                return userAccess;
            }
            set
            {
                userAccess = value;
            }
        }

        /// <summary>
        /// productVersionID
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
        /// phaseCoverageDetailID
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
        /// vendorID
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
        /// isSelected
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                isSelected = value;
            }
        }

        #endregion

    }
}
