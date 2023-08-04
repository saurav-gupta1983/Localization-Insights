using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using DTO = Adobe.OogwayBatch.Common.DataTransferObjects;
using Adobe.OogwayBatch.Common;
using CONST = Adobe.OogwayBatch.Common.Constants;

namespace Adobe.OogwayBatch.DataLayer.Query
{
    /// <summary>
    /// QueryDAO
    /// </summary>
    public static class QueryDAO
    {
        #region Batch Process

        /// <summary>
        /// DumpDataToDatabase
        /// </summary>
        /// <param name="watsonResponseXml"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string DumpDataToDatabase(XmlDocument watsonResponseXml, string type)
        {
            string query = "INSERT INTO watsondata_dump_temp (Type, Product, Version, BugOwner, BugState, BugStatus, BugCount, AddedBy, AddedOn ) VALUES {0}";

            StringBuilder insertValues = new StringBuilder();
            XmlNodeList groupList = watsonResponseXml.GetElementsByTagName("group");

            foreach (XmlNode group in groupList)
            {
                insertValues.Append("('" + type + "',");
                XmlNodeList dataList = group.ChildNodes;
                foreach (XmlNode data in dataList)
                {
                    if (data.InnerText == "NULL")
                        insertValues.Append("NULL,");
                    else
                        insertValues.Append("'" + data.InnerText + "',");
                }
                insertValues.Append("'BATCH', SysDate()");
                insertValues.Append("),");
            }
            insertValues.Length--;

            return String.Format(query, insertValues.ToString());
        }

        /// <summary>
        /// GetBugStatusDetails
        /// </summary>
        /// <returns></returns>
        public static string GetBugStatusDetails()
        {
            string query = "SELECT BugState, BugStatus FROM WatsonBugsStatus";
            return query;
        }


        /// <summary>
        /// GetProductDetailsFromDump
        /// </summary>
        /// <returns></returns>
        public static string GetProductDetails(DateTime executionDate)
        {
            StringBuilder query = new StringBuilder();

            query.Append(" SELECT WatsonProductDetailID, Product, Version, LastExecutionDate");
            query.Append(" FROM watsonproductdetails");
            query.Append(String.Format(" WHERE LastExecutionDate = '{0}'", executionDate.ToString("u")));
            return query.ToString();
        }

        /// <summary>
        /// GetBugsCount
        /// </summary>
        /// <returns></returns>
        public static string GetBugsCount(DataRow drProduct, string type)
        {
            StringBuilder query = new StringBuilder();

            query.Append(" SELECT stat.BugState, stat.BugStatus, DAY(ExtractedDate) as Days, MONTH(ExtractedDate) as months, YEAR(ExtractedDate) as years, BugCount");
            query.Append(" From watsondata_daily daily");
            query.Append(" INNER JOIN watsonbugsstatus stat");
            query.Append(" ON daily.BugStatusID = stat.WatsonBugsStatusID");

            int typeID = 0;
            if (type == "CORE") typeID = 1;
            if (type == "LOC") typeID = 2;

            query.AppendFormat(" AND daily.TypeID = {0}", typeID);
            query.AppendFormat(" AND daily.ProductDetailID = {0}", drProduct[CONST.COL_PRODUCT_DETAIL_ID]);
            query.Append(" INNER JOIN WatsonProductDetails pd");
            query.Append(" ON daily.ProductDetailID = pd.WatsonProductDetailID");
            query.Append(" AND DATE(daily.ExtractedDate) =  DATE(pd.LastExecutionDate)");
            query.AppendFormat(" AND DATE(daily.ExtractedDate) =  DATE(pd.LastExecutionDate)");

            return query.ToString();
        }

        #endregion

        #region Other Common Functions

        /// <summary>
        /// GetColumnNames
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string GetColumnNames(string tableName)
        {
            StringBuilder query = new StringBuilder("SELECT COLUMN_NAME FROM information_schema.COLUMNS ");
            query.Append(" WHERE (TABLE_NAME = '" + tableName + "')");

            return query.ToString();
        }

        /// <summary>
        /// BulkTableInsert
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="tableName"></param>
        /// <param name="columnList"></param>
        /// <returns></returns>
        public static string BulkTableInsert(DataRow dr, string tableName, string columnList)
        {
            StringBuilder query = new StringBuilder("Insert into ");

            query.Append(tableName);
            query.Append("(" + columnList.Trim(new char[] { ',' }) + ")");
            query.Append(" values (");

            for (int i = 0; i < dr.ItemArray.Length; i++)
            {
                query.Append("'" + dr[i].ToString().Replace("'", "''") + "',");
            }
            query.Append("'ADMIN',");
            query.Append("'" + System.DateTime.Now.ToString() + "'");
            query.Append(")");

            return query.ToString();
        }

        /// <summary>
        /// FormattedDate
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private static string FormattedDate(DateTime date)
        {
            return date.Year + "-" + date.Month + "-" + date.Day;
        }

        /// <summary>
        /// FormattedDate
        /// </summary>
        /// <param name="dateString"></param>
        /// <returns></returns>
        private static string FormattedDate(string dateString)
        {
            if (dateString != null || dateString != "")
            {
                DateTime date = DateTime.Parse(dateString);
                return date.Year + "-" + date.Month + "-" + date.Day;
            }

            return "";
        }

        /// <summary>
        /// CheckForNull
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private static string CheckForNull(string value)
        {
            if (value == null || value == "")
                return "null";
            return value;
        }

        #endregion
    }
}
