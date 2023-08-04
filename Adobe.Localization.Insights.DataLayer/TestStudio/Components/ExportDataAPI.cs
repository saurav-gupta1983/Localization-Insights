using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using Adobe.Localization.Insights.Common;
using DTO = Adobe.Localization.Insights.Common.DataTransferObjects;

namespace Adobe.Localization.Insights.DataLayer.TestStudioComponents
{
    /// <summary>
    /// ExportDataAPI
    /// </summary>
    class ExportDataAPI
    {
        #region Public Members

        /// <summary>
        /// ExportTR
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public DTO.TestSudio ExportTR(DTO.TestSudio ts)
        {
            StringBuilder xmlFile = new StringBuilder("<InputXML><searchCriteria>");
            xmlFile.Append("<filterList>");
            xmlFile.Append(String.Format("<filter><fieldName>TR_SUITE_RUN_DISPLAYNAME</fieldName><operator>=</operator><fieldValues><value>{0}</value></fieldValues></filter>", ts.TestSuiteRunID));
            xmlFile.Append(String.Format("<filter><fieldName>TR_STATUS</fieldName><operator>=</operator><fieldValues><value>{0}</value></fieldValues></filter>", ts.Status));
            xmlFile.Append("</filterList>");
            xmlFile.Append("<pageNumber>1</pageNumber>");
            xmlFile.Append("<pageSize>-1</pageSize>");
            xmlFile.Append("<fetchConfig>false</fetchConfig>");
            xmlFile.Append("<fetchUsers>false</fetchUsers>");
            xmlFile.Append("<fetchBugInfo>false</fetchBugInfo>");
            xmlFile.Append("<fetchFileInfo>false</fetchFileInfo>");
            xmlFile.Append("<fetchStepInfo>false</fetchStepInfo>");
            xmlFile.Append("<CountOnly>true</CountOnly>");
            xmlFile.Append("</searchCriteria></InputXML>");

            // Read test Input Data
            //TextReader tr = new StreamReader(xmlFile.ToString());
            //string xmlin = tr.ReadToEnd();
            //tr.Close();

            //Load Test Input Data into XML Doc

            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(xmlFile.ToString());

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            XmlNode inputxml = doc.GetElementsByTagName("InputXML")[0];

            //XmlNode entityType = doc.GetElementsByTagName("EntityType")[0];
            //Int32.TryParse(entityType.InnerText, out entitytype);

            TestStudioService.TestStudio service = new TestStudioService.TestStudioClient();

            TestStudioService.exportData expdObj = new TestStudioService.exportData();
            expdObj.strData = inputxml.InnerXml;
            expdObj.ielementType = 2;
            expdObj.SessionID = ts.SessionID;

            TestStudioService.exportDataRequest request = new TestStudioService.exportDataRequest(expdObj);
            TestStudioService.exportDataResponse1 response = null;

            try
            {
                response = service.exportData(request);
                doc.LoadXml(response.exportDataResponse.@return.ToString());
                ts.TestCasesCount = Convert.ToInt32((doc.GetElementsByTagName("testrunlist")[0]).Attributes[0].Value);
            }
            catch (Exception ex)
            {
                ts.ErrorCode = "-1";
                ts.ErrorMessage = ex.Message;
            }

            return ts;
        }

        #endregion
    }
}
