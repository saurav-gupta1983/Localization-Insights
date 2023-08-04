using System;
using System.IO;
using System.Collections;
using System.Data;
using BO = Adobe.Localization.Insights.BusinessLayer;
using System.Windows.Forms;
using System.Threading;
using DTO = Adobe.Localization.Insights.Common.DataTransferObjects;

namespace Adobe.Localization.Insights.Web.UserControls
{
    public partial class DefineUserAccessUserControl : System.Web.UI.UserControl
    {
        #region Variables

        private DTO.TransferData transferData = new DTO.TransferData();

        #endregion

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateSections(transferData);
                ButtonSave.Enabled = false;
            }

            MessageLabel.Text = "";
        }

        /// <summary>
        /// PopulateSections
        /// </summary>
        private void PopulateSections(DTO.TransferData transferData)
        {
            DataSet sections = BO.UsersBO.GetSections(transferData);

            //sections.Tables[0].Rows.InsertAt(sections.Tables[0].NewRow(), 0);
            DropDownListSection.DataSource = sections.Tables[0];
            DropDownListSection.DataTextField = "Section";
            DropDownListSection.DataValueField = "SectionID";
            DropDownListSection.DataBind();
        }

        #region Button Events

        /// <summary>
        /// ButtonSave_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (TextBoxUserID.Text != "")
            {
                DTO.Users accessData = new DTO.Users();
                accessData.LoginID = TextBoxUserID.Text;
                accessData.UserID = BO.UsersBO.GetUserID(TextBoxUserID.Text).ToString();

                if (accessData.UserID != "")
                {
                    SaveAccessDetails(accessData);
                    MessageLabel.Text = "User Rights are updated successfully.";
                }
            }
        }

        /// <summary>
        /// SaveAccessDetails
        /// </summary>
        /// <param name="transferData"></param>
        private void SaveAccessDetails(DTO.Users accessData)
        {
            accessData.SectionID = DropDownListSection.SelectedValue;
            if (RadioButtonGeneral.Checked)
                accessData.RoleID = "GEN";
            if (RadioButtonView.Checked)
                accessData.RoleID = "VW";
            if (RadioButtonWrite.Checked)
                accessData.RoleID = "WR";
            if (RadioButtonReporting.Checked)
                accessData.RoleID = "REP";
            if (RadioButtonAdmin.Checked)
                accessData.RoleID = "ADMIN";
            if (RadioButtonAdminAndReport.Checked)
                accessData.RoleID = "ADMINREP";

            //BO.UsersBO.SaveAccessDetails(accessData);
        }

        /// <summary>
        /// ButtonSearch_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            DataSet userDetails = BO.UsersBO.GetUserDetails(TextBoxUserID.Text);
            if (userDetails.Tables[0].Rows.Count > 0)
            {
                transferData.UserID = userDetails.Tables[0].Rows[0]["UserID"].ToString();
                SetAccess();
                ButtonSave.Enabled = true;
            }
            else
            {
                MessageLabel.Text = "User doesn't exists";
                ButtonSave.Enabled = false;
            }
        }

        #endregion

        /// <summary>
        /// SetAccess
        /// </summary>
        /// <param name="transferData"></param>
        private void SetAccess()
        {
            transferData.LoginID = TextBoxUserID.Text;
            transferData.SectionID = DropDownListSection.SelectedValue;

            DataSet userRoles = GetUserRoles(transferData);

            if (userRoles.Tables[0].Rows.Count > 0)
            {
                RadioButtonGeneral.Checked = false;
                RadioButtonView.Checked = false;
                RadioButtonWrite.Checked = false;
                RadioButtonReporting.Checked = false;
                RadioButtonAdmin.Checked = false;
                RadioButtonAdminAndReport.Checked = false;

                if (userRoles.Tables[0].Select("RoleCode = 'GEN'").Length == 1)
                {
                    RadioButtonGeneral.Checked = true;
                }
                if (userRoles.Tables[0].Select("RoleCode = 'VW'").Length == 1)
                {
                    RadioButtonView.Checked = true;
                }
                if (userRoles.Tables[0].Select("RoleCode = 'WR'").Length == 1)
                {
                    RadioButtonWrite.Checked = true;
                }
                if (userRoles.Tables[0].Select("RoleCode = 'REP'").Length == 1)
                {
                    RadioButtonReporting.Checked = true;
                }
                if (userRoles.Tables[0].Select("RoleCode = 'ADMIN'").Length == 1)
                {
                    RadioButtonAdmin.Checked = true;
                }
                if (userRoles.Tables[0].Select("RoleCode = 'ADMINREP'").Length == 1)
                {
                    RadioButtonAdminAndReport.Checked = true;
                }
            }
        }

        /// <summary>
        /// GetUserRoles
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private DataSet GetUserRoles(DTO.TransferData userRights)
        {
            //return BO.UsersBO.GetUserProjectRoles(userRights);

            return null;
        }

        /// <summary>
        /// DropDownListSection_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListSection_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            SetAccess();
        }
    }
}