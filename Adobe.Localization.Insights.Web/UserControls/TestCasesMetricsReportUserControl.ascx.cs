/*
 * http://bloggingabout.net/blogs/jschreuder/archive/2007/12/10/crystal-reports-for-visual-studio-2005-deployment-trouble.aspx
 * http://forums.sdn.sap.com/thread.jspa?threadID=1601959
 * 
 */
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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using BO = Adobe.Localization.Insights.BusinessLayer;
using DTO = Adobe.Localization.Insights.Common.DataTransferObjects;

namespace Adobe.Localization.Insights.Web.UserControls
{
    /// <summary>
    /// GenerateWSRUserControl
    /// </summary>
    public partial class TestCasesMetricsReportUserControl : System.Web.UI.UserControl
    {
        //private Int64 userProductID;
        //private Int64 userVendorID;
        //private Int64 weekID;

        #region Page Load

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (Session["Report"] != null)
                {
                    CrystalReportViewer1.ReportSource = (ReportDocument)Session["Report"];
                }
            }
            //SetProduct(Session["ProductID"].ToString(), Session["userID"].ToString());
            //if (!IsPostBack)
            //{
            //    PopulateVendors();
            //    SetReportingWeek();
            //    SetWSRData();
            //}
        }

        /*
        /// <summary>
        /// SetProduct
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        private void SetProduct(string productID, string userID)
        {
            DTO.TransferData transferData = new DTO.TransferData();
            transferData.UserID = userID;
            transferData.ProductID = productID;

            DataSet product = BO.UsersBO.GetProducts(transferData);

            if (product.Tables.Count > 0)
            {
                if (product.Tables[0].Rows.Count > 0)
                {
                    LabelProductName.Text = product.Tables[0].Rows[0]["Product"].ToString();
                    userProductID = (Int64)product.Tables[0].Rows[0]["UserProductID"];
                }
            }
        }

        /// <summary>
        /// PopulateVendors
        /// </summary>
        /// <param name="transferData"></param>
        private void PopulateVendors()
        {
            DTO.TransferData transferData = new DTO.TransferData();
            DataSet vendors = BO.UsersBO.GetVendorDetails(transferData);

            DataRow drAll = vendors.Tables[0].NewRow();
            drAll["VendorID"] = 0;
            drAll["Vendor"] = "All";
            vendors.Tables[0].Rows.InsertAt(drAll, 0);
            
            DropDownListVendor.DataSource = vendors.Tables[0];
            DropDownListVendor.DataTextField = "Vendor";
            DropDownListVendor.DataValueField = "VendorID";
            DropDownListVendor.DataBind();

       }

        /// <summary>
        /// SetWeek
        /// </summary>
        private void SetReportingWeek()
        {
            DataSet week = BO.UsersBO.GetReportingWeek(DropDownListReportingType.SelectedValue);

            DropDownListWeek.DataSource = week.Tables[0];

            if (DropDownListReportingType.SelectedValue == "Weekly")
            {
                DropDownListWeek.DataTextField = "Week";
                DropDownListWeek.DataValueField = "WeekID";
            }
            else
            {
                DropDownListWeek.DataTextField = "Month";
                DropDownListWeek.DataValueField = "Month";
            }

            DropDownListWeek.DataBind();
        }

        /// <summary>
        /// SetWSRData
        /// </summary>
        private void SetWSRData()
        {
            DTO.WSRData dtoWSRData = new DTO.WSRData();

            dtoWSRData.UserProductID = userProductID.ToString();
            dtoWSRData.ReportingType = DropDownListReportingType.SelectedValue;
            dtoWSRData.WeekID = DropDownListWeek.SelectedValue;

            if (DropDownListVendor.SelectedValue != "0")
            {
                dtoWSRData.VendorID = DropDownListVendor.SelectedValue;
            }

            dtoWSRData = BO.UsersBO.GetCombinedWSRData(dtoWSRData);

            ViewState["WSRData"] = dtoWSRData;

            #region Issues and Accomplishments

            TextBoxRedIssues.Text = dtoWSRData.RedIssues;
            TextBoxYellowIssues.Text = dtoWSRData.YellowIssues;
            TextBoxGreenAccomplishments.Text = dtoWSRData.GreenAccom;

            #endregion

            #region Metrics

            //Test Cases
            TextBoxTestCasesExecuted.Text = dtoWSRData.TestCasesExecuted.ToString();
            //TextBoxOtherTCSInfo.Text = dtoWSRData.TcRemarks;

            //Bugs
            TextBoxTotalBugsFound.Text = dtoWSRData.TotalBugs.ToString();
            TextBoxBugsRegressed.Text = dtoWSRData.BugsRegressed.ToString();
            TextBoxBugsPending.Text = dtoWSRData.BugsPending.ToString();
            //TextBoxOtherBugInfo.Text = dtoWSRData.BugRemarks;

            //Efforts
            TextBoxExecutionHours.Text = dtoWSRData.ExecHours.ToString();
            TextBoxRegressionHours.Text = dtoWSRData.RegressionHours.ToString();
            TextBoxMachineSetup.Text = dtoWSRData.MachineSetupHours.ToString();
            TextBoxMeetings.Text = dtoWSRData.MeetingHours.ToString();
            TextBoxWSR.Text = dtoWSRData.WSRHours.ToString();
            //TextBoxOtherEffortInfo.Text = dtoWSRData.EffortRemarks;
            #endregion

            #region Next Week Priorities

            TextBoxNewDeliverables.Text = dtoWSRData.NewDeliverables;
            //Prev Week Deliverables
            GridViewOutstandingDeliverables.DataSource = dtoWSRData.PrevWeekDeliverables;
            GridViewOutstandingDeliverables.DataBind();

            #endregion
        }
        */
        #endregion

        #region Button Events

        /// <summary>
        /// ButtonShowReports_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonShowReports_Click(object sender, EventArgs e)
        {
            DataSet metricsData = GetData();

            LoadReport(metricsData);
        }

        /// <summary>
        /// GetData
        /// </summary>
        /// <returns></returns>
        private DataSet GetData()
        {
            DTO.WSRData data = new DTO.WSRData();

            data.ReportingType = ReportsFilterUserControl.ReportingType;
            data.WeekID = ReportsFilterUserControl.ReportingWeek;
            data.ProductID = ReportsFilterUserControl.Products;
            data.VendorID = ReportsFilterUserControl.Vendors;
            data.Reporting = true;

            return BO.BusinessObjects.GetWSRDetails("TestCasesStats", data);
        }

        /// <summary>
        /// metricsData
        /// </summary>
        /// <param name="metricsData"></param>
        protected void LoadReport(DataSet metricsData)
        {
            //Reports.TestCasesReport report = new Reports.TestCasesReport();

            //report.SetDataSource(metricsData.Tables[0]);

            //CrystalReportViewer1.ReportSource = report;
            //CrystalReportViewer1.DisplayStatusbar = false;
            //CrystalReportViewer1.DisplayToolbar = false;
            ////CrystalReportViewer1.GroupTreeStyle = CrystalDecisions.Shared.GroupTreeStyle.BLANK;
            //CrystalReportViewer1.DataBind();

            metricsData.Tables[0].TableName = "ViewTestCasesData";
            ReportDocument rptDoc = new ReportDocument();
            rptDoc.Load(Server.MapPath("/Reports/Copy of TestCasesReport.rpt"));
            rptDoc.SetDataSource(metricsData);
            CrystalReportViewer1.ReportSource = rptDoc;

            Session["Report"] = rptDoc;
        }

        #endregion

        /*
        #region GridView Events

        ///// <summary>
        ///// GridViewOutstandingDeliverables_OnRowCommand
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void GridViewOutstandingDeliverables_OnRowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    DTO.WSRData dtoWSRData = (DTO.WSRData)ViewState["WSRData"];
        //    DataSet prevWeekDeliverables = dtoWSRData.PrevWeekDeliverables;


        //    GridViewRow gridViewRow = (GridViewRow)(((Button)e.CommandSource).NamingContainer); ;
        //    int index = gridViewRow.RowIndex;

        //    TextBox textBoxPrevWeekDeliverables = (TextBox)gridViewRow.FindControl("TextBoxPrevWeekDeliverables");
        //    TextBox textBoxReason = (TextBox)gridViewRow.FindControl("TextBoxReason");
        //    Calendar calendarOrigScheduleDate = (Calendar)gridViewRow.FindControl("DateCalendarOrigScheduleDate");
        //    Button buttonDelete = ((Button)gridViewRow.FindControl("ButtonDelete"));

        //    if (e.CommandName == "Save")
        //    {
        //        if (textBoxPrevWeekDeliverables.Text != "")
        //        {
        //            if (index == prevWeekDeliverables.Tables[0].Rows.Count - 1)
        //            {
        //                DataRow dr = prevWeekDeliverables.Tables[0].Rows[prevWeekDeliverables.Tables[0].Rows.Count - 1];
        //                dr["PrevWeekDeliverables"] = textBoxPrevWeekDeliverables.Text;
        //                dr["OriginalScheduleDate"] = (DateTime.Parse(calendarOrigScheduleDate.GetDate())).ToShortDateString();
        //                dr["Reason"] = textBoxReason.Text;
        //                prevWeekDeliverables.Tables[0].Rows.InsertAt(prevWeekDeliverables.Tables[0].NewRow(), prevWeekDeliverables.Tables[0].Rows.Count);
        //                buttonDelete.Enabled = true;
        //            }
        //            else
        //            {
        //                DataRow dr = prevWeekDeliverables.Tables[0].Rows[index];
        //                dr["PrevWeekDeliverables"] = textBoxPrevWeekDeliverables.Text;
        //                dr["OriginalScheduleDate"] = (DateTime.Parse(calendarOrigScheduleDate.GetDate())).ToShortDateString();
        //                dr["Reason"] = textBoxReason.Text;
        //            }
        //        }
        //    }

        //    if (e.CommandName == "Delete")
        //    {
        //        DataRow dr = prevWeekDeliverables.Tables[0].Rows[index];
        //        dr.Delete();
        //    }

        //    ViewState["WSRData"] = dtoWSRData;
        //    GridViewOutstandingDeliverables.DataSource = dtoWSRData.PrevWeekDeliverables;
        //    GridViewOutstandingDeliverables.DataBind();
        //}

        ///// <summary>
        ///// GridViewOutstandingDeliverables_OnRowDataBound
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void GridViewOutstandingDeliverables_OnRowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    TextBox textBox = (TextBox)e.Row.FindControl("TextBoxPrevWeekDeliverables");

        //    if (textBox != null)
        //    {
        //        Calendar calendar = (Calendar)e.Row.FindControl("DateCalendarOrigScheduleDate");
        //        HtmlInputButton dateButton;
        //        HtmlGenericControl dateDiv;

        //        dateButton = (HtmlInputButton)calendar.FindControl("button");
        //        dateDiv = (HtmlGenericControl)calendar.FindControl("DivCalendar");
        //        dateButton.Attributes.Add(WebConstants.CONTROL_DATECALENDAR_ONCLICK, "javascript:return OnClick('" + dateDiv.ClientID + "')");

        //        if (textBox.Text == "")
        //        {
        //            ((Button)e.Row.FindControl("ButtonDelete")).Enabled = false;
        //        }
        //    }
        //}

        #endregion

        #region DropDownList Events

        /// <summary>
        /// DropDownListVendor_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListVendor_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //    LabelVendorName.Text = DropDownListVendor.Text;
            //    userVendorID = Convert.ToInt64(DropDownListVendor.SelectedValue);
        }

        /// <summary>
        /// DropDownListReportingType_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListReportingType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            SetReportingWeek();
            SetWSRData();
        }

        /// <summary>
        /// DropDownListWeek_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListWeek_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            SetWSRData();
        }

        #endregion

        */
    }
}