using System;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Adobe.Localization.Insights.Web.UserControls;
using Adobe.Localization.Insights.Common;
using COM = Adobe.Localization.Insights.Common.Common;
using BO = Adobe.Localization.Insights.BusinessLayer;
using DTO = Adobe.Localization.Insights.Common.DataTransferObjects;

namespace Adobe.Localization.Insights.Web
{
    /// <summary>
    /// Home
    /// </summary>
    public partial class Index : System.Web.UI.Page
    {
        #region Variables

        private DataTable dtScreenLabels;
        private DataTable dtProducts;

        private const string STR_ADOBE_INDIA_TEAM_NAME = "Adobe India";
        private const string STR_VIEWSTATE_PRODUCTS_DATA = "Products";
        private const string STR_USER_ERROR = "User Error";
        private const string STR_VENDOR_ERROR = "Vendor Error";
        private const string STR_PRODUCT_ERROR = "Product Error";
        private const string STR_PRODUCT_EXISTS = "Product Exists";
        private const string STR_IS_ACTIVE_CHECK = "ISActive = 1";
        private const string CONST_ZERO = "0";

        private const int NUM_EACH_PRODUCT_DISPLAY_HEIGHT = 70;

        private bool isProductEnabled = false;

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

            if (!IsPostBack)
            {
                SetImageEnableDisable();
                ViewState["STR_VIEWSTATE_PRODUCTS_DATA"] = ShowProducts();
            }
            else
                dtProducts = (DataTable)ViewState["STR_VIEWSTATE_PRODUCTS_DATA"];
        }

        #endregion

        #region ImageButtonReport Events

        /// <summary>
        /// ImageButtonReport_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ImageButtonReport_Click(object sender, EventArgs e)
        {
            Session[WebConstants.SESSION_IDENTIFIER] = WebConstants.USER_CONTROL_REPORTING_SCREEN_IDENTIFIER;
            Response.Redirect(WebConstants.PAGE_REPORT);
        }

        /// <summary>
        /// ImageButtonTestManagement_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ImageButtonTestManagement_Click(object sender, EventArgs e)
        {
            Session[WebConstants.SESSION_IDENTIFIER] = WebConstants.USER_CONTROL_PRODUCT_VERSION_SCREEN_IDENTIFIER;
            Response.Redirect(WebConstants.PAGE_LOCALIZATION_INSIGHT);
        }

        #endregion

        #region DropDownList Events

        /// <summary>
        /// DropDownListProducts_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListProducts_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownListProducts.SelectedValue != "0")
            {
                DropDownListProductVersion.Enabled = true;
                Session[WebConstants.SESSION_PRODUCT_ID] = DropDownListProducts.SelectedValue;

                DataView dvProducts = new DataView(dtProducts);
                dvProducts.RowFilter = WebConstants.COL_PRODUCT_ID + "=" + DropDownListProducts.SelectedValue;
                dvProducts.Sort = "isActive DESC, AddedOn DESC, ProductVersion DESC";
                DataTable distinctProductVersions = dvProducts.ToTable(true, WebConstants.COL_PRODUCT_VERSION_ID, WebConstants.COL_PRODUCT_VERSION);

                DropDownListProductVersion.DataSource = distinctProductVersions;
                DropDownListProductVersion.DataTextField = WebConstants.COL_PRODUCT_VERSION;
                DropDownListProductVersion.DataValueField = WebConstants.COL_PRODUCT_VERSION_ID;
                DropDownListProductVersion.DataBind();

                if (dtProducts.Select(WebConstants.COL_PRODUCT_ID + "=" + DropDownListProducts.SelectedValue)[0][WebConstants.COL_PRODUCT_IS_ENABLED].ToString() == "1" || IsAdobeIndiaTeam())
                    isProductEnabled = true;

                if (distinctProductVersions.Rows.Count > 0)
                {
                    if (Session[WebConstants.SESSION_PRODUCT_VERSION_ID] != null)
                        DropDownListProductVersion.SelectedIndex = DropDownListProductVersion.Items.IndexOf(DropDownListProductVersion.Items.FindByValue(Session[WebConstants.SESSION_PRODUCT_VERSION_ID].ToString()));
                    DropDownListProductVersion_OnSelectedIndexChanged(null, null);
                }
            }
            else
            {
                DropDownListProductVersion.Enabled = false;
            }
        }

        /// <summary>
        /// DropDownListProductYear_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListProductVersion_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Session[WebConstants.SESSION_PRODUCT_VERSION_ID] = DropDownListProductVersion.SelectedValue;
            Session[WebConstants.SESSION_VIEW_PRODUCT_VERSION] = true;
            SetImageEnableDisable(true);

        }

        #endregion

        #region Private Functions

        /// <summary>
        /// GetUserProductInformation
        /// </summary>
        /// <param name="userID"></param>
        private bool GetUserProductInformation(string userID)
        {
            DTO.Users userData = new DTO.Users();
            userData.UserID = userID.ToString();
            userData.IsRequiredProductVersion = true;

            if (Session[WebConstants.SESSION_PROJECT_ROLE_ID] != null)
                userData.ProjectRoleID = Session[WebConstants.SESSION_PROJECT_ROLE_ID].ToString();

            dtProducts = BO.BusinessObjects.GetUserProductsPreferred(userData).Tables[0];

            if (dtProducts.Rows.Count > 0)
            {
                StringBuilder productID = new StringBuilder(CONST_ZERO);

                foreach (DataRow dr in dtProducts.Rows)
                    productID.Append("," + dr[WebConstants.COL_PRODUCT_ID].ToString());

                userData.ProductID = productID.ToString();
            }

            DataTable dtUserRoles = BO.BusinessObjects.GetUserProjectRoles(userData).Tables[0];
            DataRow[] drRPM = dtUserRoles.Select(WebConstants.COL_PROJECT_ROLE_CODE + " = '" + WebConstants.STR_PROJECT_ROLE_REPORT_MANAGER_CODE + "'");

            if (drRPM.Length > 0)
                userData.IsManager = true;

            dtProducts = BO.BusinessObjects.GetUserProducts(userData).Tables[0];

            if (dtProducts.Rows.Count > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// ShowProducts
        /// </summary>
        private DataTable ShowProducts()
        {
            if (GetUserProductInformation(Session[WebConstants.SESSION_USER_ID].ToString()))
            {
                DropDownListProductVersion.Enabled = false;

                DataView dvProducts = new DataView(dtProducts);
                DataTable distinctProducts = dvProducts.ToTable(true, WebConstants.COL_PRODUCT_ID, WebConstants.COL_PRODUCT);

                DropDownListProducts.DataSource = distinctProducts;
                DropDownListProducts.DataTextField = WebConstants.COL_PRODUCT;
                DropDownListProducts.DataValueField = WebConstants.COL_PRODUCT_ID;
                DropDownListProducts.DataBind();

                ListItem itemSelectProduct = new ListItem("-- Select Product --", "0");
                DropDownListProducts.Items.Insert(0, itemSelectProduct);

                if (Session[WebConstants.SESSION_PRODUCT_ID] != null)
                {
                    DropDownListProductVersion.Enabled = true;
                    DropDownListProducts.SelectedIndex = DropDownListProducts.Items.IndexOf(DropDownListProducts.Items.FindByValue(Session[WebConstants.SESSION_PRODUCT_ID].ToString()));
                    DropDownListProducts_OnSelectedIndexChanged(null, null);
                }
            }
            else
                AddFooterPadding(0);

            return dtProducts;
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
            DTO.Screens screenData = new DTO.Screens();
            screenData.ScreenIdentifier = WebConstants.SCREEN_HOME;
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
        /// SetImageEnableDisable
        /// </summary>
        /// <param name="isDefault"></param>
        /// <param name="isEnabled"></param>
        private void SetImageEnableDisable(bool isDefault = false)
        {
            ImageButtonReport.Enabled = isDefault;
            ImageButtonTestManagement.Enabled = isDefault && isProductEnabled;
        }

        /// <summary>
        /// IsAdobeIndiaTeam
        /// </summary>
        /// <returns></returns>
        private bool IsAdobeIndiaTeam()
        {
            DTO.Users userData = new DTO.Users();
            userData.VendorID = Session[WebConstants.SESSION_VENDOR_ID].ToString();
            return (BO.BusinessObjects.GetVendorDetails(userData).Tables[0].Rows[0][WebConstants.COL_VENDOR] == STR_ADOBE_INDIA_TEAM_NAME);
        }


        /// <summary>
        /// SetScreenLabels
        /// </summary>
        /// <param name="dtScreenLabels"></param>
        private void SetScreenLabels(DataTable dtScreenLabels)
        {
            LabelHeading.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelHeading.Text);
        }

        /// <summary>
        /// AddFooterPadding
        /// </summary>
        private void AddFooterPadding(int rowsCount)
        {
            int spaceCount = (rowsCount * NUM_EACH_PRODUCT_DISPLAY_HEIGHT) - NUM_EACH_PRODUCT_DISPLAY_HEIGHT;

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
