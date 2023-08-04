using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Web.UI.WebControls;
using Adobe.Localization.Insights.Common;
using COM = Adobe.Localization.Insights.Common.Common;
using BO = Adobe.Localization.Insights.BusinessLayer;
using DTO = Adobe.Localization.Insights.Common.DataTransferObjects;

namespace Adobe.Localization.Insights.Web.UserControls
{
    /// <summary>
    /// MasterDataUserControl
    /// </summary>
    public partial class MasterDataUserControl : System.Web.UI.UserControl
    {
        #region Variables

        private bool isReadOnly = true;

        private DataTable dtScreenLabels;
        private DataTable dtMasterDetails = new DataTable();
        private DataTable dtTypeDetails = new DataTable();
        private DTO.Header dataHeader = new DTO.Header();

        private string tableName = "";
        private string typeTableName = "";
        private string filter = "";
        private bool isCode = false;
        private bool isType = false;
        private bool isTypeCode = false;
        private bool isSubScreen = false;
        private ArrayList columnNames;

        private string header;
        private string screenIdentifier;

        private const int CONST_ZERO = 0;
        private const int COL_CODE_NO = 2;
        private const int COL_DESCRIPTION_NO = 3;
        private const int COL_TYPE_NO = 4;
        private const int COL_SAVE_UPDATE_NO = 5;
        private const int COL_CANCEL_DELETE_NO = 6;
        private const string STR_SPACE = "&nbsp;";
        private const int NUM_EACH_GRID_ROW_DISPLAY_HEIGHT = 20;

        #endregion

        #region Page Load Event

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            GetScreenMasterDataInformation();
            SetScreenLabels();

            if (!this.IsPostBack)
            {
                ViewState[WebConstants.COL_STR_COLUMN_NAMES] = GetColumnNames(tableName);
                PopulateData();
            }

            columnNames = (ArrayList)ViewState[WebConstants.COL_STR_COLUMN_NAMES];
            dtTypeDetails = (DataTable)ViewState[WebConstants.COL_STR_TYPE_TABLE_DETAILS];

            SetScreenAccess();
        }

        #endregion

        #region Grid Events

        /// <summary>
        /// GridViewMasterDataDetails_RowDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewMasterDataDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                SetGridLinkButtonsDisplayText(e.Row);

                if (e.Row.Cells[CONST_ZERO].Text.ToString() == CONST_ZERO.ToString() || e.Row.Cells[CONST_ZERO].Text.ToString() == STR_SPACE)
                {
                    e.Row.Cells[CONST_ZERO].Text = "";
                    SetLinkButtons(e.Row, true);
                }
                else
                {
                    if (isType)
                    {
                        Label LabelType = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_TYPE);
                        LabelType.Text = dtTypeDetails.Select(WebConstants.COL_ID + " = '" + LabelType.Text + "'")[0][WebConstants.COL_DESCRIPTION].ToString();
                    }
                }
            }
        }

        /// <summary>
        /// GridViewMasterDataDetails_RowUpdating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewMasterDataDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
        }

        /// <summary>
        /// GridViewMasterDataDetails_RowCancelingEdit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewMasterDataDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
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
            GridViewRow detailsGridViewRow = GridViewMasterDataDetails.Rows[index];

            dataHeader.LoginID = Session[WebConstants.SESSION_LOGIN_ID].ToString();

            DTO.MasterData masterData = new DTO.MasterData();
            masterData.DataHeader = dataHeader;
            masterData.TableName = tableName;
            masterData.ColumnNames = columnNames;

            TextBox TextBoxCode = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_CODE);
            TextBox TextBoxDescription = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_DESCRIPTION);
            DropDownList DropDownListType = (DropDownList)detailsGridViewRow.FindControl(WebConstants.CONTROL_DROPDOWNLIST_TYPE);

            if (isCode)
            {
                if (TextBoxCode.Text == "")
                {
                    if (isSubScreen)
                        LabelMessage.Text = string.Format(COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.SCREEN_CODE_MANDATORY), header);
                    else
                        LabelMessage.Text = string.Format(COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.SCREEN_CODE_MANDATORY), header);
                    return;
                }
                masterData.Code = TextBoxCode.Text;
            }
            else
            {
                if (TextBoxDescription.Text == "")
                {
                    if (isSubScreen)
                        LabelMessage.Text = string.Format(COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.SCREEN_DESCRIPTION_MANDATORY), header);
                    else
                        LabelMessage.Text = string.Format(COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.SCREEN_DESCRIPTION_MANDATORY), header);
                    return;
                }
            }
            masterData.Description = TextBoxDescription.Text;

            if (isType)
            {
                masterData.Type = DropDownListType.SelectedValue;
                if (LabelTypeValue.Text != "")
                {
                    masterData.Type = LabelTypeValue.Text;
                }
            }

            if (detailsGridViewRow.Cells[CONST_ZERO].Text != "")
            {
                Label LabelID = (Label)detailsGridViewRow.FindControl(WebConstants.CONTROL_LABEL_ID);
                masterData.MasterDataID = LabelID.Text;
            }

            if (BO.BusinessObjects.AddUpdateDetailsforMasterData(masterData))
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_SUCCESS_SAVE_MESSAGE);
            else
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_SAVE_MESSAGE);

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
            GridViewRow detailsGridViewRow = GridViewMasterDataDetails.Rows[index];

            if (detailsGridViewRow.Cells[CONST_ZERO].Text == "")
            {
                TextBox TextBoxDescription = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_DESCRIPTION);
                TextBoxDescription.Text = "";

                if (isCode)
                {
                    TextBox TextBoxCode = (TextBox)detailsGridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_CODE);
                    TextBoxCode.Text = "";
                }
            }
            else
            {
                SetLinkButtons(detailsGridViewRow, false);
            }
        }

        /// <summary>
        /// LinkButtonUpdateDetails_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonUpdateDetails_Click(object sender, CommandEventArgs e)
        {
            SetLinkButtons(GridViewMasterDataDetails.Rows[Convert.ToInt32(e.CommandArgument)], true);
        }

        /// <summary>
        /// LinkButtonDeleteDetails_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonDeleteDetails_Click(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow detailsGridViewRow = GridViewMasterDataDetails.Rows[index];

            DTO.MasterData masterData = new DTO.MasterData();
            masterData.TableName = tableName;
            masterData.ColumnNames = columnNames;

            Label LabelID = (Label)detailsGridViewRow.FindControl(WebConstants.CONTROL_LABEL_ID);
            masterData.MasterDataID = LabelID.Text;

            if (BO.BusinessObjects.AddUpdateDetailsforMasterData(masterData))
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_SUCCESS_DELETE_MESSAGE);
            else
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_DELETE_MESSAGE); ;

            PopulateData();
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// GetScreenMasterDataInformation
        /// </summary>
        private void GetScreenMasterDataInformation()
        {
            if (Session[WebConstants.SESSION_IDENTIFIER] != null)
                screenIdentifier = Session[WebConstants.SESSION_IDENTIFIER].ToString();
            else if (Session[WebConstants.SESSION_IDENTIFIER_TEMP] != null)
                screenIdentifier = Session[WebConstants.SESSION_IDENTIFIER_TEMP].ToString();

            DTO.Screens screenData = new DTO.Screens();
            screenData.ScreenIdentifier = screenIdentifier;

            DataRow drMasterData = BO.BusinessObjects.GetMasterScreensDetails(screenData).Tables[0].Rows[0];

            tableName = drMasterData[WebConstants.COL_TABLE_NAME].ToString();
            isCode = Convert.ToBoolean(drMasterData[WebConstants.COL_IS_CODE]);
            isType = Convert.ToBoolean(drMasterData[WebConstants.COL_IS_TYPE]);
            typeTableName = drMasterData[WebConstants.COL_TYPE_TABLE_NAME].ToString();
            isTypeCode = Convert.ToBoolean(drMasterData[WebConstants.COL_IS_TYPE_CODE]);
            isSubScreen = Convert.ToBoolean(drMasterData[WebConstants.COL_IS_SUB_SCREEN]);
            filter = drMasterData[WebConstants.COL_MASTER_DATA_FILTER].ToString();

            GetScreenAccess();
        }

        /// <summary>
        /// PopulateData
        /// </summary>
        private void PopulateData()
        {
            GetMasterDetails();

            if (typeTableName != "")
            {
                PopulateTypeDetails();
            }

            GridViewMasterDataDetails.DataSource = dtMasterDetails;
            GridViewMasterDataDetails.DataBind();
        }

        /// <summary>
        /// PopulateTypeDetails
        /// </summary>
        private void PopulateTypeDetails()
        {
            DTO.MasterData masterData = new DTO.MasterData();
            masterData.TableName = typeTableName;
            masterData.Type = LabelTypeValue.Text;

            if (isSubScreen && LabelTypeValue.Text != "")
                masterData.Filter = filter;

            dtTypeDetails = BO.BusinessObjects.GetDetailsforMasterData(masterData).Tables[0];

            dtTypeDetails.Rows.RemoveAt(0);
            if (isTypeCode)
            {
                dtTypeDetails.Columns.RemoveAt(1);
            }

            dtTypeDetails.Columns[0].ColumnName = WebConstants.COL_ID;
            dtTypeDetails.Columns[1].ColumnName = WebConstants.COL_DESCRIPTION;

            ViewState[WebConstants.COL_STR_TYPE_TABLE_DETAILS] = dtTypeDetails;
        }

        /// <summary>
        /// GetMasterDetails
        /// </summary>
        private void GetMasterDetails()
        {
            DTO.MasterData masterData = new DTO.MasterData();
            masterData.TableName = tableName;
            masterData.ColumnNames = columnNames;
            masterData.Type = LabelTypeValue.Text;

            if (isSubScreen && LabelTypeValue.Text != "")
                masterData.Filter = filter;

            dtMasterDetails = BO.BusinessObjects.GetDetailsforMasterData(masterData).Tables[0];

            int descriptionColNo = 2;
            int typeColNo = 3;
            dtMasterDetails.Columns[0].ColumnName = WebConstants.COL_ID;
            if (!isCode)
            {
                dtMasterDetails.Columns.Add(WebConstants.COL_CODE, Type.GetType(WebConstants.STR_SYSTEM_STRING));
                descriptionColNo = 1;
                typeColNo = 2;
            }
            else
                dtMasterDetails.Columns[1].ColumnName = WebConstants.COL_CODE;

            if (!isType)
            {
                dtMasterDetails.Columns.Add(WebConstants.COL_TYPE, Type.GetType(WebConstants.STR_SYSTEM_STRING));
            }
            else
            {
                dtMasterDetails.Columns[typeColNo].ColumnName = WebConstants.COL_TYPE;
            }
            dtMasterDetails.Columns[descriptionColNo].ColumnName = WebConstants.COL_DESCRIPTION;

            if (isReadOnly)
                dtMasterDetails.Rows.RemoveAt(0);
        }

        /// <summary>
        /// SetLinkButtons
        /// </summary>
        /// <param name="gridViewRow"></param>
        /// <param name="isSave"></param>
        private void SetLinkButtons(GridViewRow gridViewRow, bool isSave)
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
            Label LabelDescription = (Label)gridViewRow.FindControl(WebConstants.CONTROL_LABEL_DESCRIPTION);
            LabelDescription.Visible = (!isSave);
            TextBox TextBoxDescription = (TextBox)gridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_DESCRIPTION);
            TextBoxDescription.Visible = isSave;
            TextBoxDescription.Text = LabelDescription.Text;

            //Code
            if (isCode)
            {
                Label LabelCode = (Label)gridViewRow.FindControl(WebConstants.CONTROL_LABEL_CODE);
                LabelCode.Visible = (!isSave);
                TextBox TextBoxCode = (TextBox)gridViewRow.FindControl(WebConstants.CONTROL_TEXTBOX_CODE);
                TextBoxCode.Visible = isSave;
                TextBoxCode.Text = LabelCode.Text;
            }

            //Type
            if (isType)
            {
                Label LabelType = (Label)gridViewRow.FindControl(WebConstants.CONTROL_LABEL_TYPE);
                LabelType.Visible = (!isSave);
                DropDownList DropDownListType = (DropDownList)gridViewRow.FindControl(WebConstants.CONTROL_DROPDOWNLIST_TYPE);
                DropDownListType.Visible = isSave;

                if (isSave)
                {
                    DropDownListType.DataSource = dtTypeDetails;
                    DropDownListType.DataValueField = WebConstants.COL_ID;
                    DropDownListType.DataTextField = WebConstants.COL_DESCRIPTION;
                    DropDownListType.DataBind();
                    if (LabelType.Text != "")
                    {
                        DropDownListType.SelectedIndex = DropDownListType.Items.IndexOf(DropDownListType.Items.FindByText(LabelType.Text));
                    }
                }
            }

        }

        /// <summary>
        /// GetColumnNames
        /// </summary>
        private ArrayList GetColumnNames(string tableName)
        {
            columnNames = BO.BusinessObjects.GetColumnNames(tableName);
            return columnNames;
        }

        #endregion

        #region Screen Access and Labels

        /// <summary>
        /// GetScreenAccess
        /// </summary>
        private void GetScreenAccess()
        {
            if (Session[WebConstants.SESSION_CACHE_SCREEN_ACCESS_DATA_TABLE] != null)
            {
                DataTable dtScreenAccess = (DataTable)Session[WebConstants.SESSION_CACHE_SCREEN_ACCESS_DATA_TABLE];
                DataRow[] drScreenAccess = dtScreenAccess.Select(WebConstants.COL_SCREEN_IDENTIFIER + " = '" + screenIdentifier + "'");

                if (drScreenAccess.Length == 1)
                {
                    if (drScreenAccess[0][WebConstants.COL_SCREEN_IS_READ_WRITE].ToString() == WebConstants.DEF_VAL_ACCESS_CHECKED)
                        isReadOnly = false;
                }
            }

        }

        /// <summary>
        /// SetScreenAccess
        /// </summary>
        private void SetScreenAccess()
        {
            if (isReadOnly)
            {
                GridViewMasterDataDetails.Columns[COL_SAVE_UPDATE_NO].Visible = false;
                GridViewMasterDataDetails.Columns[COL_CANCEL_DELETE_NO].Visible = false;
            }

            if (isSubScreen)
            {
                GridViewMasterDataDetails.Columns[COL_CODE_NO].Visible = true;
                GridViewMasterDataDetails.Columns[COL_TYPE_NO].Visible = false;
                AddFooterPadding(15);
            }
            else
                AddFooterPadding(GridViewMasterDataDetails.Rows.Count);
        }

        /// <summary>
        /// PopulateScreenLabels
        /// </summary>
        /// <param name="userID"></param>
        private void PopulateScreenLabels()
        {
            DTO.Screens screenData = new DTO.Screens();
            screenData.ScreenIdentifier = screenIdentifier;
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

            LabelHeader.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, screenIdentifier, WebConstants.SCREEN_HEADER_MASTER_DATA);

            if (isSubScreen)
            {
                //LabelHeader.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.SCREEN_GRID_HEADER_MASTER_DATA);
                LabelHeader.Visible = false;
                foreach (DataControlField field in GridViewMasterDataDetails.Columns)
                    if (field.HeaderText != "")
                        field.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, field.HeaderText);
            }
            else
            {
                header = COM.GetScreenLocalizedLabel(dtScreenLabels, screenIdentifier, WebConstants.SCREEN_GRID_HEADER_MASTER_DATA);

                GridViewMasterDataDetails.Columns[CONST_ZERO].HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.SCREEN_SNO);

                if (isCode)
                {
                    GridViewMasterDataDetails.Columns[COL_CODE_NO].Visible = true;
                    GridViewMasterDataDetails.Columns[COL_CODE_NO].HeaderText = header + " " + COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.SCREEN_HEADER_CODE);
                }

                GridViewMasterDataDetails.Columns[COL_DESCRIPTION_NO].HeaderText = header;

                if (isType)
                {
                    GridViewMasterDataDetails.Columns[COL_TYPE_NO].Visible = true;
                    GridViewMasterDataDetails.Columns[COL_TYPE_NO].HeaderText = header + " " + COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.SCREEN_HEADER_TYPE);
                }
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
                if (gridViewRow.Cells[CONST_ZERO].Text.ToString() == "0" || gridViewRow.Cells[CONST_ZERO].Text.ToString() == STR_SPACE)
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
        private void AddFooterPadding(int rowsCount)
        {
            int spaceCount = (rowsCount * NUM_EACH_GRID_ROW_DISPLAY_HEIGHT);

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

        #region Properties

        /// <summary>
        /// TypeValue
        /// </summary>
        [Browsable(true)]
        [Category("TypeValue")]
        [Description("Gets and sets the TypeValue")]
        public string TypeValue
        {
            set
            {
                LabelTypeValue.Text = value;
                GetScreenMasterDataInformation();
                SetScreenLabels();
                ViewState[WebConstants.COL_STR_COLUMN_NAMES] = GetColumnNames(tableName);
                PopulateData();
                SetScreenAccess();
            }
        }

        #endregion
    }
}