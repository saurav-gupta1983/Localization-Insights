using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using Adobe.Localization.Insights.Common;
using DTO = Adobe.Localization.Insights.Common.DataTransferObjects;

namespace Adobe.Localization.Insights.DataLayer.TestStudioComponents
{
    /// <summary>
    /// LoginAPI
    /// </summary>
    public class LoginAPI
    {
        #region Private Variables

        private string userLoginID;
        private string userPassword;

        #endregion

        #region Public Members

        /// <summary>
        /// Login
        /// </summary>
        public DTO.TestSudio Login(DTO.TestSudio ts)
        {
            if (System.Configuration.ConfigurationManager.AppSettings[WebConstants.SETTING_ENVIRONMENT].ToString() == "PROD")
            {
                userLoginID = ts.DataHeader.LoginID;
                userPassword = ts.DataHeader.Password;
            }
            else
            {
                userLoginID = "tsuser1";
                userPassword = "Te5t$tud";
            }

            TestStudioService.TestStudio service = new TestStudioService.TestStudioClient();

            TestStudioService.login loginObj = new TestStudioService.login();
            loginObj.ldapID = userLoginID;
            loginObj.ldapPasswd = userPassword;

            TestStudioService.loginRequest request = new TestStudioService.loginRequest(loginObj);
            TestStudioService.loginResponse1 response = null;
            try
            {
                response = service.login(request);

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(response.loginResponse.@return);

                XmlNodeList sessionID = doc.GetElementsByTagName(WebConstants.TS_SESSION_ID);

                if (sessionID != null && sessionID.Count > 0)
                    ts.SessionID = sessionID[0].InnerText;
            }

            catch (Exception ex)
            {
                ts.ErrorCode = "-1";
                ts.ErrorMessage = ex.Message;
            }

            return ts;
        }

        #endregion
    }
}
