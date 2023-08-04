using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Generic;

namespace Adobe.Localization.Insights.Common.DataTransferObjects
{
    [Serializable]
    public class Product
    {
        #region Variables

        private Header dataHeader;
        private string productID;
        private string productVersionID;
        private string projectPhaseID;
        private string aboutProduct;
        private string userID;
        private string vendorID;
        private string productOwnerID;
        private string userProductID;
        private string productVersion;
        private string productCodeName;
        private string releaseTypeID;
        private string productSprintID;
        private string productSprint;
        private string productSprintDetails;
        private string projectBuildDetailID;
        private string projectBuildCode;
        private string projectBuildDetails;
        private string startDate;
        private string endDate;
        private string productYear;
        private string isActive;
        private string isClosed;
        private string windUpId;
        private string projectGMDetailID;
        private string productLinkID;
        private string postMortemDetails;
        private string learnings;
        private string bestPractices;
        private string productBuildID;
        private string buildNo;
        private string gmDate;
        private string buildPath;
        private string copyProductVersionID;
        private string documentName;
        private string documentLink;
        private string isReleaseBuild = "0";
        private bool isOwner = false;
        private bool isCopyLocales = false;
        private bool isCopyPlatforms = false;
        private bool isCopyUsers = false;
        private bool isCopyPhases = false;
        private bool isCopyWSRParameters = false;
        private bool isCopySprintDetails = false;
        private bool isCopyProjectBuildDetails = false;
        private ProjectPhases projectPhase;
        private ArrayList localeIDCollection;
        private ArrayList platformIDCollection;
        private ArrayList productPlatformIDCollection;
        private ArrayList productPlatformPriorityCollection;
        private ArrayList projectBuildDetailIDCollection;
        private ArrayList projectBuildLocaleIDCollection;

        //private bool isUsersFilter;
        //private bool isVendorFilter;

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
        /// AboutProduct
        /// </summary>
        public string AboutProduct
        {
            get
            {
                if (aboutProduct == null)
                    return "";
                return aboutProduct;
            }
            set
            {
                aboutProduct = value;
            }
        }

        /// <summary>
        /// UserID
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
        /// ProductOwnerID
        /// </summary>
        public string ProductOwnerID
        {
            get
            {
                if (productOwnerID == null || productOwnerID == "0")
                    return "";
                return productOwnerID;
            }
            set
            {
                productOwnerID = value;
            }
        }

        /// <summary>
        /// UserProductID
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
        /// ProductVersion
        /// </summary>
        public string ProductVersion
        {
            get
            {
                if (productVersion == null || productVersion == "0")
                    return "";
                return productVersion;
            }
            set
            {
                productVersion = value;
            }
        }

        /// <summary>
        /// ProductCodeName
        /// </summary>
        public string ProductCodeName
        {
            get
            {
                if (productCodeName == null)
                    return "";
                return productCodeName;
            }
            set
            {
                productCodeName = value;
            }
        }

        /// <summary>
        /// ReleaseTypeID
        /// </summary>
        public string ReleaseTypeID
        {
            get
            {
                if (releaseTypeID == null || releaseTypeID == "0")
                    return "";
                return releaseTypeID;
            }
            set
            {
                releaseTypeID = value;
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
        /// ProjectBuildDetailID
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
        /// ProductSprint
        /// </summary>
        public string ProductSprint
        {
            get
            {
                if (productSprint == null)
                    return "";
                return productSprint;
            }
            set
            {
                productSprint = value;
            }
        }

        /// <summary>
        /// ProductSprintDetails
        /// </summary>
        public string ProductSprintDetails
        {
            get
            {
                if (productSprintDetails == null)
                    return "";
                return productSprintDetails;
            }
            set
            {
                productSprintDetails = value;
            }
        }

        /// <summary>
        /// projectBuildCode
        /// </summary>
        public string ProjectBuildCode
        {
            get
            {
                if (projectBuildCode == null)
                    return "";
                return projectBuildCode;
            }
            set
            {
                projectBuildCode = value;
            }
        }

        /// <summary>
        /// projectBuildDetails
        /// </summary>
        public string ProjectBuildDetails
        {
            get
            {
                if (projectBuildDetails == null)
                    return "";
                return projectBuildDetails;
            }
            set
            {
                projectBuildDetails = value;
            }
        }

        /// <summary>
        /// ProductYear
        /// </summary>
        public string ProductYear
        {
            get
            {
                if (productYear == null || productYear == "0" || productYear == "")
                    return System.DateTime.Now.Year.ToString();
                return productYear;
            }
            set
            {
                productYear = value;
            }
        }

        /// <summary>
        /// LocaleIDCollection
        /// </summary>
        public ArrayList LocaleIDCollection
        {
            get
            {
                if (localeIDCollection == null)
                    return new ArrayList();
                return localeIDCollection;
            }
            set
            {
                localeIDCollection = value;
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
        /// ProductPlatformIDCollection
        /// </summary>
        public ArrayList ProductPlatformIDCollection
        {
            get
            {
                if (productPlatformIDCollection == null)
                    return new ArrayList();
                return productPlatformIDCollection;
            }
            set
            {
                productPlatformIDCollection = value;
            }
        }

        /// <summary>
        /// ProductPlatformPriorityCollection
        /// </summary>
        public ArrayList ProductPlatformPriorityCollection
        {
            get
            {
                if (productPlatformPriorityCollection == null)
                    return new ArrayList();
                return productPlatformPriorityCollection;
            }
            set
            {
                productPlatformPriorityCollection = value;
            }
        }

        /// <summary>
        /// ProjectBuildDetailIDCollection
        /// </summary>
        public ArrayList ProjectBuildDetailIDCollection
        {
            get
            {
                if (projectBuildDetailIDCollection == null)
                    return new ArrayList();
                return projectBuildDetailIDCollection;
            }
            set
            {
                projectBuildDetailIDCollection = value;
            }
        }

        /// <summary>
        /// projectBuildLocaleIDCollection
        /// </summary>
        public ArrayList ProjectBuildLocaleIDCollection
        {
            get
            {
                if (projectBuildLocaleIDCollection == null)
                    return new ArrayList();
                return projectBuildLocaleIDCollection;
            }
            set
            {
                projectBuildLocaleIDCollection = value;
            }
        }

        /// <summary>
        /// IsOwner
        /// </summary>
        public bool IsOwner
        {
            get
            {
                return isOwner;
            }
            set
            {
                isOwner = value;
            }
        }

        /// <summary>
        /// IsActive
        /// </summary>
        public string IsActive
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
        /// IsClosed
        /// </summary>
        public string IsClosed
        {
            get
            {
                return isClosed;
            }
            set
            {
                isClosed = value;
            }
        }

        /// <summary>
        /// ProjectPhase
        /// </summary>
        public ProjectPhases ProjectPhase
        {
            get
            {
                if (projectPhase == null)
                    return new ProjectPhases();
                return projectPhase;
            }
            set
            {
                projectPhase = value;
            }
        }

        ///// <summary>
        ///// IsUsersFilter
        ///// </summary>
        //public bool IsUsersFilter
        //{
        //    get
        //    {
        //        return isUsersFilter;
        //    }
        //    set
        //    {
        //        isUsersFilter = value;
        //    }
        //}

        ///// <summary>
        ///// IsVendorFilter
        ///// </summary>
        //public bool IsVendorFilter
        //{
        //    get
        //    {
        //        return isVendorFilter;
        //    }
        //    set
        //    {
        //        isVendorFilter = value;
        //    }
        //}

        /// <summary>
        /// WindUpId
        /// </summary>
        public string WindUpId
        {
            get
            {
                if (windUpId == null || windUpId == "0")
                    return "";
                return windUpId;
            }
            set
            {
                windUpId = value;
            }
        }

        /// <summary>
        /// ProjectGMDetailID
        /// </summary>
        public string ProjectGMDetailID
        {
            get
            {
                if (projectGMDetailID == null || projectGMDetailID == "0")
                    return "";
                return projectGMDetailID;
            }
            set
            {
                projectGMDetailID = value;
            }
        }

        /// <summary>
        /// gmDate
        /// </summary>
        public string GmDate
        {
            get
            {
                return gmDate;
            }
            set
            {
                gmDate = value;
            }
        }

        /// <summary>
        /// ProductLinkID
        /// </summary>
        public string ProductLinkID
        {
            get
            {
                if (productLinkID == null || productLinkID == "0")
                    return "";
                return productLinkID;
            }
            set
            {
                productLinkID = value;
            }
        }

        /// <summary>
        /// BuildNo
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
        /// BuildPath
        /// </summary>
        public string BuildPath
        {
            get
            {
                return buildPath;
            }
            set
            {
                buildPath = value;
            }
        }

        /// <summary>
        /// productBuildID
        /// </summary>
        public string ProductBuildID
        {
            get
            {
                return productBuildID;
            }
            set
            {
                productBuildID = value;
            }
        }

        /// <summary>
        /// PostMortemDetails
        /// </summary>
        public string PostMortemDetails
        {
            get
            {
                return postMortemDetails;
            }
            set
            {
                postMortemDetails = value;
            }
        }

        /// <summary>
        /// learnings
        /// </summary>
        public string Learnings
        {
            get
            {
                return learnings;
            }
            set
            {
                learnings = value;
            }
        }

        /// <summary>
        /// BestPractices
        /// </summary>
        public string BestPractices
        {
            get
            {
                return bestPractices;
            }
            set
            {
                bestPractices = value;
            }
        }

        /// <summary>
        /// CopyProductVersionID
        /// </summary>
        public string CopyProductVersionID
        {
            get
            {
                if (copyProductVersionID == null || copyProductVersionID == "0")
                    return "";
                return copyProductVersionID;
            }
            set
            {
                copyProductVersionID = value;
            }
        }

        /// <summary>
        /// DocumentName
        /// </summary>
        public string DocumentName
        {
            get
            {
                if (documentName == null)
                    return "";
                return documentName;
            }
            set
            {
                documentName = value;
            }
        }

        /// <summary>
        /// DocumentLink
        /// </summary>
        public string DocumentLink
        {
            get
            {
                if (documentLink == null)
                    return "";
                return documentLink;
            }
            set
            {
                documentLink = value;
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
        /// isCopyPhases
        /// </summary>
        public bool IsCopyPhases
        {
            get
            {
                return isCopyPhases;
            }
            set
            {
                isCopyPhases = value;
            }
        }

        /// <summary>
        /// isCopyUsers
        /// </summary>
        public bool IsCopyUsers
        {
            get
            {
                return isCopyUsers;
            }
            set
            {
                isCopyUsers = value;
            }
        }

        /// <summary>
        /// isCopyWSRParameters
        /// </summary>
        public bool IsCopyWSRParameters
        {
            get
            {
                return isCopyWSRParameters;
            }
            set
            {
                isCopyWSRParameters = value;
            }
        }

        /// <summary>
        /// isReleaseBuild
        /// </summary>
        public string IsReleaseBuild
        {
            get
            {
                return isReleaseBuild;
            }
            set
            {
                isReleaseBuild = value;
            }
        }

        /// <summary>
        /// isCopySprintDetails
        /// </summary>
        public bool IsCopySprintDetails
        {
            get
            {
                return isCopySprintDetails;
            }
            set
            {
                isCopySprintDetails = value;
            }
        }

        /// <summary>
        /// isCopyProjectBuildDetails
        /// </summary>
        public bool IsCopyProjectBuildDetails
        {
            get
            {
                return isCopyProjectBuildDetails;
            }
            set
            {
                isCopyProjectBuildDetails = value;
            }
        }

        #endregion
    }
}
