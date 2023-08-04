using System;
using System.Data;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Adobe.Localization.Insights.Common;
using COM = Adobe.Localization.Insights.Common.Common;
using BO = Adobe.Localization.Insights.BusinessLayer;
using DTO = Adobe.Localization.Insights.Common.DataTransferObjects;

namespace Adobe.Localization.Insights.Web.UserControls
{
    /// <summary>
    /// MaintainScreenLabelsUserControl
    /// </summary>
    public partial class MaintainScreenLabelsUserControl : System.Web.UI.UserControl
    {
        #region Variables

        private DataTable dtLocales;

        private string localeID;
        private string localeCode;
        private string locale;

        private DataTable dtScreenLabels;
        private DTO.Header dataHeader = new DTO.Header();

        private bool isReadOnly = true;

        private const string STR_SPACE = "&nbsp;";
        private const string STR_ZERO = "0";
        private const string STR_ASSIGNED_PRODUCTS = "AssignedProducts";
        private const string STR_ROLE_ADMIN_FILTER_CRITERIA = "Role = 'Product' OR ProjectRole='Group Manager'";
        private const string STR_ROLE_NON_ADMIN_FILTER_CRITERIA = "Role = 'Product'";
        private const string STR_ROLE_VENDOR_FILTER_CRITERIA = "Role = 'Product' AND IsContractorApplicable = 1";
        private const string STR_VAL_ISACTIVE = "1";
        private const string STR_LOCALIZED_VALUE = "Localized";

        private const int NUM_EACH_GRID_ROW_DISPLAY_HEIGHT = 20;
        private const int COL_LOCALIZED_NO = 4;

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

            if (DropDownListLocales.Items.Count > 0)
                SetScreenLabels();

            if (!IsPostBack)
            {
                PopulateLocales();
                PopulateScreens();
                PopulateData();
            }

            dtLocales = (DataTable)ViewState[WebConstants.TBL_LOCALES];
            SetScreenAccess();
        }

        /// <summary>
        /// Page_Init
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
        }

        #endregion

        #region Button Events

        /// <summary>
        /// ButtonSaveScreenLabels_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSaveScreenLabels_Click(object sender, EventArgs e)
        {
            DTO.Screens screenData = new DTO.Screens();
            screenData.DataHeader = dataHeader;
            screenData.LocaleID = localeID;
            screenData.LocaleCode = localeCode;
            screenData.LabelCollections = new System.Collections.Generic.List<DTO.Screens>();

            foreach (GridViewRow row in GridViewScreenLabels.Rows)
            {
                Label LabelScreenLocalizedLabelID = (Label)row.FindControl(WebConstants.CONTROL_LABEL_SCREEN_LOCALIZED_LABELID);
                Label LabelScreenLabelID = (Label)row.FindControl(WebConstants.CONTROL_LABEL_SCREEN_LABELID);
                TextBox TextBoxLocalizedValue = (TextBox)row.FindControl(WebConstants.CONTROL_TEXTBOX_LOCALIZED_VALUE);

                if (LabelScreenLocalizedLabelID.Text != "" || TextBoxLocalizedValue.Text != "")
                {
                    DTO.Screens localizedData = new DTO.Screens();
                    localizedData.ScreenLocalizedLabelID = LabelScreenLocalizedLabelID.Text;
                    localizedData.ScreenLabelID = LabelScreenLabelID.Text;
                    localizedData.LocalizedValue = TextBoxLocalizedValue.Text.Replace("'", "''");

                    screenData.LabelCollections.Add(localizedData);
                }
            }

            if (BO.BusinessObjects.UpdateScreenLocalizedValues(screenData))
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_SUCCESS_SAVE_MESSAGE);
            else
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_SAVE_MESSAGE);

            PopulateData();
        }

        #endregion

        #region DropDownList events

        /// <summary>
        /// DropDownListLocales_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListLocales_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateData();
            LabelMessage.Text = "";
        }

        /// <summary>
        /// DropDownListScreen_OnSelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListScreen_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateData();
            LabelMessage.Text = "";
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// PopulateData
        /// </summary>
        private void PopulateData()
        {
            SetScreenLabels();

            DTO.Screens screenData = new DTO.Screens();
            screenData.ScreenID = DropDownListScreen.SelectedValue;
            screenData.LocaleID = localeID;
            screenData.LocaleCode = localeCode;

            DataTable dtScreenLabels = BO.BusinessObjects.GetScreenLocalizedLabels(screenData).Tables[0];

            GridViewScreenLabels.DataSource = dtScreenLabels;
            GridViewScreenLabels.DataBind();
        }

        /// <summary>
        /// PopulateScreens
        /// </summary>
        private void PopulateScreens()
        {
            DTO.Screens screenData = new DTO.Screens();
            screenData.IsAllScreens = true;

            DataTable dtScreens = BO.BusinessObjects.GetScreensDetails(screenData).Tables[0];

            DropDownListScreen.DataSource = dtScreens;
            DropDownListScreen.DataValueField = WebConstants.COL_SCREEN_ID;
            DropDownListScreen.DataTextField = WebConstants.COL_SCREEN;
            DropDownListScreen.DataBind();
        }

        /// <summary>
        /// PopulateLocales
        /// </summary>
        private void PopulateLocales()
        {
            dtLocales = PopulateTypeDetails(WebConstants.TBL_LOCALES, DropDownListLocales);

            ViewState[WebConstants.TBL_LOCALES] = dtLocales;
        }

        /// <summary>
        /// PopulateTypeDetails
        /// </summary>
        private DataTable PopulateTypeDetails(string tableName, DropDownList dropDownList)
        {
            DTO.MasterData masterData = new DTO.MasterData();
            masterData.TableName = tableName;

            DataTable typeDetailsDataTable = BO.BusinessObjects.GetDetailsforMasterData(masterData).Tables[0];

            typeDetailsDataTable.Rows.RemoveAt(0);

            if (dropDownList != null)
            {
                dropDownList.DataSource = typeDetailsDataTable;
                dropDownList.DataValueField = WebConstants.COL_LOCALE_ID;
                dropDownList.DataTextField = WebConstants.COL_LOCALE;
                dropDownList.DataBind();
            }

            return typeDetailsDataTable;
        }

        /// <summary>
        /// SetLocale
        /// </summary>
        private void SetLocale()
        {
            localeID = DropDownListLocales.SelectedValue;
            dtLocales = (DataTable)ViewState[WebConstants.TBL_LOCALES];

            DataRow dr = dtLocales.Select(WebConstants.COL_LOCALE_ID + " = " + DropDownListLocales.SelectedValue)[0];
            while (true)
            {
                if (dr[WebConstants.COL_FALLBACK_LOCALE_ID].ToString() != "")
                {
                    dr = dtLocales.Select(WebConstants.COL_LOCALE_ID + " = " + dr[WebConstants.COL_FALLBACK_LOCALE_ID].ToString())[0];
                    localeID = dr[WebConstants.COL_LOCALE_ID].ToString();
                }
                else
                    break;
            }
            localeCode = dr[WebConstants.COL_LOCALE_CODE].ToString();
            locale = dr[WebConstants.COL_LOCALE].ToString();
        }

        #endregion

        #region Screen Access and Labels

        /// <summary>
        /// GetScreenAccess
        /// </summary>
        private void GetScreenAccess()
        {
            dataHeader.LoginID = Session[WebConstants.SESSION_LOGIN_ID].ToString();

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
                ButtonSaveScreenLabels.Enabled = false;
            }

            AddFooterPadding(GridViewScreenLabels.Rows.Count);
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
            SetLocale();
            PopulateScreenLabels();

            if (Session[WebConstants.SESSION_LOCALE_ID] != null && Session[WebConstants.SESSION_LOCALE_ID].ToString() != WebConstants.DEF_VAL_LOCALE_ID)
            {
                LabelHeader.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelHeader.Text);
                LabelScreen.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelScreen.Text);
                LabelLocales.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelLocales.Text);

                PanelSearchFilter.GroupingText = COM.GetScreenLocalizedLabel(dtScreenLabels, PanelSearchFilter.GroupingText);
                ButtonSaveScreenLabels.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, ButtonSaveScreenLabels.Text);

                foreach (DataControlField field in GridViewScreenLabels.Columns)
                    if (field.HeaderText != "")
                        field.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, field.HeaderText);
            }

            GridViewScreenLabels.Columns[COL_LOCALIZED_NO].HeaderText = String.Format(COM.GetScreenLocalizedLabel(dtScreenLabels, STR_LOCALIZED_VALUE), locale);
        }

        /// <summary>
        /// AddFooterPadding
        /// </summary>
        private void AddFooterPadding(int rowsCount)
        {
            int spaceCount = (rowsCount * 2 * NUM_EACH_GRID_ROW_DISPLAY_HEIGHT) - 2 * NUM_EACH_GRID_ROW_DISPLAY_HEIGHT;

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