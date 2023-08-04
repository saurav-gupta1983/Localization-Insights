using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Adobe.Localization.Insights.Common;
using COM = Adobe.Localization.Insights.Common.Common;
using Adobe.Localization.Insights.Client.Classes;

namespace Adobe.Localization.Insights.Client.Forms
{
    /// <summary>
    /// LoginForm
    /// </summary>
    public partial class LoginForm : Form
    {
        #region Private Variables

        public bool IsLoggedIn { get; private set; }

        private string domain;
        private string loginID;
        private string password;

        private const string STR_INVALID_CREDENTIALS = "Invalid Credentials";
        private const string STR_PROVIDE_CREDENTIALS = "Provide Credentials";
        private const string STR_SITE_ISSUES = "Site Issues";
        private const string STR_AUTHENTICATION_FAILED = "Authentication Failed";
        private const string STR_AUTHENTICATION_ERROR = "Authentication Error";

        #endregion

        #region Constructor

        /// <summary>
        /// LoginForm
        /// </summary>
        public LoginForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Page Load

        /// <summary>
        /// LoginForm_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginForm_Load(object sender, EventArgs e)
        {
            GlobalData.LoginID = "";
            GlobalData.Password = "";
        }

        #endregion

        #region Button Click Events

        /// <summary>
        /// btnlogin_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnlogin_Click(object sender, EventArgs e)
        {
            domain = WebConstants.STR_DOMAIN;
            loginID = TextBoxLoginID.Text;
            password = TextBoxPassword.Text;

            string environment = System.Configuration.ConfigurationManager.AppSettings["Environment"];
            if (environment == "DEV")
            {
                loginID = "sauragup";
                //loginID = "hemendrb";
                //loginID = "rghai";
                //loginID = "hjiang";
                //loginID = "nandan";
                password = "01Sep$12";
            }

            if (loginID != "" && password != "")
            {
                IsLoggedIn = PerformLDAPAuthentication();

                if (IsLoggedIn)
                {
                    GlobalData.LoginID = loginID;
                    GlobalData.Password = password;
                    this.Close();
                }
                else
                    return;
            }
            else
                MessageBox.Show(STR_PROVIDE_CREDENTIALS);

        }

        /// <summary>
        /// btnOffline_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOffline_Click(object sender, EventArgs e)
        {
            IsLoggedIn = true;
            this.Close();
        }

        #endregion

        #region Private Functions

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
                    MessageBox.Show(STR_AUTHENTICATION_FAILED);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format(STR_AUTHENTICATION_FAILED, ex.Message));
                return false;
            }
        }

        #endregion
    }
}
