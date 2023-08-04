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
    public partial class DefineProductOwnersUserControl : System.Web.UI.UserControl
    {
        #region Variables

        private DataTable projectRoleDetailsDataTable = new DataTable();
        private DataTable userDetailsDataTable = new DataTable();
        private DataTable productOwnerDataTable = new DataTable();
        private int colID = 0;
        private string tableName = "ProductOwner";
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
                ViewState["columnNames"] = BO.UsersBO.GetColumnNames(tableName);
                PopulateProduct();
                PopulateGridDropDownListData();
                PopulateData();
            }
            else
            {
                columnNames = (ArrayList)ViewState["columnNames"];
                projectRoleDetailsDataTable = (DataTable)ViewState["ProjectRoleDetailsDataTable"];
                userDetailsDataTable = (DataTable)ViewState["UserDetailsDataTable"];
            }
            SetUserDisplay();
            LabelMessage.Text = "";
        }

        #endregion

        #region Grid Events

        /// <summary>
        /// GridViewProductOwnerDetails_RowDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewProductOwnerDetails_RowDataBound(object sender, GridViewRowEventArgs e)
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
                    Label LabelProjectRoleID = (Label)e.Row.FindControl("LabelProjectRoleID");
                    if (LabelProjectRoleID.Text != "")
                    {
                        LabelProjectRoleID.Text = projectRoleDetailsDataTable.Select("ProjectRoleID= " + LabelProjectRoleID.Text)[0]["ProjectRole"].ToString();
                    }

                    Label LabelUserID = (Label)e.Row.FindControl("LabelUserID");
                    if (LabelUserID.Text != "")
                    {
                        LabelUserID.Text = userDetailsDataTable.Select("UserID= " + LabelUserID.Text)[0]["UserNameID"].ToString();
                    }
                }
            }
        }

        /// <summary>
        /// GridViewProductOwnerDetails_RowUpdating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewProductOwnerDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
        }

        /// <summary>
        /// GridViewProductOwnerDetails_RowCancelingEdit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewProductOwnerDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
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
            GridViewRow detailsGridViewRow = GridViewProductOwnerDetails.Rows[index];

            PopulateProductVersion();

            DTO.Product productData = new DTO.Product();
            productData.ProductID = DropDownListProduct.SelectedValue;

            DropDownList DropDownListUserID = (DropDownList)detailsGridViewRow.FindControl("DropDownListUserID");
            productData.UserID = DropDownListUserID.SelectedValue;

            if (detailsGridViewRow.Cells[colID].Text != "")
            {
                Label IDLabel = (Label)detailsGridViewRow.FindControl("IDLabel");
                productData.UserProductID = IDLabel.Text;
            }

            if (LabelVersionID.Text == "-1")
            {
                productData.ProductVersion = TextBoxVersion.Text;
                productData.ProductVersionID = "";
                if (BO.UsersBO.AddUpdateProductVersion(productData))
                {
                    LabelVersionID.Text = BO.UsersBO.GetProductVersion(productData).Tables[0].Rows[0]["ProductVersionID"].ToString();
                }
                else
                {
                    LabelMessage.Text = WebConstants.FAILURE;
                    return;
                }
            }

            productData.ProductID = "";
            productData.ProductVersionID = LabelVersionID.Text;
            productData.IsOwner = true;

            productOwnerDataTable = BO.UsersBO.GetProductUsers(productData).Tables[0];
            if (productOwnerDataTable.Select("UserID = " + productData.UserID).Length > 0)
            {
                LabelMessage.Text = "This user is already an owner for this Product.";
                //PopulateData();
                return;
            }
            else
            {
                if (BO.UsersBO.AddUpdateProductOwnerforMasterData(productData))
                {
                    LabelMessage.Text = WebConstants.SAVED_SUCCESS;
                }
                else
                {
                    LabelMessage.Text = WebConstants.FAILURE;
                }
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
            GridViewRow detailsGridViewRow = GridViewProductOwnerDetails.Rows[index];

            if (detailsGridViewRow.Cells[colID].Text == "")
            {
                //TextBox DescriptionTextBox = (TextBox)detailsGridViewRow.FindControl("DescriptionTextBox");
                //DescriptionTextBox.Text = "";
                //TextBox CodeTextBox = (TextBox)detailsGridViewRow.FindControl("CodeTextBox");
                //CodeTextBox.Text = "";
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
            setLinkButtons(GridViewProductOwnerDetails.Rows[Convert.ToInt32(e.CommandArgument)], true);
        }

        /// <summary>
        /// LinkButtonDeleteDetails_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonDeleteDetails_Click(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow detailsGridViewRow = GridViewProductOwnerDetails.Rows[index];

            DTO.Product productData = new DTO.Product();

            Label IDLabel = (Label)detailsGridViewRow.FindControl("IDLabel");
            productData.UserProductID = IDLabel.Text;

            if (BO.UsersBO.AddUpdateProductOwnerforMasterData(productData))
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
            PopulateProductVersion();

            DTO.Product productData = new DTO.Product();
            productData.ProductVersionID = LabelVersionID.Text;
            productData.IsOwner = true;

            productOwnerDataTable = BO.UsersBO.GetProductUsers(productData).Tables[0];
            GridViewProductOwnerDetails.DataSource = productOwnerDataTable;
            GridViewProductOwnerDetails.DataBind();
        }

        /// <summary>
        /// PopulateGridDropDownListData
        /// </summary>
        private void PopulateGridDropDownListData()
        {
            DTO.MasterData masterData = new DTO.MasterData();
            masterData.TableName = "ProjectRoles";
            projectRoleDetailsDataTable = BO.UsersBO.GetDetailsforMasterData(masterData).Tables[0];
            ViewState["ProjectRoleDetailsDataTable"] = projectRoleDetailsDataTable;

            userDetailsDataTable = BO.UsersBO.GetUserDetails("").Tables[0];
            ViewState["UserDetailsDataTable"] = userDetailsDataTable;
        }

        /// <summary>
        /// PopulateProduct
        /// </summary>
        private void PopulateProduct()
        {
            DataSet products = BO.UsersBO.GetProducts(new DTO.TransferData());

            DropDownListProduct.DataSource = products.Tables[0];
            DropDownListProduct.DataValueField = "ProductID";
            DropDownListProduct.DataTextField = "Product";
            DropDownListProduct.DataBind();
        }

        /// <summary>
        /// PopulateUserIDDropDownList
        /// </summary>
        private void PopulateUserIDDropDownList(DropDownList dropDownListUserID, string projectRoleID)
        {
            DataRow[] dataRows = userDetailsDataTable.Select("ProjectRoleID = " + projectRoleID);
            DataTable userTable = userDetailsDataTable.Clone();

            foreach (DataRow dr in dataRows)
            {
                DataRow newdr = userTable.NewRow();
                newdr["UserID"] = dr["UserID"];
                newdr["UserNameID"] = dr["UserNameID"];

                userTable.Rows.Add(newdr);
            }

            dropDownListUserID.DataSource = userTable;
            dropDownListUserID.DataValueField = "UserID";
            dropDownListUserID.DataTextField = "UserNameID";
            dropDownListUserID.DataBind();
        }

        /// <summary>
        /// PopulateProductVersion
        /// </summary>
        private void PopulateProductVersion()
        {
            DTO.Product productData = new DTO.Product();
            productData.ProductID = DropDownListProduct.SelectedValue.ToString();
            DataSet productVersion = BO.UsersBO.GetProductVersion(productData);

            DataRow[] drproductVersion = productVersion.Tables[0].Select();
            if (drproductVersion.Length == 0)
            {
                TextBoxVersion.Text = DropDownListProduct.SelectedItem.Text + "_Default";
                LabelVersionID.Text = "-1";
                TextBoxVersion.Enabled = true;
            }
            else
            {
                TextBoxVersion.Text = drproductVersion[drproductVersion.Length - 1]["ProductVersion"].ToString();
                LabelVersionID.Text = drproductVersion[drproductVersion.Length - 1]["ProductVersionID"].ToString();
                TextBoxVersion.Enabled = false;
            }
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

            //ProjectRoleID
            Label LabelProjectRoleID = (Label)gridViewRow.FindControl("LabelProjectRoleID");
            LabelProjectRoleID.Visible = (!isSave);
            DropDownList DropDownListProjectRoleID = (DropDownList)gridViewRow.FindControl("DropDownListProjectRoleID");
            DropDownListProjectRoleID.Visible = isSave;
            if (isSave)
            {
                DropDownListProjectRoleID.DataSource = projectRoleDetailsDataTable;
                DropDownListProjectRoleID.DataValueField = "ProjectRoleID";
                DropDownListProjectRoleID.DataTextField = "ProjectRole";
                DropDownListProjectRoleID.DataBind();
                if (LabelProjectRoleID.Text != "")
                {
                    DropDownListProjectRoleID.SelectedIndex = DropDownListProjectRoleID.Items.IndexOf(DropDownListProjectRoleID.Items.FindByText(LabelProjectRoleID.Text));
                }
            }

            //UserID
            Label LabelUserID = (Label)gridViewRow.FindControl("LabelUserID");
            LabelUserID.Visible = (!isSave);
            DropDownList DropDownListUserID = (DropDownList)gridViewRow.FindControl("DropDownListUserID");
            DropDownListUserID.Visible = isSave;
            if (isSave)
            {
                PopulateUserIDDropDownList(DropDownListUserID, DropDownListProjectRoleID.SelectedValue);
                if (LabelUserID.Text != "")
                {
                    DropDownListUserID.SelectedIndex = DropDownListUserID.Items.IndexOf(DropDownListUserID.Items.FindByText(LabelUserID.Text));
                }
            }
        }

        /// <summary>
        /// SetUserDisplay
        /// </summary>
        private void SetUserDisplay()
        {
            if (Session["Identifier"].ToString() != WebConstants.DEFINE_PRODUCT_OWNER)
            {
                LabelProduct.Visible = false;
                DropDownListProduct.Visible = false;
                LabelProductVersion.Visible = false;
                TextBoxVersion.Visible = false;
                PanelDisplayProduct.Visible = false;
            }
        }

        #endregion

        #region DropDownList events

        /// <summary>
        /// DropDownListProduct_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListProduct_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateData();
        }

        /// <summary>
        /// DropDownListProjectRoleID_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListProjectRoleID_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList projectRoleIDDropDownList = (DropDownList)sender;
            GridViewRow row = (GridViewRow)projectRoleIDDropDownList.NamingContainer;
            if (row != null)
            {
                DropDownList userIDDropDownList = (DropDownList)row.FindControl("DropDownListUserID");
                {
                    PopulateUserIDDropDownList(userIDDropDownList, projectRoleIDDropDownList.SelectedValue);
                }
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
                return DropDownListProduct.SelectedValue;
            }
            set
            {
                DropDownListProduct.SelectedIndex = DropDownListProduct.Items.IndexOf(DropDownListProduct.Items.FindByValue(value));
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
                return LabelVersionID.Text;
            }
            set
            {
                LabelVersionID.Text = value;
            }
        }

        #endregion
    }
}