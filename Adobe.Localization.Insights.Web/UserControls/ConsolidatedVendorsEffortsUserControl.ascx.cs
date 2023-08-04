using System;
using System.Web;
using System.Data;
using System.Text;
using System.Collections;
using System.Globalization;
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
    /// ConsolidatedVendorsEffortsUserControl
    /// </summary>
    public partial class ConsolidatedVendorsEffortsUserControl : System.Web.UI.UserControl
    {
        #region Variables

        private DataTable dtScreenLabels;
        private DTO.Header dataHeader = new DTO.Header();

        private string productID = "";

        private bool isReport = false;
        private bool isContractor = false;

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

            if (!IsPostBack)
            {
                PopulateWeekStartDate();
                PopulateVendor();
            }

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

        #region Calendar Control Events

        /// <summary>
        /// CalendarControl_SelectionChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalendarControl_SelectionChanged(object sender, EventArgs e)
        {
            DateCalendarWeekStartDate.Date = GetFirstDayOfWeek(DateCalendarWeekStartDate.GetDate()).ToShortDateString();
            PopulateData();
        }

        #endregion

        #region DropDownList events

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
            PopulateProducts();
        }

        /// <summary>
        /// DropDownListProducts_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListProducts_OnSelectedIndexChanged(object sender, EventArgs e)
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
            PopulateProjectPhases();
        }

        /// <summary>
        /// DropDownListProjectPhases_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListProjectPhases_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateData();
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// PopulateVendor
        /// </summary>
        private void PopulateVendor()
        {
            DropDownListVendor.Items.Clear();
            DropDownListUser.Items.Clear();
            DropDownListProducts.Items.Clear();
            DropDownListProductVersion.Items.Clear();
            DropDownListProjectPhases.Items.Clear();

            DTO.MasterData masterData = new DTO.MasterData();
            masterData.TableName = WebConstants.TBL_VENDORS;

            DataTable dtVendors = BO.BusinessObjects.GetDetailsforMasterData(masterData).Tables[0];

            dtVendors.Rows[0][WebConstants.COL_VENDOR] = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.DEF_VAL_TEAMS_ALL);

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
            }

            PopulateUsers();
        }

        /// <summary>
        /// PopulateUsers
        /// </summary>
        private void PopulateUsers()
        {
            DropDownListUser.Items.Clear();
            DropDownListProducts.Items.Clear();
            DropDownListProductVersion.Items.Clear();
            DropDownListProjectPhases.Items.Clear();

            DTO.MasterData masterData = new DTO.MasterData();
            masterData.MasterDataID = DropDownListVendor.SelectedValue;

            DataTable dtUsers = BO.BusinessObjects.GetUserVendorAssociations(masterData).Tables[0];

            dtUsers.Rows[0][WebConstants.COL_USER_NAME] = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.DEF_VAL_USERS_ALL);

            DropDownListUser.DataSource = dtUsers;
            DropDownListUser.DataValueField = WebConstants.COL_USER_ID;
            DropDownListUser.DataTextField = WebConstants.COL_USER_NAME;
            DropDownListUser.DataBind();

            PopulateProducts();
        }

        /// <summary>
        /// PopulateProducts
        /// </summary>
        private void PopulateProducts()
        {
            DropDownListProducts.Items.Clear();
            DropDownListProductVersion.Items.Clear();
            DropDownListProjectPhases.Items.Clear();

            DataTable dtProducts = GetProducts();

            DataRow drNew = dtProducts.NewRow();
            drNew[WebConstants.COL_PRODUCT_ID] = CONST_ZERO;
            drNew[WebConstants.COL_PRODUCT] = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.DEF_VAL_PRODUCTS_ALL);
            dtProducts.Rows.InsertAt(drNew, CONST_ZERO);

            DropDownListProducts.DataSource = dtProducts;
            DropDownListProducts.DataValueField = WebConstants.COL_PRODUCT_ID;
            DropDownListProducts.DataTextField = WebConstants.COL_PRODUCT;
            DropDownListProducts.DataBind();

            DropDownListProducts.SelectedIndex = DropDownListProducts.Items.IndexOf(DropDownListProducts.Items.FindByValue(productID));
            LabelProductValue.Text = DropDownListProducts.SelectedItem.Text;

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
                productData.IsActive = STR_VAL_ISACTIVE;

                DataTable dtProductVersion = BO.BusinessObjects.GetProductVersion(productData).Tables[0];

                if (dtProductVersion.Rows.Count > CONST_ZERO)
                {
                    DataRow drNew = dtProductVersion.NewRow();
                    drNew[WebConstants.COL_PRODUCT_VERSION_ID] = CONST_ZERO;
                    drNew[WebConstants.COL_PRODUCT_VERSION] = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.DEF_VAL_PRODUCT_VERSIONS_ALL);
                    dtProductVersion.Rows.InsertAt(drNew, CONST_ZERO);

                    DropDownListProductVersion.DataSource = dtProductVersion;
                    DropDownListProductVersion.DataValueField = WebConstants.COL_PRODUCT_VERSION_ID;
                    DropDownListProductVersion.DataTextField = WebConstants.COL_PRODUCT_VERSION;
                    DropDownListProductVersion.DataBind();

                    if (Session[WebConstants.SESSION_PRODUCT_VERSION_ID] != null)
                        DropDownListProductVersion.SelectedIndex = DropDownListProductVersion.Items.IndexOf(DropDownListProductVersion.Items.FindByValue(Session[WebConstants.SESSION_PRODUCT_VERSION_ID].ToString()));

                    LabelProductVersionValue.Text = DropDownListProductVersion.SelectedItem.Text;
                    PopulateProjectPhases();
                    return;
                }
            }

            PopulateData();
        }

        /// <summary>
        /// PopulateProjectPhases
        /// </summary>
        private void PopulateProjectPhases()
        {
            DropDownListProjectPhases.Items.Clear();

            if (DropDownListProductVersion.SelectedValue != STR_ZERO)
            {
                DTO.ProjectPhases projectPhaseData = new DTO.ProjectPhases();
                projectPhaseData.ProductVersionID = DropDownListProductVersion.SelectedValue;
                projectPhaseData.IsActive = true;

                DataTable dtProjectPhases = BO.BusinessObjects.GetProjectPhases(projectPhaseData).Tables[0];

                if (dtProjectPhases.Rows.Count > CONST_ZERO)
                {
                    DataRow drNew = dtProjectPhases.NewRow();
                    drNew[WebConstants.COL_PROJECT_PHASE_ID] = CONST_ZERO;
                    drNew[WebConstants.COL_PROJECT_PHASE] = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.DEF_VAL_PROJECT_PHASES_ALL); ;
                    dtProjectPhases.Rows.InsertAt(drNew, CONST_ZERO);

                    DropDownListProjectPhases.DataSource = dtProjectPhases;
                    DropDownListProjectPhases.DataValueField = WebConstants.COL_PROJECT_PHASE_ID;
                    DropDownListProjectPhases.DataTextField = WebConstants.COL_PROJECT_PHASE;
                    DropDownListProjectPhases.DataBind();

                    if (Session[WebConstants.SESSION_PROJECT_PHASE_ID] != null)
                        DropDownListProjectPhases.SelectedIndex = DropDownListProjectPhases.Items.IndexOf(DropDownListProjectPhases.Items.FindByValue(Session[WebConstants.SESSION_PROJECT_PHASE_ID].ToString()));
                }
            }

            PopulateData();
        }

        /// <summary>
        /// PopulateWeekStartDate
        /// </summary>
        private void PopulateWeekStartDate()
        {
            DateCalendarWeekStartDate.Date = GetFirstDayOfWeek(System.DateTime.Now).ToShortDateString();
            SetDateCalendar(DateCalendarWeekStartDate);
        }

        /// <summary>
        /// PopulateData
        /// </summary>
        private void PopulateData()
        {
            DTO.WSRData wsrData = new DTO.WSRData();
            wsrData.VendorID = DropDownListVendor.SelectedValue;
            wsrData.UserID = DropDownListUser.SelectedValue;
            wsrData.ProductID = DropDownListProducts.SelectedValue;
            wsrData.ProductVersionID = DropDownListProductVersion.SelectedValue;
            wsrData.ProjectPhaseID = DropDownListProjectPhases.SelectedValue;
            wsrData.WeekStartDate = DateCalendarWeekStartDate.GetDate();
            wsrData.Reporting = true;

            DataTable dtWSRParameters = BO.BusinessObjects.GetProductWSRParameters(wsrData).Tables[0];

            if (DropDownListProducts.SelectedValue == STR_ZERO)
            {
                StringBuilder productID = new StringBuilder("-1");
                foreach (ListItem item in DropDownListProducts.Items)
                    productID.Append("," + item.Value);
                wsrData.ProductID = productID.ToString();
            }

            DataTable dtVendorEfforts = BO.BusinessObjects.GetVendorEfforts(wsrData).Tables[0];

            PopulateEffortsGridView(dtWSRParameters, dtVendorEfforts);

            if (isReport)
                GridViewEffortsTrack.Visible = true;
            else
                GridViewEffortsTrack.Visible = false;
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
            //dtGridHeader.Rows.Add("Remarks", "");
            ViewState[WebConstants.TBL_TEMP_GRID_HEADER] = dtGridHeader;

            DataTable dtCompiledEfforts = new DataTable();

            for (int counter = 0; counter < dtGridHeader.Rows.Count; counter++)
            {
                string columnName = dtGridHeader.Rows[counter][0].ToString().Replace(" ", "");

                DataColumn col;
                //if (counter < precolumns || counter == dtGridHeader.Rows.Count - 1)
                if (counter < CONST_PRE_COLUMNS)
                {
                    col = new DataColumn(columnName, Type.GetType(WebConstants.STR_SYSTEM_STRING));
                    dtCompiledEfforts.Columns.Add(col);
                }
                else
                {
                    GridViewEffortsTrack.Columns[counter].HeaderText = columnName;

                    int strCnt = counter - CONST_PRE_COLUMNS + 1;
                    //col = new DataColumn("EffortsTrackIDDay" + (strCnt).ToString(), Type.GetType(WebConstants.STR_SYSTEM_STRING));
                    //dtCompiledEfforts.Columns.Add(col);
                    col = new DataColumn(WebConstants.COL_TEMP_EFFORTS_DAY + (strCnt).ToString(), Type.GetType(WebConstants.STR_SYSTEM_STRING));
                    dtCompiledEfforts.Columns.Add(col);
                }
            }
            ViewState[WebConstants.TBL_TEMP_COMPILED_EFFORTS] = dtCompiledEfforts;

            for (int counter = 0; counter < CONST_PRE_COLUMNS; counter++)
                dtGridHeader.Rows.RemoveAt(0);
            //dtGridHeader.Rows.RemoveAt(dtGridHeader.Rows.Count - 1);

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
                    if (drCol.Length > 0)
                    {
                        //drData["EffortsTrackIDDay" + row] = drCol[rowCnt]["EffortsTrackID"];
                        drData[WebConstants.COL_TEMP_EFFORTS_DAY + row] = drCol[rowCnt][WebConstants.COL_WSR_EFFORTS];

                        //if (rowCnt == 0)
                        //    drData["Remarks"] = drCol[rowCnt]["Remarks"];

                        dtGridHeader.Rows[rowCnt][WebConstants.COL_TEMP_TOTAL] = (Convert.ToDouble(dtGridHeader.Rows[rowCnt][WebConstants.COL_TEMP_TOTAL]) + Convert.ToDouble(drCol[rowCnt][WebConstants.COL_WSR_EFFORTS])).ToString();
                    }
                    else
                    {
                        //drData["EffortsTrackIDDay" + row] = 0;
                        drData[WebConstants.COL_TEMP_EFFORTS_DAY + row] = CONST_ZERO;
                    }
                }
                dtCompiledEfforts.Rows.Add(drData);
            }

            DataRow drTotal = dtCompiledEfforts.NewRow();
            drTotal[WebConstants.COL_WSR_SECTION] = COM.GetScreenLocalizedLabel(dtScreenLabels, STR_TOTAL);

            for (int rowCnt = 0; rowCnt < 7; rowCnt++)
            {
                string row = (rowCnt + 1).ToString();
                //drTotal["EffortsTrackIDDay" + row] = dtGridHeader.Rows[rowCnt]["Total"];
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
        /// Returns the first day of the week that the specified
        /// date is in using the current culture.
        /// </summary>
        private DateTime GetFirstDayOfWeek(string date)
        {
            return GetFirstDayOfWeek(DateTime.Parse(date));
        }

        /// <summary>
        /// Returns the first day of the week that the specified
        /// date is in using the current culture.
        /// </summary>
        private DateTime GetFirstDayOfWeek(DateTime dayInWeek)
        {
            CultureInfo defaultCultureInfo = CultureInfo.CurrentCulture;
            return GetFirstDayOfWeek(dayInWeek, defaultCultureInfo);
        }

        /// <summary>
        /// Returns the first day of the week that the specified date
        /// is in.
        /// </summary>
        private DateTime GetFirstDayOfWeek(DateTime dayInWeek, CultureInfo cultureInfo)
        {
            DayOfWeek firstDay = cultureInfo.DateTimeFormat.FirstDayOfWeek;
            DateTime firstDayInWeek = dayInWeek.Date;
            while (firstDayInWeek.DayOfWeek != firstDay)
                firstDayInWeek = firstDayInWeek.AddDays(-1);
            return firstDayInWeek;
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

                if (drScreenAccess[WebConstants.COL_SCREEN_IS_REPORT].ToString() == WebConstants.DEF_VAL_ACCESS_CHECKED)
                    isReport = true;
            }
        }

        /// <summary>
        /// SetScreenAccess
        /// </summary>
        private void SetScreenAccess()
        {
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
            int spaceCount = (rowsCount * NUM_EACH_GRID_ROW_DISPLAY_HEIGHT) - 2 * NUM_EACH_GRID_ROW_DISPLAY_HEIGHT;

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