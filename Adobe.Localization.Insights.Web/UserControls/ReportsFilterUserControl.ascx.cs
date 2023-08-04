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
using System.Text;
using BO = Adobe.Localization.Insights.BusinessLayer;
using DTO = Adobe.Localization.Insights.Common.DataTransferObjects;

namespace Adobe.Localization.Insights.Web.UserControls
{
    /// <summary>
    /// GenerateWSRUserControl
    /// </summary>
    public partial class ReportsFilterUserControl : System.Web.UI.UserControl
    {
        //private Int64 userProductID;
        //private Int64 userVendorID;
        //private Int64 weekID;
        private Control controlFired;
        private DataSet products;
        private DataSet vendors;

        #region Page Load

        /// <summary>
        /// OnInit
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            PopulateProducts();
            PopulateVendors();
        }

        /// <summary>
        /// OnPreRender
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            if (controlFired is CheckBox)
            {
                CheckBox chkBoxControl = (CheckBox)controlFired;
                bool isChecked = false;

                if (chkBoxControl.ID.Contains("Products"))
                {
                    string chkType = "Products";
                    isChecked = ((CheckBox)ShowProductCheckBoxes.FindControl(chkBoxControl.ID)).Checked;

                    if (chkBoxControl.Text.Contains("Select All"))
                        foreach (DataRow dr in products.Tables[0].Select())
                            ((CheckBox)ShowProductCheckBoxes.FindControl(chkType + dr[0].ToString())).Checked = isChecked;
                    else if (!isChecked)
                        ((CheckBox)ShowProductCheckBoxes.FindControl(chkType)).Checked = false;
                }

                if (chkBoxControl.ID.Contains("Vendors"))
                {
                    string chkType = "Vendors";
                    isChecked = ((CheckBox)ShowVendorCheckBoxes.FindControl(chkBoxControl.ID)).Checked;

                    if (chkBoxControl.Text.Contains("Select All"))
                        foreach (DataRow dr in vendors.Tables[0].Select())
                            ((CheckBox)ShowVendorCheckBoxes.FindControl(chkType + dr[0].ToString())).Checked = isChecked;
                    else if (!isChecked)
                        ((CheckBox)ShowVendorCheckBoxes.FindControl(chkType)).Checked = false;
                }
            }
        }

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //PopulateProducts();
                //PopulateVendors();
                SetReportingWeek();
                //SetWSRData();
            }
            else
            {
                controlFired = GetPostBackControl(Page);
            }
        }

        /// <summary>
        /// 
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

        #endregion

        #region Populate Controls

        /// <summary>
        /// SetProduct
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        private void PopulateProducts()
        {
            DTO.Product productData = new DTO.Product();
            products = BO.BusinessObjects.GetProducts(productData);

            ShowProductCheckBoxes.Controls.Add(CreateDynamicallyLinkedCheckBoxes("Products", products));
        }

        /// <summary>
        /// PopulateVendors
        /// </summary>
        /// <param name="transferData"></param>
        private void PopulateVendors()
        {
            DTO.Users userData = new DTO.Users();
            vendors = BO.BusinessObjects.GetVendorDetails(userData);

            ShowVendorCheckBoxes.Controls.Add(CreateDynamicallyLinkedCheckBoxes("Vendors", vendors));
        }

        /// <summary>
        /// SetReportingWeek
        /// </summary>
        private void SetReportingWeek()
        {
            DataSet week = BO.BusinessObjects.GetReportingWeek(DropDownListReportingType.SelectedValue);

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
            DropDownListWeek.Items.Insert(0, new ListItem("All", "-1"));
            DropDownListWeek.DataBind();
        }

        #endregion

        #region Create CheckBoxes Dynamically

        /// <summary>
        /// CreateDynamicallyLinkedCheckBoxes
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        private Table CreateDynamicallyLinkedCheckBoxes(string chkType, Object ds)
        {
            DataSet checkBoxesData = (DataSet)ds;
            int colCounter = 0;
            int maxCols = 5;

            Table tb = SetTableProperties();
            TableRow tr = SetTableRowProperties();

            for (; colCounter < checkBoxesData.Tables[0].Rows.Count; colCounter++)
            {
                DataRow dr = checkBoxesData.Tables[0].Rows[colCounter];

                if (colCounter % maxCols == 0)
                {
                    if (colCounter != 0)
                        tb.Controls.Add(tr);
                    tr = SetTableRowProperties();
                }

                tr.Controls.Add(SetTableCellProperties(AddCheckBox(chkType + dr[0].ToString(), dr[2].ToString())));
            }

            for (; colCounter < maxCols; colCounter++)
            {
                tr.Controls.Add(SetTableCellProperties(AddCheckBox((colCounter + 100).ToString(), "")));
            }

            if (colCounter != 0)
                tb.Controls.Add(tr);

            #region Add SelectAll Option

            tr = SetTableRowProperties();

            tr.Controls.Add(SetTableCellProperties(AddCheckBox(chkType, "Select All")));
            tb.Controls.Add(tr);

            #endregion

            return tb;
        }

        /// <summary>
        /// AddCheckBox
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        private CheckBox AddCheckBox(string id, string text)
        {
            CheckBox chk = new CheckBox();

            if (text == "")
            {
                chk.Visible = false;
            }
            chk.ID = id;
            chk.Text = text;
            chk.AutoPostBack = true;
            chk.ForeColor = System.Drawing.Color.AntiqueWhite;
            chk.EnableViewState = true;
            chk.Width = System.Web.UI.WebControls.Unit.Percentage(100);
            return chk;
        }

        /// <summary>
        /// SetTableProperties
        /// </summary>
        /// <param name="width"></param>
        /// <returns></returns>
        private Table SetTableProperties()
        {
            Table tb = new Table();
            tb.BorderWidth = 2;
            tb.BorderColor = System.Drawing.Color.Blue;
            tb.Width = System.Web.UI.WebControls.Unit.Percentage(100);
            return tb;
        }

        /// <summary>
        /// SetTableRowProperties
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        private TableRow SetTableRowProperties()
        {
            TableRow tr = new TableRow();
            tr.Width = System.Web.UI.WebControls.Unit.Percentage(100);

            return tr;
        }

        /// <summary>
        /// SetTableCellProperties
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        private TableCell SetTableCellProperties(CheckBox checkbox)
        {
            TableCell td = new TableCell();
            if (checkbox != null)
            {
                td.Controls.Add(checkbox);
                td.Width = System.Web.UI.WebControls.Unit.Percentage(20);
            }
            else
            {
                td.Width = System.Web.UI.WebControls.Unit.Pixel(10);
            }
            td.HorizontalAlign = HorizontalAlign.Left;
            return td;
        }

        #endregion

        /*
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
        /*
        #region Button Events

        /// <summary>
        /// ButtonSave_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonGenerateWSR_Click(object sender, EventArgs e)
        {
        }

        #endregion

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
        */

        #region DropDownList Events

        /// <summary>
        /// DropDownListReportingType_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListReportingType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            SetReportingWeek();
        }

        #endregion

        #region Get Properties

        /// <summary>
        /// ReportingType
        /// </summary>
        public string ReportingType
        {
            get
            {
                return DropDownListReportingType.SelectedValue;
            }
        }

        /// <summary>
        /// ReportingWeek
        /// </summary>
        public string ReportingWeek
        {
            get
            {
                return DropDownListWeek.SelectedValue;
            }
        }

        /// <summary>
        /// Products
        /// </summary>
        public string Products
        {
            get
            {
                StringBuilder product = new StringBuilder("0");
                string chkType = "Products";
                foreach (DataRow dr in products.Tables[0].Select())
                {
                    if (((CheckBox)ShowProductCheckBoxes.FindControl(chkType + dr[0].ToString())).Checked)
                    {
                        product.Append("," + dr[0].ToString());
                    }
                }

                return product.ToString();
            }
        }

        /// <summary>
        /// Vendors
        /// </summary>
        public string Vendors
        {
            get
            {
                StringBuilder vendor = new StringBuilder("0");
                string chkType = "Vendors";
                foreach (DataRow dr in vendors.Tables[0].Select())
                {
                    if (((CheckBox)ShowVendorCheckBoxes.FindControl(chkType + dr[0].ToString())).Checked)
                    {
                        vendor.Append("," + dr[0].ToString());
                    }
                }

                return vendor.ToString();
            }
        }

        #endregion

    }
}