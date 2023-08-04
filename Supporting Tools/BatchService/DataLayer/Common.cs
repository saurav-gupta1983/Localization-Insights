using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Text;
using System.Collections;
using DTO = Adobe.OogwayBatch.Common.DataTransferObjects;
using Adobe.OogwayBatch.Common;

namespace Adobe.OogwayBatch.DataLayer
{
    public static class Common
    {
        #region Variable Declaration

        // Set the connection string        
        private static string connectionString = string.Empty;
        private static DataSet returnDataSet;
        //SqlTransaction transaction = null;

        #endregion

        #region Connection Strings

        /// <summary>
        /// This function is used to get the Correct Connection String based on the Environement Set 
        /// in the Web Config File.
        /// </summary>
        /// <returns></returns>
        public static string GetConnectionString()
        {
            string conString = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"];

            try
            {
                //string appEnvironmentConfig = System.Configuration.ConfigurationManager.AppSettings["Environment"];

                //if (appEnvironmentConfig == null)
                //{
                //    throw new Exception("Unable to obtain reference to the AppEnvironmentConfig object based upon the config element Environment.");
                //}
                //else
                //{
                //    switch (appEnvironmentConfig)
                //    {
                //        case "PROD":
                //            conString = "server=localhost; database=localization; user id=root; password=root; pooling=false";
                //            break;
                //        case "QA":
                //            conString = "server=localhost; database=localization; user id=root; password=root; pooling=false";
                //            break;
                //        case "TEST":
                //            conString = "server=localhost; database=localization; user id=root; password=root; pooling=false";
                //            break;
                //        case "DEV":
                //            conString = "server=localhost; database=localization; user id=root; password=root; pooling=false";
                //            break;
                //        case "LOCAL":
                //            //conString = @"Server=sauragup-xp1\SQLEXPRESS;uid='';pwd='';Database=model;Integrated Security=true;";
                //            //conString = "Provider=MSDASQL; DRIVER={MySQL ODBC 3.51Driver}; SERVER= localhost; DATABASE=localization; UID=root; PASSWORD=root; OPTION=3";
                //            conString = "server=localhost; database=localization; user id=root; password=root; pooling=false";
                //            break;
                //        default:
                //            throw new ArgumentOutOfRangeException("humana.environment setting", "Invalid or missing Humana.Environment setting.");
                //    }
                //    return conString;
                //}

                return conString;
            }
            catch (Exception ex)
            {
                ExceptionLogger.Log(ex, conString);
                return conString = null;
            }
        }

        /// <summary>
        /// GetXLSConnectionString
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetXLSConnectionString(string fileName)
        {
            StringBuilder sbConn = new StringBuilder();
            try
            {

                sbConn = new StringBuilder();
                sbConn.Append(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=");
                sbConn.Append(System.Configuration.ConfigurationManager.AppSettings["FilePath"].ToString() + fileName + ".xls");
                sbConn.Append(";Extended Properties=");
                sbConn.Append(Convert.ToChar(34));
                sbConn.Append("Excel 8.0");
                sbConn.Append(Convert.ToChar(34));
                //;HDR=Yes;IMEX=2
                return sbConn.ToString();
            }
            catch (Exception ex)
            {
                ExceptionLogger.Log(ex, fileName);
            }
            return "";
        }

        /// <summary>
        /// GetXLSXConnectionString
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetXLSXConnectionString(string fileName)
        {
            StringBuilder sbConn = new StringBuilder();
            try
            {
                sbConn = new StringBuilder();
                sbConn.Append(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=");
                //sbConn.Append(System.Configuration.ConfigurationManager.AppSettings["FilePath"].ToString() + fileName + ".xls");
                sbConn.Append(fileName);
                sbConn.Append(";Extended Properties=");
                sbConn.Append(Convert.ToChar(34));
                sbConn.Append("Excel 12.0");
                sbConn.Append(Convert.ToChar(34));
                //sbConn.Append(";HDR=Yes");
                //sbConn.Append(";IMEX=1");
                //;HDR=Yes;IMEX=2
                return sbConn.ToString();
            }
            catch (Exception ex)
            {
                ExceptionLogger.Log(ex, fileName);
            }
            return "";
        }

        #endregion

        #region SQL Execution Functions

        /// <summary>
        /// GetColumnNames
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataSet GetColumnNames(string query)
        {
            connectionString = GetConnectionString();
            SqlConnection connection = new SqlConnection(connectionString);
            returnDataSet = new DataSet();
            try
            {
                connection.Open();
                // Call the ExecuteDataset() method to execute the stored procedure
                SqlCommand sqlCommand = new SqlCommand();
                SqlDataAdapter sqlAdapter = new SqlDataAdapter();

                sqlCommand.Connection = connection;

                sqlCommand.CommandText = query;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandTimeout = 3600;

                sqlAdapter.SelectCommand = sqlCommand;
                sqlAdapter.Fill(returnDataSet, "ColumnNames");

            }
            catch (SqlException sqlException)
            {
                ExceptionLogger.Log(sqlException, query);
            }
            catch (Exception ex)
            {
                ExceptionLogger.Log(ex, query);
            }
            finally
            {
                if (connection != null)
                    connection.Dispose();
            }
            return returnDataSet;
        }

        /// <summary>
        /// Takes a dataset and creates a OPENXML script dynamically around it for 
        /// bulk inserts 
        /// </summary> 
        /// <remarks>The DataSet must have at least one primary key, otherwise it'll wipe 
        /// out the entire table, then insert the dataset. Multiple Primary Keys are okay. 
        /// The dataset's columns must match the target table's columns EXACTLY. A 
        /// dataset column "democd" does not work for the sql column
        /// "DemoCD". Any missing or incorrect data is assumed NULL (default).
        /// </remarks>
        /// <param name="objDS">Dataset containing target DataTable.
        /// <param name="objCon">Open Connection to the database.
        /// <param name="tablename">Name of table to save.
        public static void BulkTableInsert(DataTable objDS, string user, ArrayList columns)
        {
            //Change the column mapping first.
            //System.Text.StringBuilder sb = new System.Text.StringBuilder(1000);
            //System.IO.StringWriter sw = new System.IO.StringWriter(sb);

            //foreach (DataColumn col in objDS.Columns)
            //{
            //    col.ColumnMapping = System.Data.MappingType.Attribute;
            //}
            //objDS.WriteXml(sw, System.Data.XmlWriteMode.WriteSchema);
            //string sqlText = buildBulkUpdateSql(sb.ToString(), objDS);
            //execSql(sqlText);

            StringBuilder columnList = new StringBuilder();

            for (int i = 1; i < columns.Count; i++)
            {
                columnList.Append(columns[i] + ",");
            }

            SqlConnection objCon = new SqlConnection(GetConnectionString());

            try
            {
                objCon.Open();

                foreach (DataRow dr in objDS.Rows)
                {
                    StringBuilder query = new StringBuilder("Insert into ");
                    query.Append(objDS.TableName);
                    query.Append("(" + columnList.ToString().Trim(new char[] { ',' }) + ")");
                    query.Append(" values (");
                    for (int i = 0; i < dr.ItemArray.Length; i++)
                    {
                        query.Append("'" + dr[i].ToString().Replace("'", "''") + "',");
                    }
                    query.Append("'" + user + "',");
                    query.Append("'" + System.DateTime.Now.ToString() + "'");
                    query.Append(")");

                    execSql(query.ToString(), objCon);
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.Log(ex, objDS.ToString());
            }
            finally
            {
                if (objCon != null)
                {
                    objCon.Close();
                    objCon.Dispose();
                }
            }
        }

        /// <summary>
        /// buildBulkUpdateSql
        /// </summary>
        /// <param name="dataXml"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        static string buildBulkUpdateSql(string dataXml, DataTable table)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            dataXml = dataXml.Replace(Environment.NewLine, "");
            dataXml = dataXml.Replace("\"", "''");
            //init the xml doc
            sb.Append(" SET NOCOUNT ON");
            sb.Append(" DECLARE @hDoc INT");
            sb.AppendFormat(" EXEC sp_xml_preparedocument @hDoc OUTPUT, '{0}'", dataXml);
            //This code deletes old data based on PK.
            sb.AppendFormat(" DELETE {0} FROM {0} INNER JOIN ", table.TableName);
            sb.AppendFormat(" (SELECT * FROM OPENXML (@hdoc, '/NewDataSet/{0}', 1)",
            table.TableName);
            sb.AppendFormat(" WITH {0}) xmltable ON 1 = 1", table.TableName);
            foreach (DataColumn col in table.PrimaryKey)
            {
                sb.AppendFormat(" AND {0}.{1} = xmltable.{1}", table.TableName,
                col.ColumnName);
            }
            //This code inserts new data.
            sb.AppendFormat(" INSERT INTO {0} SELECT *", table.TableName);
            sb.AppendFormat(" FROM OPENXML (@hdoc, '/NewDataSet/{0}', 1) WITH {0}",
            table.TableName);
            //clear the xml doc
            sb.Append(" EXEC sp_xml_removedocument @hDoc");
            return sb.ToString();
        }

        /// <summary>
        /// execSql
        /// </summary>
        /// <param name="objCon"></param>
        /// <param name="sqlText"></param>
        public static void execSql(string sqlText, SqlConnection objCon)
        {
            try
            {
                SqlCommand objCom = new SqlCommand();
                objCom.Connection = objCon;
                objCom.CommandType = CommandType.Text;
                objCom.CommandText = sqlText;
                objCom.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ExceptionLogger.Log(ex, sqlText);
            }
        }

        /// <summary>
        /// ExecuteQuery
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static DataSet ExecuteQuery(string query)
        {
            connectionString = GetConnectionString();
            SqlConnection connection = new SqlConnection(connectionString);
            returnDataSet = new DataSet();
            try
            {
                connection.Open();
                // Call the ExecuteDataset() method to execute the stored procedure
                SqlCommand sqlCommand = new SqlCommand();
                SqlDataAdapter sqlAdapter = new SqlDataAdapter();

                sqlCommand.Connection = connection;

                sqlCommand.CommandText = query;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandTimeout = 3600;

                sqlAdapter.SelectCommand = sqlCommand;
                sqlAdapter.Fill(returnDataSet, "Details");

            }
            catch (SqlException sqlException)
            {
                ExceptionLogger.Log(sqlException, query);
            }
            catch (Exception ex)
            {
                ExceptionLogger.Log(ex, query);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            return returnDataSet;
        }

        /// <summary>
        /// ExecuteQuery
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteDataReaderQuery(string query)
        {
            connectionString = GetConnectionString();
            SqlConnection connection = new SqlConnection(connectionString);

            SqlDataReader dbRead;

            dbRead = null;

            try
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();

                sqlCommand.Connection = connection;
                sqlCommand.CommandText = query;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandTimeout = 3600;

                dbRead = sqlCommand.ExecuteReader();

            }
            catch (SqlException sqlException)
            {
                ExceptionLogger.Log(sqlException, query);
            }
            catch (Exception ex)
            {
                ExceptionLogger.Log(ex, query);
            }
            finally
            {
                if (connection != null)
                    connection.Dispose();
            }
            return dbRead;
        }

        /// <summary>
        /// SaveDetails
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static bool SaveDetails(string query)
        {
            SqlConnection objCon = new SqlConnection(GetConnectionString()); ;
            try
            {
                objCon.Open();
                execSql(query.ToString(), objCon);
                return true;
            }
            catch (Exception ex)
            {
                ExceptionLogger.Log(ex, query);
            }
            finally
            {
                if (objCon != null)
                {
                    objCon.Close();
                    objCon.Dispose();
                }
            }

            return false;

        }

        #endregion

        #region MySQLQuery Functions

        /// <summary>
        /// ExecuteMySQLQuery
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static DataSet ExecuteMySQLQuery(string query)
        {
            connectionString = GetConnectionString();
            MySqlConnection connection = new MySqlConnection(connectionString);
            returnDataSet = new DataSet();
            try
            {
                connection.Open();
                // Call the ExecuteDataset() method to execute the stored procedure
                MySqlCommand command = new MySqlCommand();
                MySqlDataAdapter adapter = new MySqlDataAdapter();

                command.Connection = connection;

                command.CommandText = query;
                command.CommandType = CommandType.Text;
                command.CommandTimeout = 3600;

                adapter.SelectCommand = command;
                adapter.Fill(returnDataSet, "Details");

            }
            catch (MySqlException sqlException)
            {
                ExceptionLogger.Log(sqlException, query);
            }
            catch (Exception ex)
            {
                ExceptionLogger.Log(ex, query);
            }
            finally
            {
                if (connection != null)
                    connection.Dispose();
            }
            return returnDataSet;
        }

        /// <summary>
        /// ExecuteMySQLQuery
        /// </summary>
        /// <param name="storedProcedure"></param>
        /// <returns></returns>
        public static DataSet ExecuteMySQLQueryForStoredProcedures(string storedProcedure, MySqlParameter[] parameters)
        {
            connectionString = Common.GetConnectionString();
            MySqlConnection connection = new MySqlConnection(connectionString);
            returnDataSet = new DataSet();
            try
            {
                connection.Open();
                // Call the ExecuteDataset() method to execute the stored procedure
                MySqlCommand command = new MySqlCommand(storedProcedure, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                foreach (MySqlParameter parameter in parameters)
                    command.Parameters.Add(parameter);

                //Execute command
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(returnDataSet, "Result");
            }
            catch (MySqlException sqlException)
            {
                ExceptionLogger.Log(sqlException, storedProcedure);
            }
            catch (Exception ex)
            {
                ExceptionLogger.Log(ex, storedProcedure);
            }
            finally
            {
                if (connection != null)
                    connection.Dispose();
            }

            return returnDataSet;
        }

        /// <summary>
        /// ExecuteMySQLNonQuery
        /// </summary>
        /// <param name="objCon"></param>
        /// <param name="sqlText"></param>
        public static bool ExecuteMySQLNonQuery(string query)
        {
            connectionString = Common.GetConnectionString();
            MySqlConnection connection = new MySqlConnection(connectionString);
            returnDataSet = new DataSet();
            try
            {
                connection.Open();
                MySqlCommand objCom = new MySqlCommand();
                objCom.Connection = connection;
                objCom.CommandType = CommandType.Text;
                objCom.CommandText = query;
                objCom.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ExceptionLogger.Log(ex, query);
                return false;
            }
            finally
            {
                if (connection != null)
                    connection.Dispose();
            }
            return true;
        }

        /// <summary>
        /// ExecuteMySQLQueryForOperation
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static void ExecuteMySQLQueryForOperation(string operation, string query, string tableName, string loginID, int isSuccess)
        {
            connectionString = Common.GetConnectionString();
            MySqlConnection connection = new MySqlConnection(connectionString);
            returnDataSet = new DataSet();
            try
            {
                connection.Open();
                MySqlCommand objCom = new MySqlCommand();
                objCom.Connection = connection;
                objCom.CommandType = CommandType.Text;
                objCom.CommandText = String.Format("Insert Into SiteOperations(Operation, Query, TableName, PerformedBy, isSuccess) Values('{0}','{1}','{2}','{3}', {4})", operation, query, tableName, loginID, isSuccess);
                objCom.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ExceptionLogger.Log(ex, query);
            }
            finally
            {
                if (connection != null)
                    connection.Dispose();
            }
        }

        #endregion

        #region XLS Execution Functions

        /// <summary>
        /// ExecuteExcelQuery
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static DataSet ExecuteExcelQuery(string fileType, string query, string fileName)
        {
            if (fileType == "XLS")
            {
                connectionString = GetXLSConnectionString(fileName);
            }
            else
            {
                connectionString = GetXLSXConnectionString(fileName);
            }
            returnDataSet = new DataSet();

            OleDbConnection oledbConn = new OleDbConnection(connectionString);
            try
            {
                // Open connection
                oledbConn.Open();

                // Create OleDbCommand object and select data from worksheet Sheet1
                OleDbCommand cmd = new OleDbCommand(query, oledbConn);

                // Create new OleDbDataAdapter
                OleDbDataAdapter oleda = new OleDbDataAdapter();

                oleda.SelectCommand = cmd;

                // Fill the DataSet from the data extracted from the worksheet.
                oleda.Fill(returnDataSet, "Data");
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                // Close connection
                oledbConn.Close();
            }

            return returnDataSet;
        }

        /// <summary>
        /// ExecuteXLSQuery
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static DataSet ExecuteXLSQuery(string query, string fileName)
        {
            return ExecuteExcelQuery("XLS", query, fileName);
        }

        /// <summary>
        /// ExecuteXLSXQuery
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static DataSet ExecuteXLSXQuery(string query, string fileName)
        {
            return ExecuteExcelQuery("XLSX", query, fileName);
        }

        /// <summary>
        /// ExecuteXLSMQuery
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static DataSet ExecuteXLSMQuery(string query, string fileName)
        {
            return ExecuteExcelQuery("XLSM", query, fileName);
        }

        /// <summary>
        /// SaveToXLS
        /// </summary>
        /// <param name="query"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool SaveToXLS(string query, string fileName)
        {
            connectionString = GetXLSConnectionString(fileName);
            returnDataSet = new DataSet();

            OleDbConnection oledbConn = new OleDbConnection(connectionString);
            try
            {
                // Open connection
                oledbConn.Open();
                OleDbCommand objCmd = new OleDbCommand(query, oledbConn);
                objCmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception)
            {
            }
            finally
            {
                if (oledbConn != null)
                {
                    oledbConn.Close();
                    oledbConn.Dispose();
                }
            }

            return false;

        }

        #endregion

        #region WORD Execution Functions

        /// <summary>
        /// ExecuteXLSQuery
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static DataSet ExecuteWordQuery(string query, string fileName)
        {
            connectionString = GetXLSConnectionString(fileName);
            returnDataSet = new DataSet();

            OleDbConnection oledbConn = new OleDbConnection(connectionString);
            try
            {
                // Open connection
                oledbConn.Open();

                // Create OleDbCommand object and select data from worksheet Sheet1
                OleDbCommand cmd = new OleDbCommand(query, oledbConn);

                // Create new OleDbDataAdapter
                OleDbDataAdapter oleda = new OleDbDataAdapter();

                oleda.SelectCommand = cmd;

                // Fill the DataSet from the data extracted from the worksheet.
                oleda.Fill(returnDataSet, "Data");
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                // Close connection
                oledbConn.Close();
            }

            return returnDataSet;
        }

        /// <summary>
        /// SaveToXLS
        /// </summary>
        /// <param name="query"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool SaveToWord(string query, string fileName)
        {
            connectionString = GetXLSConnectionString(fileName);
            returnDataSet = new DataSet();

            OleDbConnection oledbConn = new OleDbConnection(connectionString);
            try
            {
                // Open connection
                oledbConn.Open();
                OleDbCommand objCmd = new OleDbCommand(query, oledbConn);
                objCmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception)
            {
            }
            finally
            {
                if (oledbConn != null)
                {
                    oledbConn.Close();
                    oledbConn.Dispose();
                }
            }

            return false;

        }

        #endregion
    }
}
