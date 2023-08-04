using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Adobe.Localization.Insights.Common;
using COM = Adobe.Localization.Insights.Common.Common;
using BO = Adobe.Localization.Insights.BusinessLayer;
using DTO = Adobe.Localization.Insights.Common.DataTransferObjects;

namespace Adobe.Localization.Insights.Web.UserControls
{
    /// <summary>
    /// MessageUserControl
    /// </summary>
    public partial class MessageUserControl : System.Web.UI.UserControl
    {
        #region Variables

        private DTO.Screens screenData;
        private DataTable dtScreenLabels;
        private DataRow[] drChildScreensCol;

        private const int NUM_EACH_CHILD_ROW_DISPLAY_HEIGHT = 20;
        private const string STR_SCREEN_MENU_TITLE = "Menu Title";
        public Color LINK_BUTTON_COLOR_WHITE = Color.AntiqueWhite;

        #endregion

        #region Page Load Event

        /// <summary>
        /// Identifier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            SetScreenAccessAndLabels();
            PopulateData();
            AddFooterPadding(drChildScreensCol.Length);
        }

        #endregion

        #region LinkButton Click Events

        /// <summary>
        /// LinkButtonScreen_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonScreen_Click(object sender, EventArgs e)
        {
            Session[WebConstants.SESSION_IDENTIFIER] = ((LinkButton)sender).CommandArgument;
            Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri);
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// PopulateData
        /// </summary>
        private void PopulateData()
        {
            if (Session[WebConstants.SESSION_CACHE_SCREEN_ACCESS_DATA_TABLE] != null)
            {
                DataTable dtScreenAccess = (DataTable)Session[WebConstants.SESSION_CACHE_SCREEN_ACCESS_DATA_TABLE];
                DataRow drScreenAccess = dtScreenAccess.Select(WebConstants.COL_SCREEN_IDENTIFIER + " = '" + Session[WebConstants.SESSION_IDENTIFIER].ToString() + "'")[0];

                string screenID = drScreenAccess[WebConstants.COL_SCREEN_ID].ToString();
                drChildScreensCol = dtScreenAccess.Select(WebConstants.COL_SCREEN_PARENT_ID + " = " + screenID);

                PanelShowAllChildScreens.Controls.Add(LinkButtonControls());
            }
        }

        /// <summary>
        /// ShowChildScreens
        /// </summary>
        private void ShowChildScreens()
        {
        }

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
            screenData = new DTO.Screens();
            screenData.ScreenIdentifier = Session[WebConstants.SESSION_IDENTIFIER].ToString();

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
            LabelHeading.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, Session[WebConstants.SESSION_IDENTIFIER].ToString(), WebConstants.SCREEN_HEADER_MASTER_DATA);
            LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, Session[WebConstants.SESSION_IDENTIFIER].ToString(), WebConstants.SCREEN_HEADER_DEFINITION);
        }

        /// <summary>
        /// LinkButtonControls
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        private Table LinkButtonControls()
        {
            Table tb = SetTableProperties();

            foreach (DataRow drChildScreen in drChildScreensCol)
            {
                TableRow tr = SetTableRowProperties();
                TableCell tdLabel;

                screenData.ScreenIdentifier = drChildScreen[WebConstants.COL_SCREEN_IDENTIFIER].ToString();
                screenData.LabelCategoryID = WebConstants.MENU_TITLE;
                dtScreenLabels = BO.BusinessObjects.GetScreenLocalizedLabels(screenData).Tables[0];

                tdLabel = SetTableCellProperties(LinkButtons(dtScreenLabels));
                tr.Controls.Add(tdLabel);

                tdLabel = SetTableCellProperties(null);
                tr.Controls.Add(tdLabel);

                tdLabel = SetTableCellProperties(Labels(dtScreenLabels));
                tr.Controls.Add(tdLabel);

                tb.Controls.Add(tr);
            }

            return tb;
        }

        /// <summary>
        /// LinkButtons
        /// </summary>
        /// <param name="drProductVersion"></param>
        /// <returns></returns>
        private LinkButton LinkButtons(DataTable dtScreenLabels)
        {
            LinkButton lb = new LinkButton();
            lb.CausesValidation = true;

            lb.Attributes.Add(WebConstants.STR_ATTRIBUTE_RUNAT, WebConstants.STR_SERVER);
            lb.CommandArgument = dtScreenLabels.Rows[0][WebConstants.COL_SCREEN_IDENTIFIER].ToString();

            lb.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, lb.CommandArgument);

            lb.EnableViewState = true;
            lb.Enabled = true;
            lb.ForeColor = LINK_BUTTON_COLOR_WHITE;
            lb.Click += new EventHandler(this.LinkButtonScreen_Click);

            return lb;
        }

        /// <summary>
        /// Labels
        /// </summary>
        /// <param name="drProjectPhase"></param>
        /// <returns></returns>
        private Label Labels(DataTable dtScreenLabels)
        {
            Label lb = new Label();

            string labelText = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.SCREEN_DEFINITION);

            if (labelText != WebConstants.SCREEN_DEFINITION)
                lb.Text = " - " + labelText;

            lb.ForeColor = LINK_BUTTON_COLOR_WHITE;
            lb.EnableViewState = true;
            lb.Enabled = true;
            return lb;
        }

        /// <summary>
        /// SetTableProperties
        /// </summary>
        /// <param name="width"></param>
        /// <returns></returns>
        private Table SetTableProperties()
        {
            Table tb = new Table();
            tb.Width = System.Web.UI.WebControls.Unit.Percentage(100);
            tb.Height = System.Web.UI.WebControls.Unit.Percentage(100);
            tb.EnableViewState = true;
            return tb;
        }

        /// <summary>
        /// SetTableRowProperties
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        private TableRow SetTableRowProperties()
        {
            TableRow tr = new TableRow();
            tr.Width = System.Web.UI.WebControls.Unit.Percentage(100);
            tr.Height = System.Web.UI.WebControls.Unit.Pixel(NUM_EACH_CHILD_ROW_DISPLAY_HEIGHT);
            tr.EnableViewState = true;
            return tr;
        }

        /// <summary>
        /// SetTableCellProperties
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        private TableCell SetTableCellProperties(Control control)
        {
            TableCell td = new TableCell();
            td.EnableViewState = true;
            if (control != null)
                td.Controls.Add(control);
            else
                td.Width = Unit.Pixel(NUM_EACH_CHILD_ROW_DISPLAY_HEIGHT);

            td.HorizontalAlign = HorizontalAlign.Left;
            return td;
        }

        /// <summary>
        /// AddFooterPadding
        /// </summary>
        private void AddFooterPadding(int productCount)
        {
            int spaceCount = productCount * NUM_EACH_CHILD_ROW_DISPLAY_HEIGHT;

            if (spaceCount < WebConstants.VAL_FOOTER_MAXIMUM_PIXEL)
            {
                Table tb = new Table();
                TableRow tr = new TableRow();
                TableCell tc = new TableCell();
                tc.Height = Unit.Pixel(WebConstants.VAL_FOOTER_MAXIMUM_PIXEL - spaceCount);
                tr.Controls.Add(tc);
                tb.Controls.Add(tr);

                PanelFooterPadding.Controls.Add(tb);
                PanelFooterPadding.Visible = true;
            }
        }

        #endregion
    }
}