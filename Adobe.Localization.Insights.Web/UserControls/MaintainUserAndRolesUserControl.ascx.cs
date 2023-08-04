using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Adobe.Localization.Insights.Common;
using COM = Adobe.Localization.Insights.Common.Common;
using BO = Adobe.Localization.Insights.BusinessLayer;
using DTO = Adobe.Localization.Insights.Common.DataTransferObjects;

namespace Adobe.Localization.Insights.Web.UserControls
{
    /// <summary>
    /// MaintainUserAndRolesUserControl
    /// </summary>
    public partial class MaintainUserAndRolesUserControl : System.Web.UI.UserControl
    {
        #region Variables

        private Control controlFired;
        private DataTable dtUserDetails;
        private DataTable dtUserProjectRoles;
        private DataTable dtUserProducts;
        private DataTable dtVendors;
        private DataTable dtProjectRoles;
        private DataTable dtProducts;
        private DataTable dtProductVersions;
        private DataTable dtScreenLabels;
        private DTO.Header dataHeader = new DTO.Header();

        private string productID = "";
        private int colID = 0;

        private bool isReadOnly = true;
        private bool isContractor = false;
        private bool isAdmin = true;

        private const string STR_SPACE = "&nbsp;";
        private const string STR_ZERO = "0";
        private const string STR_ASSIGNED_PRODUCTS = "AssignedProducts";
        //private const string STR_ROLE_ADMIN_FILTER_CRITERIA = "Role = 'Product' OR ProjectRole='Group Manager'";
        private const string STR_ROLE_NON_ADMIN_FILTER_CRITERIA = "Role = 'Product'";
        private const string STR_ROLE_VENDOR_FILTER_CRITERIA = "Role = 'Product' AND IsContractorApplicable = 1";
        private const string STR_VAL_ISACTIVE = "1";

        private const int PAGE_INDEX = 10;
        private const int TAB_INDEX_USER_ROLES = 1;
        private const int NUM_EACH_GRID_ROW_DISPLAY_HEIGHT = 20;

        private const int COL_VIEW_USER_SAVE_UPDATE_NO = 6;
        private const int COL_VIEW_USER_CANCEL_DELETE_NO = 7;
        private const int COL_USER_ROLE_SELECT_NO = 5;
        private const int COL_ASSIGNED_PRODUCT_SAVE_UPDATE_NO = 7;
        private const int COL_ASSIGNED_PRODUCT_CANCEL_DELETE_NO = 8;

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

            if (!this.IsPostBack)
            {
                PopulateDropDownListsData();
                PopulateVendors();
                PopulateProjectRoles();
                PopulateUserDetails();
                SetVisibility(false);
            }
            else
            {
                controlFired = GetPostBackControl(Page);
            }

            dtVendors = (DataTable)ViewState[WebConstants.TBL_VENDORS];
            dtProjectRoles = (DataTable)ViewState[WebConstants.TBL_PROJECT_ROLES];
            dtUserDetails = (DataTable)ViewState[WebConstants.TBL_USERS];
            dtProducts = (DataTable)ViewState[WebConstants.TBL_PRODUCT];
            dtProductVersions = (DataTable)ViewState[WebConstants.TBL_PRODUCT_VERSIONS];

            SetScreenAccess();
        }

        /// <summary>
        /// GetPostBackControl
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static Control GetPostBackControl(Page page)
        {
            Control postbackControlInstance = null;

            string postbackControlName = page.Request.Params.Get("__EVENTTARGET");
            if (postbackControlName != null && postbackControlName != string.Empty)
            {
                postbackControlInstance = page.FindControl(postbackControlName);
            }
            else
            {
                // handle the Button control postbacks
                for (int i = 0; i < page.Request.Form.Keys.Count; i++)
                {
                    postbackControlInstance = page.FindControl(page.Request.Form.Keys[i]);
                    if (postbackControlInstance is System.Web.UI.WebControls.Button)
                    {
                        return postbackControlInstance;
                    }
                }
            }
            // handle the ImageButton postbacks
            if (postbackControlInstance == null)
            {
                for (int i = 0; i < page.Request.Form.Count; i++)
                {
                    if ((page.Request.Form.Keys[i].EndsWith(".x")) || (page.Request.Form.Keys[i].EndsWith(".y")))
                    {
                        postbackControlInstance = page.FindControl(page.Request.Form.Keys[i].Substring(0, page.Request.Form.Keys[i].Length - 2));
                        return postbackControlInstance;
                    }
                }
            }
            return postbackControlInstance;
        }

        /// <summary>
        /// OnPreRender
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            if (controlFired != null)
            {
                if (controlFired.ClientID.Contains(STR_ASSIGNED_PRODUCTS))
                    TabContainerPopulateUserRolesData.ActiveTabIndex = TAB_INDEX_USER_ROLES;
            }
        }

        #endregion

        #region User Details

        #region Grid Events

        /// <summary>
        /// GridViewUsersDetails_PageIndexChanging
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewUsersDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewUsersDetails.PageIndex = e.NewPageIndex;
            if (!(dtUserDetails.Rows[e.NewPageIndex * 10][colID].ToString() == STR_ZERO || dtUserDetails.Rows[e.NewPageIndex * 10][colID].ToString() == ""))
            {
                DataRow dr = dtUserDetails.NewRow();
                dr[colID] = 0;
                dtUserDetails.Rows.InsertAt(dr, e.NewPageIndex * PAGE_INDEX);
            }
            GridViewUsersDetails.DataSource = dtUserDetails;
            GridViewUsersDetails.DataBind();
        }

        /// <summary>
        /// GridViewUsersDetails_RowDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewUsersDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                SetGridLinkButtonsDisplayText(e.Row);

                if (e.Row.Cells[colID].Text.ToString() == STR_ZERO || e.Row.Cells[colID].Text.ToString() == STR_SPACE)
                {
                    e.Row.Cells[colID].Text = "";
                    SetViewUsersLinkButtons(e.Row, true);
                }
            }
        }

        /// <summary>
        /// GridViewUsersDetails_RowUpdating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewUsersDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
        }

        /// <summary>
        /// GridViewUsersDetails_RowCancelingEdit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewUsersDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
        }

        #endregion

        #region GridView Button Click Events

        /// <summary>
        /// SaveViewUsersDetailsLinkButton_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SaveViewUsersDetailsLinkButton_Click(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow detailsGridViewRow = GridViewUsersDetails.Rows[index];

            DTO.Users userData = new DTO.Users();
            userData.DataHeader = dataHeader;

            TextBox TextBoxLoginID = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_LOGIN_ID);
            TextBox TextBoxFirstName = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_FIRSTNAME);
            TextBox TextBoxLastName = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_LASTNAME);
            DropDownList DropDownListSelectVendor = (DropDownList)detailsGridViewRow.FindControl(WebConstants.CONTROL_DROPDOWNLIST_SELECT_VENDOR);

            if (TextBoxLoginID.Text == "")
            {
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.SCREEN_LOGINID_MANDATORY);
                return;
            }
            userData.LoginID = TextBoxLoginID.Text;

            if (TextBoxFirstName.Text == "")
            {
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.SCREEN_FIRSTNAME_MANDATORY);
                return;
            }
            userData.FirstName = TextBoxFirstName.Text;
            userData.LastName = TextBoxLastName.Text;

            if (isContractor)
                userData.VendorID = Session[WebConstants.SESSION_VENDOR_ID].ToString();
            else
            {
                if (DropDownListSelectVendor.SelectedValue == STR_ZERO)
                {
                    LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.SCREEN_TEAM_MANDATORY);
                    return;
                }
                userData.VendorID = DropDownListSelectVendor.SelectedValue;
            }

            if (detailsGridViewRow.Cells[colID].Text != "")
            {
                Label LabelUserID = (Label)detailsGridViewRow.FindControl(WebConstants.CONTROL_LABEL_USER_ID);
                userData.UserID = LabelUserID.Text;
            }

            if (BO.BusinessObjects.AddUpdateUserDetails(userData))
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_SUCCESS_SAVE_MESSAGE);
            else
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_SAVE_MESSAGE);

            PopulateUserDetails();
        }

        /// <summary>
        /// UpdateViewUsersDetailsLinkButton_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UpdateViewUsersDetailsLinkButton_Click(object sender, CommandEventArgs e)
        {
            SetViewUsersLinkButtons(GridViewUsersDetails.Rows[Convert.ToInt32(e.CommandArgument)], true);
        }

        /// <summary>
        /// CancelViewUsersDetailsLinkButton_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CancelViewUsersDetailsLinkButton_Click(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow detailsGridViewRow = GridViewUsersDetails.Rows[index];

            if (detailsGridViewRow.Cells[colID].Text != "")
                SetViewUsersLinkButtons(detailsGridViewRow, false);
            else
                SetViewUsersLinkButtons(detailsGridViewRow, true);
        }

        /// <summary>
        /// DeleteViewUsersDetailsLinkButton_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DeleteViewUsersDetailsLinkButton_Click(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow detailsGridViewRow = GridViewUsersDetails.Rows[index];

            DTO.Users userData = new DTO.Users();

            Label LabelUserID = (Label)detailsGridViewRow.FindControl(WebConstants.CONTROL_LABEL_USER_ID);
            userData.UserID = LabelUserID.Text;

            if (BO.BusinessObjects.AddUpdateUserDetails(userData))
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_SUCCESS_DELETE_MESSAGE);
            else
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_DELETE_MESSAGE); ;

            PopulateUserDetails();
        }

        /// <summary>
        /// LinkButtonViewUserRoles_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonViewUserRoles_Click(object sender, CommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow detailsGridViewRow = GridViewUsersDetails.Rows[index];

                TextBox TextBoxLoginID = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_LOGIN_ID);
                TextBox TextBoxFirstName = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_FIRSTNAME);
                TextBox TextBoxLastName = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_LASTNAME);
                Label LabelUserID = (Label)detailsGridViewRow.FindControl(WebConstants.CONTROL_LABEL_USER_ID);

                LabelUserIDValue.Text = LabelUserID.Text;
                LabelUserNameValue.Text = TextBoxFirstName.Text + " " + TextBoxLastName.Text + " (" + TextBoxLoginID.Text + ")";

                SetVisibility(true);
                PopulateUserProjectRoles();
                PopulateUserProducts();
            }
            catch (Exception ex)
            {
                ExceptionLogger.Log(ex, "User Info: " + Session[WebConstants.SESSION_LOGIN_ID].ToString());
                throw (ex);
            }
        }

        #endregion

        /// <summary>
        /// setViewUsersLinkButtons
        /// </summary>
        /// <param name="gridViewRow"></param>
        /// <param name="isSave"></param>
        private void SetViewUsersLinkButtons(GridViewRow gridViewRow, bool isSave)
        {
            LinkButton LinkButtonSave = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_SAVE);
            LinkButtonSave.Visible = isSave;
            LinkButton LinkButtonCancel = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_CANCEL);
            LinkButtonCancel.Visible = isSave;
            LinkButton LinkButtonUpdate = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_UPDATE);
            LinkButtonUpdate.Visible = (!isSave);
            LinkButton LinkButtonDelete = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_DELETE);
            LinkButtonDelete.Visible = (!isSave);

            if (gridViewRow.Cells[colID].Text == "")
            {
                LinkButton LinkButtonViewUserRoles = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_VIEW_USER_ROLES);
                LinkButtonViewUserRoles.Enabled = false;
            }

            //LoginID
            SetLinkButtonsForTextBoxes(gridViewRow, WebConstants.CONTROL_LABEL_LOGIN_ID, WebConstants.CONTROL_TEXTBOX_LOGIN_ID, isSave);

            //FirstName
            SetLinkButtonsForTextBoxes(gridViewRow, WebConstants.CONTROL_LABEL_FIRSTNAME, WebConstants.CONTROL_TEXTBOX_FIRSTNAME, isSave);

            //LastName
            SetLinkButtonsForTextBoxes(gridViewRow, WebConstants.CONTROL_LABEL_LASTNAME, WebConstants.CONTROL_TEXTBOX_LASTNAME, isSave);

            //VendorID
            if (isContractor)
            {
                Label LabelVendor = (Label)gridViewRow.FindControl(WebConstants.CONTROL_LABEL_VENDOR);
                LabelVendor.Text = dtVendors.Select(WebConstants.COL_ID + " = " + Session[WebConstants.SESSION_VENDOR_ID].ToString())[0][WebConstants.COL_DESCRIPTION].ToString();
                LabelVendor.Visible = true;
            }
            else
                SetLinkButtonsForDropDowns(gridViewRow, dtVendors, WebConstants.CONTROL_LABEL_VENDOR, WebConstants.CONTROL_DROPDOWNLIST_SELECT_VENDOR, isSave);
        }

        #endregion

        #region User Roles

        #region Grid Events

        #region GridViewUserProjectRoles

        /// <summary>
        /// GridViewUserProjectRoles_RowDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewUserProjectRoles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label LabelProjectRoleID = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_PROJECT_ROLE_ID);

                DataRow[] drUserProjectRoles = dtUserProjectRoles.Select(WebConstants.COL_PROJECT_ROLE_ID + " = " + LabelProjectRoleID.Text);

                CheckBox CheckBoxRolesSelected = (CheckBox)e.Row.FindControl(WebConstants.CONTROL_CHECKBOX_ROLES_SELECTED);
                if (drUserProjectRoles.Length == 1)
                {
                    Label LabelUserProjectRoleID = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_USER_PROJECT_ROLE_ID);
                    LabelUserProjectRoleID.Text = drUserProjectRoles[0][WebConstants.COL_USER_PROJECT_ROLE_ID].ToString();

                    CheckBoxRolesSelected.Checked = true;
                }

                if (isReadOnly)
                    CheckBoxRolesSelected.Enabled = false;
            }
        }

        #endregion

        #endregion

        #region Button Events

        /// <summary>
        /// ButtonSaveUserProjectRoles_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSaveUserProjectRoles_Click(object sender, EventArgs e)
        {
            DTO.Users userData = new DTO.Users();
            userData.DataHeader = dataHeader;
            userData.UserID = LabelUserIDValue.Text;

            userData.UserCollection = new ArrayList();

            for (int i = 0; i < GridViewUserProjectRoles.Rows.Count; i++)
            {
                DTO.Users userPrjRoles = new DTO.Users();
                GridViewRow row = GridViewUserProjectRoles.Rows[i];

                userPrjRoles.UserProjectRoleID = ((Label)row.FindControl(WebConstants.CONTROL_LABEL_USER_PROJECT_ROLE_ID)).Text;
                userPrjRoles.ProjectRoleID = ((Label)row.FindControl(WebConstants.CONTROL_LABEL_PROJECT_ROLE_ID)).Text;
                userPrjRoles.IsSelected = ((CheckBox)row.FindControl(WebConstants.CONTROL_CHECKBOX_ROLES_SELECTED)).Checked.ToString();

                userData.UserCollection.Add(userPrjRoles);
            }

            if (BO.BusinessObjects.AddUpdateUserProjectRoles(userData))
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_SUCCESS_SAVE_MESSAGE);
            else
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_SAVE_MESSAGE);

            PopulateUserProjectRoles();
            PopulateUserProducts();
        }

        #endregion

        #endregion

        #region User Products

        #region Grid Events

        /// <summary>
        /// GridViewAssignedProducts_RowDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewAssignedProducts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                SetGridLinkButtonsDisplayText(e.Row);
                if (e.Row.Cells[colID].Text.ToString() == STR_ZERO || e.Row.Cells[colID].Text.ToString() == STR_SPACE)
                {
                    e.Row.Cells[colID].Text = "";
                    SetViewUserProductsLinkButtons(e.Row, true);
                }
                //else
                //{
                //    Label LabelUserProductID = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_USER_PRODUCT_ID);

                //    //DataRow[] drUserProduct = dtUserProducts.Select(WebConstants.COL_USER_PRODUCT_ID + LabelUserProductID.Text);
                //    //if (drUserProduct.Length == 1)
                //    //    if (drUserProduct[0]["IsOwner"].ToString() == "1")
                //    //    {
                //    //        CheckBox CheckBoxProjectRoleOwner = (CheckBox)e.Row.FindControl("CheckBoxProjectRoleOwner");
                //    //        CheckBoxProjectRoleOwner.Checked = true;
                //    //    }
                //}
            }
        }

        /// <summary>
        /// GridViewAssignedProduct_RowUpdating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewAssignedProduct_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
        }

        /// <summary>
        /// GridViewAssignedProduct_RowCancelingEdit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewAssignedProduct_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
        }

        #endregion

        #region GridView Button Click Events

        /// <summary>
        /// LinkButtonSaveDetails_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonSaveDetails_Click(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow detailsGridViewRow = GridViewAssignedProducts.Rows[index];

            DTO.Users userData = new DTO.Users();
            userData.DataHeader = dataHeader;

            Label LabelUserProjectRoleID = (Label)detailsGridViewRow.FindControl(WebConstants.CONTROL_LABEL_USER_PROJECT_ROLE_ID);
            DropDownList DropDownListProjectRoles = (DropDownList)detailsGridViewRow.FindControl(WebConstants.CONTROL_DROPDOWNLIST_PROJECT_ROLES);
            DropDownList DropDownListProductVersion = (DropDownList)detailsGridViewRow.FindControl(WebConstants.CONTROL_DROPDOWNLIST_PRODUCT_VERSION);
            //CheckBox CheckBoxProjectRoleOwner = (CheckBox)detailsGridViewRow.FindControl("CheckBoxProjectRoleOwner");

            userData.UserProjectRoleID = DropDownListProjectRoles.SelectedValue;
            userData.UserID = LabelUserIDValue.Text;
            userData.ProductVersionID = DropDownListProductVersion.SelectedValue;

            //userData.IsProductOwner = "0";

            //if (CheckBoxProjectRoleOwner.Checked)
            //    userData.IsProductOwner = "1";

            if (detailsGridViewRow.Cells[colID].Text != "")
            {
                Label LabelUserProductID = (Label)detailsGridViewRow.FindControl(WebConstants.CONTROL_LABEL_USER_PRODUCT_ID);
                userData.UserProductID = LabelUserProductID.Text;
            }

            if (BO.BusinessObjects.AddUpdateUserProducts(userData))
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_SUCCESS_SAVE_MESSAGE);
            else
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_SAVE_MESSAGE);

            PopulateUserProducts();
        }

        /// <summary>
        /// LinkButtonUpdateDetails_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonUpdateDetails_Click(object sender, CommandEventArgs e)
        {
            SetViewUserProductsLinkButtons(GridViewAssignedProducts.Rows[Convert.ToInt32(e.CommandArgument)], true);
        }

        /// <summary>
        /// LinkButtonCancelDetails_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonCancelDetails_Click(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow detailsGridViewRow = GridViewUsersDetails.Rows[index];

            if (detailsGridViewRow.Cells[colID].Text != "")
                SetViewUserProductsLinkButtons(detailsGridViewRow, false);
            else
                SetViewUserProductsLinkButtons(detailsGridViewRow, true);
        }

        /// <summary>
        /// LinkButtonDeleteDetails_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonDeleteDetails_Click(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow detailsGridViewRow = GridViewAssignedProducts.Rows[index];

            DTO.Users userData = new DTO.Users();

            Label LabelUserProductID = (Label)detailsGridViewRow.FindControl(WebConstants.CONTROL_LABEL_USER_PRODUCT_ID);
            userData.UserProductID = LabelUserProductID.Text;

            if (BO.BusinessObjects.AddUpdateUserProducts(userData))
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_SUCCESS_DELETE_MESSAGE);
            else
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_DELETE_MESSAGE); ;

            PopulateUserProducts();
        }

        #endregion

        #region DropDownList events

        /// <summary>
        /// DropDownListProduct_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListProduct_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList dropDownListProduct = (DropDownList)sender;
            GridViewRow gridViewRow = (GridViewRow)dropDownListProduct.NamingContainer;

            PopulateProductVersionsGridDropDownList(gridViewRow, dropDownListProduct);
        }

        #endregion

        /// <summary>
        /// SetViewUserProductsLinkButtons
        /// </summary>
        /// <param name="gridViewRow"></param>
        /// <param name="isSave"></param>
        private void SetViewUserProductsLinkButtons(GridViewRow gridViewRow, bool isSave)
        {
            LinkButton LinkButtonSave = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_SAVE);
            LinkButtonSave.Visible = isSave;
            LinkButton LinkButtonCancel = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_CANCEL);
            LinkButtonCancel.Visible = isSave;
            LinkButton LinkButtonUpdate = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_UPDATE);
            LinkButtonUpdate.Visible = (!isSave);
            LinkButton LinkButtonDelete = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_DELETE);
            LinkButtonDelete.Visible = (!isSave);

            //Project Role
            Label LabelProjectRole = (Label)gridViewRow.FindControl(WebConstants.CONTROL_LABEL_PROJECT_ROLE);
            LabelProjectRole.Visible = (!isSave);
            DropDownList DropDownListProjectRoles = (DropDownList)gridViewRow.FindControl(WebConstants.CONTROL_DROPDOWNLIST_PROJECT_ROLES);
            DropDownListProjectRoles.Visible = isSave;

            dtUserProjectRoles = ((DataTable)ViewState[WebConstants.TBL_USER_PROJECT_ROLES]);

            DropDownListProjectRoles.DataSource = dtUserProjectRoles;
            DropDownListProjectRoles.DataValueField = WebConstants.COL_USER_PROJECT_ROLE_ID;
            DropDownListProjectRoles.DataTextField = WebConstants.COL_PROJECT_ROLE;
            DropDownListProjectRoles.DataBind();
            DropDownListProjectRoles.SelectedIndex = DropDownListProjectRoles.Items.IndexOf(DropDownListProjectRoles.Items.FindByText(LabelProjectRole.Text));

            //Products
            SetLinkButtonsForDropDowns(gridViewRow, dtProducts, WebConstants.CONTROL_LABEL_PRODUCT, WebConstants.CONTROL_DROPDOWNLIST_PRODUCT, isSave);

            //Product Version 
            DropDownList dropDownListProduct = (DropDownList)gridViewRow.FindControl(WebConstants.CONTROL_DROPDOWNLIST_PRODUCT);
            Label LabelProductVersion = (Label)gridViewRow.FindControl(WebConstants.CONTROL_LABEL_PRODUCT_VERSION);
            LabelProductVersion.Visible = (!isSave);
            DropDownList DropDownListProductVersion = (DropDownList)gridViewRow.FindControl(WebConstants.CONTROL_DROPDOWNLIST_PRODUCT_VERSION);
            DropDownListProductVersion.Visible = isSave;

            PopulateProductVersionsGridDropDownList(gridViewRow, dropDownListProduct);
            DropDownListProductVersion.SelectedIndex = DropDownListProductVersion.Items.IndexOf(DropDownListProductVersion.Items.FindByText(LabelProductVersion.Text));
        }

        /// <summary>
        /// PopulateProductVersions
        /// </summary>
        private void PopulateProductVersionsGridDropDownList(GridViewRow row, DropDownList productDropDownList)
        {
            DropDownList dropDownListProductVersion = (DropDownList)row.FindControl("DropDownListProductVersion");

            //DataRow[] drCol = dtProductVersions.Select(WebConstants.COL_PRODUCT_ID + " = " + productDropDownList.SelectedValue);

            DataView dv = dtProductVersions.DefaultView;
            dv.RowFilter = WebConstants.COL_PRODUCT_ID + " = " + productDropDownList.SelectedValue;

            dropDownListProductVersion.DataSource = dv;
            dropDownListProductVersion.DataValueField = WebConstants.COL_PRODUCT_VERSION_ID;
            dropDownListProductVersion.DataTextField = WebConstants.COL_PRODUCT_VERSION;
            dropDownListProductVersion.DataBind();
        }

        #endregion

        #region Button Events

        /// <summary>
        /// ButtonSearch_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            LabelMessage.Text = "";
            PopulateUserDetails();
        }

        /// <summary>
        /// ButtonBackToUsers_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonBackToUsers_Click(object sender, EventArgs e)
        {
            SetVisibility(false);
        }

        #endregion

        #region DropDownList Events

        /// <summary>
        /// DropDownListVendor_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListVendor_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            LabelMessage.Text = "";
            PopulateUserDetails();
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// PopulateDropDownListsData
        /// </summary>
        private void PopulateDropDownListsData()
        {
            //dtVendors = (DataTable)ViewState[WebConstants.TBL_VENDORS];
            //dtProjectRoles = (DataTable)ViewState[WebConstants.TBL_PROJECT_ROLES];
            //dtProducts = (DataTable)ViewState[WebConstants.TBL_PRODUCT];
            //dtProductVersions = (DataTable)ViewState[WebConstants.TBL_PRODUCT_VERSIONS];

            dtVendors = PopulateTypeDetails(WebConstants.TBL_VENDORS, null, true);
            ViewState[WebConstants.TBL_VENDORS] = dtVendors;

            //dtProjectRoles = PopulateTypeDetails(WebConstants.TBL_PROJECT_ROLES, null, true);
            //dtProjectRoles = BO.UsersBO.GetProjectRoles(userData).Tables[0];

            dtProducts = PopulateTypeDetails(WebConstants.TBL_PRODUCT, null, true);

            if (!isAdmin)
            {
                DataView dvProducts = new DataView(dtProducts);
                dvProducts.RowFilter = WebConstants.COL_ID + " = " + Session[WebConstants.SESSION_PRODUCT_ID].ToString();
                dtProducts = dvProducts.ToTable();
            }
            ViewState[WebConstants.TBL_PRODUCT] = dtProducts;

            dtProductVersions = PopulateProductVersion();
            ViewState[WebConstants.TBL_PRODUCT_VERSIONS] = dtProductVersions;
        }

        /// <summary>
        /// PopulateProductVersion
        /// </summary>
        private DataTable PopulateProductVersion()
        {
            DTO.Product productData = new DTO.Product();
            productData.IsActive = STR_VAL_ISACTIVE;

            return BO.BusinessObjects.GetProductVersion(productData).Tables[0];
        }

        /// <summary>
        /// PopulateTypeDetails
        /// </summary>
        private DataTable PopulateTypeDetails(string tableName, DropDownList dropDownList, bool isCode)
        {
            DTO.MasterData masterData = new DTO.MasterData();
            masterData.TableName = tableName;

            DataTable typeDetailsDataTable = BO.BusinessObjects.GetDetailsforMasterData(masterData).Tables[0];

            typeDetailsDataTable.Rows.RemoveAt(0);

            if (isCode)
                typeDetailsDataTable.Columns.RemoveAt(1);

            typeDetailsDataTable.Columns[0].ColumnName = WebConstants.COL_ID;
            typeDetailsDataTable.Columns[1].ColumnName = WebConstants.COL_DESCRIPTION;

            if (dropDownList != null)
            {
                dropDownList.DataSource = typeDetailsDataTable;
                dropDownList.DataValueField = WebConstants.COL_ID;
                dropDownList.DataTextField = WebConstants.COL_DESCRIPTION;
                dropDownList.DataBind();
            }

            return typeDetailsDataTable;
        }

        /// <summary>
        /// PopulateVendors
        /// </summary>
        private void PopulateVendors()
        {
            DataTable dtPopulateVendors = dtVendors.Copy();

            DataRow dr = dtPopulateVendors.NewRow();
            dr[WebConstants.COL_ID] = STR_ZERO;
            dr[WebConstants.COL_DESCRIPTION] = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.DEF_VAL_TEAMS_ALL); ;
            dtPopulateVendors.Rows.InsertAt(dr, 0);

            DropDownListVendor.DataSource = dtPopulateVendors;
            DropDownListVendor.DataValueField = WebConstants.COL_ID;
            DropDownListVendor.DataTextField = WebConstants.COL_DESCRIPTION;
            DropDownListVendor.DataBind();

            DropDownListVendor.SelectedIndex = DropDownListVendor.Items.IndexOf(DropDownListVendor.Items.FindByValue(Session[WebConstants.SESSION_VENDOR_ID].ToString()));
        }

        /// <summary>
        /// PopulateProjectRoles
        /// </summary>
        private void PopulateProjectRoles()
        {
            DTO.Users userData = new DTO.Users();
            dtProjectRoles = BO.BusinessObjects.GetProjectRoles(userData).Tables[0];

            dtProjectRoles = ApplyFiltersOnProjectRoles(dtProjectRoles);
            ViewState[WebConstants.TBL_PROJECT_ROLES] = dtProjectRoles;
        }

        /// <summary>
        /// PopulateUserDetails
        /// </summary>
        private void PopulateUserDetails()
        {
            DTO.Users userData = new DTO.Users();
            userData.UserName = TextBoxUser.Text;
            userData.VendorID = DropDownListVendor.SelectedValue;
            userData.IsUserMasterScreen = true;

            dtUserDetails = BO.BusinessObjects.GetUserDetails(userData).Tables[0];

            if (isReadOnly)
                dtUserDetails.Rows.RemoveAt(0);

            ViewState[WebConstants.TBL_USERS] = dtUserDetails.Copy();

            GridViewUsersDetails.DataSource = dtUserDetails;
            GridViewUsersDetails.DataBind();
        }

        /// <summary>
        /// PopulateUserProjectRoles
        /// </summary>
        private void PopulateUserProjectRoles()
        {
            DTO.Users userData = new DTO.Users();
            userData.UserID = LabelUserIDValue.Text;

            if (Session[WebConstants.SESSION_PRODUCT_VERSION_ID] != null)
                userData.ProductVersionID = Session[WebConstants.SESSION_PRODUCT_VERSION_ID].ToString();

            dtUserProjectRoles = BO.BusinessObjects.GetUserProjectRoles(userData).Tables[0];

            userData.UserID = Session[WebConstants.SESSION_USER_ID].ToString();
            dtProjectRoles = ApplyFiltersOnProjectRoles(dtProjectRoles, BO.BusinessObjects.GetUserProjectRoles(userData).Tables[0]);
            ViewState[WebConstants.TBL_USER_PROJECT_ROLES] = dtUserProjectRoles;

            GridViewUserProjectRoles.DataSource = dtProjectRoles;
            GridViewUserProjectRoles.DataBind();
        }

        /// <summary>
        /// ApplyFiltersOnProjectRoles
        /// </summary>
        private DataTable ApplyFiltersOnProjectRoles(DataTable dtFilterData, DataTable dtUserProjectRoles)
        {
            if (!isAdmin)
            {
                DataView dvfilter = new DataView(dtFilterData);

                if (!isContractor)
                    dvfilter.RowFilter = STR_ROLE_NON_ADMIN_FILTER_CRITERIA;
                else
                {
                    StringBuilder projectRoleFilter = new StringBuilder("0");

                    foreach (DataRow drUserProjectRole in dtUserProjectRoles.Rows)
                    {
                        string projectRoleID = drUserProjectRole[WebConstants.COL_PROJECT_ROLE_ID].ToString();
                        projectRoleFilter.Append("," + projectRoleID);

                        if (projectRoleID == "8")
                            projectRoleFilter.Append(",5,6");
                        if (projectRoleID == "6")
                            projectRoleFilter.Append(",5");
                    }

                    dvfilter.RowFilter = STR_ROLE_VENDOR_FILTER_CRITERIA + " AND ProjectRoleID IN (" + projectRoleFilter +")";
                    //dvfilter.RowFilter = STR_ROLE_VENDOR_FILTER_CRITERIA + " AND ProjectRoleID <=" + dtUserProjectRoles.Rows[dtUserProjectRoles.Rows.Count - 1][WebConstants.COL_PROJECT_ROLE_ID].ToString();
                }
                dtFilterData = dvfilter.ToTable();
            }

            return dtFilterData;
        }

        /// <summary>
        /// ApplyFiltersOnProjectRoles
        /// </summary>
        private DataTable ApplyFiltersOnProjectRoles(DataTable dtFilterData)
        {
            if (!isAdmin)
            {
                DataView dvfilter = new DataView(dtFilterData);

                if (!isContractor)
                    dvfilter.RowFilter = STR_ROLE_NON_ADMIN_FILTER_CRITERIA;
                else
                    dvfilter.RowFilter = STR_ROLE_VENDOR_FILTER_CRITERIA;

                dtFilterData = dvfilter.ToTable();
            }

            return dtFilterData;
        }

        /// <summary>
        /// PopulateUserProducts
        /// </summary>
        private void PopulateUserProducts()
        {
            DTO.Users userData = new DTO.Users();
            userData.UserID = LabelUserIDValue.Text;

            dtUserProducts = BO.BusinessObjects.GetUserProductRoles(userData).Tables[0];

            if (isReadOnly)
                dtUserProducts.Rows.RemoveAt(0);

            GridViewAssignedProducts.DataSource = dtUserProducts;
            GridViewAssignedProducts.DataBind();
        }

        /// <summary>
        /// SetLinkButtonsForDropDowns
        /// </summary>
        /// <param name="gridViewRow"></param>
        /// <param name="labelType"></param>
        /// <param name="dropDownListType"></param>
        /// <param name="isSave"></param>
        private void SetLinkButtonsForDropDowns(GridViewRow gridViewRow, DataTable dt, string labelType, string dropDownListType, bool isSave)
        {
            Label LabelType = (Label)gridViewRow.FindControl(labelType);
            LabelType.Visible = (!isSave);
            DropDownList DropDownListType = (DropDownList)gridViewRow.FindControl(dropDownListType);
            DropDownListType.Visible = isSave;

            DropDownListType.DataSource = dt;
            DropDownListType.DataValueField = WebConstants.COL_ID;
            DropDownListType.DataTextField = WebConstants.COL_DESCRIPTION;
            DropDownListType.DataBind();
            DropDownListType.SelectedIndex = DropDownListType.Items.IndexOf(DropDownListType.Items.FindByText(LabelType.Text));
        }

        /// <summary>
        /// SetLinkButtonsForTextBoxes
        /// </summary>
        /// <param name="gridViewRow"></param>
        /// <param name="labelType"></param>
        /// <param name="textBoxType"></param>
        /// <param name="isSave"></param>
        private void SetLinkButtonsForTextBoxes(GridViewRow gridViewRow, string labelType, string textBoxType, bool isSave)
        {
            Label LabelType = (Label)gridViewRow.FindControl(labelType);
            LabelType.Visible = (!isSave);
            TextBox TextBoxType = (TextBox)gridViewRow.FindControl(textBoxType);
            TextBoxType.Visible = isSave;
            TextBoxType.Text = LabelType.Text;
        }

        /// <summary>
        /// SetLinkButtonsForDateCalendar
        /// </summary>
        /// <param name="gridViewRow"></param>
        /// <param name="labelType"></param>
        /// <param name="dateCalendarType"></param>
        /// <param name="isSave"></param>
        private void SetLinkButtonsForDateCalendar(GridViewRow gridViewRow, string labelType, string dateCalendarType, bool isSave)
        {
            Label LabelType = (Label)gridViewRow.FindControl(labelType);
            LabelType.Visible = (!isSave);
            Calendar CalendarType = (Calendar)gridViewRow.FindControl(dateCalendarType);
            CalendarType.Visible = isSave;
            CalendarType.Date = LabelType.Text;
        }

        /// <summary>
        /// SetVisibility
        /// </summary>
        /// <param name="isUpdate"></param>
        /// <param name="isPopulate"></param>
        private void SetVisibility(bool isView)
        {
            PanelSearchFilter.Visible = !isView;
            PanelUserDetails.Visible = !isView;

            PanelShowUserDetails.Visible = isView;
            PanelUserRolesDetails.Visible = isView;

            LabelMessage.Text = "";
        }

        #endregion

        #region Screen Access and Labels

        /// <summary>
        /// GetScreenAccess
        /// </summary>
        private void GetScreenAccess()
        {
            dataHeader.LoginID = Session[WebConstants.SESSION_LOGIN_ID].ToString();

            if (Session[WebConstants.SESSION_IS_ADMIN] != null)
                isAdmin = (bool)Session[WebConstants.SESSION_IS_ADMIN];

            if (Session[WebConstants.SESSION_PRODUCT_ID] != null)
                productID = Session[WebConstants.SESSION_PRODUCT_ID].ToString();

            isContractor = (bool)Session[WebConstants.SESSION_VENDOR_IS_CONTRACTOR];

            if (Session[WebConstants.SESSION_CACHE_SCREEN_ACCESS_DATA_TABLE] != null)
            {
                DataTable dtScreenAccess = (DataTable)Session[WebConstants.SESSION_CACHE_SCREEN_ACCESS_DATA_TABLE];

                DataRow drScreenAccess = dtScreenAccess.Select(WebConstants.COL_SCREEN_IDENTIFIER + " = '" + Session[WebConstants.SESSION_IDENTIFIER] + "'")[0];

                if (drScreenAccess[WebConstants.COL_SCREEN_IS_READ_WRITE].ToString() == WebConstants.DEF_VAL_ACCESS_CHECKED)
                    isReadOnly = false;
            }
        }

        /// <summary>
        /// SetScreenAccess
        /// </summary>
        private void SetScreenAccess()
        {
            if (isReadOnly)
            {
                GridViewUsersDetails.Columns[COL_VIEW_USER_SAVE_UPDATE_NO].Visible = false;
                GridViewUsersDetails.Columns[COL_VIEW_USER_CANCEL_DELETE_NO].Visible = false;

                ButtonSaveUserProjectRoles.Enabled = false;

                GridViewAssignedProducts.Columns[COL_ASSIGNED_PRODUCT_SAVE_UPDATE_NO].Visible = false;
                GridViewAssignedProducts.Columns[COL_ASSIGNED_PRODUCT_CANCEL_DELETE_NO].Visible = false;
            }

            if (isContractor)
            {
                LabelVendorValue.Text = DropDownListVendor.SelectedItem.Text;
                LabelVendorValue.Visible = true;
                DropDownListVendor.Visible = false;
            }

            if (GridViewUsersDetails.Visible)
                AddFooterPadding(GridViewUsersDetails.Rows.Count);
            else
                PanelFooterPadding.Visible = false;
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
                LabelUser.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelUser.Text);
                LabelVendor.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelVendor.Text);
                LabelUserName.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelUserName.Text);

                ButtonSearch.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, ButtonSearch.Text);
                ButtonBackToUsers.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, ButtonBackToUsers.Text);
                ButtonSaveUserProjectRoles.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, ButtonSaveUserProjectRoles.Text);

                PanelSearchFilter.GroupingText = COM.GetScreenLocalizedLabel(dtScreenLabels, PanelSearchFilter.GroupingText);

                TabPanelUserRoles.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, TabPanelUserRoles.HeaderText);
                TabPanelProductsAssigned.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, TabPanelProductsAssigned.HeaderText);

                foreach (DataControlField field in GridViewUsersDetails.Columns)
                    if (field.HeaderText != "")
                        field.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, field.HeaderText);

                foreach (DataControlField field in GridViewUserProjectRoles.Columns)
                    if (field.HeaderText != "")
                        field.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, field.HeaderText);

                foreach (DataControlField field in GridViewAssignedProducts.Columns)
                    if (field.HeaderText != "")
                        field.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, field.HeaderText);
            }
        }

        /// <summary>
        /// SetGridLinkButtonsDisplayText
        /// </summary>
        /// <param name="gridView"></param>
        private void SetGridLinkButtonsDisplayText(GridViewRow gridViewRow)
        {
            LinkButton LinkButtonSave = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_SAVE);
            LinkButton LinkButtonCancel = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_CANCEL);
            LinkButton LinkButtonUpdate = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_UPDATE);
            LinkButton LinkButtonDelete = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_DELETE);
            LinkButton LinkButtonViewUserRoles = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_VIEW_USER_ROLES);

            if (LinkButtonSave != null)
                LinkButtonSave.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LinkButtonSave.Text);
            if (LinkButtonCancel != null)
            {
                if (gridViewRow.Cells[colID].Text.ToString() == STR_ZERO || gridViewRow.Cells[colID].Text.ToString() == STR_SPACE)
                    LinkButtonCancel.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.DEF_CONTROL_LINKBUTTON_CLEAR);
                else
                    LinkButtonCancel.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LinkButtonCancel.Text);
            }
            if (LinkButtonUpdate != null)
                LinkButtonUpdate.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LinkButtonUpdate.Text);
            if (LinkButtonDelete != null)
                LinkButtonDelete.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LinkButtonDelete.Text);
            if (LinkButtonViewUserRoles != null)
                LinkButtonViewUserRoles.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LinkButtonViewUserRoles.Text);
        }

        /// <summary>
        /// AddFooterPadding
        /// </summary>
        private void AddFooterPadding(int rowsCount)
        {
            int spaceCount = (rowsCount * NUM_EACH_GRID_ROW_DISPLAY_HEIGHT) + 4 * NUM_EACH_GRID_ROW_DISPLAY_HEIGHT;

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