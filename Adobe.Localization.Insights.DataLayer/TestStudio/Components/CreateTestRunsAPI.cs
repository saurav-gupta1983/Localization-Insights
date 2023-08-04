using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Data;
using Adobe.Localization.Insights.Common;
using DTO = Adobe.Localization.Insights.Common.DataTransferObjects;

namespace Adobe.Localization.Insights.DataLayer.TestStudioComponents
{
    /// <summary>
    /// CreateTestRunsAPI
    /// </summary>
    class CreateTestRunsAPI
    {
        #region Variables

        private const int CONST_ZERO = 0;

        private const string CONST_STATUS_NOT_EXECUTED = "Not Executed";
        private const string CONST_TEST_SUITE_NAME = "Suite: {0}";
        private const string CONST_SUITE_RUN_NAME = "SID created for suite: {0}";
        private const string CONST_KEY_PLATFORM = "PLATFORM";
        private const string CONST_KEY_LANGUAGE = "LANGUAGE";
        private const string CONST_MILESTONE_DEFAULT = "MileStone";
        private const string CONST_BUILD_NO_DEFAULT = "BuildNo";

        #endregion

        #region Public Menbers

        /// <summary>
        /// CreateTestRuns
        /// </summary>
        /// <param name="ts"></param>
        public DTO.TestSudio CreateTestRuns(DTO.TestSudio ts)
        {
            #region Commented Code
            /*
            #region Read ScriptData

            #region Variables

            #region <ScriptData> variables

            string UserName = string.Empty;
            string Password = string.Empty;
            string ExpectedResult = string.Empty;
            string sessionID = string.Empty;

            #endregion

            #region <SearchObject> Variables

            string AssignedTo = string.Empty;
            bool AssignToPrimaryOwner = false;
            bool CopyAutomationDataFromTC = false;
            bool CopyExecutionModeFromTC = false;
            bool CopyFileInfoFromTC = false;
            bool CopyOwnerInfoFromParent = false;
            bool CopyStepInfoFromTC = false;
            bool NoCopyTCOwner = false;
            string Status = string.Empty;
            bool isStaticSuite = false;
            bool saveConfig = false;
            string SuiteName = string.Empty;
            string SuiteRunID = string.Empty;
            string TestRunName = string.Empty;
            bool UseSuiteRunID = false;
            bool UseSearchCriterion = false;
            #endregion

            #endregion

            // Read test Input Data
            TextReader tr = new StreamReader(@"C:\Saurav\Projects\Adobe-IQE\TestStudio\createTestRuns\SampleCreateTestRuns.xml");
            string xmlin = tr.ReadToEnd();
            tr.Close();

            //Load Test Input Data into XML Doc

            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(xmlin);

            }
            catch (Exception)
            {
                //Console.WriteLine("\nSEVERE ERROR : Input Script - '" + file + "' is Not a Valid XML File!");
                ////continue;
            }

            //Read ScriptData Tags

            //XmlNode testcaseID = doc.GetElementsByTagName("TestCaseID")[0];
            //string TestCaseID = testcaseID.InnerText;

            //XmlNode testcaseScenario = doc.GetElementsByTagName("TestCaseScenario")[0];
            //string ScenarioDescription = testcaseScenario.InnerText;

            //XmlNode username = doc.GetElementsByTagName("username")[0];
            //UserName = username.InnerText;

            //XmlNode password = doc.GetElementsByTagName("password")[0];
            //Password = password.InnerText;

            //XmlNode sessionid = doc.GetElementsByTagName("sessionID")[0];
            //sessionID = sessionid.InnerText;

            //XmlNode expectedResult = doc.GetElementsByTagName("ExpectedResult")[0];
            //ExpectedResult = expectedResult.InnerText;

            XmlNode inputxml = doc.GetElementsByTagName("InputXML")[0];

            XmlNodeList SearchList = inputxml.SelectNodes("/TestCase/InputXML/SearchList/SearchObject");
            int count = SearchList.Count;

            XmlNode SearchObject = null;
            //SearchCriterion[] sclist = new SearchCriterion[count];
            int i = 0;
            SearchObject = doc.GetElementsByTagName("SearchObject")[0];
            //do
            //{
            //list of <SearchObject>
            XmlNodeList SearchObjectChildren = SearchObject.ChildNodes;


            TestStudioService.createTestRuns logObj_temp = new TestStudioService.createTestRuns();
            TestStudioService.SearchCriterion sc_temp = new TestStudioService.SearchCriterion();
            TestStudioService.ReferenceTR refTR_temp = new TestStudioService.ReferenceTR();
            TestStudioService.selectedConfigs[] selConfigsList_temp = null;

            //for every <SearchObject>
            for (int CCount = 0; CCount < SearchObjectChildren.Count; CCount++)
            {
                //match the name of first (CCount = 0) child of <SearchObject>
                switch (SearchObjectChildren[CCount].Name)
                {
                    case "AssignedTo":
                        refTR_temp.assignedTo = SearchObjectChildren[CCount].InnerText;
                        break;
                    case "AssignToPrimaryOwner":
                        bool.TryParse(SearchObjectChildren[CCount].InnerText, out AssignToPrimaryOwner);
                        refTR_temp.assignToPrimaryOwner = AssignToPrimaryOwner;
                        break;
                    case "CopyAutomationDataFromTC":
                        bool.TryParse(SearchObjectChildren[CCount].InnerText, out CopyAutomationDataFromTC);
                        refTR_temp.copyAutomationDataFromTC = CopyAutomationDataFromTC;
                        break;
                    case "CopyExecutionModeFromTC":
                        bool.TryParse(SearchObjectChildren[CCount].InnerText, out CopyExecutionModeFromTC);
                        refTR_temp.copyExecutionModeFromTC = CopyExecutionModeFromTC;
                        break;
                    case "CopyFileInfoFromTC":
                        bool.TryParse(SearchObjectChildren[CCount].InnerText, out CopyFileInfoFromTC);
                        refTR_temp.copyFileInfoFromTC = CopyFileInfoFromTC;
                        break;
                    case "CopyOwnerInfoFromParent":
                        bool.TryParse(SearchObjectChildren[CCount].InnerText, out CopyOwnerInfoFromParent);
                        refTR_temp.copyOwnerInfoFromParent = CopyOwnerInfoFromParent;
                        break;
                    case "CopyStepInfoFromTC":
                        bool.TryParse(SearchObjectChildren[CCount].InnerText, out CopyStepInfoFromTC);
                        refTR_temp.copyStepsInfoFromTC = CopyStepInfoFromTC;
                        break;
                    case "NoCopyTCOwner":
                        bool.TryParse(SearchObjectChildren[CCount].InnerText, out NoCopyTCOwner);
                        refTR_temp.noCopyTCOwner = NoCopyTCOwner;
                        break;
                    case "isStaticSuite":
                        bool.TryParse(SearchObjectChildren[CCount].InnerText, out isStaticSuite);
                        refTR_temp.staticSuite = isStaticSuite;
                        break;
                    case "eEndDate":
                        long eEndDate = 0;
                        long.TryParse(SearchObjectChildren[CCount].InnerText, out eEndDate);
                        refTR_temp.eenddate = eEndDate;
                        break;
                    case "eStartDate":
                        long eStartDate = 0;
                        long.TryParse(SearchObjectChildren[CCount].InnerText, out eStartDate);
                        refTR_temp.estartdate = eStartDate;
                        break;
                    case "Status":
                        refTR_temp.status = SearchObjectChildren[CCount].InnerText;
                        break;
                    case "SuiteName":
                        refTR_temp.suiteName = SearchObjectChildren[CCount].InnerText;
                        break;
                    case "SuiteRunID":
                        refTR_temp.suiteRunID = SearchObjectChildren[CCount].InnerText;
                        break;
                    case "BugsID":
                        string bugsID_csv = SearchObjectChildren[CCount].InnerText;
                        if (bugsID_csv.Equals(string.Empty))
                        {
                            refTR_temp.bugsid = null;
                            break;
                        }
                        string[] bugsID_arr = bugsID_csv.Split(',');
                        refTR_temp.bugsid = bugsID_arr;
                        break;
                    case "TestRunName":
                        refTR_temp.testrunname = SearchObjectChildren[CCount].InnerText;
                        break;
                    case "Time":
                        int Time = 0;
                        Int32.TryParse(SearchObjectChildren[CCount].InnerText, out Time);
                        refTR_temp.time = Time;
                        break;
                    case "UseSuiteRunID":
                        bool.TryParse(SearchObjectChildren[CCount].InnerText, out UseSuiteRunID);
                        refTR_temp.useSuiteRunID = UseSuiteRunID;
                        break;
                    case "saveConfig":
                        bool.TryParse(SearchObjectChildren[CCount].InnerText, out saveConfig);
                        refTR_temp.saveConfig = saveConfig;
                        break;
                    case "IDListArray":
                        int[] idLIST = null;
                        string idlist_str = SearchObjectChildren[CCount].InnerText;
                        if (idlist_str.Equals(string.Empty))
                        {
                            logObj_temp.idList = null;
                            break;

                        }

                        string[] idlist_csv = idlist_str.Split(',');
                        idLIST = new int[idlist_csv.Length];
                        int idX = 0;
                        foreach (string id in idlist_csv)
                        {
                            idLIST[idX] = Int32.Parse(id.ToString());
                            idX++;
                        }

                        logObj_temp.idList = idLIST;
                        break;
                    case "UseSearchCriterion":
                        bool.TryParse(SearchObjectChildren[CCount].InnerText, out UseSearchCriterion);
                        break;
                    case "SelectedConfigs":
                        XmlNodeList SelectConfigNodesList = SearchObjectChildren[CCount].ChildNodes;
                        int indexOfSelectConfigNodes = 0;
                        selConfigsList_temp = new TestStudioService.selectedConfigs[SelectConfigNodesList.Count];
                        TestStudioService.selectedConfigs selConfigs = null;
                        foreach (XmlNode SELCON in SelectConfigNodesList)
                        {
                            #region <SelectConfig>

                            selConfigs = new TestStudioService.selectedConfigs();
                            XmlNodeList Configs = SELCON.ChildNodes;
                            int configCount = Configs.Count;

                            for (int selConChild = 0; selConChild < configCount; selConChild++)
                            {

                                switch (Configs[selConChild].Name)
                                {
                                    case "Build":
                                        selConfigs.build = Configs[selConChild].InnerText;
                                        break;
                                    case "MileStone":
                                        selConfigs.milestone = Configs[selConChild].InnerText;
                                        break;
                                    case "Product":
                                        TestStudioService.IDNameObject prodObj = new TestStudioService.IDNameObject();
                                        prodObj.name = Configs[selConChild].InnerText;
                                        selConfigs.product = prodObj;
                                        break;
                                    case "ProductVersion":
                                        TestStudioService.IDNameObject prodVer = new TestStudioService.IDNameObject();
                                        prodVer.name = Configs[selConChild].InnerText;
                                        selConfigs.selVersion = prodVer;
                                        break;
                                    case "GlobalAttributes":
                                        // XmlNodeList GlobalAttributesChildren = Configs[selConChild].ChildNodes;

                                        #region Global Attributes

                                        XmlNodeList AttributeNodes = Configs[selConChild].ChildNodes;
                                        int AttributeCount = AttributeNodes.Count;
                                        TestStudioService.Attribute[] AttributesList = new TestStudioService.Attribute[AttributeCount];

                                        int attrIndex = 0; // index of AttributeList

                                        //pick up each <Attribute> node
                                        foreach (XmlNode attributeNode in AttributeNodes)
                                        {
                                            TestStudioService.Attribute attrObj = new TestStudioService.Attribute();

                                            //pickup each childnode of <Attribute>
                                            XmlNodeList AttributeNodeChildren = attributeNode.ChildNodes;
                                            for (int attributeNodeChild = 0; attributeNodeChild < AttributeNodeChildren.Count; attributeNodeChild++)
                                            {

                                                switch (AttributeNodeChildren[attributeNodeChild].Name)
                                                {
                                                    case "externalID":
                                                        attrObj.externalID = AttributeNodeChildren[attributeNodeChild].InnerText;
                                                        break;
                                                    case "ID":
                                                        int Id = 0;
                                                        Int32.TryParse(AttributeNodeChildren[attributeNodeChild].InnerText, out Id);
                                                        attrObj.id = Id;
                                                        break;
                                                    case "Key":
                                                        attrObj.key = AttributeNodeChildren[attributeNodeChild].InnerText;
                                                        break;
                                                    case "Type":
                                                        int type = 0;
                                                        Int32.TryParse(AttributeNodeChildren[attributeNodeChild].InnerText, out type);
                                                        attrObj.type = type;
                                                        break;
                                                    case "AttributeValues":
                                                        XmlNodeList AttributeValuesNodes = AttributeNodeChildren[attributeNodeChild].ChildNodes;
                                                        TestStudioService.AttrValue[] AttributeValuesList = new TestStudioService.AttrValue[AttributeValuesNodes.Count];
                                                        int AttributeValuesListIndex = 0;
                                                        foreach (XmlNode attributeValueNode in AttributeValuesNodes)
                                                        {
                                                            TestStudioService.AttrValue attrValObj = new TestStudioService.AttrValue();
                                                            //pickup each childnode of <AtrValue>
                                                            XmlNodeList AttributeValueNodeChildren = attributeValueNode.ChildNodes;
                                                            for (int attributeValueNodeChild = 0; attributeValueNodeChild < AttributeValueNodeChildren.Count; attributeValueNodeChild++)
                                                            {
                                                                switch (AttributeValueNodeChildren[attributeValueNodeChild].Name)
                                                                {
                                                                    case "externalId":
                                                                        attrValObj.externalID = AttributeValueNodeChildren[attributeValueNodeChild].InnerText;
                                                                        break;
                                                                    case "value":
                                                                        attrValObj.value = AttributeValueNodeChildren[attributeValueNodeChild].InnerText;
                                                                        break;
                                                                    case "id":
                                                                        int id = 0;
                                                                        Int32.TryParse(AttributeValueNodeChildren[attributeValueNodeChild].InnerText, out id);
                                                                        attrObj.id = id;
                                                                        break;
                                                                    case "Selected":
                                                                        bool Selected = false;
                                                                        bool.TryParse(AttributeValueNodeChildren[attributeValueNodeChild].InnerText, out Selected);
                                                                        attrValObj.selected = Selected;
                                                                        break;
                                                                    case "visible":
                                                                        bool visible = false;
                                                                        bool.TryParse(AttributeValueNodeChildren[attributeValueNodeChild].InnerText, out visible);
                                                                        attrValObj.visible = visible;
                                                                        break;
                                                                    default:
                                                                        break;
                                                                }
                                                            }

                                                            AttributeValuesList[AttributeValuesListIndex] = attrValObj;
                                                            AttributeValuesListIndex++;
                                                        }
                                                        attrObj.values = AttributeValuesList;
                                                        break;
                                                    default:
                                                        break;
                                                }
                                            }

                                            AttributesList[attrIndex] = attrObj;
                                            attrIndex++;

                                        }

                                        selConfigs.globalAttrs = AttributesList;


                                        #endregion
                                        break;
                                    default:
                                        break;
                                }
                            }


                            #endregion

                            //Add the selConfigs object to the ConfigList

                            selConfigsList_temp[indexOfSelectConfigNodes] = selConfigs;
                            indexOfSelectConfigNodes++;

                        }
                        break;
                    case "filterList":
                        if (!UseSearchCriterion)
                        {
                            sc_temp.filterList = null;
                            break;
                        }
                        //gives all <filter> nodes
                        XmlNodeList Filters = SearchObjectChildren[CCount].ChildNodes;
                        int filterCount = Filters.Count;
                        TestStudioService.Filter[] filterList = new TestStudioService.Filter[filterCount];

                        int f = 0; // index of filterList

                        //pick up each <filter> node
                        foreach (XmlNode filterNode in Filters)
                        {
                            TestStudioService.Filter filterObj = new TestStudioService.Filter();

                            //pickup each childnode of <filter>
                            XmlNodeList filterNodeChildren = filterNode.ChildNodes;
                            for (int filterNodeChild = 0; filterNodeChild < filterNodeChildren.Count; filterNodeChild++)
                            {

                                switch (filterNodeChildren[filterNodeChild].Name)
                                {
                                    case "fieldName":
                                        filterObj.fieldName = filterNodeChildren[filterNodeChild].InnerText;
                                        break;
                                    case "operator":
                                        filterObj.@operator = filterNodeChildren[filterNodeChild].InnerText;
                                        break;
                                    case "fieldValues":
                                        XmlNodeList fieldValues = filterNodeChildren[filterNodeChild].ChildNodes;
                                        string[] fieldVALUES = new string[fieldValues.Count];
                                        int nFieldVal = 0;
                                        foreach (XmlNode fieldValue in fieldValues)
                                        {
                                            fieldVALUES[nFieldVal] = fieldValue.InnerText;
                                            nFieldVal++;
                                        }
                                        filterObj.fieldValue = fieldVALUES;
                                        break;
                                    default:
                                        break;
                                }
                            }

                            filterList[f] = filterObj;
                            f++;

                        }

                        sc_temp.filterList = filterList;

                        break;
                    case "filterRelationship":
                        if (!UseSearchCriterion)
                        {
                            sc_temp.filterList = null;
                            break;
                        }
                        sc_temp.filterRelationship = SearchObjectChildren[CCount].InnerText;
                        break;
                    default:
                        break;
                }



            }

            if (!UseSearchCriterion)
            {
                logObj_temp.sc = null;

            }
            if (UseSearchCriterion)
            {
                logObj_temp.sc = sc_temp;

            }
            //sclist[i] = sc;
            SearchObject = SearchObject.NextSibling;
            i++;

            //} while (i < count && SearchObject!=null);

            //TestStudioService.SearchCriterion sc = new TestStudioService.SearchCriterion();

            logObj_temp.refTR = refTR_temp;
            logObj_temp.sc = sc_temp;
            logObj_temp.selectedConfigs = selConfigsList_temp;
            logObj_temp.SessionID = ts.SessionID;

            #endregion

             ////FetchAmalgmConfigsAPI configs = new FetchAmalgmConfigsAPI();
            ////ts = configs.FetchAmalgmConfigs(ts);

            TestStudioService.ReferenceTR refTR = new TestStudioService.ReferenceTR();
            TestStudioService.selectedConfigs[] selConfigsList = new TestStudioService.selectedConfigs[1];

            refTR.assignedTo = ts.DataHeader.LoginID.ToLower();
            refTR.assignToPrimaryOwner = true;
            refTR.copyAutomationDataFromTC = false;
            refTR.copyExecutionModeFromTC = false;
            refTR.copyFileInfoFromTC = true;
            refTR.copyOwnerInfoFromParent = false;
            refTR.copyStepsInfoFromTC = true;
            refTR.eenddate = 0;
            refTR.estartdate = 0;
            refTR.time = 0;
            refTR.noCopyTCOwner = false;
            refTR.status = "Passed";

            //refTR.
            refTR.staticSuite = false;
            //refTR.suiteID = ts.TestSuiteID;
            //refTR.suiteID = "TS_43883";
            refTR.suiteName = "ProdSsuite_name";
            refTR.testrunname = refTR.suiteName + " TS_43883";
            refTR.useSuiteRunID = false;
            refTR.saveConfig = false;

            TestStudioService.Attribute[] attributesList = new TestStudioService.Attribute[2];

            attributesList[0] = AttributeConfigurations("PLATFORM", 13, 2, 1212, "MAC 10.8");
            attributesList[1] = AttributeConfigurations("LANGUAGE", 14, 2, 177, "ALL");

            selConfigsList[0] = new TestStudioService.selectedConfigs();
            selConfigsList[0].globalAttrs = attributesList;
            selConfigsList[0].build = "180.0";
            selConfigsList[0].milestone = "milestone";

            TestStudioService.IDNameObject product = new TestStudioService.IDNameObject();
            product.id = 0;
            product.name = "Illustrator";
            selConfigsList[0].product = product;

            TestStudioService.IDNameObject productVersion = new TestStudioService.IDNameObject();
            productVersion.id = 0;
            //productVersion.name = ts.TestSuiteDetails.Rows[0][0].ToString();
            productVersion.name = "16.1";

            selConfigsList[0].selVersion = productVersion;
            //#endregion
            //TestStudioService.TestStudio service = new TestStudioService.TestStudioClient();
            TestStudioService.createTestRuns logObj = new TestStudioService.createTestRuns();

            logObj.SessionID = ts.SessionID;
            logObj.refTR = refTR;
            logObj.selectedConfigs = selConfigsList;
            //logObj.idList = 308923;
            logObj.sc = new TestStudioService.SearchCriterion();
            logObj.sc.filterList = new TestStudioService.Filter[2];
            logObj.sc.filterRelationship = "AND";
            logObj.sc.filterList[0] = TSFilters("TS_DISPLAYNAME", "in", "TS_43883");
            logObj.sc.filterList[1] = TSFilters("SHOW_DELETED_TC", "=", "N");

            //logObj.idList = new int[1];
            //logObj.idList[0] = 308923;

            TestStudioService.TestStudio service = new TestStudioService.TestStudioClient();

            TestStudioService.TestStudio service_temp = new TestStudioService.TestStudioClient();
            TestStudioService.createTestRunsRequest request_temp = new TestStudioService.createTestRunsRequest(logObj_temp);

            */
            #endregion

            bool firstLoop = true;
            foreach (DataRow drVersion in ts.VersionList.Rows)
            {
                TestStudioService.ReferenceTR refTR = new TestStudioService.ReferenceTR();
                TestStudioService.selectedConfigs[] selConfigsList = new TestStudioService.selectedConfigs[1];

                refTR.assignedTo = ts.DataHeader.LoginID.ToLower();
                refTR.assignToPrimaryOwner = true;
                refTR.copyAutomationDataFromTC = false;
                refTR.copyExecutionModeFromTC = false;
                refTR.copyFileInfoFromTC = true;
                refTR.copyOwnerInfoFromParent = false;
                refTR.copyStepsInfoFromTC = true;
                refTR.eenddate = CONST_ZERO;
                refTR.estartdate = CONST_ZERO;
                refTR.time = CONST_ZERO;
                refTR.noCopyTCOwner = false;
                refTR.status = CONST_STATUS_NOT_EXECUTED;

                refTR.staticSuite = false;
                refTR.suiteName = String.Format(CONST_TEST_SUITE_NAME, ts.TestSuiteID);
                refTR.testrunname = String.Format(CONST_SUITE_RUN_NAME, ts.TestSuiteID);
                refTR.useSuiteRunID = true;
                refTR.saveConfig = false;

                if (!firstLoop)
                    refTR.suiteRunID = ts.TestSuiteRunID;

                TestStudioService.Attribute[] attributesList = new TestStudioService.Attribute[2];

                attributesList[0] = AttributeConfigurations(CONST_KEY_PLATFORM, 2, ts.Platform);
                attributesList[1] = AttributeConfigurations(CONST_KEY_LANGUAGE, 2, ts.Locale);

                selConfigsList[0] = new TestStudioService.selectedConfigs();
                selConfigsList[0].globalAttrs = attributesList;

                if (ts.BuildNo != null || ts.BuildNo != "")
                    selConfigsList[0].build = CONST_BUILD_NO_DEFAULT;
                else
                    selConfigsList[0].build = ts.BuildNo;

                if (ts.Milestone != null || ts.Milestone != "")
                    selConfigsList[0].milestone = CONST_MILESTONE_DEFAULT;
                else
                    selConfigsList[0].milestone = ts.Milestone;

                TestStudioService.IDNameObject product = new TestStudioService.IDNameObject();
                product.id = CONST_ZERO;
                product.name = ts.Product;
                selConfigsList[0].product = product;

                TestStudioService.IDNameObject productVersion = new TestStudioService.IDNameObject();
                productVersion.id = CONST_ZERO;
                productVersion.name = drVersion[CONST_ZERO].ToString();

                selConfigsList[0].selVersion = productVersion;

                TestStudioService.createTestRuns logObj = new TestStudioService.createTestRuns();

                logObj.SessionID = ts.SessionID;
                logObj.refTR = refTR;
                logObj.selectedConfigs = selConfigsList;
                logObj.sc = new TestStudioService.SearchCriterion();
                logObj.sc.filterList = new TestStudioService.Filter[2];
                logObj.sc.filterRelationship = "AND";
                logObj.sc.filterList[0] = TSFilters("TS_DISPLAYNAME", "in", ts.TestSuiteID);
                logObj.sc.filterList[1] = TSFilters("SHOW_DELETED_TC", "=", "N");

                TestStudioService.TestStudio service = new TestStudioService.TestStudioClient();
                TestStudioService.createTestRunsRequest request = new TestStudioService.createTestRunsRequest(logObj);
                TestStudioService.createTestRunsResponse1 response = null;

                try
                {
                    response = service.createTestRuns(request);

                    if (firstLoop)
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(response.createTestRunsResponse.@return);

                        ts.TestSuiteRunID = doc.GetElementsByTagName("suiteRunID")[0].InnerText;
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

        /// <summary>
        /// AttributeConfigurations
        /// </summary>
        /// <param name="key"></param>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private TestStudioService.Attribute AttributeConfigurations(string key, int type, string value)
        {
            TestStudioService.Attribute attrObj = new TestStudioService.Attribute();
            TestStudioService.AttrValue[] attributeValuesList = new TestStudioService.AttrValue[1];
            TestStudioService.AttrValue attrValObj = new TestStudioService.AttrValue();

            attrObj.key = key;
            attrObj.externalID = "";
            attrObj.id = 0;
            attrObj.type = type;
            attrObj.visible = false;

            attrValObj.id = 0;
            attrValObj.externalID = "";
            attrValObj.value = value;
            attrValObj.selected = false;
            attrValObj.visible = false;

            attributeValuesList[0] = attrValObj;
            attrObj.values = attributeValuesList;

            return attrObj;
        }

        /// <summary>
        /// TSFilters
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="oper"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private TestStudioService.Filter TSFilters(string fieldName, string oper, string value)
        {
            TestStudioService.Filter filterObj = new TestStudioService.Filter();

            filterObj.fieldName = fieldName;
            filterObj.@operator = oper;
            filterObj.fieldValue = new string[1];
            filterObj.fieldValue[0] = value;

            return filterObj;
        }

        #endregion
    }
}
