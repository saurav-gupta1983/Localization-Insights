using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Globalization;

namespace Adobe.Localization.Insights.Common
{
    /// <summary>
    /// Common
    /// </summary>
    public static class Common
    {
        #region Date Functions

        /// <summary>
        /// GetFirstDayOfWeek
        /// </summary>
        /// <returns></returns>
        public static DateTime GetFirstDayOfWeek()
        {
            return GetFirstDayOfWeek(System.DateTime.Now);
        }

        /// <summary>
        /// Returns the first day of the week that the specified
        /// date is in using the current culture.
        /// </summary>
        public static DateTime GetFirstDayOfWeek(string date)
        {
            return GetFirstDayOfWeek(DateTime.Parse(date));
        }

        /// <summary>
        /// Returns the first day of the week that the specified
        /// date is in using the current culture.
        /// </summary>
        public static DateTime GetFirstDayOfWeek(DateTime dayInWeek)
        {
            CultureInfo defaultCultureInfo = CultureInfo.CurrentCulture;
            return GetFirstDayOfWeek(dayInWeek, defaultCultureInfo);
        }

        /// <summary>
        /// Returns the first day of the week that the specified date
        /// is in.
        /// </summary>
        public static DateTime GetFirstDayOfWeek(DateTime dayInWeek, CultureInfo cultureInfo)
        {
            DayOfWeek firstDay = cultureInfo.DateTimeFormat.FirstDayOfWeek;
            DateTime firstDayInWeek = dayInWeek.Date;
            while (firstDayInWeek.DayOfWeek != firstDay)
                firstDayInWeek = firstDayInWeek.AddDays(-1);
            return firstDayInWeek;
        }

        #endregion

        #region Locales Functions

        /// <summary>
        /// GetLocaleForScreenLabels
        /// </summary>
        /// <param name="localeID"></param>
        /// <param name="dtLocales"></param>
        /// <returns></returns>
        public static DataRow GetLocaleForScreenLabels(string localeID, DataTable dtLocales)
        {
            DataRow dr = dtLocales.Select("LocaleID = " + localeID)[0];
            while (true)
            {
                if (dr["FallBackLocaleID"].ToString() != "")
                {
                    dr = dtLocales.Select("LocaleID = " + dr["FallBackLocaleID"].ToString())[0];
                    localeID = dr["LocaleID"].ToString();
                }
                else
                    break;
            }

            return dr;
        }

        #endregion

        #region Page Functions

        /// <summary>
        /// GetScreenLocalizedLabel
        /// </summary>
        /// <param name="dtLabels"></param>
        /// <param name="label"></param>
        /// <returns></returns>
        public static string GetScreenLocalizedLabel(DataTable dtLabels, string label, string labelCategory)
        {
            string filterCriteria = WebConstants.COL_SCREEN_LABEL + " = '" + label.Replace("'", "''") + "' AND LabelCategory = '" + labelCategory + "'";

            return GetLabelValue(dtLabels, label, filterCriteria);
        }

        /// <summary>
        /// GetScreenLocalizedLabel
        /// </summary>
        /// <param name="dtLabels"></param>
        /// <param name="label"></param>
        /// <returns></returns>
        public static string GetScreenLocalizedLabel(DataTable dtLabels, string label)
        {
            string filterCriteria = WebConstants.COL_SCREEN_LABEL + " = '" + label.Replace("'", "''") + "'";

            return GetLabelValue(dtLabels, label, filterCriteria);
        }

        ///// <summary>
        ///// GetScreenLocalizedLabel
        ///// </summary>
        ///// <param name="dtLabels"></param>
        ///// <param name="label"></param>
        ///// <returns></returns>
        //public static string GetScreenLocalizedLabel(DataTable dtLabels, string label, bool isIncludeCommon)
        //{
        //    string filterCriteria = WebConstants.COL_SCREEN_LABEL + " = '" + label.Replace("'", "''") + "'";

        //    if (!isIncludeCommon)
        //    {
        //        filterCriteria = filterCriteria + " AND ScreenIdentifier <> '" + WebConstants.SCREEN_COMMON + "'";
        //    }

        //    return GetLabelValue(dtLabels, label, filterCriteria);
        //}

        /// <summary>
        /// GetLabelValue
        /// </summary>
        /// <param name="dtLabels"></param>
        /// <param name="label"></param>
        /// <returns></returns>
        public static string GetLabelValue(DataTable dtLabels, string label, string filterCriteria)
        {
            DataRow[] dr = dtLabels.Select(filterCriteria);

            if (dr.Length == 1)
                return dr[0][WebConstants.COL_SCREEN_LOCALIZED_VALUE].ToString();
            else
                return label;
        }

        #endregion

        #region General Common Functions

        /// <summary>
        /// AddRowandColumntoDataSet
        /// </summary>
        /// <param name="dsDetails"></param>
        /// <returns></returns>
        public static DataSet AddRowandColumntoDataSet(DataSet dsDetails, int tableNo)
        {
            if (dsDetails == null)
                return dsDetails;

            AddRowandColumntoDataTable(dsDetails.Tables[tableNo]);

            return dsDetails;
        }

        /// <summary>
        /// AddRowandColumntoDataTable
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        public static DataTable AddRowandColumntoDataTable(DataTable dtDetails)
        {
            DataColumn col = new DataColumn(WebConstants.COL_TEMP_ROW, Type.GetType(WebConstants.STR_SYSTEM_INT32));
            dtDetails.Columns.Add(col);

            for (int rowCount = 0; rowCount < dtDetails.Rows.Count; rowCount++)
            {
                dtDetails.Rows[rowCount][WebConstants.COL_TEMP_ROW] = rowCount + 1;
            }

            DataRow dr = dtDetails.NewRow();
            dr[0] = 0;
            dtDetails.Rows.InsertAt(dr, 0);

            return dtDetails;
        }

        /// <summary>
        /// AddSequenceColumnToDataTable
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        public static DataTable AddSequenceColumnToDataTable(DataTable dtDetails)
        {
            DataColumn col = new DataColumn(WebConstants.COL_TEMP_ROW, Type.GetType(WebConstants.STR_SYSTEM_INT32));
            dtDetails.Columns.Add(col);

            for (int rowCount = 0; rowCount < dtDetails.Rows.Count; rowCount++)
            {
                dtDetails.Rows[rowCount][WebConstants.COL_TEMP_ROW] = rowCount + 1;
            }

            return dtDetails;
        }

        /// <summary>
        /// AddEmptyRowtoDataTable
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        public static DataTable AddEmptyRowtoDataTable(DataTable dtDetails)
        {
            DataRow dr = dtDetails.NewRow();
            dr[0] = 0;
            dtDetails.Rows.InsertAt(dr, 0);

            return dtDetails;
        }

        #endregion
    }
}
