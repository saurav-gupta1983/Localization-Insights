using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Web.UI.WebControls;
using Adobe.Localization.Insights.Common;
using BO = Adobe.Localization.Insights.BusinessLayer;
using DTO = Adobe.Localization.Insights.Common.DataTransferObjects;

namespace Adobe.Localization.Insights.Web.UserControls
{
    public partial class AddUpdateUsersUserControl : System.Web.UI.UserControl
    {
        #region Variables

        private DataTable masterDetailsDataTable = new DataTable();
        private DataTable typeDetailsDataTable = new DataTable();
        private DataTable vendorDetailsDataTable = new DataTable();

        private int colID = 0;
        private string tableName = "";
        private string typeTableName = "";
        private string vendorTableName = "";
        private ArrayList columnNames;
        private bool isSearchFilter = false;

        #endregion

        #region Page Load Event

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            tableName = "Users";
            typeTableName = "ProjectRoles";
            vendorTableName = "Vendors";
            if (!this.IsPostBack)
            {
                ViewState["columnNames"] = GetColumnNames(tableName);
                PopulateData();
            }
            else
            {
                columnNames = (ArrayList)ViewState["columnNames"];
            }
            typeDetailsDataTable = (DataTable)ViewState["TypeDetailsDataTable"];
            vendorDetailsDataTable = (DataTable)ViewState["VendorDetailsDataTable"];
            SetUserDisplay();
            MessageLabel.Text = "";
        }

        #endregion

        #region Button Events

        /// <summary>
        /// ButtonSearch_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SearchButton_Click(object sender, EventArgs e)
        {
            isSearchFilter = true;
            GetMasterDetails();
            isSearchFilter = false;
        }

        #endregion

        #region Grid Events

        /// <summary>
        /// GridViewUserDetails_PageIndexChanging
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewUserDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewUserDetails.PageIndex = e.NewPageIndex;
            GetMasterDetails();
            if (!(masterDetailsDataTable.Rows[e.NewPageIndex * 10][colID].ToString() == "0" || masterDetailsDataTable.Rows[e.NewPageIndex * 10][colID].ToString() == ""))
            {
                DataRow dr = masterDetailsDataTable.NewRow();
                dr[colID] = 0;
                masterDetailsDataTable.Rows.InsertAt(dr, e.NewPageIndex * 10);
            }
            GridViewUserDetails.DataSource = masterDetailsDataTable;
            GridViewUserDetails.DataBind();
        }

        /// <summary>
        /// GridViewUserDetails_RowDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewUserDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[colID].Text.ToString() == "0" || e.Row.Cells[colID].Text.ToString() == "&nbsp;")
                {
                    e.Row.Cells[colID].Text = "";
                    setLinkButtons(e.Row, true);
                }
                else
                {
                    //ProjectRole
                    Label TypeLabel = (Label)e.Row.FindControl("TypeLabel");
                    if (TypeLabel.Text != "")
                    {
                        TypeLabel.Text = typeDetailsDataTable.Select("ProjectRoleID= " + TypeLabel.Text)[0]["ProjectRole"].ToString();
                    }

                    //Vendor
                    Label LabelVendor = (Label)e.Row.FindControl("LabelVendor");
                    if (LabelVendor.Text != "")
                    {
                        LabelVendor.Text = vendorDetailsDataTable.Select("VendorID= " + LabelVendor.Text)[0]["Vendor"].ToString();
                    }

                    //IsSuperUser
                    Label SuperUserLabel = (Label)e.Row.FindControl("SuperUserLabel");
                    if (SuperUserLabel.Text == "1")
                    {
                        SuperUserLabel.Text = "True";
                    }
                    else
                    {
                        SuperUserLabel.Text = "";
                    }
                }
            }
        }

        /// <summary>
        /// GridViewUserDetails_RowUpdating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewUserDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
        }

        /// <summary>
        /// GridViewUserDetails_RowCancelingEdit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewUserDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
        }

        #endregion

        #region GridView Button Click Events

        /// <summary>
        /// AddDetailsLinkButton_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SaveDetailsLinkButton_Click(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow detailsGridViewRow = GridViewUserDetails.Rows[index];

            DTO.MasterData masterData = new DTO.MasterData();
            masterData.TableName = tableName;
            masterData.ColumnNames = columnNames;

            TextBox CodeTextBox = (TextBox)detailsGridViewRow.FindControl("CodeTextBox");
            TextBox DescriptionTextBox = (TextBox)detailsGridViewRow.FindControl("DescriptionTextBox");
            TextBox TextBoxEmailID = (TextBox)detailsGridViewRow.FindControl("TextBoxEmailID");
            DropDownList TypeDropDownList = (DropDownList)detailsGridViewRow.FindControl("TypeDropDownList");
            DropDownList DropDownListVendor = (DropDownList)detailsGridViewRow.FindControl("DropDownListVendor");
            CheckBox SuperUserCheckBox = (CheckBox)detailsGridViewRow.FindControl("SuperUserCheckBox");

            masterData.Description = DescriptionTextBox.Text;
            masterData.EmailID = TextBoxEmailID.Text;
            if (CodeTextBox.Text == "")
            {
                MessageLabel.Text = "Please provide Vendor Code";
                return;
            }
            masterData.Code = CodeTextBox.Text;
            masterData.Type = TypeDropDownList.SelectedValue;

            Label LabelUserVendorID = (Label)detailsGridViewRow.FindControl("LabelUserVendorID");
            masterData.UserVendorID = LabelUserVendorID.Text;
            masterData.VendorID = DropDownListVendor.SelectedValue;

            if (SuperUserCheckBox.Checked)
            {
                masterData.IsSuperUser = "1";
            }
            else
            {
                masterData.IsSuperUser = "0";
            }

            if (detailsGridViewRow.Cells[colID].Text != "")
            {
                Label IDLabel = (Label)detailsGridViewRow.FindControl("IDLabel");
                masterData.MasterDataID = IDLabel.Text;
            }

            if (BO.UsersBO.AddUpdateUserDetailsforMasterData(masterData))
            {
                MessageLabel.Text = WebConstants.SAVED_SUCCESS;
            }
            else
            {
                MessageLabel.Text = WebConstants.FAILURE;
            }

            PopulateData();
        }

        /// <summary>
        /// CancelDetailsLinkButton_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CancelDetailsLinkButton_Click(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow detailsGridViewRow = GridViewUserDetails.Rows[index];

            if (detailsGridViewRow.Cells[colID].Text == "")
            {
                TextBox DescriptionTextBox = (TextBox)detailsGridViewRow.FindControl("DescriptionTextBox");
                DescriptionTextBox.Text = "";
                TextBox CodeTextBox = (TextBox)detailsGridViewRow.FindControl("CodeTextBox");
                CodeTextBox.Text = "";
                TextBox TextBoxEmailID = (TextBox)detailsGridViewRow.FindControl("TextBoxEmailID");
                TextBoxEmailID.Text = "";
                CheckBox SuperUserCheckBox = (CheckBox)detailsGridViewRow.FindControl("SuperUserCheckBox");
                SuperUserCheckBox.Checked = false;
            }
            else
            {
                setLinkButtons(detailsGridViewRow, false);
            }
        }

        /// <summary>
        /// UpdateDetailsLinkButton_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UpdateDetailsLinkButton_Click(object sender, CommandEventArgs e)
        {
            setLinkButtons(GridViewUserDetails.Rows[Convert.ToInt32(e.CommandArgument)], true);
        }

        /// <summary>
        /// DeleteDetailsLinkButton_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DeleteDetailsLinkButton_Click(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow detailsGridViewRow = GridViewUserDetails.Rows[index];

            DTO.MasterData masterData = new DTO.MasterData();
            masterData.TableName = tableName;
            masterData.ColumnNames = columnNames;

            Label IDLabel = (Label)detailsGridViewRow.FindControl("IDLabel");
            masterData.MasterDataID = IDLabel.Text;

            Label LabelUserVendorID = (Label)detailsGridViewRow.FindControl("LabelUserVendorID");
            masterData.UserVendorID = LabelUserVendorID.Text;

            if (BO.UsersBO.AddUpdateUserDetailsforMasterData(masterData))
            {
                MessageLabel.Text = WebConstants.DELETED_SUCCESS;
            }

            PopulateData();
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// PopulateData
        /// </summary>
        private void PopulateData()
        {
            typeDetailsDataTable = PopulateTypeDetails("TypeDetailsDataTable", typeDetailsDataTable, typeTableName);
            vendorDetailsDataTable = PopulateTypeDetails("VendorDetailsDataTable", vendorDetailsDataTable, vendorTableName);
            GetMasterDetails();
        }

        /// <summary>
        /// PopulateTypeDetails
        /// </summary>
        private DataTable PopulateTypeDetails(string viewStateKey, DataTable dt, string tableName)
        {
            DTO.MasterData masterData = new DTO.MasterData();
            masterData.TableName = tableName;
            dt = BO.UsersBO.GetDetailsforMasterData(masterData).Tables[0];
            dt.Rows.RemoveAt(0);

            ViewState[viewStateKey] = dt;

            return dt;
        }

        /// <summary>
        /// GetMasterDetails
        /// </summary>
        private void GetMasterDetails()
        {
            DTO.MasterData masterData = new DTO.MasterData();
            masterData.TableName = tableName;
            masterData.ColumnNames = columnNames;
            masterData.IsUsersFilter = isSearchFilter;
            masterData.Description = TextBoxUser.Text;
            masterDetailsDataTable = BO.UsersBO.GetDetailsforMasterData(masterData).Tables[0];
            GridViewUserDetails.DataSource = masterDetailsDataTable;
            GridViewUserDetails.DataBind();
        }

        /// <summary>
        /// setLinkButtons
        /// </summary>
        /// <param name="gridViewRow"></param>
        /// <param name="isSave"></param>
        private void setLinkButtons(GridViewRow gridViewRow, bool isSave)
        {
            LinkButton SaveLinkButton = (LinkButton)gridViewRow.FindControl("SaveLinkButton");
            SaveLinkButton.Visible = isSave;
            LinkButton LinkButtonCancel = (LinkButton)gridViewRow.FindControl("CancelLinkButton");
            LinkButtonCancel.Visible = isSave;
            LinkButton UpdateButtonControl = (LinkButton)gridViewRow.FindControl("UpdateLinkButton");
            UpdateButtonControl.Visible = (!isSave);
            LinkButton DeleteButtonControl = (LinkButton)gridViewRow.FindControl("DeleteLinkButton");
            DeleteButtonControl.Visible = (!isSave);

            //Code
            Label CodeLabel = (Label)gridViewRow.FindControl("CodeLabel");
            CodeLabel.Visible = (!isSave);
            TextBox CodeTextBox = (TextBox)gridViewRow.FindControl("CodeTextBox");
            CodeTextBox.Visible = isSave;
            CodeTextBox.Text = CodeLabel.Text;

            if (!IsProjectSpecific)
            {
                //Description
                Label DescriptionLabel = (Label)gridViewRow.FindControl("DescriptionLabel");
                DescriptionLabel.Visible = (!isSave);
                TextBox DescriptionTextBox = (TextBox)gridViewRow.FindControl("DescriptionTextBox");
                DescriptionTextBox.Visible = isSave;
                DescriptionTextBox.Text = DescriptionLabel.Text;

                //Type
                Label TypeLabel = (Label)gridViewRow.FindControl("TypeLabel");
                TypeLabel.Visible = (!isSave);
                DropDownList TypeDropDownList = (DropDownList)gridViewRow.FindControl("TypeDropDownList");
                TypeDropDownList.Visible = isSave;
                if (isSave)
                {
                    TypeDropDownList.DataSource = typeDetailsDataTable;
                    TypeDropDownList.DataValueField = "ProjectRoleID";
                    TypeDropDownList.DataTextField = "ProjectRole";
                    TypeDropDownList.DataBind();
                    if (TypeLabel.Text != "")
                    {
                        TypeDropDownList.SelectedIndex = TypeDropDownList.Items.IndexOf(TypeDropDownList.Items.FindByText(TypeLabel.Text));
                    }
                }

                //Vendor
                Label LabelVendor = (Label)gridViewRow.FindControl("LabelVendor");
                LabelVendor.Visible = (!isSave);
                DropDownList DropDownListVendor = (DropDownList)gridViewRow.FindControl("DropDownListVendor");
                DropDownListVendor.Visible = isSave;
                if (isSave)
                {
                    DropDownListVendor.DataSource = vendorDetailsDataTable;
                    DropDownListVendor.DataValueField = "VendorID";
                    DropDownListVendor.DataTextField = "Vendor";
                    DropDownListVendor.DataBind();
                    if (TypeLabel.Text != "")
                    {
                        DropDownListVendor.SelectedIndex = DropDownListVendor.Items.IndexOf(DropDownListVendor.Items.FindByText(LabelVendor.Text));
                    }
                }

                //EmailID
                Label LabelEmailID = (Label)gridViewRow.FindControl("LabelEmailID");
                LabelEmailID.Visible = (!isSave);
                TextBox TextBoxEmailID = (TextBox)gridViewRow.FindControl("TextBoxEmailID");
                TextBoxEmailID.Visible = isSave;
                TextBoxEmailID.Text = LabelEmailID.Text;

                //IS SuperUser
                Label SuperUserLabel = (Label)gridViewRow.FindControl("SuperUserLabel");
                SuperUserLabel.Visible = (!isSave);
                CheckBox SuperUserCheckBox = (CheckBox)gridViewRow.FindControl("SuperUserCheckBox");
                SuperUserCheckBox.Visible = isSave;
                if (isSave)
                {
                    SuperUserCheckBox.Checked = false;
                    if (SuperUserLabel.Text == "1" || SuperUserLabel.Text == "True")
                    {
                        SuperUserCheckBox.Checked = true;
                    }
                }
            }
        }

        /// <summary>
        /// GetColumnNames
        /// </summary>
        private ArrayList GetColumnNames(string tableName)
        {
            return BO.UsersBO.GetColumnNames(tableName);
        }

        /// <summary>
        /// SetUserDisplay
        /// </summary>
        private void SetUserDisplay()
        {
            if (IsProjectSpecific)
            {
                SearchFilterPanel.Visible = false;
                GridViewUserDetails.Columns[8].Visible = false;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// ProductID
        /// </summary>
        [Browsable(true)]
        [Category("ProductID")]
        [Description("Gets and sets the ProductID")]
        public string ProductID
        {
            get
            {
                return LabelProductID.Text;
            }
            set
            {
                LabelProductID.Text = value;
            }
        }

        /// <summary>
        /// ProductVersionID
        /// </summary>
        [Browsable(true)]
        [Category("ProductVersionID")]
        [Description("Gets and sets the ProductVersionID")]
        public string ProductVersionID
        {
            get
            {
                return LabelProductVersionID.Text;
            }
            set
            {
                LabelProductVersionID.Text = value;
            }
        }

        /// <summary>
        /// IsProjectSpecific
        /// </summary>
        public bool IsProjectSpecific
        {
            get
            {
                return (Session["Identifier"].ToString() != WebConstants.DEFINE_PRODUCT_OWNER);
            }
        }

        #endregion
    }
}