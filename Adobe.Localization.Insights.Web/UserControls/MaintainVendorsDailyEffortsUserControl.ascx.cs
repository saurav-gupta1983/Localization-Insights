using System;
using System.Web;
using System.Data;
using System.Text;
using System.Collections;
using System.Configuration;
using System.ComponentModel;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Drawing;
using Adobe.Localization.Insights.Common;
using COM = Adobe.Localization.Insights.Common.Common;
using BO = Adobe.Localization.Insights.BusinessLayer;
using DTO = Adobe.Localization.Insights.Common.DataTransferObjects;

namespace Adobe.Localization.Insights.Web.UserControls
{
    /// <summary>
    /// MaintainVendorsDailyEffortsUserControl
    /// </summary>
    public partial class MaintainVendorsDailyEffortsUserControl : System.Web.UI.UserControl
    {
        #region Variables

        private DataTable dtScreenLabels;
        private DTO.Header dataHeader = new DTO.Header();

        private string productID = "";

        private bool isReadOnly = false;
        private bool isReport = false;
        private bool isContractor = false;
        private bool isProductVersionActive = false;

        private const string STR_NON_ACTIVE = "Not Active";
        private const string STR_SPACE = "&nbsp;";
        private const string STR_ZERO = "0";
        private const string STR_ONE = "1";
        private const string STR_TOTAL = "Total";
        private const string STR_NO_VENDOR_INFO = "No Vendor";
        private const string STR_NO_USER_INFO = "No User";
        private const string STR_VAL_ISACTIVE = "1";

        private const int CONST_PRE_COLUMNS = 3;
        private const int CONST_ZERO = 0;
        private const int VAL_DAYS_IN_WEEK = 7;
        private const int NUM_EACH_GRID_ROW_DISPLAY_HEIGHT = 20;

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
            if (!IsPostBack)
            {
                PopulateWeekStartDate();
                PopulateProductVersion();
            }

            isProductVersionActive = (bool)ViewState[WebConstants.STR_IS_PRODUCT_VERSION_ACTIVE];

            SetScreenAccess();
        }

        /// <summary>
        /// Page_Init
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Init(object sender, EventArgs e)
        {
            DateCalendarWeekStartDate.OnCalendarSelectionChanged += new EventHandler(CalendarControl_SelectionChanged);
        }

        #endregion

        #region Grid Events

        /// <summary>
        /// GridViewEffortsTrack_RowDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewEffortsTrack_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridViewRow dr = e.Row;

                Label LabelWSRSection = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_WSR_SECTION);

                if (LabelWSRSection.Text == STR_TOTAL || isReadOnly)
                {
                    Label LabelRemarks = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_REMARKS);
                    LabelRemarks.Visible = true;
                    TextBox TextBoxRemarks = (TextBox)e.Row.FindControl(WebConstants.CONTROL_TEXTBOX_REMARKS);
                    TextBoxRemarks.Visible = false;

                    for (int counter = 1; counter <= VAL_DAYS_IN_WEEK; counter++)
                    {
                        Label LabelEffortsDay = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_EFFORTS_DAY + counter.ToString());
                        LabelEffortsDay.Visible = true;
                        TextBox TextBoxEffortsDay = (TextBox)e.Row.FindControl(WebConstants.CONTROL_TEXTBOX_EFFORTS_DAY + counter.ToString());
                        TextBoxEffortsDay.Visible = false;
                    }
                }
            }
        }

        #endregion

        #region Button Events

        /// <summary>
        /// ButtonSaveEfforts_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSaveEfforts_Click(object sender, EventArgs e)
        {
            DTO.WSRData data = new DTO.WSRData();
            data.DataHeader = dataHeader;
            data.ProjectPhaseID = DropDownListProjectPhases.SelectedValue;
            data.UserID = DropDownListUser.SelectedValue;
            data.VendorEffortsCollection = new ArrayList();

            DataTable dtColsEfforts = (DataTable)ViewState[WebConstants.TBL_TEMP_COMPILED_EFFORTS];
            DataTable dtGridHeader = (DataTable)ViewState[WebConstants.TBL_TEMP_GRID_HEADER];

            for (int rowCount = 0; rowCount < GridViewEffortsTrack.Rows.Count - 1; rowCount++)
            {
                GridViewRow row = GridViewEffortsTrack.Rows[rowCount];
                TextBox TextBoxRemarks = (TextBox)row.FindControl(WebConstants.CONTROL_TEXTBOX_REMARKS);

                for (int colCount = CONST_PRE_COLUMNS; colCount < dtColsEfforts.Columns.Count - 1; colCount += 2)
                {
                    string dayCount = ((colCount / 2)).ToString();

                    Label LabelWSRParameterID = (Label)row.FindControl(WebConstants.CONTROL_LABEL_WSR_PARAMETER_ID);
                    Label LabelEffortsTrackIDDay = (Label)row.FindControl(WebConstants.CONTROL_LABEL_EFFORTS_TRACK_ID_DAY + dayCount);
                    TextBox TextBoxEffortsDay = (TextBox)row.FindControl(WebConstants.CONTROL_TEXTBOX_EFFORTS_DAY + dayCount);

                    string date = dtGridHeader.Rows[(colCount / 2) - 1][1].ToString();

                    DTO.Efforts effort = new DTO.Efforts();
                    effort.PrimaryKeyID = LabelEffortsTrackIDDay.Text;
                    effort.WSRParameterID = LabelWSRParameterID.Text;
                    effort.Effort = TextBoxEffortsDay.Text == "" ? STR_ZERO : TextBoxEffortsDay.Text;
                    effort.Date = date;

                    if (dayCount == STR_ONE)
                        effort.Remarks = TextBoxRemarks.Text;

                    data.VendorEffortsCollection.Add(effort);
                }
            }

            if (BO.BusinessObjects.AddUpdateVendorEfforts(data))
            {
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_SUCCESS_SAVE_MESSAGE);
                PopulateData();
            }
            else
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_SAVE_MESSAGE);
        }

        #endregion

        #region Calendar Control Events

        /// <summary>
        /// CalendarControl_SelectionChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalendarControl_SelectionChanged(object sender, EventArgs e)
        {
            DateCalendarWeekStartDate.Date = COM.GetFirstDayOfWeek(DateCalendarWeekStartDate.GetDate()).ToShortDateString();
            PopulateData();
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
            //PopulateProjectPhases();
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
            PopulateUsers();
        }

        /// <summary>
        /// DropDownListUser_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListUser_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateData();
        }

        #endregion

        #region Private Functions

        ///// <summary>
        ///// PopulateProductVersion
        ///// </summary>
        //private void PopulateProductVersion()
        //{
        //    DropDownListProductVersion.Items.Clear();
        //    DropDownListProjectPhases.Items.Clear();
        //    DropDownListVendor.Items.Clear();
        //    DropDownListUser.Items.Clear();

        //    DTO.Product productData = new DTO.Product();
        //    productData.ProductID = productID;
        //    productData.IsActive = STR_VAL_ISACTIVE;

        //    DataTable dtProductVersion = BO.BusinessObjects.GetProductVersion(productData).Tables[0];

        //    if (dtProductVersion.Rows.Count > 0)
        //    {
        //        LabelProductValue.Text = dtProductVersion.Rows[0][WebConstants.COL_PRODUCT].ToString();

        //        DropDownListProductVersion.DataSource = dtProductVersion;
        //        DropDownListProductVersion.DataValueField = WebConstants.COL_PRODUCT_VERSION_ID;
        //        DropDownListProductVersion.DataTextField = WebConstants.COL_PRODUCT_VERSION;
        //        DropDownListProductVersion.DataBind();

        //        if (Session[WebConstants.SESSION_PRODUCT_VERSION_ID] != null)
        //            DropDownListProductVersion.SelectedIndex = DropDownListProductVersion.Items.IndexOf(DropDownListProductVersion.Items.FindByValue(Session[WebConstants.SESSION_PRODUCT_VERSION_ID].ToString()));

        //        PopulateProjectPhases();
        //    }
        //    else
        //    {
        //        LabelProductValue.Text = BO.BusinessObjects.GetProducts(productData).Tables[0].Rows[0][WebConstants.COL_PRODUCT].ToString();
        //        PopulateData();
        //    }
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
            DropDownListUser.Items.Clear();

            DTO.ProjectPhases projectPhaseData = new DTO.ProjectPhases();
            projectPhaseData.ProductVersionID = DropDownListProductVersion.SelectedValue;
            projectPhaseData.IsActive = true;

            DataTable dtProjectPhases = BO.BusinessObjects.GetProjectPhases(projectPhaseData).Tables[0];

            if (dtProjectPhases.Rows.Count > 0)
            {
                //DataRow drNew = dtProjectPhases.NewRow();
                //drNew[WebConstants.COL_PROJECT_PHASE_ID] = CONST_ZERO;
                //drNew[WebConstants.COL_PROJECT_PHASE] = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.DEF_VAL_PHASES_ALL);
                //dtProjectPhases.Rows.InsertAt(drNew, 0);

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
                PopulateData();
            }
        }

        /// <summary>
        /// PopulateVendor
        /// </summary>
        private void PopulateVendors()
        {
            DropDownListVendor.Items.Clear();
            DropDownListUser.Items.Clear();

            DTO.ProjectPhases phasesData = new DTO.ProjectPhases();
            phasesData.ProjectPhaseID = DropDownListProjectPhases.SelectedValue;
            phasesData.ProductVersionID = DropDownListProductVersion.SelectedValue;

            DataTable dtVendors = BO.BusinessObjects.GetProjectPhaseVendors(phasesData).Tables[0];

            if (dtVendors.Rows.Count > CONST_ZERO)
            {
                DropDownListVendor.DataSource = dtVendors;
                DropDownListVendor.DataValueField = WebConstants.COL_VENDOR_ID;
                DropDownListVendor.DataTextField = WebConstants.COL_VENDOR;
                DropDownListVendor.DataBind();

                if (isContractor)
                {
                    LabelSelectVendorValue.Visible = true;
                    DropDownListVendor.Visible = false;
                    string vendorID = Session[WebConstants.SESSION_VENDOR_ID].ToString();
                    if (dtVendors.Select(WebConstants.COL_VENDOR_ID + " = " + vendorID).Length > 0)
                    {
                        DropDownListVendor.SelectedIndex = DropDownListVendor.Items.IndexOf(DropDownListVendor.Items.FindByValue(vendorID));
                        LabelSelectVendorValue.Text = DropDownListVendor.SelectedItem.Text;
                    }
                    else
                    {
                        DropDownListVendor.Items.Clear();
                        LabelSelectVendorValue.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, STR_NO_VENDOR_INFO); ;
                    }
                }

                PopulateUsers();
            }
            else
            {
                PopulateData();
            }
        }

        /// <summary>
        /// PopulateUsers
        /// </summary>
        private void PopulateUsers()
        {
            DropDownListUser.Items.Clear();

            DTO.ProjectPhases phasesData = new DTO.ProjectPhases();
            phasesData.ProjectPhaseID = DropDownListProjectPhases.SelectedValue;
            phasesData.ProductVersionID = DropDownListProductVersion.SelectedValue;
            phasesData.VendorID = DropDownListVendor.SelectedValue;

            DataTable dtUsers = BO.BusinessObjects.GetProjectPhaseVendorUsers(phasesData).Tables[0];

            if (dtUsers.Rows.Count > 0)
            {
                DropDownListUser.DataSource = dtUsers;
                DropDownListUser.DataValueField = WebConstants.COL_USER_ID;
                DropDownListUser.DataTextField = WebConstants.COL_USER_NAME;
                DropDownListUser.DataBind();

                if (!isReport)
                {
                    LabelSelectUserValue.Visible = true;
                    DropDownListUser.Visible = false;
                    string userID = Session[WebConstants.SESSION_USER_ID].ToString();
                    if (dtUsers.Select(WebConstants.COL_USER_ID + " = " + userID).Length > 0)
                    {
                        DropDownListUser.SelectedIndex = DropDownListUser.Items.IndexOf(DropDownListUser.Items.FindByValue(userID));
                        LabelSelectUserValue.Text = DropDownListUser.SelectedItem.Text;
                    }
                    else
                    {
                        DropDownListUser.Items.Clear();
                        LabelSelectUserValue.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, STR_NO_USER_INFO);
                    }
                }

                DropDownListUser.SelectedIndex = DropDownListUser.Items.IndexOf(DropDownListUser.Items.FindByValue(Session[WebConstants.SESSION_USER_ID].ToString()));
            }

            PopulateData();
        }

        /// <summary>
        /// PopulateWeekStartDate
        /// </summary>
        private void PopulateWeekStartDate()
        {
            DateCalendarWeekStartDate.Date = COM.GetFirstDayOfWeek().ToShortDateString();
            SetDateCalendar(DateCalendarWeekStartDate);
        }

        /// <summary>
        /// PopulateData
        /// </summary>
        private void PopulateData()
        {
            GridViewEffortsTrack.Enabled = true;
            ButtonSaveEfforts.Enabled = true;

            DTO.WSRData wsrData = new DTO.WSRData();

            if (DropDownListUser.Items.Count == 0)
            {
                GridViewEffortsTrack.Enabled = false;
                ButtonSaveEfforts.Enabled = false;
                wsrData.ProductID = productID;
                wsrData.UserID = "-1";
            }
            else
            {
                wsrData.UserID = DropDownListUser.SelectedValue;
                wsrData.VendorID = DropDownListVendor.SelectedValue;
                wsrData.ProjectPhaseID = DropDownListProjectPhases.SelectedValue;
            }
            wsrData.WeekStartDate = DateCalendarWeekStartDate.GetDate();

            DataTable dtWSRParameters = BO.BusinessObjects.GetProductWSRParameters(wsrData).Tables[0];
            DataTable dtVendorEfforts = BO.BusinessObjects.GetVendorEfforts(wsrData).Tables[0];

            PopulateEffortsGridView(dtWSRParameters, dtVendorEfforts);
        }

        /// <summary>
        /// PopulateEfforts
        /// </summary>
        /// <param name="dtWSRParameters"></param>
        /// <param name="dtEfforts"></param>
        private void PopulateEffortsGridView(DataTable dtWSRParameters, DataTable dtVendorEfforts)
        {
            string date = DateCalendarWeekStartDate.Date;
            DataTable dtGridHeader = new DataTable();

            DataColumn headerCol = new DataColumn(WebConstants.COL_TEMP_GRID_HEADER, Type.GetType(WebConstants.STR_SYSTEM_STRING));
            dtGridHeader.Columns.Add(headerCol);
            headerCol = new DataColumn(WebConstants.COL_TEMP_DATE, Type.GetType(WebConstants.STR_SYSTEM_STRING));
            dtGridHeader.Columns.Add(headerCol);
            headerCol = new DataColumn(WebConstants.COL_TEMP_TOTAL, Type.GetType(WebConstants.STR_SYSTEM_STRING));
            dtGridHeader.Columns.Add(headerCol);

            dtGridHeader.Rows.Add(WebConstants.COL_WSR_SECTION, "");
            dtGridHeader.Rows.Add(WebConstants.COL_WSR_PARAMETER_ID, "");
            dtGridHeader.Rows.Add(WebConstants.COL_WSR_PARAMETER, "");

            for (int counter = 0; counter < VAL_DAYS_IN_WEEK; counter++)
            {
                DataRow drGridHeader = dtGridHeader.NewRow();
                drGridHeader = GridHeaderDateFormat(drGridHeader, date, counter);
                dtGridHeader.Rows.Add(drGridHeader);
            }
            dtGridHeader.Rows.Add(WebConstants.COL_WSR_REMARKS, "");
            ViewState[WebConstants.TBL_TEMP_GRID_HEADER] = dtGridHeader;

            DataTable dtCompiledEfforts = new DataTable();

            for (int counter = 0; counter < dtGridHeader.Rows.Count; counter++)
            {
                string columnName = dtGridHeader.Rows[counter][0].ToString().Replace(" ", "");

                DataColumn col;
                if (counter < CONST_PRE_COLUMNS || counter == dtGridHeader.Rows.Count - 1)
                {
                    col = new DataColumn(columnName, Type.GetType(WebConstants.STR_SYSTEM_STRING));
                    dtCompiledEfforts.Columns.Add(col);
                }
                else
                {
                    GridViewEffortsTrack.Columns[counter].HeaderText = columnName;

                    int strCnt = counter - CONST_PRE_COLUMNS + 1;
                    col = new DataColumn(WebConstants.COL_TEMP_EFFORTS_TRACK_ID_DAY + (strCnt).ToString(), Type.GetType(WebConstants.STR_SYSTEM_STRING));
                    dtCompiledEfforts.Columns.Add(col);
                    col = new DataColumn(WebConstants.COL_TEMP_EFFORTS_DAY + (strCnt).ToString(), Type.GetType(WebConstants.STR_SYSTEM_STRING));
                    dtCompiledEfforts.Columns.Add(col);
                }
            }
            ViewState[WebConstants.TBL_TEMP_COMPILED_EFFORTS] = dtCompiledEfforts;

            for (int counter = 0; counter < CONST_PRE_COLUMNS; counter++)
                dtGridHeader.Rows.RemoveAt(0);
            dtGridHeader.Rows.RemoveAt(dtGridHeader.Rows.Count - 1);

            foreach (DataRow drParam in dtWSRParameters.Rows)
            {
                DataRow drData = dtCompiledEfforts.NewRow();
                drData[WebConstants.COL_WSR_SECTION] = drParam[WebConstants.COL_WSR_SECTION];
                drData[WebConstants.COL_WSR_PARAMETER_ID] = drParam[WebConstants.COL_WSR_PARAMETER_ID];
                drData[WebConstants.COL_WSR_PARAMETER] = drParam[WebConstants.COL_WSR_PARAMETER];

                DataRow[] drCol = dtVendorEfforts.Select(WebConstants.COL_WSR_PARAMETER_ID + " = " + drParam[WebConstants.COL_WSR_PARAMETER_ID].ToString());

                for (int rowCnt = 0; rowCnt < VAL_DAYS_IN_WEEK; rowCnt++)
                {
                    string row = (rowCnt + 1).ToString();
                    if (drCol.Length > CONST_ZERO)
                    {
                        drData[WebConstants.COL_TEMP_EFFORTS_TRACK_ID_DAY + row] = drCol[rowCnt][WebConstants.COL_WSR_EFFORTS_TRACK_ID];
                        drData[WebConstants.COL_TEMP_EFFORTS_DAY + row] = drCol[rowCnt][WebConstants.COL_WSR_EFFORTS];

                        if (rowCnt == CONST_ZERO)
                            drData[WebConstants.COL_WSR_REMARKS] = drCol[rowCnt][WebConstants.COL_WSR_REMARKS];

                        dtGridHeader.Rows[rowCnt][WebConstants.COL_TEMP_TOTAL] = (Convert.ToDouble(dtGridHeader.Rows[rowCnt][WebConstants.COL_TEMP_TOTAL]) + Convert.ToDouble(drCol[rowCnt][WebConstants.COL_WSR_EFFORTS])).ToString();
                    }
                    else
                    {
                        drData[WebConstants.COL_TEMP_EFFORTS_TRACK_ID_DAY + row] = CONST_ZERO;
                        drData[WebConstants.COL_TEMP_EFFORTS_DAY + row] = CONST_ZERO;
                    }
                }
                dtCompiledEfforts.Rows.Add(drData);
            }

            DataRow drTotal = dtCompiledEfforts.NewRow();
            drTotal[WebConstants.COL_WSR_SECTION] = COM.GetScreenLocalizedLabel(dtScreenLabels, STR_TOTAL);

            for (int rowCnt = 0; rowCnt < VAL_DAYS_IN_WEEK; rowCnt++)
            {
                string row = (rowCnt + 1).ToString();
                drTotal[WebConstants.COL_TEMP_EFFORTS_TRACK_ID_DAY + row] = dtGridHeader.Rows[rowCnt][WebConstants.COL_TEMP_TOTAL];
                drTotal[WebConstants.COL_TEMP_EFFORTS_DAY + row] = dtGridHeader.Rows[rowCnt][WebConstants.COL_TEMP_TOTAL];
            }
            dtCompiledEfforts.Rows.Add(drTotal);

            GridViewEffortsTrack.DataSource = dtCompiledEfforts;
            GridViewEffortsTrack.DataBind();
        }

        /// <summary>
        /// GridHeaderDateFormat
        /// </summary>
        /// <param name="date"></param>
        /// <param name="add"></param>
        /// <returns></returns>
        private DataRow GridHeaderDateFormat(DataRow drGridHeader, string date, int add)
        {
            StringBuilder formattedDate = new StringBuilder();
            DateTime newDate = DateTime.Parse(date);
            newDate = newDate.AddDays(add);

            formattedDate.Append(newDate.Month + "/");
            formattedDate.Append(newDate.Day + "<BR>");
            formattedDate.Append(newDate.DayOfWeek.ToString().Substring(0, 3));

            drGridHeader[0] = formattedDate.ToString();
            drGridHeader[1] = newDate.ToShortDateString();
            drGridHeader[2] = STR_ZERO;

            return drGridHeader;
        }

        /// <summary>
        /// SetDateCalendar
        /// </summary>
        public void SetDateCalendar(Calendar dateCalendar)
        {
            HtmlInputButton dateButton;
            HtmlGenericControl dateDiv;

            dateButton = (HtmlInputButton)dateCalendar.FindControl(WebConstants.CONTROL_DATECALENDAR_BUTTON);
            dateDiv = (HtmlGenericControl)dateCalendar.FindControl(WebConstants.CONTROL_DATECALENDAR_DIVCALENDAR);
            dateButton.Attributes.Add(WebConstants.CONTROL_DATECALENDAR_ONCLICK, "javascript:return OnClick('" + dateDiv.ClientID + "')");
        }

        #endregion

        #region Screen Access and Labels

        /// <summary>
        /// GetScreenAccess
        /// </summary>
        private void GetScreenAccess()
        {
            dataHeader.LoginID = Session[WebConstants.SESSION_LOGIN_ID].ToString();

            if (Session[WebConstants.SESSION_PRODUCT_ID] != null)
                productID = Session[WebConstants.SESSION_PRODUCT_ID].ToString();

            isContractor = (bool)Session[WebConstants.SESSION_VENDOR_IS_CONTRACTOR];

            if (Session[WebConstants.SESSION_CACHE_SCREEN_ACCESS_DATA_TABLE] != null)
            {
                DataTable dtScreenAccess = (DataTable)Session[WebConstants.SESSION_CACHE_SCREEN_ACCESS_DATA_TABLE];

                DataRow drScreenAccess = dtScreenAccess.Select(WebConstants.COL_SCREEN_IDENTIFIER + " = '" + Session[WebConstants.SESSION_IDENTIFIER] + "'")[0];

                if (drScreenAccess[WebConstants.COL_SCREEN_IS_READ].ToString() == WebConstants.DEF_VAL_ACCESS_CHECKED)
                    isReadOnly = true;
                if (drScreenAccess[WebConstants.COL_SCREEN_IS_READ_WRITE].ToString() == WebConstants.DEF_VAL_ACCESS_CHECKED)
                    isReadOnly = false;
                if (isReadOnly || drScreenAccess[WebConstants.COL_SCREEN_IS_REPORT].ToString() == WebConstants.DEF_VAL_ACCESS_CHECKED)
                    isReport = true;
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
                ButtonSaveEfforts.Enabled = false;
                GridViewEffortsTrack.Enabled = false;
            }

            AddFooterPadding(GridViewEffortsTrack.Rows.Count);
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
                LabelProjectPhases.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelProjectPhases.Text);
                LabelWeekStartDate.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelWeekStartDate.Text);
                LabelSelectVendor.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelSelectVendor.Text);
                LabelSelectUser.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelSelectUser.Text);

                ButtonSaveEfforts.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, ButtonSaveEfforts.Text);

                foreach (DataControlField field in GridViewEffortsTrack.Columns)
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