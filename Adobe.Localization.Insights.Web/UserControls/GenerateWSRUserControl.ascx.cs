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
using BO = Adobe.Localization.Insights.BusinessLayer;
using DTO = Adobe.Localization.Insights.Common.DataTransferObjects;

namespace Adobe.Localization.Insights.Web.UserControls
{
    /// <summary>
    /// GenerateWSRUserControl
    /// </summary>
    public partial class GenerateWSRUserControl : System.Web.UI.UserControl
    {
        private Int64 userProductID;
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
            SetProduct(Session["ProductID"].ToString(), Session["userID"].ToString());
            if (!IsPostBack)
            {
                PopulateVendors();
                SetReportingWeek();
                SetWSRData();
            }
        }

        /// <summary>
        /// SetProduct
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        private void SetProduct(string productID, string userID)
        {
            DTO.Users userData = new DTO.Users();
            userData.UserID = userID;
            userData.ProductID = productID;

            DataSet product = BO.UsersBO.GetProducts(userData);

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
            DTO.Users userData = new DTO.Users();
            DataSet vendors = BO.UsersBO.GetVendorDetails(userData);

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

            //dtoWSRData.UserProductID = userProductID.ToString();
            dtoWSRData.ProductID = Session["ProductID"].ToString();
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

        #endregion

        #region Button Events

        /// <summary>
        /// ButtonGenerateWSR_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonGenerateWSR_Click(object sender, EventArgs e)
        {
        }

        #endregion

        #region DropDownList Events

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

        /// <summary>
        /// DropDownListVendor_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListVendor_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            SetWSRData();
        }

        #endregion
    }
}