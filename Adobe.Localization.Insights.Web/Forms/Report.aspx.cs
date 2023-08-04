using System;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Adobe.Localization.Insights.Common;
using BO = Adobe.Localization.Insights.BusinessLayer;
using DTO = Adobe.Localization.Insights.Common.DataTransferObjects;

namespace Adobe.Localization.Insights.Web
{
    /// <summary>
    /// Report
    /// </summary>
    public partial class Report : System.Web.UI.Page
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
            DTO.Users userData = new DTO.Users();
            userData.UserID = Session[WebConstants.SESSION_USER_ID].ToString();
            userData.ProductID = Session[WebConstants.SESSION_PRODUCT_ID].ToString();
            userData.ProductVersionID = Session[WebConstants.SESSION_PRODUCT_VERSION_ID].ToString();

            DataTable dtScreenAccess = BO.BusinessObjects.GetUserScreenAccess(userData).Tables[0];

            if (Session[WebConstants.SESSION_USER_ID] == null || Session[WebConstants.SESSION_VENDOR_ID] == null)
            {
                Response.Redirect(WebConstants.PAGE_HOME);
                return;
            }
            else
            {
                if (Session[WebConstants.SESSION_IDENTIFIER] == null)
                {
                    if (dtScreenAccess.Select(WebConstants.COL_SCREEN_IDENTIFIER + "='" + WebConstants.USER_CONTROL_WEEKLY_CONSOLIDATION_SCREEN_IDENTIFIER + "'").Length > 0)
                    {
                        Session[WebConstants.SESSION_IDENTIFIER] = WebConstants.USER_CONTROL_WEEKLY_CONSOLIDATION_SCREEN_IDENTIFIER;
                        userControl = this.LoadControl(WebConstants.USER_CONTROL_ROOT_PATH + WebConstants.USER_CONTROL_VIEW_WEEKLY_CONSOLIDATION);
                    }
                    else if (dtScreenAccess.Select(WebConstants.COL_SCREEN_IDENTIFIER + "='" + WebConstants.USER_CONTROL_WEEKLY_SCREEN_IDENTIFIER + "'").Length > 0)
                    {
                        Session[WebConstants.SESSION_IDENTIFIER] = WebConstants.USER_CONTROL_WEEKLY_SCREEN_IDENTIFIER;
                        userControl = this.LoadControl(WebConstants.USER_CONTROL_ROOT_PATH + WebConstants.USER_CONTROL_SUBMIT_WEEKLY_DATA);
                    }
                    else if (dtScreenAccess.Select(WebConstants.COL_SCREEN_IDENTIFIER + "='" + WebConstants.USER_CONTROL_DAILY_CONSOLIDATION_SCREEN_IDENTIFIER + "'").Length > 0)
                    {
                        Session[WebConstants.SESSION_IDENTIFIER] = WebConstants.USER_CONTROL_DAILY_CONSOLIDATION_SCREEN_IDENTIFIER;
                        userControl = this.LoadControl(WebConstants.USER_CONTROL_ROOT_PATH + WebConstants.USER_CONTROL_VIEW_DAILY_CONSOLIDATION);
                    }
                    else if (dtScreenAccess.Select(WebConstants.COL_SCREEN_IDENTIFIER + "='" + WebConstants.USER_CONTROL_DAILY_SCREEN_IDENTIFIER + "'").Length > 0)
                    {
                        Session[WebConstants.SESSION_IDENTIFIER] = WebConstants.USER_CONTROL_DAILY_SCREEN_IDENTIFIER;
                        userControl = this.LoadControl(WebConstants.USER_CONTROL_ROOT_PATH + WebConstants.USER_CONTROL_SUBMIT_DAILY_EFFORT_DATA);
                    }
                    //else if (dtScreenAccess.Select(WebConstants.COL_SCREEN_IDENTIFIER + "='" + WebConstants.USER_CONTROL_UPLOAD_REPORT_SCREEN_IDENTIFIER + "'").Length > 0)
                    //{
                    //    Session[WebConstants.SESSION_IDENTIFIER] = WebConstants.USER_CONTROL_UPLOAD_REPORT_SCREEN_IDENTIFIER;
                    //    Response.Write("<script>window.open('" + WebConstants.PAGE_UPLOAD_REPORT + "');</script>");
                    //    return;
                    //}
                    else
                    {
                        Response.Redirect(WebConstants.PAGE_HOME);
                        return;
                    }
                }
                else
                {
                    //if (dtScreenAccess.Select(WebConstants.COL_SCREEN_IDENTIFIER + "='" + WebConstants.USER_CONTROL_UPLOAD_REPORT_SCREEN_IDENTIFIER + "'").Length > 0)
                    //{
                    //    Session[WebConstants.SESSION_IDENTIFIER] = WebConstants.USER_CONTROL_UPLOAD_REPORT_SCREEN_IDENTIFIER;
                    //    Response.Write("<script>window.open('" + WebConstants.PAGE_UPLOAD_REPORT + "');</script>");
                    //    return;
                    //}

                    DTO.Screens screenData = new DTO.Screens();
                    screenData.ScreenIdentifier = Session[WebConstants.SESSION_IDENTIFIER].ToString();

                    DataTable dtScreenIdentifier = BO.BusinessObjects.GetScreensDetails(screenData).Tables[0];

                    string userControlName = dtScreenIdentifier.Rows[0][WebConstants.COL_PAGE_NAME].ToString();

                    if (userControlName != "")
                        userControl = this.LoadControl(WebConstants.USER_CONTROL_ROOT_PATH + userControlName);
                    else
                        userControl = this.LoadControl(WebConstants.USER_CONTROL_ROOT_PATH + WebConstants.USER_CONTROL_MESSAGE);
                }
            }

            ControlId.Controls.Add(userControl);
        }

        #endregion
    }
}
