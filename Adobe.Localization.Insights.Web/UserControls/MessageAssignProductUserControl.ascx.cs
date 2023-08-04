using System;
using Adobe.Localization.Insights.Common;

namespace Adobe.Localization.Insights.Web.UserControls
{
    /// <summary>
    /// MessageUserControl
    /// </summary>
    public partial class MessageAssignProductUserControl : System.Web.UI.UserControl
    {
        #region Page Load Event

        protected void Page_Load(object sender, EventArgs e)
        {

            string message = "";

            if (Session["Identifier"] != null)
            {
                message = Session["Identifier"].ToString();
            }

            if (message == WebConstants.WSR)
            {
                PanelWSR.Visible = true;
            }
            else if (message == WebConstants.METRICS || message == WebConstants.STATISTICS)
            {
                PanelMetrics.Visible = true;
            }
            else
            {
                PanelWelcome.Visible = true;
            }
        }

        #endregion
    }
}