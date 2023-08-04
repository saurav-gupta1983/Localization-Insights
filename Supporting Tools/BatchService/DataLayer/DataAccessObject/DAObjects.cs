using System;
using System.Xml;
using System.Data;
using System.Text;
using System.Collections;
using MySql.Data.MySqlClient;
using Adobe.OogwayBatch.DataLayer;
using COM = Adobe.OogwayBatch.Common.Common;
using DTO = Adobe.OogwayBatch.Common.DataTransferObjects;
using CONST = Adobe.OogwayBatch.Common.Constants;

namespace Adobe.OogwayBatch.DataLayer.DataAccessObjects
{
    /// <summary>
    /// DAObjects
    /// </summary>
    public class DAObjects
    {
        #region Batch Process

        /// <summary>
        /// DumpDataToDatabase
        /// </summary>
        /// <param name="watsonResponseXml"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool DumpDataToDatabase(XmlDocument watsonResponseXml, string type)
        {
            return Common.ExecuteMySQLNonQuery(Query.QueryDAO.DumpDataToDatabase(watsonResponseXml, type));
        }

        /// <summary>
        /// ProcessDumpData
        /// </summary>
        /// <param name="watsonResponseXml"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static DataSet ProcessDumpData(DateTime executionDate)
        {
            MySqlParameter[] parameters = new MySqlParameter[1];

            parameters[0] = new MySqlParameter("@ExecutionDate", executionDate);
            return Common.ExecuteMySQLQueryForStoredProcedures(CONST.BATCH_STORED_PROCEDURE, parameters);
        }

        /// <summary>
        /// GetBugStatusDetails
        /// </summary>
        /// <returns></returns>
        public static DataSet GetBugStatusDetails()
        {
            return Common.ExecuteMySQLQuery(Query.QueryDAO.GetBugStatusDetails());
        }

        /// <summary>
        /// executionDate
        /// </summary>
        /// <returns></returns>
        public static DataSet GetProductDetails(DateTime executionDate)
        {
            return Common.ExecuteMySQLQuery(Query.QueryDAO.GetProductDetails(executionDate));
        }

        /// <summary>
        /// GetBugsCount
        /// </summary>
        /// <returns></returns>
        public static DataSet GetBugsCount(DataRow drProduct, string type)
        {
            return Common.ExecuteMySQLQuery(Query.QueryDAO.GetBugsCount(drProduct, type));
        }

        #endregion
    }
}
