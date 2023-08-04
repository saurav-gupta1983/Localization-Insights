using System;
using System.Data;
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
    /// WSRDataUserControl
    /// </summary>
    public partial class WSRDataUserControl : System.Web.UI.UserControl
    {
        #region Variables

        private string weekID;
        private string productID;
        private string vendorID;
        private string userID;
        private string wsrDataID;

        private Control controlFired;

        private DataTable dtScreenLabels;
        private DTO.Header dataHeader = new DTO.Header();

        //private string prevWSRSection = "Old";
        //private string nextWSRSection = "New";
        //private int firstValue;
        //private int nextValue;

        private bool isReadOnly = false;
        private bool isContractor = false;
        private bool isProductVersionActive = false;

        private const string STR_NON_ACTIVE = "Not Active";
        private const string STR_NA = "NA";
        private const string STR_ZERO = "0";
        private const string STR_VAL_ISACTIVE = "1";

        private const string STR_CALENDAR_USER_CONTROL = "CalendarUserControl";
        private const string STR_BUTTON_SAVE = "ButtonSave";
        private const string STR_BUTTON_DELETE = "ButtonDelete";
        private const string STR_COMMAND_SAVE = "Save";
        private const string STR_COMMAND_DELETE = "Delete";
        private const string STR_MERGED_CELL_PARAMETERS = "Merged Cells Header";
        private const string STR_MESSAGE_TEAM_MANDATORY = "Please provide Team information.";

        private const int VAL_ACTIVE_TAB_INDEX_NEXT_WEEK = 2;
        private const int CONST_ZERO = 0;
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

            LabelMessage.Text = "";

            //SetProduct();
            SetVendorInfo();
            SetWeek();

            if (!IsPostBack)
            {
                PopulateProductVersion();
                if (DropDownListProductVersion.SelectedValue != "")
                {
                    PopulateProjectPhases();
                }
                PopulateWSRData();
            }
            else
            {
                controlFired = GetPostBackControl(Page);
            }

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
                if (controlFired.ID.Contains(STR_CALENDAR_USER_CONTROL) || controlFired.ID.Contains(STR_BUTTON_SAVE) || controlFired.ID.Contains(STR_BUTTON_DELETE))
                    TabContainerWSR.ActiveTabIndex = VAL_ACTIVE_TAB_INDEX_NEXT_WEEK;
        }

        #endregion

        #region Button Events

        /// <summary>
        /// ButtonSubmitWSR_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSubmitWSR_Click(object sender, EventArgs e)
        {
            if (!(TextBoxResourcesCount.Text == "" || TextBoxResourcesName.Text == ""))
            {
                if (BO.BusinessObjects.AddUpdateWSRData(GetModifiedWSRData()))
                {
                    LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_SUCCESS_SAVE_MESSAGE);
                    PopulateWSRData();
                }
                else
                    LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_SAVE_MESSAGE);
            }
            else
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, STR_MESSAGE_TEAM_MANDATORY);
        }

        /// <summary>
        /// ButtonImport_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonImport_Click(object sender, EventArgs e)
        {
            string fileName = @"C:\Documents and Settings\sauragup\Desktop\SR-Illustrator16-WIPRO-WeekEnding-0413-v2.3.xlsm";
            string sheetName = "Status$";
            DataSet dsimportedData = BO.BusinessObjects.GetDataFromXLSM(fileName, sheetName);

            string column = "";
            foreach (DataColumn dc in dsimportedData.Tables[0].Columns)
            {
                column = column + ";" + dc.ColumnName;
            }

            //int i = 0;

            //DTO.WSRData dtoWSRData = (DTO.WSRData)ViewState["WSRData"];
            //if (ValidateData())
            //{
            //    dtoWSRData = GetModifiedWSRData(dtoWSRData);
            //    SaveWSRData(dtoWSRData);

            //    if (dtoWSRData.WsrDataID == "")
            //    {
            //        LabelMessage.Text = "System is facing some issues. Please try afetr sometime.";
            //    }
            //    else
            //    {
            //        ViewState["WSRData"] = dtoWSRData;
            //        LabelMessage.Text = "WSR Data is saved successfully.";
            //    }
            //}
            //else
            //{
            //    LabelMessage.Text = "Metrics Data should be numeric.";
            //}
        }

        #endregion

        #region GridView Events

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

                AddMergedCells(objgridviewrow, objtablecell, 2, COM.GetScreenLocalizedLabel(dtScreenLabels, STR_MERGED_CELL_PARAMETERS), headerBackColor.Name);
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

                Label LabelWSRSection = (Label)gridRow.FindControl(WebConstants.CONTROL_LABEL_WSR_SECTION);
                //nextWSRSection = LabelWSRSection.Text;

                //if (nextWSRSection == prevWSRSection)
                //{
                //    LabelWSRSection.Text = "";
                //    e.Row.Cells.Remove(gridRow.Cells[0]);
                //    ((GridView)sender).Rows[firstValue].Cells[0].RowSpan = nextValue;
                //    nextValue++;
                //}
                //else
                //{
                //    firstValue = gridRow.RowIndex;
                //    nextValue = 2;
                //    gridRow.Cells[0].BackColor = headerBackColor;
                //    gridRow.Cells[0].Font.Bold = true;
                //}
                gridRow.Cells[0].BackColor = headerBackColor;
                gridRow.Cells[0].Font.Bold = true;

                //prevWSRSection = nextWSRSection;

                if (LabelWSRSection.Text == WebConstants.COL_TEMP_TOTAL)
                {
                    //e.Row.Enabled = false;
                    gridRow.BackColor = headerBackColor;
                    gridRow.Font.Bold = true;
                    gridRow.HorizontalAlign = HorizontalAlign.Center;

                    //gridRow.Cells[1].Visible = false;
                    //gridRow.Cells[1].Controls.Clear();
                    //gridRow.Cells[0].Attributes[WebConstants.STR_COL_SPAN] = "2";

                    Label LabelRevisedQuantity = (Label)gridRow.FindControl(WebConstants.CONTROL_LABEL_REVISED_QUANTITY);
                    TextBox TextBoxRevisedQuantity = (TextBox)gridRow.FindControl(WebConstants.CONTROL_TEXTBOX_REVISED_QUANTITY);
                    TextBoxRevisedQuantity.Visible = false;
                    LabelRevisedQuantity.Visible = true;

                    Label LabelRevisedEfforts = (Label)gridRow.FindControl(WebConstants.CONTROL_LABEL_REVISED_EFFORTS);
                    TextBox TextBoxRevisedEfforts = (TextBox)gridRow.FindControl(WebConstants.CONTROL_TEXTBOX_REVISED_EFFORTS);
                    TextBoxRevisedEfforts.Visible = false;
                    LabelRevisedEfforts.Visible = true;
                    LabelRevisedEfforts.Text = TextBoxRevisedEfforts.Text;

                    TextBox TextBoxRemarks = (TextBox)gridRow.FindControl(WebConstants.CONTROL_TEXTBOX_REMARKS);
                    TextBoxRemarks.Visible = false;
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

        /// <summary>
        /// GridViewOutstandingDeliverables_OnRowCommand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewOutstandingDeliverables_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            DTO.WSRData dtoWSRData = (DTO.WSRData)ViewState[WebConstants.DTO_WSR_DATA];
            DataSet prevWeekDeliverables = dtoWSRData.PrevWeekDeliverables;

            GridViewRow gridViewRow = (GridViewRow)(((Button)e.CommandSource).NamingContainer); ;
            int index = gridViewRow.RowIndex;

            TextBox textBoxPrevWeekDeliverables = (TextBox)gridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_PREVIOUS_WEEK_DELIVERABLES);
            TextBox textBoxReason = (TextBox)gridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_REASON);
            Calendar calendarOrigScheduleDate = (Calendar)gridViewRow.FindControl(WebConstants.CONTROL_DATECALENDAR_ORIG_SCHEDULE_DATE);
            Button buttonDelete = ((Button)gridViewRow.FindControl(WebConstants.CONTROL_BUTTON_DELETE));

            if (e.CommandName == STR_COMMAND_SAVE)
            {
                if (textBoxPrevWeekDeliverables.Text != "")
                {
                    if (index == prevWeekDeliverables.Tables[0].Rows.Count - 1)
                    {
                        DataRow dr = prevWeekDeliverables.Tables[0].Rows[prevWeekDeliverables.Tables[0].Rows.Count - 1];
                        dr[WebConstants.COL_WSR_PREV_WEEK_DELIVERABLES] = textBoxPrevWeekDeliverables.Text;
                        dr[WebConstants.COL_WSR_ORIG_SCHEDULE_DATE] = (DateTime.Parse(calendarOrigScheduleDate.GetDate())).ToShortDateString();
                        dr[WebConstants.COL_WSR_REASON] = textBoxReason.Text;
                        prevWeekDeliverables.Tables[0].Rows.InsertAt(prevWeekDeliverables.Tables[0].NewRow(), prevWeekDeliverables.Tables[0].Rows.Count);
                        buttonDelete.Enabled = true;
                    }
                    else
                    {
                        DataRow dr = prevWeekDeliverables.Tables[0].Rows[index];
                        dr[WebConstants.COL_WSR_PREV_WEEK_DELIVERABLES] = textBoxPrevWeekDeliverables.Text;
                        dr[WebConstants.COL_WSR_ORIG_SCHEDULE_DATE] = (DateTime.Parse(calendarOrigScheduleDate.GetDate())).ToShortDateString();
                        dr[WebConstants.COL_WSR_REASON] = textBoxReason.Text;
                    }
                }
            }

            if (e.CommandName == STR_COMMAND_DELETE)
            {
                DataRow dr = prevWeekDeliverables.Tables[0].Rows[index];
                dr.Delete();
            }

            ViewState[WebConstants.DTO_WSR_DATA] = dtoWSRData;
            GridViewOutstandingDeliverables.DataSource = dtoWSRData.PrevWeekDeliverables;
            GridViewOutstandingDeliverables.DataBind();
        }

        /// <summary>
        /// GridViewOutstandingDeliverables_OnRowDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewOutstandingDeliverables_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            TextBox TextBoxPrevWeekDeliverables = (TextBox)e.Row.FindControl(WebConstants.CONTROL_TEXTBOX_PREVIOUS_WEEK_DELIVERABLES);

            if (TextBoxPrevWeekDeliverables != null)
            {
                Calendar calendar = (Calendar)e.Row.FindControl(WebConstants.CONTROL_DATECALENDAR_ORIG_SCHEDULE_DATE);
                HtmlInputButton dateButton;
                HtmlGenericControl dateDiv;

                dateButton = (HtmlInputButton)calendar.FindControl(WebConstants.CONTROL_DATECALENDAR_BUTTON);
                dateDiv = (HtmlGenericControl)calendar.FindControl(WebConstants.CONTROL_DATECALENDAR_DIVCALENDAR);
                dateButton.Attributes.Add(WebConstants.CONTROL_DATECALENDAR_ONCLICK, "javascript:return OnClick('" + dateDiv.ClientID + "')");

                if (TextBoxPrevWeekDeliverables.Text == "")
                {
                    ((Button)e.Row.FindControl(WebConstants.CONTROL_BUTTON_DELETE)).Enabled = false;
                }
            }
        }

        /// <summary>
        /// GridViewOutstandingDeliverables_OnRowDeleting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewOutstandingDeliverables_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }

        #endregion

        #region DropDownList Events

        /// <summary>
        /// DropDownListProductVersion_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListProductVersion_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateProjectPhases();
        }

        /// <summary>
        /// DropDownListProjectPhases_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListProjectPhases_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateWSRData();
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

        #endregion

        #region Private Functions

        /// <summary>
        /// SetWeek
        /// </summary>
        private void SetWeek()
        {
            DataSet week = BO.BusinessObjects.GetWeeksInfo();

            if (week.Tables.Count > 0)
            {
                if (week.Tables[0].Rows.Count > 0)
                {
                    LabelWeekName.Text = Convert.ToDateTime(week.Tables[0].Rows[0][WebConstants.COL_WEEK_START_DATE]).ToShortDateString() + " - " + Convert.ToDateTime(week.Tables[0].Rows[0][WebConstants.COL_WEEK_END_DATE]).ToShortDateString();
                    weekID = week.Tables[0].Rows[0][WebConstants.COL_WEEK_ID].ToString();
                }
            }
        }

        ///// <summary>
        ///// SetProduct
        ///// </summary>
        ///// <param name="productID"></param>
        ///// <returns></returns>
        //private void SetProduct()
        //{
        //    DTO.Product productData = new DTO.Product();
        //    productData.ProductID = productID;

        //    DataSet product = BO.BusinessObjects.GetProducts(productData);

        //    if (product.Tables.Count > 0)
        //    {
        //        if (product.Tables[0].Rows.Count > 0)
        //        {
        //            LabelProductName.Text = product.Tables[0].Rows[0][WebConstants.COL_PRODUCT].ToString();
        //        }
        //    }
        //}

        /// <summary>
        /// GetVendorInfo
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private void SetVendorInfo()
        {
            DTO.Users userData = new DTO.Users();
            userData.UserID = userID;
            userData.VendorID = vendorID;

            DataTable dtVendor = BO.BusinessObjects.GetVendorDetails(userData).Tables[0];

            LabelVendorName.Text = dtVendor.Rows[0][WebConstants.COL_VENDOR].ToString();
        }

        ///// <summary>
        ///// PopulateProductVersion
        ///// </summary>
        //private void PopulateProductVersion()
        //{
        //    DTO.Product productData = new DTO.Product();
        //    productData.ProductID = productID;
        //    productData.IsActive = STR_VAL_ISACTIVE;

        //    DataSet productVersion = BO.BusinessObjects.GetProductVersion(productData);

        //    DropDownListProductVersion.DataSource = productVersion;
        //    DropDownListProductVersion.DataValueField = WebConstants.COL_PRODUCT_VERSION_ID;
        //    DropDownListProductVersion.DataTextField = WebConstants.COL_PRODUCT_VERSION;
        //    DropDownListProductVersion.DataBind();

        //    if (Session[WebConstants.SESSION_PRODUCT_VERSION_ID] != null)
        //        DropDownListProductVersion.SelectedIndex = DropDownListProductVersion.Items.IndexOf(DropDownListProductVersion.Items.FindByValue(Session[WebConstants.SESSION_PRODUCT_VERSION_ID].ToString()));
        //}

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

            PopulateProjectPhases();
        }

        /// <summary>
        /// PopulateProjectPhases
        /// </summary>
        private void PopulateProjectPhases()
        {
            DropDownListProjectPhases.Items.Clear();
            DropDownListVendor.Items.Clear();

            DTO.ProjectPhases projectPhaseData = new DTO.ProjectPhases();
            projectPhaseData.ProductVersionID = DropDownListProductVersion.SelectedValue;
            projectPhaseData.IsActive = true;

            if (isContractor)
                projectPhaseData.VendorID = Session[WebConstants.SESSION_VENDOR_ID].ToString();

            DataTable dtProjectPhases = BO.BusinessObjects.GetProjectPhases(projectPhaseData).Tables[0];

            if (dtProjectPhases.Rows.Count > 0)
            {
                DropDownListProjectPhases.DataSource = dtProjectPhases;
                DropDownListProjectPhases.DataValueField = WebConstants.COL_PROJECT_PHASE_ID;
                DropDownListProjectPhases.DataTextField = WebConstants.COL_PROJECT_PHASE;
                DropDownListProjectPhases.DataBind();

                if (Session[WebConstants.SESSION_PROJECT_PHASE_ID] != null)
                    DropDownListProjectPhases.SelectedIndex = DropDownListProjectPhases.Items.IndexOf(DropDownListProjectPhases.Items.FindByValue(Session[WebConstants.SESSION_PROJECT_PHASE_ID].ToString()));

                PopulateVendors();
            }
            else
            {
                DropDownListVendor.Items.Clear();
                PopulateWSRData();
            }
        }

        /// <summary>
        /// PopulateVendors
        /// </summary>
        /// <param name="transferData"></param>
        private void PopulateVendors()
        {
            DropDownListVendor.Items.Clear();

            DTO.ProjectPhases phasesData = new DTO.ProjectPhases();
            //phasesData.ProjectPhaseID = DropDownListProjectPhases.SelectedValue;
            //phasesData.ProductVersionID = DropDownListProductVersion.SelectedValue;
            //phasesData.ProductID = DropDownListProducts.SelectedValue;

            DataTable dtVendors = BO.BusinessObjects.GetProjectPhaseVendors(phasesData).Tables[0];

            ViewState[WebConstants.TBL_VENDORS] = dtVendors;

            if (dtVendors.Rows.Count > 0)
            {
                DataRow drNew = dtVendors.NewRow();
                drNew[WebConstants.COL_VENDOR_ID] = CONST_ZERO;
                drNew[WebConstants.COL_VENDOR] = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.DEF_VAL_TEAMS_ALL); ;

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
            TextBoxFeaturesTested.Text = "";
            TextBoxNotes.Text = "";
            TextBoxResourcesCount.Text = "";
            TextBoxResourcesName.Text = "";

            if (DropDownListProjectPhases.Items.Count == 0)
            {
                TabContainerWSR.Enabled = false;
                ButtonSubmitWSR.Enabled = false;
                return;
            }
            else
            {
                TabContainerWSR.Enabled = true;
                ButtonSubmitWSR.Enabled = true;
            }

            DTO.WSRData dtoWSRData = new DTO.WSRData();
            dtoWSRData.DataHeader = dataHeader;
            dtoWSRData.ProjectPhaseID = DropDownListProjectPhases.SelectedValue;
            dtoWSRData.VendorID = DropDownListVendor.SelectedValue;
            dtoWSRData.WeekID = weekID;

            DataTable dtWSRParameters = BO.BusinessObjects.GetProductWSRParameters(dtoWSRData).Tables[0];
            dtoWSRData = BO.BusinessObjects.GetCombinedWSRData(dtoWSRData);

            wsrDataID = dtoWSRData.WsrDataID;

            DataSet dsWSRDetails = null;
            dsWSRDetails = BO.BusinessObjects.GetWSRDetails(dtoWSRData);

            #region Resources Info

            TextBoxResourcesCount.Text = dtoWSRData.ResourceCount;
            TextBoxResourcesName.Text = dtoWSRData.ResourceNames;
            TextBoxFeaturesTested.Text = dtoWSRData.FeaturesTested;
            TextBoxNotes.Text = dtoWSRData.Notes;

            #endregion

            #region Issues and Accomplishments

            TextBoxRedIssues.Text = dtoWSRData.RedIssues;
            TextBoxYellowIssues.Text = dtoWSRData.YellowIssues;
            TextBoxGreenAccomplishments.Text = dtoWSRData.GreenAccom;

            #endregion

            #region Metrics

            PopulateGridViewEfforts(dtWSRParameters, dsWSRDetails);

            #endregion

            #region Next Week Priorities

            TextBoxNewDeliverables.Text = dtoWSRData.NewDeliverables;

            if (dtoWSRData.PrevWeekDeliverables == null)
            {
                DataSet prevWeekDeliverables = BO.BusinessObjects.GetWSRDetails(WebConstants.TBL_WSR_OUTSTANDING_DELIVERABLES, dtoWSRData);
                prevWeekDeliverables.Tables[0].Rows.InsertAt(prevWeekDeliverables.Tables[0].NewRow(), prevWeekDeliverables.Tables[0].Rows.Count);
                dtoWSRData.PrevWeekDeliverables = prevWeekDeliverables;
            }

            //Prev Week Deliverables
            GridViewOutstandingDeliverables.DataSource = dtoWSRData.PrevWeekDeliverables;
            GridViewOutstandingDeliverables.DataBind();

            #endregion

            ViewState[WebConstants.DTO_WSR_DATA] = dtoWSRData;
        }

        /// <summary>
        /// PopulateGridViewEfforts
        /// </summary>
        /// <param name="dtWSRParameters"></param>
        /// <param name="dsWSRDetails"></param>
        private void PopulateGridViewEfforts(DataTable dtWSRParameters, DataSet dsWSRDetails)
        {
            //string date = DateCalendarWeekStartDate.Date;
            DataTable dtGridHeader = new DataTable();

            DataColumn headerCol = new DataColumn(WebConstants.COL_TEMP_GRID_HEADER, Type.GetType(WebConstants.STR_SYSTEM_STRING));
            dtGridHeader.Columns.Add(headerCol);

            dtGridHeader.Rows.Add(WebConstants.COL_WSR_SECTION);
            dtGridHeader.Rows.Add(WebConstants.COL_WSR_PARAMETER_ID);
            dtGridHeader.Rows.Add(WebConstants.COL_WSR_PARAMETER);
            dtGridHeader.Rows.Add(WebConstants.COL_WSR_DETAIL_ID);
            dtGridHeader.Rows.Add(WebConstants.COL_WSR_REVISED_QUANTITY);
            dtGridHeader.Rows.Add(WebConstants.COL_WSR_ACTUAL_EFFORTS);
            dtGridHeader.Rows.Add(WebConstants.COL_WSR_REVISED_EFFORTS);
            dtGridHeader.Rows.Add(WebConstants.COL_WSR_REMARKS);

            ViewState[WebConstants.TBL_TEMP_GRID_HEADER] = dtGridHeader;

            DataTable dtCompiledEfforts = new DataTable();

            for (int counter = 0; counter < dtGridHeader.Rows.Count; counter++)
            {
                DataColumn col = new DataColumn(dtGridHeader.Rows[counter][0].ToString(), Type.GetType(WebConstants.STR_SYSTEM_STRING));
                dtCompiledEfforts.Columns.Add(col);
            }
            ViewState[WebConstants.TBL_TEMP_COMPILED_EFFORTS] = dtCompiledEfforts;

            foreach (DataRow drParam in dtWSRParameters.Rows)
            {
                DataRow drData = dtCompiledEfforts.NewRow();
                drData[WebConstants.COL_WSR_SECTION] = drParam[WebConstants.COL_WSR_SECTION];
                drData[WebConstants.COL_WSR_PARAMETER_ID] = drParam[WebConstants.COL_WSR_PARAMETER_ID];
                drData[WebConstants.COL_WSR_PARAMETER] = drParam[WebConstants.COL_WSR_PARAMETER];
                drData[WebConstants.COL_WSR_REVISED_QUANTITY] = STR_ZERO;
                drData[WebConstants.COL_WSR_ACTUAL_EFFORTS] = STR_ZERO;
                drData[WebConstants.COL_WSR_REVISED_EFFORTS] = STR_ZERO;

                DataRow[] drActualsCol = dsWSRDetails.Tables[0].Select(WebConstants.COL_WSR_PARAMETER_ID + " = " + drParam[WebConstants.COL_WSR_PARAMETER_ID].ToString());

                if (drActualsCol.Length > CONST_ZERO)
                {
                    drData[WebConstants.COL_WSR_ACTUAL_EFFORTS] = drActualsCol[0][WebConstants.COL_WSR_EFFORTS].ToString();
                    drData[WebConstants.COL_WSR_REVISED_EFFORTS] = drActualsCol[0][WebConstants.COL_WSR_EFFORTS].ToString();
                }

                DataRow[] drRevisedCol = dsWSRDetails.Tables[1].Select(WebConstants.COL_WSR_PARAMETER_ID + " = " + drParam[WebConstants.COL_WSR_PARAMETER_ID].ToString());

                if (drRevisedCol.Length > CONST_ZERO)
                {
                    drData[WebConstants.COL_WSR_DETAIL_ID] = drRevisedCol[0][WebConstants.COL_WSR_DETAIL_ID].ToString();
                    drData[WebConstants.COL_WSR_REVISED_QUANTITY] = drRevisedCol[0][WebConstants.COL_WSR_QUANTITY].ToString();
                    drData[WebConstants.COL_WSR_REVISED_EFFORTS] = drRevisedCol[0][WebConstants.COL_WSR_EFFORTS].ToString();
                    drData[WebConstants.COL_WSR_REMARKS] = drRevisedCol[0][WebConstants.COL_WSR_REMARKS].ToString();
                }

                dtCompiledEfforts.Rows.Add(drData);
            }

            #region Add Total Field

            DataRow drTotal = dtCompiledEfforts.NewRow();
            drTotal[WebConstants.COL_WSR_SECTION] = WebConstants.COL_TEMP_TOTAL;
            drTotal[WebConstants.COL_WSR_REVISED_QUANTITY] = STR_NA;

            double totalActualEfforts = 0.00;
            double totalRevisedEfforts = 0.00;
            foreach (DataRow drEfforts in dtCompiledEfforts.Rows)
            {
                totalActualEfforts += Convert.ToDouble(drEfforts[WebConstants.COL_WSR_ACTUAL_EFFORTS]);
                totalRevisedEfforts += Convert.ToDouble(drEfforts[WebConstants.COL_WSR_REVISED_EFFORTS]);
            }

            drTotal[WebConstants.COL_WSR_ACTUAL_EFFORTS] = string.Format("{0:f2}", totalActualEfforts);
            drTotal[WebConstants.COL_WSR_REVISED_EFFORTS] = string.Format("{0:f2}", totalRevisedEfforts);

            dtCompiledEfforts.Rows.Add(drTotal);

            #endregion

            GridViewEffortsTrack.DataSource = dtCompiledEfforts;
            GridViewEffortsTrack.DataBind();
        }

        /// <summary>
        /// GetModifiedWSRData
        /// </summary>
        /// <param name="dtoWSRData"></param>
        /// <returns></returns>
        private DTO.WSRData GetModifiedWSRData()
        {
            DTO.WSRData dtoWSRData = (DTO.WSRData)ViewState[WebConstants.DTO_WSR_DATA];

            //Issues and Accomplishments
            dtoWSRData.RedIssues = TextBoxRedIssues.Text;
            dtoWSRData.YellowIssues = TextBoxYellowIssues.Text;
            dtoWSRData.GreenAccom = TextBoxGreenAccomplishments.Text;
            dtoWSRData.NewDeliverables = TextBoxNewDeliverables.Text;

            dtoWSRData.FeaturesTested = TextBoxFeaturesTested.Text;
            dtoWSRData.Notes = TextBoxNotes.Text;
            dtoWSRData.ResourceCount = TextBoxResourcesCount.Text;
            dtoWSRData.ResourceNames = TextBoxResourcesName.Text;

            //Efforts
            dtoWSRData.VendorEffortsCollection = new ArrayList();

            for (int rowCount = 0; rowCount < GridViewEffortsTrack.Rows.Count - 1; rowCount++)
            {
                GridViewRow row = GridViewEffortsTrack.Rows[rowCount];

                Label LabelWSRParameterID = (Label)row.FindControl(WebConstants.CONTROL_LABEL_WSR_PARAMETER_ID);
                Label LabelWSRDetailID = (Label)row.FindControl(WebConstants.CONTROL_LABEL_WSR_DETAIL_ID);
                TextBox TextBoxRevisedQuantity = (TextBox)row.FindControl(WebConstants.CONTROL_TEXTBOX_REVISED_QUANTITY);
                TextBox TextBoxRevisedEfforts = (TextBox)row.FindControl(WebConstants.CONTROL_TEXTBOX_REVISED_EFFORTS);
                TextBox TextBoxRemarks = (TextBox)row.FindControl(WebConstants.CONTROL_TEXTBOX_REMARKS);

                DTO.Efforts effort = new DTO.Efforts();
                effort.PrimaryKeyID = LabelWSRDetailID.Text;
                effort.WSRParameterID = LabelWSRParameterID.Text;
                effort.Effort = TextBoxRevisedEfforts.Text == "" ? STR_ZERO : TextBoxRevisedEfforts.Text;
                effort.Quantity = TextBoxRevisedQuantity.Text == "" ? STR_ZERO : TextBoxRevisedQuantity.Text;
                effort.Remarks = TextBoxRemarks.Text;

                dtoWSRData.VendorEffortsCollection.Add(effort);
            }

            //dtoWSRData.PrevWeekDeliverables = (DataSet)GridViewOutstandingDeliverables.DataSource;

            return dtoWSRData;
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
            vendorID = Session[WebConstants.SESSION_VENDOR_ID].ToString();

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
            if (!isProductVersionActive)
                isReadOnly = true;

            if (isReadOnly)
            {
                ButtonSubmitWSR.Enabled = false;
                ButtonImport.Enabled = false;
                TextBoxFeaturesTested.Enabled = false;
                TextBoxNotes.Enabled = false;
                TextBoxResourcesCount.Enabled = false;
                TextBoxResourcesName.Enabled = false;
            }

            AddFooterPadding(1000);
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
                LabelVendor.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelVendor.Text);
                LabelWeek.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelWeek.Text);

                ButtonSubmitWSR.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, ButtonSubmitWSR.Text);
                ButtonImport.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, ButtonImport.Text);

                TabPanelIssuesandAccomplishments.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, TabPanelIssuesandAccomplishments.HeaderText);
                TabPanelMetrics.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, TabPanelMetrics.HeaderText);

                foreach (DataControlField field in GridViewOutstandingDeliverables.Columns)
                    if (field.HeaderText != "")
                        field.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, field.HeaderText);
            }

            LabelIssuesandAccomplishments.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelIssuesandAccomplishments.Text);
            LabelRedIssues.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelRedIssues.Text);
            LabelYellowIssues.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelYellowIssues.Text);
            LabelGreenAccomplishments.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelGreenAccomplishments.Text);
            LabelNextWeekPriority.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelNextWeekPriority.Text);
            LabelBlueDeliverables.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelBlueDeliverables.Text);
            LabelBlackDeliverables.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelBlackDeliverables.Text);
            LabelFeaturesTested.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelFeaturesTested.Text);
            LabelNotes.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelNotes.Text);
            LabelResources.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelResources.Text);
            LabelResourceCount.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelResourceCount.Text);
            LabelResourceName.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelResourceName.Text);

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
}