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
    /// ShowProductSummaryUserControl
    /// </summary>
    public partial class ShowProductSummaryUserControl : System.Web.UI.UserControl
    {
        #region Variables

        private DataTable dtScreenLabels;
        private DataTable dtProducts;

        private Color LINK_BUTTON_COLOR_RED = Color.DarkSalmon;
        private Color LINK_BUTTON_COLOR_WHITE = Color.AntiqueWhite;
        private Color LINK_BUTTON_COLOR_BLUE = Color.CornflowerBlue;
        private Color LINK_BUTTON_COLOR_YELLOW = Color.Yellow;
        private Color LINK_BUTTON_COLOR_GREEN = Color.ForestGreen;

        private int tdWidthCount = 0;
        private const int CONST_ZERO = 0;
        private const int CONST_DISPLAY_LABEL_FACTOR = 3;
        private const int CONST_MAX_PER = 100;
        private const int NUM_EACH_GRID_ROW_DISPLAY_HEIGHT = 20;
        private const string STR_PRODUCT_VERSION_SORT_CRITERIA = "ProductVersionID DESC";
        private const string STR_PERCENT = " %";
        private const string STR_PRODUCT_ACTIVE = "1";
        private const string STR_TEXT_BOLD_OPEN_TAG = "<B>";
        private const string STR_TEXT_BOLD_CLOSED_TAG = "</B>";

        #endregion

        #region Page Load Event

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            PopulateProductInformation();
            PopulateLatestProductVersion();
            PopulateProjectPhases();
            PopulateActiveVersions();
            PopulateNonActiveVersions();

            SetScreenAccessAndLabels();
        }

        #endregion

        #region LinkButton Click Events

        /// <summary>
        /// LinkButtonProduct_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonProduct_Click(object sender, EventArgs e)
        {
            Session.Remove(WebConstants.SESSION_PRODUCT_VERSION_ID);
            Response.Redirect(WebConstants.PAGE_LOCALIZATION_INSIGHT);
        }

        /// <summary>
        /// LinkButtonSelectedProductVersion_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonSelectedProductVersion_Click(object sender, EventArgs e)
        {
            Session[WebConstants.SESSION_PRODUCT_VERSION_ID] = LabelProductVersionID.Text;
            //Session[WebConstants.SESSION_IDENTIFIER] = WebConstants.USER_CONTROL_PRODUCT_VERSION_SCREEN_IDENTIFIER;
            Response.Redirect(WebConstants.PAGE_LOCALIZATION_INSIGHT);
        }

        /// <summary>
        /// LinkButtonProductVersion_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonProductVersion_Click(object sender, EventArgs e)
        {
            Session[WebConstants.SESSION_PRODUCT_VERSION_ID] = ((LinkButton)sender).CommandArgument;
            Response.Redirect(WebConstants.PAGE_LOCALIZATION_INSIGHT);
        }

        /// <summary>
        /// LinkButtonSelectedProjectPhase_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonSelectedProjectPhase_Click(object sender, EventArgs e)
        {
            Session[WebConstants.SESSION_PRODUCT_VERSION_ID] = LabelProductVersionID.Text;
            Session[WebConstants.SESSION_PROJECT_PHASE_ID] = ((LinkButton)sender).CommandArgument;
            Session[WebConstants.SESSION_IDENTIFIER] = WebConstants.USER_CONTROL_PROJECT_PHASE_SCREEN_IDENTIFIER;
            Response.Redirect(WebConstants.PAGE_LOCALIZATION_INSIGHT);
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// PopulateProductInformation
        /// </summary>
        private void PopulateProductInformation()
        {
            DTO.Product productData = new DTO.Product();
            productData.ProductID = Session[WebConstants.SESSION_PRODUCT_ID].ToString();

            dtProducts = BO.BusinessObjects.GetProductVersion(productData).Tables[0];

            DataView dvProducts = new DataView(dtProducts);
            dvProducts.Sort = STR_PRODUCT_VERSION_SORT_CRITERIA;

            dtProducts = dvProducts.ToTable();

            if (dtProducts.Rows.Count == 0)
                LinkButtonProduct.Text = BO.BusinessObjects.GetProducts(productData).Tables[0].Rows[0][WebConstants.COL_PRODUCT].ToString();
            else
                LinkButtonProduct.Text = dtProducts.Rows[0][WebConstants.COL_PRODUCT].ToString();
        }

        /// <summary>
        /// PopulateLatestProductVersion
        /// </summary>
        private void PopulateLatestProductVersion()
        {
            LabelProductVersionID.Text = "";
            if (Session[WebConstants.SESSION_PRODUCT_VERSION_ID] != null)
                LabelProductVersionID.Text = Session[WebConstants.SESSION_PRODUCT_VERSION_ID].ToString();
            else if (dtProducts.Rows.Count > 0)
                LabelProductVersionID.Text = dtProducts.Rows[0][WebConstants.COL_PRODUCT_VERSION_ID].ToString();

            if (LabelProductVersionID.Text != "")
            {
                DataRow drProductVersion = dtProducts.Select(WebConstants.COL_PRODUCT_VERSION_ID + " = " + LabelProductVersionID.Text)[0];

                if (drProductVersion[WebConstants.COL_PRODUCT_VERSION_CODE].ToString() != "")
                    LinkButtonSelectedProductVersion.Text = drProductVersion[WebConstants.COL_PRODUCT_VERSION_CODE].ToString() + " ( " + drProductVersion[WebConstants.COL_PRODUCT_VERSION].ToString() + " ) ";
                else
                    LinkButtonSelectedProductVersion.Text = drProductVersion[WebConstants.COL_PRODUCT_VERSION].ToString();
            }
            else
                PanelLatestProductVersion.Visible = false;
        }

        /// <summary>
        /// PopulateProjectPhases
        /// </summary>
        private void PopulateProjectPhases()
        {
            DTO.ProjectPhases phaseData = new DTO.ProjectPhases();
            phaseData.ProductVersionID = LabelProductVersionID.Text;

            DataTable dtProjectPhases = BO.BusinessObjects.GetProjectPhaseSummary(phaseData).Tables[0];

            if (dtProjectPhases.Rows.Count > 0)
                PanelShowProjectPhases.Controls.Add(ProjectPhaseControls(dtProjectPhases));
            else
                PanelPhasesNotCreated.Visible = true;
        }

        /// <summary>
        /// PopulateActiveVersions
        /// </summary>
        private void PopulateActiveVersions()
        {
            DataRow[] drActiveVersions = dtProducts.Select(WebConstants.COL_PRODUCT_VERSION_ACTIVE + " = " + STR_PRODUCT_ACTIVE);

            if (drActiveVersions.Length > 0)
                PanelActiveVersionsList.Controls.Add(ProductVersionControls(drActiveVersions));
            else
                PanelNoActiveVersions.Visible = true;

        }

        /// <summary>
        /// PopulateNonActiveVersions
        /// </summary>
        private void PopulateNonActiveVersions()
        {
            DataRow[] drNonActiveVersions = dtProducts.Select(WebConstants.COL_PRODUCT_VERSION_ACTIVE + " <> " + STR_PRODUCT_ACTIVE);

            if (drNonActiveVersions.Length > 0)
                PanelNonActiveVersionList.Controls.Add(ProductVersionControls(drNonActiveVersions));
            else
                PanelNoNonActiveVersions.Visible = true;
        }

        /// <summary>
        /// ProductVersionControls
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        private Table ProductVersionControls(DataRow[] drProductVersionCol)
        {
            Table tb = SetTableProperties();
            TableRow tr = SetTableRowProperties();
            tdWidthCount = 100 / drProductVersionCol.Length;

            foreach (DataRow drProductVersion in drProductVersionCol)
            {
                TableCell tdLabel;

                tdLabel = SetTableCellProperties(LinkButtonsProductVersion(drProductVersion));
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
        private LinkButton LinkButtonsProductVersion(DataRow drProductVersion)
        {
            LinkButton lb = new LinkButton();
            lb.CausesValidation = true;
            lb.Attributes.Add(WebConstants.STR_ATTRIBUTE_RUNAT, WebConstants.STR_SERVER);
            lb.CommandArgument = drProductVersion[WebConstants.COL_PRODUCT_VERSION_ID].ToString();

            if (drProductVersion[WebConstants.COL_PRODUCT_VERSION_CODE].ToString() != "")
                lb.Text = drProductVersion[WebConstants.COL_PRODUCT_VERSION_CODE].ToString() + " ( " + drProductVersion[WebConstants.COL_PRODUCT_VERSION].ToString() + " ) ";
            else
                lb.Text = drProductVersion[WebConstants.COL_PRODUCT_VERSION].ToString();

            lb.EnableViewState = true;
            lb.Enabled = true;
            lb.ForeColor = LINK_BUTTON_COLOR_WHITE;
            lb.Click += new EventHandler(this.LinkButtonProductVersion_Click);

            return lb;
        }

        /// <summary>
        /// drProductVersion
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        private Table ProjectPhaseControls(DataTable dtProjectPhases)
        {
            Table tb = SetTableProperties();

            foreach (DataRow drProjectPhase in dtProjectPhases.Rows)
            {
                int tcCount = 0;
                int remCount = 0;

                if (drProjectPhase[WebConstants.COL_PROJECT_PHASE_TOTAL_COUNT].ToString() != "")
                    tcCount = Convert.ToInt32(drProjectPhase[WebConstants.COL_PROJECT_PHASE_TOTAL_COUNT].ToString());

                remCount = tcCount;

                if (drProjectPhase[WebConstants.COL_PROJECT_PHASE_TOTAL_EXECUTED].ToString() != "")
                    remCount -= Convert.ToInt32(drProjectPhase[WebConstants.COL_PROJECT_PHASE_TOTAL_EXECUTED].ToString());

                if (drProjectPhase[WebConstants.COL_PROJECT_PHASE_TOTAL_NA].ToString() != "")
                    remCount -= Convert.ToInt32(drProjectPhase[WebConstants.COL_PROJECT_PHASE_TOTAL_NA].ToString());

                Color displayColor = LINK_BUTTON_COLOR_WHITE;
                double displayValue = 0;

                if (tcCount == CONST_ZERO)
                {
                    displayColor = LINK_BUTTON_COLOR_WHITE;
                    displayValue = tcCount;
                }
                else if (remCount == CONST_ZERO)
                {
                    displayColor = LINK_BUTTON_COLOR_GREEN;
                    displayValue = CONST_MAX_PER;
                }
                else if (tcCount == remCount)
                {
                    displayColor = LINK_BUTTON_COLOR_BLUE;
                    displayValue = CONST_ZERO;
                }
                else if (remCount < CONST_ZERO)
                {
                    displayColor = LINK_BUTTON_COLOR_RED;
                    displayValue = CONST_ZERO;
                }
                else
                {
                    displayColor = LINK_BUTTON_COLOR_YELLOW;
                    displayValue = ((tcCount - remCount) * CONST_MAX_PER) / tcCount;
                }

                TableRow tr = SetTableRowProperties();
                TableCell tdLabel;
                tdLabel = SetTableCellProperties(LinkButtonProjectPhases(drProjectPhase, displayColor));
                tr.Controls.Add(tdLabel);

                tdLabel = SetTableCellProperties(LabelPhaseDates(drProjectPhase, LINK_BUTTON_COLOR_WHITE));
                tr.Controls.Add(tdLabel);

                tdLabel = SetTableCellProperties(null);
                tr.Controls.Add(tdLabel);

                tdLabel = SetTableCellProperties(LabelProjectPhases(drProjectPhase, displayValue, displayColor));
                tdLabel.HorizontalAlign = HorizontalAlign.Left;
                tr.Controls.Add(tdLabel);

                tb.Controls.Add(tr);
            }

            return tb;
        }

        /// <summary>
        /// LinkButtons
        /// </summary>
        /// <param name="drProjectPhase"></param>
        /// <returns></returns>
        private LinkButton LinkButtonProjectPhases(DataRow drProjectPhase, Color displayColor)
        {
            LinkButton lb = new LinkButton();

            lb.CausesValidation = true;
            lb.Text = STR_TEXT_BOLD_OPEN_TAG + drProjectPhase[WebConstants.COL_PROJECT_PHASE].ToString() + STR_TEXT_BOLD_CLOSED_TAG;
            lb.Attributes.Add(WebConstants.STR_ATTRIBUTE_RUNAT, WebConstants.STR_SERVER);
            lb.CommandArgument = drProjectPhase[WebConstants.COL_PROJECT_PHASE_ID].ToString();
            lb.EnableViewState = true;
            lb.Enabled = true;
            lb.ForeColor = displayColor;
            lb.Click += new EventHandler(this.LinkButtonSelectedProjectPhase_Click);

            return lb;
        }

        /// <summary>
        /// LabelPhaseDates
        /// </summary>
        /// <param name="drProjectPhase"></param>
        /// <returns></returns>
        private Label LabelPhaseDates(DataRow drProjectPhase, Color displayColor)
        {
            Label lb = new Label();

            string date = ((DateTime)drProjectPhase[WebConstants.COL_PROJECT_PHASE_START_DATE]).ToShortDateString() + " - ";
            if (drProjectPhase[WebConstants.COL_PROJECT_PHASE_END_DATE].ToString() != "")
                date = date + ((DateTime)drProjectPhase[WebConstants.COL_PROJECT_PHASE_END_DATE]).ToShortDateString();

            lb.Text = " ( " + date + " ) ";
            lb.ForeColor = displayColor;
            lb.EnableViewState = true;
            lb.Enabled = true;
            return lb;
        }

        /// <summary>
        /// LabelProjectPhases
        /// </summary>
        /// <param name="drProjectPhase"></param>
        /// <returns></returns>
        private Label LabelProjectPhases(DataRow drProjectPhase, double displayValue, Color displayColor)
        {
            Label lb = new Label();
            lb.Attributes.Add(WebConstants.STR_ATTRIBUTE_RUNAT, WebConstants.STR_SERVER);
            lb.Attributes.Add(WebConstants.STR_ATTRIBUTE_STYLE, WebConstants.STR_ATTRIBUTE_STYLE_VALUE_ALIGN_CENTER);
            lb.BackColor = displayColor;
            lb.Text = displayValue.ToString() + STR_PERCENT;
            lb.Font.Bold = true;
            lb.EnableViewState = true;
            lb.Enabled = true;
            if (displayValue == CONST_ZERO)
                lb.Width = Unit.Pixel(CONST_ZERO);
            else
                lb.Width = Unit.Pixel(Convert.ToInt32(displayValue * CONST_DISPLAY_LABEL_FACTOR));
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
                int width = tdWidthCount;
                td.Width = System.Web.UI.WebControls.Unit.Percentage(width);
            }
            else
            {
                td.Width = System.Web.UI.WebControls.Unit.Pixel(20);
            }
            td.HorizontalAlign = HorizontalAlign.Left;
            //td.Height = System.Web.UI.WebControls.Unit.Pixel(20);
            return td;
        }

        /// <summary>
        /// SetScreenAccessAndLabels
        /// </summary>
        /// <param name="userID"></param>
        private void SetScreenAccessAndLabels()
        {
            if (Session[WebConstants.SESSION_IDENTIFIER] != null)
                Session.Remove(WebConstants.SESSION_IDENTIFIER);
            PopulateScreenLabels();
            SetScreenLabels(dtScreenLabels);

            if (!PanelLatestProductVersion.Visible)
                AddFooterPadding(5);
        }

        /// <summary>
        /// PopulateScreenLabels
        /// </summary>
        /// <param name="userID"></param>
        private void PopulateScreenLabels()
        {
            DTO.Screens screenData = new DTO.Screens();
            screenData.ScreenIdentifier = WebConstants.SCREEN_MAINTAIN_PRODUCTS;
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
            LabelViewDetails.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelViewDetails.Text);
            LabelPhasesNotCreated.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelPhasesNotCreated.Text);
            LabelActiveVersions.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelActiveVersions.Text);
            LabelNoActiveVersions.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelNoActiveVersions.Text);
            LabelSelectActive.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelSelectActive.Text);
            LabelNonActiveVersions.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelNonActiveVersions.Text);
            LabelNoNonActiveVersions.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelNoNonActiveVersions.Text);
            LabelSelectNonActive.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelSelectNonActive.Text);
        }

        /// <summary>
        /// AddFooterPadding
        /// </summary>
        private void AddFooterPadding(int rowsCount)
        {
            int spaceCount = (rowsCount * NUM_EACH_GRID_ROW_DISPLAY_HEIGHT);

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