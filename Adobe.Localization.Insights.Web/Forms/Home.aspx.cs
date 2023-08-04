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
    public partial class Home : System.Web.UI.Page
    {
        #region Variables

        private DataTable dtScreenLabels;
        private DataTable dtProducts;

        private const string STR_USER_ERROR = "User Error";
        private const string STR_VENDOR_ERROR = "Vendor Error";
        private const string STR_PRODUCT_ERROR = "Product Error";
        private const string STR_PRODUCT_EXISTS = "Product Exists";
        private const string STR_IS_ACTIVE_CHECK = "ISActive = 1";
        private const string CONST_ZERO = "0";

        private const int NUM_EACH_PRODUCT_DISPLAY_HEIGHT = 70;

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

            if (Session[WebConstants.SESSION_USER_ID] == null)
            {
                string localeID = WebConstants.DEF_VAL_LOCALE_ID;
                if (Session[WebConstants.SESSION_LOCALE_ID] != null)
                    localeID = Session[WebConstants.SESSION_LOCALE_ID].ToString();
                Session.RemoveAll();
                Session[WebConstants.SESSION_LOCALE_ID] = localeID;

                try
                {
                    FormsAuthentication.SignOut();
                }
                catch (Exception)
                {
                }
                Response.Redirect(WebConstants.PAGE_HOME);
                //Response.Redirect(WebConstants.PAGE_LOGOUT);
                //LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, STR_USER_ERROR);
            }
            else if (Session[WebConstants.SESSION_VENDOR_ID] == null || Session[WebConstants.SESSION_VENDOR_ID].ToString() == "")
            {
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, STR_VENDOR_ERROR);
            }
            else if (!GetUserProductInformation(Session[WebConstants.SESSION_USER_ID].ToString()))
            {
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, STR_PRODUCT_ERROR);
            }
            //else if (dtProducts.Rows.Count == 1)
            //{
            //    Session[WebConstants.SESSION_PRODUCT_ID] = dtProducts.Rows[0][WebConstants.COL_PRODUCT_ID].ToString();
            //    Session[WebConstants.SESSION_PRODUCT_VERSION_ID] = dtProducts.Rows[0][WebConstants.COL_PRODUCT_VERSION_ID].ToString();
            //    Response.Redirect(WebConstants.PAGE_LOCALIZATION_INSIGHT);
            //}
            else
            {
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, STR_PRODUCT_EXISTS);
                Response.Redirect(WebConstants.PAGE_INDEX);
            }

            //ShowProducts();
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
        private void ShowProducts()
        {
            if (dtProducts != null)
            {
                DataView dvProducts = new DataView(dtProducts);
                DataTable distinctProducts = dvProducts.ToTable(true, WebConstants.COL_PRODUCT_ID, WebConstants.COL_PRODUCT);

                foreach (DataRow drProduct in distinctProducts.Rows)
                {
                    ShowProductsUserControl showProductsControl = (ShowProductsUserControl)this.LoadControl(WebConstants.USER_CONTROL_ROOT_PATH + WebConstants.USER_CONTROL_SHOW_PRODUCTS);

                    showProductsControl.Product = drProduct;
                    showProductsControl.ProductVersionCollections = dtProducts.Select(WebConstants.COL_PRODUCT_ID + " = " + drProduct[WebConstants.COL_PRODUCT_ID].ToString() + " AND " + STR_IS_ACTIVE_CHECK);
                    PanelShowProducts.Controls.Add(showProductsControl);
                }

                AddFooterPadding(distinctProducts.Rows.Count);
            }
            else
                AddFooterPadding(0);
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
