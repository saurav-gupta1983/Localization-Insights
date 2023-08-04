using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Adobe.Localization.Insights.Common;
using EMAIL = Adobe.Localization.Insights.Common.EmailNotifications;
using COM = Adobe.Localization.Insights.Common.Common;
using BO = Adobe.Localization.Insights.BusinessLayer;
using DTO = Adobe.Localization.Insights.Common.DataTransferObjects;

namespace Adobe.Localization.Insights.Web
{
    public partial class ContactUs : System.Web.UI.Page
    {
        #region Variables

        private DataTable dtScreenLabels;

        private const string STR_SUBJECT_MANDATORY = "Subject Mandatory";
        private const string STR_BODY_MANDATORY = "Body Mandatory";

        #endregion

        #region Page Load event

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            SetScreenAccessAndLabels();
        }

        #endregion

        #region ButtonClickEvents

        /// <summary>
        /// ButtonSendMessage_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSendMessage_Click(object sender, EventArgs e)
        {
            LabelMessage.Visible = false;
            string message;

            if (dtScreenLabels == null)
                PopulateScreenLabels();

            if (TextBoxSubject.Text == "")
                message = COM.GetScreenLocalizedLabel(dtScreenLabels, STR_SUBJECT_MANDATORY);
            else if (TextBoxEmailBody.Text == "")
                message = COM.GetScreenLocalizedLabel(dtScreenLabels, STR_BODY_MANDATORY);
            else
            {
                EMAIL emailNotify = new EMAIL("", Session[WebConstants.SESSION_LOGIN_ID].ToString() + WebConstants.STR_ADOBE_COM);
                if (emailNotify.SendEmail(TextBoxSubject.Text, TextBoxEmailBody.Text))
                    message = String.Format(COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_SUCCESS_EMAIL_MESSAGE), System.Configuration.ConfigurationManager.AppSettings[WebConstants.SETTING_EMAIL_ID]);
                else
                    message = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_EMAIL_MESSAGE);
            }
            LabelMessage.Visible = true;
            LabelMessage.Text = message;
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// SetScreenAccessAndLabels
        /// </summary>
        /// <param name="userID"></param>
        private void SetScreenAccessAndLabels()
        {
            PopulateScreenLabels();
            SetScreenLabels(dtScreenLabels);
        }

        /// <summary>
        /// PopulateScreenLabels
        /// </summary>
        /// <param name="userID"></param>
        private void PopulateScreenLabels()
        {
            DTO.Screens screenData = new DTO.Screens();
            screenData.ScreenIdentifier = WebConstants.SCREEN_CONTACTUS;
            screenData.IsIncludeCommon = true;

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

            dtScreenLabels = BO.BusinessObjects.GetScreenLocalizedLabels(screenData).Tables[0];
        }

        /// <summary>
        /// SetScreenLabels
        /// </summary>
        /// <param name="dtScreenLabels"></param>
        private void SetScreenLabels(DataTable dtScreenLabels)
        {
            LabelHeading.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelHeading.Text);
            LabelFeedback.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelFeedback.Text);
            LabelSubject.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelSubject.Text);
            LabelEmailBody.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelEmailBody.Text);
            ButtonSendMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, ButtonSendMessage.Text);
        }

        #endregion

    }
}
