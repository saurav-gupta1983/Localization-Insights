using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Adobe.Localization.Insights.Common.DataTransferObjects
{
    [Serializable]
    public class Users
    {
        #region Variables

        private Header dataHeader;
        private string userID;
        private string loginID;
        private string productID;
        private string productPrefID;
        private string productVersionID;
        private string vendorID;
        private string userRightID;
        private string userProductID;
        private string sectionID;
        private string roleID;
        private string projectRoleID;
        private string userProjectRoleID;
        private string firstName;
        private string lastName;
        private string userName;
        private string nickName;
        private string emailID;
        private string alternateEmailID;
        private string managerUserID;
        private string contactNo;
        private string superUser = "0";
        private string isProductOwner;
        private string isSelected;
        private string incidentDetails;
        private string severity;
        private string teamFeedbackID;
        private bool isRequiredProductVersion = false;
        private bool isManager = false;
        private bool isUserMasterScreen = false;

        private ArrayList userCollection;

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
        /// UserID
        /// </summary>
        public string UserID
        {
            get
            {
                if (userID == null)
                    return "";
                return userID;
            }
            set
            {
                userID = value;
            }
        }

        /// <summary>
        /// LoginID
        /// </summary>
        public string LoginID
        {
            get
            {
                if (loginID == null)
                    return "";
                return loginID;
            }
            set
            {
                loginID = value;
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
        /// productPrefID
        /// </summary>
        public string ProductPrefID
        {
            get
            {
                if (productPrefID == null)
                    return "";
                return productPrefID;
            }
            set
            {
                productPrefID = value;
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
        /// UserRightID
        /// </summary>
        public string UserRightID
        {
            get
            {
                return userRightID;
            }
            set
            {
                userRightID = value;
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
        /// SectionID
        /// </summary>
        public string SectionID
        {
            get
            {
                return sectionID;
            }
            set
            {
                sectionID = value;
            }
        }

        /// <summary>
        /// RoleID
        /// </summary>
        public string RoleID
        {
            get
            {
                if (roleID == null || roleID == "0")
                    return "";
                return roleID;
            }
            set
            {
                roleID = value;
            }
        }

        /// <summary>
        /// ProjectRoleID
        /// </summary>
        public string ProjectRoleID
        {
            get
            {
                if (projectRoleID == null || projectRoleID == "0")
                    return "";
                return projectRoleID;
            }
            set
            {
                projectRoleID = value;
            }
        }

        /// <summary>
        /// ProjectRoleID
        /// </summary>
        public string UserProjectRoleID
        {
            get
            {
                if (userProjectRoleID == null || userProjectRoleID == "0")
                    return "";
                return userProjectRoleID;
            }
            set
            {
                userProjectRoleID = value;
            }
        }

        /// <summary>
        /// FirstName
        /// </summary>
        public string FirstName
        {
            get
            {
                if (firstName == null)
                    return "";
                return firstName;
            }
            set
            {
                firstName = value;
            }
        }

        /// <summary>
        /// LastName
        /// </summary>
        public string LastName
        {
            get
            {
                if (lastName == null)
                    return "";
                return lastName;
            }
            set
            {
                lastName = value;
            }
        }

        /// <summary>
        /// UserName
        /// </summary>
        public string UserName
        {
            get
            {
                if (userName == null)
                    return "";
                return userName;
            }
            set
            {
                userName = value;
            }
        }

        /// <summary>
        /// NickName
        /// </summary>
        public string NickName
        {
            get
            {
                if (nickName == null)
                    return "";
                return nickName;
            }
            set
            {
                nickName = value;
            }
        }

        /// <summary>
        /// emailID
        /// </summary>
        public string EmailID
        {
            get
            {
                if (emailID == null)
                    return "";
                return emailID;
            }
            set
            {
                emailID = value;
            }
        }

        /// <summary>
        /// alternateEmailID
        /// </summary>
        public string AlternateEmailID
        {
            get
            {
                if (alternateEmailID == null)
                    return "";
                return alternateEmailID;
            }
            set
            {
                alternateEmailID = value;
            }
        }

        /// <summary>
        /// managerUserID
        /// </summary>
        public string ManagerUserID
        {
            get
            {
                if (managerUserID == null)
                    return "";
                return managerUserID;
            }
            set
            {
                managerUserID = value;
            }
        }

        /// <summary>
        /// ContactNo
        /// </summary>
        public string ContactNo
        {
            get
            {
                if (contactNo == null)
                    return "";
                return contactNo;
            }
            set
            {
                contactNo = value;
            }
        }

        /// <summary>
        /// SuperUser
        /// </summary>
        public string SuperUser
        {
            get
            {
                return superUser;
            }
            set
            {
                superUser = value;
            }
        }

        /// <summary>
        /// projectRoles
        /// </summary>
        public ArrayList UserCollection
        {
            get
            {
                return userCollection;
            }
            set
            {
                userCollection = value;
            }
        }

        /// <summary>
        /// isProductOwner
        /// </summary>
        public string IsProductOwner
        {
            get
            {
                return isProductOwner;
            }
            set
            {
                isProductOwner = value;
            }
        }

        /// <summary>
        /// isSelected
        /// </summary>
        public string IsSelected
        {
            get
            {
                if (isSelected == null || isSelected == "0" || isSelected.ToUpper() == "FALSE")
                    return "0";
                return "1";
            }
            set
            {
                isSelected = value;
            }
        }
        
        /// <summary>
        /// TeamFeedbackID
        /// </summary>
        public string TeamFeedbackID
        {
            get
            {
                if (teamFeedbackID == null)
                    return "";
                return teamFeedbackID;
            }
            set
            {
                teamFeedbackID = value;
            }
        }

        /// <summary>
        /// incidentDetails
        /// </summary>
        public string IncidentDetails
        {
            get
            {
                return incidentDetails;
            }
            set
            {
                incidentDetails = value;
            }
        }

        /// <summary>
        /// severity
        /// </summary>
        public string Severity
        {
            get
            {
                return severity;
            }
            set
            {
                severity = value;
            }
        }

        /// <summary>
        /// IsRequiredProductVersion
        /// </summary>
        public bool IsRequiredProductVersion
        {
            get
            {
                return isRequiredProductVersion;
            }
            set
            {
                isRequiredProductVersion = value;
            }
        }

        /// <summary>
        /// IsManager
        /// </summary>
        public bool IsManager
        {
            get
            {
                return isManager;
            }
            set
            {
                isManager = value;
            }
        }

        /// <summary>
        /// IsUserMasterScreen
        /// </summary>
        public bool IsUserMasterScreen
        {
            get
            {
                return isUserMasterScreen;
            }
            set
            {
                isUserMasterScreen = value;
            }
        }

        #endregion
    }
}
