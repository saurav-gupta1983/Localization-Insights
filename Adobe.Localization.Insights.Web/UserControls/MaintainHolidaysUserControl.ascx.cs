using System;
using System.Data;
using System.Collections;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Adobe.Localization.Insights.Common;
using COM = Adobe.Localization.Insights.Common.Common;
using BO = Adobe.Localization.Insights.BusinessLayer;
using DTO = Adobe.Localization.Insights.Common.DataTransferObjects;

namespace Adobe.Localization.Insights.Web.UserControls
{
    /// <summary>
    /// DefineHolidaysUserControl
    /// </summary>
    public partial class MaintainHolidaysUserControl : System.Web.UI.UserControl
    {
        #region Variables

        private DataTable dtVendors;
        private DataTable dtScreenLabels;
        private DTO.Header dataHeader = new DTO.Header();

        private int colID = 0;

        private bool isReadOnly = true;
        private bool isContractor = false;

        private const int COL_SAVE_UPDATE_NO = 6;
        private const int COL_CANCEL_DELETE_NO = 7;
        private const int NUM_EACH_GRID_ROW_DISPLAY_HEIGHT = 20;
        private const string STR_SPACE = "&nbsp;";
        private const string STR_ZERO = "0";

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
                ViewState[WebConstants.TBL_VENDORS] = PopulateVendors();
                DropDownListVendors.SelectedIndex = DropDownListVendors.Items.IndexOf(DropDownListVendors.Items.FindByValue(Session[WebConstants.SESSION_VENDOR_ID].ToString()));
                PopulateData();
            }
            dtVendors = (DataTable)ViewState[WebConstants.TBL_VENDORS];

            SetScreenAccess();
            AddFooterPadding(GridViewHolidayDetails.Rows.Count);
        }

        #endregion

        #region Grid Events

        /// <summary>
        /// GridViewHolidayDetails_RowDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewHolidayDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridViewRow gridViewRow = e.Row;

                SetGridLinkButtonsDisplayText(gridViewRow);

                Label LabelStartDate = (Label)gridViewRow.FindControl(WebConstants.CONTROL_LABEL_START_DATE);
                Calendar DateCalendarStartDate = (Calendar)gridViewRow.FindControl(WebConstants.CONTROL_DATECALENDAR_START_DATE);
                SetDateCalendar(DateCalendarStartDate);

                Label LabelEndDate = (Label)gridViewRow.FindControl(WebConstants.CONTROL_LABEL_END_DATE);
                Calendar DateCalendarEndDate = (Calendar)gridViewRow.FindControl(WebConstants.CONTROL_DATECALENDAR_END_DATE);
                SetDateCalendar(DateCalendarEndDate);

                if (gridViewRow.Cells[colID].Text.ToString() == STR_ZERO || gridViewRow.Cells[colID].Text.ToString() == STR_SPACE)
                {
                    e.Row.Cells[colID].Text = "";
                    setLinkButtons(e.Row, true);
                    string currentDate = System.DateTime.Now.ToShortDateString();

                    LabelStartDate.Text = currentDate;
                    LabelEndDate.Text = currentDate;
                    DateCalendarStartDate.Date = currentDate;
                    DateCalendarEndDate.Date = currentDate;
                }
                else
                {
                    LabelStartDate.Text = DateTime.Parse(DateCalendarStartDate.GetDate()).ToShortDateString();
                    LabelEndDate.Text = DateTime.Parse(DateCalendarEndDate.GetDate()).ToShortDateString();
                }
            }
        }

        /// <summary>
        /// GridViewHolidayDetails_RowUpdating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewHolidayDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
        }

        /// <summary>
        /// GridViewHolidayDetails_RowCancelingEdit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewHolidayDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
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
            GridViewRow detailsGridViewRow = GridViewHolidayDetails.Rows[index];

            dataHeader.LoginID = Session[WebConstants.SESSION_LOGIN_ID].ToString();

            DTO.Holidays holidayData = new DTO.Holidays();
            holidayData.DataHeader = dataHeader;

            Label LabelHolidayID = (Label)detailsGridViewRow.FindControl(WebConstants.CONTROL_LABEL_HOLIDAY_ID);
            TextBox TextBoxHolidayReason = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_HOLIDAY_REASON);
            Calendar DateCalendarStartDate = (Calendar)detailsGridViewRow.FindControl(WebConstants.CONTROL_DATECALENDAR_START_DATE);
            Calendar DateCalendarEndDate = (Calendar)detailsGridViewRow.FindControl(WebConstants.CONTROL_DATECALENDAR_END_DATE);
            DropDownList DropDownListSelectVendor = (DropDownList)detailsGridViewRow.FindControl(WebConstants.CONTROL_DROPDOWNLIST_SELECT_VENDOR);

            holidayData.HolidayID = LabelHolidayID.Text;
            if (TextBoxHolidayReason.Text == "")
            {
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.SCREEN_HOLIDAY_REASON_MANDATORY);
                return;
            }
            holidayData.HolidayReason = TextBoxHolidayReason.Text;

            if (isContractor)
                holidayData.VendorID = Session[WebConstants.SESSION_VENDOR_ID].ToString();
            else
                holidayData.VendorID = DropDownListSelectVendor.SelectedValue;

            holidayData.StartDate = DateCalendarStartDate.GetDate();
            holidayData.EndDate = DateCalendarEndDate.GetDate();

            if (DateTime.Parse(holidayData.EndDate) < DateTime.Parse(holidayData.StartDate))
            {
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.SCREEN_HOLIDAY_DATE_CHECK);
                return;
            }

            if (BO.BusinessObjects.AddUpdateHolidaysListData(holidayData))
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_SUCCESS_SAVE_MESSAGE);
            else
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_SAVE_MESSAGE);

            ResetFilterParameters(DropDownListSelectVendor.SelectedValue, DateCalendarStartDate.GetDate());
            PopulateGridViewHolidays();
        }

        /// <summary>
        /// LinkButtonCancelDetails_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonCancelDetails_Click(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow detailsGridViewRow = GridViewHolidayDetails.Rows[index];

            if (detailsGridViewRow.Cells[colID].Text == "")
            {
                Label LabelStartDate = (Label)detailsGridViewRow.FindControl(WebConstants.CONTROL_LABEL_START_DATE);
                Calendar DateCalendarStartDate = (Calendar)detailsGridViewRow.FindControl(WebConstants.CONTROL_DATECALENDAR_START_DATE);

                Label LabelEndDate = (Label)detailsGridViewRow.FindControl(WebConstants.CONTROL_LABEL_END_DATE);
                Calendar DateCalendarEndDate = (Calendar)detailsGridViewRow.FindControl(WebConstants.CONTROL_DATECALENDAR_END_DATE);

                DateCalendarStartDate.Date = LabelStartDate.Text;
                DateCalendarEndDate.Date = LabelEndDate.Text;

                TextBox TextBoxHolidayReason = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_HOLIDAY_REASON);
                TextBoxHolidayReason.Text = "";
            }
            else
            {
                setLinkButtons(detailsGridViewRow, false);
            }
        }

        /// <summary>
        /// LinkButtonUpdateDetails_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonUpdateDetails_Click(object sender, CommandEventArgs e)
        {
            setLinkButtons(GridViewHolidayDetails.Rows[Convert.ToInt32(e.CommandArgument)], true);
        }

        /// <summary>
        /// LinkButtonDeleteDetails_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonDeleteDetails_Click(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow detailsGridViewRow = GridViewHolidayDetails.Rows[index];

            DTO.Holidays holidayData = new DTO.Holidays();

            Label LabelHolidayID = (Label)detailsGridViewRow.FindControl(WebConstants.CONTROL_LABEL_HOLIDAY_ID);
            holidayData.HolidayID = LabelHolidayID.Text;

            if (BO.BusinessObjects.AddUpdateHolidaysListData(holidayData))
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_SUCCESS_DELETE_MESSAGE);
            else
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_DELETE_MESSAGE); ;

            PopulateGridViewHolidays();
        }

        #endregion

        #region DropDownList Events

        /// <summary>
        /// DropDownListVendors_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListVendors_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            LabelMessage.Text = "";
            PopulateGridViewHolidays();
        }

        /// <summary>
        /// DropDownListReportingType_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListReportingType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            LabelMessage.Text = "";
            PopulateDates();
        }

        /// <summary>
        /// DropDownListReportingDate_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListReportingDate_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            LabelMessage.Text = "";
            PopulateGridViewHolidays();
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// PopulateData
        /// </summary>
        private void PopulateData()
        {
            PopulateDates();
        }

        /// <summary>
        /// PopulateDates
        /// </summary>
        private void PopulateDates()
        {
            DropDownListReportingDate.Items.Clear();

            DTO.Holidays holidayData = new DTO.Holidays();
            holidayData.ReportingType = DropDownListReportingType.SelectedValue;

            DataTable dtMinMaxDates = BO.BusinessObjects.GetHolidaysDate(holidayData).Tables[0];

            if (dtMinMaxDates.Rows.Count > 0)
            {
                DataTable dtDates = new DataTable();
                DataColumn col = new DataColumn(WebConstants.COL_REPORTING_DATE, Type.GetType(WebConstants.STR_SYSTEM_STRING));
                dtDates.Columns.Add(col);

                if (dtMinMaxDates.Rows[0][WebConstants.COL_REPORTING_MIN_DATE].ToString() == "")
                    dtMinMaxDates.Rows[0][WebConstants.COL_REPORTING_MIN_DATE] = "1/1/2013";
                if (dtMinMaxDates.Rows[0][WebConstants.COL_REPORTING_MAX_DATE].ToString() == "")
                    dtMinMaxDates.Rows[0][WebConstants.COL_REPORTING_MAX_DATE] = System.DateTime.Now.ToShortDateString();
                
                DateTime minDate = DateTime.Parse(dtMinMaxDates.Rows[0][WebConstants.COL_REPORTING_MIN_DATE].ToString());
                DateTime maxDate = DateTime.Parse(dtMinMaxDates.Rows[0][WebConstants.COL_REPORTING_MAX_DATE].ToString());

                while (minDate < maxDate.AddMonths(1))
                {
                    string reportingDate = GetReportingDate(minDate);

                    DataRow[] dr = dtDates.Select(WebConstants.COL_REPORTING_DATE + " = '" + reportingDate + "'");

                    if (dr.Length == 0)
                    {
                        DataRow drNew = dtDates.NewRow();
                        drNew[WebConstants.COL_REPORTING_DATE] = reportingDate;
                        dtDates.Rows.InsertAt(drNew, 0);
                    }

                    minDate = minDate.AddMonths(1);
                }

                DropDownListReportingDate.DataSource = dtDates;
                DropDownListReportingDate.DataTextField = WebConstants.COL_REPORTING_DATE;
                DropDownListReportingDate.DataValueField = WebConstants.COL_REPORTING_DATE;
                DropDownListReportingDate.DataBind();

                DropDownListReportingDate.SelectedIndex = DropDownListReportingDate.Items.IndexOf(DropDownListReportingDate.Items.FindByValue(GetReportingDate(System.DateTime.Now)));
            }

            PopulateGridViewHolidays();
        }

        /// <summary>
        /// PopulateVendor
        /// </summary>
        private DataTable PopulateVendors()
        {
            DropDownListVendors.Items.Clear();

            DTO.MasterData masterData = new DTO.MasterData();
            masterData.TableName = WebConstants.TBL_VENDORS;

            dtVendors = BO.BusinessObjects.GetDetailsforMasterData(masterData).Tables[0];

            if (dtVendors.Rows.Count > 0)
            {
                dtVendors.Rows[0][WebConstants.COL_VENDOR] = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.DEF_VAL_TEAMS_ALL); ;

                DropDownListVendors.DataSource = dtVendors;
                DropDownListVendors.DataValueField = WebConstants.COL_VENDOR_ID;
                DropDownListVendors.DataTextField = WebConstants.COL_VENDOR;
                DropDownListVendors.DataBind();
            }

            return dtVendors;
        }

        /// <summary>
        /// PopulateGridViewHolidays
        /// </summary>
        private void PopulateGridViewHolidays()
        {
            DTO.Holidays holidayData = new DTO.Holidays();

            if (isContractor)
                holidayData.VendorID = Session[WebConstants.SESSION_VENDOR_ID].ToString();
            else
                holidayData.VendorID = DropDownListVendors.SelectedValue;

            if (DropDownListReportingType.SelectedValue == WebConstants.DEF_VAL_REPORTING_TYPE_YEARLY)
                holidayData.Year = DropDownListReportingDate.SelectedValue;

            if (DropDownListReportingType.SelectedValue == WebConstants.DEF_VAL_REPORTING_TYPE_MONTHLY)
            {
                string[] dateSplit = DropDownListReportingDate.SelectedValue.Split(new char[] { ',' });
                DateTime date = DateTime.Parse(dateSplit[0].Trim() + "/01/" + dateSplit[1].Trim());
                holidayData.Month = date.Month.ToString();
                holidayData.Year = date.Year.ToString();
            }

            DataTable dtHolidays = BO.BusinessObjects.GetHolidaysList(holidayData).Tables[0];

            if (isReadOnly)
                dtHolidays.Rows.RemoveAt(0);

            GridViewHolidayDetails.DataSource = dtHolidays;
            GridViewHolidayDetails.DataBind();
        }

        /// <summary>
        /// setLinkButtons
        /// </summary>
        /// <param name="gridViewRow"></param>
        /// <param name="isSave"></param>
        private void setLinkButtons(GridViewRow gridViewRow, bool isSave)
        {
            LinkButton LinkButtonSave = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_SAVE);
            LinkButtonSave.Visible = isSave;
            LinkButton LinkButtonCancel = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_CANCEL);
            LinkButtonCancel.Visible = isSave;
            LinkButton LinkButtonUpdate = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_UPDATE);
            LinkButtonUpdate.Visible = (!isSave);
            LinkButton LinkButtonDelete = (LinkButton)gridViewRow.FindControl(WebConstants.CONTROL_LINKBUTTON_DELETE);
            LinkButtonDelete.Visible = (!isSave);

            //Vendor
            Label LabelVendor = (Label)gridViewRow.FindControl(WebConstants.CONTROL_LABEL_VENDOR);
            LabelVendor.Visible = true;

            if (isContractor)
                LabelVendor.Text = dtVendors.Select(WebConstants.COL_VENDOR_ID + " = " + Session[WebConstants.SESSION_VENDOR_ID].ToString())[0][WebConstants.COL_VENDOR].ToString();
            else
            {
                LabelVendor.Visible = (!isSave);
                DropDownList DropDownListSelectVendor = (DropDownList)gridViewRow.FindControl(WebConstants.CONTROL_DROPDOWNLIST_SELECT_VENDOR);
                DropDownListSelectVendor.Visible = isSave;
                if (isSave)
                {
                    DataTable dtSelectvendor = dtVendors.Copy();
                    dtSelectvendor.Rows.RemoveAt(0);

                    DropDownListSelectVendor.DataSource = dtSelectvendor;
                    DropDownListSelectVendor.DataValueField = WebConstants.COL_VENDOR_ID;
                    DropDownListSelectVendor.DataTextField = WebConstants.COL_VENDOR;
                    DropDownListSelectVendor.DataBind();

                    if (LabelVendor.Text != "")
                    {
                        DropDownListSelectVendor.SelectedIndex = DropDownListSelectVendor.Items.IndexOf(DropDownListSelectVendor.Items.FindByValue(LabelVendor.Text));
                    }
                }
            }

            //Holiday Reason
            Label LabelHolidayReason = (Label)gridViewRow.FindControl(WebConstants.CONTROL_LABEL_HOLIDAY_REASON);
            LabelHolidayReason.Visible = (!isSave);
            TextBox TextBoxHolidayReason = (TextBox)gridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_HOLIDAY_REASON);
            TextBoxHolidayReason.Visible = isSave;
            TextBoxHolidayReason.Text = LabelHolidayReason.Text;

            //Start Date
            Label LabelStartDate = (Label)gridViewRow.FindControl(WebConstants.CONTROL_LABEL_START_DATE);
            LabelStartDate.Visible = (!isSave);
            Calendar DateCalendarStartDate = (Calendar)gridViewRow.FindControl(WebConstants.CONTROL_DATECALENDAR_START_DATE);
            DateCalendarStartDate.Visible = isSave;
            DateCalendarStartDate.Date = LabelStartDate.Text;

            //End Date
            Label LabelEndDate = (Label)gridViewRow.FindControl(WebConstants.CONTROL_LABEL_END_DATE);
            LabelEndDate.Visible = (!isSave);
            Calendar DateCalendarEndDate = (Calendar)gridViewRow.FindControl(WebConstants.CONTROL_DATECALENDAR_END_DATE);
            DateCalendarEndDate.Visible = isSave;
            DateCalendarEndDate.Date = LabelEndDate.Text;

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
        /// ResetFilterParameters
        /// </summary>
        /// <param name="vendorID"></param>
        /// <param name="startDate"></param>
        private void ResetFilterParameters(string vendorID, string startDate)
        {
            PopulateDates();
            DropDownListVendors.SelectedIndex = DropDownListVendors.Items.IndexOf(DropDownListVendors.Items.FindByValue(vendorID));
            DropDownListReportingDate.SelectedIndex = DropDownListReportingDate.Items.IndexOf(DropDownListReportingDate.Items.FindByValue(GetReportingDate(startDate)));
        }

        /// <summary>
        /// GetReportingDate
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private string GetReportingDate(string date)
        {
            return GetReportingDate(DateTime.Parse(date));
        }

        /// <summary>
        /// GetReportingDate
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private string GetReportingDate(DateTime date)
        {
            if (DropDownListReportingType.SelectedValue == WebConstants.DEF_VAL_REPORTING_TYPE_MONTHLY)
            {
                return date.ToString(WebConstants.DEF_VAL_DATE_MONTH_FORMAT) + " , " + date.Year.ToString();
            }
            if (DropDownListReportingType.SelectedValue == WebConstants.DEF_VAL_REPORTING_TYPE_YEARLY)
            {
                return date.Year.ToString();
            }

            return "";
        }

        #endregion

        #region Screen Access and Labels

        /// <summary>
        /// GetScreenAccess
        /// </summary>
        private void GetScreenAccess()
        {
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
                GridViewHolidayDetails.Columns[COL_SAVE_UPDATE_NO].Visible = false;
                GridViewHolidayDetails.Columns[COL_CANCEL_DELETE_NO].Visible = false;
            }

            if (isContractor)
            {
                LabelVendorValue.Text = DropDownListVendors.SelectedItem.Text;
                LabelVendorValue.Visible = true;
                DropDownListVendors.Visible = false;
            }
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
                PanelSearchFilter.GroupingText = COM.GetScreenLocalizedLabel(dtScreenLabels, PanelSearchFilter.GroupingText);
                LabelDate.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelDate.Text);
                LabelVendor.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelVendor.Text);

                foreach (ListItem item in DropDownListReportingType.Items)
                    item.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, item.Value);

                foreach (DataControlField field in GridViewHolidayDetails.Columns)
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