using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Adobe.Localization.Insights.Common;
using COM = Adobe.Localization.Insights.Common.Common;
using BO = Adobe.Localization.Insights.BusinessLayer;
using DTO = Adobe.Localization.Insights.Common.DataTransferObjects;

namespace Adobe.Localization.Insights.Web
{
    /// <summary>
    /// Logout
    /// </summary>
    public partial class Logout : System.Web.UI.Page
    {
        #region Variables

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

            if (!this.IsPostBack)
            {
                FormsAuthentication.SignOut();
                //FormsAuthentication.RedirectToLoginPage();
            }
        }

        #endregion

        #region ButtonClickEvents

        /// <summary>
        /// ButtonLogin_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect(WebConstants.PAGE_HOME);
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// SetScreenAccessAndLabels
        /// </summary>
        /// <param name="userID"></param>
        private void SetScreenAccessAndLabels()
        {
            DTO.Screens screenData = new DTO.Screens();
            screenData.ScreenIdentifier = WebConstants.SCREEN_LOGOUT;
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
            LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelMessage.Text);
            ButtonLogin.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, ButtonLogin.Text);
        }

        #endregion

    }
}
