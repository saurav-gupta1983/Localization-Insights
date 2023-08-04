using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using DTO = Adobe.Localization.Insights.Common.DataTransferObjects;
using Adobe.Localization.Insights.Common;

namespace Adobe.Localization.Insights.DataLayer.Query
{
    /// <summary>
    /// TSQueryDAO
    /// </summary>
    public static class TSQueryDAO
    {
        #region Users

        /// <summary>
        /// Query_FirstTimeUser
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static string GetUserDetails(string loginID)
        {
            StringBuilder query = new StringBuilder(" Select UserID, LoginID, FirstName, LastName, EmailID, ContactNo, VendorID, ManagerUserID, SuperUser, SuperUserPassword, ");

            query.Append(" CONCAT(FirstName, ' ', LastName) UserName, CONCAT(FirstName, ' ', LastName, ' (', LoginID, ')') As UserNameID");
            query.Append(" From Users");

            if (loginID != "")
                query.Append(" Where LoginID = '" + loginID + "'");

            return query.ToString();
        }

        /// <summary>
        /// GetUserDetails
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static string GetUserDetails(DTO.Users userData)
        {
            StringBuilder query = new StringBuilder(" Select U.UserID, U.LoginID, U.FirstName, U.LastName, U.ManagerUserID, U.LoginID ManagerLoginID, ");

            query.Append(" U.AlternateEmailID, U.NickName, U.EmailID, U.ContactNo,");
            query.Append(" U.SuperUser, U.SuperUserPassword, ");
            query.Append(" CONCAT(U.FirstName, ' ', U.LastName) UserName, CONCAT(U.FirstName, ' ', U.LastName, ' (', U.LoginID, ')') As UserNameID, ");
            query.Append(" U.VendorID, U.Vendor, U.IsContractor");
            query.Append(" From (Select U.UserID, U.LoginID, U.FirstName, U.LastName, U.AlternateEmailID, U.NickName, U.EmailID, U.ContactNo, U.SuperUser, U.SuperUserPassword, U.VendorID, ");
            query.Append(" U.ManagerUserID, MU.LoginID ManagerLoginID, V.Vendor, V.IsContractor ");
            query.Append(" From (Select * From Users ");

            if (userData.UserID != "")
                query.Append(" WHERE UserID = " + userData.UserID);
            else if (userData.LoginID != "")
                query.Append(" WHERE LoginID = '" + userData.LoginID + "'");
            else if (userData.UserName != "")
            {
                query.Append(" WHERE (LoginID like '%" + userData.UserName + "%'");
                query.Append(" OR EmailID like '%" + userData.UserName + "%'");
                query.Append(" OR FirstName like '%" + userData.UserName + "%'");
                query.Append(" OR LastName like '%" + userData.UserName + "%')");
            }

            query.Append(" ) U");
            query.Append(" LEFT OUTER JOIN Users MU");
            query.Append(" ON U.ManagerUserID = MU.UserID");
            query.Append(" LEFT OUTER JOIN Vendors V");
            query.Append(" ON U.VendorID = V.VendorID");

            query.Append(" ) U");

            if (userData.IsUserMasterScreen && userData.VendorID != "")
            {
                query.Append(" WHERE U.VendorID IS NULL");
                query.Append(" OR U.VendorID = ''");
                query.Append(" OR U.VendorID = " + userData.VendorID);
            }
            else if (userData.VendorID != "")
                query.Append(" WHERE U.VendorID = " + userData.VendorID);

            return query.ToString();
        }

        /// <summary>
        /// AddNewUser
        /// </summary>
        /// <param name="loginID"></param>
        /// <returns></returns>
        public static string AddNewUser(string loginID)
        {
            StringBuilder query = new StringBuilder("Insert Into users(LoginID, FirstName, EmailID, AddedOn, AddedBy) Values ");

            query.Append("('" + loginID + "',");
            query.Append("'" + loginID + "',");
            query.Append("'" + loginID + "@adobe.com',");
            query.Append("SysDate(),");
            query.Append("'" + loginID.ToUpper() + "')");

            return query.ToString();
        }

        /// <summary>
        /// AddUserDetails
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static string AddUserDetails(DTO.Users userData)
        {
            StringBuilder query = new StringBuilder("Insert Into Users");

            query.Append("(LoginID, FirstName, LastName, EmailID, VendorID, AddedOn, AddedBy)");
            query.Append("Values (");

            query.Append("'" + userData.LoginID + "',");
            query.Append("'" + userData.FirstName + "',");
            query.Append("'" + userData.LastName + "',");
            query.Append("'" + userData.LoginID + "@adobe.com',");
            query.Append(userData.VendorID + ",");
            query.Append("SysDate(),");
            query.Append("'" + userData.DataHeader.LoginID + "')");

            return query.ToString();
        }

        /// <summary>
        /// UpdateUserDetails
        /// </summary>
        /// <param name="transferData"></param>
        /// <returns></returns>
        public static string UpdateUserDetails(DTO.Users userData)
        {
            StringBuilder query = new StringBuilder("Update Users SET ");

            query.Append(CheckForBlank("LoginID", userData.LoginID));
            query.Append(CheckForBlank("FirstName", userData.FirstName));
            query.Append(CheckForBlank("LastName", userData.LastName));
            query.Append(CheckForBlank("EmailID", userData.EmailID));
            query.Append(CheckForBlank("AlternateEmailID", userData.AlternateEmailID));
            query.Append(CheckForBlank("NickName", userData.NickName));
            query.Append(CheckForBlank("ContactNo", userData.ContactNo));
            query.Append(CheckForBlank("ManagerUserID", userData.ManagerUserID));
            query.Append(CheckForBlank("VendorID", userData.VendorID));

            query.Append("UpdatedOn = SYSDATE(),");
            query.Append("UpdatedBy = '" + userData.DataHeader.LoginID + "'");
            query.Append(" WHERE UserID = '" + userData.UserID + "'");

            return query.ToString();
        }

        /// <summary>
        /// CheckForBlank
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string CheckForBlank(string fieldName, string value)
        {
            if (value != "")
                return fieldName + " = '" + value + "',";
            else
                return "";
        }

        /// <summary>
        /// DeleteUserDetails
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static string DeleteUserDetails(DTO.Users userData)
        {
            StringBuilder query = new StringBuilder("Delete From Users ");

            query.Append(" WHERE UserID = '" + userData.UserID + "'");

            return query.ToString();
        }

        /// <summary>
        /// SaveUserProductDetails
        /// </summary>
        /// <param name="transferData"></param>
        /// <returns></returns>
        public static string SaveUserProductDetails(DTO.Users userData)
        {
            StringBuilder query = new StringBuilder("Insert Into UserProducts (UserID, ProductID, AddedOn, AddedBy) Values");

            query.Append("(" + userData.UserID + ",");
            query.Append("" + userData.ProductID + ",");
            query.Append("SysDate(),");
            query.Append("'" + userData.DataHeader.LoginID + "')");

            return query.ToString();
        }

        /// <summary>
        /// UpdateProfileAndPreferences
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static string UpdateProfileAndPreferences(DTO.Users userData)
        {
            StringBuilder query = new StringBuilder();

            query.Append(UpdateUserDetails(userData));
            query.Append(" ; ");

            foreach (DTO.Users userProdPref in userData.UserCollection)
            {
                if (userProdPref.ProductPrefID != "" && userProdPref.IsSelected == "0")
                {
                    query.Append(" Delete From UserProductsPreference Where UserProductPreferenceID = " + userProdPref.ProductPrefID);
                    query.Append(" ; ");
                }
                else if (userProdPref.ProductPrefID == "" && userProdPref.IsSelected == "1")
                {
                    query.Append(" Insert Into UserProductsPreference (UserID, ProductID, AddedOn, AddedBy) ");
                    query.Append("Values (");

                    query.Append(userData.UserID + ",");
                    query.Append(userProdPref.ProductID + ",");
                    query.Append("SysDate(),");
                    query.Append("'" + userData.DataHeader.LoginID + "')");

                    query.Append(" ; ");
                }
            }

            return query.ToString();
        }

        ///// <summary>
        ///// DeleteAccessDetails
        ///// </summary>
        ///// <param name="accessData"></param>
        ///// <returns></returns>
        //public static string DeleteAccessDetails(DTO.Users accessData)
        //{
        //    StringBuilder query = new StringBuilder("Delete from UserRights");

        //    query.Append(" WHERE UserProductID = " + accessData.UserProductID);
        //    query.Append(" AND SectionID = " + accessData.SectionID);

        //    //query.Append("AND RoleID = (Select RoleID Where RoleCode = '" + accessData.RoleID + "'),");

        //    return query.ToString();
        //}

        ///// <summary>
        ///// AddAccessDetails
        ///// </summary>
        ///// <param name="userData"></param>
        ///// <returns></returns>
        //public static string AddAccessDetails(DTO.Users userData)
        //{
        //    StringBuilder query = new StringBuilder("Insert Into UserRights(UserProductID, SectionID, RoleID, AddedOn, AddedBy) ");

        //    query.Append("Select " + userData.UserProductID + ", " + userData.SectionID + ", r.RoleID, SysDate(), '" + userData.DataHeader.LoginID + "' ");
        //    query.Append(" FROM Roles r");
        //    query.Append(" WHERE r.RoleCode = '" + userData.RoleID + "'");

        //    return query.ToString();

        //}

        #endregion

        #region Project Roles

        /// <summary>
        /// GetUserRoles
        /// </summary>
        /// <param name="transferData"></param>
        /// <returns></returns>
        public static string GetUserProjectRoles(DTO.Users userData)
        {
            StringBuilder query = new StringBuilder();

            query.Append("Select Distinct U.UserID, UPR.UserProjectRoleID, UPR.ProjectRoleID, PR.ProjectRoleCode, PR.ProjectRole, PR.IsContractorApplicable,  R.RoleID, R.Role");
            query.Append(" From Users U");

            query.Append(" INNER JOIN UserProjectRoles UPR");
            query.Append(" ON U.UserID = UPR.UserID");
            query.Append(" AND u.UserID = '" + userData.UserID + "'");

            query.Append(" INNER JOIN ProjectRoles PR");
            query.Append(" ON UPR.ProjectRoleID = PR.ProjectRoleID");

            if (userData.ProjectRoleID != "")
                query.Append(" AND UPR.ProjectRoleID = " + userData.ProjectRoleID);

            query.Append(" INNER JOIN Roles R");
            query.Append(" ON R.RoleID = PR.RoleID");

            query.Append(" ORDER BY ProjectRoleID ASC");

            return query.ToString();
        }

        /// <summary>
        /// GetProjectRoles
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static string GetProjectRoles(DTO.Users userData)
        {
            StringBuilder query = new StringBuilder();

            query.Append("Select PR.ProjectRoleID, PR.ProjectRoleCode, PR.ProjectRole, PR.IsContractorApplicable,  R.RoleID, R.Role");
            query.Append(" From ProjectRoles PR");

            query.Append(" INNER JOIN Roles R");
            query.Append(" ON R.RoleID = PR.RoleID");

            if (userData.RoleID != "")
                query.Append(" AND PR.RoleID = " + userData.RoleID);

            return query.ToString();
        }

        /// <summary>
        /// AddUpdateUserProjectRoles
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static string AddUpdateUserProjectRoles(DTO.Users userData)
        {
            StringBuilder query = new StringBuilder();

            foreach (DTO.Users userPrjRoles in userData.UserCollection)
            {
                if (userPrjRoles.UserProjectRoleID == "" && userPrjRoles.IsSelected == "1")
                {
                    query.Append(" Insert Into UserProjectRoles (UserID, ProjectRoleID, AddedOn, AddedBy)");
                    query.Append(" Values (");

                    query.Append(userData.UserID + ",");
                    query.Append(userPrjRoles.ProjectRoleID + ",");
                    query.Append(" SysDate(),");
                    query.Append(" '" + userData.DataHeader.LoginID + "')");
                    query.Append(" ; ");
                }
                else if (userPrjRoles.IsSelected == "0")
                {
                    if (userPrjRoles.UserProjectRoleID != "")
                    {
                        query.Append(" Delete From UserProjectRoles");
                        query.Append(" Where UserProjectRoleID = " + userPrjRoles.UserProjectRoleID);
                        query.Append(" ; ");
                    }
                }
            }

            return query.ToString();
        }

        /// <summary>
        /// GetUserProductRoles
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static string GetUserProductRoles(DTO.Users userData)
        {
            StringBuilder query = new StringBuilder();

            query.Append("Select Distinct UP.UserProductID,  UPR.UserProjectRoleID, UPR.ProjectRoleID, PR.ProjectRole, PR.RoleID, PV.ProductVersionID, PV.ProductVersion,  P.ProductID, P.Product");
            query.Append(" From UserProducts UP");

            query.Append(" INNER JOIN UserProjectRoles UPR");
            query.Append(" ON UPR.UserProjectRoleID = UP.UserProjectRoleID");

            if (userData.UserID != "")
                query.Append(" AND UPR.UserID = " + userData.UserID);

            query.Append(" INNER JOIN ProjectRoles PR");
            query.Append(" ON UPR.ProjectRoleID = PR.ProjectRoleID");

            query.Append(" INNER JOIN ProductVersions PV");
            query.Append(" ON UP.ProductVersionID = PV.ProductVersionID");

            query.Append(" INNER JOIN Products P");
            query.Append(" ON PV.ProductID = P.ProductID");

            return query.ToString();
        }

        /// <summary>
        /// GetUserProducts
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static string GetUserProducts(DTO.Users userData)
        {
            StringBuilder query = new StringBuilder();

            if (userData.IsManager)
            {
                query.Append(" Select Distinct P.ProductID, P.Product, PV.ProductVersionID, PV.ProductVersion, PV.ProductCodeName, PV.IsActive");

                if (userData.ProductID == "")
                    query.Append(" From Products P");
                else
                    query.Append(" From (Select * From Products Where ProductID IN (" + userData.ProductID + ")) P");

                query.Append(" LEFT OUTER JOIN ProductVersions PV");
                query.Append(" ON PV.ProductID = P.ProductID");
            }
            else
            {
                if (userData.IsRequiredProductVersion)
                    query.Append(" Select Distinct P.ProductID, P.Product, PV.ProductVersionID, PV.ProductVersion, PV.ProductCodeName, PV.IsActive");
                else
                    query.Append(" Select Distinct P.ProductID, P.Product");

                query.Append(" From UserProducts UP");

                query.Append(" INNER JOIN UserProjectRoles UPR");
                query.Append(" ON UPR.UserProjectRoleID = UP.UserProjectRoleID");

                if (userData.UserID != "")
                    query.Append(" AND UPR.UserID = " + userData.UserID);

                if (userData.ProjectRoleID != "")
                    query.Append(" AND UPR.ProjectRoleID = " + userData.ProjectRoleID);

                query.Append(" INNER JOIN ProductVersions PV");
                query.Append(" ON UP.ProductVersionID = PV.ProductVersionID");

                query.Append(" INNER JOIN Products P");
                query.Append(" ON PV.ProductID = P.ProductID");

                if (userData.ProductID != "")
                    query.Append(" AND P.ProductID IN (" + userData.ProductID + ")");
            }

            if (userData.IsRequiredProductVersion)
                query.Append(" Order By P.Product ASC, PV.ProductVersionID DESC");
            else
                query.Append(" Order By P.Product ASC");

            return query.ToString();
        }

        /// <summary>
        /// GetUserRoles
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static string GetUserRoles(DTO.Users userData)
        {
            StringBuilder query = new StringBuilder();

            query.Append("Select Distinct PR.ProjectRole, P.Product");
            query.Append(" From UserProjectRoles UPR");

            query.Append(" INNER JOIN ProjectRoles PR");
            query.Append(" ON UPR.ProjectRoleID = PR.ProjectRoleID");

            if (userData.UserID != "")
                query.Append(" AND UPR.UserID = " + userData.UserID);

            query.Append(" LEFT OUTER JOIN UserProducts UP");
            query.Append(" ON UPR.UserProjectRoleID = UP.UserProjectRoleID");

            query.Append(" LEFT OUTER JOIN ProductVersions PV");
            query.Append(" ON UP.ProductVersionID = PV.ProductVersionID");

            query.Append(" LEFT OUTER JOIN Products P");
            query.Append(" ON PV.ProductID = P.ProductID");

            query.Append(" Order By PR.RoleID DESC, P.Product, PR.ProjectRole ASC");

            return query.ToString();
        }

        /// <summary>
        /// GetUserScreenAccess
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static string GetUserScreenAccess(DTO.Users userData)
        {
            StringBuilder query = new StringBuilder();
            StringBuilder productQuery = new StringBuilder();

            query.Append(" Select S.ScreenID, S.ScreenIdentifier, S.Sequence, S.IsProductType, ");
            query.Append(" Cast(Max(SAL.IsRead) As UNSIGNED) IsRead, Cast(Max(SAL.IsReadWrite) As UNSIGNED) IsReadWrite, Cast(Max(SAL.IsReport) As UNSIGNED) IsReport, ");
            query.Append(" (Case When IsNull(S.ParentScreenID) = 1 THEN 0 Else S.ParentScreenID END) ParentScreenID ");
            //query.Append(" ,SAL.ProjectRoleID, UP.ProductVersionID, PV.ProductID");
            query.Append(" From Screens S");

            query.Append(" INNER JOIN ScreenAccessLevels SAL");
            query.Append(" ON S.ScreenID = SAL.ScreenID");
            query.Append(" AND S.IsPage = 0");
            query.Append(" AND S.Sequence <> 0 ");
            query.Append(" AND (SAL.IsRead <> 0");
            query.Append(" OR SAL.IsReadWrite <> 0");
            query.Append(" OR SAL.IsReport <> 0)");

            if (userData.ProjectRoleID != "")
                query.Append(" AND SAL.ProjectRoleID = " + userData.ProjectRoleID);

            query.Append(" INNER JOIN UserProjectRoles UPR");
            query.Append(" On UPR.ProjectRoleID = SAL.ProjectRoleID");

            if (userData.UserID != "")
                query.Append(" AND UPR.UserID = " + userData.UserID);

            if (userData.ProductVersionID != "" || userData.ProductID != "")
            {
                productQuery = new StringBuilder(query.ToString());

                productQuery.Append(" INNER JOIN UserProducts UP");
                productQuery.Append(" On UP.UserProjectRoleID = UPR.UserProjectRoleID");

                if (userData.ProductVersionID != "")
                    productQuery.Append(" AND UP.ProductVersionID = " + userData.ProductVersionID);

                productQuery.Append(" INNER JOIN ProductVersions PV");
                productQuery.Append(" On PV.ProductVersionID = UP.ProductVersionID");

                if (userData.ProductID != "")
                    productQuery.Append(" AND PV.ProductID = " + userData.ProductID);
            }

            query.Append(" AND S.IsProductType <> 1");
            query.Append(" Group By S.ScreenID, ParentScreenID, S.ScreenIdentifier, S.Sequence, S.IsProductType");

            if (userData.ProductVersionID != "" || userData.ProductID != "")
            {
                query.Append(" UNION ");
                query.Append(productQuery.ToString());
                query.Append(" Group By S.ScreenID, ParentScreenID, S.ScreenIdentifier, S.Sequence, S.IsProductType");

            }

            //query.Append(" Group By S.ScreenID, ParentScreenID, S.ScreenIdentifier, S.Sequence, SAL.ProjectRoleID, UP.ProductVersionID, PV.ProductID");

            return query.ToString();
        }

        /// <summary>
        /// GetUserProductsPreferred
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static string GetUserProductsPreferred(DTO.Users userData)
        {
            StringBuilder query = new StringBuilder();

            query.Append(" Select * From UserProductsPreference UPP");

            if (userData.UserID != "")
                query.Append(" WHere UPP.UserID = " + userData.UserID);

            return query.ToString();
        }

        /// <summary>
        /// AddUserProducts
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static string AddUserProducts(DTO.Users userData)
        {
            StringBuilder query = new StringBuilder(" Insert Into UserProducts");

            query.Append(" (UserProjectRoleID, ProductVersionID, AddedOn, AddedBy)");
            //query.Append(" (UserProjectRoleID, ProductVersionID, IsOwner, AddedOn, AddedBy)");
            query.Append(" Values (");

            query.Append(userData.UserProjectRoleID + ",");
            query.Append(userData.ProductVersionID + ",");
            //query.Append(userData.IsProductOwner + ",");
            query.Append(" SysDate(),");
            query.Append(" '" + userData.DataHeader.LoginID + "')");

            return query.ToString();
        }

        /// <summary>
        /// UpdateUserProducts
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static string UpdateUserProducts(DTO.Users userData)
        {
            StringBuilder query = new StringBuilder(" Update UserProducts SET ");

            query.Append(" UserProjectRoleID = " + userData.UserProjectRoleID + ",");
            query.Append(" ProductVersionID = " + userData.ProductVersionID + ",");
            //query.Append(" IsOwner = " + userData.IsProductOwner + ",");
            query.Append(" UpdatedOn = SYSDATE(),");
            query.Append(" UpdatedBy = '" + userData.DataHeader.LoginID + "'");
            query.Append(" WHERE UserProductID = " + userData.UserProductID);

            return query.ToString();
        }

        /// <summary>
        /// DeleteUserProducts
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static string DeleteUserProducts(DTO.Users userData)
        {
            StringBuilder query = new StringBuilder("Delete From UserProducts ");

            query.Append(" WHERE UserProductID = " + userData.UserProductID);

            return query.ToString();
        }

        #endregion

        #region Products

        /// <summary>
        /// GetUserProducts
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static string GetUserProducts(DTO.Product productData)
        {
            StringBuilder query = new StringBuilder(" Select PR.UserID, pv.ProductVersionID, p.ProductID, p.ProductCode, p.Product, up.UserProductID ");

            query.Append(" From UserProducts up");
            query.Append(" INNER JOIN ProductVersions pv");
            query.Append(" ON up.ProductVersionID = pv.ProductVersionID");

            query.Append(" INNER JOIN Products p");
            query.Append(" ON pv.ProductID = p.ProductID");

            query.Append(" INNER JOIN UserProjectRoles UPR");
            query.Append(" ON up.UserProjectRoleID = UPR.UserProjectRoleID");

            if (productData.UserID != "")
            {
                query.Append(" AND UPR.UserID = " + productData.UserID);
            }
            if (productData.ProductID != "")
            {
                query.Append(" AND p.ProductID = " + productData.ProductID);
            }
            if (productData.ProductVersionID != "")
            {
                query.Append(" AND p.ProductVersionID = " + productData.ProductVersionID);
            }

            return query.ToString();
        }

        /// <summary>
        /// GetProduct
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static string GetProducts(DTO.Product productData)
        {
            StringBuilder query = new StringBuilder();

            if (productData.UserID == "")
            {
                query.Append("Select * From Products");

                if (productData.ProductID != "")
                    query.Append(" WHERE ProductID = " + productData.ProductID);
            }
            else
            {
                query.Append("Select p.ProductID, p.Product, up.userProductID from Products p");
                query.Append(" INNER JOIN UserProducts up");
                query.Append(" ON up.ProductID = p.ProductID");
                query.Append(" AND up.UserID = " + productData.UserID);

                if (productData.ProductID != "")
                    query.Append(" AND p.ProductID = " + productData.ProductID);
            }

            return query.ToString();
        }

        /// <summary>
        /// GetProductVersion
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static string GetProductVersion(DTO.Product productData)
        {
            StringBuilder query = new StringBuilder();
            query.Append(" Select pv.ProductVersionID, p.productID, p.Product, pv.ProductVersion, pv.ProductYear, pv.ProductCodeName, pv.ReleaseTypeID, pv.IsActive, pv.IsClosed, pv.AboutProduct ");
            query.Append(" From Products P");
            query.Append(" INNER JOIN ProductVersions PV");
            query.Append(" ON PV.ProductID = P.ProductID");

            if (productData.ProductID != "")
            {
                query.Append(" AND pv.ProductID = " + productData.ProductID);
            }
            if (productData.ProductVersionID != "")
            {
                query.Append(" AND pv.ProductVersionID = " + productData.ProductVersionID);
            }
            if (productData.ProductVersion != "")
            {
                query.Append(" AND pv.ProductVersion = '" + productData.ProductVersion + "'");
            }
            if (productData.ProductCodeName != "")
            {
                query.Append(" AND pv.ProductCodeName = '" + productData.ProductCodeName + "'");
            }
            if (productData.IsActive == "1")
            {
                query.Append(" AND pv.IsActive = " + productData.IsActive);
            }

            query.Append(" ORDER BY pv.IsActive DESC, pv.AddedOn Desc");

            return query.ToString();
        }

        /// <summary>
        /// GetProductUsers
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static string GetProductUsers(DTO.Product productData)
        {
            StringBuilder query = new StringBuilder();
            query.Append(" Select up.UserProductID, pv.ProductVersionID, pv.ProductVersion, U.userID, UPR.ProjectRoleID, u.LoginID As LoginID, u.FirstName, U.LastName, ");
            query.Append(" CONCAT(U.FirstName, ' ', U.LastName) UserName, U.EmailID, U.ContactNo, V.vendorID, V.Vendor, PR.ProjectRole, PR.ProjectRoleCode ");
            query.Append(" From UserProducts up");

            query.Append(" INNER JOIN UserProjectRoles UPR");
            query.Append(" ON UPR.UserProjectRoleID = UP.UserProjectRoleID");

            if (productData.UserID != "")
                query.Append(" AND UPR.UserID = " + productData.UserID);

            query.Append(" INNER JOIN Users U");
            query.Append(" ON UPR.UserID = U.UserID");

            query.Append(" INNER JOIN Vendors V");
            query.Append(" ON U.VendorID = V.VendorID");

            query.Append(" INNER JOIN ProductVersions pv");
            query.Append(" ON pv.ProductVersionID = up.ProductVersionID");

            if (productData.ProductVersionID != "")
                query.Append(" AND pv.ProductVersionID = " + productData.ProductVersionID);

            if (productData.ProductID != "")
                query.Append(" AND pv.ProductID = " + productData.ProductID);

            query.Append(" INNER JOIN Products p");
            query.Append(" ON pv.ProductID = p.ProductID");

            query.Append(" INNER JOIN ProjectRoles PR");
            query.Append(" ON UPR.ProjectRoleID = pr.ProjectRoleID");

            return query.ToString();
        }

        /// <summary>
        /// AddProductVersion
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static string AddProductVersion(DTO.Product productData)
        {
            StringBuilder query = new StringBuilder("Insert into ProductVersions(ProductID, ProductVersion, ProductYear, ProductCodeName, ReleaseTypeID, IsActive,");

            query.Append("AddedOn, AddedBy) Values(");

            query.Append(productData.ProductID + ",");
            query.Append("'" + productData.ProductVersion + "',");
            query.Append("'" + productData.ProductYear + "',");
            query.Append("'" + productData.ProductCodeName + "',");
            query.Append(productData.ReleaseTypeID + ",");
            query.Append(productData.IsActive + ",");
            query.Append("SysDate(),");
            query.Append("'" + productData.DataHeader.LoginID + "')");

            return query.ToString();
        }

        /// <summary>
        /// UpdateProductVersion
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static string UpdateProductVersion(DTO.Product productData)
        {
            StringBuilder query = new StringBuilder("Update ProductVersions Set ");

            query.Append("ProductID= " + productData.ProductID + ",");
            query.Append("ProductVersion= '" + productData.ProductVersion + "',");
            query.Append("ProductYear= '" + productData.ProductYear + "',");
            query.Append("ProductCodeName= '" + productData.ProductCodeName + "',");
            query.Append("ReleaseTypeID=" + productData.ReleaseTypeID + ",");
            query.Append("IsActive= " + productData.IsActive + ",");
            query.Append("UpdatedOn = SYSDATE(),");
            query.Append("UpdatedBy = '" + productData.DataHeader.LoginID + "'");
            query.Append(" WHERE ProductVersionID=" + productData.ProductVersionID);

            return query.ToString();
        }

        /// <summary>
        /// CopyProductVersionData
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static string CopyProductVersionData(DTO.Product productData)
        {
            StringBuilder query = new StringBuilder();

            if (productData.IsCopyLocales)
            {
                query.Append(" Insert Into ProductLocales(ProductVersionID, LocaleID, AddedBy, AddedOn)");
                query.Append(String.Format(" Select {0}, LocaleID, '{1}', SysDate()", productData.ProductVersionID, productData.DataHeader.LoginID));
                query.Append(" From ProductLocales");
                query.Append(" Where ProductVersionID = " + productData.CopyProductVersionID);
                query.Append(" ; ");
            }

            if (productData.IsCopyPlatforms)
            {
                query.Append(" Insert Into ProductPlatforms(ProductVersionID, PlatformID, Priority, AddedBy, AddedOn)");
                query.Append(String.Format(" Select {0}, PlatformID, Priority, '{1}', SysDate()", productData.ProductVersionID, productData.DataHeader.LoginID));
                query.Append(" From ProductPlatforms");
                query.Append(" Where ProductVersionID = " + productData.CopyProductVersionID);
                query.Append(" ; ");
            }

            if (productData.IsCopyUsers)
            {
                query.Append(" Insert Into UserProducts(ProductVersionID, UserProjectRoleID, AddedBy, AddedOn)");
                query.Append(String.Format(" Select {0}, UserProjectRoleID, '{1}', SysDate()", productData.ProductVersionID, productData.DataHeader.LoginID));
                query.Append(" From UserProducts");
                query.Append(" Where ProductVersionID = " + productData.CopyProductVersionID);
                query.Append(" ; ");
            }

            if (productData.IsCopyPhases)
            {
                query.Append(" Insert Into ProjectPhases(ProjectPhase, ProductVersionID, PhaseTypeID, StatusID, TestingTypeID, PhaseStartDate, AddedBy, AddedOn)");
                query.Append(String.Format(" Select ProjectPhase, {0}, PhaseTypeID, 1, TestingTypeID, SysDate(), '{1}', SysDate()", productData.ProductVersionID, productData.DataHeader.LoginID));
                query.Append(" From ProjectPhases");
                query.Append(" Where ProductVersionID = " + productData.CopyProductVersionID);
                query.Append(" ; ");
            }

            if (productData.IsCopySprintDetails)
            {
                query.Append(" Insert Into ProductSprints(ProductVersionID, Sprint, SprintDetails, StartDate, EndDate, AddedBy, AddedOn)");
                query.Append(String.Format(" Select {0}, Sprint, SprintDetails, StartDate, EndDate, '{1}', SysDate()", productData.ProductVersionID, productData.DataHeader.LoginID));
                query.Append(" From ProductSprints");
                query.Append(" Where ProductVersionID = " + productData.CopyProductVersionID);
                query.Append(" ; ");
            }

            if (productData.IsCopyProjectBuildDetails)
            {
                query.Append(" Insert Into ProjectBuildDetails(ProductVersionID, ProjectBuildCode, ProjectBuild, IsReleaseBuild, AddedBy, AddedOn)");
                query.Append(String.Format(" Select {0}, ProjectBuildCode, ProjectBuild, IsReleaseBuild, '{1}', SysDate()", productData.ProductVersionID, productData.DataHeader.LoginID));
                query.Append(" From ProjectBuildDetails");
                query.Append(" Where ProductVersionID = " + productData.CopyProductVersionID);
                query.Append(" ; ");

                query.Append(" Insert Into ProjectBuildLocales(ProjectBuildDetailID, ProductLocaleID, AddedBy, AddedOn)");
                query.Append(String.Format(" Select PBD.ProjectBuildDetailID, PL.ProductLocaleID, '{0}', SysDate()", productData.DataHeader.LoginID));
                query.Append(" From ");

                query.Append(" (Select PBL.ProjectBuildDetailID, PBD.ProjectBuildCode, PBL.ProductLocaleID, PL.LocaleID");
                query.Append(" From Projectbuildlocales PBL");
                query.Append(" INNER JOIN projectbuilddetails PBD");
                query.Append(" ON PBL.ProjectBuildDetailID = PBD.ProjectBuildDetailID");
                query.Append(String.Format(" AND PBD.productversionid = {0} ", productData.CopyProductVersionID));
                query.Append(" INNER JOIN productLocales PL");
                query.Append(" ON PL.ProductLocaleID = PBL.ProductLocaleID ");
                query.Append(String.Format(" AND PL.productversionid = {0} ) PB", productData.CopyProductVersionID));

                query.Append(" INNER JOIN productlocales PL");
                query.Append(" ON PL.LocaleID = PB.LocaleID");
                query.Append(" AND PL.productversionid = " + productData.ProductVersionID);

                query.Append(" INNER JOIN projectbuilddetails PBD ");
                query.Append(" ON PBD.ProjectBuildCode = PB.ProjectBuildCode");
                query.Append(" AND PBD.productversionid = " + productData.ProductVersionID);
                query.Append(" ; ");
            }

            return query.ToString();
        }

        /// <summary>
        /// AddProductOwnerforMasterData
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static string AddProductOwnerforMasterData(DTO.Product productData)
        {
            StringBuilder query = new StringBuilder("Insert into UserProducts(ProductVersionID, userID, IsOwner,");

            query.Append("AddedOn, AddedBy) Values(");

            query.Append(productData.ProductVersionID + ",");
            query.Append(productData.UserID + ",");
            query.Append("1,");
            query.Append("SysDate(),");
            query.Append("'" + productData.DataHeader.LoginID + "')");

            return query.ToString();
        }

        /// <summary>
        /// UpdateProductOwnerforMasterData
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static string UpdateProductOwnerforMasterData(DTO.Product productData)
        {
            StringBuilder query = new StringBuilder();

            query.Append(" Update UserProducts Set ");
            query.Append(" IsOwner = " + "0,");
            query.Append(" UpdatedOn = SYSDATE(),");
            query.Append(" UpdatedBy = '" + productData.DataHeader.LoginID + "'");
            query.Append(" WHERE UserProductID = " + productData.UserProductID);

            query.Append(" ; ");

            query.Append(" Update UserProducts Set ");
            query.Append(" IsOwner = " + "1,");
            query.Append(" UpdatedOn = SYSDATE(),");
            query.Append(" UpdatedBy = '" + productData.DataHeader.LoginID + "'");
            query.Append(" WHERE ProductVersionID = " + productData.ProductVersionID);
            query.Append(" AND UserID = " + productData.UserID);

            return query.ToString();
        }

        /// <summary>
        /// DeleteProductOwnerforMasterData
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static string DeleteProductOwnerforMasterData(DTO.Product productData)
        {
            StringBuilder query = new StringBuilder();

            query.Append(" Update UserProducts Set ");
            query.Append(" IsOwner = " + "0,");
            query.Append(" UpdatedOn = SYSDATE(),");
            query.Append(" UpdatedBy = '" + productData.DataHeader.LoginID + "'");
            query.Append(" WHERE UserProductID = " + productData.UserProductID);

            return query.ToString();
        }

        /// <summary>
        /// AddProductLinks
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static string AddProductLinks(DTO.Product productData)
        {
            StringBuilder query = new StringBuilder("Insert into ProductLinks(ProductVersionID, DocumentName, DocumentLink,");

            query.Append("AddedOn, AddedBy) Values(");

            query.Append(productData.ProductVersionID + ",");
            query.Append("'" + productData.DocumentName + "',");
            query.Append("'" + productData.DocumentLink + "',");
            query.Append("SysDate(),");
            query.Append("'" + productData.DataHeader.LoginID + "')");

            return query.ToString();
        }

        /// <summary>
        /// UpdateProductLinks
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static string UpdateProductLinks(DTO.Product productData)
        {
            StringBuilder query = new StringBuilder("Update ProductLinks Set ");

            query.Append("DocumentName = '" + productData.DocumentName + "',");
            query.Append("DocumentLink = '" + productData.DocumentLink + "',");
            query.Append("UpdatedOn = SYSDATE(),");
            query.Append("UpdatedBy = '" + productData.DataHeader.LoginID + "'");
            query.Append(" WHERE ProductLinkID = " + productData.ProductLinkID);

            return query.ToString();

        }

        /// <summary>
        /// DeleteProductLinks
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static string DeleteProductLinks(DTO.Product productData)
        {
            StringBuilder query = new StringBuilder("Delete From ProductLinks ");

            query.Append(" WHERE ProductLinkID = " + productData.ProductLinkID);

            return query.ToString();
        }

        /// <summary>
        /// GetProductSprints
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static string GetProductSprints(DTO.Product productData)
        {
            StringBuilder query = new StringBuilder();
            query.Append(" Select ProductSprintID, ProductVersionID, Sprint, SprintDetails, StartDate, EndDate ");
            query.Append(" From ProductSprints");

            if (productData.ProductVersionID != "")
                query.Append(" WHERE ProductVersionID = " + productData.ProductVersionID);

            query.Append(" ORDER BY StartDate ASC, EndDate ASC");

            return query.ToString();
        }

        /// <summary>
        /// AddProductSprint
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static string AddProductSprint(DTO.Product productData)
        {
            StringBuilder query = new StringBuilder(" Insert into ProductSprints(ProductVersionID, Sprint, SprintDetails, StartDate, EndDate,");

            query.Append(" AddedOn, AddedBy) Values(");

            query.Append(productData.ProductVersionID + ",");
            query.Append(" '" + productData.ProductSprint + "',");
            query.Append(" '" + productData.ProductSprintDetails + "',");
            query.Append(" '" + FormattedDate(productData.StartDate) + "',");
            query.Append(" '" + FormattedDate(productData.EndDate) + "',");
            query.Append(" SysDate(),");
            query.Append(" '" + productData.DataHeader.LoginID + "')");

            return query.ToString();
        }

        /// <summary>
        /// UpdateProductSprint
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static string UpdateProductSprint(DTO.Product productData)
        {
            StringBuilder query = new StringBuilder();

            query.Append(" Update ProductSprints Set ");

            query.Append(" Sprint = '" + productData.ProductSprint + "',");
            query.Append(" SprintDetails = '" + productData.ProductSprintDetails + "',");
            query.Append(" StartDate = '" + FormattedDate(productData.StartDate) + "',");
            query.Append(" EndDate = '" + FormattedDate(productData.EndDate) + "',");
            query.Append(" UpdatedOn = SYSDATE(),");
            query.Append(" UpdatedBy = '" + productData.DataHeader.LoginID + "'");
            query.Append(" WHERE ProductSprintID = " + productData.ProductSprintID + " ; ");

            return query.ToString();
        }

        #endregion

        #region Vendors

        /// <summary>
        /// GetProjectPhaseVendors
        /// </summary>
        /// <param name="projectPhaseData"></param>
        /// <returns></returns>
        public static string GetProjectPhaseVendors(DTO.ProjectPhases projectPhaseData)
        {
            StringBuilder query = new StringBuilder("Select Distinct V.VendorID, V.Vendor from Vendors V ");

            query.Append(" INNER JOIN ProjectLocales pl");
            query.Append(" ON V.VendorID = pl.VendorID");

            if (projectPhaseData.ProjectPhaseID != "")
                query.Append(" AND pl.ProjectPhaseID = " + projectPhaseData.ProjectPhaseID);

            else if (projectPhaseData.ProductVersionID != "" || projectPhaseData.TestingTypeID != "" || projectPhaseData.PhaseTypeID != "")
            {
                query.Append(" INNER JOIN ProjectPhases pp");
                query.Append(" ON pl.ProjectPhaseID = pp.ProjectPhaseID");

                if (projectPhaseData.ProductVersionID != "")
                    query.Append(" AND pp.ProductVersionID = " + projectPhaseData.ProductVersionID);

                if (projectPhaseData.TestingTypeID != "")
                    query.Append(" AND pp.TestingTypeID = " + projectPhaseData.TestingTypeID);

                if (projectPhaseData.PhaseTypeID != "")
                {
                    query.Append(" AND pp.PhaseTypeID = " + projectPhaseData.PhaseTypeID);

                    if (projectPhaseData.ProductSprintID != "")
                        query.Append(" AND pp.ProductSprintID = " + projectPhaseData.ProductSprintID);
                }
            }

            return query.ToString();
        }

        /// <summary>
        /// GetProjectPhaseVendorUsers
        /// </summary>
        /// <param name="phasesData"></param>
        /// <returns></returns>
        public static string GetProjectPhaseVendorUsers(DTO.ProjectPhases phasesData)
        {
            StringBuilder query = new StringBuilder(" Select Distinct U.UserID, CONCAT(U.FirstName,' ',U.LastName) UserName from Users U");

            query.Append(" INNER JOIN ProjectLocales pl");
            query.Append(" ON U.VendorID = pl.VendorID");

            if (phasesData.VendorID != "")
                query.Append(" AND U.VendorID = " + phasesData.VendorID);

            if (phasesData.ProjectPhaseID != "")
                query.Append(" AND pl.ProjectPhaseID = " + phasesData.ProjectPhaseID);

            else if (phasesData.ProductVersionID != "")
            {
                query.Append(" INNER JOIN ProjectPhases pp");
                query.Append(" ON pl.ProjectPhaseID = pp.ProjectPhaseID");
                query.Append(" AND pp.ProductVersionID = " + phasesData.ProductVersionID);
            }

            return query.ToString();
        }

        /// <summary>
        /// GetVendorEfforts
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string GetVendorEfforts(DTO.WSRData wsrData)
        {
            StringBuilder query = new StringBuilder();

            if (wsrData.Reporting)
            {
                query.Append("Select et.WSRParameterID, et.Date, Sum(et.Quantity) Quantity, Sum(et.Efforts) Efforts");
            }
            else
            {
                query.Append("Select et.EffortsTrackID, et.UserID, et.ProjectPhaseID, et.WSRParameterID, et.Date, et.Quantity, et.Efforts, et.Remarks ");
            }

            query.Append(" From EffortsTracker et");
            query.Append(" INNER JOIN WSRParameters Param");
            query.Append(" ON et.WSRParameterID = Param.WSRParameterID");

            if (wsrData.UserID != "")
                query.Append(" AND et.UserID = " + wsrData.UserID);

            DateTime wkEndDate = DateTime.Parse(wsrData.WeekStartDate).AddDays(7);
            query.Append(" AND Date >= '" + FormattedDate(wsrData.WeekStartDate) + "' AND Date < '" + FormattedDate(wkEndDate) + "'");

            query.Append(" INNER JOIN WSRParamSelected PSel");
            query.Append(" ON Param.WSRParameterID = PSel.WSRParameterID");
            query.Append(" AND PSel.IsSelected = 1");

            query.Append(" INNER JOIN Users U");
            query.Append(" ON et.UserID = U.UserID");

            query.Append(" INNER JOIN (Select Distinct ProjectPhaseID, VendorID From ProjectLocales) pl");
            query.Append(" ON U.VendorID = pl.VendorID");

            if (wsrData.VendorID != "")
                query.Append(" AND U.VendorID = " + wsrData.VendorID);

            query.Append(" INNER JOIN ProjectPhases pp");
            query.Append(" ON pl.ProjectPhaseID = pp.ProjectPhaseID");

            if (wsrData.ProjectPhaseID != "")
                query.Append(" AND pl.ProjectPhaseID = " + wsrData.ProjectPhaseID);

            query.Append(" INNER JOIN ProductVersions pv");
            query.Append(" ON pp.ProductVersionID = pv.ProductVersionID");
            query.Append(" AND pv.ProductID = PSel.ProductID");

            if (wsrData.ProductID != "")
                query.Append(" AND pv.ProductID IN (" + wsrData.ProductID + ")");

            if (wsrData.ProductVersionID != "")
                query.Append(" AND pv.ProductVersionID = " + wsrData.ProductVersionID);

            if (wsrData.Reporting)
            {
                query.Append(" Group By et.WSRParameterID, et.Date ");
                query.Append(" Order By et.WSRParameterID, et.Date ");
            }

            return query.ToString();
        }

        /// <summary>
        /// AddUpdateVendorEfforts
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string AddUpdateVendorEfforts(DTO.WSRData wsrData)
        {
            StringBuilder query = new StringBuilder();

            foreach (DTO.Efforts effort in wsrData.VendorEffortsCollection)
            {
                if (effort.PrimaryKeyID == "")
                {
                    query.Append(" Insert Into EffortsTracker(UserID, ProjectPhaseID, WSRParameterID, Date, Efforts, Remarks, ");
                    query.Append(" AddedOn, AddedBy) ");
                    query.Append(String.Format(" Values ({0},{1},{2},'{3}',{4},'{5}',{6},{7}) ", wsrData.UserID, wsrData.ProjectPhaseID, effort.WSRParameterID, FormattedDate(effort.Date), effort.Effort, effort.Remarks, "SysDate()", "'" + wsrData.DataHeader.LoginID + "'"));
                    query.Append(" ; ");
                }
                else
                {
                    query.Append(" Update EffortsTracker Set ");
                    query.Append(" Date = '" + FormattedDate(effort.Date) + "',");
                    query.Append(" Efforts = " + effort.Effort + ",");
                    query.Append(" Remarks = '" + effort.Remarks + "',");
                    query.Append(" UpdatedOn = SYSDATE(),");
                    query.Append(" UpdatedBy = '" + wsrData.DataHeader.LoginID + "'");
                    query.Append(" WHERE EffortsTrackID = " + effort.PrimaryKeyID);
                    query.Append(" ; ");
                }
            }

            return query.ToString();
        }

        /// <summary>
        /// GetVendorDetails
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static string GetVendorDetails(DTO.Users userData)
        {
            StringBuilder query = new StringBuilder();

            query.Append(" Select V.VendorID, V.Vendor From Vendors V ");
            query.Append(" INNER JOIN Users U ");
            query.Append(" ON U.VendorID = U.VendorID ");

            if (userData.UserID != "")
                query.Append(" AND U.UserID = " + userData.UserID);

            if (userData.VendorID != "")
                query.Append(" AND V.VendorID = " + userData.VendorID);

            return query.ToString();
        }

        /// <summary>
        /// UpdateUserVendor
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static string UpdateUserVendor(DTO.Users userData)
        {
            StringBuilder query = new StringBuilder("Update Users SET ");

            query.Append("UserID = " + userData.UserID + ",");
            query.Append("VendorID = " + userData.VendorID + ",");
            query.Append("UpdatedOn = SYSDATE(),");
            query.Append("UpdatedBy = '" + userData.DataHeader.LoginID + "'");
            query.Append(" WHERE UserID = " + userData.UserID);

            return query.ToString();
        }

        /// <summary>
        /// AddUserVendor
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static string AddUserVendor(DTO.Users userData)
        {
            StringBuilder query = new StringBuilder(" Insert Into Users (userID, VendorID, AddedOn, AddedBy) Values");

            query.Append(" (" + userData.UserID + ",");
            query.Append(userData.VendorID + ",");
            query.Append(" SysDate(),");
            query.Append(" '" + userData.DataHeader.LoginID + "')");

            return query.ToString();
        }

        #endregion

        #region Weeks

        /// <summary>
        /// GetWeeksInfo
        /// </summary>
        /// <returns></returns>
        public static string GetWeekInfo(string weekID)
        {
            StringBuilder query = new StringBuilder("Select * from weeks ");

            if (weekID != "")
            {
                query.Append(" Where weekID = " + weekID);
            }

            return query.ToString();
        }

        /// <summary>
        /// AddUpdateWeekInfo
        /// </summary>
        /// <returns></returns>
        public static string AddUpdateWeekInfo(DateTime weekStartDate)
        {
            DateTime weekEndDate = weekStartDate.AddDays(6);
            StringBuilder query = new StringBuilder("Insert Into Weeks(WeekStartDate, WeekEndDate, Year, AddedOn, AddedBy) Values ");

            query.Append("('" + weekStartDate.Year + "-" + weekStartDate.Month + "-" + weekStartDate.Day + "',");
            query.Append("'" + weekEndDate.Year + "-" + weekEndDate.Month + "-" + weekEndDate.Day + "',");
            query.Append(weekEndDate.Year.ToString() + ",");
            query.Append("SysDate(),");
            query.Append("'ADMIN')");

            return query.ToString();
        }

        /// <summary>
        /// GetReportingWeek
        /// </summary>
        /// <param name="reportingType"></param>
        /// <returns></returns>
        public static string GetReportingWeek(string reportingType)
        {
            StringBuilder query = new StringBuilder();

            if (reportingType == WebConstants.DEF_VAL_REPORTING_TYPE_WEEKLY)
            {
                // query.Append("Select WeekID, CONCAT(WeekStartDate, ' - ', WeekEndDate) As ReportingWeek ");
                query.Append(" Select WeekID, cast((CONCAT(date_format(WeekStartDate,'%m/%d/%Y'), ' - ', date_format(WeekEndDate,'%m/%d/%Y'))) As char(30)) As Week ");
                //query.Append(" From Weeks ");
                //query.Append(" Where YEAR(WeekEndDate) >= YEAR(SYSDATE())-1");
            }
            else if (reportingType == WebConstants.DEF_VAL_REPORTING_TYPE_MONTHLY)
            {
                query.Append(" Select Distinct cast((CONCAT(DATE_FORMAT(WeekEndDate, '%M') , ' , ' , YEAR(WeekEndDate))) As char(20)) AS Month");
                //query.Append(" From Weeks ");
                //query.Append(" WHERE YEAR(WeekEndDate) >= YEAR(SYSDATE())-1");
            }
            else if (reportingType == WebConstants.DEF_VAL_REPORTING_TYPE_YEARLY)
            {
                query.Append(" Select Distinct Year AS Month");
                //query.Append(" From Weeks ");
                //query.Append(" WHERE YEAR(WeekEndDate) >= YEAR(SYSDATE())-1");
            }
            else
            {
                query.Append(" Select WeekID, WeekStartDate, WeekEndDate, ");
                query.Append(" cast((CONCAT(DATE_FORMAT(WeekEndDate, '%M') , ' , ' , YEAR(WeekEndDate))) As char(20)) AS Month, ");
                query.Append(" Year ");
            }

            query.Append(" From Weeks ");
            query.Append(" Where YEAR(WeekEndDate) >= YEAR(SYSDATE())-1");
            query.Append(" ORDER BY WeekEndDate Desc");

            return query.ToString();
        }

        #endregion

        #region WSR Data

        /// <summary>
        /// GetProductWSRParameters
        /// </summary>
        /// <param name="wsrParametersData"></param>
        /// <returns></returns>
        public static string GetProductWSRParameters(DTO.WSRData wsrParametersData)
        {
            StringBuilder query = new StringBuilder();

            if (wsrParametersData.ProductID == "")
            {
                query.Append("Select Param.WSRParameterID, Param.WSRParameter, Param.WSRSectionID, Sec.WSRSection");
                query.Append(" From WSRParameters Param");
            }
            else
            {
                query.Append("Select PSel.WSRParamSelectedID, PSel.IsSelected, Param.WSRParameterID, Param.WSRParameter, Param.WSRSectionID, Sec.WSRSection ");
                query.Append(" From WSRParamSelected PSel");
                query.Append(" INNER JOIN WSRParameters Param");
                query.Append(" ON Param.WSRParameterID = PSel.WSRParameterID");
                query.Append(" AND PSel.ProductID = " + wsrParametersData.ProductID);

                if (wsrParametersData.IsSelectedWSRParameters)
                    query.Append(" AND PSel.IsSelected = 1");
            }

            query.Append(" INNER JOIN WSRSections Sec");
            query.Append(" ON Param.WSRSectionID = Sec.WSRSectionID");

            query.Append(" Order By Param.WSRSectionID, Param.WSRParameterID");

            return query.ToString();
        }

        /// <summary>
        /// GetWSRData
        /// </summary>
        /// <param name="dtoWSRData"></param>
        /// <returns></returns>
        public static string GetWSRData(DTO.WSRData dtoWSRData)
        {
            StringBuilder query = new StringBuilder();

            query.Append(" Select wsr.WSRDataID, wsr.WeekID, wsr.RedIssues, wsr.YellowIssues, wsr.GreenAccom, wsr.NextWeekDeliverables ");
            query.Append(" From WSRData wsr");
            query.Append(" INNER JOIN ProjectPhases pp");
            query.Append(" ON wsr.ProjectPhaseID = pp.ProjectPhaseID");

            if (dtoWSRData.ProjectPhaseID != "")
                query.Append(" AND wsr.ProjectPhaseID = " + dtoWSRData.ProjectPhaseID);

            if (dtoWSRData.ProductVersionID != "")
                query.Append(" AND pp.ProductVersionID = " + dtoWSRData.ProductVersionID);

            if (dtoWSRData.PhaseTypeID != "")
            {
                query.Append(" AND pp.PhaseTypeID = " + dtoWSRData.PhaseTypeID);

                if (dtoWSRData.ProductSprintID != "")
                    query.Append(" AND pp.ProductSprintID = " + dtoWSRData.ProductSprintID);
            }

            if (dtoWSRData.VendorID != "")
                query.Append(" AND wsr.VendorID = " + dtoWSRData.VendorID);

            if (dtoWSRData.ProductID != "")
            {
                query.Append(" INNER JOIN ProductVersions PV");
                query.Append(" ON pp.ProductVersionID = pv.ProductVersionID");
                query.Append(" AND pv.ProductID = " + dtoWSRData.ProductID);
            }

            if (dtoWSRData.WeekID != "")
            {
                if (dtoWSRData.ReportingType == "Monthly")
                    query.Append(" AND wsr.WEEKID IN (Select WeekID From Weeks Where CONCAT(DATE_FORMAT(WeekEndDate, '%M') , ' , ' , YEAR(WeekEndDate)) = '" + dtoWSRData.WeekID + "')");
                else if (dtoWSRData.ReportingType == "Weekly" || dtoWSRData.ReportingType == "")
                    query.Append(" AND wsr.WeekID = " + dtoWSRData.WeekID);
            }

            return query.ToString();
        }

        /// <summary>
        /// GetWSRActualEfforts
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dtoWSRData"></param>
        /// <returns></returns>
        public static string GetWSRActualEfforts(DTO.WSRData dtoWSRData)
        {
            StringBuilder query = new StringBuilder();

            //Actuals
            if (dtoWSRData.ReportingType == "")
                query.Append("Select et.WSRParameterID, Sum(et.Quantity) Quantity, Sum(et.Efforts) Efforts");
            else
                query.Append("Select et.WSRParameterID, U.VendorID, Sum(et.Quantity) Quantity, Sum(et.Efforts) Efforts");


            query.Append(" From EffortsTracker et");

            query.Append(" INNER JOIN Weeks wk");
            query.Append(" ON et.Date >= wk.WeekStartDate");
            query.Append(" AND et.Date <= wk.WeekEndDate");

            if (dtoWSRData.ReportingType == "" || dtoWSRData.ReportingType == "Weekly")
            {
                query.Append(" AND wk.WeekID = " + dtoWSRData.WeekID);
            }
            else if (dtoWSRData.ReportingType == "Monthly")
            {
                query.Append(" AND wk.WEEKID IN (Select WeekID From Weeks Where CONCAT(DATE_FORMAT(WeekEndDate, '%M') , ' , ' , YEAR(WeekEndDate)) = '" + dtoWSRData.WeekID + "')");
            }

            query.Append(" INNER JOIN Users U");
            query.Append(" ON et.UserID = U.UserID");

            if (dtoWSRData.VendorID != "")
                query.Append(" AND U.VendorID = " + dtoWSRData.VendorID);

            query.Append(" INNER JOIN (Select Distinct ProjectPhaseID, VendorID From ProjectLocales) pl");
            query.Append(" ON U.VendorID = pl.VendorID");

            if (dtoWSRData.ProjectPhaseID != "")
            {
                query.Append(" AND pl.ProjectPhaseID = " + dtoWSRData.ProjectPhaseID);
            }
            else if (dtoWSRData.ProductVersionID != "" || dtoWSRData.TestingTypeID != "" || dtoWSRData.PhaseTypeID != "")
            {
                query.Append(" INNER JOIN ProjectPhases pp");
                query.Append(" ON pl.ProjectPhaseID = pp.ProjectPhaseID");

                if (dtoWSRData.ProductVersionID != "")
                    query.Append(" AND pp.ProductVersionID = " + dtoWSRData.ProductVersionID);

                if (dtoWSRData.TestingTypeID != "")
                    query.Append(" AND pp.TestingTypeID = " + dtoWSRData.TestingTypeID);

                if (dtoWSRData.PhaseTypeID != "")
                {
                    query.Append(" AND pp.PhaseTypeID = " + dtoWSRData.PhaseTypeID);

                    if (dtoWSRData.ProductSprintID != "")
                        query.Append(" AND pp.ProductSprintID = " + dtoWSRData.ProductSprintID);
                }
            }

            if (dtoWSRData.ReportingType == "")
                query.Append(" Group By et.WSRParameterID ");
            else
                query.Append(" Group By et.WSRParameterID, U.VendorID ");

            return query.ToString();
        }

        /// <summary>
        /// GetWSRRevisedEfforts
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dtoWSRData"></param>
        /// <returns></returns>
        public static string GetWSRRevisedEfforts(DTO.WSRData dtoWSRData)
        {
            StringBuilder query = new StringBuilder();

            //Revised
            if (dtoWSRData.ReportingType == "")
                query.Append("Select wdd.WSRDetailID, wdd.WSRDataID, wdd.WSRParameterID, wdd.Quantity, wdd.Efforts, wdd.Remarks");
            else
                query.Append("Select wdd.WSRParameterID, wd.VendorID, Sum(wdd.Quantity) Quantity, Sum(wdd.Efforts) Efforts");

            query.Append(" From WSRDetails wdd");

            query.Append(" INNER JOIN WSRData wd");
            query.Append(" ON wdd.WSRDataID = wd.WSRDataID");

            if (dtoWSRData.ReportingType == "" || dtoWSRData.ReportingType == "Weekly")
            {
                query.Append(" AND wd.WeekID = " + dtoWSRData.WeekID);
            }
            else if (dtoWSRData.ReportingType == "Monthly")
            {
                query.Append(" AND wd.WEEKID IN (Select WeekID From Weeks Where CONCAT(DATE_FORMAT(WeekEndDate, '%M') , ' , ' , YEAR(WeekEndDate)) = '" + dtoWSRData.WeekID + "')");
            }

            //query.Append(" AND wd.WeekID = " + dtoWSRData.WeekID);

            if (dtoWSRData.WsrDataID != "ALL")
            {
                if (dtoWSRData.WsrDataID != "")
                    query.Append(" AND wdd.WSRDataID = " + dtoWSRData.WsrDataID);
                else
                    query.Append(" AND wdd.WSRDataID = 0");
            }

            if (dtoWSRData.VendorID != "")
                query.Append(" AND wd.VendorID = " + dtoWSRData.VendorID);

            if (dtoWSRData.ProjectPhaseID != "")
            {
                query.Append(" AND wd.ProjectPhaseID = " + dtoWSRData.ProjectPhaseID);
            }
            else if (dtoWSRData.ProductVersionID != "" || dtoWSRData.TestingTypeID != "" || dtoWSRData.PhaseTypeID != "")
            {
                query.Append(" INNER JOIN ProjectPhases pp");
                query.Append(" ON wd.ProjectPhaseID = pp.ProjectPhaseID");

                if (dtoWSRData.ProductVersionID != "")
                    query.Append(" AND pp.ProductVersionID = " + dtoWSRData.ProductVersionID);

                if (dtoWSRData.TestingTypeID != "")
                    query.Append(" AND pp.TestingTypeID = " + dtoWSRData.TestingTypeID);

                if (dtoWSRData.PhaseTypeID != "")
                {
                    query.Append(" AND pp.PhaseTypeID = " + dtoWSRData.PhaseTypeID);

                    if (dtoWSRData.ProductSprintID != "")
                        query.Append(" AND pp.ProductSprintID = " + dtoWSRData.ProductSprintID);
                }
            }

            if (dtoWSRData.ReportingType != "")
                query.Append(" Group By wdd.WSRParameterID, wd.VendorID");

            return query.ToString();
        }

        /// <summary>
        /// GetWSRActualTCsExecuted
        /// </summary>
        /// <param name="dtoWSRData"></param>
        /// <returns></returns>
        public static string GetWSRActualTCsExecuted(DTO.WSRData dtoWSRData)
        {
            StringBuilder query = new StringBuilder();

            query.Append(" Select pl.VendorID, Sum(TestCasesExecuted) TestCasesExecuted");
            query.Append(" From ProjectLocalevsPlatformData data");
            query.Append(" INNER JOIN ProjectLocales pl ");
            query.Append(" ON data.ProjectLocaleID = pl.ProjectLocaleID ");

            if (dtoWSRData.VendorID != "")
                query.Append(" AND pl.VendorID = " + dtoWSRData.VendorID);

            if (dtoWSRData.ProjectPhaseID != "")
            {
                query.Append(" AND pl.ProjectPhaseID = " + dtoWSRData.ProjectPhaseID);
            }
            else if (dtoWSRData.ProductVersionID != "" || dtoWSRData.TestingTypeID != "" || dtoWSRData.PhaseTypeID != "")
            {
                query.Append(" INNER JOIN ProjectPhases pp");
                query.Append(" ON pl.ProjectPhaseID = pp.ProjectPhaseID");

                if (dtoWSRData.ProductVersionID != "")
                    query.Append(" AND pp.ProductVersionID = " + dtoWSRData.ProductVersionID);

                if (dtoWSRData.TestingTypeID != "")
                    query.Append(" AND pp.TestingTypeID = " + dtoWSRData.TestingTypeID);

                if (dtoWSRData.PhaseTypeID != "")
                {
                    query.Append(" AND pp.PhaseTypeID = " + dtoWSRData.PhaseTypeID);

                    if (dtoWSRData.ProductSprintID != "")
                        query.Append(" AND pp.ProductSprintID = " + dtoWSRData.ProductSprintID);
                }
            }

            query.Append(" Group By pl.VendorID ");

            return query.ToString();
        }

        /// <summary>
        /// GetWSRDetails
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dtoWSRData"></param>
        /// <returns></returns>
        public static string GetWSRDetails(string tableName, DTO.WSRData dtoWSRData)
        {
            StringBuilder query = new StringBuilder();

            if (!dtoWSRData.Reporting)
            {
                query.Append("Select * from " + tableName + " Where ");

                Int64 WsrDataID = 0;
                if (dtoWSRData.WsrDataID != "")
                    WsrDataID = Convert.ToInt64(dtoWSRData.WsrDataID);

                query.Append(" WSRDataID = " + WsrDataID.ToString());
            }
            else
            {
                query.Append("Select * from ViewTestCasesData ");
                query.Append(" Where ProductID IN (" + dtoWSRData.ProductID + ")");
                //query.Append(" AND up.ProductID = p.ProductID");

                //query.Append(" AND wsr.UserVendorID = uv.UserVendorID");

                if (dtoWSRData.VendorID != "")
                {
                    query.Append(" AND VendorID IN (" + dtoWSRData.VendorID + ")");
                }
                //query.Append(" AND uv.VendorID = v.VendorID");

                if (dtoWSRData.ReportingType == "Monthly")
                {
                    query.Append(" AND WEEKID IN (Select WeekID From Weeks Where CONCAT(DATE_FORMAT(WeekEndDate, '%M') , ' , ' , YEAR(WeekEndDate)) = '" + dtoWSRData.WeekID + "')");
                }
                else
                {
                    query.Append(" AND WeekID = " + dtoWSRData.WeekID);
                }

                //query.Append(" AND wsr.WeekID = wk.WeekID");
            }

            return query.ToString();
        }

        /// <summary>
        /// UpdateProductWSRParameters
        /// </summary>
        /// <param name="wsrData"></param>
        /// <returns></returns>
        public static string UpdateProductWSRParameters(DTO.WSRData wsrData)
        {
            StringBuilder query = new StringBuilder();

            for (int counter = 0; counter < wsrData.WsrProductParameterCollection.Count; counter++)
            {
                DTO.WSRProductParameter wsrProdParam = (DTO.WSRProductParameter)wsrData.WsrProductParameterCollection[counter];

                if (wsrProdParam.WsrParamSelectedID == "")
                {
                    query.Append(" Insert Into WSRParamSelected(ProductID, WSRParameterID, IsSelected,");
                    query.Append(" AddedOn, AddedBy) ");
                    query.Append(String.Format(" Values ({0},{1},{2},{3},{4}); ", wsrData.ProductID, wsrProdParam.WsrParameterID, Convert.ToInt32(wsrProdParam.IsSelected), "SysDate()", "'" + wsrData.DataHeader.LoginID + "'"));
                }
                else
                {
                    query.Append(" Update WSRParamSelected Set ");
                    query.Append(" IsSelected = " + Convert.ToInt32(wsrProdParam.IsSelected) + ",");
                    query.Append(" UpdatedOn = SYSDATE(),");
                    query.Append(" UpdatedBy = '" + wsrData.DataHeader.LoginID + "'");
                    query.Append(" WHERE WSRParamSelectedID = " + wsrProdParam.WsrParamSelectedID);
                    query.Append(" ; ");
                }
            }

            return query.ToString();
        }

        /// <summary>
        /// UpdateWSRData
        /// </summary>
        /// <param name="wsrData"></param>
        /// <returns></returns>
        public static string UpdateWSRData(DTO.WSRData wsrData)
        {
            StringBuilder query = new StringBuilder(" Update WSRData SET ");

            query.Append(" RedIssues = '" + wsrData.RedIssues.Replace("'", "''") + "',");
            query.Append(" YellowIssues = '" + wsrData.YellowIssues.Replace("'", "''") + "',");
            query.Append(" GreenAccom = '" + wsrData.GreenAccom.Replace("'", "''") + "',");
            query.Append(" NextWeekDeliverables = '" + wsrData.NewDeliverables.Replace("'", "''") + "',");
            query.Append(" UpdatedOn = SYSDATE(),");
            query.Append(" UpdatedBy = '" + wsrData.DataHeader.LoginID + "'");
            query.Append(" WHERE WSRDATAID = " + wsrData.WsrDataID);

            return query.ToString();

        }

        /// <summary>
        /// AddWSRData
        /// </summary>
        /// <param name="wsrData"></param>
        /// <returns></returns>
        public static string AddWSRData(DTO.WSRData wsrData)
        {
            StringBuilder query = new StringBuilder(" Insert Into WSRData");

            query.Append(" (ProjectPhaseID, VendorID, WeekID, RedIssues, YellowIssues, GreenAccom, NextWeekDeliverables, AddedOn, AddedBy)");
            query.Append(" Values (");

            query.Append(wsrData.ProjectPhaseID + ",");
            query.Append(wsrData.VendorID + ",");
            query.Append(wsrData.WeekID + ",");
            query.Append(" '" + wsrData.RedIssues.Replace("'", "''") + "',");
            query.Append(" '" + wsrData.YellowIssues.Replace("'", "''") + "',");
            query.Append(" '" + wsrData.GreenAccom.Replace("'", "''") + "',");
            query.Append(" '" + wsrData.NewDeliverables.Replace("'", "''") + "',");
            query.Append(" SysDate(),");
            query.Append("'" + wsrData.DataHeader.LoginID + "')");

            return query.ToString();

        }

        /// <summary>
        /// AddUpdateWSRDetails
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string AddUpdateWSRDetails(DTO.WSRData wsrData)
        {
            StringBuilder query = new StringBuilder();

            foreach (DTO.Efforts effort in wsrData.VendorEffortsCollection)
            {
                if (effort.PrimaryKeyID == "")
                {
                    query.Append(" Insert Into WSRDetails(WSRDataID, WSRParameterID, Quantity, Efforts, Remarks, ");
                    query.Append(" AddedOn, AddedBy) ");
                    query.Append(String.Format(" Values ({0},{1},{2},{3},'{4}',{5},{6}) ", wsrData.WsrDataID, effort.WSRParameterID, effort.Quantity, effort.Effort, effort.Remarks, "SysDate()", "'" + wsrData.DataHeader.LoginID + "'"));
                    query.Append(" ; ");
                }
                else
                {
                    query.Append(" Update WSRDetails Set ");
                    query.Append(" Quantity = " + effort.Quantity + ",");
                    query.Append(" Efforts = " + effort.Effort + ",");
                    query.Append(" Remarks = '" + effort.Remarks + "',");
                    query.Append(" UpdatedOn = SYSDATE(),");
                    query.Append(" UpdatedBy = '" + wsrData.DataHeader.LoginID + "'");
                    query.Append(" WHERE WSRDetailID = " + effort.PrimaryKeyID);
                    query.Append(" ; ");
                }
            }

            return query.ToString();
        }

        /// <summary>
        /// DeletePrevWeekDelivData
        /// </summary>
        /// <param name="dtoWSRData"></param>
        /// <returns></returns>
        public static string DeletePrevWeekDelivData(DTO.WSRData dtoWSRData)
        {
            StringBuilder query = new StringBuilder("Delete From OutStandingDeliverables ");

            query.Append(" WHERE WSRDataID = " + dtoWSRData.WsrDataID.ToString());

            return query.ToString();
        }

        /// <summary>
        /// AddPrevWeekDelivData
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static string AddPrevWeekDelivData(System.Data.DataRow drOutDeliverables, DTO.WSRData wsrData)
        {
            StringBuilder query = new StringBuilder(" Insert Into OutStandingDeliverables");

            query.Append(" (WSRDataID, PrevWeekDeliverables, OriginalScheduleDate, Reason, AddedOn, AddedBy)");
            query.Append(" Values (");

            query.Append(wsrData.WsrDataID + ",");
            query.Append(" '" + drOutDeliverables["PrevWeekDeliverables"].ToString() + "',");
            DateTime originalScheduleDate = (DateTime)drOutDeliverables["OriginalScheduleDate"];
            query.Append(" '" + originalScheduleDate.Year + "-" + originalScheduleDate.Month + "-" + originalScheduleDate.Day + "',");
            query.Append(" '" + drOutDeliverables["Reason"].ToString() + "',");
            query.Append(" SysDate(),");
            query.Append(" '" + wsrData.DataHeader.LoginID + "')");

            return query.ToString();
        }

        #endregion

        #region Master Data

        /// <summary>
        /// GetDetailsforMasterData
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnNames"></param>
        /// <returns></returns>
        public static string GetDetailsforMasterData(DTO.MasterData masterData)
        {
            StringBuilder query = new StringBuilder();

            if (masterData.TableName == "Users")
            {
                query.Append(" Select u.UserID, u.LoginID, u.FirstName, U.LastName, u.EmailID, u.SuperUser, u.ProjectRoleID, u.VendorID");
                query.Append(" From Users U ");

                if (masterData.IsUsersFilter)
                {
                    query.Append(" WHERE (LoginID like '%" + masterData.Description + "%'");
                    query.Append(" OR EmailID like '%" + masterData.Description + "%'");
                    query.Append(" OR FirstName like '%" + masterData.Description + "%')");
                    query.Append(" OR LastName like '%" + masterData.Description + "%')");
                }
            }
            else
            {
                query.Append("Select * from " + masterData.TableName);

                if (masterData.Filter != "")
                    query.Append(" WHERE " + masterData.Filter + " = " + masterData.Type);
                else if (masterData.Type != "")
                    query.Append(" WHERE " + masterData.ColumnNames[3] + " = " + masterData.Type);
            }

            return query.ToString();
        }

        /// <summary>
        /// AddDetailsforMasterData - To Add new record to Database
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="columnValue"></param>
        /// <returns></returns>
        public static string AddDetailsforMasterData(DTO.MasterData masterData)
        {
            StringBuilder query = new StringBuilder(" Insert into " + masterData.TableName + "(");

            StringBuilder queryValues = new StringBuilder(" Values(");
            int parameters = 1;
            if (masterData.Code != "")
            {
                queryValues.Append("'" + masterData.Code + "',");
                parameters++;
            }
            queryValues.Append("'" + masterData.Description + "',");

            if (masterData.Type != "")
            {
                queryValues.Append(masterData.Type + ",");
                parameters++;
            }
            queryValues.Append("SysDate(),");
            queryValues.Append("'" + masterData.DataHeader.LoginID + "')");

            for (int colCount = 1; colCount <= parameters; colCount++)
            {
                query.Append(masterData.ColumnNames[colCount] + ",");
            }
            query.Append("AddedOn, AddedBy) ");
            query.Append(queryValues.ToString());

            return query.ToString();
        }

        /// <summary>
        /// UpdateDetailsforMasterData - To update records in Database
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="columnValue"></param>
        /// <returns></returns>
        public static string UpdateDetailsforMasterData(DTO.MasterData masterData)
        {
            StringBuilder query = new StringBuilder("Update " + masterData.TableName + " Set ");

            ArrayList columnNames = masterData.ColumnNames;
            int isCodeExit = 0;
            if (masterData.Code != "")
            {
                query.Append(columnNames[1] + "= '" + masterData.Code + "',");
                isCodeExit = 1;
            }
            query.Append(columnNames[1 + isCodeExit] + "= '" + masterData.Description + "',");

            if (masterData.Type != "")
            {
                query.Append(columnNames[2 + isCodeExit] + "= '" + masterData.Type + "',");
            }
            query.Append("UpdatedOn = SYSDATE(),");
            query.Append("UpdatedBy = '" + masterData.DataHeader.LoginID + "'");
            query.Append(" WHERE " + columnNames[0] + " = " + masterData.MasterDataID);

            return query.ToString();
        }

        /// <summary>
        /// DeleteDetailsforMasterData - To delete records from Database
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="columnValue"></param>
        /// <returns></returns>
        public static string DeleteDetailsforMasterData(DTO.MasterData masterData)
        {
            StringBuilder query = new StringBuilder("Delete from " + masterData.TableName);
            query.Append(" Where " + masterData.ColumnNames[0] + " = " + masterData.MasterDataID);

            return query.ToString();
        }

        /// <summary>
        /// UpdateVendorDetailsforMasterData
        /// </summary>
        /// <param name="masterData"></param>
        /// <returns></returns>
        public static string UpdateVendorDetailsforMasterData(DTO.MasterData masterData)
        {
            StringBuilder query = new StringBuilder(" Update " + masterData.TableName + " Set ");


            query.Append(" VendorCode= '" + masterData.Code + "',");
            query.Append(" Vendor= '" + masterData.Description + "',");
            query.Append(" VendorLocation= '" + masterData.VendorLocation + "',");
            query.Append(" VendorTypeID= " + masterData.Type + ",");
            query.Append(" IsContractor= " + masterData.IsContractor + ",");
            query.Append(" UpdatedOn = SYSDATE(),");
            query.Append(" UpdatedBy = '" + masterData.DataHeader.LoginID + "'");
            query.Append(" WHERE VendorID=" + masterData.MasterDataID);

            return query.ToString();
        }

        /// <summary>
        /// AddVendorDetailsforMasterData
        /// </summary>
        /// <param name="masterData"></param>
        /// <returns></returns>
        public static string AddVendorDetailsforMasterData(DTO.MasterData masterData)
        {
            StringBuilder query = new StringBuilder(" Insert into " + masterData.TableName + "(VendorCode, Vendor, VendorLocation, VendorTypeID, IsContractor,");

            query.Append(" AddedOn, AddedBy) Values(");

            query.Append(" '" + masterData.Code + "',");
            query.Append(" '" + masterData.Description + "',");
            query.Append(" '" + masterData.VendorLocation + "',");
            query.Append(masterData.Type + ",");
            query.Append(masterData.IsContractor + ",");
            query.Append(" SysDate(),");
            query.Append(" '" + masterData.DataHeader.LoginID + "')");

            return query.ToString();
        }

        /// <summary>
        /// AddUserDetailsforMasterData
        /// </summary>
        /// <param name="masterData"></param>
        /// <returns></returns>
        public static string AddUserDetailsforMasterData(DTO.MasterData masterData)
        {
            StringBuilder query = new StringBuilder(" Insert into " + masterData.TableName + "(LoginID, FirstName, LastName, EmailID, ProjectRoleID, SuperUser,");

            query.Append(" AddedOn, AddedBy) Values(");

            query.Append(" '" + masterData.Code + "',");
            query.Append(" '" + masterData.Description + "',");
            query.Append(" '" + masterData.EmailID + "',");
            query.Append(masterData.Type + ",");
            query.Append(masterData.IsSuperUser + ",");
            query.Append("SysDate(),");
            query.Append(" '" + masterData.DataHeader.LoginID + "')");

            return query.ToString();
        }

        /// <summary>
        /// UpdateUserDetailsforMasterData
        /// </summary>
        /// <param name="masterData"></param>
        /// <returns></returns>
        public static string UpdateUserDetailsforMasterData(DTO.MasterData masterData)
        {
            StringBuilder query = new StringBuilder(" Update " + masterData.TableName + " Set ");

            query.Append(" LoginID= '" + masterData.Code + "',");
            query.Append(" UserName= '" + masterData.Description + "',");
            query.Append(" EmailID= '" + masterData.EmailID + "',");
            query.Append(" ProjectRoleID= " + masterData.Type + ",");
            query.Append(" SuperUser= " + masterData.IsSuperUser + ",");
            query.Append(" UpdatedOn = SYSDATE(),");
            query.Append(" UpdatedBy = '" + masterData.DataHeader.LoginID + "'");
            query.Append(" WHERE UserID=" + masterData.MasterDataID);

            return query.ToString();
        }

        /// <summary>
        /// GetUserVendorAssociations
        /// </summary>
        /// <param name="masterData"></param>
        /// <returns></returns>
        public static string GetUserVendorAssociations(DTO.MasterData masterData)
        {
            StringBuilder query = new StringBuilder("Select U.UserID, U.LoginID, CONCAT(U.FirstName, ' ', U.LastName) UserName, V.VendorID ");
            query.Append(" From Users U");
            query.Append(" INNER JOIN Vendors V");
            query.Append(" ON V.VendorID = U.VendorID");

            if (masterData.Description != "")
            {
                query.Append(" Where (u.LoginID like '%" + masterData.Description + "%'");
                query.Append(" OR u.EmailID like '%" + masterData.Description + "%'");
                query.Append(" OR u.FirstName like '%" + masterData.Description + "%'");
                query.Append(" OR u.LastName like '%" + masterData.Description + "%')");
            }

            if (masterData.MasterDataID != "")
            {
                query.Append(" AND U.VendorID = " + masterData.MasterDataID);
            }

            return query.ToString();
        }

        /// <summary>
        /// AddUserVendorAssociationsDetailsforMasterData
        /// </summary>
        /// <param name="masterData"></param>
        /// <returns></returns>
        public static string AddUserVendorAssociationsDetailsforMasterData(DTO.MasterData masterData)
        {
            StringBuilder query = new StringBuilder(" Insert into Users(UserID, VendorID, ");

            query.Append(" AddedOn, AddedBy) Values(");

            query.Append(" '" + masterData.Code + "',");
            query.Append(masterData.Type + ",");
            query.Append(" SysDate(),");
            query.Append(" '" + masterData.DataHeader.LoginID + "')");

            return query.ToString();
        }

        /// <summary>
        /// UpdateUserVendorAssociationsDetailsforMasterData
        /// </summary>
        /// <param name="masterData"></param>
        /// <returns></returns>
        public static string UpdateUserVendorAssociationsDetailsforMasterData(DTO.MasterData masterData)
        {
            StringBuilder query = new StringBuilder("Update Users Set ");

            query.Append(" UserID= '" + masterData.Code + "',");
            query.Append(" VendorID= " + masterData.Type + ",");
            query.Append(" UpdatedOn = SYSDATE(),");
            query.Append(" UpdatedBy = '" + masterData.DataHeader.LoginID + "'");
            query.Append(" WHERE UserVendorID=" + masterData.MasterDataID);

            return query.ToString();
        }

        #endregion

        #region Product Version Details

        /// <summary>
        /// GetAllLocales
        /// </summary>
        /// <returns></returns>
        public static string GetAllLocales()
        {
            StringBuilder query = new StringBuilder();

            query.Append("Select L.LocaleID, L.LocaleCode, L.Locale, L.FallbackLocaleID, CONCAT(L.Locale, ' (', L.LocaleCode, ')') Locale, L.DisplayOrder, LT.TierID, LT.TierCode Tier");
            query.Append(" From Locales L");
            query.Append(" INNER JOIN LocaleTiers LT");
            query.Append(" ON l.TierID = LT.TierID");
            query.Append(" ORDER BY LT.TierCode ASC, L.DisplayOrder ASC, L.Locale ASC");

            return query.ToString();
        }

        /// <summary>
        /// GetProductVersionLocales
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static string GetProductVersionLocales(DTO.Product productData)
        {
            StringBuilder query = new StringBuilder();

            query.Append("Select pl.ProductLocaleID, pl.ProductVersionID, pl.LocaleID, CONCAT(L.Locale, ' (', L.LocaleCode, ')') Locale, L.DisplayOrder, LT.TierID, LT.Tier");
            query.Append(" From ProductLocales pl");
            query.Append(" INNER JOIN Locales L");
            query.Append(" ON pl.LocaleID = L.LocaleID");
            query.Append(" INNER JOIN LocaleTiers LT");
            query.Append(" ON l.TierID = LT.TierID");

            if (productData.ProductVersionID != "")
                query.Append(" AND pl.ProductversionID = " + productData.ProductVersionID);

            query.Append(" ORDER BY LT.TierCode ASC, L.DisplayOrder ASC, L.Locale ASC");

            return query.ToString();
        }

        /// <summary>
        /// GetAllPlatforms
        /// </summary>
        /// <returns></returns>
        public static string GetAllPlatforms()
        {
            StringBuilder query = new StringBuilder();

            query.Append("Select p.PlatformID, p.Platform, p.DisplayOrder, p.PlatformTypeID, pt.Platformtype, 0 Priority");
            query.Append(" From Platforms P");
            query.Append(" INNER JOIN PlatformTypes PT");
            query.Append(" ON p.PlatformTypeID = pt.PlatformTypeID");
            query.Append(" ORDER BY pt.Platformtype ASC, p.DisplayOrder ASC,  p.Platform ASC");

            return query.ToString();
        }

        /// <summary>
        /// GetProductVersionPlatforms
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static string GetProductVersionPlatforms(DTO.Product productData)
        {
            StringBuilder query = new StringBuilder();

            query.Append("Select pp.ProductPlatformID, pp.ProductPlatformID, pp.ProductVersionID, pp.PlatformID, pp.Priority, p.Platform, p.DisplayOrder, p.PlatformTypeID, pt.Platformtype");
            query.Append(" From ProductPlatforms pp");
            query.Append(" INNER JOIN Platforms p");
            query.Append(" ON pp.PlatformID = p.PlatformID");
            query.Append(" INNER JOIN PlatformTypes PT");
            query.Append(" ON p.PlatformTypeID = pt.PlatformTypeID");

            if (productData.ProductVersionID != "")
                query.Append(" AND pp.ProductversionID = " + productData.ProductVersionID);

            query.Append(" ORDER BY pt.Platformtype ASC, p.DisplayOrder ASC,  p.Platform ASC");

            return query.ToString();
        }

        /// <summary>
        /// UpdateProductVersionLocales
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static string UpdateProductVersionLocales(DTO.Product productData)
        {
            StringBuilder query = new StringBuilder();
            StringBuilder localeIDs = new StringBuilder("0");

            foreach (string localeID in productData.LocaleIDCollection)
                localeIDs.Append("," + localeID);

            query.Append(" Delete From ProjectBuildLocales");
            query.Append(" Where ProductLocaleID IN (Select ProductLocaleID From ProductLocales ");
            query.Append(" Where ProductVersionID = " + productData.ProductVersionID);
            query.Append(" AND LocaleID NOT IN (" + localeIDs.ToString() + ")); ");

            query.Append(" Delete From ProductLocales");
            query.Append(" Where ProductVersionID = " + productData.ProductVersionID);
            query.Append(" AND LocaleID NOT IN (" + localeIDs.ToString() + "); ");

            query.Append(" Insert Into ProductLocales(ProductVersionID, LocaleID, ");
            query.Append(" AddedOn, AddedBy) ");
            query.Append(" Select " + productData.ProductVersionID + ", LocaleID, SysDate(), '" + productData.DataHeader.LoginID + "' From Locales");
            query.Append(" WHERE LocaleID IN (" + localeIDs.ToString() + ")");
            query.Append(" AND LocaleID NOT IN (");
            query.Append(" Select LocaleID From ProductLocales WHERE ProductVersionID = " + productData.ProductVersionID);
            query.Append(")");

            return query.ToString();

        }

        /// <summary>
        /// UpdateProductVersionPlatforms
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static string UpdateProductVersionPlatforms(DTO.Product productData)
        {
            StringBuilder query = new StringBuilder();
            StringBuilder platformIDs = new StringBuilder("0");

            foreach (string platformID in productData.PlatformIDCollection)
            {
                platformIDs.Append("," + platformID);
            }

            query.Append(" Delete From ProductPlatforms");
            query.Append(" Where ProductVersionID = " + productData.ProductVersionID);
            query.Append(" AND PlatformID NOT IN (" + platformIDs.ToString() + "); ");

            for (int counter = 0; counter < productData.PlatformIDCollection.Count; counter++)
            {
                string priority = productData.ProductPlatformPriorityCollection[counter].ToString();
                if (priority == "")
                    priority = "0";

                if (productData.ProductPlatformIDCollection[counter].ToString() == "")
                {
                    query.Append(" Insert Into ProductPlatforms(ProductVersionID, PlatformID, Priority,");
                    query.Append(" AddedOn, AddedBy) ");
                    query.Append(String.Format(" Values ({0},{1},{2},{3},{4}); ", productData.ProductVersionID, productData.PlatformIDCollection[counter], priority, "SysDate()", "'" + productData.DataHeader.LoginID + "'"));
                }
                else
                {
                    query.Append(" Update ProductPlatforms Set ");
                    query.Append(" Priority = " + priority + ",");
                    query.Append(" UpdatedOn = SYSDATE(),");
                    query.Append(" UpdatedBy = '" + productData.DataHeader.LoginID + "'");
                    query.Append(" WHERE ProductVersionID = " + productData.ProductVersionID);
                    query.Append(" AND PlatformID = " + productData.PlatformIDCollection[counter] + " ; ");
                }
            }

            return query.ToString();
        }

        /// <summary>
        /// UpdateAboutProductDetails
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static string UpdateAboutProductDetails(DTO.Product productData)
        {
            StringBuilder query = new StringBuilder();

            query.Append(" Update ProductVersions Set ");
            query.Append(" AboutProduct = '" + productData.AboutProduct + "',");
            query.Append(" UpdatedOn = SYSDATE(),");
            query.Append(" UpdatedBy = '" + productData.DataHeader.LoginID + "'");
            query.Append(" WHERE ProductVersionID = " + productData.ProductVersionID);

            return query.ToString();
        }

        #endregion

        #region Project Phases

        ///// <summary>
        ///// GetProjectPhases
        ///// </summary>
        ///// <param name="projectPhaseData"></param>
        ///// <returns></returns>
        //public static string GetProjectPhases(DTO.ProjectPhases projectPhaseData)
        //{
        //    StringBuilder query = new StringBuilder();
        //    query.Append(" Select PPS.ProjectPhaseID, PPS.ProjectPhase, PPS.PhaseStartDate, PPS.PhaseEndDate, PPS.PhaseTypeID, PPS.ProductSprintID, PPS.TestingTypeID, PPS.AboutProjectPhase, PPS.StatusID, ");
        //    query.Append(" Sum(PLPD.TestCasesCount) TestCasesPlanned, Sum(PLPD.TestCasesExecuted) TestCasesExecuted");
        //    query.Append(" From (Select pp.ProjectPhaseID, pp.ProjectPhase, pp.PhaseStartDate, pp.PhaseEndDate, pp.PhaseTypeID, PP.ProductSprintID, pp.TestingTypeID, pp.AboutProjectPhase, pp.StatusID ");
        //    query.Append(" From ProjectPhases pp");
        //    query.Append(" INNER JOIN ProductVersions pv");
        //    query.Append(" ON pp.ProductVersionID = pv.ProductVersionID");

        //    if (projectPhaseData.ProductVersionID != "")
        //        query.Append(" AND pp.ProductVersionID = " + projectPhaseData.ProductVersionID);

        //    if (projectPhaseData.ProjectPhase != "")
        //        query.Append(" AND pp.ProjectPhase = '" + projectPhaseData.ProjectPhase + "'");

        //    if (projectPhaseData.IsActive)
        //        query.Append(" AND pp.StatusID = 2");

        //    if (projectPhaseData.TestingTypeID != "")
        //        query.Append(" AND pp.TestingTypeID = " + projectPhaseData.TestingTypeID);

        //    if (projectPhaseData.PhaseTypeID != "")
        //    {
        //        query.Append(" AND pp.PhaseTypeID = " + projectPhaseData.PhaseTypeID);

        //        if (projectPhaseData.ProductSprintID != "")
        //            query.Append(" AND pp.ProductSprintID = " + projectPhaseData.ProductSprintID);
        //    }

        //    if (projectPhaseData.VendorID != "")
        //    {
        //        query.Append(" INNER JOIN ProjectLocales PL");
        //        query.Append(" ON pp.ProjectPhaseID = PL.ProjectPhaseID");
        //        query.Append(" AND PL.VendorID = " + projectPhaseData.VendorID);
        //    }

        //    query.Append(" ) PPS ");
        //    query.Append(" LEFT OUTER JOIN ProjectLocaleVsPlatformData PLPD");
        //    query.Append(" ON PLPD.ProjectPhaseID = PPS.ProjectPhaseID ");

        //    query.Append(" GROUP BY PPS.ProjectPhaseID, PPS.ProjectPhase, PPS.PhaseStartDate, PPS.PhaseEndDate, PPS.PhaseTypeID, PPS.TestingTypeID, PPS.AboutProjectPhase, PPS.StatusID");
        //    query.Append(" ORDER BY PPS.PhaseEndDate DESC, PPS.PhaseStartDate DESC");

        //    return query.ToString();
        //}

        /// <summary>
        /// GetProjectPhaseSummary
        /// </summary>
        /// <param name="projectPhasesData"></param>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public static string GetProjectPhases(DTO.ProjectPhases projectPhaseData)
        {
            StringBuilder query = new StringBuilder();

            query.Append(" Select PPS.ProductVersionID, PPS.ProjectPhaseID, PPS.ProjectPhase, PPS.PhaseStartDate, PPS.PhaseEndDate, PPS.PhaseTypeID, PPS.ProductSprintID, PPS.TestingTypeID, PPS.AboutProjectPhase, PPS.StatusID, ");
            query.Append(" PD.TotalCount, PD.TotalExecuted, PD.TotalNA");

            query.Append(" From (Select PP.ProductVersionID, pp.ProjectPhaseID, pp.ProjectPhase, pp.PhaseStartDate, pp.PhaseEndDate, pp.PhaseTypeID, PP.ProductSprintID, pp.TestingTypeID, pp.AboutProjectPhase, pp.StatusID ");
            query.Append(" From ProjectPhases pp");
            query.Append(" INNER JOIN ProductVersions pv");
            query.Append(" ON pp.ProductVersionID = pv.ProductVersionID");

            if (projectPhaseData.ProductVersionID != "")
                query.Append(" AND pp.ProductVersionID = " + projectPhaseData.ProductVersionID);

            if (projectPhaseData.ProjectPhase != "")
                query.Append(" AND pp.ProjectPhase = '" + projectPhaseData.ProjectPhase + "'");

            if (projectPhaseData.IsActive)
                query.Append(" AND pp.StatusID = 2");

            if (projectPhaseData.TestingTypeID != "")
                query.Append(" AND pp.TestingTypeID = " + projectPhaseData.TestingTypeID);

            if (projectPhaseData.PhaseTypeID != "")
            {
                query.Append(" AND pp.PhaseTypeID = " + projectPhaseData.PhaseTypeID);

                if (projectPhaseData.ProductSprintID != "")
                    query.Append(" AND pp.ProductSprintID = " + projectPhaseData.ProductSprintID);
            }

            if (projectPhaseData.VendorID != "")
            {
                query.Append(" INNER JOIN ProjectLocales PL");
                query.Append(" ON pp.ProjectPhaseID = PL.ProjectPhaseID");
                query.Append(" AND PL.VendorID = " + projectPhaseData.VendorID);
            }

            query.Append(" ) PPS");
            query.Append(" LEFT OUTER JOIN ");
            query.Append(" (Select PCD.ProjectPhaseID, Sum(PLPD.TestCasesCount) TotalCount, Sum(PLPD.TestCasesExecuted) TotalExecuted, Sum(PLPD.TestCasesNA) TotalNA");
            query.Append(" From ProjectLocaleVsPlatformData PLPD ");
            query.Append(" INNER JOIN PhaseCoverageDetails PCD");
            query.Append(" ON PLPD.PhaseCoverageDetailID = PCD.PhaseCoverageDetailID ");

            if (projectPhaseData.ProjectPhaseID != "")
                query.Append(" AND pp.ProjectPhaseID = " + projectPhaseData.ProjectPhaseID);
            else if (projectPhaseData.ProductVersionID != "")
            {
                query.Append(" INNER JOIN ProjectPhases PP");
                query.Append(" ON PP.ProjectPhaseID = PCD.ProjectPhaseID");
                query.Append(" AND PP.ProductVersionID = " + projectPhaseData.ProductVersionID);
            }
            query.Append(" GROUP BY PCD.ProjectPhaseID ");
            query.Append(" ) PD");
            query.Append(" ON PD.ProjectPhaseID = PPS.ProjectPhaseID ");

            query.Append(" ORDER BY PPS.PhaseEndDate DESC, PPS.PhaseStartDate DESC");

            return query.ToString();
        }

        /// <summary>
        /// GetProjectLocales
        /// </summary>
        /// <param name="projectPhaseData"></param>
        /// <returns></returns>
        public static string GetProjectLocales(DTO.ProjectPhases projectPhaseData)
        {
            StringBuilder query = new StringBuilder();
            query.Append(" Select pl.ProjectLocaleID, pl.ProductLocaleID, pl.ProjectPhaseID, pl.LocaleWeight, L.Locale, L.DisplayOrder, LT.TierID, V.VendorID, V.Vendor");
            query.Append(" From ProjectLocales pl");

            if (projectPhaseData.ProjectBuildDetailID != "")
            {
                query.Append(" INNER JOIN ProjectBuildLocales PBL");
                query.Append(" ON PBL.ProductLocaleID = PL.ProductLocaleID ");
                query.Append(" INNER JOIN ProjectBuildDetails PBD");
                query.Append(" ON PBD.ProjectBuildDetailID = PBL.ProjectBuildDetailID ");
                query.Append(string.Format(" AND PBD.ProjectBuildDetailID = {0}", projectPhaseData.ProjectBuildDetailID));
            }

            query.Append(" INNER JOIN ProductLocales PDL");
            query.Append(" ON pl.ProductLocaleID = PDL.ProductLocaleID");

            if (projectPhaseData.ProductLocaleID != "")
                query.Append(string.Format(" AND PDL.ProductLocaleID = {0}", projectPhaseData.ProductLocaleID));

            query.Append(" INNER JOIN Locales L");
            query.Append(" ON PDL.LocaleID = L.LocaleID");
            query.Append(" INNER JOIN LocaleTiers LT");
            query.Append(" ON l.TierID = LT.TierID");

            if (projectPhaseData.ProjectPhaseID != "")
                query.Append(" AND pl.ProjectPhaseID = " + projectPhaseData.ProjectPhaseID);

            if (projectPhaseData.ProductVersionID != "")
                query.Append(" AND PDL.ProductVersionID = " + projectPhaseData.ProductVersionID);

            if (projectPhaseData.VendorID != "")
                query.Append(" AND pl.VendorID IN ( " + projectPhaseData.VendorID + ")");

            query.Append(" INNER JOIN Vendors V");
            query.Append(" ON V.VendorID = pl.VendorID");

            query.Append(" ORDER BY LT.TierID, L.DisplayOrder, pl.ProductLocaleID");

            return query.ToString();
        }

        /// <summary>
        /// GetProjectPlatforms
        /// </summary>
        /// <param name="projectPhaseData"></param>
        /// <returns></returns>
        public static string GetProjectPlatforms(DTO.ProjectPhases projectPhaseData)
        {
            StringBuilder query = new StringBuilder();
            query.Append(" Select pp.ProjectPlatformID, pp.ProductPlatformID, pp.ProjectPhaseID, p.Platform");
            query.Append(" From ProjectPlatforms pp");
            query.Append(" INNER JOIN ProductPlatforms PDP");
            query.Append(" ON pp.ProductPlatformID = PDP.ProductPlatformID");
            query.Append(" INNER JOIN Platforms P");
            query.Append(" ON PDP.PlatformID = P.PlatformID");
            query.Append(" INNER JOIN PlatformTypes PT");
            query.Append(" ON P.PlatformTypeID = PT.PlatformTypeID");

            if (projectPhaseData.PlatformTypeID != "")
                query.Append(string.Format(" AND P.PlatformTypeID = {0}", projectPhaseData.PlatformTypeID));

            if (projectPhaseData.ProjectPhaseID != "")
                query.Append(" AND pp.ProjectPhaseID = " + projectPhaseData.ProjectPhaseID);

            if (projectPhaseData.ProductVersionID != "")
                query.Append(" AND PDP.ProductVersionID = " + projectPhaseData.ProductVersionID);

            query.Append(" ORDER BY PT.PlatformType DESC, PDP.Priority DESC, p.DisplayOrder ASC");

            return query.ToString();
        }

        /// <summary>
        /// GetPhaseExecutableTestCases
        /// </summary>
        /// <param name="projectPhaseData"></param>
        /// <returns></returns>
        public static string GetPhaseExecutableTestCases(DTO.ProjectPhases projectPhaseData)
        {
            StringBuilder query = new StringBuilder();

            query.Append(" SELECT Sum(TestCasesCount)");
            query.Append(" FROM PhaseCoverageDetails");

            if (projectPhaseData.PhaseCoverageDetailID != "")
                query.Append(" WHERE PhaseCoverageDetailID = " + projectPhaseData.PhaseCoverageDetailID);
            else if (projectPhaseData.ProjectPhaseID != "")
                query.Append(" WHERE ProjectPhaseID = " + projectPhaseData.ProjectPhaseID);

            return query.ToString();
        }

        /// <summary>
        /// GetPhaseCoverageDetails
        /// </summary>
        /// <param name="projectPhaseData"></param>
        /// <returns></returns>
        public static string GetPhaseCoverageDetails(DTO.ProjectPhases projectPhaseData)
        {
            StringBuilder query = new StringBuilder();

            query.Append(" SELECT PCD.ProjectPhaseID, PP.ProjectPhase, PP.PhaseStartDate, PP.PhaseEndDate, PCD.PhaseCoverageDetailID, PCD.PhaseCoverageDetails, PCD.TestCasesCount, PCD.SuiteID, PCD.TestCasesRepository, PCD.TestCasesDistribution, PCD.AcrossLocalesandPlatforms ");
            query.Append(" FROM PhaseCoverageDetails PCD");
            query.Append(" INNER JOIN ProjectPhases PP");
            query.Append(" ON PP.ProjectPhaseID = PCD.ProjectPhaseID");

            if (projectPhaseData.ProductVersionID != "")
                query.Append(" AND PP.ProductVersionID = " + projectPhaseData.ProductVersionID);

            if (projectPhaseData.ProjectPhaseID != "")
                query.Append(" AND PCD.ProjectPhaseID = " + projectPhaseData.ProjectPhaseID);

            if (projectPhaseData.PhaseTypeID != "")
                query.Append(" AND PP.PhaseTypeID = " + projectPhaseData.PhaseTypeID);

            if (projectPhaseData.ProductSprintID != "")
                query.Append(" AND PP.ProductSprintID = " + projectPhaseData.ProductSprintID);

            query.Append(" ORDER BY PP.PhaseEndDate Desc, PP.PhaseStartDate Desc, PP.ProjectPhase");

            return query.ToString();
        }

        /// <summary>
        /// GetLocaleVsPlatformMatrixData
        /// </summary>
        /// <param name="projectPhasesData"></param>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public static string GetLocaleVsPlatformMatrixData(DTO.ProjectLocaleVsPlatformMatrix matrixData)
        {
            StringBuilder query = new StringBuilder();

            if (matrixData.ProductVersionID != "")
            {
                query.Append(" SELECT PP.ProductPlatformID ProjectPlatformID, PL.ProductLocaleID ProjectLocaleID, ");
                query.Append(" Sum(PLPD.TestCasesCount) TestCasesCount, Sum(PLPD.TestCasesPercent) TestCasesPercent, Sum(PLPD.TestCasesExecuted) TestCasesExecuted, Sum(PLPD.TestCasesNA) TestCasesNA, 0 TestCasesRemaining");
            }
            else if (matrixData.ProjectPhaseID != "")
            {
                query.Append(" SELECT PLPD.ProjectPlatformID, PLPD.ProjectLocaleID, ");
                query.Append(" Sum(PLPD.TestCasesCount) TestCasesCount, Sum(PLPD.TestCasesPercent) TestCasesPercent, Sum(PLPD.TestCasesExecuted) TestCasesExecuted, Sum(PLPD.TestCasesNA) TestCasesNA, 0 TestCasesRemaining");
            }
            else
            {
                query.Append(" SELECT PLPD.ProjectDataID, PLPD.ProjectPlatformID, PLPD.ProjectLocaleID, P.Platform, L.Locale, ");
                query.Append(" PLPD.TestSuiteNo, PLPD.SIDs, PLPD.TestCasesCount, PLPD.TestCasesPercent, PLPD.TestCasesExecuted, PLPD.TestCasesNA, 0 TestCasesRemaining");
            }

            query.Append(" FROM ProjectLocaleVsPlatformData PLPD");
            query.Append(" INNER JOIN PhaseCoverageDetails PCD");
            query.Append(" ON PLPD.PhaseCoverageDetailID = PCD.PhaseCoverageDetailID ");

            if (matrixData.PhaseCoverageDetailID != "")
                query.Append(" AND PCD.PhaseCoverageDetailID = " + matrixData.PhaseCoverageDetailID);

            query.Append(" INNER JOIN ProjectLocales PL");
            query.Append(" ON PLPD.ProjectLocaleID = PL.ProjectLocaleID ");
            query.Append(" INNER JOIN ProjectPlatforms PP");
            query.Append(" ON PLPD.ProjectPlatformID = PP.ProjectPlatformID ");

            if (matrixData.ProjectPhaseID != "")
                query.Append(" AND PCD.ProjectPhaseID = " + matrixData.ProjectPhaseID);

            if (matrixData.VendorID != "")
                query.Append(" AND PL.VendorID IN (" + matrixData.VendorID + ")");

            if (matrixData.ProductVersionID != "")
            {
                query.Append(" INNER JOIN ProductLocales PDL");
                query.Append(" ON PDL.ProductLocaleID = PL.ProductLocaleID ");
                query.Append(" AND PDL.ProductVersionID = " + matrixData.ProductVersionID);

                query.Append(" GROUP BY PP.ProductPlatformID, PL.ProductLocaleID");
            }
            else if (matrixData.ProjectPhaseID != "")
            {
                query.Append(" GROUP BY PLPD.ProjectPlatformID, PLPD.ProjectLocaleID");
            }
            else
            {
                query.Append(" INNER JOIN ProductPlatforms PDP");
                query.Append(" ON PDP.ProductPlatformID = PP.ProductPlatformID ");
                query.Append(" INNER JOIN Platforms P");
                query.Append(" ON P.PlatformID = PDP.PlatformID ");

                if (matrixData.PlatformTypeID != "")
                    query.Append(string.Format(" AND P.PlatformTypeID = {0}", matrixData.PlatformTypeID));

                if (matrixData.ProjectBuildDetailID != "")
                {
                    query.Append(" INNER JOIN ProjectBuildLocales PBL");
                    query.Append(" ON PBL.ProductLocaleID = PL.ProductLocaleID ");
                    query.Append(" INNER JOIN ProjectBuildDetails PBD");
                    query.Append(" ON PBD.ProjectBuildDetailID = PBL.ProjectBuildDetailID ");
                    query.Append(string.Format(" AND PBD.ProjectBuildDetailID = {0}", matrixData.ProjectBuildDetailID));
                }

                query.Append(" INNER JOIN ProductLocales PDL");
                query.Append(" ON PDL.ProductLocaleID = PL.ProductLocaleID ");
                query.Append(" INNER JOIN Locales L");
                query.Append(" ON PDL.LocaleID = L.LocaleID ");

                if (matrixData.ProductLocaleID != "")
                    query.Append(string.Format(" AND PDL.ProductLocaleID = {0}", matrixData.ProductLocaleID));

                query.Append(" ORDER BY P.DisplayOrder ASC, L.DisplayOrder ASC");

            }

            return query.ToString();
        }

        /// <summary>
        /// AddProjectPhase
        /// </summary>
        /// <param name="projectPhaseData"></param>
        /// <returns></returns>
        public static string AddProjectPhase(DTO.ProjectPhases projectPhaseData)
        {
            StringBuilder query = new StringBuilder(" Insert into ProjectPhases(ProductVersionID, ProjectPhase, PhaseTypeID, ProductSprintID, StatusID, TestingTypeID, PhaseStartDate, PhaseEndDate,");

            query.Append(" AddedOn, AddedBy) Values(");

            query.Append(projectPhaseData.ProductVersionID + ",");
            query.Append(" '" + projectPhaseData.ProjectPhase + "',");
            query.Append(projectPhaseData.PhaseTypeID + ",");
            query.Append(CheckForNull(projectPhaseData.ProductSprintID) + ",");
            query.Append(projectPhaseData.StatusID + ",");
            query.Append(projectPhaseData.TestingTypeID + ",");
            query.Append(" '" + FormattedDate(projectPhaseData.StartDate) + "',");
            query.Append(" '" + FormattedDate(projectPhaseData.EndDate) + "',");
            query.Append(" SysDate(),");
            query.Append(" '" + projectPhaseData.DataHeader.LoginID + "')");

            return query.ToString();
        }

        /// <summary>
        /// UpdateProjectPhase
        /// </summary>
        /// <param name="projectPhaseData"></param>
        /// <returns></returns>
        public static string UpdateProjectPhase(DTO.ProjectPhases projectPhaseData)
        {
            StringBuilder query = new StringBuilder();

            query.Append(" Update ProjectPhases Set ");

            if (!projectPhaseData.IsUpdatePhaseDetails)
            {
                query.Append(" ProjectPhase = '" + projectPhaseData.ProjectPhase + "',");
                query.Append(" TestCasesPlanned = " + projectPhaseData.TestCasesPlanned + ",");
                query.Append(" PhaseTypeID = " + projectPhaseData.PhaseTypeID + ",");
                query.Append(" ProductSprintID = " + CheckForNull(projectPhaseData.ProductSprintID) + ",");
                query.Append(" StatusID = " + projectPhaseData.StatusID + ",");
                query.Append(" TestingTypeID = " + projectPhaseData.TestingTypeID + ",");
                query.Append(" PhaseStartDate = '" + FormattedDate(projectPhaseData.StartDate) + "',");
                query.Append(" PhaseEndDate = '" + FormattedDate(projectPhaseData.EndDate) + "',");
            }
            else
            {
                if (projectPhaseData.AboutProjectPhase != "")
                    query.Append(" AboutProjectPhase = '" + projectPhaseData.AboutProjectPhase + "',");
            }

            query.Append(" UpdatedOn = SYSDATE(),");
            query.Append(" UpdatedBy = '" + projectPhaseData.DataHeader.LoginID + "'");
            query.Append(" WHERE ProjectPhaseID = " + projectPhaseData.ProjectPhaseID + " ; ");

            if (projectPhaseData.IsUpdatePhaseDetails)
            {
                query.Append(UpdateProjectPhaseLocales(projectPhaseData));
                query.Append(UpdateProjectPhasePlatforms(projectPhaseData));
            }
            return query.ToString();
        }

        /// <summary>
        /// CopyProjectPhasesData
        /// </summary>
        /// <param name="projectPhaseData"></param>
        /// <returns></returns>
        public static string CopyProjectPhasesData(DTO.ProjectPhases projectPhaseData)
        {
            StringBuilder query = new StringBuilder();

            if (projectPhaseData.IsCopyLocales)
            {
                query.Append(" Insert Into ProjectLocales(ProductLocaleID, ProjectPhaseID, VendorID, LocaleWeight, AddedBy, AddedOn)");
                query.Append(String.Format(" Select ProductLocaleID, {0}, VendorID, LocaleWeight, '{1}', SysDate()", projectPhaseData.ProjectPhaseID, projectPhaseData.DataHeader.LoginID));
                query.Append(" From ProjectLocales");
                query.Append(" Where ProjectPhaseID = " + projectPhaseData.CopyProjectPhaseID);
                query.Append(" ; ");
            }

            if (projectPhaseData.IsCopyPlatforms)
            {
                query.Append(" Insert Into ProjectPlatforms(ProductPlatformID, ProjectPhaseID, AddedBy, AddedOn)");
                query.Append(String.Format(" Select ProductPlatformID, {0}, '{1}', SysDate()", projectPhaseData.ProjectPhaseID, projectPhaseData.DataHeader.LoginID));
                query.Append(" From ProjectPlatforms");
                query.Append(" Where ProjectPhaseID = " + projectPhaseData.CopyProjectPhaseID);
                query.Append(" ; ");
            }

            //if (projectPhaseData.IsCopyMatrix)
            //{
            //    query.Append(" Insert Into ProjectLocaleVsPlatformData(ProjectPhaseID, UserProjectRoleID, AddedBy, AddedOn)");
            //    query.Append(String.Format(" Select {0}, UserProjectRoleID, '{1}', SysDate()", projectPhaseData.ProjectPhaseID, projectPhaseData.DataHeader.LoginID));
            //    query.Append(" From UserProducts");
            //    query.Append(" Where ProjectPhaseID = " + projectPhaseData.CopyProjectPhaseID);
            //    query.Append(" ; ");
            //}

            return query.ToString();
        }

        /// <summary>
        /// AddProjectPhaseCoverages
        /// </summary>
        /// <param name="projectPhaseData"></param>
        /// <returns></returns>
        public static string AddProjectPhaseCoverages(DTO.ProjectPhases projectPhaseData)
        {
            StringBuilder query = new StringBuilder(" Insert into PhaseCoverageDetails(ProjectPhaseID, PhaseCoverageDetails, SuiteID, TestCasesCount, ");

            query.Append(" AddedOn, AddedBy) Values(");

            query.Append(projectPhaseData.ProjectPhaseID + ",");
            query.Append(" '" + projectPhaseData.ProjectPhaseCoverage + "',");
            query.Append(" '" + projectPhaseData.SuiteID + "',");
            query.Append(projectPhaseData.TestCasesCount + ",");
            query.Append(" SysDate(),");
            query.Append(" '" + projectPhaseData.DataHeader.LoginID + "')");

            return query.ToString();
        }

        /// <summary>
        /// UpdateProjectPhaseCoverages
        /// </summary>
        /// <param name="projectPhaseData"></param>
        /// <returns></returns>
        public static string UpdateProjectPhaseCoverages(DTO.ProjectPhases projectPhaseData)
        {
            StringBuilder query = new StringBuilder();

            query.Append(" Update PhaseCoverageDetails Set ");

            query.Append(" PhaseCoverageDetails = '" + projectPhaseData.ProjectPhaseCoverage + "',");
            query.Append(" SuiteID = '" + projectPhaseData.SuiteID + "',");
            query.Append(" TestCasesCount = " + projectPhaseData.TestCasesCount + ",");
            query.Append(" UpdatedOn = SYSDATE(),");
            query.Append(" UpdatedBy = '" + projectPhaseData.DataHeader.LoginID + "'");
            query.Append(" WHERE PhaseCoverageDetailID = " + projectPhaseData.PhaseCoverageDetailID);

            return query.ToString();
        }

        /// <summary>
        /// DeleteProjectPhaseCoverages
        /// </summary>
        /// <param name="projectPhaseData"></param>
        /// <returns></returns>
        public static string DeleteProjectPhaseCoverages(DTO.ProjectPhases projectPhaseData)
        {
            StringBuilder query = new StringBuilder();

            query.Append(" Delete From PhaseCoverageDetails ");
            query.Append(" WHERE PhaseCoverageDetailID = " + projectPhaseData.PhaseCoverageDetailID);

            return query.ToString();
        }

        /// <summary>
        /// UpdateProjectPhaseLocales
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static string UpdateProjectPhaseLocales(DTO.ProjectPhases projectPhaseData)
        {
            StringBuilder query = new StringBuilder();
            StringBuilder productLocaleIDs = new StringBuilder("0");

            foreach (string productLocaleID in projectPhaseData.ProductLocaleIDCollection)
            {
                productLocaleIDs.Append("," + productLocaleID);
            }

            query.Append(" Delete From ProjectLocaleVsPlatformData ");
            query.Append(" Where PhasecoverageDetailID IN ");
            query.Append(String.Format(" (Select PhasecoverageDetailID From PhaseCoverageDetails Where ProjectPhaseID = {0}) ", projectPhaseData.ProjectPhaseID));
            query.Append(" AND ProjectLocaleID IN ");
            query.Append(String.Format(" (Select ProjectLocaleID From ProjectLocales Where ProjectPhaseID = {0} AND ProductLocaleID NOT IN ({1}))", projectPhaseData.ProjectPhaseID, productLocaleIDs.ToString()));
            query.Append(" ; ");

            query.Append(" Delete From ProjectLocales");
            query.Append(" Where ProjectPhaseID = " + projectPhaseData.ProjectPhaseID);
            query.Append(" AND ProductLocaleID NOT IN (" + productLocaleIDs.ToString() + "); ");

            for (int counter = 0; counter < projectPhaseData.ProductLocaleIDCollection.Count; counter++)
            {
                string localeWeight = projectPhaseData.LocaleWeightCollection[counter].ToString();
                if (localeWeight == "")
                    localeWeight = "0";

                if (projectPhaseData.ProjectLocaleIDCollection[counter].ToString() == "")
                {
                    query.Append(" Insert Into ProjectLocales(ProductLocaleID, ProjectPhaseID, VendorID, LocaleWeight,");
                    query.Append(" AddedOn, AddedBy) ");
                    query.Append(String.Format(" Values ({0}, {1}, {2}, {3}, {4}, {5}); ", projectPhaseData.ProductLocaleIDCollection[counter], projectPhaseData.ProjectPhaseID, projectPhaseData.LocaleVendorIDCollection[counter], localeWeight, "SysDate()", "'" + projectPhaseData.DataHeader.LoginID + "'"));
                }
                else
                {
                    query.Append(" Update ProjectLocales Set ");
                    query.Append(" VendorID = " + projectPhaseData.LocaleVendorIDCollection[counter] + ",");
                    query.Append(" LocaleWeight = " + localeWeight + ",");
                    query.Append(" UpdatedOn = SYSDATE(),");
                    query.Append(" UpdatedBy = '" + projectPhaseData.DataHeader.LoginID + "'");
                    query.Append(" WHERE ProjectLocaleID = " + projectPhaseData.ProjectLocaleIDCollection[counter] + " ; ");
                }
            }

            return query.ToString();
        }

        /// <summary>
        /// UpdateProjectPhasePlatforms
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static string UpdateProjectPhasePlatforms(DTO.ProjectPhases projectPhaseData)
        {
            StringBuilder query = new StringBuilder();
            StringBuilder productPlatformIDs = new StringBuilder("0");

            foreach (string productPlatformID in projectPhaseData.PlatformIDCollection)
                productPlatformIDs.Append("," + productPlatformID);

            query.Append(" Delete From ProjectLocaleVsPlatformData ");
            query.Append(" Where PhasecoverageDetailID IN ");
            query.Append(String.Format(" (Select PhasecoverageDetailID From PhaseCoverageDetails Where ProjectPhaseID = {0}) ", projectPhaseData.ProjectPhaseID));
            query.Append(" AND ProjectPlatformID IN ");
            query.Append(String.Format(" (Select ProjectPlatformID From ProjectPlatforms Where ProjectPhaseID = {0} AND ProductPlatformID NOT IN ({1}))  ", projectPhaseData.ProjectPhaseID, productPlatformIDs.ToString()));
            query.Append(" ; ");

            //query.Append(" Delete From PLPD, PCD ");
            //query.Append(" using ProjectLocaleVsPlatformData PLPD ");
            //query.Append(" INNER JOIN PhaseCoverageDetails PCD ");
            //query.Append(" ON PCD.PhaseCoverageDetailID = PLPD.PhaseCoverageDetailID");
            //query.Append(" AND PCD.ProjectPhaseID = " + projectPhaseData.ProjectPhaseID);
            //query.Append(" AND ProjectPlatformID IN (");
            //query.Append(" Select ProjectPlatformID From ProjectPlatforms ");
            //query.Append(" Where ProjectPhaseID = " + projectPhaseData.ProjectPhaseID);
            //query.Append(" AND ProductPlatformID NOT IN (" + productPlatformIDs.ToString() + ") ");
            //query.Append(" ) ");
            //query.Append(" ; ");

            query.Append(" Delete From ProjectPlatforms");
            query.Append(" Where ProjectPhaseID = " + projectPhaseData.ProjectPhaseID);
            query.Append(" AND ProductPlatformID NOT IN (" + productPlatformIDs.ToString() + "); ");

            query.Append(" Insert Into ProjectPlatforms(ProjectPhaseID, ProductPlatformID, ");
            query.Append(" AddedOn, AddedBy) ");
            query.Append(" Select " + projectPhaseData.ProjectPhaseID + ", ProductPlatformID, SysDate(), '" + projectPhaseData.DataHeader.LoginID + "' From ProductPlatforms");
            query.Append(" WHERE ProductPlatformID IN (" + productPlatformIDs.ToString() + ")");
            query.Append(" AND ProductPlatformID NOT IN (");
            query.Append(" Select ProductPlatformID From ProjectPlatforms WHERE ProjectPhaseID = " + projectPhaseData.ProjectPhaseID);
            query.Append(")");

            return query.ToString();
        }

        ///// <summary>
        ///// UpdateProjectLocalesVsPlatforms
        ///// </summary>
        ///// <param name="projectPhaseData"></param>
        ///// <returns></returns>
        //public static string UpdateProjectLocalesVsPlatforms(DTO.ProjectPhases projectPhaseData)
        //{
        //    StringBuilder query = new StringBuilder();

        //    query.Append(" Delete From ProjectLocaleVsPlatformData ");
        //    query.Append(" WHERE ProjectPhaseID = " + projectPhaseData.ProjectPhaseID);
        //    query.Append(" AND NOT EXISTS(");
        //    query.Append(" SELECT pl.ProjectLocaleID, pp.ProjectPlatformID");
        //    query.Append(" FROM ProjectLocales pl INNER JOIN Projectplatforms pp ON pl.ProjectPhaseID = pp.ProjectPhaseID ");
        //    query.Append(" WHERE pl.ProjectPhaseID = " + projectPhaseData.ProjectPhaseID);
        //    query.Append(" AND pl.ProjectLocaleID = ProjectLocaleVsPlatformData.ProjectLocaleID");
        //    query.Append(" AND pp.ProjectPlatformID = ProjectLocaleVsPlatformData.ProjectPlatformID");
        //    query.Append(") ;");

        //    query.Append(" Insert Into ProjectLocaleVsPlatformData(ProjectPhaseID, ProjectLocaleID, ProjectPlatformID, ");
        //    query.Append(" AddedOn, AddedBy) ");
        //    query.Append(" SELECT pl.ProjectPhaseID, pl.ProjectLocaleID, pp.ProjectPlatformID, SysDate(), '" + projectPhaseData.DataHeader.LoginID + "'");
        //    query.Append(" FROM ProjectLocales pl INNER JOIN Projectplatforms pp ON pl.ProjectPhaseID = pp.ProjectPhaseID ");
        //    query.Append(" WHERE pl.ProjectPhaseID = " + projectPhaseData.ProjectPhaseID);
        //    query.Append(" AND NOT EXISTS(");
        //    query.Append(" SELECT plpd.ProjectLocaleID, plpd.ProjectPlatformID");
        //    query.Append(" FROM projectlocalevsplatformdata plpd ");
        //    query.Append(" WHERE pl.ProjectLocaleID = plpd.ProjectLocaleID");
        //    query.Append(" AND pp.ProjectPlatformID = plpd.ProjectPlatformID");
        //    query.Append(") ;");

        //    return query.ToString();
        //}

        /// <summary>
        /// UpdateProjectLocalesVsPlatformsMatrix
        /// </summary>
        /// <param name="projectPhaseData"></param>
        /// <returns></returns>
        public static string UpdateProjectLocalesVsPlatformsMatrix(DTO.ProjectPhases projectPhaseData)
        {
            StringBuilder query = new StringBuilder();

            foreach (DTO.ProjectLocaleVsPlatformMatrix matrixData in projectPhaseData.ProjectLocaleVsPlatformMatrix)
            {
                if (matrixData.ProjectDataID == "")
                {
                    query.Append(" Insert into ProjectLocaleVsPlatformData (ProjectPhaseID, ProjectLocaleID, ProjectPlatformID, TestSuiteNo, TestCasesCount,");
                    query.Append(" AddedOn, AddedBy) Values(");

                    query.Append(projectPhaseData.ProjectPhaseID + ",");
                    query.Append(matrixData.ProjectLocaleID + ",");
                    query.Append(matrixData.ProjectPlatformID + ",");
                    query.Append("'" + matrixData.TsNo + "',");
                    query.Append(matrixData.TC_Count + ",");
                    query.Append(" SysDate(),");
                    query.Append(" '" + projectPhaseData.DataHeader.LoginID + "')");
                }
                else
                {
                    query.Append(" UPDATE ProjectLocaleVsPlatformData SET");

                    if (matrixData.UserAccess == WebConstants.STR_OTMATRIX_ACCESS_DEFINE)
                    {
                        query.Append(" TestSuiteNo = '" + matrixData.TsNo + "',");
                        query.Append(" TestCasesCount = " + matrixData.TC_Count + ",");
                    }
                    if (matrixData.UserAccess == WebConstants.STR_OTMATRIX_ACCESS_UPDATE)
                    {
                        query.Append(" SIDs = '" + matrixData.SIDs + "',");
                        query.Append(" TestCasesPercent = " + matrixData.TC_Percent + ",");
                        query.Append(" TestCasesNA = " + matrixData.TC_NA + ",");
                        query.Append(" TestCasesExecuted = " + matrixData.TC_Executed + ",");
                    }
                    query.Append(" UpdatedOn = SYSDATE(),");
                    query.Append(" UpdatedBy = '" + projectPhaseData.DataHeader.LoginID + "'");
                    query.Append(" WHERE ProjectDataID = " + matrixData.ProjectDataID);
                }
                query.Append(" ; ");
            }

            return query.ToString();
        }

        #endregion

        #region Product WindUp

        /// <summary>
        /// GetProjectBuildDetails
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static string GetProjectBuildDetails(DTO.Product productData)
        {
            StringBuilder query = new StringBuilder();
            query.Append(" Select ProjectBuildDetailID, ProjectBuildCode, ProjectBuild, IsReleaseBuild, GMBuildNo, GMDate, GMBuildPath");
            query.Append(" From ProjectBuildDetails ");
            query.Append(" WHERE ProductVersionID = " + productData.ProductVersionID);

            if (productData.IsReleaseBuild == "1")
                query.Append(" AND isReleaseBuild = 1");

            return query.ToString();
        }

        /// <summary>
        /// AddProjectBuildDetails
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static string AddProjectBuildDetails(DTO.Product productData)
        {
            StringBuilder query = new StringBuilder("Insert Into ProjectBuildDetails");

            query.Append("(ProductVersionID, ProjectBuildCode, ProjectBuild, IsReleaseBuild, AddedOn, AddedBy)");
            query.Append(" Values (");

            query.Append(productData.ProductVersionID + ",");
            query.Append("'" + productData.ProjectBuildCode + "',");
            query.Append("'" + productData.ProjectBuildDetails + "',");
            query.Append(productData.IsReleaseBuild + ",");
            query.Append("SysDate(),");
            query.Append("'" + productData.DataHeader.LoginID + "')");

            return query.ToString();
        }

        /// <summary>
        /// UpdateProjectBuildDetails
        /// </summary>
        /// <param name="holidayData"></param>
        /// <returns></returns>
        public static string UpdateProjectBuildDetails(DTO.Product productData)
        {
            StringBuilder query = new StringBuilder("Update ProjectBuildDetails SET ");

            query.Append("IsReleaseBuild = " + productData.IsReleaseBuild + ",");
            query.Append("ProjectBuildCode = '" + productData.ProjectBuildCode + "',");
            query.Append("ProjectBuild = '" + productData.ProjectBuildDetails + "',");
            query.Append("UpdatedOn = SYSDATE(),");
            query.Append("UpdatedBy = '" + productData.DataHeader.LoginID + "'");
            query.Append(" WHERE ProjectBuildDetailID = " + productData.ProjectBuildDetailID);

            return query.ToString();
        }

        /// <summary>
        /// DeleteProjectBuildDetails
        /// </summary>
        /// <param name="holidayData"></param>
        /// <returns></returns>
        public static string DeleteProjectBuildDetails(DTO.Product productData)
        {
            StringBuilder query = new StringBuilder();

            query.Append(" Delete From ProjectBuildLocales");
            query.Append(" Where ProjectBuildDetailID = " + productData.ProjectBuildDetailID);

            query.Append(" ; ");

            query.Append(" Delete From ProjectBuildDetails");
            query.Append(" Where ProjectBuildDetailID = " + productData.ProjectBuildDetailID);

            return query.ToString();
        }

        /// <summary>
        /// GetProjectBuildLocales
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static string GetProjectBuildLocales(DTO.Product productData)
        {
            StringBuilder query = new StringBuilder();

            query.Append(" Select PBL.ProjectBuildLocaleID, PBL.ProjectBuildDetailID, PBD.ProjectBuildCode, PBD.ProjectBuild, PBL.ProductLocaleID, L.Locale");
            query.Append(" From ProjectBuildLocales PBL ");
            query.Append(" INNER JOIN ProjectBuildDetails PBD ");
            query.Append(" ON PBL.ProjectBuildDetailID = PBD.ProjectBuildDetailID");
            query.Append(" AND PBD.ProductVersionID = " + productData.ProductVersionID);

            if (productData.ProjectBuildDetailID != "")
                query.Append(" AND PBD.ProjectBuildDetailID = " + productData.ProjectBuildDetailID);

            query.Append(" INNER JOIN ProductLocales PL ");
            query.Append(" ON PL.ProductLocaleID = PBL.ProductLocaleID");
            query.Append(" INNER JOIN Locales L ");
            query.Append(" ON PL.LocaleID = L.LocaleID");

            query.Append(" ORDER BY PBD.ProjectBuild ASC, L.Locale ASC");

            return query.ToString();
        }

        /// <summary>
        /// AddUpdateProjectBuildLocales
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static string AddUpdateProjectBuildLocales(DTO.Product productData)
        {
            StringBuilder query = new StringBuilder();

            for (int counter = 0; counter < productData.LocaleIDCollection.Count; counter++)
            {
                if (productData.ProjectBuildLocaleIDCollection[counter].ToString() == "")
                {
                    query.Append(" Insert Into ProjectBuildLocales(ProjectBuildDetailID, ProductLocaleID, AddedOn, AddedBy) ");
                    query.Append(" Values (");
                    query.Append(productData.ProjectBuildDetailIDCollection[counter].ToString() + ", ");
                    query.Append(productData.LocaleIDCollection[counter].ToString() + ",");
                    query.Append("SysDate(),");
                    query.Append("'" + productData.DataHeader.LoginID + "')");
                    query.Append(" ; ");
                }
                else
                {
                    query.Append("Update ProjectBuildLocales SET ");
                    query.Append("ProjectBuildDetailID = " + productData.ProjectBuildDetailIDCollection[counter].ToString() + ",");
                    query.Append("UpdatedOn = SYSDATE(),");
                    query.Append("UpdatedBy = '" + productData.DataHeader.LoginID + "'");
                    query.Append(" WHERE ProjectBuildLocaleID = " + productData.ProjectBuildLocaleIDCollection[counter].ToString());
                    query.Append(" ; ");
                }
            }

            return query.ToString();
        }

        /// <summary>
        /// GetProjectWindUpDetails
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static string GetProjectWindUpDetails(DTO.Product productData)
        {
            StringBuilder query = new StringBuilder();
            query.Append("Select * from ProjectWindUp ");
            query.Append(" WHERE ProductVersionID = " + productData.ProductVersionID);

            return query.ToString();
        }

        ///// <summary>
        ///// GetProjectGMDetails
        ///// </summary>
        ///// <param name="productData"></param>
        ///// <returns></returns>
        //public static string GetProjectGMDetails(DTO.Product productData)
        //{
        //    StringBuilder query = new StringBuilder();
        //    query.Append("Select * from ProjectGMDetails ");

        //    if (productData.WindUpId != "")
        //        query.Append(" WHERE ProjectWindUpID = " + productData.WindUpId);
        //    else
        //        query.Append(" WHERE ProjectWindUpID = -1");

        //    return query.ToString();
        //}

        /// <summary>
        /// AddProjectWindUpDetails
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static string AddProjectWindUpDetails(DTO.Product productData)
        {
            StringBuilder query = new StringBuilder("Insert into ProjectWindUp(ProductVersionID, PostMortemAnalysis, Learnings, BestPractices,");

            query.Append("AddedOn, AddedBy) Values(");

            query.Append(productData.ProductVersionID + ",");
            query.Append("'" + productData.PostMortemDetails + "',");
            query.Append("'" + productData.Learnings + "',");
            query.Append("'" + productData.BestPractices + "',");
            query.Append("SysDate(),");
            query.Append("'" + productData.DataHeader.LoginID + "')");

            return query.ToString();
        }

        /// <summary>
        /// UpdateProjectWindUpDetails
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static string UpdateProjectWindUpDetails(DTO.Product productData)
        {
            StringBuilder query = new StringBuilder();

            query.Append(" Update ProjectWindUp Set ");

            query.Append(" PostMortemAnalysis = '" + productData.PostMortemDetails + "',");
            query.Append(" Learnings = '" + productData.Learnings + "',");
            query.Append(" BestPractices = '" + productData.BestPractices + "',");
            query.Append(" UpdatedOn = SYSDATE(),");
            query.Append(" UpdatedBy = '" + productData.DataHeader.LoginID + "'");
            query.Append(" WHERE ProjectWindUpID = " + productData.WindUpId + " ; ");

            return query.ToString();
        }

        /// <summary>
        /// UpdateProjectWindUpDetails
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static string SubmitWindUpDetails(DTO.Product productData)
        {
            StringBuilder query = new StringBuilder();

            query.Append(" Update ProductVersions Set ");

            query.Append(" isActive = 0,");
            query.Append(" isClosed = 1,");
            query.Append(" UpdatedOn = SYSDATE(),");
            query.Append(" UpdatedBy = '" + productData.DataHeader.LoginID + "'");
            query.Append(" WHERE ProductVersionID = " + productData.ProductVersionID + " ; ");

            return query.ToString();
        }

        ///// <summary>
        ///// AddProjectGMDetails
        ///// </summary>
        ///// <param name="productData"></param>
        ///// <returns></returns>
        //public static string AddProjectGMDetails(DTO.Product productData)
        //{
        //    StringBuilder query = new StringBuilder("Insert into ProjectGMDetails(ProjectWindUpID, ProductBuildID, GMBuildNo, GMDate,");

        //    query.Append("AddedOn, AddedBy) Values(");

        //    query.Append(productData.WindUpId + ",");
        //    query.Append(productData.ProductBuildID + ",");
        //    query.Append("'" + productData.BuildNo + "',");
        //    query.Append("'" + FormattedDate(productData.GmDate) + "',");
        //    query.Append("SysDate(),");
        //    query.Append("'" + productData.DataHeader.LoginID + "')");

        //    return query.ToString();
        //}

        /// <summary>
        /// UpdateProjectGMDetails
        /// </summary>
        /// <param name="productData"></param>
        /// <returns></returns>
        public static string UpdateProjectGMDetails(DTO.Product productData)
        {
            StringBuilder query = new StringBuilder();

            query.Append(" Update ProjectBuildDetails Set ");

            query.Append(" GMBuildNo = '" + productData.BuildNo + "',");
            query.Append(" GMBuildPath = '" + productData.BuildPath + "',");
            query.Append(" GMDate = '" + FormattedDate(productData.GmDate) + "',");
            query.Append(" UpdatedOn = SYSDATE(),");
            query.Append(" UpdatedBy = '" + productData.DataHeader.LoginID + "'");
            query.Append(" WHERE ProjectBuildDetailID = " + productData.ProjectBuildDetailID + " ; ");

            return query.ToString();
        }

        #endregion

        #region Holidays

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtoHolidayData"></param>
        /// <returns></returns>
        public static string GetHolidaysDate(DTO.Holidays dtoHolidayData)
        {
            StringBuilder query = new StringBuilder();

            query.Append("Select Min(StartDate) MinDate, Max(EndDate) MaxDate From Holidays");

            //if (dtoHolidayData.ReportingType == "Monthly")
            //{
            //    query.Append(" Select Distinct cast((CONCAT(DATE_FORMAT(EndDate, '%M') , ' , ' , YEAR(EndDate))) As char(20)) AS Month");
            //    query.Append(" From Holidays ");
            //    query.Append(" WHERE YEAR(EndDate) >= YEAR(SYSDATE())-1");
            //}

            //if (dtoHolidayData.ReportingType == "Yearly")
            //{
            //    query.Append(" Select Distinct YEAR(EndDate) AS Month");
            //    query.Append(" From Holidays ");
            //    query.Append(" WHERE YEAR(EndDate) >= YEAR(SYSDATE())-1");
            //}

            query.Append(" WHERE StartDate >= DATE_SUB(SysDate(), INTERVAL 6 MONTH)");

            //query.Append(" ORDER BY EndDate Desc");

            return query.ToString();
        }

        /// <summary>
        /// GetHolidaysList
        /// </summary>
        /// <param name="holidayData"></param>
        /// <returns></returns>
        public static string GetHolidaysList(DTO.Holidays dtoHolidayData)
        {
            StringBuilder query = new StringBuilder(" Select Distinct Hol.HolidayID, Hol.VendorID, vend.Vendor, Hol.HolidayReason, Hol.StartDate, Hol.EndDate ");

            query.Append(" From Holidays Hol ");
            query.Append(" INNER JOIN Vendors  Vend");
            query.Append(" ON  Hol.VendorID = Vend.VendorID");

            if (dtoHolidayData.VendorID != "")
                query.Append(" AND  Hol.VendorID = " + dtoHolidayData.VendorID);

            if (dtoHolidayData.Month != "")
            {
                query.Append(" AND MONTH(Hol.StartDate) <= '" + dtoHolidayData.Month + "'");
                query.Append(" AND MONTH(Hol.EndDate) >= '" + dtoHolidayData.Month + "'");
                query.Append(" AND YEAR(Hol.StartDate) = '" + dtoHolidayData.Year + "'");
            }
            else
            {
                query.Append(" AND ( YEAR(Hol.StartDate) = '" + dtoHolidayData.Year + "'");
                query.Append(" OR YEAR(Hol.EndDate) = '" + dtoHolidayData.Year + "' )");
            }

            query.Append(" ORDER BY EndDate, StartDate Desc");

            return query.ToString();
        }

        /// <summary>
        /// AddHolidaysListData
        /// </summary>
        /// <param name="dtoHolidayData"></param>
        /// <returns></returns>
        public static string AddHolidaysListData(DTO.Holidays dtoHolidayData)
        {
            StringBuilder query = new StringBuilder("Insert Into Holidays");

            query.Append("(VendorID, HolidayReason, StartDate, EndDate, AddedOn, AddedBy)");
            query.Append("Values (");

            query.Append(dtoHolidayData.VendorID + ",");
            query.Append("'" + dtoHolidayData.HolidayReason + "',");
            query.Append("'" + FormattedDate(dtoHolidayData.StartDate) + "',");
            query.Append("'" + FormattedDate(dtoHolidayData.EndDate) + "',");
            query.Append("SysDate(),");
            query.Append("'" + dtoHolidayData.DataHeader.LoginID + "')");

            return query.ToString();
        }

        /// <summary>
        /// UpdateHolidaysListData
        /// </summary>
        /// <param name="holidayData"></param>
        /// <returns></returns>
        public static string UpdateHolidaysListData(DTO.Holidays dtoHolidayData)
        {
            StringBuilder query = new StringBuilder("Update Holidays SET ");

            query.Append("VendorID = " + dtoHolidayData.VendorID + ",");
            query.Append("HolidayReason = '" + dtoHolidayData.HolidayReason + "',");
            query.Append("StartDate = '" + FormattedDate(dtoHolidayData.StartDate) + "',");
            query.Append("EndDate = '" + FormattedDate(dtoHolidayData.EndDate) + "',");
            query.Append("UpdatedOn = SYSDATE(),");
            query.Append("UpdatedBy = '" + dtoHolidayData.DataHeader.LoginID + "'");
            query.Append(" WHERE HolidayID = " + dtoHolidayData.HolidayID);

            return query.ToString();
        }

        /// <summary>
        /// DeleteHolidaysListData
        /// </summary>
        /// <param name="holidayData"></param>
        /// <returns></returns>
        public static string DeleteHolidaysListData(DTO.Holidays dtoHolidayData)
        {
            StringBuilder query = new StringBuilder("Delete From Holidays ");

            query.Append(" Where HolidayID = " + dtoHolidayData.HolidayID);

            return query.ToString();
        }

        #endregion

        #region Screens

        /// <summary>
        /// GetScreensDetails
        /// </summary>
        /// <returns></returns>
        public static string GetScreensDetails(DTO.Screens screenData)
        {
            StringBuilder query = new StringBuilder();

            query.Append(" Select S.ScreenID, S.ParentScreenID, S.ScreenIdentifier, S.Sequence, S.PageName, S.IsProductType, ");
            query.Append(" (Case When IsNull(S.ParentScreenID) = 1 Then S.ScreenIdentifier Else  CONCAT(PS.ScreenIdentifier, ' - ', S.ScreenIdentifier) END) Screen");

            if (screenData.ScreenIdentifier != "")
            {
                query.Append(" From (Select * From Screens ");
                query.Append(" Where ScreenIdentifier = '" + screenData.ScreenIdentifier + "'");
                query.Append(" ) S");
            }
            else
            {
                query.Append(" From Screens S");
            }

            query.Append(" Left Outer Join Screens PS");
            query.Append(" On S.ParentScreenID = PS.ScreenID");

            if (!screenData.IsAllScreens)
            {
                query.Append(" AND S.IsPage = 0");
                query.Append(" AND S.Sequence <> 0 ");
            }

            query.Append(" ORDER BY Screen");

            return query.ToString();
        }

        /// <summary>
        /// GetMasterScreensDetails
        /// </summary>
        /// <returns></returns>
        public static string GetMasterScreensDetails(DTO.Screens screenData)
        {
            StringBuilder query = new StringBuilder();

            query.Append(" Select MD.ScreenID, MD.TableName, MD.IsCode, MD.IsType, MD.TypeTableName, MD.IsTypeCode, MD.IsSubScreen, MD.Filter");
            query.Append(" From MasterData MD");
            query.Append(" INNER JOIN Screens S");
            query.Append(" On MD.ScreenID = S.ScreenID");

            if (screenData.ScreenIdentifier != "")
                query.Append(" AND S.ScreenIdentifier = '" + screenData.ScreenIdentifier + "'");

            return query.ToString();
        }

        /// <summary>
        /// GetScreenLocalizedLabels
        /// </summary>
        /// <returns></returns>
        public static string GetScreenLocalizedLabels(DTO.Screens screenData)
        {
            StringBuilder query = new StringBuilder();


            if (screenData.ScreenID != "")
            {
                query.Append(" Select S.ScreenID, S.ScreenIdentifier, SLL.ScreenLocalizedLabelID, SL.ScreenLabelID, SL.ScreenLabel, SL.ScreenValue, ");
                query.Append(String.Format(" (Case When '{0}' = '{1}' Then SL.ScreenValue When IsNull(L.LocaleCode)= 1 THEN '' Else SLL.LocalizedValue End) LocalizedValue", screenData.LocaleCode, System.Configuration.ConfigurationManager.AppSettings["EnglishLocaleCode"]));
            }
            else
            {
                query.Append(" Select S.ScreenID, S.ScreenIdentifier, SL.ScreenLabelID, SL.ScreenLabel, SL.ScreenValue, ");
                query.Append(String.Format(" (Case When '{0}' = '{1}' Then SL.ScreenValue When IsNull(L.LocaleCode)= 1 THEN SL.ScreenValue Else SLL.LocalizedValue End) LocalizedValue", screenData.LocaleCode, System.Configuration.ConfigurationManager.AppSettings["EnglishLocaleCode"]));
            }

            query.Append(" From ScreenLabels SL");
            query.Append(" INNER JOIN ScreenLabelAssociations SLA");
            query.Append(" ON SLA.ScreenLabelID = SL.ScreenLabelID");
            query.Append(" INNER JOIN Screens S");
            query.Append(" ON SLA.ScreenID = S.ScreenID");

            if (screenData.ScreenID != "")
                query.Append(" AND S.ScreenID = " + screenData.ScreenID);

            if (screenData.ScreenLabel != "")
                query.Append(" AND SL.ScreenLabel= '" + screenData.ScreenLabel + "%'");

            if (screenData.ScreenIdentifier != "")
            {
                if (screenData.IsIncludeCommon)
                {
                    query.Append(" AND (S.ScreenIdentifier = '" + screenData.ScreenIdentifier + "'");
                    query.Append(" OR S.ScreenIdentifier = '" + WebConstants.SCREEN_COMMON + "')");
                }
                else
                    query.Append(" AND S.ScreenIdentifier = '" + screenData.ScreenIdentifier + "'");
            }

            if (screenData.LabelCategoryID != "")
                query.Append(" AND SL.LabelCategoryID = " + screenData.LabelCategoryID);

            query.Append(" Left Outer Join ScreenLocalizedLabels SLL");
            query.Append(" On SLL.ScreenLabelID =SL.ScreenLabelID");
            query.Append(" Left Outer JOIN Locales L");
            query.Append(" ON  L.LocaleID = SLL.LocaleID ");
            query.Append(" AND L.LocaleID = " + screenData.LocaleID);

            query.Append(" ORDER BY SL.ScreenLabel");

            return query.ToString();
        }

        /// <summary>
        /// GetProjectRoles
        /// </summary>
        /// <returns></returns>
        public static string GetProjectRoles()
        {
            StringBuilder query = new StringBuilder();

            query.Append(" Select Distinct PR.ProjectRoleID, PR.ProjectRole, PR.ProjectRoleCode, R.RoleCode, R.Role ");
            query.Append(" FROM ProjectRoles PR");
            query.Append(" INNER JOIN Roles R");
            query.Append(" ON PR.RoleID = R.RoleID");

            return query.ToString();
        }

        /// <summary>
        /// GetScreenAccess
        /// </summary>
        /// <returns></returns>
        public static string GetScreenAccess()
        {
            StringBuilder query = new StringBuilder();

            query.Append(" Select SAL.ScreenID, SAL.ProjectRoleID, SAL.IsRead, SAL.IsReadWrite, SAL.IsReport ");
            query.Append(" From ScreenAccessLevels SAL");

            return query.ToString();
        }

        /// <summary>
        /// AddScreensDetails
        /// </summary>
        /// <param name="screensData"></param>
        /// <returns></returns>
        public static string AddScreensDetails(DTO.Screens screensData)
        {
            StringBuilder query = new StringBuilder("Insert Into Screens");

            query.Append("(ParentScreenID, ScreenIdentifier, Sequence, IsPage, PageName, AddedOn, AddedBy)");
            query.Append("Values (");

            query.Append(screensData.ParentScreenID + ",");
            query.Append("'" + screensData.ScreenIdentifier + "',");
            query.Append(screensData.Sequence + ",");
            query.Append(screensData.IsPage + ",");
            query.Append("'" + screensData.PageName + "',");
            query.Append("SysDate(),");
            query.Append("'" + screensData.DataHeader.LoginID + "')");

            return query.ToString();
        }

        /// <summary>
        /// UpdateScreensDetails
        /// </summary>
        /// <param name="screensData"></param>
        /// <returns></returns>
        public static string UpdateScreensDetails(DTO.Screens screensData)
        {
            StringBuilder query = new StringBuilder("Update Screens SET ");

            query.Append(" ParentScreenID = " + screensData.ParentScreenID + ",");
            query.Append(" ScreenIdentifier = '" + screensData.ScreenIdentifier + "',");
            query.Append(" Sequence = " + screensData.Sequence + ",");
            query.Append(" IsPage = " + screensData.IsPage + ",");
            query.Append(" PageName = '" + screensData.PageName + "',");
            query.Append(" UpdatedOn = SYSDATE(),");
            query.Append(" UpdatedBy = '" + screensData.DataHeader.LoginID + "'");
            query.Append(" WHERE ScreenID = " + screensData.ScreenID);

            return query.ToString();

        }

        /// <summary>
        /// DeleteScreensDetails
        /// </summary>
        /// <param name="screensData"></param>
        /// <returns></returns>
        public static string DeleteScreensDetails(DTO.Screens screensData)
        {
            StringBuilder query = new StringBuilder("Delete From Screens ");

            query.Append(" Where ScreenID = " + screensData.ScreenID);

            return query.ToString();
        }

        /// <summary>
        /// accessList
        /// </summary>
        /// <param name="accessList"></param>
        /// <returns></returns>
        public static string AddUpdateScreensAccessDetails(ArrayList accessList)
        {
            StringBuilder query = new StringBuilder();

            foreach (DTO.Screens screenAccess in accessList)
            {
                if (screenAccess.ScreenAccessExists == "1")
                {
                    query.Append(" Update ScreenAccessLevels Set");
                    query.Append(" IsRead = " + screenAccess.IsRead + ",");
                    query.Append(" IsReadWrite = " + screenAccess.IsReadWrite + ",");
                    query.Append(" IsReport = " + screenAccess.IsReport + ",");
                    query.Append(" UpdatedOn = SYSDATE(),");
                    query.Append(" UpdatedBy = '" + screenAccess.DataHeader.LoginID + "'");
                    query.Append(" WHERE ScreenID = " + screenAccess.ScreenID);
                    query.Append(" AND ProjectRoleID = " + screenAccess.ProjectRoleID);
                }
                else
                {
                    query.Append(" Insert Into ScreenAccessLevels ");
                    query.Append("(ScreenID, ProjectRoleID, IsRead, IsReadWrite, IsReport, AddedOn, AddedBy)");
                    query.Append("Values (");

                    query.Append(screenAccess.ScreenID + ",");
                    query.Append(screenAccess.ProjectRoleID + ",");
                    query.Append(screenAccess.IsRead + ",");
                    query.Append(screenAccess.IsReadWrite + ",");
                    query.Append(screenAccess.IsReport + ",");
                    query.Append("SysDate(),");
                    query.Append("'" + screenAccess.DataHeader.LoginID + "')");
                }

                query.Append(" ; ");
            }


            return query.ToString();
        }

        /// <summary>
        /// UpdateScreenLocalizedValues
        /// </summary>
        /// <param name="screenData"></param>
        /// <returns></returns>
        public static string UpdateScreenLocalizedValues(DTO.Screens screenData)
        {
            StringBuilder query = new StringBuilder();

            if (screenData.LocaleCode != System.Configuration.ConfigurationManager.AppSettings["EnglishLocaleCode"])
            {
                foreach (DTO.Screens localizedData in screenData.LabelCollections)
                {
                    if (localizedData.ScreenLocalizedLabelID == "")
                    {
                        query.Append(" Insert Into ScreenLocalizedLabels ");
                        query.Append("(ScreenLabelID, LocaleID, LocalizedValue, AddedOn, AddedBy)");
                        query.Append("Values (");
                        query.Append(localizedData.ScreenLabelID + ",");
                        query.Append(screenData.LocaleID + ",");
                        query.Append("'" + localizedData.LocalizedValue + "',");
                        query.Append("SysDate(),");
                        query.Append("'" + screenData.DataHeader.LoginID + "')");
                    }
                    else if (localizedData.LocalizedValue == "")
                    {
                        query.Append(" Delete From ScreenLocalizedLabels ");
                        query.Append(" Where  ScreenLocalizedLabelID =  " + localizedData.ScreenLocalizedLabelID);
                    }
                    else
                    {
                        query.Append(" Update ScreenLocalizedLabels Set ");
                        query.Append(" LocalizedValue = '" + localizedData.LocalizedValue + "',");
                        query.Append(" UpdatedOn = SYSDATE(),");
                        query.Append(" UpdatedBy = '" + screenData.DataHeader.LoginID + "'");
                        query.Append(" Where ScreenLocalizedLabelID =  " + localizedData.ScreenLocalizedLabelID);
                    }
                    query.Append(" ; ");
                }
            }
            else
            {
                foreach (DTO.Screens localizedData in screenData.LabelCollections)
                {
                    query.Append(" Update ScreenLabels Set ");
                    query.Append(" ScreenValue = '" + localizedData.LocalizedValue + "',");
                    query.Append(" UpdatedOn = SYSDATE(),");
                    query.Append(" UpdatedBy = '" + screenData.DataHeader.LoginID + "'");
                    query.Append(" Where  ScreenLabelID =  " + localizedData.ScreenLabelID);

                    query.Append(" ; ");
                }
            }

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
