using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Adobe.Localization.Insights.Common.DataTransferObjects
{
    [Serializable]
    public class Efforts
    {
        #region Variables

        private Header dataHeader;
        private string primaryKeyID;
        private string wsrParameterID;
        private string date;
        private string quantity;
        private string effort;
        private string remarks;

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
        /// PrimaryKeyID
        /// </summary>
        public string PrimaryKeyID
        {
            get
            {
                if (primaryKeyID == null || primaryKeyID == "0")
                    return "";
                return primaryKeyID;
            }
            set
            {
                primaryKeyID = value;
            }
        }

        /// <summary>
        /// wsrParameterID
        /// </summary>
        public string WSRParameterID
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
        /// Date
        /// </summary>
        public string Date
        {
            get
            {
                if (date == null || date == "0")
                    return "";
                return date;
            }
            set
            {
                date = value;
            }
        }

        /// <summary>
        /// quantity
        /// </summary>
        public string Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                quantity = value;
            }
        }

        /// <summary>
        /// Efforts
        /// </summary>
        public string Effort
        {
            get
            {
                if (effort == null || effort == "")
                    return "0";
                return effort;
            }
            set
            {
                effort = value;
            }
        }

        /// <summary>
        /// remarks
        /// </summary>
        public string Remarks
        {
            get
            {
                return remarks;
            }
            set
            {
                remarks = value;
            }
        }

        #endregion
    }
}
