using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Adobe.Localization.Insights.Common.DataTransferObjects
{
    [Serializable]
    public class Header
    {
        #region Variables

        private string loginID;
        private string password;

        #endregion

        #region Public Properties

        /// <summary>
        /// LoginID
        /// </summary>
        public string LoginID
        {
            get
            {
                if (loginID == null || loginID == "")
                    return WebConstants.DEF_VAL_LOGIN_ID;
                return loginID.ToUpper();
            }
            set
            {
                loginID = value;
            }
        }

        /// <summary>
        /// Password
        /// </summary>
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

        #endregion
    }
}
