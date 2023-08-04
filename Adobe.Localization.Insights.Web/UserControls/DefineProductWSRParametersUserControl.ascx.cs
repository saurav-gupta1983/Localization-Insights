using System;
using System.Data;
using System.Collections;
using System.Web.UI.WebControls;
using Adobe.Localization.Insights.Common;
using COM = Adobe.Localization.Insights.Common.Common;
using BO = Adobe.Localization.Insights.BusinessLayer;
using DTO = Adobe.Localization.Insights.Common.DataTransferObjects;

namespace Adobe.Localization.Insights.Web.UserControls
{
    /// <summary>
    /// DefineProductWSRParametersUserControl
    /// </summary>
    public partial class DefineProductWSRParametersUserControl : System.Web.UI.UserControl
    {
        #region Variables

        private string productID = "";
        private DataTable dtProductWSRParameters = new DataTable();
        private DataTable dtScreenLabels;

        private DTO.Header dataHeader = new DTO.Header();

        private bool isReadOnly = true;
        private const string STR_ONE = "1";
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
            GetScreenAccess();
            SetScreenLabels();

            productID = Session[WebConstants.SESSION_PRODUCT_ID].ToString();
            PopulateProductWSRParameters();
            PopulateWSRParameters();
            LabelMessage.Text = "";

            SetScreenAccess();
        }

        #endregion

        #region Grid Events

        /// <summary>
        /// GridViewProductWSRParameters_RowDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewProductWSRParameters_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label LabelWSRParameterID = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_WSR_PARAMETER_ID);
                CheckBox CheckBoxWSRParameterSelected = (CheckBox)e.Row.FindControl(WebConstants.CONTROL_CHECKBOX_WSR_PARAMETERS_SELECTED);

                DataRow[] drColl = dtProductWSRParameters.Select(WebConstants.COL_WSR_PARAMETER_ID + " = " + LabelWSRParameterID.Text);
                if (drColl.Length == 1)
                {
                    Label LabelProductWSRParameterID = (Label)e.Row.FindControl(WebConstants.CONTROL_LABEL_PRODUCT_WSR_PARAMETER_ID);
                    LabelProductWSRParameterID.Text = drColl[0][WebConstants.COL_WSR_PARAMETER_SELECTED_ID].ToString();

                    if (drColl[0][WebConstants.COL_WSR_PARAMETER_IS_SELECTED].ToString() == STR_ONE)
                        CheckBoxWSRParameterSelected.Checked = true;
                }

                if (isReadOnly)
                    CheckBoxWSRParameterSelected.Enabled = false;
            }
        }

        #endregion

        #region Button Events

        /// <summary>
        /// ButtonUpdateProductWSRParameters_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonUpdateProductWSRParameters_Click(object sender, EventArgs e)
        {
            DTO.WSRData wsrData = new DTO.WSRData();
            wsrData.DataHeader = dataHeader;
            wsrData.ProductID = productID;
            wsrData.WsrProductParameterCollection = new ArrayList();

            for (int i = 0; i < GridViewProductWSRParameters.Rows.Count; i++)
            {
                GridViewRow row = GridViewProductWSRParameters.Rows[i];
                Label LabelProductWSRParameterID = (Label)row.FindControl(WebConstants.CONTROL_LABEL_PRODUCT_WSR_PARAMETER_ID);
                Label LabelWSRParameterID = (Label)row.FindControl(WebConstants.CONTROL_LABEL_WSR_PARAMETER_ID);
                CheckBox CheckBoxSelectedWSRParameters = (CheckBox)row.FindControl(WebConstants.CONTROL_CHECKBOX_WSR_PARAMETERS_SELECTED);

                DTO.WSRProductParameter wsrProductParameter = new DTO.WSRProductParameter();
                wsrProductParameter.WsrParamSelectedID = LabelProductWSRParameterID.Text;
                wsrProductParameter.WsrParameterID = LabelWSRParameterID.Text;
                wsrProductParameter.IsSelected = CheckBoxSelectedWSRParameters.Checked;

                wsrData.WsrProductParameterCollection.Add(wsrProductParameter);
            }

            if (BO.BusinessObjects.UpdateProductWSRParameters(wsrData))
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_SUCCESS_SAVE_MESSAGE);
            else
                LabelMessage.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, WebConstants.COM_FAILURE_SAVE_MESSAGE);
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// PopulateProductWSRParameters
        /// </summary>
        /// <param name="productData"></param>
        private void PopulateProductWSRParameters()
        {
            DTO.WSRData wsrParametersData = new DTO.WSRData();
            wsrParametersData.ProductID = productID;
            wsrParametersData.IsSelectedWSRParameters = false;

            dtProductWSRParameters = BO.BusinessObjects.GetProductWSRParameters(wsrParametersData).Tables[0];
        }

        /// <summary>
        /// PopulateProductVersion
        /// </summary>
        private void PopulateWSRParameters()
        {
            DTO.WSRData wsrParametersData = new DTO.WSRData();
            DataTable dtWSRParameter = BO.BusinessObjects.GetProductWSRParameters(wsrParametersData).Tables[0];

            GridViewProductWSRParameters.DataSource = dtWSRParameter;
            GridViewProductWSRParameters.DataBind();
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
                ButtonUpdateProductWSRParameters.Enabled = false;
            }

            AddFooterPadding(GridViewProductWSRParameters.Rows.Count);
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
            PopulateScreenLabels();

            if (Session[WebConstants.SESSION_LOCALE_ID] != null && Session[WebConstants.SESSION_LOCALE_ID].ToString() != WebConstants.DEF_VAL_LOCALE_ID)
            {
                LabelHeader.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, LabelHeader.Text);
                ButtonUpdateProductWSRParameters.Text = COM.GetScreenLocalizedLabel(dtScreenLabels, ButtonUpdateProductWSRParameters.Text);

                foreach (DataControlField field in GridViewProductWSRParameters.Columns)
                    if (field.HeaderText != "")
                        field.HeaderText = COM.GetScreenLocalizedLabel(dtScreenLabels, field.HeaderText);
            }
        }

        /// <summary>
        /// AddFooterPadding
        /// </summary>
        private void AddFooterPadding(int rowsCount)
        {
            int spaceCount = (rowsCount * NUM_EACH_GRID_ROW_DISPLAY_HEIGHT) + 4 * NUM_EACH_GRID_ROW_DISPLAY_HEIGHT;

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