using System;
using System.Web;
using System.Web.UI;
using System.Data;
using Adobe.Localization.Insights.Common;
using BO = Adobe.Localization.Insights.BusinessLayer;
using DTO = Adobe.Localization.Insights.Common.DataTransferObjects;

namespace Adobe.Localization.Insights.Web
{
    /// <summary>
    /// LocalizationInsight
    /// </summary>
    public partial class LocalizationInsight : System.Web.UI.Page
    {
        #region Variables

        private Control userControl;

        private const string STR_IS_PRODUCT_TYPE = "1";

        #endregion

        #region Page Load event

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[WebConstants.SESSION_USER_ID] == null || Session[WebConstants.SESSION_VENDOR_ID] == null)
            {
                Response.Redirect(WebConstants.PAGE_HOME);
                return;
            }

            if (Session[WebConstants.SESSION_IDENTIFIER] == null)
            {
                if (Session[WebConstants.SESSION_PROJECT_PHASE_ID] != null)
                    userControl = this.LoadControl(WebConstants.USER_CONTROL_ROOT_PATH + WebConstants.USER_CONTROL_MAINTAIN_PROJECT_PHASE);
                else if (Session[WebConstants.SESSION_PRODUCT_VERSION_ID] != null)
                    userControl = this.LoadControl(WebConstants.USER_CONTROL_ROOT_PATH + WebConstants.USER_CONTROL_VIEW_PRODUCT_VERSION);
                else if (Session[WebConstants.SESSION_PRODUCT_ID] != null)
                    userControl = this.LoadControl(WebConstants.USER_CONTROL_ROOT_PATH + WebConstants.USER_CONTROL_PRODUCT_SUMMARY);
                else
                    Response.Redirect(WebConstants.PAGE_HOME);
            }
            else
            {
                if (Session[WebConstants.SESSION_IDENTIFIER].ToString() == WebConstants.USER_CONTROL_PRODUCT_VERSION_SCREEN_IDENTIFIER)
                {
                    if (Session[WebConstants.SESSION_PRODUCT_VERSION_ID] != null && Session[WebConstants.SESSION_VIEW_PRODUCT_VERSION] != null)
                    {
                        Session.Remove(WebConstants.SESSION_DATA_TREEVIEW_NODE_COLLECTION);
                        userControl = this.LoadControl(WebConstants.USER_CONTROL_ROOT_PATH + WebConstants.USER_CONTROL_VIEW_PRODUCT_VERSION);
                    }
                    else
                        userControl = this.LoadControl(WebConstants.USER_CONTROL_ROOT_PATH + WebConstants.USER_CONTROL_MAINTAIN_PRODUCT_VERSION);
                }
                else
                {
                    if (Session[WebConstants.SESSION_VIEW_PRODUCT_VERSION] != null)
                        Session.Remove(WebConstants.SESSION_VIEW_PRODUCT_VERSION);

                    DTO.Screens screenData = new DTO.Screens();
                    screenData.ScreenIdentifier = Session[WebConstants.SESSION_IDENTIFIER].ToString();

                    DataTable dtScreenIdentifier = BO.BusinessObjects.GetScreensDetails(screenData).Tables[0];

                    if (dtScreenIdentifier.Select(WebConstants.COL_SCREEN_IS_PRODUCT_TYPE + " = " + STR_IS_PRODUCT_TYPE).Length > 0 && Session[WebConstants.SESSION_PRODUCT_ID] == null)
                    {
                        Session.Remove(WebConstants.SESSION_IDENTIFIER);
                        Response.Redirect(WebConstants.PAGE_HOME);
                    }
                    else
                    {
                        string userControlName = dtScreenIdentifier.Rows[0][WebConstants.COL_PAGE_NAME].ToString();

                        if (userControlName != "")
                            userControl = this.LoadControl(WebConstants.USER_CONTROL_ROOT_PATH + userControlName);
                        else
                            userControl = this.LoadControl(WebConstants.USER_CONTROL_ROOT_PATH + WebConstants.USER_CONTROL_MESSAGE);
                    }
                }
            }

            ControlId.Controls.Add(userControl);
        }

        #endregion
    }
}
