using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Adobe.Localization.Insights.Common.DataTransferObjects
{
    [Serializable]
    public class Screens
    {
        #region Variables

        private Header dataHeader;
        private string screenID;
        private string screenLabelID;
        private string labelCategoryID;
        private string localeID;
        private string localeCode;
        private string screenLocalizedLabelID;
        private string parentScreenID;
        private string screenIdentifier;
        private string screenLabel;
        private string localizedValue;
        private string sequence;
        private string isPage;
        private string pageName;
        private string projectRoleID;
        private string isRead;
        private string isReadWrite;
        private string isReport;
        private string screenAccessExists;
        private bool isAllScreen = true;
        private bool isIncludeCommon = false;
        private List<Screens> labelCollections;

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
        /// screenID
        /// </summary>
        public string ScreenID
        {
            get
            {
                if (screenID == null || screenID == "0")
                    return "";
                return screenID;
            }
            set
            {
                screenID = value;
            }
        }

        /// <summary>
        /// screenLabelID
        /// </summary>
        public string ScreenLabelID
        {
            get
            {
                if (screenLabelID == null || screenLabelID == "0")
                    return "";
                return screenLabelID;
            }
            set
            {
                screenLabelID = value;
            }
        }

        /// <summary>
        /// LabelCategoryID
        /// </summary>
        public string LabelCategoryID
        {
            get
            {
                if (labelCategoryID == null || labelCategoryID == "0")
                    return "";
                return labelCategoryID;
            }
            set
            {
                labelCategoryID = value;
            }
        }

        /// <summary>
        /// LocaleID
        /// </summary>
        public string LocaleID
        {
            get
            {
                if (localeID == null || localeID == "0")
                    return "";
                return localeID;
            }
            set
            {
                localeID = value;
            }
        }

        /// <summary>
        /// LocaleCode
        /// </summary>
        public string LocaleCode
        {
            get
            {
                if (localeCode == null)
                    return "";
                return localeCode;
            }
            set
            {
                localeCode = value;
            }
        }

        /// <summary>
        /// ScreenLocalizedLabelID
        /// </summary>
        public string ScreenLocalizedLabelID
        {
            get
            {
                if (screenLocalizedLabelID == null || screenLocalizedLabelID == "0")
                    return "";
                return screenLocalizedLabelID;
            }
            set
            {
                screenLocalizedLabelID = value;
            }
        }

        /// <summary>
        /// parentScreenID
        /// </summary>
        public string ParentScreenID
        {
            get
            {
                if (parentScreenID == null || parentScreenID == "0")
                    return "";
                return parentScreenID;
            }
            set
            {
                parentScreenID = value;
            }
        }

        /// <summary>
        /// screenIdentifier
        /// </summary>
        public string ScreenIdentifier
        {
            get
            {
                if (screenIdentifier == null || screenIdentifier == "0")
                    return "";
                return screenIdentifier;
            }
            set
            {
                screenIdentifier = value;
            }
        }

        /// <summary>
        /// ScreenLabel
        /// </summary>
        public string ScreenLabel
        {
            get
            {
                if (screenLabel == null)
                    return "";
                return screenLabel;
            }
            set
            {
                screenLabel = value;
            }
        }

        /// <summary>
        /// LocalizedValue
        /// </summary>
        public string LocalizedValue
        {
            get
            {
                if (localizedValue == null || localizedValue == "0")
                    return "";
                return localizedValue;
            }
            set
            {
                localizedValue = value;
            }
        }

        /// <summary>
        /// sequence
        /// </summary>
        public string Sequence
        {
            get
            {
                if (isPage == null || isPage == "")
                    return "0";
                return sequence;
            }
            set
            {
                sequence = value;
            }
        }

        /// <summary>
        /// IsPage
        /// </summary>
        public string IsPage
        {
            get
            {
                if (isPage == null || isPage == "")
                    return "";
                return isPage;
            }
            set
            {
                isPage = value;
            }
        }

        /// <summary>
        /// pageName
        /// </summary>
        public string PageName
        {
            get
            {
                return pageName;
            }
            set
            {
                pageName = value;
            }
        }

        /// <summary>
        /// projectRoleID
        /// </summary>
        public string ProjectRoleID
        {
            get
            {
                if (projectRoleID == null || projectRoleID == "")
                    return "";
                return projectRoleID;
            }
            set
            {
                projectRoleID = value;
            }
        }

        /// <summary>
        /// screenAccessExists
        /// </summary>
        public string ScreenAccessExists
        {
            get
            {
                if (screenAccessExists == null || screenAccessExists == "")
                    return "";
                return screenAccessExists;
            }
            set
            {
                screenAccessExists = value;
            }
        }

        /// <summary>
        /// isRead
        /// </summary>
        public string IsRead
        {
            get
            {
                if (isRead == null || isRead.ToUpper() == "FALSE")
                    return "0";
                else
                    return "1";
            }
            set
            {
                isRead = value;
            }
        }

        /// <summary>
        /// isReadWrite
        /// </summary>
        public string IsReadWrite
        {
            get
            {
                if (isReadWrite == null || isReadWrite.ToUpper() == "FALSE")
                    return "0";
                else
                    return "1";
            }
            set
            {
                isReadWrite = value;
            }
        }

        /// <summary>
        /// isReport
        /// </summary>
        public string IsReport
        {
            get
            {
                if (isReport == null || isReport.ToUpper() == "FALSE")
                    return "0";
                else
                    return "1";
            }
            set
            {
                isReport = value;
            }
        }

        /// <summary>
        /// IsAllScreen
        /// </summary>
        public bool IsAllScreens
        {
            get
            {
                return isAllScreen;
            }
            set
            {
                isAllScreen = value;
            }
        }

        /// <summary>
        /// IsIncludeCommon
        /// </summary>
        public bool IsIncludeCommon
        {
            get
            {
                return isIncludeCommon;
            }
            set
            {
                isIncludeCommon = value;
            }
        }

        /// <summary>
        /// LabelCollections
        /// </summary>
        public List<Screens> LabelCollections
        {
            get
            {
                return labelCollections;
            }
            set
            {
                labelCollections = value;
            }
        }

        #endregion
    }
}
