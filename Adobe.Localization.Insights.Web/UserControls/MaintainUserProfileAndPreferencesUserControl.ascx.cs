using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;
using Adobe.Localization.Insights.Common;
using COM = Adobe.Localization.Insights.Common.Common;
using BO = Adobe.Localization.Insights.BusinessLayer;
using DTO = Adobe.Localization.Insights.Common.DataTransferObjects;

namespace Adobe.Localization.Insights.Web.UserControls
{
    /// <summary>
    /// MaintainUserProfileAndPreferencesUserControl
    /// </summary>
    public partial class MaintainUserProfileAndPreferencesUserControl : System.Web.UI.UserControl
    {
        #region Variables

        private const int CONST_ZERO = 0;

        private DataTable dtUserProductsPreferred;
        private DataTable dtScreenLabels;
        private DTO.Header dataHeader = new DTO.Header();

        private const string STR_SPACE = "&nbsp;";
        private const string STR_ZERO = "0";
        private const string STR_MANAGER_LOGINID_CHECK = "Manager LoginID Check";

        private const int NUM_EACH_GRID_ROW_DISPLAY_HEIGHT = 20;

        #endregion

        #region Page Load Event

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            GetScreenAccess();
            SetScreenLabels();

            PopulateData();

            SetScreenAccess();
        }

        #endregion

        #region Button Click Events

        /// <summary>
        /// ButtonSaveUserInformation_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSaveUserInformation_Click(object sender, EventArgs e)
        {
            DTO.Users userData = new DTO.Users();
            userData.DataHeader = dataHeader;
            userData.UserID = Session[WebConstants.SESSION_USER_ID].ToString();
            //ManagerLoginID
            if (TextBoxManagerLoginID.Text != "")
            {
                DTO.Users managerData = new DTO.Users();
                managerData.LoginID = TextBoxManagerLoginID.Text;
                DataTable dtGetManagerUser = BO.BusinessObjects.GetUserDetails(managerData).Tables[0];
                dtGetManagerUser.Rows.RemoveAt(0);

                if (dtGetManagerUser.Rows.Count == 1)
                    userData.ManagerUserID = dtGetManagerUser.Rows[0][WebConstants.COL_USER_ID].ToString();
                else
                {
                    LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, STR_MANAGER_LOGINID_CHECK); ;
                    return;
                }

            }

            userData.FirstName = TextBoxFirstName.Text;
            userData.LastName = TextBoxLastName.Text;
            userData.NickName = TextBoxNickName.Text;
            userData.EmailID = TextBoxEmailID.Text;
            userData.AlternateEmailID = TextBoxAlternateEmailID.Text;
            userData.ContactNo = TextBoxContactNo.Text;

            userData.UserCollection = new ArrayList();
            foreach (GridViewRow row in GridViewProducts.Rows)
            {
                DTO.Users userProdPref = new DTO.Users();

                Label LabelProductID = (Label)row.FindControl(WebConstants.CONTROL_LABEL_PRODUCT_ID);
                Label LabelUserProductPreferenceID = (Label)row.FindControl(WebConstants.CONTROL_LABEL_USER_PRODUCT_PREFERENCE_ID);
                CheckBox CheckBoxProductSelected = (CheckBox)row.FindControl(WebConstants.CONTROL_CHECKBOX_PRODUCTS_SELECTED);

                userProdPref.ProductPrefID = LabelUserProductPreferenceID.Text;
                userProdPref.ProductID = LabelProductID.Text;
                userProdPref.IsSelected = CheckBoxProductSelected.Checked.ToString();

                userData.UserCollection.Add(userProdPref);
            }

            if (BO.BusinessObjects.UpdateProfileAndPreferences(userData))
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_SUCCESS_SAVE_MESSAGE);
            else
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_SAVE_MESSAGE);

            PopulateData();
        }

        #endregion

        #region User Profile Information

        /// <summary>
        /// PopulateUserProfileInformation
        /// </summary>
        private void PopulateUserProfileInformation()
        {
            DTO.Users userData = new DTO.Users();
            userData.UserID = Session[WebConstants.SESSION_USER_ID].ToString();

            DataRow users = BO.BusinessObjects.GetUserDetails(userData).Tables[0].Rows[1];

            LabelLoginIDValue.Text = users[WebConstants.COL_LOGIN_ID].ToString();
            LabelUserIDValue.Text = users[WebConstants.COL_USER_ID].ToString();
            LabelVendorValue.Text = users[WebConstants.COL_VENDOR].ToString();
            LabelVendorID.Text = users[WebConstants.COL_VENDOR_ID].ToString();
            TextBoxFirstName.Text = users[WebConstants.COL_FIRST_NAME].ToString();
            TextBoxLastName.Text = users[WebConstants.COL_LAST_NAME].ToString();
            TextBoxNickName.Text = users[WebConstants.COL_NICK_NAME].ToString();
            TextBoxEmailID.Text = users[WebConstants.COL_EMAIL_ID].ToString();
            TextBoxAlternateEmailID.Text = users[WebConstants.COL_ALTERNATE_EMAIL_ID].ToString();
            TextBoxContactNo.Text = users[WebConstants.COL_CONTACT_NO].ToString();
            TextBoxManagerLoginID.Text = users[WebConstants.COL_MANAGER_LOGIN_ID].ToString();
        }

        #endregion

        #region User Products

        /// <summary>
        /// PopulateUserProductPreferences
        /// </summary>
        private void PopulateUserProductPreferences()
        {
            DTO.Users userData = new DTO.Users();
            userData.UserID = Session[WebConstants.SESSION_USER_ID].ToString();

            if (Session[WebConstants.SESSION_PROJECT_ROLE_ID] != null)
                userData.ProjectRoleID = Session[WebConstants.SESSION_PROJECT_ROLE_ID].ToString();

            DataTable dtUserRoles = BO.BusinessObjects.GetUserProjectRoles(userData).Tables[0];
            DataRow[] drRPM = dtUserRoles.Select(WebConstants.COL_PROJECT_ROLE_CODE + " = '" + WebConstants.STR_PROJECT_ROLE_REPORT_MANAGER_CODE + "'");

            if (drRPM.Length > 0)
                userData.IsManager = true;

            DataTable dtUserProducts = BO.BusinessObjects.GetUserProducts(userData).Tables[0];

            DataView dvProducts = new DataView(dtUserProducts);
            dtUserProducts = dvProducts.ToTable(true, WebConstants.COL_PRODUCT_ID, WebConstants.COL_PRODUCT);

            dtUserProductsPreferred = BO.BusinessObjects.GetUserProductsPreferred(userData).Tables[0];

            GridViewProducts.DataSource = dtUserProducts;
            GridViewProducts.DataBind();
        }

        #region Grid Events

        /// <summary>
        /// GridViewProducts_RowDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewProducts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label LabelProductID = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_PRODUCT_ID);

                DataRow[] dr = dtUserProductsPreferred.Select(WebConstants.COL_PRODUCT_ID + " = " + LabelProductID.Text);
                if (dr.Length == 1)
                {
                    Label LabelUserProductPreferenceID = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_USER_PRODUCT_PREFERENCE_ID);
                    LabelUserProductPreferenceID.Text = dr[0][WebConstants.COL_USER_PRODUCT_PREFERENCE_ID].ToString();

                    CheckBox CheckBoxProductSelected = (CheckBox)e.Row.FindControl(WebConstants.CONTROL_CHECKBOX_PRODUCTS_SELECTED);
                    CheckBoxProductSelected.Checked = true;
                }
            }
        }

        #endregion

        #endregion

        #region User Products Roles

        /// <summary>
        /// PopulateUserProjectRoles
        /// </summary>
        private void PopulateUserProjectRoles()
        {
            DTO.Users userData = new DTO.Users();
            userData.UserID = Session[WebConstants.SESSION_USER_ID].ToString();

            DataTable dtUserProjectRoles = BO.BusinessObjects.GetUserRoles(userData).Tables[0];

            //DataView dvUserProjectRoles = dtUserProjectRoles.DefaultView;
            //dvUserProjectRoles.Sort = "RoleID Desc, ProjectRoleID ASC";

            GridViewProjectRoles.DataSource = dtUserProjectRoles;
            GridViewProjectRoles.DataBind();
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// PopulateData
        /// </summary>
        private void PopulateData()
        {
            PopulateUserProfileInformation();
            PopulateUserProductPreferences();
            PopulateUserProjectRoles();
        }

        #endregion

        #region Screen Access and Labels

        /// <summary>
        /// GetScreenAccess
        /// </summary>
        private void GetScreenAccess()
        {
            dataHeader.LoginID = Session[WebConstants.SESSION_LOGIN_ID].ToString();
        }

        /// <summary>
        /// SetScreenAccess
        /// </summary>
        private void SetScreenAccess()
        {
            if (GridViewProjectRoles.Rows.Count > GridViewProducts.Rows.Count)
                AddFooterPadding(GridViewProjectRoles.Rows.Count);
            else
                AddFooterPadding(GridViewProducts.Rows.Count);
        }

        /// <summary>
        /// PopulateScreenLabels
        /// </summary>
        /// <param name="userID"></param>
        private void PopulateScreenLabels()
        {
            DTO.Screens screenData = new DTO.Screens();
            screenData.ScreenIdentifier = Session[WebConstants.SESSION_IDENTIFIER].ToString();
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

            if (Session[WebConstants.SESSION_LOCALE_ID] != null && Session[WebConstants.SESSION_LOCALE_ID].ToString() != WebConstants.DEF_VAL_LOCALE_ID)
            {
                LabelHeader.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelHeader.Text);
                LabelLoginID.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelLoginID.Text);
                LabelVendor.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelVendor.Text);
                LabelFirstName.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelFirstName.Text);
                LabelLastName.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelLastName.Text);
                LabelNickName.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelNickName.Text);
                LabelEmailID.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelEmailID.Text);
                LabelAlternateEmailID.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelAlternateEmailID.Text);
                LabelContactNo.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelContactNo.Text);
                LabelManagerLoginID.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelManagerLoginID.Text);

                ButtonSaveUserInformation.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, ButtonSaveUserInformation.Text);

                foreach (DataControlField field in GridViewProducts.Columns)
                    if (field.HeaderText != "")
                        field.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, field.HeaderText);

                foreach (DataControlField field in GridViewProjectRoles.Columns)
                    if (field.HeaderText != "")
                        field.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, field.HeaderText);
            }
        }

        /// <summary>
        /// AddFooterPadding
        /// </summary>
        private void AddFooterPadding(int holidaysCount)
        {
            int spaceCount = (holidaysCount * NUM_EACH_GRID_ROW_DISPLAY_HEIGHT) + 4 * NUM_EACH_GRID_ROW_DISPLAY_HEIGHT;

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