using System;
using System.Data;
using System.Text;
using System.Collections;
using Adobe.Localization.Insights.DataLayer;
using Adobe.Localization.Insights.Common;
using COM = Adobe.Localization.Insights.Common.Common;
using DTO = Adobe.Localization.Insights.Common.DataTransferObjects;
using TS = Adobe.Localization.Insights.DataLayer.TestStudioComponents;

namespace Adobe.Localization.Insights.DataLayer.DataAccessObjects
{
    /// <summary>
    /// DAObjects
    /// </summary>
    public class DAObjects
    {
        #region Variable Declaration

        // Set the connection string        
        private static string connectionString = string.Empty;
        //SqlTransaction transaction = null;

        #endregion

        #region User Details

        /// <summary>
        /// AddNewUser
        /// </summary>
        /// <param name="projectSearchDTO"></param>
        /// <returns></returns>
        public static bool AddNewUser(string loginID)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.AddNewUser(loginID));
        }

        /// <summary>
        /// GetUserDetails
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static DataSet GetUserDetails(string loginID)
        {
            return common.ExecuteMySQLQuery(Query.QueryDAO.GetUserDetails(loginID));
        }

        /// <summary>
        /// GetUserDetails
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static DataSet GetUserDetails(DTO.Users userData)
        {
            DataSet ds = common.ExecuteMySQLQuery(Query.QueryDAO.GetUserDetails(userData));
            return AddRowandColumntoTable(ds, 0);
        }

        /// <summary>
        /// AddUserDetails
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static bool AddUserDetails(DTO.Users userData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.AddUserDetails(userData));
        }

        /// <summary>
        /// UpdateUserDetails
        /// </summary>
        /// <param name="transferData"></param>
        /// <returns></returns>
        public static bool UpdateUserDetails(DTO.Users userData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.UpdateUserDetails(userData));
        }

        /// <summary>
        /// DeleteUserDetails
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static bool DeleteUserDetails(DTO.Users userData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.DeleteUserDetails(userData));
        }

        /// <summary>
        /// UpdateProfileAndPreferences
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static bool UpdateProfileAndPreferences(DTO.Users userData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.UpdateProfileAndPreferences(userData));
        }

        /// <summary>
        /// GetTeamFeedback
        /// </summary>
        /// <returns></returns>
        public static DataSet GetTeamFeedback(DTO.Users userData)
        {
            return common.ExecuteMySQLQuery(Query.QueryDAO.GetTeamFeedback(userData));
        }

        /// <summary>
        /// GetTeamFeedbackLogged
        /// </summary>
        /// <returns></returns>
        public static DataSet GetTeamFeedbackLogged(DTO.Users userData)
        {
            DataSet ds = common.ExecuteMySQLQuery(Query.QueryDAO.GetTeamFeedback(userData));
            return AddRowandColumntoTable(ds, 0);
        }

        /// <summary>
        /// AddTeamFeedback
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static bool AddTeamFeedback(DTO.Users userData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.AddTeamFeedback(userData));
        }

        /// <summary>
        /// UpdateTeamFeedback
        /// </summary>
        /// <param name="transferData"></param>
        /// <returns></returns>
        public static bool UpdateTeamFeedback(DTO.Users userData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.UpdateTeamFeedback(userData));
        }

        /// <summary>
        /// DeleteTeamFeedback
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static bool DeleteTeamFeedback(DTO.Users userData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.DeleteTeamFeedback(userData));
        }

        #endregion

        #region Products

        /// <summary>
        /// GetUserProducts
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static DataSet GetUserProducts(DTO.Product productData)
        {
            return common.ExecuteMySQLQuery(Query.QueryDAO.GetUserProducts(productData));
        }

        /// <summary>
        /// GetProducts
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static DataSet GetProducts(DTO.Product productData)
        {
            return common.ExecuteMySQLQuery(Query.QueryDAO.GetProducts(productData));
        }

        /// <summary>
        /// GetProductVersion
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static DataSet GetProductVersion(DTO.Product productData)
        {
            return common.ExecuteMySQLQuery(Query.QueryDAO.GetProductVersion(productData));
        }

        /// <summary>
        /// GetProductUsers
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static DataSet GetProductUsers(DTO.Product productData)
        {
            DataSet details = common.ExecuteMySQLQuery(Query.QueryDAO.GetProductUsers(productData));
            return AddRowandColumntoTable(details, 0);
        }

        /// <summary>
        /// SaveUserProductDetails
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static bool SaveUserProductDetails(DTO.Users userData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.SaveUserProductDetails(userData));
        }

        /// <summary>
        /// AddProductVersion
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static bool AddProductVersion(DTO.Product productData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.AddProductVersion(productData));
        }

        /// <summary>
        /// UpdateProductVersion
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static bool UpdateProductVersion(DTO.Product productData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.UpdateProductVersion(productData));
        }

        /// <summary>
        /// CopyProductVersionData
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static bool CopyProductVersionData(DTO.Product productData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.CopyProductVersionData(productData));
        }

        /// <summary>
        /// AddProductLinks
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static bool AddProductLinks(DTO.Product productData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.AddProductLinks(productData));
        }

        /// <summary>
        /// UpdateProductLinks
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static bool UpdateProductLinks(DTO.Product productData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.UpdateProductLinks(productData));
        }

        /// <summary>
        /// DeleteProductLinks
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static bool DeleteProductLinks(DTO.Product productData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.DeleteProductLinks(productData));
        }

        /// <summary>
        /// GetProductSprints
        /// </summary>
        /// <param name="projectPhaseData"></param>
        /// <returns></returns>
        public static DataSet GetProductSprints(DTO.Product productData)
        {
            return common.ExecuteMySQLQuery(Query.QueryDAO.GetProductSprints(productData));
        }

        /// <summary>
        /// AddProductSprint
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static bool AddProductSprint(DTO.Product productData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.AddProductSprint(productData));
        }

        /// <summary>
        /// UpdateProductSprint
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static bool UpdateProductSprint(DTO.Product productData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.UpdateProductSprint(productData));
        }

        ///// <summary>
        ///// AddProductOwnerforMasterData
        ///// </summary>
        ///// <param name="productData"></param>
        ///// <returns></returns>
        //public static bool AddProductOwnerforMasterData(DTO.Product productData)
        //{
        //    return common.ExecuteMySQLNonQuery(Query.QueryDAO.AddProductOwnerforMasterData(productData));
        //}

        ///// <summary>
        ///// UpdateProductOwnerforMasterData
        ///// </summary>
        ///// <param name="productData"></param>
        ///// <returns></returns>
        //public static bool UpdateProductOwnerforMasterData(DTO.Product productData)
        //{
        //    return common.ExecuteMySQLNonQuery(Query.QueryDAO.UpdateProductOwnerforMasterData(productData));
        //}

        ///// <summary>
        ///// DeleteProductOwnerforMasterData
        ///// </summary>
        ///// <param name="productData"></param>
        ///// <returns></returns>
        //public static bool DeleteProductOwnerforMasterData(DTO.Product productData)
        //{
        //    return common.ExecuteMySQLNonQuery(Query.QueryDAO.DeleteProductOwnerforMasterData(productData));
        //}

        #endregion

        #region Vendors

        /// <summary>
        /// GetProjectPhaseVendors
        /// </summary>
        /// <param name="phasesData"></param>
        /// <returns></returns>
        public static DataSet GetProjectPhaseVendors(DTO.ProjectPhases phasesData)
        {
            return common.ExecuteMySQLQuery(Query.QueryDAO.GetProjectPhaseVendors(phasesData));
        }

        /// <summary>
        /// GetProjectPhaseVendorUsers
        /// </summary>
        /// <param name="phasesData"></param>
        /// <returns></returns>
        public static DataSet GetProjectPhaseVendorUsers(DTO.ProjectPhases phasesData)
        {
            return common.ExecuteMySQLQuery(Query.QueryDAO.GetProjectPhaseVendorUsers(phasesData));
        }

        /// <summary>
        /// GetVendorEfforts
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DataSet GetVendorEfforts(DTO.WSRData wsrData)
        {
            return common.ExecuteMySQLQuery(Query.QueryDAO.GetVendorEfforts(wsrData));
        }

        /// <summary>
        /// AddUpdateVendorEfforts
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool AddUpdateVendorEfforts(DTO.WSRData wsrData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.AddUpdateVendorEfforts(wsrData));
        }

        /// <summary>
        /// GetVendorDetails
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static DataSet GetVendorDetails(DTO.Users userData)
        {
            return common.ExecuteMySQLQuery(Query.QueryDAO.GetVendorDetails(userData));
        }

        /// <summary>
        /// UpdateUserVendor
        /// </summary>
        /// <param name="accessData"></param>
        public static void UpdateUserVendor(DTO.Users accessData)
        {
            common.ExecuteMySQLNonQuery(Query.QueryDAO.UpdateUserVendor(accessData));
        }

        /// <summary>
        /// AddUserVendor
        /// </summary>
        /// <param name="accessData"></param>
        public static void AddUserVendor(DTO.Users accessData)
        {
            common.ExecuteMySQLNonQuery(Query.QueryDAO.AddUserVendor(accessData));
        }

        #endregion

        #region Week Data

        /// <summary>
        /// GetVendor
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static DataSet GetWeeksInfo()
        {
            DataSet dsWeeksInfo = null;
            int counter = 0;
            DateTime weekStartDate = COM.GetFirstDayOfWeek("01/01/2011");
            DateTime weekEndDate;

            while (true && counter < 100)
            {
                dsWeeksInfo = common.ExecuteMySQLQuery(Query.QueryDAO.GetWeekInfo(""));

                if (dsWeeksInfo.Tables[0].Rows.Count > 0)
                {
                    DataRow[] drActiveCol = dsWeeksInfo.Tables[0].Select("isActive=1");

                    if (drActiveCol.Length > 0)
                        return common.ExecuteMySQLQuery(Query.QueryDAO.GetWeekInfo(drActiveCol[0][WebConstants.COL_WEEK_ID].ToString()));
                    else
                    {
                        DataRow drLastWeek = dsWeeksInfo.Tables[0].Rows[dsWeeksInfo.Tables[0].Rows.Count - 1];
                        weekStartDate = (DateTime)drLastWeek[WebConstants.COL_WEEK_START_DATE];
                        weekEndDate = (DateTime)drLastWeek[WebConstants.COL_WEEK_END_DATE];

                        if (System.DateTime.Now.Subtract(weekEndDate).Days <= 4)
                            return common.ExecuteMySQLQuery(Query.QueryDAO.GetWeekInfo(drLastWeek[WebConstants.COL_WEEK_ID].ToString()));
                        else if (!AddUpdateWeekInfo(weekStartDate.AddDays(7)))
                            return null;
                    }
                }
                else if (!AddUpdateWeekInfo(weekStartDate))
                    return null;

                counter++;
            }

            return null;
        }

        /// <summary>
        /// AddUpdateWeekInfo
        /// </summary>
        /// <param name="wkStartDate"></param>
        /// <returns></returns>
        public static bool AddUpdateWeekInfo(DateTime wkStartDate)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.AddUpdateWeekInfo(wkStartDate));
        }

        /// <summary>
        /// GetReportingWeek
        /// </summary>
        /// <param name="reportingType"></param>
        /// <returns></returns>
        public static DataSet GetReportingWeek(string reportingType)
        {
            return common.ExecuteMySQLQuery(Query.QueryDAO.GetReportingWeek(reportingType));
        }

        #endregion

        #region Project Roles

        /// <summary>
        /// GetUserProjectRoles
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static DataSet GetUserProjectRoles(DTO.Users userData)
        {
            return common.ExecuteMySQLQuery(Query.QueryDAO.GetUserProjectRoles(userData));
        }

        /// <summary>
        /// GetProjectRoles
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static DataSet GetProjectRoles(DTO.Users userData)
        {
            return common.ExecuteMySQLQuery(Query.QueryDAO.GetProjectRoles(userData));
        }

        /// <summary>
        /// GetUserProductRoles
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static DataSet GetUserProductRoles(DTO.Users userData)
        {
            DataSet ds = common.ExecuteMySQLQuery(Query.QueryDAO.GetUserProductRoles(userData));
            return AddRowandColumntoTable(ds, 0);
        }

        /// <summary>
        /// GetUserProducts
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static DataSet GetUserProducts(DTO.Users userData)
        {
            return common.ExecuteMySQLQuery(Query.QueryDAO.GetUserProducts(userData));
        }

        /// <summary>
        /// GetUserRoles
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static DataSet GetUserRoles(DTO.Users userData)
        {
            return common.ExecuteMySQLQuery(Query.QueryDAO.GetUserRoles(userData));
        }

        /// <summary>
        /// GetUserScreenAccess
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static DataSet GetUserScreenAccess(DTO.Users userData)
        {
            return common.ExecuteMySQLQuery(Query.QueryDAO.GetUserScreenAccess(userData));
        }

        /// <summary>
        /// GetUserProductsPreferred
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static DataSet GetUserProductsPreferred(DTO.Users userData)
        {
            return common.ExecuteMySQLQuery(Query.QueryDAO.GetUserProductsPreferred(userData));
        }

        /// <summary>
        /// AddUpdateUserProjectRoles
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static bool AddUpdateUserProjectRoles(DTO.Users userData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.AddUpdateUserProjectRoles(userData));
        }

        /// <summary>
        /// AddUserProducts
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static bool AddUserProducts(DTO.Users userData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.AddUserProducts(userData));
        }

        /// <summary>
        /// UpdateUserProducts
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static bool UpdateUserProducts(DTO.Users userData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.UpdateUserProducts(userData));
        }

        /// <summary>
        /// DeleteUserProducts
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static bool DeleteUserProducts(DTO.Users userData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.DeleteUserProducts(userData));
        }

        #endregion

        #region WSR Data

        /// <summary>
        /// GetProductWSRParameters
        /// </summary>
        /// <param name="wsrParametersData"></param>
        /// <returns></returns>
        public static DataSet GetProductWSRParameters(DTO.WSRData wsrParametersData)
        {
            return common.ExecuteMySQLQuery(Query.QueryDAO.GetProductWSRParameters(wsrParametersData));
        }

        /// <summary>
        /// GetWSRData
        /// </summary>
        /// <param name="userProductID"></param>
        /// <param name="userVendorID"></param>
        /// <param name="weekID"></param>
        /// <returns></returns>
        public static DataSet GetWSRData(DTO.WSRData dtoWSRData)
        {
            return common.ExecuteMySQLQuery(Query.QueryDAO.GetWSRData(dtoWSRData));
        }

        /// <summary>
        /// GetWSRDetails
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="wsrDataID"></param>
        /// <returns></returns>
        public static DataSet GetWSRDetails(DTO.WSRData dtoWSRData)
        {
            DataSet dsWSRDetails = new DataSet();

            dsWSRDetails.Tables.Add(common.ExecuteMySQLQuery(Query.QueryDAO.GetWSRActualEfforts(dtoWSRData)).Tables[0].Copy());
            dsWSRDetails.Tables[0].TableName = WebConstants.TBL_TEMP_ACTUAL_EFFORTS;
            dsWSRDetails.Tables.Add(common.ExecuteMySQLQuery(Query.QueryDAO.GetWSRRevisedEfforts(dtoWSRData)).Tables[0].Copy());
            dsWSRDetails.Tables[1].TableName = WebConstants.TBL_TEMP_REVISED_EFFORTS;

            if (dtoWSRData.ReportingType == WebConstants.DEF_VAL_REPORTING_TYPE_TOTAL)
            {
                dsWSRDetails.Tables.Add(common.ExecuteMySQLQuery(Query.QueryDAO.GetWSRActualTCsExecuted(dtoWSRData)).Tables[0].Copy());
                dsWSRDetails.Tables[2].TableName = WebConstants.TBL_TEMP_ACTUAL_TC_EXECUTED;
            }

            DTO.ProjectPhases projectPhasesData = new DTO.ProjectPhases();
            projectPhasesData.ProductID = dtoWSRData.ProductID;
            projectPhasesData.ProductVersionID = dtoWSRData.ProductVersionID;
            projectPhasesData.PhaseTypeID = dtoWSRData.PhaseTypeID;
            projectPhasesData.ProductSprintID = dtoWSRData.ProductSprintID;
            projectPhasesData.TestingTypeID = dtoWSRData.TestingTypeID;
            projectPhasesData.ProjectPhaseID = dtoWSRData.ProjectPhaseID;
            projectPhasesData.VendorID = dtoWSRData.VendorID;

            dsWSRDetails.Tables.Add(common.ExecuteMySQLQuery(Query.QueryDAO.GetProjectLocales(projectPhasesData)).Tables[0].Copy());
            dsWSRDetails.Tables[dsWSRDetails.Tables.Count - 1].TableName = WebConstants.TBL_TEMP_VENDOR_LOCALES;

            dsWSRDetails.Tables.Add(common.ExecuteMySQLQuery(Query.QueryDAO.GetWSRResourcesCount(dtoWSRData)).Tables[0].Copy());
            dsWSRDetails.Tables[dsWSRDetails.Tables.Count - 1].TableName = WebConstants.TBL_WSR_RESOURCE_DATA;

            return dsWSRDetails;
        }

        /// <summary>
        /// GetWSRDetails
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="wsrDataID"></param>
        /// <returns></returns>
        public static DataSet GetWSRDetails(string tableName, DTO.WSRData dtoWSRData)
        {
            DataSet ds_wsrDetails = common.ExecuteMySQLQuery(Query.QueryDAO.GetWSRDetails(tableName, dtoWSRData));

            ds_wsrDetails.Tables[0].TableName = tableName;

            return ds_wsrDetails;
        }

        /// <summary>
        /// UpdateProductWSRParameters
        /// </summary>
        /// <param name="dtoWSRData"></param>
        /// <returns></returns>
        public static bool UpdateProductWSRParameters(DTO.WSRData dtoWSRData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.UpdateProductWSRParameters(dtoWSRData));
        }

        /// <summary>
        /// SaveWSRData
        /// </summary>
        /// <param name="dtoWSRData"></param>
        /// <returns></returns>
        public static bool AddUpdateWSRData(DTO.WSRData dtoWSRData)
        {
            if (dtoWSRData.WsrDataID != "")
            {
                return common.ExecuteMySQLNonQuery(Query.QueryDAO.UpdateWSRData(dtoWSRData));
            }
            else
            {
                return common.ExecuteMySQLNonQuery(Query.QueryDAO.AddWSRData(dtoWSRData));
            }
        }

        /// <summary>
        /// SaveWSRDetails
        /// </summary>
        /// <param name="dtoWSRData"></param>
        /// <returns></returns>
        public static bool AddUpdateWSRDetails(DTO.WSRData dtoWSRData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.AddUpdateWSRDetails(dtoWSRData));
        }

        /// <summary>
        /// SavePrevWeekPrioritiesData
        /// </summary>
        /// <param name="dtoWSRData"></param>
        public static bool SavePrevWeekPrioritiesData(DTO.WSRData dtoWSRData)
        {
            if (common.ExecuteMySQLNonQuery(Query.QueryDAO.DeletePrevWeekDelivData(dtoWSRData)))
            {
                if (dtoWSRData.PrevWeekDeliverables != null)
                {
                    if (dtoWSRData.PrevWeekDeliverables.Tables.Count > 0)
                    {
                        foreach (DataRow dr in dtoWSRData.PrevWeekDeliverables.Tables[0].Select())
                        {
                            common.ExecuteMySQLNonQuery(Query.QueryDAO.AddPrevWeekDelivData(dr, dtoWSRData));
                        }
                    }
                }
            }

            return true;
        }

        #endregion

        #region Master Data

        /// <summary>
        /// AddRowandColumntoTable
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        public static DataSet AddRowandColumntoTable(DataSet details, int tableNo)
        {
            if (details == null)
                return details;

            DataTable dtDetails = details.Tables[tableNo];

            DataColumn col = new DataColumn(WebConstants.COL_TEMP_ROW, Type.GetType(WebConstants.STR_SYSTEM_INT32));
            dtDetails.Columns.Add(col);

            for (int rowCount = 0; rowCount < dtDetails.Rows.Count; rowCount++)
            {
                dtDetails.Rows[rowCount][WebConstants.COL_TEMP_ROW] = rowCount + 1;
            }

            DataRow dr = dtDetails.NewRow();
            dr[0] = 0;
            dtDetails.Rows.InsertAt(dr, 0);

            return details;
        }

        /// <summary>
        /// GetDetailsforMasterData
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnNames"></param>
        /// <returns></returns>
        public static DataSet GetDetailsforMasterData(DTO.MasterData masterData)
        {
            DataSet details = common.ExecuteMySQLQuery(Query.QueryDAO.GetDetailsforMasterData(masterData));
            return AddRowandColumntoTable(details, 0);
        }

        /// <summary>
        /// AddDetailsforMasterData - To Add new record to Database
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="columnValue"></param>
        /// <returns></returns>
        public static bool AddDetailsforMasterData(DTO.MasterData masterData)
        {
            if (masterData.TableName == WebConstants.TBL_PRODUCT)
            {
                if (common.ExecuteMySQLNonQuery(Query.QueryDAO.AddDetailsforMasterData(masterData)))
                {
                    DTO.Product productData = new DTO.Product();
                    productData.DataHeader = masterData.DataHeader;
                    productData.ProductID = GetDetailsforMasterData(masterData).Tables[0].Select(WebConstants.COL_PRODUCT + " = '" + masterData.Description + "'")[0][WebConstants.COL_PRODUCT_ID].ToString();
                    productData.ProductCodeName = WebConstants.DEF_VALUE;
                    productData.ProductVersion = WebConstants.DEF_VALUE;
                    productData.ReleaseTypeID = "1";
                    productData.IsActive = "1";
                    return AddProductVersion(productData);
                }
                else
                    return false;
            }
            else
                return common.ExecuteMySQLNonQuery(Query.QueryDAO.AddDetailsforMasterData(masterData));
        }

        /// <summary>
        /// UpdateDetailsforMasterData - To update records in Database
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="columnValue"></param>
        /// <returns></returns>
        public static bool UpdateDetailsforMasterData(DTO.MasterData masterData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.UpdateDetailsforMasterData(masterData));
        }

        /// <summary>
        /// DeleteDetailsforMasterData - To delete records from Database
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="columnValue"></param>
        /// <returns></returns>
        public static bool DeleteDetailsforMasterData(DTO.MasterData masterData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.DeleteDetailsforMasterData(masterData));
        }

        /// <summary>
        /// AddVendorDetailsforMasterData
        /// </summary>
        /// <param name="masterData"></param>
        /// <returns></returns>
        public static bool AddVendorDetailsforMasterData(DTO.MasterData masterData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.AddVendorDetailsforMasterData(masterData));
        }

        /// <summary>
        /// UpdateVendorDetailsforMasterData
        /// </summary>
        /// <param name="masterData"></param>
        /// <returns></returns>
        public static bool UpdateVendorDetailsforMasterData(DTO.MasterData masterData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.UpdateVendorDetailsforMasterData(masterData));
        }

        /// <summary>
        /// AddUserDetailsforMasterData
        /// </summary>
        /// <param name="masterData"></param>
        /// <returns></returns>
        public static bool AddUserDetailsforMasterData(DTO.MasterData masterData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.AddUserDetailsforMasterData(masterData));
        }

        /// <summary>
        /// UpdateUserDetailsforMasterData
        /// </summary>
        /// <param name="masterData"></param>
        /// <returns></returns>
        public static bool UpdateUserDetailsforMasterData(DTO.MasterData masterData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.UpdateUserDetailsforMasterData(masterData));
        }

        /// <summary>
        /// GetUserVendorAssociations
        /// </summary>
        /// <param name="masterData"></param>
        /// <returns></returns>
        public static DataSet GetUserVendorAssociations(DTO.MasterData masterData)
        {
            DataSet details = common.ExecuteMySQLQuery(Query.QueryDAO.GetUserVendorAssociations(masterData));
            return AddRowandColumntoTable(details, 0);
        }

        /// <summary>
        /// AddUserVendorAssociationsDetailsforMasterData
        /// </summary>
        /// <param name="masterData"></param>
        /// <returns></returns>
        public static bool AddUserVendorAssociationsDetailsforMasterData(DTO.MasterData masterData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.AddUserVendorAssociationsDetailsforMasterData(masterData));
        }

        /// <summary>
        /// UpdateUserVendorAssociationsDetailsforMasterData
        /// </summary>
        /// <param name="masterData"></param>
        /// <returns></returns>
        public static bool UpdateUserVendorAssociationsDetailsforMasterData(DTO.MasterData masterData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.UpdateUserVendorAssociationsDetailsforMasterData(masterData));
        }

        #endregion

        #region Product Version Details

        /// <summary>
        /// GetAllLocales
        /// </summary>
        /// <returns></returns>
        public static DataSet GetAllLocales()
        {
            return common.ExecuteMySQLQuery(Query.QueryDAO.GetAllLocales());
        }

        /// <summary>
        /// GetProductVersionLocales
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static DataSet GetProductVersionLocales(DTO.Product productData)
        {
            return common.ExecuteMySQLQuery(Query.QueryDAO.GetProductVersionLocales(productData));
        }

        /// <summary>
        /// GetAllPlatforms
        /// </summary>
        /// <returns></returns>
        public static DataSet GetAllPlatforms()
        {
            return common.ExecuteMySQLQuery(Query.QueryDAO.GetAllPlatforms());
        }

        /// <summary>
        /// GetProductVersionPlatforms
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static DataSet GetProductVersionPlatforms(DTO.Product productData)
        {
            return common.ExecuteMySQLQuery(Query.QueryDAO.GetProductVersionPlatforms(productData));
        }

        /// <summary>
        /// UpdateProductVersionLocalesAndPlatforms
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static bool UpdateProductVersionLocalesAndPlatforms(DTO.Product productData)
        {
            if (common.ExecuteMySQLNonQuery(Query.QueryDAO.UpdateProductVersionLocales(productData)))
                return common.ExecuteMySQLNonQuery(Query.QueryDAO.UpdateProductVersionPlatforms(productData));

            return false;
        }

        /// <summary>
        /// UpdateAboutProductDetails
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static bool UpdateAboutProductDetails(DTO.Product productData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.UpdateAboutProductDetails(productData));
        }

        #endregion

        #region Project Phases

        /// <summary>
        /// GetProjectPhases
        /// </summary>
        /// <param name="projectPhaseData"></param>
        /// <returns></returns>
        public static DataSet GetProjectPhases(DTO.ProjectPhases projectPhaseData)
        {
            return common.ExecuteMySQLQuery(Query.QueryDAO.GetProjectPhases(projectPhaseData));
        }

        /// <summary>
        /// GetProjectLocales
        /// </summary>
        /// <param name="projectPhaseData"></param>
        /// <returns></returns>
        public static DataSet GetProjectLocales(DTO.ProjectPhases projectPhaseData)
        {
            return common.ExecuteMySQLQuery(Query.QueryDAO.GetProjectLocales(projectPhaseData));
        }

        /// <summary>
        /// GetProjectPlatforms
        /// </summary>
        /// <param name="projectPhaseData"></param>
        /// <returns></returns>
        public static DataSet GetProjectPlatforms(DTO.ProjectPhases projectPhaseData)
        {
            return common.ExecuteMySQLQuery(Query.QueryDAO.GetProjectPlatforms(projectPhaseData));
        }

        /// <summary>
        /// GetPhaseExecutableTestCases
        /// </summary>
        /// <param name="projectPhaseData"></param>
        /// <returns></returns>
        public static DataSet GetPhaseExecutableTestCases(DTO.ProjectPhases projectPhaseData)
        {
            return common.ExecuteMySQLQuery(Query.QueryDAO.GetPhaseExecutableTestCases(projectPhaseData));
        }

        /// <summary>
        /// GetPhaseCoverageDetails
        /// </summary>
        /// <param name="projectPhaseData"></param>
        /// <returns></returns>
        public static DataSet GetPhaseCoverageDetails(DTO.ProjectPhases projectPhaseData)
        {
            return common.ExecuteMySQLQuery(Query.QueryDAO.GetPhaseCoverageDetails(projectPhaseData));
        }

        /// <summary>
        /// GetPhaseCoverageAllDetails
        /// </summary>
        /// <param name="projectPhaseData"></param>
        /// <returns></returns>
        public static DataSet GetPhaseCoverageAllDetails(DTO.ProjectPhases projectPhaseData)
        {
            return common.ExecuteMySQLQuery(Query.QueryDAO.GetPhaseCoverageAllDetails(projectPhaseData));
        }

        /// <summary>
        /// GetLocaleVsPlatformMatrixData
        /// </summary>
        /// <param name="projectPhasesData"></param>
        /// <returns></returns>
        public static DataSet GetLocaleVsPlatformMatrixData(DTO.ProjectLocaleVsPlatformMatrix matrixData)
        {
            return common.ExecuteMySQLQuery(Query.QueryDAO.GetLocaleVsPlatformMatrixData(matrixData));
        }

        /// <summary>
        /// GetProjectPhaseSummary
        /// </summary>
        /// <param name="projectPhasesData"></param>
        /// <returns></returns>
        public static DataSet GetProjectPhaseSummary(DTO.ProjectPhases projectPhaseData)
        {
            return common.ExecuteMySQLQuery(Query.QueryDAO.GetProjectPhases(projectPhaseData));
        }

        /// <summary>
        /// AddProjectPhase
        /// </summary>
        /// <param name="projectPhaseData"></param>
        /// <returns></returns>
        public static bool AddProjectPhase(DTO.ProjectPhases projectPhaseData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.AddProjectPhase(projectPhaseData));
        }

        /// <summary>
        /// UpdateProjectPhase
        /// </summary>
        /// <param name="projectPhaseData"></param>
        /// <returns></returns>
        public static bool UpdateProjectPhase(DTO.ProjectPhases projectPhaseData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.UpdateProjectPhase(projectPhaseData));
        }

        /// <summary>
        /// CopyProjectPhasesData
        /// </summary>
        /// <param name="projectPhaseData"></param>
        /// <returns></returns>
        public static bool CopyProjectPhasesData(DTO.ProjectPhases projectPhaseData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.CopyProjectPhasesData(projectPhaseData));
        }

        /// <summary>
        /// AddProjectPhaseCoverages
        /// </summary>
        /// <param name="projectPhaseData"></param>
        /// <returns></returns>
        public static bool AddProjectPhaseCoverages(DTO.ProjectPhases projectPhaseData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.AddProjectPhaseCoverages(projectPhaseData));
        }

        /// <summary>
        /// UpdateProjectPhaseCoverages
        /// </summary>
        /// <param name="projectPhaseData"></param>
        /// <returns></returns>
        public static bool UpdateProjectPhaseCoverages(DTO.ProjectPhases projectPhaseData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.UpdateProjectPhaseCoverages(projectPhaseData));
        }

        /// <summary>
        /// DeleteProjectPhaseCoverages
        /// </summary>
        /// <param name="projectPhaseData"></param>
        /// <returns></returns>
        public static bool DeleteProjectPhaseCoverages(DTO.ProjectPhases projectPhaseData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.DeleteProjectPhaseCoverages(projectPhaseData));
        }

        /// <summary>
        /// SaveProjectLocalesVsPlatformsMatrix
        /// </summary>
        /// <param name="projectPhaseData"></param>
        /// <returns></returns>
        public static bool SaveProjectLocalesVsPlatformsMatrix(DTO.ProjectPhases projectPhaseData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.SaveProjectLocalesVsPlatformsMatrix(projectPhaseData));
        }

        /// <summary>
        /// CopyLocalesVsPlatformsMatrix
        /// </summary>
        /// <param name="projectPhaseData"></param>
        /// <returns></returns>
        public static bool CopyLocalesVsPlatformsMatrix(DTO.ProjectPhases newProjectPhaseData, DTO.ProjectPhases oldProjectPhaseData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.CopyLocalesVsPlatformsMatrix(newProjectPhaseData, oldProjectPhaseData));
        }

        /// <summary>
        /// UpdateProjectLocalesVsPlatformsMatrix
        /// </summary>
        /// <param name="projectPhaseData"></param>
        /// <returns></returns>
        public static bool UpdateProjectLocalesVsPlatformsMatrix(DTO.ProjectPhases projectPhaseData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.UpdateProjectLocalesVsPlatformsMatrix(projectPhaseData));
        }

        #endregion

        #region Product WindUp

        /// <summary>
        /// GetProjectBuildDetails
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static DataSet GetProjectBuildDetails(DTO.Product productData)
        {
            return common.ExecuteMySQLQuery(Query.QueryDAO.GetProjectBuildDetails(productData));
        }

        /// <summary>
        /// AddProjectBuildDetails
        /// </summary>
        /// <returns></returns>
        public static bool AddProjectBuildDetails(DTO.Product productData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.AddProjectBuildDetails(productData));
        }

        /// <summary>
        /// UpdateHolidaysListData
        /// </summary>
        /// <returns></returns>
        public static bool UpdateProjectBuildDetails(DTO.Product productData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.UpdateProjectBuildDetails(productData));
        }

        /// <summary>
        /// DeleteProjectBuildDetails
        /// </summary>
        /// <returns></returns>
        public static bool DeleteProjectBuildDetails(DTO.Product productData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.DeleteProjectBuildDetails(productData));
        }

        /// <summary>
        /// GetProjectBuildLocales
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static DataSet GetProjectBuildLocales(DTO.Product productData)
        {
            return common.ExecuteMySQLQuery(Query.QueryDAO.GetProjectBuildLocales(productData));
        }

        /// <summary>
        /// AddUpdateProjectBuildLocales
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static bool AddUpdateProjectBuildLocales(DTO.Product productData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.AddUpdateProjectBuildLocales(productData));
        }

        /// <summary>
        /// GetProjectWindUpDetails
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static DataSet GetProjectWindUpDetails(DTO.Product productData)
        {
            return common.ExecuteMySQLQuery(Query.QueryDAO.GetProjectWindUpDetails(productData));
        }

        ///// <summary>
        ///// GetProjectGMDetails
        ///// </summary>
        ///// <param name="productData"></param>
        ///// <returns></returns>
        //public static DataSet GetProjectGMDetails(DTO.Product productData)
        //{
        //    return AddRowandColumntoTable(common.ExecuteMySQLQuery(Query.QueryDAO.GetProjectGMDetails(productData)), 0);
        //}

        /// <summary>
        /// AddProjectWindUpDetails
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static bool AddProjectWindUpDetails(DTO.Product productData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.AddProjectWindUpDetails(productData));
        }

        /// <summary>
        /// UpdateProjectWindUpDetails
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static bool UpdateProjectWindUpDetails(DTO.Product productData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.UpdateProjectWindUpDetails(productData));
        }

        /// <summary>
        /// SubmitWindUpDetails
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static bool SubmitWindUpDetails(DTO.Product productData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.SubmitWindUpDetails(productData));
        }

        ///// <summary>
        ///// AddProjectGMDetails
        ///// </summary>
        ///// <param name="productData"></param>
        ///// <returns></returns>
        //public static bool AddProjectGMDetails(DTO.Product productData)
        //{
        //    return common.ExecuteMySQLNonQuery(Query.QueryDAO.AddProjectGMDetails(productData));
        //}

        /// <summary>
        /// UpdateProjectGMDetails
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static bool UpdateProjectGMDetails(DTO.Product productData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.UpdateProjectGMDetails(productData));
        }

        #endregion

        #region Holidays

        /// <summary>
        /// GetHolidaysDate
        /// </summary>
        /// <param name="holidayData"></param>
        /// <returns></returns>
        public static DataSet GetHolidaysDate(DTO.Holidays holidayData)
        {
            return common.ExecuteMySQLQuery(Query.QueryDAO.GetHolidaysDate(holidayData));
        }

        /// <summary>
        /// GetHolidaysList
        /// </summary>
        /// <param name="holidayData"></param>
        /// <returns></returns>
        public static DataSet GetHolidaysList(DTO.Holidays holidayData)
        {
            DataSet ds = common.ExecuteMySQLQuery(Query.QueryDAO.GetHolidaysList(holidayData));
            return AddRowandColumntoTable(ds, 0);
        }

        /// <summary>
        /// AddHolidaysListData
        /// </summary>
        /// <param name="holidayData"></param>
        /// <returns></returns>
        public static bool AddHolidaysListData(DTO.Holidays holidayData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.AddHolidaysListData(holidayData));
        }

        /// <summary>
        /// UpdateHolidaysListData
        /// </summary>
        /// <param name="holidayData"></param>
        /// <returns></returns>
        public static bool UpdateHolidaysListData(DTO.Holidays holidayData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.UpdateHolidaysListData(holidayData));
        }

        /// <summary>
        /// DeleteHolidaysListData
        /// </summary>
        /// <param name="holidayData"></param>
        /// <returns></returns>
        public static bool DeleteHolidaysListData(DTO.Holidays holidayData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.DeleteHolidaysListData(holidayData));
        }

        #endregion

        #region Screens

        /// <summary>
        /// GetScreensData
        /// </summary>
        /// <returns></returns>
        public static DataSet GetScreensData()
        {
            DataSet dsScreenDetails = new DataSet();

            DTO.Screens screenData = new DTO.Screens();
            screenData.IsAllScreens = false;

            dsScreenDetails.Tables.Add(common.ExecuteMySQLQuery(Query.QueryDAO.GetScreensDetails(screenData)).Tables[0].Copy());
            dsScreenDetails.Tables[0].TableName = WebConstants.TBL_SCREENS;
            dsScreenDetails.Tables.Add(common.ExecuteMySQLQuery(Query.QueryDAO.GetProjectRoles()).Tables[0].Copy());
            dsScreenDetails.Tables[1].TableName = WebConstants.TBL_PROJECT_ROLES;
            dsScreenDetails.Tables.Add(common.ExecuteMySQLQuery(Query.QueryDAO.GetScreenAccess()).Tables[0].Copy());
            dsScreenDetails.Tables[2].TableName = WebConstants.TBL_SCREEN_ACCESS_LEVELS;

            return AddRowandColumntoTable(dsScreenDetails, 0);
        }

        /// <summary>
        /// GetScreensDetails
        /// </summary>
        /// <returns></returns>
        public static DataSet GetScreensDetails(DTO.Screens screenData)
        {
            return common.ExecuteMySQLQuery(Query.QueryDAO.GetScreensDetails(screenData));
        }

        /// <summary>
        /// GetMasterScreensDetails
        /// </summary>
        /// <returns></returns>
        public static DataSet GetMasterScreensDetails(DTO.Screens screenData)
        {
            return common.ExecuteMySQLQuery(Query.QueryDAO.GetMasterScreensDetails(screenData));
        }

        /// <summary>
        /// GetScreenLocalizedLabels
        /// </summary>
        /// <returns></returns>
        public static DataSet GetScreenLocalizedLabels(DTO.Screens screenData)
        {
            return common.ExecuteMySQLQuery(Query.QueryDAO.GetScreenLocalizedLabels(screenData));
        }

        /// <summary>
        /// AddScreensDetails
        /// </summary>
        /// <param name="screensData"></param>
        /// <returns></returns>
        public static bool AddScreensDetails(DTO.Screens screensData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.AddScreensDetails(screensData));
        }

        /// <summary>
        /// UpdateScreensDetails
        /// </summary>
        /// <param name="screensData"></param>
        /// <returns></returns>
        public static bool UpdateScreensDetails(DTO.Screens screensData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.UpdateScreensDetails(screensData));
        }

        /// <summary>
        /// DeleteScreensDetails
        /// </summary>
        /// <param name="screensData"></param>
        /// <returns></returns>
        public static bool DeleteScreensDetails(DTO.Screens screensData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.DeleteScreensDetails(screensData));
        }

        /// <summary>
        /// AddUpdateScreensAccessDetails
        /// </summary>
        /// <param name="accessList"></param>
        /// <returns></returns>
        public static bool AddUpdateScreensAccessDetails(ArrayList accessList)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.AddUpdateScreensAccessDetails(accessList));
        }

        /// <summary>
        /// UpdateScreenLocalizedValues
        /// </summary>
        /// <param name="screenData"></param>
        /// <returns></returns>
        public static bool UpdateScreenLocalizedValues(DTO.Screens screenData)
        {
            return common.ExecuteMySQLNonQuery(Query.QueryDAO.UpdateScreenLocalizedValues(screenData));
        }

        #endregion

        #region Other Common Functions

        /// <summary>
        /// GetColumnNames
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataSet GetColumnNames(string tableName)
        {
            return common.ExecuteMySQLQuery(Query.QueryDAO.GetColumnNames(tableName));
        }

        /// <summary>
        /// BulkTableInsert
        /// </summary>
        /// <param name="objDS"></param>
        /// <param name="user"></param>
        /// <param name="columns"></param>
        public static void BulkTableInsert(DataTable objDS, ArrayList columns)
        {
            StringBuilder columnList = new StringBuilder();

            for (int i = 1; i < columns.Count; i++)
            {
                columnList.Append(columns[i] + ",");
            }

            foreach (DataRow dr in objDS.Rows)
            {
                common.ExecuteMySQLNonQuery(Query.QueryDAO.BulkTableInsert(dr, objDS.TableName, columnList.ToString()));
            }
        }

        /// <summary>
        /// GetDataFromXLSM
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static DataSet GetDataFromXLSM(string fileName, string sheetName)
        {
            if (true)
            {
                return common.ExecuteXLSXQuery("Select TOP 2 * from [" + sheetName + "]", fileName);
            }
        }

        #endregion

        #region TestStudio

        /// <summary>
        /// GetLoginSessionID
        /// </summary>
        /// <returns></returns>
        public static DTO.TestSudio GetLoginSessionID(DTO.TestSudio ts)
        {
            TS.LoginAPI login = new TS.LoginAPI();
            return login.Login(ts);
        }

        /// <summary>
        /// GetTestSuiteDetails
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DTO.TestSudio GetTestSuiteDetails(DTO.TestSudio ts)
        {
            TS.FetchTestSuiteRunDetailsExAPI fetchSuiteData = new TS.FetchTestSuiteRunDetailsExAPI();

            return fetchSuiteData.FetchTestSuiteRunDetailsEx(ts);
        }

        /// <summary>
        /// CreateTestSuite
        /// </summary>
        /// <param name="sessionID"></param>
        /// <param name="drTestCasesCol"></param>
        /// <returns></returns>
        public static DTO.TestSudio CreateTestSuite(DTO.TestSudio ts)
        {
            TS.CreateTestSuiteAPI createSuite = new TS.CreateTestSuiteAPI();

            return createSuite.CreateTestSuite(ts);
        }

        /// <summary>
        /// CreateTestSuiteRuns
        /// </summary>
        /// <returns></returns>
        public static DTO.TestSudio CreateTestSuiteRuns(DTO.TestSudio ts)
        {
            //TS.CreateTestRunsAPI createTestRuns = new TS.CreateTestRunsAPI();

            //return createTestRuns.CreateTestRuns(ts);

            TS.ImportDataAPI createTestRuns = new TS.ImportDataAPI();

            return createTestRuns.ImportTR(ts);
        }

        /// <summary>
        /// GetTestSuiteRunDetails
        /// </summary>
        /// <param name="sessionID"></param>
        /// <param name="drTestCasesCol"></param>
        /// <returns></returns>
        public static DTO.TestSudio GetTestSuiteRunDetails(DTO.TestSudio ts, string status)
        {
            //TS.FetchSuiteRunDetailsAPI fetchSuiteRunDetails = new TS.FetchSuiteRunDetailsAPI();
            //return fetchSuiteRunDetails.FetchSuiteRunDetails(ts);
            ts.Status = status;
            TS.ExportDataAPI fetchSuiteRunDetails = new TS.ExportDataAPI();
            return fetchSuiteRunDetails.ExportTR(ts);
        }

        /// <summary>
        /// GetTestStudioConfigurations
        /// </summary>
        /// <param name="sessionID"></param>
        /// <param name="drTestCasesCol"></param>
        /// <returns></returns>
        public static DTO.TestSudio GetTestStudioConfigurations(DTO.TestSudio ts)
        {
            TS.FetchAmalgmConfigsAPI configs = new TS.FetchAmalgmConfigsAPI();
            return configs.FetchAmalgmConfigs(ts);
        }

        #endregion
    }
}
