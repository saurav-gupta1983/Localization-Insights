using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.IO;
using System.Data;
using Adobe.Localization.Insights.Common;
using DTO = Adobe.Localization.Insights.Common.DataTransferObjects;

namespace Adobe.Localization.Insights.DataLayer.TestStudioComponents
{
    /// <summary>
    /// FetchAmalgmConfigsAPI
    /// </summary>
    class FetchAmalgmConfigsAPI
    {
        #region Public Members

        /// <summary>
        /// FetchAmalgmConfigs
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public DTO.TestSudio FetchAmalgmConfigs(DTO.TestSudio ts)
        {
            TestStudioService.TestStudio service = new TestStudioService.TestStudioClient();
            TestStudioService.fetchAmalgmConfigs logObj = new TestStudioService.fetchAmalgmConfigs();
            logObj.SessionID = ts.SessionID;
            logObj.ts_id = Convert.ToInt32(ts.TestSuiteID.Replace("TS_", ""));

            TestStudioService.fetchAmalgmConfigsRequest lr = new TestStudioService.fetchAmalgmConfigsRequest(logObj);
            TestStudioService.fetchAmalgmConfigsResponse1 lres = null;

            try
            {
                lres = service.fetchAmalgmConfigs(lr);

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(lres.fetchAmalgmConfigsResponse.@return);

                DataSet dsConfigurations = new DataSet();

                #region Product Info

                DataTable dtProduct = new DataTable();
                dtProduct.Columns.Add(new DataColumn(WebConstants.COL_PRODUCT_ID, Type.GetType(WebConstants.STR_SYSTEM_INT32)));
                dtProduct.Columns.Add(new DataColumn(WebConstants.COL_PRODUCT, Type.GetType(WebConstants.STR_SYSTEM_STRING)));
                dtProduct.TableName = WebConstants.TBL_PRODUCT;

                dsConfigurations.Tables.Add(FetchIdValuePair(dtProduct, doc.GetElementsByTagName("product")[0]));

                #endregion

                #region Product Versions

                DataTable dtProductVersions = new DataTable();
                dtProductVersions.Columns.Add(new DataColumn(WebConstants.COL_PRODUCT_VERSION_ID, Type.GetType(WebConstants.STR_SYSTEM_INT32)));
                dtProductVersions.Columns.Add(new DataColumn(WebConstants.COL_PRODUCT_VERSION, Type.GetType(WebConstants.STR_SYSTEM_STRING)));
                dtProductVersions.TableName = WebConstants.TBL_PRODUCT_VERSIONS;

                dsConfigurations.Tables.Add(FetchIdValuePair(dtProductVersions, doc.GetElementsByTagName("versions")[0]));

                #endregion

                #region Global Attributes

                XmlNode attributesNode = doc.GetElementsByTagName("globalattributelist")[0];
                foreach (XmlNode attribute in attributesNode.ChildNodes)
                {
                    switch (attribute.ChildNodes[0].InnerText)
                    {
                        case "APP LANGUAGE":
                            #region AppLanguages

                            DataTable dtAppLanguages = new DataTable();
                            dtAppLanguages.Columns.Add(new DataColumn(WebConstants.COL_LOCALE_ID, Type.GetType(WebConstants.STR_SYSTEM_INT32)));
                            dtAppLanguages.Columns.Add(new DataColumn(WebConstants.COL_LOCALE, Type.GetType(WebConstants.STR_SYSTEM_STRING)));
                            dtAppLanguages.TableName = "AppLanguages";

                            dsConfigurations.Tables.Add(FetchIdValuePair(dtAppLanguages, attribute.ChildNodes[3]));

                            #endregion
                            break;

                        case "LANGUAGE":
                            #region Language

                            DataTable dtLanguages = new DataTable();
                            dtLanguages.Columns.Add(new DataColumn(WebConstants.COL_LOCALE_ID, Type.GetType(WebConstants.STR_SYSTEM_INT32)));
                            dtLanguages.Columns.Add(new DataColumn(WebConstants.COL_LOCALE, Type.GetType(WebConstants.STR_SYSTEM_STRING)));
                            dtLanguages.TableName = WebConstants.TBL_LOCALES;

                            dsConfigurations.Tables.Add(FetchIdValuePair(dtLanguages, attribute.ChildNodes[3]));

                            #endregion
                            break;

                        case "PLATFORM":
                            #region Platforms

                            DataTable dtPlatforms = new DataTable();
                            dtPlatforms.Columns.Add(new DataColumn(WebConstants.COL_PLATFORM_ID, Type.GetType(WebConstants.STR_SYSTEM_INT32)));
                            dtPlatforms.Columns.Add(new DataColumn(WebConstants.COL_PLATFORM, Type.GetType(WebConstants.STR_SYSTEM_STRING)));
                            dtPlatforms.TableName = WebConstants.TBL_PLATFORMS;

                            dsConfigurations.Tables.Add(FetchIdValuePair(dtPlatforms, attribute.ChildNodes[3]));

                            #endregion
                            break;
                    }
                }

                #endregion

                ts.Configurations = dsConfigurations;
            }
            catch (Exception ex)
            {
                ts.ErrorCode = "-1";
                ts.ErrorMessage = ex.Message;
            }

            return ts;
        }

        #endregion

        #region Private Members

        /// <summary>
        /// FetchIdValuePair
        /// </summary>
        /// <param name="dtData"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        private DataTable FetchIdValuePair(DataTable dtData, XmlNode node)
        {
            foreach (XmlNode root in node.ChildNodes)
            {
                DataRow dr = dtData.NewRow();

                foreach (XmlNode secondChild in root.ChildNodes)
                    switch (secondChild.Name)
                    {
                        case "id":
                            dr[0] = secondChild.InnerText;
                            break;
                        case "name":
                            dr[1] = secondChild.InnerText;
                            break;
                        case "value":
                            dr[1] = secondChild.InnerText;
                            break;
                    }

                dtData.Rows.Add(dr);
            }

            return dtData;
        }

        #endregion
    }
}
