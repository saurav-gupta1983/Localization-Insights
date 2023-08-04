using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Adobe.Localization.Insights.Common.DataTransferObjects
{
    [Serializable]
    public class WSRProductParameter
    {
        #region Variables

        private Header dataHeader;
        private string wsrParamSelectedID;
        private string wsrParameterID;
        private bool isSelected;

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
        /// wsrParamSelectedID
        /// </summary>
        public string WsrParamSelectedID
        {
            get
            {
                if (wsrParamSelectedID == null || wsrParamSelectedID == "0")
                    return "";
                return wsrParamSelectedID;
            }
            set
            {
                wsrParamSelectedID = value;
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

        #endregion
    }
}
