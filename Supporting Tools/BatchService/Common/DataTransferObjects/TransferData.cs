using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Adobe.Localization.Insights.Common.DataTransferObjects
{
    [Serializable]
    public class TransferData
    {
        #region Variables

        private Header dataHeader;
        private string userID;
        private string loginID;
        private string productID;
        private string productVersionID;
        private string vendorID;
        private string userRightID;
        private string userProductID;
        private string sectionID;
        private string roleID;

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
        /// LoginID
        /// </summary>
        public string LoginID
        {
            get
            {
                if (loginID == null || loginID == "0")
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
                if (userRightID == null || userRightID == "0")
                    return "";
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
                if (sectionID == null || sectionID == "0")
                    return "";
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

        #endregion
    }
}
