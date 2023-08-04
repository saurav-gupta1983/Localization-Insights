using System;
using System.Collections.Generic;
using System.Data;
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
    class ImportDataAPI
    {
        #region Variables

        private const string xmlTRInput = "<testrunlist count=\"CDATA[count]\" createNewSID=\"CDATA[createNewSID]\" assignToPrimaryOwner=\"false\" NoCopyTCOwner=\"true\" copyStepsInfoFromTC=\"true\" copyFileInfoFromTC=\"true\" copyAutomationDataFromTC=\"false\" overrideTCConfig=\"true\" suiteRunDisplayName=\"CDATA[SuiteRun]\"><executioninfo><exceptionlist><exception><attributes></attributes></exception></exceptionlist><tcidlist>CDATA[TCLIST]</tcidlist><productcombinationattributes></productcombinationattributes><globalattributes><attribute><key>PLATFORM</key><type>2</type><id>-1</id><valuelist><attrvalue><value>CDATA[PLATFORM]</value><id>-1</id><externalid>-1</externalid></attrvalue></valuelist><externalid/></attribute><attribute><key>LANGUAGE</key><type>2</type><id>-1</id><valuelist><attrvalue><value>CDATA[LANGUAGE]</value><id>-1</id><externalid>-1</externalid></attrvalue></valuelist><externalid/></attribute></globalattributes><product><idnameobject><name>CDATA[PRODUCT]</name><id>-1</id></idnameobject></product><productversion><idnameobject><name>CDATA[VERSION]</name></idnameobject></productversion><buildnbr>CDATA[BuildNo]</buildnbr><milestone /><buglist /><creationdate>1320767099000</creationdate><modificationdate>1320767099000</modificationdate><createdby><idnameobject><name>CDATA[User]</name><id>-1</id></idnameobject></createdby><modifiedby><idnameobject><name>CDATA[User]</name><id>-1</id></idnameobject></modifiedby><subarealist /><assignedto><idnameobject><name>CDATA[User]</name><id>-1</id></idnameobject></assignedto><status><idnameobject><name>Not Executed</name><id>-1</id></idnameobject></status></executioninfo></testrunlist>";

        #endregion

        #region Public Members

        /// <summary>
        /// ImportTR
        /// </summary>
        /// <param name="loggerObj"></param>
        /// <param name="OptionalParams"></param>
        public DTO.TestSudio ImportTR(DTO.TestSudio ts)
        {
            bool firstLoop = true;

            foreach (DataRow drVersion in ts.VersionList.Rows)
            {
                string inputXml = xmlTRInput;

                DataRow[] drTestCasesCol = ts.TestSuiteDetails.Select(WebConstants.COL_TEST_CASES_VERSION + "='" + drVersion[0].ToString() + "'");

                inputXml = inputXml.Replace("CDATA[count]", drTestCasesCol.Length.ToString());

                if (firstLoop)
                {
                    inputXml = inputXml.Replace("CDATA[createNewSID]", "true");
                    inputXml = inputXml.Replace("CDATA[SuiteRun]", "");
                }
                else
                {
                    inputXml = inputXml.Replace("CDATA[createNewSID]", "false");
                    inputXml = inputXml.Replace("CDATA[SuiteRun]", ts.TestSuiteRunID);
                }

                StringBuilder testCasesList = new StringBuilder();
                foreach (DataRow drTestCase in drTestCasesCol)
                    testCasesList.AppendFormat("<tcid>{0}</tcid>", drTestCase[0].ToString());
                inputXml = inputXml.Replace("CDATA[TCLIST]", testCasesList.ToString());

                inputXml = inputXml.Replace("CDATA[BuildNo]", ts.BuildNo);
                inputXml = inputXml.Replace("CDATA[PLATFORM]", ts.Platform);
                inputXml = inputXml.Replace("CDATA[LANGUAGE]", ts.Locale);
                inputXml = inputXml.Replace("CDATA[PRODUCT]", ts.Product);
                inputXml = inputXml.Replace("CDATA[VERSION]", drVersion[0].ToString());
                inputXml = inputXml.Replace("CDATA[User]", ts.DataHeader.LoginID.ToLower());

                TestStudioService.TestStudio service = new TestStudioService.TestStudioClient();
                TestStudioService.importData impdObj = new TestStudioService.importData();

                impdObj.SessionID = ts.SessionID;
                impdObj.strData = inputXml;
                impdObj.ielementType = 2;
                impdObj.iAction = 13;

                TestStudioService.importDataRequest request = new TestStudioService.importDataRequest();
                request.importData = impdObj;
                TestStudioService.importDataResponse1 response = null;

                try
                {
                    response = service.importData(request);

                    if (firstLoop)
                    {
                        XmlDocument docResponse = new XmlDocument();
                        docResponse.LoadXml(response.importDataResponse.@return);

                        ts.TestSuiteRunID = docResponse.GetElementsByTagName("suiteRunID")[0].InnerText;
                        firstLoop = false;
                    }

                }
                catch (Exception ex)
                {
                    ts.ErrorCode = "-1";
                    ts.ErrorMessage = ex.Message;
                    break;
                }
            }

            return ts;
        }

        #endregion


    }
}
