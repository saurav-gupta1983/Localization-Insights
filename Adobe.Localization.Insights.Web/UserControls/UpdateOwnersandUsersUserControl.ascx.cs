using System;
using System.Data;
using System.Collections;
using System.Web.UI.WebControls;
using Adobe.Localization.Insights.Common;
using BO = Adobe.Localization.Insights.BusinessLayer;
using DTO = Adobe.Localization.Insights.Common.DataTransferObjects;

namespace Adobe.Localization.Insights.Web.UserControls
{
    public partial class UpdateOwnersandUsersUserControl : System.Web.UI.UserControl
    {
        #region Variables

        #endregion

        #region Page Load Event

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            PopulateProductVersion();
            SetUserControlProperties();
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// PopulateProductVersion
        /// </summary>
        private void PopulateProductVersion()
        {
            DTO.Product productData = new DTO.Product();
            productData.ProductID = Session["ProductID"].ToString();
            productData.IsActive = "1";

            DataSet productVersion = BO.UsersBO.GetProductVersion(productData);

            LabelProductValue.Text = productVersion.Tables[0].Rows[0]["Product"].ToString();

            DropDownListProductVersion.DataSource = productVersion;
            DropDownListProductVersion.DataTextField = "ProductVersion";
            DropDownListProductVersion.DataValueField = "ProductVersionID";
            DropDownListProductVersion.DataBind();
        }

        /// <summary>
        /// SetUserControlProperties
        /// </summary>
        private void SetUserControlProperties()
        {
            DefineProductOwners.ProductID = Session["ProductID"].ToString();
            DefineProductOwners.ProductVersionID = DropDownListProductVersion.SelectedValue;

            //AddUpdateUsers.ProductID = Session["ProductID"].ToString();
            //AddUpdateUsers.ProductVersionID = DropDownListProductVersion.SelectedValue;
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
            SetUserControlProperties();
        }

        #endregion
    }
}