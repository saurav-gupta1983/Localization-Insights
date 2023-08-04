using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Adobe.Localization.Insights.Common;
using COM = Adobe.Localization.Insights.Common.Common;
using BO = Adobe.Localization.Insights.BusinessLayer;
using DTO = Adobe.Localization.Insights.Common.DataTransferObjects;

namespace Adobe.Localization.Insights.Web
{
    public partial class AboutUs : System.Web.UI.Page
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
            screenData.ScreenIdentifier = WebConstants.SCREEN_ABOUTUS;
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
            LabelHeading.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelHeading.Text);
            LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelMessage.Text);
            LabelIE.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelIE.Text);
            LabelIQE.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelIQE.Text);
            LabelIPM.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelIPM.Text);
            LabelTerms.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelTerms.Text);
            LabelI18N.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelI18N.Text);
            LabelL10N.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelL10N.Text);
            LabelT9N.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelT9N.Text);
            LabelG11N.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelG11N.Text);
            LabelWorldReadiness.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelWorldReadiness.Text);
        }

        #endregion

    }
}
