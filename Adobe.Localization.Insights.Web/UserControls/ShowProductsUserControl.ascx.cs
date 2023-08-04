using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using Adobe.Localization.Insights.Common;
using COM = Adobe.Localization.Insights.Common.Common;
using BO = Adobe.Localization.Insights.BusinessLayer;
using DTO = Adobe.Localization.Insights.Common.DataTransferObjects;

namespace Adobe.Localization.Insights.Web.UserControls
{
    /// <summary>
    /// ShowProductsUserControl
    /// </summary>
    public partial class ShowProductsUserControl : System.Web.UI.UserControl
    {
        #region Variables

        private DataTable dtScreenLabels;
        private DataRow drProduct;
        private DataRow[] drProductVersionCol;

        public Color LINK_BUTTON_COLOR_WHITE = Color.AntiqueWhite;
        int versionLabelWidth = 0;

        #endregion

        #region Page Load Event

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            LinkButtonProductHeading.Text = drProduct[WebConstants.COL_PRODUCT].ToString();
            LinkButtonProductHeading.CommandArgument = drProduct[WebConstants.COL_PRODUCT_ID].ToString();
            LabelProductID.Text = drProduct[WebConstants.COL_PRODUCT_ID].ToString();

            if (drProductVersionCol != null && drProductVersionCol.Length > 0 && drProductVersionCol[0][WebConstants.COL_PRODUCT_VERSION_ID].ToString() != "")
                PanelProductVersions.Controls.Add(LinkButtonControls());
            else
            {
                SetScreenLabels();
                LabelNoVersionsAvailable.Visible = true;
            }
        }

        #endregion

        #region LinkButton Click Events

        /// <summary>
        /// LinkButtonProductHeading_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonProductHeading_Click(object sender, EventArgs e)
        {
            Session[WebConstants.SESSION_PRODUCT_ID] = LabelProductID.Text;

            if (Session[WebConstants.SESSION_IDENTIFIER] != null)
                Session.Remove(WebConstants.SESSION_IDENTIFIER);
            if (Session[WebConstants.SESSION_PRODUCT_VERSION_ID] != null)
                Session.Remove(WebConstants.SESSION_PRODUCT_VERSION_ID);
            if (Session[WebConstants.SESSION_PROJECT_PHASE_ID] != null)
                Session.Remove(WebConstants.SESSION_PROJECT_PHASE_ID);
            Response.Redirect(WebConstants.PAGE_LOCALIZATION_INSIGHT);
        }

        /// <summary>
        /// LinkButtonProductVersion_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonProductVersion_Click(object sender, EventArgs e)
        {
            Session[WebConstants.SESSION_PRODUCT_ID] = LabelProductID.Text;
            Session[WebConstants.SESSION_PRODUCT_VERSION_ID] = ((LinkButton)sender).CommandArgument;

            if (Session[WebConstants.SESSION_IDENTIFIER] != null)
                Session.Remove(WebConstants.SESSION_IDENTIFIER);
            if (Session[WebConstants.SESSION_PROJECT_PHASE_ID] != null)
                Session.Remove(WebConstants.SESSION_PROJECT_PHASE_ID);

            Response.Redirect(WebConstants.PAGE_LOCALIZATION_INSIGHT);
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// LinkButtonControls
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        private Table LinkButtonControls()
        {
            Table tb = SetTableProperties();
            TableRow tr = SetTableRowProperties();
            versionLabelWidth = 100 / (drProductVersionCol.Length);

            foreach (DataRow drProductVersion in drProductVersionCol)
            {
                TableCell tdLabel;

                tdLabel = SetTableCellProperties(LinkButtons(drProductVersion));
                tr.Controls.Add(tdLabel);

                tdLabel = SetTableCellProperties(null);
                tr.Controls.Add(tdLabel);
            }

            tb.Controls.Add(tr);

            return tb;
        }

        /// <summary>
        /// LinkButtons
        /// </summary>
        /// <param name="drProductVersion"></param>
        /// <returns></returns>
        private LinkButton LinkButtons(DataRow drProductVersion)
        {
            LinkButton lb = new LinkButton();
            lb.CausesValidation = true;

            lb.Attributes.Add(WebConstants.STR_ATTRIBUTE_RUNAT, WebConstants.STR_SERVER);
            //lb.CommandName = "lb_Click";
            lb.CommandArgument = drProductVersion[WebConstants.COL_PRODUCT_VERSION_ID].ToString();

            if (drProductVersion[WebConstants.COL_PRODUCT_VERSION_CODE].ToString() != "")
                lb.Text = drProductVersion[WebConstants.COL_PRODUCT_VERSION_CODE].ToString() + " ( " + drProductVersion[WebConstants.COL_PRODUCT_VERSION].ToString() + " ) ";
            else
                lb.Text = drProductVersion[WebConstants.COL_PRODUCT_VERSION].ToString();

            lb.EnableViewState = true;
            lb.Enabled = true;
            lb.ForeColor = LINK_BUTTON_COLOR_WHITE;
            lb.Click += new EventHandler(this.LinkButtonProductVersion_Click);
            lb.Width = Unit.Percentage(versionLabelWidth);

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
            tr.Height = System.Web.UI.WebControls.Unit.Percentage(100);
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
            {
                td.Controls.Add(control);
                td.Width = System.Web.UI.WebControls.Unit.Percentage(versionLabelWidth);
            }
            else
            {
                td.Width = System.Web.UI.WebControls.Unit.Pixel(10);
            }
            td.HorizontalAlign = HorizontalAlign.Left;
            //td.Height = System.Web.UI.WebControls.Unit.Pixel(20);
            return td;
        }

        #endregion

        #region Properties

        /// <summary>
        /// ProductVersions
        /// </summary>
        [Browsable(true)]
        [Category("ProductVersions")]
        [Description("Assign ProductVersions")]
        public DataRow[] ProductVersionCollections
        {
            set
            {
                drProductVersionCol = value;
            }
        }

        /// <summary>
        /// Product
        /// </summary>
        [Browsable(true)]
        [Category("Product")]
        [Description("Assign Product")]
        public DataRow Product
        {
            set
            {
                drProduct = value;
            }
        }

        #endregion

        #region Screen Access and Labels

        /// <summary>
        /// PopulateScreenLabels
        /// </summary>
        /// <param name="userID"></param>
        private void PopulateScreenLabels()
        {
            DTO.Screens screenData = new DTO.Screens();
            screenData.ScreenIdentifier = WebConstants.USER_CONTROL_SHOW_PRODUCTS;
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

            screenData.LocaleID = COM.GetLocaleForScreenLabels(screenData.LocaleID, dtLocales)[WebConstants.COL_LOCALE_ID].ToString();

            dtScreenLabels = BO.BusinessObjects.GetScreenLocalizedLabels(screenData).Tables[0];
        }

        /// <summary>
        /// SetScreenLabels
        /// </summary>
        /// <param name="dtScreenLabels"></param>
        private void SetScreenLabels()
        {
            PopulateScreenLabels();

            LabelNoVersionsAvailable.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelNoVersionsAvailable.Text);

        }

        #endregion
    }
}