using System;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Generic;
using Adobe.Localization.Insights.Common;
using COM = Adobe.Localization.Insights.Common.Common;
using BO = Adobe.Localization.Insights.BusinessLayer;
using DTO = Adobe.Localization.Insights.Common.DataTransferObjects;

namespace Adobe.Localization.Insights.Web
{
    /// <summary>
    /// Site - MasterPage
    /// </summary>
    public partial class Site : System.Web.UI.MasterPage
    {
        #region Variables

        private string userID;
        private DataTable dtMenuLabels;

        private const string CONST_ZERO = "0";
        private string VAL_ALL = "ALL";
        private const string VAL_SUPER_USER = "1";
        private const string STR_SCREEN_LABEL_CRITERIA = "Menu Title";
        private const string STR_SCREEN_SORTING_CRITERIA = "Sequence ASC, ScreenIdentifier ASC";
        private const string STR_SCREEN_ID_FILTER = "ScreenID";
        private const string STR_PARENT_SCREEN_ID_FILTER = "ParentScreenID";
        private const string STR_INDEX_GROUP_FILTER = "IndexGroup";
        private const string STR_PRODUCT_TYPE_FILTER = "IsProductType <> 2";

        private const string STR_HOME = "Home";
        private const string STR_TEAM = "Team";
        private const string STR_CONTACT_US = "ContactUs";
        private const string STR_ABOUT_US = "AboutUs";

        private const int VAL_LEFT_PANEL_MAX_HEIGHT = 650;
        private const int VAL_LEFT_PANEL_MIN_HEIGHT = 400;
        private const int VAL_LEFT_PANEL_MAX_COUNT = 32;

        #endregion

        #region Page Load event

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            userID = "";
            if (Session[WebConstants.SESSION_USER_ID] != null)
                userID = Session[WebConstants.SESSION_USER_ID].ToString();

            if (!IsPostBack)
            {
                PopulateLocales();
                LeftPanel.Visible = false;
                if (userID != "")
                {
                    DTO.Users userData = new DTO.Users();
                    userData.UserID = userID;

                    DataTable dtuserDetails = BO.BusinessObjects.GetUserDetails(userData).Tables[0];
                    dtuserDetails.Rows.RemoveAt(0);

                    if (dtuserDetails.Rows.Count > 0)
                    {
                        //Set Contractor Details
                        Session[WebConstants.SESSION_VENDOR_IS_CONTRACTOR] = true;
                        if (dtuserDetails.Rows[0][WebConstants.COL_VENDOR_IS_CONTRACTOR].ToString() == CONST_ZERO)
                            Session[WebConstants.SESSION_VENDOR_IS_CONTRACTOR] = false;

                        if (dtuserDetails.Rows[0][WebConstants.COL_USER_NAME].ToString() != "")
                            LabelLoginID.Text = dtuserDetails.Rows[0][WebConstants.COL_USER_NAME].ToString();
                        else
                            LabelLoginID.Text = dtuserDetails.Rows[0][WebConstants.COL_FIRST_NAME].ToString();

                        LeftPanel.Visible = true;
                        if (dtuserDetails.Rows[0][WebConstants.COL_SUPER_USER].ToString() == VAL_SUPER_USER)
                            DropDownListUserRoles.Visible = true;

                        SetMenuAndRoles(userID);
                    }
                    else
                    {
                        LabelLoginID.Text = Session[WebConstants.SESSION_LOGIN_ID].ToString();
                    }
                }
            }

            if (userID == "")
                SetControlsVisibility(false);
            else
                SetControlsVisibility(true);

            if (!LeftPanel.Visible)
            {
                TDLeftPanel.Style.Remove("width");
            }

            SetScreenAccessAndLabels();
        }

        #endregion

        #region MenuItem Events

        /// <summary>
        /// masterMenu_OnMenuItemClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void masterMenu_OnMenuItemClick(object sender, MenuEventArgs e)
        {
            if (e.Item.Value == STR_TEAM)
                Response.Redirect(WebConstants.PAGE_TEAM);

            if (e.Item.Value == STR_CONTACT_US)
                Response.Redirect(WebConstants.PAGE_CONTACTUS);

            if (e.Item.Value == STR_ABOUT_US)
                Response.Redirect(WebConstants.PAGE_ABOUT_US);
        }

        #endregion

        #region Image and Link Buttons

        /// <summary>
        /// Home_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Home_Click(object sender, EventArgs e)
        {
            Home();
        }

        /// <summary>
        /// Home_Click
        /// </summary>
        /// <param name="e"></param>
        protected void Home_Click(object sender, ImageClickEventArgs e)
        {
            Home();
        }

        /// <summary>
        /// Logout_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Logout_Click(object sender, EventArgs e)
        {
            Logout();
        }

        /// <summary>
        /// Logout_Click
        /// </summary>
        /// <param name="e"></param>
        protected void Logout_Click(object sender, ImageClickEventArgs e)
        {
            Logout();
        }

        /// <summary>
        /// Home
        /// </summary>
        private void Home()
        {
            Session.Remove(WebConstants.SESSION_IDENTIFIER);
            //Session.Remove(WebConstants.SESSION_PRODUCT_ID);
            //Session.Remove(WebConstants.SESSION_PRODUCT_VERSION_ID);

            if (Session[WebConstants.SESSION_VIEW_PRODUCT_VERSION] != null)
                Session.Remove(WebConstants.SESSION_VIEW_PRODUCT_VERSION);

            if (Session[WebConstants.SESSION_PROJECT_PHASE_ID] != null)
                Session.Remove(WebConstants.SESSION_PROJECT_PHASE_ID);

            if (Session[WebConstants.SESSION_DATA_TREEVIEW_NODE_COLLECTION] != null)
                Session.Remove(WebConstants.SESSION_DATA_TREEVIEW_NODE_COLLECTION);

            if (Session[WebConstants.SESSION_CACHE_SCREEN_ACCESS_DATA_TABLE] != null)
                Session.Remove(WebConstants.SESSION_CACHE_SCREEN_ACCESS_DATA_TABLE);

            Response.Redirect(WebConstants.PAGE_HOME);
        }

        /// <summary>
        /// Logout
        /// </summary>
        private void Logout()
        {
            string localeID;

            if (Session[WebConstants.SESSION_LOCALE_ID] != null)
                localeID = Session[WebConstants.SESSION_LOCALE_ID].ToString();
            else
                localeID = DropDownListLocales.SelectedValue;

            Session.RemoveAll();

            Session[WebConstants.SESSION_LOCALE_ID] = localeID;
            Response.Redirect(WebConstants.PAGE_LOGOUT);
        }

        #endregion

        #region TreeView Events

        /// <summary>
        /// MasterTree_OnSelectedNodeChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void MasterTree_OnSelectedNodeChanged(object sender, EventArgs e)
        {
            if (Session[WebConstants.SESSION_IDENTIFIER] == null || Session[WebConstants.SESSION_IDENTIFIER].ToString() != DataTreeview.SelectedNode.Value)
            {
                DataTreeview.SelectedNode.Expand();
                Session[WebConstants.SESSION_IDENTIFIER] = DataTreeview.SelectedNode.Value;
            }
            else
            {
                if ((bool)DataTreeview.SelectedNode.Expanded)
                    DataTreeview.SelectedNode.Collapse();
                else
                    DataTreeview.SelectedNode.Expand();
            }

            List<TreeNode> treeList = new List<TreeNode>();
            foreach (TreeNode node in DataTreeview.Nodes)
                treeList.Add(node);

            Session[WebConstants.SESSION_DATA_TREEVIEW_NODE_COLLECTION] = treeList;
            Response.Redirect(WebConstants.PAGE_LOCALIZATION_INSIGHT);
        }

        #endregion

        #region DropDownList Events

        /// <summary>
        /// DropDownListLocales_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListLocales_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Session[WebConstants.SESSION_LOCALE_ID] = DropDownListLocales.SelectedValue;
            Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri);
        }

        /// <summary>
        /// DropDownListUserRoles_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListUserRoles_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Session[WebConstants.SESSION_PROJECT_ROLE_ID] = DropDownListUserRoles.SelectedValue;
            SetAdminAccess();

            if (Session[WebConstants.SESSION_IDENTIFIER] != null)
                Session.Remove(WebConstants.SESSION_IDENTIFIER);
            if (Session[WebConstants.SESSION_PRODUCT_ID] != null)
                Session.Remove(WebConstants.SESSION_PRODUCT_ID);
            if (Session[WebConstants.SESSION_PRODUCT_VERSION_ID] != null)
                Session.Remove(WebConstants.SESSION_PRODUCT_VERSION_ID);
            if (Session[WebConstants.SESSION_DATA_TREEVIEW_NODE_COLLECTION] != null)
                Session.Remove(WebConstants.SESSION_DATA_TREEVIEW_NODE_COLLECTION);

            Response.Redirect(WebConstants.PAGE_HOME);
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// PopulateLocales
        /// </summary>
        private void PopulateLocales()
        {
            DataTable dtLocales;
            if ((DataTable)ViewState[WebConstants.TBL_LOCALES] == null)
            {
                dtLocales = PopulateTypeDetails(WebConstants.TBL_LOCALES, DropDownListLocales);
                ViewState[WebConstants.TBL_LOCALES] = dtLocales;
            }
            else
                dtLocales = (DataTable)ViewState[WebConstants.TBL_LOCALES];

            //if (Session[WebConstants.SESSION_LOCALE_ID] == null || Session[WebConstants.SESSION_NEW_LOCALE_ID] == null)
            if (Session[WebConstants.SESSION_LOCALE_ID] == null)
            {
                Session[WebConstants.SESSION_LOCALE_ID] = DropDownListLocales.SelectedValue;
            }
            else
            {
                DropDownListLocales.SelectedIndex = DropDownListLocales.Items.IndexOf(DropDownListLocales.Items.FindByValue(Session[WebConstants.SESSION_LOCALE_ID].ToString()));
                //Session[WebConstants.SESSION_LOCALE_ID] = Session[WebConstants.SESSION_NEW_LOCALE_ID];
            }
        }

        /// <summary>
        /// PopulateScreenLabels
        /// </summary>
        private void PopulateScreenLabels()
        {
            if ((DataTable)ViewState[WebConstants.TBL_SCREEN_LABELS] == null)
            {
                DTO.Screens screenData = new DTO.Screens();
                screenData.LocaleID = DropDownListLocales.SelectedValue;
                screenData.LabelCategoryID = WebConstants.MENU_TITLE;

                dtMenuLabels = BO.BusinessObjects.GetScreenLocalizedLabels(screenData).Tables[0];

                ViewState[WebConstants.TBL_SCREEN_LABELS] = dtMenuLabels;
            }
            else
            {
                dtMenuLabels = (DataTable)ViewState[WebConstants.TBL_SCREEN_LABELS];
            }
        }

        /// <summary>
        /// PopulateUserRoles
        /// </summary>
        private void PopulateUserRoles()
        {
            DataTable dtUserRoles;
            if ((DataTable)ViewState[WebConstants.TBL_USER_PROJECT_ROLES] == null)
            {
                DTO.Users userData = new DTO.Users();
                userData.UserID = userID;

                dtUserRoles = BO.BusinessObjects.GetUserProjectRoles(userData).Tables[0];

                DataRow newDr = dtUserRoles.NewRow();
                newDr[WebConstants.COL_PROJECT_ROLE_ID] = CONST_ZERO;
                newDr[WebConstants.COL_PROJECT_ROLE] = VAL_ALL;
                dtUserRoles.Rows.InsertAt(newDr, 0);

                ViewState[WebConstants.TBL_USER_PROJECT_ROLES] = dtUserRoles;

                Session[WebConstants.SESSION_IS_ADMIN] = false;
                if (dtUserRoles.Select(WebConstants.COL_PROJECT_ROLE + " = '" + WebConstants.VAL_ROLE_ADMIN + "'").Length > 0)
                    Session[WebConstants.SESSION_IS_ADMIN] = true;
            }
            else
                dtUserRoles = (DataTable)ViewState[WebConstants.TBL_USER_PROJECT_ROLES];

            DropDownListUserRoles.DataSource = dtUserRoles;
            DropDownListUserRoles.DataValueField = WebConstants.COL_PROJECT_ROLE_ID;
            DropDownListUserRoles.DataTextField = WebConstants.COL_PROJECT_ROLE;
            DropDownListUserRoles.DataBind();

            if (Session[WebConstants.SESSION_PROJECT_ROLE_ID] == null)
                Session[WebConstants.SESSION_PROJECT_ROLE_ID] = DropDownListUserRoles.SelectedValue;
            else
                DropDownListUserRoles.SelectedIndex = DropDownListUserRoles.Items.IndexOf(DropDownListUserRoles.Items.FindByValue(Session[WebConstants.SESSION_PROJECT_ROLE_ID].ToString()));
            //Session[WebConstants.SESSION_PROJECT_ROLE_ID] = Session[WebConstants.SESSION_NEW_PROJECT_ROLE_ID];

            SetAdminAccess();
        }

        /// <summary>
        /// PopulateTypeDetails
        /// </summary>
        private DataTable PopulateTypeDetails(string tableName, DropDownList dropDownList)
        {
            DTO.MasterData masterData = new DTO.MasterData();
            masterData.TableName = tableName;

            DataTable typeDetailsDataTable = BO.BusinessObjects.GetDetailsforMasterData(masterData).Tables[0];

            typeDetailsDataTable.Rows.RemoveAt(0);

            if (dropDownList != null)
            {
                dropDownList.DataSource = typeDetailsDataTable;
                dropDownList.DataValueField = WebConstants.COL_LOCALE_ID;
                dropDownList.DataTextField = WebConstants.COL_LOCALE;
                dropDownList.DataBind();
            }

            return typeDetailsDataTable;
        }

        /// <summary>
        /// SetMenuAndRoles
        /// </summary>
        /// <param name="userID"></param>
        private void SetMenuAndRoles(string userID)
        {
            PopulateUserRoles();
            PopulateScreenLabels();

            DTO.Users userData = new DTO.Users();
            userData.UserID = userID;
            userData.ProjectRoleID = DropDownListUserRoles.SelectedValue;

            if (Session[WebConstants.SESSION_PRODUCT_ID] != null)
                userData.ProductID = Session[WebConstants.SESSION_PRODUCT_ID].ToString();
            if (Session[WebConstants.SESSION_PRODUCT_VERSION_ID] != null)
                userData.ProductVersionID = Session[WebConstants.SESSION_PRODUCT_VERSION_ID].ToString();

            DataTable dtScreenAccess = BO.BusinessObjects.GetUserScreenAccess(userData).Tables[0];

            if (!(bool)Session[WebConstants.SESSION_IS_ADMIN] && Session[WebConstants.SESSION_PRODUCT_ID] == null)
            {
                DataView dvScreenAccess = new DataView(dtScreenAccess);
                dvScreenAccess.RowFilter = STR_PRODUCT_TYPE_FILTER;
                dtScreenAccess = dvScreenAccess.ToTable();
            }

            Session[WebConstants.SESSION_CACHE_SCREEN_ACCESS_DATA_TABLE] = dtScreenAccess;

            string filterCriteria = ScreenListFilterCriteria(CONST_ZERO);

            if (Session[WebConstants.SESSION_DATA_TREEVIEW_NODE_COLLECTION] != null)
            {
                List<TreeNode> treeList = (List<TreeNode>)Session[WebConstants.SESSION_DATA_TREEVIEW_NODE_COLLECTION];
                foreach (TreeNode node in treeList)
                    DataTreeview.Nodes.Add(node);

                SetLeftPanelHeight((List<TreeNode>)Session[WebConstants.SESSION_DATA_TREEVIEW_NODE_COLLECTION], 0);
            }
            else
            {
                CreateTreeView(dtScreenAccess, null, filterCriteria, 0);
                SetLeftPanelHeight(null, dtScreenAccess.Rows.Count);
            }
        }

        /// <summary>
        /// SetAdminAccess
        /// </summary>
        /// <param name="userID"></param>
        private void SetAdminAccess()
        {
            Session[WebConstants.SESSION_IS_ADMIN] = false;
            if (DropDownListUserRoles.SelectedValue == CONST_ZERO.ToString())
            {
                foreach (ListItem item in DropDownListUserRoles.Items)
                    if (item.Text.ToUpper() == WebConstants.VAL_ROLE_ADMIN)
                    {
                        Session[WebConstants.SESSION_IS_ADMIN] = true;
                        break;
                    }
            }
            else if (DropDownListUserRoles.SelectedItem.Text.ToUpper() == (WebConstants.VAL_ROLE_ADMIN))
                Session[WebConstants.SESSION_IS_ADMIN] = true;

        }

        /// <summary>
        /// CreateTreeView
        /// </summary>
        /// <param name="dtScreenAccess"></param>
        /// <param name="filterCriteria"></param>
        /// <returns></returns>
        private void CreateTreeView(DataTable dtScreenAccess, TreeNode parentNode, string filterCriteria, int level)
        {
            DataView dvLevel = dtScreenAccess.DefaultView;

            dvLevel.RowFilter = filterCriteria;
            dvLevel.Sort = STR_SCREEN_SORTING_CRITERIA;

            TreeNode node = null;

            foreach (DataRow dr in dvLevel.ToTable().Rows)
            {
                string screenID = dr[WebConstants.COL_SCREEN_ID].ToString();
                DataRow[] drScreenLabel = dtMenuLabels.Select(WebConstants.COL_SCREEN_IDENTIFIER + " = '" + dr[WebConstants.COL_SCREEN_IDENTIFIER].ToString() + "'");

                string screenIdentifier = dr[WebConstants.COL_SCREEN_IDENTIFIER].ToString();
                if (drScreenLabel.Length == 1)
                    node = AddNodes(screenIdentifier, drScreenLabel[0][WebConstants.COL_SCREEN_LOCALIZED_VALUE].ToString());
                else
                    node = AddNodes(screenIdentifier, dr[WebConstants.COL_SCREEN_VALUE].ToString());

                string newFilterCriteria = ScreenListFilterCriteria(screenID);
                if (dtScreenAccess.Select(newFilterCriteria).Length > 0)
                    CreateTreeView(dtScreenAccess, node, newFilterCriteria, level + 1);

                if (level != 0)
                    parentNode.ChildNodes.Add(node);
                else
                {
                    node.Collapse();
                    DataTreeview.Nodes.Add(node);
                }
            }

        }

        /// <summary>
        /// ScreenListFilterCriteria
        /// </summary>
        /// <param name="screenID"></param>
        /// <returns></returns>
        private string ScreenListFilterCriteria(string screenID)
        {
            int pageType = -1;

            if (HttpContext.Current.Request.Url.AbsoluteUri.Contains(WebConstants.PAGE_REPORT))
                pageType = 1;
            else if (HttpContext.Current.Request.Url.AbsoluteUri.Contains(WebConstants.PAGE_LOCALIZATION_INSIGHT))
                pageType = 0;

            if (pageType != -1)
                return STR_PARENT_SCREEN_ID_FILTER + " = " + screenID + " AND (" + STR_INDEX_GROUP_FILTER + " = " + pageType.ToString() + " OR " + STR_INDEX_GROUP_FILTER + " = " + " -1)";
            else
                return STR_PARENT_SCREEN_ID_FILTER + " = " + screenID;
        }

        /// <summary>
        /// AddNodes
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="menu"></param>
        /// <returns></returns>
        private TreeNode AddNodes(string identifier, string menu)
        {
            TreeNode node = new TreeNode();
            node.Value = identifier;
            node.Text = menu;

            return node;
        }

        /// <summary>
        /// SetControlsVisisbility
        /// </summary>
        /// <param name="isLogged"></param>
        private void SetControlsVisibility(bool isLogged)
        {
            PanelFooter.Visible = isLogged;
            LabelLoginID.Visible = isLogged;
            HomeLinkButton.Visible = isLogged;
            //HomeTextLinkButton.Visible = isLogged;
            //HelpLinkButton.Visible = isLogged;
            //HelpTextLinkButton.Visible = isLogged;
            LogoutLinkButton.Visible = isLogged;
            //LogOutTextLinkButton.Visible = isLogged;

            if (HttpContext.Current.Request.Url.AbsoluteUri.Contains(WebConstants.PAGE_INDEX))
            {
                LeftPanel.Visible = false;
                TDLeftPanel.Visible = false;
                PanelFooter.Visible = false;
            }
        }

        /// <summary>
        /// SetScreenAccessAndLabels
        /// </summary>
        /// <param name="userID"></param>
        private void SetScreenAccessAndLabels()
        {
            DropDownListLocales.Visible = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings[WebConstants.SETTING_CHANGE_LOCALE]);

            DTO.Screens screenData = new DTO.Screens();
            screenData.ScreenIdentifier = WebConstants.SCREEN_SITE;
            screenData.LocaleID = DropDownListLocales.SelectedValue;

            if (screenData.LocaleID == "")
                screenData.LocaleID = WebConstants.DEF_VAL_LOCALE_ID;

            screenData.LocaleID = Common.Common.GetLocaleForScreenLabels(screenData.LocaleID, (DataTable)ViewState[WebConstants.TBL_LOCALES])[WebConstants.COL_LOCALE_ID].ToString();

            DataTable dtScreenLabels = BO.BusinessObjects.GetScreenLocalizedLabels(screenData).Tables[0];

            SetScreenLabels(dtScreenLabels);
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetLeftPanelHeight(List<TreeNode> treeList, int height)
        {
            int panelheight = VAL_LEFT_PANEL_MIN_HEIGHT;

            if (treeList != null)
            {
                height = 0;
                foreach (TreeNode node in treeList)
                    height = height + GetTreeListHeight(node);
            }

            panelheight = (VAL_LEFT_PANEL_MAX_HEIGHT * height) / VAL_LEFT_PANEL_MAX_COUNT;

            if (panelheight > VAL_LEFT_PANEL_MIN_HEIGHT)
                DataTreeview.Height = Unit.Pixel(panelheight);
            else
                DataTreeview.Height = Unit.Pixel(VAL_LEFT_PANEL_MIN_HEIGHT);
        }

        /// <summary>
        /// GetTreeListHeight
        /// </summary>
        /// <param name="VAL_LEFT_PANEL_MIN_HEIGHT"></param>
        /// <returns></returns>
        private int GetTreeListHeight(TreeNode node)
        {
            int counter = 0;

            counter++;
            foreach (TreeNode childNode in node.ChildNodes)
                counter = counter + GetTreeListHeight(childNode);

            return counter;
        }

        /// <summary>
        /// SetScreenLabels
        /// </summary>
        /// <param name="dtScreenLabels"></param>
        private void SetScreenLabels(DataTable dtScreenLabels)
        {
            if (Session[WebConstants.SESSION_LOCALE_ID] != null && Session[WebConstants.SESSION_LOCALE_ID].ToString() != WebConstants.DEF_VAL_LOCALE_ID)
            {
                LabelLoginID.Text = LabelLoginID.Text.ToUpper();

                HomeLinkButton.ToolTip = COM.GetScreenLocalizedLabel(dtScreenLabels, HomeLinkButton.ToolTip);
                LogoutLinkButton.ToolTip = COM.GetScreenLocalizedLabel(dtScreenLabels, LogoutLinkButton.ToolTip);

                foreach (MenuItem menuItm in masterMenu.Items)
                    menuItm.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, menuItm.Value);
            }
        }

        #endregion
    }
}
