using System;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Adobe.Localization.Insights.Common;
using COM = Adobe.Localization.Insights.Common.Common;
using BO = Adobe.Localization.Insights.BusinessLayer;
using DTO = Adobe.Localization.Insights.Common.DataTransferObjects;

namespace Adobe.Localization.Insights.Web.UserControls
{
    /// <summary>
    /// ConsolidatedStatusReportUserControl
    /// </summary>
    public partial class ConsolidatedStatusReportUserControl : System.Web.UI.UserControl
    {
        #region Variables

        private string productID;
        private string userID;

        private DataTable dtCompileVendorsData;
        private ArrayList vendorDisplayColorSet;

        private DataTable dtScreenLabels;
        private DTO.Header dataHeader = new DTO.Header();

        private string prevWSRSection = "Old";
        private string nextWSRSection = "New";
        private int firstValue;
        private int nextValue;

        private bool isReport = false;
        private bool isContractor = false;

        private const string STR_ZERO = "0";
        private const string STR_NA = "NA";
        private const string STR_ALL = "ALL";
        private const string STR_VAL_ISACTIVE = "1";
        private const string STR_GRID_DEFAULT_VALUE = "0.00";
        private const string STR_EXECUTED = "EXECUTED";
        private const string STR_TEAM = "**TEAM : ";
        private const string STR_NONE = "None";
        private const string STR_MERGED_CELL_PARAMETERS = "Merged Cells Header";
        private const string STR_DATE_SORT = "WeekStartDate ASC";
        private const string STR_TESTING_TYPE_FUNCTIONAL = "1";
        private const string STR_TESTING_TYPE_LINGUISTIC = "2";
        private const string STR_PRODUCTIVITY_VALUE = "Productivity Value";

        private const int CONST_ZERO = 0;
        private const int NUM_MERGED_CELL_DATA_VENDOR = 2;
        private const int NUM_EACH_GRID_ROW_DISPLAY_HEIGHT = 20;

        private System.Drawing.Color headerBackColor = System.Drawing.Color.LightGray;

        #endregion

        #region Page Load

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            GetScreenAccess();
            SetScreenLabels();

            if (!IsPostBack)
            {
                SetReportingWeek();
                PopulateProducts();
                PopulateVendors();
            }

            SetScreenAccess();
        }

        #endregion

        //#region Button Events

        ///// <summary>
        ///// ButtonGenerateWSR_Click
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void ButtonGenerateWSR_Click(object sender, EventArgs e)
        //{
        //    string fileName = @"C:\Documents and Settings\sauragup\Desktop\SR-Illustrator16-WIPRO-WeekEnding-0413-v2.3.xlsm";
        //    string sheetName = "Status$";
        //    DataSet dsimportedData = BO.UsersBO.GetDataFromXLSM(fileName, sheetName);

        //    string column = "";
        //    foreach (DataColumn dc in dsimportedData.Tables[0].Columns)
        //    {
        //        column = column + ";" + dc.ColumnName;
        //    }

        //    int i = 0;

        //    //DTO.WSRData dtoWSRData = (DTO.WSRData)ViewState["WSRData"];
        //    //if (ValidateData())
        //    //{
        //    //    dtoWSRData = GetModifiedWSRData(dtoWSRData);
        //    //    SaveWSRData(dtoWSRData);

        //    //    if (dtoWSRData.WsrDataID == "")
        //    //    {
        //    //        LabelMessage.Text = "System is facing some issues. Please try afetr sometime.";
        //    //    }
        //    //    else
        //    //    {
        //    //        ViewState["WSRData"] = dtoWSRData;
        //    //        LabelMessage.Text = "WSR Data is saved successfully.";
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    LabelMessage.Text = "Metrics Data should be numeric.";
        //    //}
        //}

        //#endregion

        #region DropDownList Events

        /// <summary>
        /// DropDownListProducts_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListProducts_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateProductYear();
        }

        /// <summary>
        /// DropDownListProductYear_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListProductYear_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateProductVersion();
        }

        /// <summary>
        /// DropDownListProductVersion_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListProductVersion_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PopulatePhasesTypes();
        }

        /// <summary>
        /// DropDownListPhaseType_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListPhaseType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateProductSprints();
        }

        /// <summary>
        /// DropDownListProductSprint_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListProductSprint_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //if (DropDownListProductSprint.SelectedValue != STR_ZERO)

            PopulateProjectPhases();
        }

        /// <summary>
        /// DropDownListProjectPhases_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListProjectPhases_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateVendors();
        }

        /// <summary>
        /// DropDownListVendor_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListVendor_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateWSRData();
        }

        /// <summary>
        /// DropDownListReportingType_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListReportingType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            SetReportingWeek();
            PopulateWSRData();
        }

        /// <summary>
        /// DropDownListWeek_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListWeek_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateWSRData();
        }

        #endregion

        #region RadioButton Events

        /// <summary>
        /// RadioBtnPhaseBoth_OnCheckedChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RadioBtnPhaseBoth_OnCheckedChanged(object sender, EventArgs e)
        {
            LabelTestingTypeValue.Text = "";
            PopulateProjectPhases();
        }

        /// <summary>
        /// RadioBtnPhaseFunctional_OnCheckedChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RadioBtnPhaseFunctional_OnCheckedChanged(object sender, EventArgs e)
        {
            LabelTestingTypeValue.Text = STR_TESTING_TYPE_FUNCTIONAL;
            PopulateProjectPhases();
        }

        /// <summary>
        /// RadioBtnPhasePhaseLinguistic_OnCheckedChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RadioBtnPhasePhaseLinguistic_OnCheckedChanged(object sender, EventArgs e)
        {
            LabelTestingTypeValue.Text = STR_TESTING_TYPE_LINGUISTIC;
            PopulateProjectPhases();
        }

        #endregion

        #region Grid Events

        /// <summary>
        /// GridViewOutstandingDeliverables_OnRowDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewOutstandingDeliverables_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridViewRow dr = e.Row;

                Label LabelOriginalScheduleDate = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_ORIG_SCHEDULE_DATE);

                if (LabelOriginalScheduleDate.Text != "")
                    LabelOriginalScheduleDate.Text = (DateTime.Parse(LabelOriginalScheduleDate.Text)).ToShortDateString();
            }
        }

        /// <summary>
        /// GridViewEffortsTrack_RowCreated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewEffortsTrack_RowCreated(object sender, GridViewRowEventArgs e)
        {
            dtScreenLabels = (DataTable)ViewState[WebConstants.TBL_SCREEN_LABELS];

            if (e.Row.RowType == DataControlRowType.Header)
            {
                //Creating a gridview object            
                GridView objGridView = (GridView)sender;

                //Creating a gridview row object
                GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

                //Creating a table cell object
                TableCell objtablecell = new TableCell();

                #region Merge cells

                //Add a blank cell at the first three cell headers
                //This can be achieved by making the colspan property of the table cell object as 3
                // and the text property of the table cell object will be blank
                //Henceforth, add the table cell object to the grid view row object
                AddMergedCells(objgridviewrow, objtablecell, 2, COM.GetScreenLocalizedLabel(dtScreenLabels, STR_MERGED_CELL_PARAMETERS), headerBackColor.Name);

                if (dtCompileVendorsData != null && vendorDisplayColorSet != null)
                {
                    int counter = 1;
                    foreach (DataRow dr in dtCompileVendorsData.Rows)
                    {
                        AddMergedCells(objgridviewrow, objtablecell, NUM_MERGED_CELL_DATA_VENDOR, dr[WebConstants.COL_VENDOR].ToString(), vendorDisplayColorSet[1 - (counter % 2)].ToString());
                        counter++;
                    }
                }
                //Lastly add the gridrow object to the gridview object at the 0th position
                //Because,the header row position is 0.
                objGridView.Controls[CONST_ZERO].Controls.AddAt(CONST_ZERO, objgridviewrow);

                #endregion
            }
        }

        /// <summary>
        /// GridViewEffortsTrack_RowDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewEffortsTrack_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridViewRow gridRow = e.Row;

                Label LabelWSRSection = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_WSR_SECTION);
                nextWSRSection = LabelWSRSection.Text;

                if (nextWSRSection == prevWSRSection)
                {
                    LabelWSRSection.Text = "";
                    e.Row.Cells.Remove(e.Row.Cells[0]);
                    ((GridView)sender).Rows[firstValue].Cells[0].RowSpan = nextValue;
                    nextValue++;
                }
                else
                {
                    firstValue = e.Row.RowIndex;
                    nextValue = 2;
                    e.Row.Cells[0].BackColor = headerBackColor;
                    e.Row.Cells[0].Font.Bold = true;
                }

                prevWSRSection = nextWSRSection;

                //if (LabelWSRSection.Text == WebConstants.COL_TEMP_PRODUCTIVITY_OVERALL || LabelWSRSection.Text == WebConstants.COL_TEMP_PRODUCTIVITY || LabelWSRSection.Text == WebConstants.COL_TEMP_TOTAL || LabelWSRSection.Text == WebConstants.COL_TEMP_TOTAL_LOCALES)
                if (LabelWSRSection.Text == WebConstants.COL_TEMP_PRODUCTIVITY_OVERALL || LabelWSRSection.Text == WebConstants.COL_TEMP_PRODUCTIVITY || LabelWSRSection.Text.Contains(WebConstants.COL_TEMP_TOTAL) || LabelWSRSection.Text == WebConstants.COL_TEMP_TOTAL_TEAM_COUNT)
                {
                    gridRow.BackColor = headerBackColor;
                    gridRow.Font.Bold = true;
                    gridRow.HorizontalAlign = HorizontalAlign.Center;

                    if (LabelWSRSection.Text.Contains(WebConstants.COL_TEMP_TOTAL) && (LabelWSRSection.Text != WebConstants.COL_TEMP_TOTAL_LOCALES || LabelWSRSection.Text != WebConstants.COL_TEMP_TOTAL_TEAM_COUNT))
                    {
                        e.Row.Cells[1].Visible = false;
                        e.Row.Cells[1].Controls.Clear();
                        e.Row.Cells[0].Attributes[WebConstants.STR_COL_SPAN] = "2";
                    }

                    if (LabelWSRSection.Text == WebConstants.COL_TEMP_PRODUCTIVITY || LabelWSRSection.Text == WebConstants.COL_TEMP_PRODUCTIVITY_OVERALL || LabelWSRSection.Text == WebConstants.COL_TEMP_TOTAL_LOCALES || LabelWSRSection.Text == WebConstants.COL_TEMP_TOTAL_TEAM_COUNT)
                    {
                        for (int rowCount = 0; rowCount < ((GridView)sender).Columns.Count - 1; rowCount += 2)
                        {
                            gridRow.Cells[rowCount + 1].Visible = false;
                            gridRow.Cells[rowCount + 1].Controls.Clear();
                            gridRow.Cells[rowCount].Attributes[WebConstants.STR_COL_SPAN] = "2";
                        }
                    }
                }
            }
        }

        /// <summary>
        /// AddMergedCells
        /// </summary>
        /// <param name="drGridView"></param>
        /// <param name="tbcell"></param>
        /// <param name="colspan"></param>
        /// <param name="celltxt"></param>
        /// <param name="backColor"></param>
        protected void AddMergedCells(GridViewRow drGridView, TableCell tbcell, int colspan, string celltxt, string backColor)
        {
            tbcell = new TableCell();
            tbcell.Text = celltxt;
            tbcell.ColumnSpan = colspan;
            tbcell.Style.Add("background-color", backColor);
            tbcell.HorizontalAlign = HorizontalAlign.Center;
            drGridView.Cells.Add(tbcell);
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// SetReportingWeek
        /// </summary>
        private void SetReportingWeek()
        {
            DropDownListWeek.Visible = true;
            if (DropDownListReportingType.SelectedValue == WebConstants.DEF_VAL_REPORTING_TYPE_TOTAL)
            {
                DropDownListWeek.Visible = false;
                return;
            }

            DataTable dtWeek = BO.BusinessObjects.GetReportingWeek(DropDownListReportingType.SelectedValue).Tables[0];
            DropDownListWeek.DataSource = dtWeek;

            if (DropDownListReportingType.SelectedValue == WebConstants.DEF_VAL_REPORTING_TYPE_WEEKLY)
            {
                DropDownListWeek.DataValueField = WebConstants.COL_WEEK_ID;
                DropDownListWeek.DataTextField = WebConstants.COL_WEEK;
            }
            else if (DropDownListReportingType.SelectedValue == WebConstants.DEF_VAL_REPORTING_TYPE_MONTHLY)
            {
                DropDownListWeek.DataTextField = WebConstants.COL_MONTH;
                DropDownListWeek.DataValueField = WebConstants.COL_MONTH;
            }

            DropDownListWeek.DataBind();
        }

        /// <summary>
        /// PopulateProducts
        /// </summary>
        /// <returns></returns>
        private void PopulateProducts()
        {
            DataTable dtProducts = GetProducts();

            if (dtProducts.Rows.Count > 0)
            {
                if (isReport)
                {
                    DataRow drNew = dtProducts.NewRow();
                    drNew[WebConstants.COL_PRODUCT_ID] = CONST_ZERO;
                    drNew[WebConstants.COL_PRODUCT] = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.DEF_VAL_PRODUCTS_ALL);
                    dtProducts.Rows.InsertAt(drNew, CONST_ZERO);
                }

                DropDownListProducts.DataSource = dtProducts;
                DropDownListProducts.DataTextField = WebConstants.COL_PRODUCT;
                DropDownListProducts.DataValueField = WebConstants.COL_PRODUCT_ID;
                DropDownListProducts.DataBind();

                DropDownListProducts.SelectedIndex = DropDownListProducts.Items.IndexOf(DropDownListProducts.Items.FindByValue(productID));
                LabelProductName.Text = DropDownListProducts.SelectedItem.Text;

                if (!isReport)
                {
                    LabelProductName.Visible = true;
                    DropDownListProducts.Visible = false;
                }
            }

            PopulateProductYear();
        }

        /// <summary>
        /// GetProducts
        /// </summary>
        /// <returns></returns>
        private DataTable GetProducts()
        {
            DTO.Users userData = new DTO.Users();
            userData.UserID = Session[WebConstants.SESSION_USER_ID].ToString();

            if (Session[WebConstants.SESSION_PROJECT_ROLE_ID] != null)
                userData.ProjectRoleID = Session[WebConstants.SESSION_PROJECT_ROLE_ID].ToString();

            DataTable dtUserRoles = BO.BusinessObjects.GetUserProjectRoles(userData).Tables[0];
            DataRow[] drRPM = dtUserRoles.Select(WebConstants.COL_PROJECT_ROLE_CODE + " = '" + WebConstants.STR_PROJECT_ROLE_REPORT_MANAGER_CODE + "'");

            if (drRPM.Length > 0)
                userData.IsManager = true;

            DataTable dtProducts = BO.BusinessObjects.GetUserProducts(userData).Tables[0];

            DataView dvProducts = new DataView(dtProducts);
            return dvProducts.ToTable(true, WebConstants.COL_PRODUCT_ID, WebConstants.COL_PRODUCT);
        }

        /// <summary>
        /// PopulateProductYear
        /// </summary>
        private void PopulateProductYear()
        {
            DropDownListProductYear.Items.Clear();
            DropDownListProductVersion.Items.Clear();
            DropDownListProjectPhases.Items.Clear();

            DTO.Product productData = new DTO.Product();
            productData.ProductID = DropDownListProducts.SelectedValue;

            DataTable dtProductVersion = BO.BusinessObjects.GetProductVersion(productData).Tables[0];

            if (dtProductVersion.Rows.Count > 0)
            {
                DataTable dtDistinctProductYear = dtProductVersion.DefaultView.ToTable(true, WebConstants.COL_PRODUCT_YEAR);

                DropDownListProductYear.DataSource = dtDistinctProductYear;
                DropDownListProductYear.DataValueField = WebConstants.COL_PRODUCT_YEAR;
                DropDownListProductYear.DataTextField = WebConstants.COL_PRODUCT_YEAR;
                DropDownListProductYear.DataBind();

                DropDownListProductYear.Items.Insert(0, new ListItem("--Year--", "-1"));

                //DropDownListProductYear.SelectedIndex = DropDownListProductYear.Items.IndexOf(DropDownListProductYear.Items.FindByValue(System.DateTime.Now.Year.ToString()));
            }

            PopulateProductVersion();
        }

        /// <summary>
        /// PopulateProductVersion
        /// </summary>
        private void PopulateProductVersion()
        {
            DropDownListProductVersion.Items.Clear();
            DropDownListProjectPhases.Items.Clear();

            if (DropDownListProducts.SelectedValue != STR_ZERO)
            {
                DTO.Product productData = new DTO.Product();
                productData.ProductID = DropDownListProducts.SelectedValue;
                productData.ProductYear = DropDownListProductYear.SelectedValue;

                DataTable dtProductVersion = BO.BusinessObjects.GetProductVersionWithYear(productData, productData.ProductYear).Tables[0];

                if (dtProductVersion.Rows.Count > 0)
                {
                    DataRow drNew = dtProductVersion.NewRow();
                    drNew[WebConstants.COL_PRODUCT_VERSION_ID] = CONST_ZERO;
                    drNew[WebConstants.COL_PRODUCT_VERSION] = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.DEF_VAL_PRODUCT_VERSIONS_ALL); ;
                    dtProductVersion.Rows.InsertAt(drNew, CONST_ZERO);

                    DropDownListProductVersion.DataSource = dtProductVersion;
                    DropDownListProductVersion.DataValueField = WebConstants.COL_PRODUCT_VERSION_ID;
                    DropDownListProductVersion.DataTextField = WebConstants.COL_PRODUCT_VERSION;
                    DropDownListProductVersion.DataBind();

                    if (Session[WebConstants.SESSION_PRODUCT_VERSION_ID] != null)
                        DropDownListProductVersion.SelectedIndex = DropDownListProductVersion.Items.IndexOf(DropDownListProductVersion.Items.FindByValue(Session[WebConstants.SESSION_PRODUCT_VERSION_ID].ToString()));

                    PopulatePhasesTypes();
                    return;
                }
            }

            PopulateVendors();
        }

        /// <summary>
        /// PopulatePhasesTypes
        /// </summary>
        private void PopulatePhasesTypes()
        {
            DropDownListPhaseType.Items.Clear();

            DataTable dtPhaseTypes = BO.BusinessObjects.GetMasterDataWithTypeDetails(WebConstants.TBL_PHASE_TYPES, false);

            DataRow drNew = dtPhaseTypes.NewRow();
            drNew[WebConstants.COL_ID] = CONST_ZERO;
            drNew[WebConstants.COL_DESCRIPTION] = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.DEF_VAL_PHASE_TYPE_ALL); ;
            dtPhaseTypes.Rows.InsertAt(drNew, CONST_ZERO);

            DropDownListPhaseType.DataSource = dtPhaseTypes;
            DropDownListPhaseType.DataValueField = WebConstants.COL_ID;
            DropDownListPhaseType.DataTextField = WebConstants.COL_DESCRIPTION;
            DropDownListPhaseType.DataBind();

            //if (Session[WebConstants.SESSION_PHASE_TYPE_ID] != null)
            //    DropDownListPhaseType.SelectedIndex = DropDownListPhaseType.Items.IndexOf(DropDownListPhaseType.Items.FindByValue(Session[WebConstants.SESSION_PHASE_TYPE_ID].ToString()));

            PopulateProductSprints();
        }

        /// <summary>
        /// PopulateProductSprints
        /// </summary>
        private void PopulateProductSprints()
        {
            DropDownListProductSprint.Items.Clear();
            DropDownListProductSprint.Visible = false;

            if (DropDownListPhaseType.SelectedItem.Text == WebConstants.STR_CONST_SPRINTS)
            {
                DTO.Product productData = new DTO.Product();
                productData.ProductVersionID = DropDownListProductVersion.SelectedValue;
                DataTable dtProductSprints = BO.BusinessObjects.GetProductSprints(productData).Tables[0];

                if (dtProductSprints.Rows.Count > 1)
                {
                    DataRow drNew = dtProductSprints.NewRow();
                    drNew[WebConstants.COL_PRODUCT_SPRINT_ID] = CONST_ZERO;
                    drNew[WebConstants.COL_PRODUCT_SPRINT_DETAILS] = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.DEF_VAL_PRODUCT_SPRINT_ALL); ;
                    dtProductSprints.Rows.InsertAt(drNew, 0);
                }

                DropDownListProductSprint.DataSource = dtProductSprints;
                DropDownListProductSprint.DataValueField = WebConstants.COL_PRODUCT_SPRINT_ID;
                DropDownListProductSprint.DataTextField = WebConstants.COL_PRODUCT_SPRINT_DETAILS;
                DropDownListProductSprint.DataBind();

                DropDownListProductSprint.Visible = true;
                //if (Session[WebConstants.SESSION_PRODUCT_SPRINT_ID] != null)
                //    DropDownListProductSprint.SelectedIndex = DropDownListProductSprint.Items.IndexOf(DropDownListProductSprint.Items.FindByValue(Session[WebConstants.SESSION_PRODUCT_SPRINT_ID].ToString()));
            }

            PopulateProjectPhases();
        }

        /// <summary>
        /// PopulateProjectPhases
        /// </summary>
        private void PopulateProjectPhases()
        {
            DropDownListProjectPhases.Items.Clear();
            DropDownListVendor.Items.Clear();

            if (DropDownListProductVersion.SelectedValue != STR_ZERO)
            {
                DTO.ProjectPhases projectPhaseData = new DTO.ProjectPhases();
                projectPhaseData.ProductVersionID = DropDownListProductVersion.SelectedValue;
                projectPhaseData.PhaseTypeID = DropDownListPhaseType.SelectedValue;
                projectPhaseData.ProductSprintID = DropDownListProductSprint.SelectedValue;
                projectPhaseData.TestingTypeID = LabelTestingTypeValue.Text;

                DataTable dtProjectPhases = BO.BusinessObjects.GetProjectPhases(projectPhaseData).Tables[0];

                if (dtProjectPhases.Rows.Count > 0)
                {
                    DataRow drNew = dtProjectPhases.NewRow();
                    drNew[WebConstants.COL_PROJECT_PHASE_ID] = CONST_ZERO;
                    drNew[WebConstants.COL_PROJECT_PHASE] = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.DEF_VAL_PROJECT_PHASES_ALL); ;
                    dtProjectPhases.Rows.InsertAt(drNew, CONST_ZERO);

                    DropDownListProjectPhases.DataSource = dtProjectPhases;
                    DropDownListProjectPhases.DataValueField = WebConstants.COL_PROJECT_PHASE_ID;
                    DropDownListProjectPhases.DataTextField = WebConstants.COL_PROJECT_PHASE;
                    DropDownListProjectPhases.DataBind();

                    //if (Session[WebConstants.SESSION_PROJECT_PHASE_ID] != null)
                    //    DropDownListProjectPhases.SelectedIndex = DropDownListProjectPhases.Items.IndexOf(DropDownListProjectPhases.Items.FindByValue(Session[WebConstants.SESSION_PROJECT_PHASE_ID].ToString()));
                }
            }

            PopulateVendors();
        }

        /// <summary>
        /// PopulateVendors
        /// </summary>
        /// <param name="transferData"></param>
        private void PopulateVendors()
        {
            DropDownListVendor.Items.Clear();

            DTO.ProjectPhases projectPhaseData = new DTO.ProjectPhases();
            projectPhaseData.TestingTypeID = LabelTestingTypeValue.Text;
            projectPhaseData.PhaseTypeID = DropDownListPhaseType.SelectedValue;
            projectPhaseData.ProductSprintID = DropDownListProductSprint.SelectedValue;

            if (DropDownListProjectPhases.Items.Count > 0 && DropDownListProjectPhases.SelectedValue != "0")
                projectPhaseData.ProjectPhaseID = DropDownListProjectPhases.SelectedValue;

            if (DropDownListProductVersion.Items.Count > 0 && DropDownListProductVersion.SelectedValue != "0")
                projectPhaseData.ProductVersionID = DropDownListProductVersion.SelectedValue;

            if (DropDownListProducts.Items.Count > 0 && DropDownListProducts.SelectedValue != "0")
                projectPhaseData.ProductID = DropDownListProducts.SelectedValue;

            DataTable dtVendors = BO.BusinessObjects.GetProjectPhaseVendors(projectPhaseData).Tables[0];

            ViewState[WebConstants.TBL_VENDORS] = dtVendors;

            if (dtVendors.Rows.Count > 0)
            {
                DataRow drNew = dtVendors.NewRow();
                drNew[WebConstants.COL_VENDOR_ID] = CONST_ZERO;
                drNew[WebConstants.COL_VENDOR] = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.DEF_VAL_TEAMS_ALL); ;
                dtVendors.Rows.InsertAt(drNew, CONST_ZERO);

                DropDownListVendor.DataSource = dtVendors;
                DropDownListVendor.DataValueField = WebConstants.COL_VENDOR_ID;
                DropDownListVendor.DataTextField = WebConstants.COL_VENDOR;
                DropDownListVendor.DataBind();

                if (isContractor)
                {
                    DataRow[] drVendor = dtVendors.Select(WebConstants.COL_VENDOR_ID + "='" + Session[WebConstants.SESSION_VENDOR_ID].ToString() + "'");

                    if (drVendor.Length > 0)
                    {
                        DropDownListVendor.SelectedIndex = DropDownListVendor.Items.IndexOf(DropDownListVendor.Items.FindByValue(Session[WebConstants.SESSION_VENDOR_ID].ToString()));
                        LabelVendorName.Text = DropDownListVendor.SelectedItem.Text;
                    }
                    else
                    {
                        DropDownListVendor.Items.Clear();
                        LabelVendorName.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.STR_NOT_DEFINED);
                    }

                    LabelVendorName.Visible = true;
                    DropDownListVendor.Visible = false;
                }
            }

            PopulateWSRData();
        }

        /// <summary>
        /// PopulateWSRData
        /// </summary>
        private void PopulateWSRData()
        {
            ShowSelectedDateRange();

            DTO.WSRData dtoWSRData = new DTO.WSRData();

            if (DropDownListProducts.SelectedValue == CONST_ZERO.ToString())
            {
                StringBuilder productIDs = new StringBuilder("0");

                foreach (ListItem item in DropDownListProducts.Items)
                    productIDs.Append("," + item.Value);

                dtoWSRData.ProductID = productIDs.ToString();
            }
            else
            {
                dtoWSRData.ProductID = DropDownListProducts.SelectedValue;
            }

            dtoWSRData.ProductVersionID = DropDownListProductVersion.SelectedValue;
            dtoWSRData.ProductYear = DropDownListProductYear.SelectedValue;
            dtoWSRData.PhaseTypeID = DropDownListPhaseType.SelectedValue;
            dtoWSRData.ProductSprintID = DropDownListProductSprint.SelectedValue;
            dtoWSRData.TestingTypeID = LabelTestingTypeValue.Text;
            dtoWSRData.ProjectPhaseID = DropDownListProjectPhases.SelectedValue;

            if (DropDownListVendor.Items.Count > 0)
                dtoWSRData.VendorID = DropDownListVendor.SelectedValue;
            else
                dtoWSRData.VendorID = "-1";

            dtoWSRData.WeekID = DropDownListWeek.SelectedValue;
            dtoWSRData.ReportingType = DropDownListReportingType.SelectedValue;

            DataTable dtWSRParameters = BO.BusinessObjects.GetProductWSRParameters(dtoWSRData).Tables[0];

            dtoWSRData = BO.BusinessObjects.GetCombinedWSRData(dtoWSRData);

            dtoWSRData.WsrDataID = STR_ALL;
            DataSet dsWSRDetails = null;
            dsWSRDetails = BO.BusinessObjects.GetWSRDetails(dtoWSRData);

            ViewState[WebConstants.DTO_WSR_DATA] = dtoWSRData;

            #region Issues and Accomplishments

            LabelRedIssueDetails.Text = GetLocalizedValue(dtoWSRData.RedIssues);
            LabelYellowIssueDetails.Text = GetLocalizedValue(dtoWSRData.YellowIssues);
            LabelGreenAccomplishmentDetails.Text = GetLocalizedValue(dtoWSRData.GreenAccom);
            LabelFeaturesTestedDetails.Text = GetLocalizedValue(dtoWSRData.FeaturesTested);
            LabelNotesDetails.Text = GetLocalizedValue(dtoWSRData.Notes) + "<BR><BR>" + STR_TEAM.ToString() + GetLocalizedValue(dtoWSRData.ResourceNames);

            #endregion

            #region Total Efforts

            PopulateGridViewEfforts(dtWSRParameters, dsWSRDetails);

            #endregion

            #region Next Week Priorities

            //Prev Week Deliverables

            if (dtoWSRData.PrevWeekDeliverables != null && dtoWSRData.PrevWeekDeliverables.Tables.Count > 0 && dtoWSRData.PrevWeekDeliverables.Tables[0].Rows.Count > 1)
            {
                dtoWSRData.PrevWeekDeliverables.Tables[0].Rows.RemoveAt(dtoWSRData.PrevWeekDeliverables.Tables[0].Rows.Count - 1);
                GridViewOutstandingDeliverables.DataSource = dtoWSRData.PrevWeekDeliverables;
                GridViewOutstandingDeliverables.DataBind();
                GridViewOutstandingDeliverables.Visible = true;
                LabelBlueDeliverableDetails.Visible = false;
            }
            else
            {
                GridViewOutstandingDeliverables.DataSource = null;
                GridViewOutstandingDeliverables.DataBind();
                GridViewOutstandingDeliverables.Visible = false;
                LabelBlueDeliverableDetails.Visible = true;
                LabelBlueDeliverableDetails.Text = GetLocalizedValue(STR_NONE);
            }

            LabelBlackDeliverableDetails.Text = GetLocalizedValue(dtoWSRData.NewDeliverables);

            #endregion

            TabPanelIssuesandAccomplishments.Visible = false;

            if (DropDownListReportingType.SelectedValue == WebConstants.DEF_VAL_REPORTING_TYPE_WEEKLY && DropDownListProducts.SelectedValue != "0" && DropDownListProductVersion.SelectedValue != "0" || DropDownListVendor.SelectedValue != "0")
                TabPanelIssuesandAccomplishments.Visible = true;
        }

        /// <summary>
        /// ShowSelectedDateRange
        /// </summary>
        private void ShowSelectedDateRange()
        {
            DataTable dtWeek = BO.BusinessObjects.GetReportingWeek(STR_ALL).Tables[0];
            DataView dvWeek = new DataView(dtWeek);

            if (DropDownListReportingType.SelectedValue == WebConstants.DEF_VAL_REPORTING_TYPE_WEEKLY)
            {
                dvWeek.RowFilter = WebConstants.COL_WEEK_ID + " = " + DropDownListWeek.SelectedValue;
            }
            else if (DropDownListReportingType.SelectedValue == WebConstants.DEF_VAL_REPORTING_TYPE_MONTHLY)
            {
                dvWeek.RowFilter = WebConstants.COL_MONTH + " = '" + DropDownListWeek.SelectedValue + "'";
            }

            if (!(DropDownListReportingType.SelectedValue == WebConstants.DEF_VAL_REPORTING_TYPE_TOTAL) && dvWeek.Count > 0)
            {
                dvWeek.Sort = STR_DATE_SORT;
                LabelDateRangeValue.Text = ((DateTime)dvWeek[0][WebConstants.COL_WEEK_START_DATE]).ToShortDateString() + " - " + ((DateTime)dvWeek[dvWeek.Count - 1][WebConstants.COL_WEEK_END_DATE]).ToShortDateString();
                PanelReportGenerated.Visible = true;
            }
            else
                PanelReportGenerated.Visible = false;
        }

        /// <summary>
        /// PopulateGridViewEfforts
        /// </summary>
        /// <param name="dtWSRParameters"></param>
        /// <param name="dsWSRDetails"></param>
        private void PopulateGridViewEfforts(DataTable dtWSRParameters, DataSet dsWSRDetails)
        {
            GridViewEffortsTrack.Columns.Clear();

            dtCompileVendorsData = CustomizeVendorSet((DataTable)ViewState[WebConstants.TBL_VENDORS]);

            DataTable dtGridHeader = new DataTable();

            DataColumn headerCol = new DataColumn(WebConstants.COL_TEMP_GRID_HEADER, Type.GetType(WebConstants.STR_SYSTEM_STRING));
            dtGridHeader.Columns.Add(headerCol);
            headerCol = new DataColumn(WebConstants.COL_TEMP_GRID_HEADER_DISPLAY, Type.GetType(WebConstants.STR_SYSTEM_STRING));
            dtGridHeader.Columns.Add(headerCol);
            headerCol = new DataColumn(WebConstants.COL_VENDOR, Type.GetType(WebConstants.STR_SYSTEM_STRING));
            dtGridHeader.Columns.Add(headerCol);

            dtGridHeader.Rows.Add(WebConstants.COL_WSR_SECTION, GetLocalizedValue(WebConstants.COL_WSR_SECTION), "");
            dtGridHeader.Rows.Add(WebConstants.COL_WSR_PARAMETER, GetLocalizedValue(WebConstants.COL_WSR_PARAMETER), "");

            foreach (DataRow drVendor in dtCompileVendorsData.Rows)
            {
                string vendorID = drVendor[WebConstants.COL_VENDOR_ID].ToString();
                dtGridHeader.Rows.Add(WebConstants.COL_WSR_ACTUAL_QUANTITY + vendorID, GetLocalizedValue(WebConstants.COL_WSR_ACTUAL_QUANTITY), vendorID);
                dtGridHeader.Rows.Add(WebConstants.COL_WSR_REVISED_QUANTITY + vendorID, GetLocalizedValue(WebConstants.COL_WSR_REVISED_QUANTITY), vendorID);
                dtGridHeader.Rows.Add(WebConstants.COL_WSR_ACTUAL_EFFORTS + vendorID, GetLocalizedValue(WebConstants.COL_WSR_ACTUAL_EFFORTS), vendorID);
                dtGridHeader.Rows.Add(WebConstants.COL_WSR_REVISED_EFFORTS + vendorID, GetLocalizedValue(WebConstants.COL_WSR_REVISED_EFFORTS), vendorID);
            }

            DataTable dtCompiledEfforts = new DataTable();

            foreach (DataRow drGridHeader in dtGridHeader.Rows)
            {
                if (!drGridHeader[0].ToString().Contains("Actual"))
                    GridViewEffortsTrack.Columns.Add(CreateTemplateField(drGridHeader));

                DataColumn col = new DataColumn(drGridHeader[0].ToString(), Type.GetType(WebConstants.STR_SYSTEM_STRING));
                dtCompiledEfforts.Columns.Add(col);
            }
            ViewState[WebConstants.TBL_TEMP_COMPILED_EFFORTS] = dtCompiledEfforts;

            if (dtWSRParameters.Rows.Count > 0)
            {

                foreach (DataRow drParam in dtWSRParameters.Rows)
                {
                    DataRow drData = dtCompiledEfforts.NewRow();
                    drData[WebConstants.COL_WSR_SECTION] = drParam[WebConstants.COL_WSR_SECTION];
                    drData[WebConstants.COL_WSR_PARAMETER] = drParam[WebConstants.COL_WSR_PARAMETER];

                    foreach (DataRow drVendor in dtCompileVendorsData.Rows)
                    {
                        string vendorID = drVendor[WebConstants.COL_VENDOR_ID].ToString();

                        DataRow[] drActualsCol = dsWSRDetails.Tables[WebConstants.TBL_TEMP_ACTUAL_EFFORTS].Select(WebConstants.COL_WSR_PARAMETER_ID + " = " + drParam[WebConstants.COL_WSR_PARAMETER_ID].ToString() + " AND " + WebConstants.COL_VENDOR_ID + " = " + vendorID);

                        if (drActualsCol.Length > 0)
                        {
                            drData[WebConstants.COL_WSR_ACTUAL_EFFORTS + vendorID] = drActualsCol[0][WebConstants.COL_WSR_EFFORTS].ToString() == "" ? STR_ZERO : drActualsCol[0][WebConstants.COL_WSR_EFFORTS].ToString();
                            drData[WebConstants.COL_WSR_ACTUAL_QUANTITY + vendorID] = STR_NA;
                            drVendor[WebConstants.COL_WSR_ACTUAL_EFFORTS] = Convert.ToDouble(drVendor[WebConstants.COL_WSR_ACTUAL_EFFORTS]) + Convert.ToDouble(drData[WebConstants.COL_WSR_ACTUAL_EFFORTS + vendorID]);
                        }

                        DataRow[] drRevisedCol = dsWSRDetails.Tables[WebConstants.TBL_TEMP_REVISED_EFFORTS].Select(WebConstants.COL_WSR_PARAMETER_ID + " = " + drParam[WebConstants.COL_WSR_PARAMETER_ID].ToString() + " AND " + WebConstants.COL_VENDOR_ID + " = " + vendorID);

                        if (drRevisedCol.Length > 0)
                        {
                            drData[WebConstants.COL_WSR_REVISED_QUANTITY + vendorID] = drRevisedCol[0][WebConstants.COL_WSR_QUANTITY].ToString() == "" ? STR_ZERO : drRevisedCol[0][WebConstants.COL_WSR_QUANTITY].ToString();
                            drData[WebConstants.COL_WSR_REVISED_EFFORTS + vendorID] = drRevisedCol[0][WebConstants.COL_WSR_EFFORTS].ToString() == "" ? STR_ZERO : drRevisedCol[0][WebConstants.COL_WSR_EFFORTS].ToString();

                            drVendor[WebConstants.COL_WSR_REVISED_QUANTITY] = Convert.ToDouble(drVendor[WebConstants.COL_WSR_REVISED_QUANTITY]) + Convert.ToDouble(drData[WebConstants.COL_WSR_REVISED_QUANTITY + vendorID]);
                            drVendor[WebConstants.COL_WSR_REVISED_EFFORTS] = Convert.ToDouble(drVendor[WebConstants.COL_WSR_REVISED_EFFORTS]) + Convert.ToDouble(drData[WebConstants.COL_WSR_REVISED_EFFORTS + vendorID]);
                        }
                    }

                    dtCompiledEfforts.Rows.Add(drData);
                }

                if (DropDownListReportingType.SelectedValue == WebConstants.DEF_VAL_REPORTING_TYPE_TOTAL)
                {
                    foreach (DataRow dr in dtCompiledEfforts.Rows)
                    {
                        if (dr[WebConstants.COL_WSR_PARAMETER].ToString().ToUpper().Contains(STR_EXECUTED))
                        {
                            foreach (DataRow drActualQty in dsWSRDetails.Tables[2].Rows)
                            {
                                if (drActualQty[WebConstants.COL_TESTCASES_EXECUTED].ToString() != STR_ZERO)
                                {
                                    string vendorID = drActualQty[WebConstants.COL_VENDOR_ID].ToString();
                                    dr[WebConstants.COL_WSR_ACTUAL_QUANTITY + vendorID] = drActualQty[WebConstants.COL_TESTCASES_EXECUTED];

                                    DataRow[] drVendor = dtCompileVendorsData.Select(WebConstants.COL_VENDOR_ID + " = " + vendorID);
                                    drVendor[0][WebConstants.COL_WSR_ACTUAL_QUANTITY] = drActualQty[WebConstants.COL_TESTCASES_EXECUTED];
                                }
                            }
                        }
                    }
                }

                #region Add Locales, Resources Count, Total and Productivity Field

                DataRow drTotal = dtCompiledEfforts.NewRow();
                drTotal[WebConstants.COL_WSR_SECTION] = WebConstants.COL_TEMP_TOTAL;
                double totalEfforts = 0.00;

                DataRow drTotalLocales = dtCompiledEfforts.NewRow();
                drTotalLocales[WebConstants.COL_WSR_SECTION] = WebConstants.COL_TEMP_TOTAL_LOCALES;

                DataTable dtLocales = dsWSRDetails.Tables[WebConstants.TBL_TEMP_VENDOR_LOCALES];

                DataRow drTotalResources = dtCompiledEfforts.NewRow();
                drTotalResources[WebConstants.COL_WSR_SECTION] = WebConstants.COL_TEMP_TOTAL_TEAM_COUNT;

                DataTable dtResources = dsWSRDetails.Tables[WebConstants.TBL_WSR_RESOURCE_DATA];

                DataRow drProductivity = dtCompiledEfforts.NewRow();
                drProductivity[WebConstants.COL_WSR_SECTION] = WebConstants.COL_TEMP_PRODUCTIVITY;

                DataRow drOverallProductivity = dtCompiledEfforts.NewRow();
                drOverallProductivity[WebConstants.COL_WSR_SECTION] = WebConstants.COL_TEMP_PRODUCTIVITY_OVERALL;

                foreach (DataRow drVendor in dtCompileVendorsData.Rows)
                {
                    string vendorID = drVendor[WebConstants.COL_VENDOR_ID].ToString();

                    DataView dvLocales = dtLocales.DefaultView;
                    drTotalLocales[WebConstants.COL_WSR_REVISED_QUANTITY + vendorID] = dvLocales.ToTable(true, WebConstants.COL_VENDOR_ID, WebConstants.COL_LOCALE_ID).Select(WebConstants.COL_VENDOR_ID + " = " + vendorID).Length;

                    drTotalResources[WebConstants.COL_WSR_REVISED_QUANTITY + vendorID] = STR_NA;
                    DataRow[] drResources = dtResources.Select(WebConstants.COL_VENDOR_ID + " = " + vendorID);

                    double min = 0, max = 0, avg = 0;

                    if (drResources.Length > 0)
                    {
                        foreach (DataRow drResource in drResources)
                            if (drResource[WebConstants.COL_WSR_MAX_RESOURCE_COUNT] != null && drResource[WebConstants.COL_WSR_MAX_RESOURCE_COUNT].ToString() != "" && Convert.ToInt32(drResource[WebConstants.COL_WSR_MAX_RESOURCE_COUNT]) != 0)
                            {
                                min += Convert.ToDouble(drResource[WebConstants.COL_WSR_MIN_RESOURCE_COUNT]);
                                max += Convert.ToDouble(drResource[WebConstants.COL_WSR_MAX_RESOURCE_COUNT]);
                                avg += Convert.ToDouble(drResource[WebConstants.COL_WSR_AVG_RESOURCE_COUNT]);
                            }

                        if (DropDownListReportingType.SelectedValue == WebConstants.DEF_VAL_REPORTING_TYPE_WEEKLY)
                            drTotalResources[WebConstants.COL_WSR_REVISED_QUANTITY + vendorID] = String.Format("{0:f0}", max);
                        else
                            drTotalResources[WebConstants.COL_WSR_REVISED_QUANTITY + vendorID] = String.Format("Min:{0:f0};", min) + String.Format(" Max:{0:f0};", max) + String.Format(" Avg:{0:f1};", avg);
                    }

                    //drTotal[WebConstants.COL_WSR_ACTUAL_QUANTITY + vendorID] = drVendor[WebConstants.COL_WSR_ACTUAL_QUANTITY];
                    drTotal[WebConstants.COL_WSR_REVISED_QUANTITY + vendorID] = "";

                    //if (drVendor[WebConstants.COL_WSR_ACTUAL_EFFORTS].ToString() != "0.00")
                    //    drTotal[WebConstants.COL_WSR_ACTUAL_EFFORTS + vendorID] = drVendor[WebConstants.COL_WSR_ACTUAL_EFFORTS];

                    if (drVendor[WebConstants.COL_WSR_REVISED_EFFORTS].ToString() != "0.00")
                    {
                        drTotal[WebConstants.COL_WSR_REVISED_QUANTITY + vendorID] = STR_NA;
                        drTotal[WebConstants.COL_WSR_REVISED_EFFORTS + vendorID] = drVendor[WebConstants.COL_WSR_REVISED_EFFORTS];
                    }
                    DataRow drProductivitydata = dtCompiledEfforts.Rows[0];

                    if (drProductivitydata[WebConstants.COL_WSR_REVISED_EFFORTS + vendorID].ToString() != "" && Convert.ToDouble(drProductivitydata[WebConstants.COL_WSR_REVISED_EFFORTS + vendorID].ToString()) > 0)
                        drProductivity[WebConstants.COL_WSR_REVISED_QUANTITY + vendorID] = string.Format("{0:f2}", (Convert.ToDouble(drProductivitydata[WebConstants.COL_WSR_REVISED_QUANTITY + vendorID].ToString()) / Convert.ToDouble(drProductivitydata[WebConstants.COL_WSR_REVISED_EFFORTS + vendorID].ToString()))) + " " + COM.GetScreenLocalizedLabel(dtScreenLabels, STR_PRODUCTIVITY_VALUE);

                    double overallQty = 0.00;
                    double overallEfforts = 0.00;
                    foreach (DataRow drTotalProductivity in dtCompiledEfforts.Rows)
                    {
                        if (drTotalProductivity[WebConstants.COL_WSR_SECTION].ToString().ToUpper() != WebConstants.COL_TEMP_SECTION_VALUE_MISCELLANEOUS)
                            if (drTotalProductivity[WebConstants.COL_WSR_REVISED_QUANTITY + vendorID].ToString() != "" && Convert.ToDouble(drTotalProductivity[WebConstants.COL_WSR_REVISED_QUANTITY + vendorID].ToString()) > 0)
                                overallQty += Convert.ToDouble(drTotalProductivity[WebConstants.COL_WSR_REVISED_QUANTITY + vendorID]);

                        if (drTotalProductivity[WebConstants.COL_WSR_REVISED_EFFORTS + vendorID].ToString() != "" && Convert.ToDouble(drTotalProductivity[WebConstants.COL_WSR_REVISED_EFFORTS + vendorID].ToString()) > 0)
                            overallEfforts += Convert.ToDouble(drTotalProductivity[WebConstants.COL_WSR_REVISED_EFFORTS + vendorID].ToString());
                    }

                    if (overallEfforts != 0.00)
                        drOverallProductivity[WebConstants.COL_WSR_REVISED_QUANTITY + vendorID] = string.Format("{0:f2}", (overallQty / overallEfforts));

                    totalEfforts += overallEfforts;
                }

                drTotal[WebConstants.COL_WSR_SECTION] = WebConstants.COL_TEMP_TOTAL + String.Format(" ( {0} hrs )", totalEfforts);

                #endregion

                dtCompiledEfforts.Rows.Add(drTotal);

                if (!isContractor)
                {
                    dtCompiledEfforts.Rows.Add(drTotalLocales);
                    dtCompiledEfforts.Rows.Add(drTotalResources);
                    dtCompiledEfforts.Rows.Add(drProductivity);
                    dtCompiledEfforts.Rows.Add(drOverallProductivity);
                }

                int counter = 0;
                foreach (DataControlField field in GridViewEffortsTrack.Columns)
                {
                    if (counter < 2)
                        field.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                    counter++;
                }
            }

            GridViewEffortsTrack.DataSource = dtCompiledEfforts;
            GridViewEffortsTrack.DataBind();
        }

        /// <summary>
        /// AddColumnsToVendor
        /// </summary>
        private DataTable CustomizeVendorSet(DataTable dtVendors)
        {
            DataTable dtVendorsData = dtVendors.Clone();

            for (int vendorCnt = 1; vendorCnt < dtVendors.Rows.Count; vendorCnt++)
            {
                if (DropDownListVendor.SelectedValue == STR_ZERO || DropDownListVendor.SelectedValue == dtVendors.Rows[vendorCnt][WebConstants.COL_VENDOR_ID].ToString())
                {
                    DataRow drNew = dtVendorsData.NewRow();

                    drNew[WebConstants.COL_VENDOR_ID] = dtVendors.Rows[vendorCnt][WebConstants.COL_VENDOR_ID];
                    drNew[WebConstants.COL_VENDOR] = dtVendors.Rows[vendorCnt][WebConstants.COL_VENDOR];

                    dtVendorsData.Rows.Add(drNew);
                }
            }

            DataColumn headerCol = new DataColumn(WebConstants.COL_WSR_ACTUAL_QUANTITY, Type.GetType(WebConstants.STR_SYSTEM_STRING));
            headerCol.DefaultValue = STR_GRID_DEFAULT_VALUE;
            dtVendorsData.Columns.Add(headerCol);
            headerCol = new DataColumn(WebConstants.COL_WSR_REVISED_QUANTITY, Type.GetType(WebConstants.STR_SYSTEM_STRING));
            headerCol.DefaultValue = STR_GRID_DEFAULT_VALUE;
            dtVendorsData.Columns.Add(headerCol);
            headerCol = new DataColumn(WebConstants.COL_WSR_ACTUAL_EFFORTS, Type.GetType(WebConstants.STR_SYSTEM_STRING));
            headerCol.DefaultValue = STR_GRID_DEFAULT_VALUE;
            dtVendorsData.Columns.Add(headerCol);
            headerCol = new DataColumn(WebConstants.COL_WSR_REVISED_EFFORTS, Type.GetType(WebConstants.STR_SYSTEM_STRING));
            headerCol.DefaultValue = STR_GRID_DEFAULT_VALUE;
            dtVendorsData.Columns.Add(headerCol);

            vendorDisplayColorSet = new ArrayList();
            vendorDisplayColorSet.Add(System.Drawing.Color.LightGreen.Name);
            vendorDisplayColorSet.Add(System.Drawing.Color.LightSkyBlue.Name);

            return dtVendorsData;
        }

        /// <summary>
        /// CreateTemplateField
        /// </summary>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        private DataControlField CreateTemplateField(DataRow drtemplateField)
        {
            TemplateField tf = new TemplateField();

            tf.HeaderText = drtemplateField[WebConstants.COL_TEMP_GRID_HEADER_DISPLAY].ToString();
            tf.ItemTemplate = new GridViewWSRTemplate(ListItemType.Header, drtemplateField);

            return tf;
        }

        #endregion

        #region Screen Access and Labels

        /// <summary>
        /// GetScreenAccess
        /// </summary>
        private void GetScreenAccess()
        {
            dataHeader.LoginID = Session[WebConstants.SESSION_LOGIN_ID].ToString();
            userID = Session[WebConstants.SESSION_USER_ID].ToString();

            if (Session[WebConstants.SESSION_PRODUCT_ID] != null)
                productID = Session[WebConstants.SESSION_PRODUCT_ID].ToString();

            isContractor = (bool)Session[WebConstants.SESSION_VENDOR_IS_CONTRACTOR];

            if (Session[WebConstants.SESSION_CACHE_SCREEN_ACCESS_DATA_TABLE] != null)
            {
                DataTable dtScreenAccess = (DataTable)Session[WebConstants.SESSION_CACHE_SCREEN_ACCESS_DATA_TABLE];

                DataRow drScreenAccess = dtScreenAccess.Select(WebConstants.COL_SCREEN_IDENTIFIER + " = '" + Session[WebConstants.SESSION_IDENTIFIER] + "'")[0];

                if (drScreenAccess[WebConstants.COL_SCREEN_IS_REPORT].ToString() == WebConstants.DEF_VAL_ACCESS_CHECKED)
                    isReport = true;
            }
        }

        /// <summary>
        /// SetScreenAccess
        /// </summary>
        private void SetScreenAccess()
        {
            AddFooterPadding(1000);
        }

        /// <summary>
        /// GetLocalizedValue
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        private string GetLocalizedValue(string label)
        {
            if (label != null && label != "")
                return COM.GetScreenLocalizedLabel(dtScreenLabels, label);
            else
                return COM.GetScreenLocalizedLabel(dtScreenLabels, STR_NONE);
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
            ViewState[WebConstants.TBL_SCREEN_LABELS] = dtScreenLabels;
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
                LabelProjectPhases.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelProjectPhases.Text);
                LabelTestingType.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelTestingType.Text);
                LabelVendor.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelVendor.Text);
                LabelWeek.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelWeek.Text);
                LabelDateRange.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelDateRange.Text);

                TabPanelIssuesandAccomplishments.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, TabPanelIssuesandAccomplishments.HeaderText);
                TabPanelMetrics.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, TabPanelMetrics.HeaderText);

                foreach (DataControlField field in GridViewOutstandingDeliverables.Columns)
                    if (field.HeaderText != "")
                        field.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, field.HeaderText);

                foreach (ListItem item in DropDownListReportingType.Items)
                    item.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, item.Value);
            }

            LabelIssuesandAccomplishments.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelIssuesandAccomplishments.Text);
            LabelRedIssues.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelRedIssues.Text);
            LabelYellowIssues.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelYellowIssues.Text);
            LabelFeaturesTested.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelFeaturesTested.Text);
            LabelGreenAccomplishments.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelGreenAccomplishments.Text);
            LabelNextWeekPriority.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelNextWeekPriority.Text);
            LabelBlueDeliverables.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelBlueDeliverables.Text);
            LabelBlackDeliverables.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelBlackDeliverables.Text);
            LabelNotes.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelNotes.Text);

            foreach (DataControlField field in GridViewEffortsTrack.Columns)
                if (field.HeaderText != "")
                    field.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, field.HeaderText);
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

    /// <summary>
    /// GridViewWSRTemplate
    /// </summary>
    public class GridViewWSRTemplate : ITemplate
    {
        ListItemType _templateType;
        DataRow _drtemplateField;

        /// <summary>
        /// GridViewWSRTemplate
        /// </summary>
        /// <param name="type"></param>
        /// <param name="drtemplateField"></param>
        public GridViewWSRTemplate(ListItemType type, DataRow drtemplateField)
        {
            _templateType = type;
            _drtemplateField = drtemplateField;
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
                    lbCol.ID = "Label" + _drtemplateField[WebConstants.COL_TEMP_GRID_HEADER].ToString();
                    //lbCol.Text = "'<%# Bind(\"" + _drtemplateField["GridHeader"].ToString() + "\") %>'";
                    lbCol.DataBinding += new EventHandler(LabelDataBinding);

                    container.Controls.Add(lbCol);        //Adds the newly created label control to the container.
                    break;


                //case ListItemType.Header:
                //    //Creates a new label control and add it to the container.
                //    Label lbl = new Label();            //Allocates the new label object.
                //    lbl.Text = _drtemplateField;             //Assigns the name of the column in the lable.
                //    container.Controls.Add(lbl);        //Adds the newly created label control to the container.
                //    break;

                //case ListItemType.Item:
                //    //Creates a new text box control and add it to the container.
                //    TextBox tb1 = new TextBox();                            //Allocates the new text box object.
                //    tb1.DataBinding += new EventHandler(tb1_DataBinding);   //Attaches the data binding event.
                //    tb1.Columns = 4;                                        //Creates a column with size 4.
                //    container.Controls.Add(tb1);                            //Adds the newly created textbox to the container.
                //    break;

                //case ListItemType.EditItem:
                //    //As, I am not using any EditItem, I didnot added any code here.
                //    break;

                //case ListItemType.Footer:
                //    CheckBox chkColumn = new CheckBox();
                //    chkColumn.ID = "Chk" + _drtemplateField;
                //    container.Controls.Add(chkColumn);
                //    break;
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
            object dataValue = DataBinder.Eval(container.DataItem, _drtemplateField[WebConstants.COL_TEMP_GRID_HEADER].ToString());
            if (dataValue != DBNull.Value)
            {
                lbl.Text = dataValue.ToString();
            }
        }
    }
}