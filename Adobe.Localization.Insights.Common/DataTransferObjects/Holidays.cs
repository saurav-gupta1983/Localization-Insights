using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Adobe.Localization.Insights.Common.DataTransferObjects
{
    [Serializable]
    public class Holidays
    {
        #region Variables

        private Header dataHeader;
        private string holidayID;
        private string vendorID;
        private string reportingType;
        private string reportingDate;
        private string startDate;
        private string endDate;
        private string holidayReason;
        private string month;
        private string year;

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
        /// holidayID
        /// </summary>
        public string HolidayID
        {
            get
            {
                if (holidayID == null || holidayID == "0")
                    return "";
                return holidayID;
            }
            set
            {
                holidayID = value;
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
        /// reportingType
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
        /// reportingDate
        /// </summary>
        public string ReportingDate
        {
            get
            {
                if (reportingDate == null || reportingDate == "0")
                    return "";
                return reportingDate;
            }
            set
            {
                reportingDate = value;
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
                if (endDate == null || endDate == "0")
                    return "";
                return endDate;
            }
            set
            {
                endDate = value;
            }
        }

        /// <summary>
        /// holidayReason
        /// </summary>
        public string HolidayReason
        {
            get
            {
                if (holidayReason == null || holidayReason == "0")
                    return "";
                return holidayReason;
            }
            set
            {
                holidayReason = value;
            }
        }

        /// <summary>
        /// Month
        /// </summary>
        public string Month
        {
            get
            {
                if (month == null || month == "0")
                    return "";
                return month;
            }
            set
            {
                month = value;
            }
        }

        /// <summary>
        /// Year
        /// </summary>
        public string Year
        {
            get
            {
                if (year == null || year == "0")
                    return "";
                return year;
            }
            set
            {
                year = value;
            }
        }

        #endregion
    }
}
