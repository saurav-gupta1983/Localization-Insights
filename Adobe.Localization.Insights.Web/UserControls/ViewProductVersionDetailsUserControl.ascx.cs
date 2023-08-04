using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adobe.Localization.Insights.Common;
using COM = Adobe.Localization.Insights.Common.Common;
using BO = Adobe.Localization.Insights.BusinessLayer;
using DTO = Adobe.Localization.Insights.Common.DataTransferObjects;

namespace Adobe.Localization.Insights.Web.UserControls
{
    /// <summary>
    /// ViewProductVersionDetailsUserControl
    /// </summary>
    public partial class ViewProductVersionDetailsUserControl : System.Web.UI.UserControl
    {
        #region Variables

        private Control controlFired;

        private DataTable dtProjectPhases;
        private DataTable dtPhaseTypes;
        private DataTable dtProductSprints;
        private DataTable dtTestingTypes;
        private DataTable dtReleaseTypes;
        private DataTable dtStatus;
        private DataTable dtVendors;
        private DataTable dtScreenLabels;

        private DTO.Header dataHeader = new DTO.Header();
        private ArrayList columnNames;

        private bool isReadOnly = true;
        private bool isReport = false;
        private bool isContractor = false;
        private bool isProductVersionActive = false;

        private const string STR_NON_ACTIVE = "Not Active";
        private const string STR_SPACE = "&nbsp;";
        private const string STR_ZERO = "0";
        private const string STR_DOCUMENT_HYPERLINK = "<a href='{0}' target='_blank'>{0}</a>";
        private const string STR_TAB_PANEL_ABOUT_PRODUCT = "TabPanelAboutProduct";
        private const string STR_TAB_PANEL_PRODUCT_LINKS = "TabPanelImportantLinks";
        private const string STR_UPDATE_ABOUT_PRODUCT_FAILED = "Update About Product Failed";
        private const string STR_DEFINE_ABOUT = "Define About";
        private const string STR_ABOUT_PRODUCT_NOT_DEFINED = "No About Product Defined";
        private const string STR_ABOUT_PRODUCT_NOT_DEFINED_READ_ONLY = "The Product information is not defined.";
        private const string STR_ABOUT_PRODUCT_EDIT_BUTTON = "Button Edit About Product";
        private const string STR_COVERAGE_NOT_DEFINED = "Coverage Not Defined";
        private const string STR_VAL_ISACTIVE = "1";
        private const string STR_VAL_ISCLOSED = "1";
        private const string STR_VAL_YES = "Yes";
        private const string STR_VAL_TRUE = "True";
        private const string STR_USER_FILTER = " OR ProjectRoleCode = 'IQE' OR ProjectRoleCode = 'IPM'";

        private const string CONFIRM_MESSAGE_PRODUCT_VERSION = "Are you certain you want to delete this Product Version?";

        private const int CONST_ZERO = 0;
        private const int TAB_INDEX_ABOUT_PRODUCT = 4;
        private const int TAB_INDEX_PRODUCT_LINKS = 5;
        private const int COL_GRID_PHASE_SAVE_UPDATE_NO = 9;
        private const int COL_GRID_PHASE_CANCEL_DELETE_NO = 10;
        private const int COL_GRID_DOCUMENT_SAVE_UPDATE_NO = 4;
        private const int COL_GRID_DOCUMENT_CANCEL_DELETE_NO = 5;
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
            RepeaterTestStrategy.ItemDataBound += new RepeaterItemEventHandler(RepeaterTestStrategy_OnItemDataBound);

            if (ViewState[WebConstants.COL_STR_COLUMN_NAMES] != null)
                columnNames = (ArrayList)ViewState[WebConstants.COL_STR_COLUMN_NAMES];
            else
                ViewState[WebConstants.COL_STR_COLUMN_NAMES] = GetColumnNames(WebConstants.TBL_PRODUCT_VERSIONS);

            if (!this.IsPostBack)
            {
                PopulateVersionDetails();
            }
            else
            {
                controlFired = GetPostBackControl(Page);
            }

            dtReleaseTypes = (DataTable)ViewState[WebConstants.TBL_RELEASE_TYPES];
            dtProductSprints = (DataTable)ViewState[WebConstants.TBL_PRODUCT_SPRINTS];
            dtPhaseTypes = (DataTable)ViewState[WebConstants.TBL_PHASE_TYPES];
            isProductVersionActive = (bool)ViewState[WebConstants.STR_IS_PRODUCT_VERSION_ACTIVE];
            if (PanelPopulateVersionDetailsData.Visible)
            {
                PopulateLocalesVsPlatformsMatrix();
                PopulateRepeaterPhaseTypes();
            }

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
                if (controlFired.ClientID.Contains(STR_TAB_PANEL_ABOUT_PRODUCT))
                {
                    TabContainerPopulateVersionDetailsData.ActiveTabIndex = TAB_INDEX_ABOUT_PRODUCT;
                    LabelMessage.Text = "";
                }
                if (controlFired.ClientID.Contains(STR_TAB_PANEL_PRODUCT_LINKS))
                    TabContainerPopulateVersionDetailsData.ActiveTabIndex = TAB_INDEX_PRODUCT_LINKS;
            }
        }

        #endregion

        #region Product Version

        #region Button Click Events

        /// <summary>
        /// ButtonViewProductVersion_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonViewProductVersion_Click(object sender, EventArgs e)
        {
            //SetVisibility(false, false, false);
            //LabelMessage.Text = "";
            //PopulateVersionDetails();
            //Session.Remove(WebConstants.SESSION_DATA_TREEVIEW_NODE_COLLECTION);
            Session.Remove(WebConstants.SESSION_VIEW_PRODUCT_VERSION);
            Response.Redirect(WebConstants.PAGE_LOCALIZATION_INSIGHT);
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// PopulateVersionDetails
        /// </summary>
        /// <param name="detailsGridViewRow"></param>
        private void PopulateVersionDetails()
        {
            //if (Session[WebConstants.SESSION_IDENTIFIER] == null)
            //    SetVisibility(true);
            //else
            //    SetVisibility(false);

            DTO.Product productData = new DTO.Product();
            productData.ProductVersionID = Session[WebConstants.SESSION_PRODUCT_VERSION_ID].ToString();

            DataTable dtProductVersion = BO.BusinessObjects.GetProductVersion(productData).Tables[0];

            LabelProductNamePopulate.Text = dtProductVersion.Rows[0][WebConstants.COL_PRODUCT].ToString();
            LabelProductVersionID.Text = productData.ProductVersionID;

            if (dtProductVersion.Rows[0][WebConstants.COL_PRODUCT_VERSION_CODE].ToString() != "")
                LabelProductVersionValuePopulate.Text = dtProductVersion.Rows[0][WebConstants.COL_PRODUCT_VERSION_CODE].ToString() + " ( " + dtProductVersion.Rows[0][WebConstants.COL_PRODUCT_VERSION].ToString() + " ) ";
            else
                LabelProductVersionValuePopulate.Text = dtProductVersion.Rows[0][WebConstants.COL_PRODUCT_VERSION].ToString();

            if (dtProductVersion.Rows[0][WebConstants.COL_PRODUCT_VERSION_ACTIVE].ToString() == "0")
                LabelProductVersionValuePopulate.Text += " - " + COM.GetScreenLocalizedLabel(dtScreenLabels, STR_NON_ACTIVE);
            else
                isProductVersionActive = true;

            ViewState[WebConstants.STR_IS_PRODUCT_VERSION_ACTIVE] = isProductVersionActive;

            LabelAboutProduct.Text = dtProductVersion.Rows[0][WebConstants.COL_PRODUCT_ABOUT].ToString();
            TextBoxAboutProduct.Text = LabelAboutProduct.Text;

            PopulateProductVersionDetailsData(productData);
        }


        #endregion

        #endregion

        #region TabPanelProjectPhases

        #region Grid Events

        /// <summary>
        /// GridViewProjectPhases_RowDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewProjectPhases_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label LabelPhaseStartDate = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_PHASE_START_DATE);
                if (LabelPhaseStartDate.Text != "")
                    LabelPhaseStartDate.Text = DateTime.Parse(LabelPhaseStartDate.Text).ToShortDateString();

                Label LabelPhaseEndDate = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_PHASE_END_DATE);
                if (LabelPhaseEndDate.Text != "")
                    LabelPhaseEndDate.Text = DateTime.Parse(LabelPhaseEndDate.Text).ToShortDateString();

                Label LabelPhaseTypeID = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_PHASE_TYPE_ID);
                if (LabelPhaseTypeID.Text != "")
                    LabelPhaseTypeID.Text = dtPhaseTypes.Select(WebConstants.COL_ID + " = '" + LabelPhaseTypeID.Text + "'")[0][WebConstants.COL_DESCRIPTION].ToString();

                Label LabelProductSprintID = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_PRODUCT_SPRINT_ID);
                if (LabelProductSprintID.Text != "")
                    LabelProductSprintID.Text = dtProductSprints.Select(WebConstants.COL_ID + " = '" + LabelProductSprintID.Text + "'")[0][WebConstants.COL_DESCRIPTION].ToString();

                Label LabelPhaseType = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_PHASE_TYPE);
                if (LabelProductSprintID.Text != "")
                    LabelPhaseType.Text = LabelProductSprintID.Text;
                else
                    LabelPhaseType.Text = LabelPhaseTypeID.Text;

                Label LabelTestingTypeID = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_TESTING_TYPE_ID);
                if (LabelTestingTypeID.Text != "")
                    LabelTestingTypeID.Text = dtTestingTypes.Select(WebConstants.COL_ID + " = '" + LabelTestingTypeID.Text + "'")[0][WebConstants.COL_DESCRIPTION].ToString();

                Label LabelStatusID = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_STATUS_ID);
                if (LabelStatusID.Text != "")
                    LabelStatusID.Text = dtStatus.Select(WebConstants.COL_ID + " = '" + LabelStatusID.Text + "'")[0][WebConstants.COL_DESCRIPTION].ToString();
            }
        }

        #endregion

        #region GridView Button Click Events

        /// <summary>
        /// LinkButtonViewPhaseDetails_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonViewPhaseDetails_Click(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow detailsGridViewRow = GridViewProjectPhases.Rows[index];

            Label LabelProjectPhaseID = (Label)detailsGridViewRow.FindControl(WebConstants.CONTROL_LABEL_PROJECT_PHASE_ID);

            Session[WebConstants.SESSION_PRODUCT_VERSION_ID] = LabelProductVersionID.Text;
            Session[WebConstants.SESSION_PROJECT_PHASE_ID] = LabelProjectPhaseID.Text;
            //Session[WebConstants.SESSION_IDENTIFIER] = WebConstants.USER_CONTROL_PROJECT_PHASE_SCREEN_IDENTIFIER;

            if (Session[WebConstants.SESSION_IDENTIFIER] != null)
                Session[WebConstants.SESSION_IDENTIFIER] = WebConstants.USER_CONTROL_PROJECT_PHASE_SCREEN_IDENTIFIER;

            Response.Redirect(WebConstants.PAGE_LOCALIZATION_INSIGHT);
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// PopulateMasterData
        /// </summary>
        private void PopulateMasterData()
        {
            dtPhaseTypes = BO.BusinessObjects.GetMasterDataWithTypeDetails(WebConstants.TBL_PHASE_TYPES, false);
            ViewState[WebConstants.TBL_PHASE_TYPES] = dtPhaseTypes;
            dtProductSprints = BO.BusinessObjects.GetMasterDataWithTypeDetails(WebConstants.TBL_PRODUCT_SPRINTS, true);
            ViewState[WebConstants.TBL_PRODUCT_SPRINTS] = dtProductSprints;
            dtTestingTypes = BO.BusinessObjects.GetMasterDataWithTypeDetails(WebConstants.TBL_VENDOR_TYPES, false);
            dtStatus = BO.BusinessObjects.GetMasterDataWithTypeDetails(WebConstants.TBL_STATUS, true);
            dtVendors = BO.BusinessObjects.GetMasterDataWithTypeDetails(WebConstants.TBL_VENDORS, true);
            ViewState[WebConstants.TBL_VENDORS] = dtVendors;
        }

        /// <summary>
        /// PopulateProjectPhases
        /// </summary>
        private void PopulateProjectPhases(string productVersionID)
        {
            DTO.ProjectPhases projectPhaseData = new DTO.ProjectPhases();
            projectPhaseData.ProductVersionID = productVersionID;

            dtProjectPhases = BO.BusinessObjects.GetProjectPhases(projectPhaseData).Tables[0];
            ViewState[WebConstants.TBL_PROJECT_PHASES] = dtProjectPhases;

            DataColumn col = new DataColumn(WebConstants.COL_TEMP_ROW, Type.GetType(WebConstants.STR_SYSTEM_INT32));

            dtProjectPhases.Columns.Add(col);
            for (int rowCount = 0; rowCount < dtProjectPhases.Rows.Count; rowCount++)
                dtProjectPhases.Rows[rowCount][WebConstants.COL_TEMP_ROW] = rowCount + 1;

            GridViewProjectPhases.DataSource = dtProjectPhases;
            GridViewProjectPhases.DataBind();

            if (dtProjectPhases.Rows.Count == 0)
                LabelNoProjectPhasesDefined.Visible = true;
        }

        /// <summary>
        /// PopulateProductVersionDetailsData
        /// </summary>
        private void PopulateProductVersionDetailsData(DTO.Product productData)
        {
            SetAboutProduct();

            PopulateNewFeaturesList();
            PopulateMasterData();
            PopulateProjectPhases(LabelProductVersionID.Text);
            PopulateRepeaterPhaseTypes();
            PopulateLocales(productData);
            PopulatePlatforms(productData);
            PopulateLocalesVsPlatformsMatrix();
            PopulateUsers(productData);
            PopulateProductLinkDetails();
        }

        #endregion

        #endregion

        #region TabPanelTestStrategy

        ///// <summary>
        ///// PopulateTestStrategy
        ///// </summary>
        //private void PopulateTestStrategy(string productVersionID)
        //{
        //    //GridViewTestStrategy.Columns.Clear();
        //    GridViewTestStrategy.Columns.Clear();

        //    int rowsCount = 0;
        //    int totalRows = 0;

        //    dtProjectPhases = (DataTable)ViewState[WebConstants.TBL_PROJECT_PHASES];
        //    if (dtProjectPhases.Rows.Count == 0)
        //    {
        //        LabelNoTestStrategyDefined.Visible = true;
        //        return;
        //    }

        //    DTO.ProjectPhases projectPhaseData = new DTO.ProjectPhases();
        //    projectPhaseData.ProductVersionID = productVersionID;
        //    DataTable dtCoverageDetails = BO.BusinessObjects.GetPhaseCoverageDetails(projectPhaseData).Tables[0];
        //    DataTable dtPhases = new DataTable();

        //    foreach (DataRow row in dtProjectPhases.Rows)
        //    {
        //        string header = row[WebConstants.COL_PROJECT_PHASE].ToString();
        //        rowsCount = 0;
        //        DataColumn headerCol = new DataColumn(header, Type.GetType(WebConstants.STR_SYSTEM_STRING));
        //        dtPhases.Columns.Add(headerCol);

        //        GridViewTestStrategy.Columns.Add(CreateTemplateField(row));

        //        DataRow[] dataRows = dtCoverageDetails.Select(WebConstants.COL_PROJECT_PHASE_ID + " = " + row[WebConstants.COL_PROJECT_PHASE_ID].ToString());

        //        if (dataRows.Length == 0)
        //        {
        //            if (totalRows == 0)
        //            {
        //                dtPhases.Rows.Add(dtPhases.NewRow());
        //                totalRows++;
        //            }

        //            dtPhases.Rows[0][header] = COM.GetScreenLocalizedLabel(dtScreenLabels, STR_COVERAGE_NOT_DEFINED);
        //        }
        //        else
        //        {
        //            foreach (DataRow dr in dataRows)
        //            {
        //                StringBuilder value = new StringBuilder();
        //                if (dr[WebConstants.COL_PHASE_COVERAGE_DETAILS].ToString() != "")
        //                    value.Append(dr[WebConstants.COL_PHASE_COVERAGE_DETAILS].ToString());
        //                if (dr[WebConstants.COL_TESTCASES_COUNT].ToString() != "")
        //                    value.Append("( " + dr[WebConstants.COL_TESTCASES_COUNT] + " )");
        //                else
        //                    value.Append("( 0 )");

        //                if (rowsCount == totalRows)
        //                {
        //                    dtPhases.Rows.Add(dtPhases.NewRow());
        //                    totalRows++;
        //                }

        //                dtPhases.Rows[rowsCount][header] = value.ToString();
        //                rowsCount++;
        //            }
        //        }
        //    }

        //    GridViewTestStrategy.DataSource = dtPhases;
        //    GridViewTestStrategy.DataBind();
        //}

        /// <summary>
        /// PopulateTestStrategy
        /// </summary>
        private void PopulateRepeaterPhaseTypes()
        {
            dtProjectPhases = (DataTable)ViewState[WebConstants.TBL_PROJECT_PHASES];
            if (dtProjectPhases.Rows.Count == 0)
            {
                LabelNoTestStrategyDefined.Visible = true;
                RepeaterTestStrategy.Visible = false;
            }
            else
            {
                DataView dvProjectPhases = new DataView(dtProjectPhases);
                DataTable dtDistinctPhaseTypes = dvProjectPhases.ToTable(true, WebConstants.COL_PRODUCT_VERSION_ID, WebConstants.COL_PHASE_TYPE_ID, WebConstants.COL_PRODUCT_SPRINT_ID);

                RepeaterTestStrategy.DataSource = dtDistinctPhaseTypes;
                RepeaterTestStrategy.DataBind();
            }
        }

        /// <summary>
        /// RepeaterTestStrategy_OnItemDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepeaterTestStrategy_OnItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;
            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {
                PopulateTestStrategy(item);
            }
        }

        /// <summary>
        /// PopulateTestStrategy
        /// </summary>
        private void PopulateTestStrategy(RepeaterItem item)
        {
            DataRowView drv = (DataRowView)item.DataItem;

            Label phaseType = (Label)item.FindControl(WebConstants.CONTROL_LABEL_PHASE_TYPE);
            if (drv.Row[WebConstants.COL_PRODUCT_SPRINT_ID].ToString() != "")
                phaseType.Text = dtProductSprints.Select(WebConstants.COL_ID + " = '" + drv.Row[WebConstants.COL_PRODUCT_SPRINT_ID].ToString() + "'")[0][WebConstants.COL_DESCRIPTION].ToString();
            else
                phaseType.Text = dtPhaseTypes.Select(WebConstants.COL_ID + " = '" + drv.Row[WebConstants.COL_PHASE_TYPE_ID].ToString() + "'")[0][WebConstants.COL_DESCRIPTION].ToString();

            DataRow drData = drv.Row;

            int rowsCount = 0;
            int totalRows = 0;

            DTO.ProjectPhases projectPhaseData = new DTO.ProjectPhases();
            projectPhaseData.ProductVersionID = drData[WebConstants.COL_PRODUCT_VERSION_ID].ToString();
            projectPhaseData.PhaseTypeID = drData[WebConstants.COL_PHASE_TYPE_ID].ToString();
            projectPhaseData.ProductSprintID = drData[WebConstants.COL_PRODUCT_SPRINT_ID].ToString();
            DataTable dtCoverageDetails = BO.BusinessObjects.GetPhaseCoverageDetails(projectPhaseData).Tables[0];
            DataTable dtPhases = new DataTable();

            if (dtCoverageDetails.Rows.Count == 0)
            {
                Label LabelNoCoveragesDefined = (Label)item.FindControl(WebConstants.CONTROL_LABEL_COVERAGES_NOT_DEFINED);
                LabelNoCoveragesDefined.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, STR_COVERAGE_NOT_DEFINED);
                LabelNoCoveragesDefined.Visible = true;
            }
            else
            {
                GridView gridViewTestStrategy = (GridView)item.FindControl(WebConstants.CONTROL_GRIDVIEW_TEST_STRATEGY);
                gridViewTestStrategy.Columns.Clear();

                DataView dvCoverageDetails = new DataView(dtCoverageDetails);
                DataTable dtProjectPhases = dvCoverageDetails.ToTable(true, WebConstants.COL_PROJECT_PHASE_ID, WebConstants.COL_PROJECT_PHASE, WebConstants.COL_PROJECT_PHASE_START_DATE, WebConstants.COL_PROJECT_PHASE_END_DATE);

                int phaseCounter = 0;
                foreach (DataRow row in dtProjectPhases.Rows)
                {
                    phaseCounter++;
                    string header = "ProjectPhase_" + phaseCounter.ToString();
                    rowsCount = 0;
                    DataColumn headerCol = new DataColumn(header, Type.GetType(WebConstants.STR_SYSTEM_STRING));
                    dtPhases.Columns.Add(headerCol);

                    gridViewTestStrategy.Columns.Add(CreateTemplateField(row, header));

                    DataRow[] dataRows = dtCoverageDetails.Select(WebConstants.COL_PROJECT_PHASE_ID + " = " + row[WebConstants.COL_PROJECT_PHASE_ID].ToString());

                    //if (dataRows.Length == 0)
                    //{
                    //    if (totalRows == 0)
                    //    {
                    //        dtPhases.Rows.Add(dtPhases.NewRow());
                    //        totalRows++;
                    //    }

                    //    dtPhases.Rows[0][header] = COM.GetScreenLocalizedLabel(dtScreenLabels, STR_COVERAGE_NOT_DEFINED);
                    //}
                    //else
                    //{
                    foreach (DataRow dr in dataRows)
                    {
                        StringBuilder value = new StringBuilder();
                        if (dr[WebConstants.COL_PHASE_COVERAGE_DETAILS].ToString() != "")
                            value.Append(dr[WebConstants.COL_PHASE_COVERAGE_DETAILS].ToString());
                        if (dr[WebConstants.COL_TESTCASES_COUNT].ToString() != "")
                            value.Append(" ( " + dr[WebConstants.COL_TESTCASES_COUNT] + " )");
                        else
                            value.Append("( 0 )");

                        if (rowsCount == totalRows)
                        {
                            dtPhases.Rows.Add(dtPhases.NewRow());
                            totalRows++;
                        }

                        dtPhases.Rows[rowsCount][header] = value.ToString();
                        rowsCount++;
                    }
                    //}
                }

                gridViewTestStrategy.DataSource = dtPhases;
                gridViewTestStrategy.DataBind();
            }
        }

        /// <summary>
        /// CreateTemplateField
        /// </summary>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        private DataControlField CreateTemplateField(DataRow drtemplateField, string header)
        {
            TemplateField tf = new TemplateField();

            string date = ((DateTime)drtemplateField[WebConstants.COL_PROJECT_PHASE_START_DATE]).ToShortDateString() + " - ";
            if (drtemplateField[WebConstants.COL_PROJECT_PHASE_END_DATE].ToString() != "")
                date = date + ((DateTime)drtemplateField[WebConstants.COL_PROJECT_PHASE_END_DATE]).ToShortDateString();

            tf.HeaderText = drtemplateField[WebConstants.COL_PROJECT_PHASE].ToString() + "<BR> ( " + date + " )";
            tf.ItemTemplate = new GridViewPhaseTemplate(ListItemType.Header, drtemplateField, header);

            return tf;
        }

        #endregion

        #region TabPanelLocalesVsPlatformMatrix

        /// <summary>
        /// LoadLocalesVsPlatformsMatrix
        /// </summary>
        private void PopulateLocalesVsPlatformsMatrix()
        {
            LoadLocalesVsPlatformsMatrixControl.Controls.Clear();

            StringBuilder vendorID = new StringBuilder(STR_ZERO);
            dtVendors = (DataTable)ViewState[WebConstants.TBL_VENDORS];

            DataView dvVendors = new DataView(dtVendors);
            if (isContractor)
                dvVendors.RowFilter = WebConstants.COL_ID + " = " + Session[WebConstants.SESSION_VENDOR_ID].ToString();

            foreach (DataRow dr in dvVendors.ToTable().Rows)
            {
                DTO.ProjectPhases vendorLocalesData = new DTO.ProjectPhases();
                vendorLocalesData.ProductVersionID = LabelProductVersionID.Text;
                vendorLocalesData.VendorID = dr[WebConstants.COL_ID].ToString();

                if (BO.BusinessObjects.GetProjectLocales(vendorLocalesData).Tables[0].Rows.Count > 0)
                    vendorID.Append("," + dr[WebConstants.COL_ID].ToString());
            }

            if (vendorID.ToString() == STR_ZERO)
                LabelNoLocalesVsPlatformMatrixDefined.Visible = true;
            else
            {
                LocalesVsPlatformsCombinedMatrixDataUserControl localesVsPlatformsUserControl = (LocalesVsPlatformsCombinedMatrixDataUserControl)this.LoadControl(WebConstants.USER_CONTROL_LOCALES_PLATFORM_MATRIX_DISPLAY);
                localesVsPlatformsUserControl.VendorID = vendorID.ToString(); ;
                localesVsPlatformsUserControl.ProductVersionID = LabelProductVersionID.Text;
                LoadLocalesVsPlatformsMatrixControl.Controls.Add(localesVsPlatformsUserControl);
            }
        }

        #endregion

        #region TabPanelLocalesAndPlatforms

        /// <summary>
        /// PopulateLocales
        /// </summary>
        private void PopulateLocales(DTO.Product productData)
        {
            DataTable dtProductVersionLocales = BO.BusinessObjects.GetProductVersionLocales(productData).Tables[0];

            GridViewLocales.DataSource = dtProductVersionLocales;
            GridViewLocales.DataBind();

            if (dtProductVersionLocales.Rows.Count == 0)
                LabelNoLocalesAndPlatforms.Visible = true;
        }

        /// <summary>
        /// PopulatePlatforms
        /// </summary>
        /// <param name="productData"></param>
        private void PopulatePlatforms(DTO.Product productData)
        {
            DataTable dtProductVersionPlatforms = BO.BusinessObjects.GetProductVersionPlatforms(productData).Tables[0];

            GridViewPlatforms.DataSource = dtProductVersionPlatforms;
            GridViewPlatforms.DataBind();

            if (dtProductVersionPlatforms.Rows.Count == 0)
                LabelNoLocalesAndPlatforms.Visible = true;
        }

        #endregion

        #region TabPanelOwnersAndUsers

        ///// <summary>
        ///// PopulateOwners
        ///// </summary>
        ///// <param name="productData"></param>
        //private void PopulateOwners(DTO.Product productData)
        //{
        //    productData.IsOwner = true;
        //    GridViewProductOwners.DataSource = BO.UsersBO.GetProductUsers(productData);
        //    GridViewProductOwners.DataBind();
        //}

        /// <summary>
        /// PopulateUsers
        /// </summary>
        /// <param name="productData"></param>
        private void PopulateUsers(DTO.Product productData)
        {
            DataTable dtUsers = BO.BusinessObjects.GetProductUsers(productData).Tables[0];
            DataView dvUsers = new DataView(dtUsers);
            dvUsers.Sort = WebConstants.COL_VENDOR + " ASC," + WebConstants.COL_PROJECT_ROLE + " ASC";

            if (isContractor)
                dvUsers.RowFilter = WebConstants.COL_VENDOR_ID + " = " + Session[WebConstants.SESSION_VENDOR_ID].ToString() + STR_USER_FILTER;
            dtUsers = dvUsers.ToTable();

            dtUsers.Rows.RemoveAt(0);
            if (dtUsers.Rows.Count == 0)
                LabelNoActiveTeam.Visible = true;

            GridViewProductUsers.DataSource = dtUsers;
            GridViewProductUsers.DataBind();
        }

        #endregion

        #region TabPanelImportantLinks

        #region Grid Events

        /// <summary>
        /// GridViewDocumentLinks_RowDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewDocumentLinks_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                SetGridLinkButtonsDisplayText(e.Row);
                if (e.Row.Cells[CONST_ZERO].Text.ToString() == CONST_ZERO.ToString() || e.Row.Cells[CONST_ZERO].Text.ToString() == STR_SPACE)
                {
                    e.Row.Cells[CONST_ZERO].Text = "";
                    SetLinkButtons_DocumentLinks(e.Row, true);
                }
                else
                {
                    Label LabelDocumentLink = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_DOCUMENT_LINK);
                    TextBox TextBoxDocumentLink = (TextBox)e.Row.FindControl(WebConstants.CONTROL_TEXTBOX_DOCUMENT_LINK);
                    LabelDocumentLink.Text = String.Format(STR_DOCUMENT_HYPERLINK, TextBoxDocumentLink.Text);
                }
            }
        }

        /// <summary>
        /// GridViewDocumentLinks_RowUpdating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewDocumentLinks_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
        }

        /// <summary>
        /// GridViewDocumentLinks_RowCancelingEdit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewDocumentLinks_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
        }

        ///// <summary>
        ///// GridViewUploadDocuments_RowDataBound
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void GridViewUploadDocuments_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        if (e.Row.Cells[CONST_ZERO].Text.ToString() == CONST_ZERO.ToString() || e.Row.Cells[CONST_ZERO].Text.ToString() == STR_SPACE)
        //        {
        //            e.Row.Cells[CONST_ZERO].Text = "";
        //            SetLinkButtons_UploadDocuments(e.Row, true);
        //        }
        //    }
        //}

        ///// <summary>
        ///// GridViewUploadDocuments_RowUpdating
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void GridViewUploadDocuments_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //}

        ///// <summary>
        ///// GridViewUploadDocuments_RowCancelingEdit
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void GridViewUploadDocuments_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        //{
        //}

        #endregion

        #region GridView Button Click Events

        /// <summary>
        /// LinkButtonSaveDocumentLinks_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonSaveDocumentLinks_Click(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow detailsGridViewRow = GridViewDocumentLinks.Rows[index];

            DTO.Product productData = new DTO.Product();
            productData.DataHeader = dataHeader;
            productData.ProductVersionID = LabelProductVersionID.Text;

            TextBox TextBoxDocumentName = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_DOCUMENT_NAME);
            TextBox TextBoxDocumentLink = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_DOCUMENT_LINK);

            if (TextBoxDocumentName.Text == "")
            {
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.SCREEN_DOCUMENT_NAME_MANDATORY);
                return;
            }
            productData.DocumentName = TextBoxDocumentName.Text;

            if (TextBoxDocumentLink.Text == "")
            {
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.SCREEN_DOCUMENT_LINK_MANDATORY);
                return;
            }
            productData.DocumentLink = TextBoxDocumentLink.Text;

            if (detailsGridViewRow.Cells[CONST_ZERO].Text != "")
            {
                Label LabelProductLinkID = (Label)detailsGridViewRow.FindControl(WebConstants.CONTROL_LABEL_DOCUMENT_LINK_ID);
                productData.ProductLinkID = LabelProductLinkID.Text;
            }

            if (BO.BusinessObjects.AddUpdateProductLinks(productData))
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_SUCCESS_SAVE_MESSAGE);
            else
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_SAVE_MESSAGE);

            PopulateProductLinkDetails();
        }

        /// <summary>
        /// LinkButtonCancelDocumentLinks_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonCancelDocumentLinks_Click(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow detailsGridViewRow = GridViewDocumentLinks.Rows[index];
            LabelMessage.Text = "";
            if (detailsGridViewRow.Cells[CONST_ZERO].Text == "")
            {
                TextBox TextBoxDocumentName = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_DOCUMENT_NAME);
                TextBoxDocumentName.Text = "";
                TextBox TextBoxDocumentLink = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_DOCUMENT_LINK);
                TextBoxDocumentLink.Text = "";
            }
            else
            {
                SetLinkButtons_DocumentLinks(detailsGridViewRow, false);
            }
        }

        /// <summary>
        /// LinkButtonUpdateDocumentLinks_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonUpdateDocumentLinks_Click(object sender, CommandEventArgs e)
        {
            LabelMessage.Text = "";
            SetLinkButtons_DocumentLinks(GridViewDocumentLinks.Rows[Convert.ToInt32(e.CommandArgument)], true);
        }

        /// <summary>
        /// LinkButtonDeleteDocumentLinks_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonDeleteDocumentLinks_Click(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow detailsGridViewRow = GridViewDocumentLinks.Rows[index];

            DTO.Product productData = new DTO.Product();

            Label LabelProductLinkID = (Label)detailsGridViewRow.FindControl(WebConstants.CONTROL_LABEL_DOCUMENT_LINK_ID);
            productData.ProductLinkID = LabelProductLinkID.Text;

            if (BO.BusinessObjects.AddUpdateProductLinks(productData))
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_SUCCESS_DELETE_MESSAGE);
            else
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_DELETE_MESSAGE); ;

            PopulateProductLinkDetails();
        }

        ///// <summary>
        ///// LinkButtonSaveUploadDocuments_Click
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void LinkButtonSaveUploadDocuments_Click(object sender, CommandEventArgs e)
        //{
        //    int index = Convert.ToInt32(e.CommandArgument);
        //    GridViewRow detailsGridViewRow = GridViewUploadDocuments.Rows[index];

        //    dataHeader.LoginID = Session[WebConstants.SESSION_LOGIN_ID].ToString();

        //    DTO.MasterData masterData = new DTO.MasterData();
        //    masterData.DataHeader = dataHeader;
        //    masterData.TableName = tableName;
        //    masterData.ColumnNames = columnNames;

        //    TextBox TextBoxCode = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_CODE);
        //    TextBox TextBoxDescription = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_DESCRIPTION);
        //    DropDownList DropDownListType = (DropDownList)detailsGridViewRow.FindControl(WebConstants.CONTROL_DROPDOWNLIST_TYPE);

        //    if (isCode)
        //    {

        //        if (TextBoxCode.Text == "")
        //        {
        //            if (isSubScreen)
        //                LabelMessage.Text = string.Format(COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.SCREEN_CODE_MANDATORY, false), header);
        //            else
        //                LabelMessage.Text = string.Format(COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.SCREEN_CODE_MANDATORY), header);
        //            return;
        //        }
        //        masterData.Code = TextBoxCode.Text;
        //    }
        //    else
        //    {
        //        if (TextBoxDescription.Text == "")
        //        {
        //            if (isSubScreen)
        //                LabelMessage.Text = string.Format(COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.SCREEN_DESCRIPTION_MANDATORY, false), header);
        //            else
        //                LabelMessage.Text = string.Format(COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.SCREEN_DESCRIPTION_MANDATORY), header);
        //            return;
        //        }
        //    }
        //    masterData.Description = TextBoxDescription.Text;

        //    if (isType)
        //    {
        //        masterData.Type = DropDownListType.SelectedValue;
        //        if (LabelTypeValue.Text != "")
        //        {
        //            masterData.Type = LabelTypeValue.Text;
        //        }
        //    }

        //    if (detailsGridViewRow.Cells[CONST_ZERO].Text != "")
        //    {
        //        Label LabelID = (Label)detailsGridViewRow.FindControl(WebConstants.CONTROL_LABEL_ID);
        //        masterData.MasterDataID = LabelID.Text;
        //    }

        //    if (BO.BusinessObjects.AddUpdateDetailsforMasterData(masterData))
        //        LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_SUCCESS_SAVE_MESSAGE);
        //    else
        //        LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_SAVE_MESSAGE);

        //    PopulateData();
        //}

        ///// <summary>
        ///// LinkButtonCancelUploadDocuments_Click
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void LinkButtonCancelUploadDocuments_Click(object sender, CommandEventArgs e)
        //{
        //    int index = Convert.ToInt32(e.CommandArgument);
        //    GridViewRow detailsGridViewRow = GridViewUploadDocuments.Rows[index];

        //    if (detailsGridViewRow.Cells[CONST_ZERO].Text == "")
        //    {
        //        TextBox TextBoxDescription = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_DESCRIPTION);
        //        TextBoxDescription.Text = "";

        //        if (isCode)
        //        {
        //            TextBox TextBoxCode = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_CODE);
        //            TextBoxCode.Text = "";
        //        }
        //    }
        //    else
        //    {
        //        SetLinkButtons(detailsGridViewRow, false);
        //    }
        //}

        ///// <summary>
        ///// LinkButtonUpdateUploadDocuments_Click
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void LinkButtonUpdateUploadDocuments_Click(object sender, CommandEventArgs e)
        //{
        //    SetLinkButtons(GridViewUploadDocuments.Rows[Convert.ToInt32(e.CommandArgument)], true);
        //}

        ///// <summary>
        ///// LinkButtonDeleteUploadDocuments_Click
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void LinkButtonDeleteUploadDocuments_Click(object sender, CommandEventArgs e)
        //{
        //    int index = Convert.ToInt32(e.CommandArgument);
        //    GridViewRow detailsGridViewRow = GridViewUploadDocuments.Rows[index];

        //    DTO.MasterData masterData = new DTO.MasterData();
        //    masterData.TableName = tableName;
        //    masterData.ColumnNames = columnNames;

        //    Label LabelID = (Label)detailsGridViewRow.FindControl(WebConstants.CONTROL_LABEL_ID);
        //    masterData.MasterDataID = LabelID.Text;

        //    if (BO.BusinessObjects.AddUpdateDetailsforMasterData(masterData))
        //        LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_SUCCESS_DELETE_MESSAGE);
        //    else
        //        LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_DELETE_MESSAGE); ;

        //    PopulateData();
        //}

        #endregion

        #region Private Functions

        /// <summary>
        /// PopulateProductLinkDetails
        /// </summary>
        private void PopulateProductLinkDetails()
        {
            DTO.MasterData masterData = new DTO.MasterData();
            masterData.TableName = WebConstants.TBL_PRODUCT_LINKS;
            masterData.Type = LabelProductVersionID.Text;
            masterData.Filter = WebConstants.COL_PRODUCT_VERSION_ID;

            DataTable dtProductLinks = BO.BusinessObjects.GetDetailsforMasterData(masterData).Tables[0];

            if (isReadOnly)
                dtProductLinks.Rows.RemoveAt(0);

            GridViewDocumentLinks.DataSource = dtProductLinks;
            GridViewDocumentLinks.DataBind();
        }

        /// <summary>
        /// SetLinkButtons_DocumentLinks
        /// </summary>
        /// <param name="gridViewRow"></param>
        /// <param name="isSave"></param>
        private void SetLinkButtons_DocumentLinks(GridViewRow gridViewRow, bool isSave)
        {
            LinkButton LinkButtonSave = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_SAVE);
            LinkButtonSave.Visible = isSave;
            LinkButton LinkButtonCancel = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_CANCEL);
            LinkButtonCancel.Visible = isSave;
            LinkButton LinkButtonUpdate = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_UPDATE);
            LinkButtonUpdate.Visible = (!isSave);
            LinkButton LinkButtonDelete = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_DELETE);
            LinkButtonDelete.Visible = (!isSave);

            //DocumentLink
            Label LabelDocumentLink = (Label)gridViewRow.FindControl(WebConstants.CONTROL_LABEL_DOCUMENT_LINK);
            LabelDocumentLink.Visible = (!isSave);
            TextBox TextBoxDocumentLink = (TextBox)gridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_DOCUMENT_LINK);
            TextBoxDocumentLink.Visible = isSave;
            //TextBoxDocumentLink.Text = LabelDocumentLink.Text;

            //DocumentName
            Label LabelDocumentName = (Label)gridViewRow.FindControl(WebConstants.CONTROL_LABEL_DOCUMENT_NAME);
            LabelDocumentName.Visible = (!isSave);
            TextBox TextBoxDocumentName = (TextBox)gridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_DOCUMENT_NAME);
            TextBoxDocumentName.Visible = isSave;
            //TextBoxDocumentName.Text = LabelDocumentName.Text;
        }

        ///// <summary>
        ///// SetLinkButtons_UploadDocuments
        ///// </summary>
        ///// <param name="gridViewRow"></param>
        ///// <param name="isSave"></param>
        //private void SetLinkButtons_UploadDocuments(GridViewRow gridViewRow, bool isSave)
        //{
        //    LinkButton LinkButtonSave = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_SAVE);
        //    LinkButtonSave.Visible = isSave;
        //    LinkButton LinkButtonCancel = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_CANCEL);
        //    LinkButtonCancel.Visible = isSave;
        //    LinkButton LinkButtonUpdate = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_UPDATE);
        //    LinkButtonUpdate.Visible = (!isSave);
        //    LinkButton LinkButtonDelete = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_DELETE);
        //    LinkButtonDelete.Visible = (!isSave);

        //    //Description
        //    Label LabelDescription = (Label)gridViewRow.FindControl(WebConstants.CONTROL_LABEL_DESCRIPTION);
        //    LabelDescription.Visible = (!isSave);
        //    TextBox TextBoxDescription = (TextBox)gridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_DESCRIPTION);
        //    TextBoxDescription.Visible = isSave;
        //    TextBoxDescription.Text = LabelDescription.Text;

        //    //Code
        //    if (isCode)
        //    {
        //        Label LabelCode = (Label)gridViewRow.FindControl(WebConstants.CONTROL_LABEL_CODE);
        //        LabelCode.Visible = (!isSave);
        //        TextBox TextBoxCode = (TextBox)gridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_CODE);
        //        TextBoxCode.Visible = isSave;
        //        TextBoxCode.Text = LabelCode.Text;
        //    }
        //}

        #endregion

        #endregion

        #region TabPanelAboutProduct

        #region Button Click Events

        /// <summary>
        /// ButtonEditAboutProduct_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonEditAboutProduct_Click(object sender, EventArgs e)
        {
            ButtonSaveAboutProduct.Visible = true;
            ButtonEditAboutProduct.Visible = false;
            TextBoxAboutProduct.Visible = true;
            LabelAboutProduct.Visible = false;
            LabelMessage.Text = "";
            //TextBoxAboutProduct.Text = LabelAboutProduct.Text;
        }

        /// <summary>
        /// ButtonSaveAboutProduct_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSaveAboutProduct_Click(object sender, EventArgs e)
        {
            if (UpdateAboutProductDetails())
            {
                ButtonSaveAboutProduct.Visible = false;
                ButtonEditAboutProduct.Visible = true;
                TextBoxAboutProduct.Visible = false;
                LabelAboutProduct.Visible = true;

                LabelAboutProduct.Text = TextBoxAboutProduct.Text;

                SetAboutProduct();
            }
            else
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, STR_UPDATE_ABOUT_PRODUCT_FAILED);
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// UpdateAboutProductDetails
        /// </summary>
        private bool UpdateAboutProductDetails()
        {
            DTO.Product productData = new DTO.Product();
            productData.DataHeader = dataHeader;
            productData.ProductVersionID = LabelProductVersionID.Text;
            productData.AboutProduct = TextBoxAboutProduct.Text;

            return BO.BusinessObjects.UpdateAboutProductDetails(productData);
        }

        /// <summary>
        /// PopulateNewFeaturesList
        /// </summary>
        private void PopulateNewFeaturesList()
        {
            DataTable dtProductFeatures = BO.BusinessObjects.GetMasterDataWithTypeDetails(WebConstants.TBL_PRODUCT_FEATURES, false);

            if (isReadOnly && dtProductFeatures.Select(WebConstants.COL_PRODUCT_VERSION_ID + " = " + LabelProductVersionID.Text).Length == 0)
                PanelNoProductFeatures.Visible = true;
            else
            {
                PanelProductFeatures.Visible = true;
                MasterDataProductFeatures.TypeValue = LabelProductVersionID.Text;
            }
        }

        #endregion

        #endregion

        #region TabPanelMachineRequirements

        #endregion

        #region TabPanelToolsUsed

        #endregion

        #region Private Functions

        /// <summary>
        /// setLinkButtons
        /// </summary>
        /// <param name="gridViewRow"></param>
        /// <param name="isSave"></param>
        private void SetLinkButtons(GridViewRow gridViewRow, bool isSave)
        {
            LinkButton LinkButtonSave = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_SAVE);
            LinkButtonSave.Visible = isSave;
            LinkButton LinkButtonCancel = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_CANCEL);
            LinkButtonCancel.Visible = isSave;
            LinkButton LinkButtonUpdate = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_UPDATE);
            LinkButtonUpdate.Visible = (!isSave);
            LinkButton LinkButtonDelete = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_DELETE);
            LinkButtonDelete.Visible = (!isSave);

            if (gridViewRow.Cells[CONST_ZERO].Text != "")
            {
                LinkButton LinkButtonViewVersionDetails = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_VIEW_VERSION_DETAILS);
                LinkButtonViewVersionDetails.Visible = !isSave;
            }

            //Product Version
            Label LabelProductVersion = (Label)gridViewRow.FindControl(WebConstants.CONTROL_LABEL_PRODUCT_VERSION);
            LabelProductVersion.Visible = (!isSave);
            TextBox TextBoxProductVersion = (TextBox)gridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_PRODUCT_VERSION);
            TextBoxProductVersion.Visible = isSave;
            TextBoxProductVersion.Text = LabelProductVersion.Text;

            //Product Code Name
            Label LabelProductCodeName = (Label)gridViewRow.FindControl(WebConstants.CONTROL_LABEL_PRODUCT_CODE);
            LabelProductCodeName.Visible = (!isSave);
            TextBox TextBoxProductCodeName = (TextBox)gridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_PRODUCT_CODE);
            TextBoxProductCodeName.Visible = isSave;
            TextBoxProductCodeName.Text = LabelProductCodeName.Text;

            //Product Year
            Label LabelProductYear = (Label)gridViewRow.FindControl(WebConstants.CONTROL_LABEL_PRODUCT_YEAR);
            LabelProductYear.Visible = (!isSave);
            TextBox TextBoxProductYear = (TextBox)gridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_PRODUCT_YEAR);
            TextBoxProductYear.Visible = isSave;
            TextBoxProductYear.Text = LabelProductYear.Text;

            //ReleaseTypeID
            SetLinkButtonsForDropDowns(gridViewRow, dtReleaseTypes, WebConstants.CONTROL_LABEL_RELEASE_TYPE_ID, WebConstants.CONTROL_DROPDOWNLIST_RELEASE_TYPE, isSave);

            //Is Active
            Label LabelActive = (Label)gridViewRow.FindControl(WebConstants.CONTROL_LABEL_ACTIVE);
            LabelActive.Visible = (!isSave);
            CheckBox CheckBoxActive = (CheckBox)gridViewRow.FindControl(WebConstants.CONTROL_CHECKBOX_ACTIVE);
            CheckBoxActive.Visible = isSave;
            if (isSave)
            {
                CheckBoxActive.Checked = false;
                if (LabelActive.Text == STR_VAL_ISACTIVE || LabelActive.Text == STR_VAL_TRUE || LabelActive.Text == STR_VAL_YES)
                {
                    CheckBoxActive.Checked = true;
                }
            }
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
        /// GetColumnNames
        /// </summary>
        private ArrayList GetColumnNames(string tableName)
        {
            return BO.BusinessObjects.GetColumnNames(tableName);
        }

        /// <summary>
        /// SetVisibility
        /// </summary>
        /// <param name="isUpdate"></param>
        /// <param name="isPopulate"></param>
        private void SetVisibility(bool isHomePageClick)
        {
            ButtonViewProductVersion.Visible = !isHomePageClick;
            SetScreenAccess();
        }

        #endregion

        #region Screen Access and Labels

        /// <summary>
        /// GetScreenAccess
        /// </summary>
        private void GetScreenAccess()
        {
            dataHeader.LoginID = Session[WebConstants.SESSION_LOGIN_ID].ToString();

            isContractor = (bool)Session[WebConstants.SESSION_VENDOR_IS_CONTRACTOR];

            if (Session[WebConstants.SESSION_CACHE_SCREEN_ACCESS_DATA_TABLE] != null)
            {
                DataTable dtScreenAccess = (DataTable)Session[WebConstants.SESSION_CACHE_SCREEN_ACCESS_DATA_TABLE];
                DataRow[] drScreenAccess = dtScreenAccess.Select(WebConstants.COL_SCREEN_IDENTIFIER + " = '" + WebConstants.USER_CONTROL_PRODUCT_VERSION_SCREEN_IDENTIFIER + "'");

                if (drScreenAccess.Length == 1)
                {
                    if (drScreenAccess[0][WebConstants.COL_SCREEN_IS_READ_WRITE].ToString() == WebConstants.DEF_VAL_ACCESS_CHECKED)
                        isReadOnly = false;
                    else if (drScreenAccess[0][WebConstants.COL_SCREEN_IS_REPORT].ToString() == WebConstants.DEF_VAL_ACCESS_CHECKED)
                        isReport = true;
                    else
                        isReport = false;
                }
            }
        }

        /// <summary>
        /// SetScreenAccess
        /// </summary>
        private void SetScreenAccess()
        {
            if (Session[WebConstants.SESSION_IDENTIFIER] == null)
                ButtonViewProductVersion.Visible = false;

            if (!isProductVersionActive)
                isReadOnly = true;

            if (isReadOnly)
            {
                GridViewDocumentLinks.Columns[COL_GRID_DOCUMENT_SAVE_UPDATE_NO].Visible = false;
                GridViewDocumentLinks.Columns[COL_GRID_DOCUMENT_CANCEL_DELETE_NO].Visible = false;

                ButtonEditAboutProduct.Visible = false;
                ButtonSaveAboutProduct.Visible = false;
                TextBoxAboutProduct.Visible = false;
                LabelAboutProduct.Visible = true;
            }

            if (isReport)
                TabPanelOwnersAndUsers.Visible = false;

            SetAboutProduct();

            if (GridViewProjectPhases.Rows.Count < GridViewLocales.Rows.Count && GridViewProjectPhases.Rows.Count < GridViewPlatforms.Rows.Count)
                AddFooterPadding(GridViewProjectPhases.Rows.Count + 5);
            else if (GridViewLocales.Rows.Count < GridViewPlatforms.Rows.Count)
                AddFooterPadding(GridViewLocales.Rows.Count + 5);
            else
                AddFooterPadding(GridViewPlatforms.Rows.Count + 5);
        }

        /// <summary>
        /// PopulateScreenLabels
        /// </summary>
        /// <param name="userID"></param>
        private void PopulateScreenLabels()
        {
            DTO.Screens screenData = new DTO.Screens();
            screenData.ScreenIdentifier = WebConstants.USER_CONTROL_PRODUCT_VERSION_SCREEN_IDENTIFIER;
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

            if (Session[WebConstants.SESSION_IDENTIFIER] == null)
                Session[WebConstants.SESSION_IDENTIFIER_TEMP] = WebConstants.USER_CONTROL_PRODUCT_VERSION_SCREEN_IDENTIFIER;
        }

        /// <summary>
        /// SetScreenLabels
        /// </summary>
        /// <param name="dtScreenLabels"></param>
        private void SetScreenLabels()
        {
            PopulateScreenLabels();

            LabelNoProductFeatures.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelNoProductFeatures.Text);
            LabelNoProjectPhasesDefined.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelNoProjectPhasesDefined.Text);
            LabelNoLocalesAndPlatforms.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelNoLocalesAndPlatforms.Text);
            LabelNoLocalesVsPlatformMatrixDefined.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelNoLocalesVsPlatformMatrixDefined.Text);
            LabelNoActiveTeam.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelNoActiveTeam.Text);
            LabelNoTestStrategyDefined.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelNoTestStrategyDefined.Text);

            if (Session[WebConstants.SESSION_LOCALE_ID] != null && Session[WebConstants.SESSION_LOCALE_ID].ToString() != WebConstants.DEF_VAL_LOCALE_ID)
            {
                LabelHeader.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelHeader.Text);
                LabelProductPopulate.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelProductPopulate.Text);
                LabelProductVersionPopulate.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelProductVersionPopulate.Text);
                LabelAboutProduct.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelAboutProduct.Text);
                //LabelLinks.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelLinks.Text);
                //LabelUpload.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelUpload.Text);

                ButtonViewProductVersion.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, ButtonViewProductVersion.Text);
                ButtonEditAboutProduct.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, ButtonEditAboutProduct.Text);
                ButtonSaveAboutProduct.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, ButtonSaveAboutProduct.Text);

                TabPanelAboutProduct.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, TabPanelAboutProduct.HeaderText);
                TabPanelProjectPhases.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, TabPanelProjectPhases.HeaderText);
                TabPanelLocalesAndPlatforms.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, TabPanelLocalesAndPlatforms.HeaderText);
                TabPanelLocalesVsPlatform.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, TabPanelLocalesVsPlatform.HeaderText);
                TabPanelOwnersAndUsers.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, TabPanelOwnersAndUsers.HeaderText);
                //TabPanelMachineRequirements.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, TabPanelMachineRequirements.HeaderText);
                //TabPanelToolsUsed.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, TabPanelToolsUsed.HeaderText);
                TabPanelTestStrategy.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, TabPanelTestStrategy.HeaderText);
                TabPanelImportantLinks.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, TabPanelImportantLinks.HeaderText);

                foreach (DataControlField field in GridViewProjectPhases.Columns)
                    if (field.HeaderText != "")
                        field.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, field.HeaderText);

                foreach (DataControlField field in GridViewLocales.Columns)
                    if (field.HeaderText != "")
                        field.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, field.HeaderText);

                foreach (DataControlField field in GridViewPlatforms.Columns)
                    if (field.HeaderText != "")
                        field.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, field.HeaderText);

                foreach (DataControlField field in GridViewProductUsers.Columns)
                    if (field.HeaderText != "")
                        field.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, field.HeaderText);

                foreach (DataControlField field in GridViewDocumentLinks.Columns)
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
            LinkButton LinkButtonViewVersionDetails = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_VIEW_VERSION_DETAILS);

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
            if (LinkButtonViewVersionDetails != null)
                LinkButtonViewVersionDetails.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LinkButtonViewVersionDetails.Text);
        }

        /// <summary>
        /// SetAboutProduct
        /// </summary>
        private void SetAboutProduct()
        {
            if (LabelAboutProduct.Text == "")
            {
                if (isReadOnly)
                    LabelAboutProduct.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, STR_ABOUT_PRODUCT_NOT_DEFINED_READ_ONLY);
                else
                    LabelAboutProduct.Text = String.Format(COM.GetScreenLocalizedLabel(dtScreenLabels, STR_ABOUT_PRODUCT_NOT_DEFINED), COM.GetScreenLocalizedLabel(dtScreenLabels, STR_ABOUT_PRODUCT_EDIT_BUTTON));
            }
        }

        /// <summary>
        /// AddFooterPadding
        /// </summary>
        private void AddFooterPadding(int rowsCount)
        {
            PanelFooterPadding.Controls.Clear();

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

    /// <summary>
    /// GridViewPhaseTemplate
    /// </summary>
    public class GridViewPhaseTemplate : ITemplate
    {
        ListItemType _templateType;
        DataRow _drtemplateField;
        string _header;

        /// <summary>
        /// GridViewTemplate
        /// </summary>
        /// <param name="type"></param>
        /// <param name="drtemplateField"></param>
        public GridViewPhaseTemplate(ListItemType type, DataRow drtemplateField, string header)
        {
            _templateType = type;
            _drtemplateField = drtemplateField;
            _header = header;
        }

        /// <summary>
        /// InstantiateIn
        /// </summary>
        /// <param name="container"></param>
        void ITemplate.InstantiateIn(System.Web.UI.Control container)
        {
            switch (_templateType)
            {
                case ListItemType.Header:
                    Label lbCol = new Label();
                    lbCol.ID = "Label" + _header;
                    //lbCol.Text = "'<%# Bind(\"" + _drtemplateField["GridHeader"].ToString() + "\") %>'";
                    lbCol.DataBinding += new EventHandler(LabelDataBinding);

                    container.Controls.Add(lbCol);        //Adds the newly created label control to the container.
                    break;
            }
        }

        /// <summary>
        /// LabelDataBinding
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LabelDataBinding(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            GridViewRow container = (GridViewRow)lbl.NamingContainer;
            object dataValue = DataBinder.Eval(container.DataItem, _header);
            if (dataValue != DBNull.Value)
            {
                lbl.Text = dataValue.ToString();
            }
        }
    }
}