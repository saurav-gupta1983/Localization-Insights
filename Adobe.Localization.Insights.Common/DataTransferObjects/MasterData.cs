using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Generic;

namespace Adobe.Localization.Insights.Common.DataTransferObjects
{
    [Serializable]
    public class MasterData
    {
        #region Variables

        private Header dataHeader;
        private string tableName;
        private ArrayList columnNames;
        private string masterDataID;
        private string vendorID;
        private string userVendorID;
        private string productVersionID;
        private string code;
        private string filter;
        private string description;
        private string type;
        private string vendorLocation;
        private string isContractor;
        private string emailID;
        private string isSuperUser;
        private bool isUsersFilter;
        private bool isVendorFilter;

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
        /// TableName
        /// </summary>
        public string TableName
        {
            get
            {
                return tableName;
            }
            set
            {
                tableName = value;
            }
        }

        /// <summary>
        /// ColumnNames
        /// </summary>
        public ArrayList ColumnNames
        {
            get
            {
                return columnNames;
            }
            set
            {
                columnNames = value;
            }
        }

        /// <summary>
        /// MasterDataID
        /// </summary>
        public string MasterDataID
        {
            get
            {
                if (masterDataID == null || masterDataID == "0")
                    return "";
                return masterDataID;
            }
            set
            {
                masterDataID = value;
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
        /// UserVendorID
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
        /// Code
        /// </summary>
        public string Code
        {
            get
            {
                if (code == null || code == "0")
                    return "";
                return code;
            }
            set
            {
                code = value;
            }
        }

        /// <summary>
        /// Description
        /// </summary>
        public string Description
        {
            get
            {
                if (description == null || description == "0")
                    return "";
                return description;
            }
            set
            {
                description = value;
            }
        }

        /// <summary>
        /// Type
        /// </summary>
        public string Type
        {
            get
            {
                if (type == null || type == "0")
                    return "";
                return type;
            }
            set
            {
                type = value;
            }
        }

        /// <summary>
        /// Filter
        /// </summary>
        public string Filter
        {
            get
            {
                if (filter == null || filter == "0")
                    return "";
                return filter;
            }
            set
            {
                filter = value;
            }
        }

        /// <summary>
        /// VendorLocation
        /// </summary>
        public string VendorLocation
        {
            get
            {
                if (vendorLocation == null || vendorLocation == "0")
                    return "";
                return vendorLocation;
            }
            set
            {
                vendorLocation = value;
            }
        }

        /// <summary>
        /// IsContractor
        /// </summary>
        public string IsContractor
        {
            get
            {
                if (isContractor == null)
                    return "0";
                return isContractor;
            }
            set
            {
                isContractor = value;
            }
        }

        /// <summary>
        /// EmailID
        /// </summary>
        public string EmailID
        {
            get
            {
                if (emailID == null || emailID == "0")
                    return "";
                return emailID;
            }
            set
            {
                emailID = value;
            }
        }

        /// <summary>
        /// IsSuperUser
        /// </summary>
        public string IsSuperUser
        {
            get
            {
                return isSuperUser;
            }
            set
            {
                isSuperUser = value;
            }
        }

        /// <summary>
        /// IsUsersFilter
        /// </summary>
        public bool IsUsersFilter
        {
            get
            {
                return isUsersFilter;
            }
            set
            {
                isUsersFilter = value;
            }
        }

        /// <summary>
        /// IsVendorFilter
        /// </summary>
        public bool IsVendorFilter
        {
            get
            {
                return isVendorFilter;
            }
            set
            {
                isVendorFilter = value;
            }
        }

        #endregion
    }
}
