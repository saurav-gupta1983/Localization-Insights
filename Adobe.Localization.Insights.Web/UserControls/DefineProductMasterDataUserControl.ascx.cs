using System;
using System.Data;
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
    /// DefineProductMasterDataUserControl
    /// </summary>
    public partial class DefineProductMasterDataUserControl : System.Web.UI.UserControl
    {
        #region Variables

        private Control controlFired;

        private DataTable dtProductVersionLocales = new DataTable();
        private DataTable dtProductVersionPlatforms = new DataTable();
        private DataTable dtProjectBuildDetails;
        private DataTable dtProjectBuildLocales;
        private DataTable dtScreenLabels;

        private ArrayList columnNames;

        private DTO.Header dataHeader = new DTO.Header();
        private bool isReadOnly = true;
        private bool isProductVersionActive = false;

        private const string STR_NON_ACTIVE = "Not Active";
        private const string STR_IS_ACTIVE = "1";
        private const string STR_SPACE = "&nbsp;";
        private const string STR_ZERO = "0";
        private const string STR_TAB_PANEL_DEFINE_SPRINTS = "TabPanelDefineSprints";
        private const string STR_TAB_PANEL_DEFINE_BUILDS = "TabPanelDefineBuilds";
        private const string STR_TAB_PANEL_LOCALES_AND_PLATFORMS = "TabPanelLocalesandPlatforms";

        private const int CONST_ZERO = 0;
        private const int NUM_EACH_GRID_ROW_DISPLAY_HEIGHT = 20;
        private const int TAB_INDEX_LOCALES_AND_Platforms = 0;
        private const int TAB_INDEX_DEFINE_SPRINTS = 1;
        private const int TAB_INDEX_DEFINE_BUILDS = 2;
        private const int TAB_INDEX_PROJECT_BUILD_LOCALES = 3;

        private const int COL_GRID_PRODUCT_SPRINT_SAVE_UPDATE_NO = 6;
        private const int COL_GRID_PRODUCT_SPRINT_CANCEL_DELETE_NO = 7;
        private const int COL_GRID_PRODUCT_BUILDS_SAVE_UPDATE_NO = 6;
        private const int COL_GRID_PRODUCT_BUILDS_CANCEL_DELETE_NO = 7;

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
                ViewState[WebConstants.COL_STR_COLUMN_NAMES] = GetColumnNames(WebConstants.TBL_PRODUCT_SPRINTS);
                PopulateData();
            }
            else
            {
                controlFired = GetPostBackControl(Page);
            }
            LabelMessage.Text = "";

            columnNames = (ArrayList)ViewState[WebConstants.COL_STR_COLUMN_NAMES];
            dtProductVersionLocales = (DataTable)ViewState[WebConstants.TBL_PRODUCT_LOCALES];
            isProductVersionActive = (bool)ViewState[WebConstants.STR_IS_PRODUCT_VERSION_ACTIVE];

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
                if (controlFired.ClientID.Contains(STR_TAB_PANEL_DEFINE_SPRINTS))
                    TabContainerPopulateProductMasterDataDetails.ActiveTabIndex = TAB_INDEX_DEFINE_SPRINTS;
                else if (controlFired.ClientID.Contains(STR_TAB_PANEL_DEFINE_BUILDS))
                    TabContainerPopulateProductMasterDataDetails.ActiveTabIndex = TAB_INDEX_DEFINE_BUILDS;
                else if (controlFired.ClientID.Contains(STR_TAB_PANEL_LOCALES_AND_PLATFORMS))
                    TabContainerPopulateProductMasterDataDetails.ActiveTabIndex = TAB_INDEX_LOCALES_AND_Platforms;
                else
                    TabContainerPopulateProductMasterDataDetails.ActiveTabIndex = TAB_INDEX_PROJECT_BUILD_LOCALES;
            }
        }

        #endregion

        #region Tab Locales and Platforms

        #region Grid Events

        /// <summary>
        /// GridViewLocales_RowDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewLocales_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label LabelLocaleID = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_LOCALE_ID);

                CheckBox CheckBoxLocalesSelected = (CheckBox)e.Row.FindControl(WebConstants.CONTROL_CHECKBOX_LOCALES_SELECTED);

                if (dtProductVersionLocales.Select(WebConstants.COL_LOCALE_ID + " = " + LabelLocaleID.Text).Length > CONST_ZERO)
                    CheckBoxLocalesSelected.Checked = true;

                if (isReadOnly)
                    CheckBoxLocalesSelected.Enabled = false;
            }
        }

        /// <summary>
        /// GridViewPlatforms_RowDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewPlatforms_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label LabelPlatformID = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_PLATFORM_ID);
                CheckBox CheckBoxPlatformSelected = (CheckBox)e.Row.FindControl(WebConstants.CONTROL_CHECKBOX_PLATFORMS_SELECTED);
                TextBox TextBoxPlatformPriority = (TextBox)e.Row.FindControl(WebConstants.CONTROL_TEXTBOX_PLATFORM_PRIORITY);

                DataRow[] dr = dtProductVersionPlatforms.Select(WebConstants.COL_PLATFORM_ID + " = " + LabelPlatformID.Text);
                if (dr.Length > CONST_ZERO)
                {
                    Label LabelProductPlatformID = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_PRODUCT_PLATFORM_ID);
                    LabelProductPlatformID.Text = dr[0][WebConstants.COL_PRODUCT_PLATFORM_ID].ToString();

                    CheckBoxPlatformSelected.Checked = true;

                    TextBoxPlatformPriority.Text = dr[0][WebConstants.COL_PRIORITY].ToString();
                }

                if (isReadOnly)
                {
                    CheckBoxPlatformSelected.Enabled = false;
                    TextBoxPlatformPriority.Enabled = false;
                }
            }
        }

        #endregion

        #region Button Events

        /// <summary>
        /// ButtonUpdateLocalesandPlatforms_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonUpdateLocalesandPlatforms_Click(object sender, EventArgs e)
        {
            DTO.Product productData = new DTO.Product();
            productData.DataHeader = dataHeader;
            productData.ProductVersionID = DropDownListProductVersion.SelectedValue;
            productData.LocaleIDCollection = new ArrayList();
            productData.PlatformIDCollection = new ArrayList();
            productData.ProductPlatformIDCollection = new ArrayList();
            productData.ProductPlatformPriorityCollection = new ArrayList();

            //Locales
            for (int i = 0; i < GridViewLocales.Rows.Count; i++)
            {
                GridViewRow row = GridViewLocales.Rows[i];
                if (((CheckBox)row.FindControl(WebConstants.CONTROL_CHECKBOX_LOCALES_SELECTED)).Checked)
                {
                    Label LabelLocaleID = (Label)row.FindControl(WebConstants.CONTROL_LABEL_LOCALE_ID);
                    productData.LocaleIDCollection.Add(LabelLocaleID.Text);
                }
            }

            //Platforms
            for (int i = 0; i < GridViewPlatforms.Rows.Count; i++)
            {
                GridViewRow row = GridViewPlatforms.Rows[i];
                if (((CheckBox)row.FindControl(WebConstants.CONTROL_CHECKBOX_PLATFORMS_SELECTED)).Checked)
                {
                    Label LabelPlatformID = (Label)row.FindControl(WebConstants.CONTROL_LABEL_PLATFORM_ID);
                    productData.PlatformIDCollection.Add(LabelPlatformID.Text);

                    Label LabelProductPlatformID = (Label)row.FindControl(WebConstants.CONTROL_LABEL_PRODUCT_PLATFORM_ID);
                    productData.ProductPlatformIDCollection.Add(LabelProductPlatformID.Text);

                    TextBox TextBoxPlatformPriority = (TextBox)row.FindControl(WebConstants.CONTROL_TEXTBOX_PLATFORM_PRIORITY);
                    productData.ProductPlatformPriorityCollection.Add(TextBoxPlatformPriority.Text);
                }
            }

            if (BO.BusinessObjects.UpdateProductVersionLocalesAndPlatforms(productData))
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_SUCCESS_SAVE_MESSAGE);
            else
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_SAVE_MESSAGE);

            PopulateLocalesAndPlatforms();
            PopulateProjectBuildLocales();
        }

        #endregion

        #region DropDownList events

        /// <summary>
        /// DropDownListProductVersion_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListProductVersion_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateLocalesAndPlatforms();
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// PopulateLocalesAndPlatforms
        /// </summary>
        private void PopulateLocalesAndPlatforms()
        {
            DTO.Product productData = new DTO.Product();
            productData.ProductVersionID = DropDownListProductVersion.SelectedValue;

            if (DropDownListProductVersion.Items.Count == 0)
                productData.ProductVersionID = "-1";

            PopulateLocales(productData);
            PopulatePlatforms(productData);
        }

        /// <summary>
        /// PopulateLocales
        /// </summary>
        private void PopulateLocales(DTO.Product productData)
        {
            DataTable dtLocales = BO.BusinessObjects.GetAllLocales().Tables[0];
            dtProductVersionLocales = BO.BusinessObjects.GetProductVersionLocales(productData).Tables[0];
            dtProductVersionLocales = COM.AddSequenceColumnToDataTable(dtProductVersionLocales);
            ViewState[WebConstants.TBL_PRODUCT_LOCALES] = dtProductVersionLocales;

            GridViewLocales.DataSource = dtLocales;
            GridViewLocales.DataBind();
        }

        /// <summary>
        /// PopulatePlatforms
        /// </summary>
        /// <param name="productData"></param>
        private void PopulatePlatforms(DTO.Product productData)
        {
            DataTable dtPlatforms = BO.BusinessObjects.GetAllPlatforms().Tables[0];
            dtPlatforms.Columns.Add(WebConstants.COL_PRODUCT_PLATFORM_ID);

            dtProductVersionPlatforms = BO.BusinessObjects.GetProductVersionPlatforms(productData).Tables[0];

            GridViewPlatforms.DataSource = dtPlatforms;
            GridViewPlatforms.DataBind();
        }

        /// <summary>
        /// PopulateProductVersion
        /// </summary>
        private void PopulateProductVersion()
        {
            DTO.Product productData = new DTO.Product();
            productData.ProductID = Session[WebConstants.SESSION_PRODUCT_ID].ToString();
            productData.ProductVersionID = Session[WebConstants.SESSION_PRODUCT_VERSION_ID].ToString();
            //productData.IsActive = STR_VAL_ISACTIVE;

            DataTable dtProductVersion = BO.BusinessObjects.GetProductVersion(productData).Tables[0];

            LabelProductValue.Text = dtProductVersion.Rows[0][WebConstants.COL_PRODUCT].ToString();

            DropDownListProductVersion.DataSource = dtProductVersion;
            DropDownListProductVersion.DataValueField = WebConstants.COL_PRODUCT_VERSION_ID;
            DropDownListProductVersion.DataTextField = WebConstants.COL_PRODUCT_VERSION;
            DropDownListProductVersion.DataBind();

            //DropDownListProductVersion.SelectedIndex = DropDownListProductVersion.Items.IndexOf(DropDownListProductVersion.Items.FindByValue(Session[WebConstants.SESSION_PRODUCT_VERSION_ID].ToString()));

            if (dtProductVersion.Rows[0][WebConstants.COL_PRODUCT_VERSION_CODE].ToString() != "")
                LabelProductVersionValue.Text = dtProductVersion.Rows[0][WebConstants.COL_PRODUCT_VERSION_CODE].ToString() + " ( " + dtProductVersion.Rows[0][WebConstants.COL_PRODUCT_VERSION].ToString() + " ) ";
            else
                LabelProductVersionValue.Text = dtProductVersion.Rows[0][WebConstants.COL_PRODUCT_VERSION].ToString();

            if (dtProductVersion.Rows[0][WebConstants.COL_PRODUCT_VERSION_ACTIVE].ToString() == "0")
                LabelProductVersionValue.Text += " - " + COM.GetScreenLocalizedLabel(dtScreenLabels, STR_NON_ACTIVE);
            else
                isProductVersionActive = true;

            ViewState[WebConstants.STR_IS_PRODUCT_VERSION_ACTIVE] = isProductVersionActive;
        }

        #endregion

        #endregion

        #region Tab Define Sprints

        #region Grid Events

        /// <summary>
        /// GridViewDefineProductSprints_RowDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewDefineProductSprints_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                SetGridLinkButtonsDisplayText(e.Row);

                Calendar DateCalendarSprintStartDate = (Calendar)e.Row.FindControl(WebConstants.CONTROL_DATECALENDAR_SPRINT_START_DATE);
                SetDateCalendar(DateCalendarSprintStartDate);

                Calendar DateCalendarSprintEndDate = (Calendar)e.Row.FindControl(WebConstants.CONTROL_DATECALENDAR_SPRINT_END_DATE);
                SetDateCalendar(DateCalendarSprintEndDate);

                if (e.Row.Cells[CONST_ZERO].Text.ToString() == CONST_ZERO.ToString() || e.Row.Cells[CONST_ZERO].Text.ToString() == STR_SPACE)
                {
                    e.Row.Cells[CONST_ZERO].Text = "";
                    SetProductSprintLinkButtons(e.Row, true);
                }
                else
                {
                    Label LabelSprintStartDate = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_SPRINT_START_DATE);
                    if (LabelSprintStartDate.Text != "")
                        LabelSprintStartDate.Text = DateTime.Parse(LabelSprintStartDate.Text).ToShortDateString();

                    Label LabelSprintEndDate = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_SPRINT_END_DATE);
                    if (LabelSprintEndDate.Text != "")
                        LabelSprintEndDate.Text = DateTime.Parse(LabelSprintEndDate.Text).ToShortDateString();
                }
            }
        }

        /// <summary>
        /// GridViewDefineProductSprints_RowUpdating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewDefineProductSprints_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
        }

        /// <summary>
        /// GridViewDefineProductSprints_RowCancelingEdit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewDefineProductSprints_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
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
            GridViewRow detailsGridViewRow = GridViewDefineProductSprints.Rows[index];

            DTO.Product productData = new DTO.Product();
            productData.DataHeader = dataHeader;
            productData.ProductVersionID = DropDownListProductVersion.SelectedValue;

            TextBox TextBoxSprint = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_PRODUCT_SPRINT);
            TextBox TextBoxSprintDetails = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_PRODUCT_SPRINT_DETAILS);
            Calendar DateCalendarSprintStartDate = (Calendar)detailsGridViewRow.FindControl(WebConstants.CONTROL_DATECALENDAR_SPRINT_START_DATE);
            Calendar DateCalendarSprintEndDate = (Calendar)detailsGridViewRow.FindControl(WebConstants.CONTROL_DATECALENDAR_SPRINT_END_DATE);

            if (TextBoxSprint.Text == "")
            {
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.SCREEN_PRODUCT_SPRINT_MANDATORY);
                return;
            }
            productData.ProductSprint = TextBoxSprint.Text;
            productData.ProductSprintDetails = TextBoxSprintDetails.Text;

            if (DateCalendarSprintStartDate.Date == "")
            {
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.SCREEN_START_DATE);
                return;
            }
            productData.StartDate = DateCalendarSprintStartDate.Date;

            if (DateCalendarSprintEndDate.Date == "")
            {
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.SCREEN_END_DATE);
                return;
            }
            productData.EndDate = DateCalendarSprintEndDate.Date;

            if (DateTime.Parse(DateCalendarSprintStartDate.Date) > DateTime.Parse(DateCalendarSprintEndDate.Date))
            {
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.SCREEN_DATE_CHECK);
                return;
            }

            if (detailsGridViewRow.Cells[CONST_ZERO].Text != "")
            {
                Label LabelProductSprintID = (Label)detailsGridViewRow.FindControl(WebConstants.CONTROL_LABEL_PRODUCT_SPRINT_ID);
                productData.ProductSprintID = LabelProductSprintID.Text;
            }

            if (BO.BusinessObjects.AddUpdateProductSprints(productData))
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_SUCCESS_SAVE_MESSAGE);
            else
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_SAVE_MESSAGE);

            PopulateProductSprintsData();
        }

        /// <summary>
        /// LinkButtonCancelDetails_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonCancelDetails_Click(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow detailsGridViewRow = GridViewDefineProductSprints.Rows[index];

            if (detailsGridViewRow.Cells[CONST_ZERO].Text == "")
            {
                TextBox TextBoxSprint = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_PRODUCT_SPRINT);
                TextBoxSprint.Text = "";
                TextBox TextBoxSprintDetails = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_PRODUCT_SPRINT_DETAILS);
                TextBoxSprintDetails.Text = "";
            }
            else
            {
                SetProductSprintLinkButtons(detailsGridViewRow, false);
            }
        }

        /// <summary>
        /// LinkButtonUpdateDetails_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonUpdateDetails_Click(object sender, CommandEventArgs e)
        {
            SetProductSprintLinkButtons(GridViewDefineProductSprints.Rows[Convert.ToInt32(e.CommandArgument)], true);
        }

        /// <summary>
        /// LinkButtonDeleteDetails_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonDeleteDetails_Click(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow detailsGridViewRow = GridViewDefineProductSprints.Rows[index];

            DTO.MasterData masterData = new DTO.MasterData();
            masterData.TableName = WebConstants.TBL_PRODUCT_SPRINTS;
            masterData.ColumnNames = columnNames;

            Label LabelProductSprintID = (Label)detailsGridViewRow.FindControl(WebConstants.CONTROL_LABEL_PRODUCT_SPRINT_ID);
            masterData.MasterDataID = LabelProductSprintID.Text;

            if (BO.BusinessObjects.AddUpdateDetailsforMasterData(masterData))
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_SUCCESS_DELETE_MESSAGE);
            else
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_DELETE_MESSAGE); ;

            PopulateProductSprintsData();
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// PopulateProductSprintsData
        /// </summary>
        private void PopulateProductSprintsData()
        {
            DTO.Product productData = new DTO.Product();
            productData.ProductVersionID = DropDownListProductVersion.SelectedValue;

            DataTable dtProductSprints = BO.BusinessObjects.GetProductSprints(productData).Tables[0];

            if (!isReadOnly)
                dtProductSprints = COM.AddRowandColumntoDataTable(dtProductSprints);

            GridViewDefineProductSprints.DataSource = dtProductSprints;
            GridViewDefineProductSprints.DataBind();
        }

        /// <summary>
        /// SetProductSprintLinkButtons
        /// </summary>
        /// <param name="gridViewRow"></param>
        /// <param name="isSave"></param>
        private void SetProductSprintLinkButtons(GridViewRow gridViewRow, bool isSave)
        {
            LinkButton LinkButtonSave = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_SAVE);
            LinkButtonSave.Visible = isSave;
            LinkButton LinkButtonCancel = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_CANCEL);
            LinkButtonCancel.Visible = isSave;
            LinkButton LinkButtonUpdate = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_UPDATE);
            LinkButtonUpdate.Visible = (!isSave);
            LinkButton LinkButtonDelete = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_DELETE);
            LinkButtonDelete.Visible = (!isSave);

            //ProductSprints
            SetLinkButtonsForTextBoxes(gridViewRow, WebConstants.CONTROL_LABEL_PRODUCT_SPRINT, WebConstants.CONTROL_TEXTBOX_PRODUCT_SPRINT, isSave);

            //ProductSprintDetails
            SetLinkButtonsForTextBoxes(gridViewRow, WebConstants.CONTROL_LABEL_PRODUCT_SPRINT_DETAILS, WebConstants.CONTROL_TEXTBOX_PRODUCT_SPRINT_DETAILS, isSave);

            //Phase Start Date
            SetLinkButtonsForDateCalendar(gridViewRow, WebConstants.CONTROL_LABEL_SPRINT_START_DATE, WebConstants.CONTROL_DATECALENDAR_SPRINT_START_DATE, isSave);

            //Phase End Date
            SetLinkButtonsForDateCalendar(gridViewRow, WebConstants.CONTROL_LABEL_SPRINT_END_DATE, WebConstants.CONTROL_DATECALENDAR_SPRINT_END_DATE, isSave);

        }

        #endregion

        #endregion

        #region Tab Define Product Builds

        #region Grid Events

        /// <summary>
        /// GridViewProductBuilds_RowDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewProductBuilds_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                SetGridLinkButtonsDisplayText(e.Row);

                if (e.Row.Cells[CONST_ZERO].Text.ToString() == CONST_ZERO.ToString() || e.Row.Cells[CONST_ZERO].Text.ToString() == STR_SPACE)
                {
                    e.Row.Cells[CONST_ZERO].Text = "";
                    SetProductBuildsLinkButtons(e.Row, true);
                }
                else
                {
                    Label LabelProjectBuildDetailID = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_PROJECT_BUILD_DETAIL_ID);
                    CheckBox CheckBoxReleaseBuildSelected = (CheckBox)e.Row.FindControl(WebConstants.CONTROL_CHECKBOX_RELEASE_BUILD_SELECTED);

                    if (dtProjectBuildDetails.Select(WebConstants.COL_PROJECT_BUILD_DETAIL_ID + " = " + LabelProjectBuildDetailID.Text + " AND " + WebConstants.COL_PROJECT_BUILD_IS_RELEASE + "=1").Length > CONST_ZERO)
                        CheckBoxReleaseBuildSelected.Checked = true;

                    if (isReadOnly)
                        CheckBoxReleaseBuildSelected.Enabled = false;
                }
            }
        }

        /// <summary>
        /// GridViewProductBuilds_RowUpdating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewProductBuilds_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
        }

        /// <summary>
        /// GridViewProductBuilds_RowCancelingEdit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewProductBuilds_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
        }

        #endregion

        #region GridView Button Click Events

        /// <summary>
        /// ProductBuildsLinkButtonSaveDetails_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ProductBuildsLinkButtonSaveDetails_Click(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow detailsGridViewRow = GridViewProductBuilds.Rows[index];

            DTO.Product productData = new DTO.Product();
            productData.DataHeader = dataHeader;
            productData.ProductVersionID = DropDownListProductVersion.SelectedValue;

            TextBox TextBoxBuildCode = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_PROJECT_BUILD_CODE);
            TextBox TextBoxBuildDetails = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_PROJECT_BUILD_DETAILS);
            CheckBox CheckBoxReleaseBuildSelected = (CheckBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_CHECKBOX_RELEASE_BUILD_SELECTED);

            if (TextBoxBuildCode.Text == "")
            {
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.SCREEN_PRODUCT_BUILD_CODE_MANDATORY);
                return;
            }
            productData.ProjectBuildCode = TextBoxBuildCode.Text;
            productData.ProjectBuildDetails = TextBoxBuildDetails.Text;
            productData.IsReleaseBuild = CheckBoxReleaseBuildSelected.Checked ? "1" : "0";

            if (detailsGridViewRow.Cells[CONST_ZERO].Text != "")
            {
                Label LabelProjectBuildDetailID = (Label)detailsGridViewRow.FindControl(WebConstants.CONTROL_LABEL_PROJECT_BUILD_DETAIL_ID);
                productData.ProjectBuildDetailID = LabelProjectBuildDetailID.Text;
            }

            if (BO.BusinessObjects.AddUpdateProjectBuildDetails(productData))
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_SUCCESS_SAVE_MESSAGE);
            else
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_SAVE_MESSAGE);

            PopulateProjectBuildDetails();
            PopulateProjectBuildLocales();
        }

        /// <summary>
        /// ProductBuildsLinkButtonCancelDetails_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ProductBuildsLinkButtonCancelDetails_Click(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow detailsGridViewRow = GridViewProductBuilds.Rows[index];

            if (detailsGridViewRow.Cells[CONST_ZERO].Text == "")
            {
                TextBox TextBoxSprint = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_PRODUCT_SPRINT);
                TextBoxSprint.Text = "";
                TextBox TextBoxSprintDetails = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_PRODUCT_SPRINT_DETAILS);
                TextBoxSprintDetails.Text = "";
                CheckBox CheckBoxReleaseBuildSelected = (CheckBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_CHECKBOX_RELEASE_BUILD_SELECTED);
                CheckBoxReleaseBuildSelected.Checked = false;
            }
            else
            {
                SetProductBuildsLinkButtons(detailsGridViewRow, false);
            }
        }

        /// <summary>
        /// ProductBuildsLinkButtonUpdateDetails_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ProductBuildsLinkButtonUpdateDetails_Click(object sender, CommandEventArgs e)
        {
            SetProductBuildsLinkButtons(GridViewProductBuilds.Rows[Convert.ToInt32(e.CommandArgument)], true);
        }

        /// <summary>
        /// ProductBuildsLinkButtonDeleteDetails_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ProductBuildsLinkButtonDeleteDetails_Click(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow detailsGridViewRow = GridViewProductBuilds.Rows[index];

            DTO.Product productData = new DTO.Product();
            productData.DataHeader = dataHeader;

            Label LabelProjectBuildDetailID = (Label)detailsGridViewRow.FindControl(WebConstants.CONTROL_LABEL_PROJECT_BUILD_DETAIL_ID);
            productData.ProjectBuildDetailID = LabelProjectBuildDetailID.Text;

            if (BO.BusinessObjects.AddUpdateProjectBuildDetails(productData))
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_SUCCESS_DELETE_MESSAGE);
            else
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_DELETE_MESSAGE); ;

            PopulateProjectBuildDetails();
            PopulateProjectBuildLocales();
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// PopulateProjectBuildDetails
        /// </summary>
        private void PopulateProjectBuildDetails()
        {
            DTO.Product productData = new DTO.Product();
            productData.ProductVersionID = DropDownListProductVersion.SelectedValue;

            dtProjectBuildDetails = BO.BusinessObjects.GetProjectBuildDetails(productData).Tables[0];
            ViewState[WebConstants.TBL_PROJECT_BUILD_DETAILS] = dtProjectBuildDetails.Copy();

            if (!isReadOnly)
                dtProjectBuildDetails = COM.AddRowandColumntoDataTable(dtProjectBuildDetails);

            GridViewProductBuilds.DataSource = dtProjectBuildDetails;
            GridViewProductBuilds.DataBind();
        }

        /// <summary>
        /// SetProductBuildsLinkButtons
        /// </summary>
        /// <param name="gridViewRow"></param>
        /// <param name="isSave"></param>
        private void SetProductBuildsLinkButtons(GridViewRow gridViewRow, bool isSave)
        {
            LinkButton LinkButtonSave = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_SAVE);
            LinkButtonSave.Visible = isSave;
            LinkButton LinkButtonCancel = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_CANCEL);
            LinkButtonCancel.Visible = isSave;
            LinkButton LinkButtonUpdate = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_UPDATE);
            LinkButtonUpdate.Visible = (!isSave);
            LinkButton LinkButtonDelete = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_DELETE);
            LinkButtonDelete.Visible = (!isSave);

            //ProductSprints
            SetLinkButtonsForTextBoxes(gridViewRow, WebConstants.CONTROL_LABEL_PROJECT_BUILD_CODE, WebConstants.CONTROL_TEXTBOX_PROJECT_BUILD_CODE, isSave);

            //ProductSprintDetails
            SetLinkButtonsForTextBoxes(gridViewRow, WebConstants.CONTROL_LABEL_PROJECT_BUILD_DETAILS, WebConstants.CONTROL_TEXTBOX_PROJECT_BUILD_DETAILS, isSave);
        }

        #endregion

        #endregion

        #region Tab Project Build Locales

        #region Grid Events

        /// <summary>
        /// GridViewProjectBuildLocales_RowDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewProjectBuildLocales_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label LabelProductLocaleID = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_PRODUCT_LOCALE_ID);
                Label LabelProjectBuildDetails = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_PROJECT_BUILD_DETAILS);
                DropDownList DropDownListProjectBuild = (DropDownList)e.Row.FindControl(WebConstants.CONTROL_DROPDOWNLIST_PROJECT_BUILD);

                DropDownListProjectBuild.DataSource = dtProjectBuildDetails;
                DropDownListProjectBuild.DataValueField = WebConstants.COL_PROJECT_BUILD_DETAIL_ID;
                DropDownListProjectBuild.DataTextField = WebConstants.COL_PROJECT_BUILD;
                DropDownListProjectBuild.DataBind();

                DataRow[] drProjectBuildLocales = dtProjectBuildLocales.Select(WebConstants.COL_PRODUCT_LOCALE_ID + " = " + LabelProductLocaleID.Text);
                if (drProjectBuildLocales.Length > CONST_ZERO)
                {
                    Label LabelProjectBuildLocaleID = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_PROJECT_BUILD_LOCALE_ID);
                    LabelProjectBuildLocaleID.Text = drProjectBuildLocales[0][WebConstants.COL_PROJECT_BUILD_LOCALE_ID].ToString();

                    DropDownListProjectBuild.SelectedIndex = DropDownListProjectBuild.Items.IndexOf(DropDownListProjectBuild.Items.FindByValue(drProjectBuildLocales[0][WebConstants.COL_PROJECT_BUILD_DETAIL_ID].ToString()));
                    LabelProjectBuildDetails.Text = DropDownListProjectBuild.SelectedItem.Text;
                }

                if (isReadOnly)
                    LabelProjectBuildDetails.Visible = true;
                else
                    DropDownListProjectBuild.Visible = true;
            }
        }

        #endregion

        #region Button Events

        /// <summary>
        /// ButtonUpdateProjectBuildLocales_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonUpdateProjectBuildLocales_Click(object sender, EventArgs e)
        {
            DTO.Product productData = new DTO.Product();
            productData.DataHeader = dataHeader;
            productData.ProductVersionID = DropDownListProductVersion.SelectedValue;
            productData.LocaleIDCollection = new ArrayList();
            productData.ProjectBuildDetailIDCollection = new ArrayList();
            productData.ProjectBuildLocaleIDCollection = new ArrayList();

            //Locales
            for (int i = 0; i < GridViewProjectBuildLocales.Rows.Count; i++)
            {
                GridViewRow row = GridViewProjectBuildLocales.Rows[i];

                Label LabelProjectBuildLocaleID = (Label)row.FindControl(WebConstants.CONTROL_LABEL_PROJECT_BUILD_LOCALE_ID);
                Label LabelProductLocaleID = (Label)row.FindControl(WebConstants.CONTROL_LABEL_PRODUCT_LOCALE_ID);
                DropDownList DropDownListProjectBuild = (DropDownList)row.FindControl(WebConstants.CONTROL_DROPDOWNLIST_PROJECT_BUILD);

                productData.ProjectBuildLocaleIDCollection.Add(LabelProjectBuildLocaleID.Text);
                productData.ProjectBuildDetailIDCollection.Add(DropDownListProjectBuild.SelectedValue);
                productData.LocaleIDCollection.Add(LabelProductLocaleID.Text);
            }

            if (BO.BusinessObjects.AddUpdateProjectBuildLocales(productData))
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_SUCCESS_SAVE_MESSAGE);
            else
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_SAVE_MESSAGE);

            PopulateProjectBuildLocales();
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// PopulateProjectBuildLocales
        /// </summary>
        private void PopulateProjectBuildLocales()
        {
            dtProjectBuildDetails = (DataTable)ViewState[WebConstants.TBL_PROJECT_BUILD_DETAILS];

            DTO.Product productData = new DTO.Product();
            productData.ProductVersionID = DropDownListProductVersion.SelectedValue;

            if (DropDownListProductVersion.Items.Count == 0)
                productData.ProductVersionID = "-1";

            dtProjectBuildLocales = BO.BusinessObjects.GetProjectBuildLocales(productData).Tables[0];

            GridViewProjectBuildLocales.DataSource = dtProductVersionLocales;
            GridViewProjectBuildLocales.DataBind();
        }

        #endregion

        #endregion

        #region Private Functions

        /// <summary>
        /// PopulateData
        /// </summary>
        private void PopulateData()
        {
            PopulateProductVersion();
            PopulateLocalesAndPlatforms();
            PopulateProductSprintsData();
            PopulateProjectBuildDetails();
            PopulateProjectBuildLocales();
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

            if (LinkButtonSave != null)
                LinkButtonSave.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LinkButtonSave.Text);
            if (LinkButtonCancel != null)
            {
                if (gridViewRow.Cells[CONST_ZERO].Text.ToString() == STR_ZERO || gridViewRow.Cells[CONST_ZERO].Text.ToString() == STR_SPACE)
                    LinkButtonCancel.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.DEF_CONTROL_LINKBUTTON_CLEAR);
                else
                    LinkButtonCancel.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LinkButtonCancel.Text);
            }
            if (LinkButtonUpdate != null)
                LinkButtonUpdate.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LinkButtonUpdate.Text);
            if (LinkButtonDelete != null)
                LinkButtonDelete.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LinkButtonDelete.Text);
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
        /// SetDateCalendar
        /// </summary>
        private void SetDateCalendar(Calendar dateCalendar)
        {
            HtmlInputButton dateButton;
            HtmlGenericControl dateDiv;

            dateButton = (HtmlInputButton)dateCalendar.FindControl(WebConstants.CONTROL_DATECALENDAR_BUTTON);
            dateDiv = (HtmlGenericControl)dateCalendar.FindControl(WebConstants.CONTROL_DATECALENDAR_DIVCALENDAR);
            dateButton.Attributes.Add(WebConstants.CONTROL_DATECALENDAR_ONCLICK, "javascript:return OnClick('" + dateDiv.ClientID + "')");
        }

        /// <summary>
        /// GetColumnNames
        /// </summary>
        private ArrayList GetColumnNames(string tableName)
        {
            return BO.BusinessObjects.GetColumnNames(tableName);
        }

        #endregion

        #region Screen Access and Labels

        /// <summary>
        /// GetScreenAccess
        /// </summary>
        private void GetScreenAccess()
        {
            dataHeader.LoginID = Session[WebConstants.SESSION_LOGIN_ID].ToString();

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
            if (!isProductVersionActive)
                isReadOnly = true;

            if (isReadOnly)
            {
                ButtonUpdateLocalesandPlatforms.Enabled = false;

                GridViewDefineProductSprints.Columns[COL_GRID_PRODUCT_SPRINT_SAVE_UPDATE_NO].Visible = false;
                GridViewDefineProductSprints.Columns[COL_GRID_PRODUCT_SPRINT_CANCEL_DELETE_NO].Visible = false;

                GridViewDefineProductSprints.Columns[COL_GRID_PRODUCT_BUILDS_SAVE_UPDATE_NO].Visible = false;
                GridViewDefineProductSprints.Columns[COL_GRID_PRODUCT_BUILDS_CANCEL_DELETE_NO].Visible = false;

                ButtonUpdateProjectBuildLocales.Enabled = false;
            }


            if (GridViewLocales.Rows.Count > GridViewPlatforms.Rows.Count)
                AddFooterPadding(GridViewLocales.Rows.Count);
            else
                AddFooterPadding(GridViewPlatforms.Rows.Count);
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
                LabelProduct.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelProduct.Text);
                LabelProductVersion.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelProductVersion.Text);

                ButtonUpdateLocalesandPlatforms.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, ButtonUpdateLocalesandPlatforms.Text);
                ButtonUpdateProjectBuildLocales.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, ButtonUpdateLocalesandPlatforms.Text);

                TabPanelLocalesandPlatforms.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, TabPanelLocalesandPlatforms.HeaderText);
                TabPanelDefineSprints.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, TabPanelDefineSprints.HeaderText);
                TabPanelDefineBuilds.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, TabPanelDefineBuilds.HeaderText);
                TabPanelProjectBuildLocales.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, TabPanelProjectBuildLocales.HeaderText);

                foreach (DataControlField field in GridViewDefineProductSprints.Columns)
                    if (field.HeaderText != "")
                        field.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, field.HeaderText);

                foreach (DataControlField field in GridViewLocales.Columns)
                    if (field.HeaderText != "")
                        field.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, field.HeaderText);

                foreach (DataControlField field in GridViewPlatforms.Columns)
                    if (field.HeaderText != "")
                        field.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, field.HeaderText);

                foreach (DataControlField field in GridViewProductBuilds.Columns)
                    if (field.HeaderText != "")
                        field.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, field.HeaderText);

                foreach (DataControlField field in GridViewProjectBuildLocales.Columns)
                    if (field.HeaderText != "")
                        field.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, field.HeaderText);
            }
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