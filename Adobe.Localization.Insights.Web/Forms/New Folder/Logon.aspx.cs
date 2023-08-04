using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;
using Adobe.Localization.Insights.Common;
using COM = Adobe.Localization.Insights.Common.Common;
using BO = Adobe.Localization.Insights.BusinessLayer;
using DTO = Adobe.Localization.Insights.Common.DataTransferObjects;

namespace Adobe.Localization.Insights.Web
{
    /// <summary>
    /// Logon
    /// </summary>
    public partial class Logon : System.Web.UI.Page
    {
        #region Variables

        private DataTable dtScreenLabels;

        private string domain;
        private string loginID;
        private string password;

        private const string STR_INVALID_CREDENTIALS = "Invalid Credentials";
        private const string STR_PROVIDE_CREDENTIALS = "Provide Credentials";
        private const string STR_SITE_ISSUES = "Site Issues";
        private const string STR_AUTHENTICATION_FAILED = "Authentication Failed";
        private const string STR_AUTHENTICATION_ERROR = "Authentication Error";

        #endregion

        #region Page Load Events

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            SetScreenAccessAndLabels();
            //LoginButton_Click(null, null);
        }

        #endregion

        #region Button Click Events

        /// <summary>
        /// LoginButton_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LoginButton_Click(object sender, EventArgs e)
        {
            domain = WebConstants.STR_DOMAIN;
            loginID = UserID.Text;
            password = UserPassword.Text;

            if (loginID != "" && password != "")
            {
                if (PerformLDAPAuthentication())
                    CreateFormAuthenticationTicket();
                else
                    FailureText.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, STR_INVALID_CREDENTIALS);
            }
            else
                FailureText.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, STR_PROVIDE_CREDENTIALS);

            //loginID = "sauragup";
            CreateFormAuthenticationTicket();
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// CreateFormAuthenticationTicket
        /// </summary>
        private void CreateFormAuthenticationTicket()
        {
            // Retrieve the user's groups
            //string groups = adAuth.GetGroups();
            // Create the authetication ticket                    
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, loginID, DateTime.Now, DateTime.Now.AddMinutes(60), false, "LOCALIZATION");
            // Now encrypt the ticket.
            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            // Create a cookie and add the encrypted ticket to the
            // cookie as data.
            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            // Add the cookie to the outgoing cookies collection.
            Response.Cookies.Add(authCookie);

            if (FirstTimeUser(loginID))
            {
                if (AddUser(loginID))
                {
                    SendEmailNotification(loginID);
                }
                else
                {
                    FailureText.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, STR_SITE_ISSUES);
                    return;
                }
            }

            // Redirect the user to the originally requested page
            Response.Redirect(FormsAuthentication.GetRedirectUrl(loginID, false));
        }

        /// <summary>
        /// AddUser
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private bool AddUser(string loginID)
        {
            if (BO.BusinessObjects.AddNewUser(loginID))
            {
                return CheckUserDetails();
            }
            else
                return false;
        }

        /// <summary>
        /// FirstTimeUser
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private bool FirstTimeUser(string loginID)
        {
            return CheckUserDetails();
        }

        /// <summary>
        /// SendEmailNotification
        /// </summary>
        private void SendEmailNotification(string loginID)
        {
            EmailNotifications emailNotify = new EmailNotifications("", loginID + WebConstants.STR_ADOBE_COM);
            emailNotify.SendEmail(WebConstants.DEF_EMAIL_SUBJECT_FIRST_TIME_USER, WebConstants.DEF_EMAIL_BODY_FIRST_TIME_USER);
        }

        /// <summary>
        /// To authenticate the user and create a forms authentication ticket 
        /// </summary>
        private bool PerformLDAPAuthentication()
        {
            try
            {
                LdapAuthentication adAuth = new LdapAuthentication();

                if (adAuth.AuthenticateUser(domain, loginID, password))
                {
                    return true;
                }
                else
                {
                    FailureText.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, STR_AUTHENTICATION_FAILED);
                    return false;
                }
            }
            catch (Exception ex)
            {
                FailureText.Text = String.Format(COM.GetScreenLocalizedLabel(dtScreenLabels, STR_AUTHENTICATION_FAILED), ex.Message);
                return false;
            }
        }

        /// <summary>
        /// CheckUserDetails
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        private bool CheckUserDetails()
        {
            DTO.Users userData = new DTO.Users();
            userData.LoginID = loginID;

            DataTable dtUsers = BO.BusinessObjects.GetUserDetails(userData).Tables[0];
            dtUsers.Rows.RemoveAt(0);

            if (dtUsers.Rows.Count == 0)
                return false;

            Session[WebConstants.SESSION_USER_ID] = dtUsers.Rows[0][WebConstants.COL_USER_ID].ToString();
            Session[WebConstants.SESSION_LOGIN_ID] = dtUsers.Rows[0][WebConstants.COL_LOGIN_ID].ToString();
            Session[WebConstants.SESSION_VENDOR_ID] = dtUsers.Rows[0][WebConstants.COL_VENDOR_ID].ToString();

            return true;
        }

        #endregion

        #region Screen Access and Labels

        /// <summary>
        /// SetScreenAccessAndLabels
        /// </summary>
        /// <param name="userID"></param>
        private void SetScreenAccessAndLabels()
        {
            DTO.Screens screenData = new DTO.Screens();
            screenData.ScreenIdentifier = WebConstants.SCREEN_LOGON;

            if (Session[WebConstants.SESSION_LOCALE_ID] != null)
                screenData.LocaleID = Session[WebConstants.SESSION_LOCALE_ID].ToString();
            else
                screenData.LocaleID = WebConstants.DEF_VAL_LOCALE_ID;

            DataTable dtLocales = (DataTable)ViewState[WebConstants.TBL_LOCALES];
            if (dtLocales == null)
            {
                dtLocales = BO.BusinessObjects.GetAllLocales().Tables[0];
                ViewState[WebConstants.TBL_LOCALES] = dtLocales;
            }

            screenData.LocaleID = Common.Common.GetLocaleForScreenLabels(screenData.LocaleID, dtLocales)[WebConstants.COL_LOCALE_ID].ToString();

            DataTable dtScreenLabels = BO.BusinessObjects.GetScreenLocalizedLabels(screenData).Tables[0];

            SetScreenLabels(dtScreenLabels);
        }

        /// <summary>
        /// SetScreenLabels
        /// </summary>
        /// <param name="dtScreenLabels"></param>
        private void SetScreenLabels(DataTable dtScreenLabels)
        {
        }

        #endregion
    }
}