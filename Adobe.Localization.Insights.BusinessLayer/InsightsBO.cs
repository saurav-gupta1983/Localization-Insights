using System;
using System.Data;
using System.Text;
using System.Collections;
using DTO = Adobe.Localization.Insights.Common.DataTransferObjects;
using DAO = Adobe.Localization.Insights.DataLayer;

namespace Adobe.Localization.Insights.BusinessLayer
{
    public class InsightsBO
    {
        #region Project Functions

        /// <summary>
        /// GetProjects
        /// </summary>
        /// <param name="projectSearchDTO"></param>
        /// <returns></returns>
        public static DataSet GetProjects(DTO.ProjectSearchDTO projectSearchDTO)
        {
            StringBuilder query = new StringBuilder("Select * from Project Where ");

            if (projectSearchDTO.ProjectID.Trim() != "")
            {
                query.Append("ProjectID = " + projectSearchDTO.ProjectID.Trim() + " AND ");
            }
            if (projectSearchDTO.ProjectTitle.Trim() != "")
            {
                query.Append("ProjectTitle Like '%" + projectSearchDTO.ProjectID.Trim() + "%' AND ");
            }
            if (projectSearchDTO.Lifecycle != "")
            {
                query.Append("LifeCycleID = " + projectSearchDTO.Lifecycle + " AND ");
            }
            if (projectSearchDTO.LifecyclePhase != "")
            {
                query.Append("LifeCyclePhaseID = " + projectSearchDTO.LifecyclePhase + " AND ");
            }
            if (projectSearchDTO.BeginDate != "")
            {
                query.Append("BeginDate = " + projectSearchDTO.BeginDate + " AND ");
            }
            if (projectSearchDTO.EndDate != "")
            {
                query.Append("EndDate = " + projectSearchDTO.EndDate);
            }

            return DAO.InsightsDAO.GetProjects(query.ToString());
        }

        #endregion

        #region Touchpoint Functions
        #endregion

        #region MasterData Functions

        /// <summary>
        /// GetDetailsforMasterData
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnNames"></param>
        /// <returns></returns>
        public static DataSet GetDetailsforMasterData(string tableName, ArrayList columnNames)
        {
            StringBuilder query = new StringBuilder("exec('Select Identity(int, 1,1) row, ");

            for (int colCount = 0; colCount < columnNames.Count; colCount++)
            {
                if (colCount == 0)
                {
                    query.Append("Convert(integer, " + columnNames[colCount].ToString() + ") ID");
                }
                else
                {
                    query.Append("," + columnNames[colCount].ToString());
                }
            }

            query.Append("  Into #temp from " + tableName + " Select * from #temp Drop table #temp')");

            DataSet details;
            details = DAO.InsightsDAO.GetDetailsforMasterData(query.ToString());

            for (int colCount = 0; colCount < columnNames.Count; colCount++)
            {
                details.Tables[0].Columns[colCount + 1].ColumnName = columnNames[colCount].ToString();
            }

            return details;
        }

        /// <summary>
        /// AddUpdateDetailsforMasterData - To Add new record to Database
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="columnValue"></param>
        /// <returns></returns>
        public static bool AddUpdateDetailsforMasterData(string tableName, string columnName, string columnValue, string user)
        {
            string query = "Insert into " + tableName + "(" + columnName + ", ModifiedBy, ModifiedDateTime) Values('" + columnValue + "', '" + user + "', '" + System.DateTime.Now.ToString() +"')";
            return DAO.InsightsDAO.AddDetailsforMasterData(query);
        }

        /// <summary>
        /// AddUpdateDetailsforMasterData - To update records in Database
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="columnValue"></param>
        /// <returns></returns>
        public static bool AddUpdateDetailsforMasterData(string tableName, ArrayList columnNames, ArrayList columnValues)
        {
            StringBuilder query = new StringBuilder("Update " + tableName + " Set ");

            for (int colCount = 1; colCount < columnNames.Count; colCount++)
            {
                if (colCount == 1)
                {
                    query.Append(columnNames[colCount].ToString() + " = '" + columnValues[colCount].ToString() + "'");
                }
                else
                {
                    query.Append("," + columnNames[colCount].ToString() + " = '" + columnValues[colCount].ToString() + "'");
                }
            }

            query.Append(" Where " + columnNames[0].ToString() + " = " + columnValues[0].ToString());

            return DAO.InsightsDAO.AddDetailsforMasterData(query.ToString());
        }

        /// <summary>
        /// AddUpdateDetailsforMasterData - To delete records from Database
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="columnValue"></param>
        /// <returns></returns>
        public static bool AddUpdateDetailsforMasterData(string tableName, string columnName, int columnValue)
        {
            string query = "Delete from " + tableName + " Where " + columnName + " = " + columnValue.ToString();
            return DAO.InsightsDAO.AddDetailsforMasterData(query);
        }

        #endregion

        #region Other Common Functions

        /// <summary>
        /// GetColumnNames
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static ArrayList GetColumnNames(string tableName)
        {
            string query = "SELECT col.name FROM sys.tables tab INNER JOIN sys.columns col ON tab.object_id = col.object_id WHERE (tab.name = '" + tableName + "')";
            DataSet columnsDataSet = DAO.InsightsDAO.GetColumnNames(query);
            ArrayList columnNames = new ArrayList();

            foreach (DataRow row in columnsDataSet.Tables[0].Rows)
            {
                columnNames.Add(row[0]);
            }

            return columnNames;
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
        public static void BulkTableInsert(DataTable objDS, string tableName, string user)
        {
            //Change the column mapping first.
            objDS.TableName = tableName;
            DAO.InsightsDAO.BulkTableInsert(objDS, user, GetColumnNames(tableName));
            //http://www.codeproject.com/KB/database/generic_OpenXml.aspx
        }

        #endregion

        #region User Details

        /// <summary>
        /// GetUserDetails
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool GetUserDetails(string userName, string password)
        {
            string query = "Select * from UserCredentials where UserName = '" + userName + "' And Password = '" +  password + "'";

            DataSet userDetails = DAO.InsightsDAO.ExecuteQuery(query);

            if (userDetails.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
