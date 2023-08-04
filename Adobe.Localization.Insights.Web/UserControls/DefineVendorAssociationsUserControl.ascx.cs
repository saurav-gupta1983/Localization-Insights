using System;
using System.Data;
using System.Collections;
using System.Web.UI.WebControls;
using Adobe.Localization.Insights.Common;
using BO = Adobe.Localization.Insights.BusinessLayer;
using DTO = Adobe.Localization.Insights.Common.DataTransferObjects;

namespace Adobe.Localization.Insights.Web.UserControls
{
    public partial class DefineVendorAssociationsUserControl : System.Web.UI.UserControl
    {
        #region Variables

        private DataTable userDetailsDataTable = new DataTable();
        private DataTable vendorDetailsDataTable = new DataTable();
        private DataTable userVendorDetailsDataTable = new DataTable();
        private DataTable typeDetailsDataTable = new DataTable();
        private int colID = 0;
        private ArrayList columnNames;
        
        #endregion

        #region Page Load Event

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                ViewState["columnNames"] = BO.UsersBO.GetColumnNames("Users");
                PopulateData();
                PopulateVendor();
            }
            else
            {
                columnNames = (ArrayList)ViewState["columnNames"];
            }

            typeDetailsDataTable = (DataTable)ViewState["TypeDetailsDataTable"];
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
            GetUserVendorAssociations();
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
            GetUserVendorAssociations();
            if (!(userVendorDetailsDataTable.Rows[e.NewPageIndex * 10][colID].ToString() == "0" || userVendorDetailsDataTable.Rows[e.NewPageIndex * 10][colID].ToString() == ""))
            {
                DataRow dr = userVendorDetailsDataTable.NewRow();
                dr[colID] = 0;
                userVendorDetailsDataTable.Rows.InsertAt(dr, e.NewPageIndex * 10);
            }
            GridViewUserDetails.DataSource = userVendorDetailsDataTable;
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
                    Label TypeLabel = (Label)e.Row.FindControl("TypeLabel");
                    if (TypeLabel.Text != "")
                    {
                        TypeLabel.Text = typeDetailsDataTable.Select("VendorID= " + TypeLabel.Text)[0]["Vendor"].ToString();
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
        /// LinkButtonSaveDetails_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonSaveDetails_Click(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow detailsGridViewRow = GridViewUserDetails.Rows[index];

            DTO.MasterData masterData = new DTO.MasterData();
            masterData.TableName = "Users";
            // masterData.ColumnNames = columnNames;

            TextBox CodeTextBox = (TextBox)detailsGridViewRow.FindControl("CodeTextBox");
            TextBox DescriptionTextBox = (TextBox)detailsGridViewRow.FindControl("DescriptionTextBox");
            DropDownList TypeDropDownList = (DropDownList)detailsGridViewRow.FindControl("TypeDropDownList");
            DataSet dsUser;

            masterData.Description = DescriptionTextBox.Text;
            if (CodeTextBox.Text == "")
            {
                LabelMessage.Text = "Please provide User ID";
                return;
            }
            else
            {
                dsUser = BO.UsersBO.GetUserDetails(CodeTextBox.Text);

                if (dsUser.Tables[0].Rows.Count > 0)
                {
                    DTO.TransferData transferData = new DTO.TransferData();
                    transferData.UserID = dsUser.Tables[0].Rows[0]["UserID"].ToString();
                    if (BO.UsersBO.GetVendorDetails(transferData).Tables[0].Rows.Count > 0)
                    {
                        LabelMessage.Text = "User Association already exists.";
                        return;
                    }
                }
                else
                {
                    LabelMessage.Text = "User doesn't exist.";
                    return;
                }
            }
            masterData.Code = dsUser.Tables[0].Rows[0]["UserID"].ToString();
            masterData.Type = TypeDropDownList.SelectedValue;

            if (detailsGridViewRow.Cells[colID].Text != "")
            {
                Label IDLabel = (Label)detailsGridViewRow.FindControl("IDLabel");
                masterData.MasterDataID = IDLabel.Text;
            }

            if (BO.UsersBO.AddUpdateUserVendorAssociationsforMasterData(masterData))
            {
                LabelMessage.Text = WebConstants.SAVED_SUCCESS;
            }
            else
            {
                LabelMessage.Text = WebConstants.FAILURE;
            }

            PopulateData();
        }

        /// <summary>
        /// LinkButtonCancelDetails_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonCancelDetails_Click(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow detailsGridViewRow = GridViewUserDetails.Rows[index];

            if (detailsGridViewRow.Cells[colID].Text == "")
            {
                TextBox DescriptionTextBox = (TextBox)detailsGridViewRow.FindControl("DescriptionTextBox");
                DescriptionTextBox.Text = "";
                TextBox CodeTextBox = (TextBox)detailsGridViewRow.FindControl("CodeTextBox");
                CodeTextBox.Text = "";
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
            setLinkButtons(GridViewUserDetails.Rows[Convert.ToInt32(e.CommandArgument)], true);
        }

        /// <summary>
        /// LinkButtonDeleteDetails_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonDeleteDetails_Click(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow detailsGridViewRow = GridViewUserDetails.Rows[index];

            DTO.MasterData masterData = new DTO.MasterData();
            masterData.TableName = "Users";
            masterData.ColumnNames = columnNames;

            Label IDLabel = (Label)detailsGridViewRow.FindControl("IDLabel");
            masterData.MasterDataID = IDLabel.Text;

            if (BO.UsersBO.AddUpdateUserDetailsforMasterData(masterData))
            {
                LabelMessage.Text = WebConstants.DELETED_SUCCESS;
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
            //DTO.MasterData masterData = new DTO.MasterData();
            //masterData.TableName = "Users";
            //userDetailsDataTable = BO.UsersBO.GetDetailsforMasterData(masterData).Tables[0];

            PopulateTypeDetails();
            GetUserVendorAssociations();

        }

        /// <summary>
        /// PopulateTypeDetails
        /// </summary>
        private void PopulateTypeDetails()
        {
            DTO.MasterData masterData = new DTO.MasterData();
            masterData.TableName = "Vendors";

            typeDetailsDataTable = BO.UsersBO.GetDetailsforMasterData(masterData).Tables[0];

            vendorDetailsDataTable = typeDetailsDataTable.Copy();
            DropDownListVendor.DataSource = typeDetailsDataTable;

            typeDetailsDataTable.Rows.RemoveAt(0);
            ViewState["TypeDetailsDataTable"] = typeDetailsDataTable;
        }

        /// <summary>
        /// PopulateVendor
        /// </summary>
        private void PopulateVendor()
        {
            vendorDetailsDataTable.Rows[0]["Vendor"] = "All";

            DropDownListVendor.DataSource = vendorDetailsDataTable;
            DropDownListVendor.DataTextField = "Vendor";
            DropDownListVendor.DataValueField = "VendorID";
            DropDownListVendor.DataBind();
        }

        /// <summary>
        /// GetUserVendorAssociations
        /// </summary>
        private void GetUserVendorAssociations()
        {
            DTO.MasterData masterData = new DTO.MasterData();
            masterData.Description = TextBoxUser.Text;
            masterData.MasterDataID = DropDownListVendor.SelectedValue;
            userVendorDetailsDataTable = BO.UsersBO.GetUserVendorAssociations(masterData).Tables[0];
            GridViewUserDetails.DataSource = userVendorDetailsDataTable;
            GridViewUserDetails.DataBind();
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

            //Description
            Label DescriptionLabel = (Label)gridViewRow.FindControl("DescriptionLabel");
            DescriptionLabel.Visible = (!isSave);
            TextBox DescriptionTextBox = (TextBox)gridViewRow.FindControl("DescriptionTextBox");
            DescriptionTextBox.Visible = isSave;
            DescriptionTextBox.Text = DescriptionLabel.Text;

            //Code
            Label CodeLabel = (Label)gridViewRow.FindControl("CodeLabel");
            CodeLabel.Visible = (!isSave);
            TextBox CodeTextBox = (TextBox)gridViewRow.FindControl("CodeTextBox");
            CodeTextBox.Visible = isSave;
            CodeTextBox.Text = CodeLabel.Text;

            //Type
            Label TypeLabel = (Label)gridViewRow.FindControl("TypeLabel");
            TypeLabel.Visible = (!isSave);
            DropDownList TypeDropDownList = (DropDownList)gridViewRow.FindControl("TypeDropDownList");
            TypeDropDownList.Visible = isSave;
            if (isSave)
            {
                TypeDropDownList.DataSource = typeDetailsDataTable;
                TypeDropDownList.DataValueField = "VendorID";
                TypeDropDownList.DataTextField = "Vendor";
                TypeDropDownList.DataBind();
                if (TypeLabel.Text != "")
                {
                    TypeDropDownList.SelectedIndex = TypeDropDownList.Items.IndexOf(TypeDropDownList.Items.FindByText(TypeLabel.Text));
                }
            }

        }

        #endregion
    }
}