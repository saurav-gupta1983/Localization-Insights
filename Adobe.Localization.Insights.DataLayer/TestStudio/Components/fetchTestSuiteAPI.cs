////////////////////////////////////////////////////////////////////////////////////////////////////
// 
// Description: TS Automation Studio - A 100% Data Driven Framework for Smoke Testing, Code Coverage
//                                     & Performance Testing.
//
// Author: Aman K. Gupta
// ADOBENET\amkumarg
// Revision History (DD/MM/YYYY): 28/06/2010 
//
////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TS_AutomationStudio.TSWSRef_PROD_Environment;
using TS_AutomationStudio.Resources;
using System.IO;
using System.Xml;
using TS_AutomationStudio.Test_Cases_PROD_Environment;
using TS_AutomationStudio.LogManager;

namespace TS_AutomationStudio.Test_Cases_PROD_Environment
{
    class fetchTestSuiteAPI
    {
        #region Variables

        string SessionID = string.Empty;
        DateTime init, stop;
        TimeSpan difference;

        static string WebServiceAPI = APICollection.fetchTestSuite.ToString();
        static string TestCaseID = string.Empty, ScenarioDescription = string.Empty, Result = string.Empty, Remarks = string.Empty, WebService_Response_Dump = string.Empty;
        static Logger loggerObj;
        static DateTime API_Invoke_Time, API_Exit_Time;
        static TimeSpan API_Execution_Time_ms;
        string[] PerformanceData = null;
        int indexPerformanceData = 0;

        #endregion

        #region fetchTestSuite


        public void fetchTestSuite(Logger loggerObj, string[] OptionalParams)
        {
            Console.WriteLine("\n" + DateTime.Now + " : " + "|> fetchTestSuite");

            fetchTestSuiteAPI.loggerObj = loggerObj;

            string[] filePaths = null;

            try
            {
                if (StudioHome.isPerformanceSuiteRunning)
                {
                    string userNo = OptionalParams[2] + @"\";
                    filePaths = Directory.GetFiles(TS_AutomationStudio.Resources.fetchTestSuite.InputFilesLocation + userNo, "*.xml");
                    PerformanceData = new string[filePaths.Length];
                    Console.WriteLine("\nUser = " + OptionalParams[2]);


                }
                else
                {
                    filePaths = Directory.GetFiles(TS_AutomationStudio.Resources.fetchTestSuite.InputFilesLocation, "*.xml");

                }

            }
            catch (System.IO.DirectoryNotFoundException)
            {
                Console.WriteLine("\nSEVERE ERROR : Input Files Location : " + TS_AutomationStudio.Resources.fetchTestSuite.InputFilesLocation + " does not exist !");
                return;
            }


            loggerObj.addRedContent1(WebServiceAPI);

            foreach (string  file in filePaths)
            {
                #region Execute

                #region Read ScriptData

                //Variables
                string UserName = string.Empty;
                string Password = string.Empty;
                string ExpectedResult = string.Empty;
                string sessionID = string.Empty;
                bool CountOnly = false; 
                               
                // Read test Input Data
                TextReader tr = new StreamReader(file);
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
                    Console.WriteLine("\nSEVERE ERROR : Input Script - '" + file + "' is Not a Valid XML File!");
                    continue;
                }

                //Read ScriptData Tags

                XmlNode testcaseID = doc.GetElementsByTagName("TestCaseID")[0];
                TestCaseID = testcaseID.InnerText;

                XmlNode testcaseScenario = doc.GetElementsByTagName("TestCaseScenario")[0];
                ScenarioDescription = testcaseScenario.InnerText;

                XmlNode username = doc.GetElementsByTagName("username")[0];
                UserName = username.InnerText;

                XmlNode password = doc.GetElementsByTagName("password")[0];
                Password = password.InnerText;

                XmlNode sessionid = doc.GetElementsByTagName("sessionID")[0];
                sessionID = sessionid.InnerText;

                XmlNode expectedResult = doc.GetElementsByTagName("ExpectedResult")[0];
                ExpectedResult = expectedResult.InnerText;

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
                     SearchCriterion sc = new SearchCriterion();

                     TS_AutomationStudio.TSWSRef_PROD_Environment.fetchTestSuite logObj = new TS_AutomationStudio.TSWSRef_PROD_Environment.fetchTestSuite();

                     //for every <SearchObject>
                     for (int CCount = 0; CCount < SearchObjectChildren.Count; CCount++)
                     {
                         //match the name of first (CCount = 0) child of <SearchObject>
                         switch (SearchObjectChildren[CCount].Name)
                         {
                             case "CountOnly":
                                 bool.TryParse(SearchObjectChildren[CCount].InnerText, out CountOnly);
                                 break;
                             case "filterList":
                                 //gives all <filter> nodes
                                 XmlNodeList Filters = SearchObjectChildren[CCount].ChildNodes;
                                 int filterCount = Filters.Count;
                                 Filter[] filterList = new Filter[filterCount];

                                 int f = 0; // index of filterList

                                 //pick up each <filter> node
                                 foreach (XmlNode filterNode in Filters)
                                 {
                                     Filter filterObj = new Filter();

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

                                 sc.filterList = filterList;

                                 break;
                             case "filterRelationship":
                                 sc.filterRelationship = SearchObjectChildren[CCount].InnerText;
                                 break;
                             default:
                                 break;
                         }

                        
                         
                     }

                        logObj.sc = sc;
                     
                     //sclist[i] = sc;
                     SearchObject = SearchObject.NextSibling;
                     i++;

                //} while (i < count && SearchObject!=null);


                #endregion

                //Initiate Login

                Console.WriteLine("\n" + DateTime.Now + " : " + "START: " + TestCaseID + "\n File: " + file);

                TestStudio port = null;

                SessionID = string.Empty;

                if (sessionID.Equals(string.Empty))
                {
                    SessionID = TS_AutomationStudio_PROD_Environment.Common.LoginUser(UserName, Password, out port);


                }
                else
                {
                    port = new TestStudioClient();
                    SessionID = sessionID;
                }

                //TS_AutomationStudio.TSWSRef_PROD_Environment.deleteData logObj = new TS_AutomationStudio.TSWSRef_PROD_Environment.deleteData();
                logObj.SessionID = SessionID;


                fetchTestSuiteRequest lr = new fetchTestSuiteRequest(logObj);


                init = StopWatch.Start();
                API_Invoke_Time = init;


                fetchTestSuiteResponse1 lres = null;

                try
                {
                    lres = port.fetchTestSuite(lr);
                }
                catch (Exception ex)
                {
                    Result = "Fail";
                    Remarks = "Webservice Exception : " + ex.Message + " Stack Trace : " + ex.StackTrace;
                    stop = StopWatch.Stop();
                    API_Exit_Time = stop;
                    difference = stop - init;
                    API_Execution_Time_ms = difference;

                    goto skipvalidation;

                }

                stop = StopWatch.Stop();
                API_Exit_Time = stop;
                difference = stop - init;
                API_Execution_Time_ms = difference;

                if (StudioHome.isPerformanceSuiteRunning)
                {
                    PerformanceData[indexPerformanceData] = API_Execution_Time_ms.Seconds.ToString();
                    indexPerformanceData++;

                }

                #region Webservice Response Validation

                StringBuilder parsedOutput = new StringBuilder();
                TSPagedObject returnedScList = lres.fetchTestSuiteResponse.@return;
                if (returnedScList!=null)
                {
                    TSInfo[] TsInfoList = returnedScList.list;

                    if (TsInfoList != null)
                    {
                        int recordCount = 1;
                        for (int tsCount = 0; tsCount < TsInfoList.Length; tsCount++)
                        {
                            StringBuilder owners = new StringBuilder();
                            IDNameObject[] nameArr = new IDNameObject[TsInfoList[tsCount].owners.Length];
                            nameArr = TsInfoList[tsCount].owners;
                            foreach (IDNameObject name in nameArr)
                            {
                                owners.Append(name.name + " , ");
                            }
                            parsedOutput.AppendLine("<Record" + recordCount + ">" + "Created By : " + TsInfoList[tsCount].createdBy.name + " , Creation Date : " + TsInfoList[tsCount].creationDate + " , ID : " + TsInfoList[tsCount].id + " , Title : " + TsInfoList[tsCount].title + " , Owners : " + owners + " , IsDeleted : " + TsInfoList[tsCount].deleted + " , External ID : " + TsInfoList[tsCount].externalID + @"</Record" + recordCount + ">");
                            recordCount++;
                        }

                    }
                }
                string returnedScListToXML = @"<TSInfoList>" + parsedOutput.ToString() + "</TSInfoList>";
                WebService_Response_Dump = returnedScListToXML;
                XmlDocument webresDoc = new XmlDocument();
                webresDoc.LoadXml(WebService_Response_Dump);

                XmlNode errorCode_ResultList = webresDoc.SelectSingleNode("/resultlist/result/error/errorCode");
                XmlNode errorMsg_ResultList = webresDoc.SelectSingleNode("/resultlist/result/error/errorMessage");
                XmlNode errorCode__ERROR = webresDoc.SelectSingleNode("/error/errorCode");
                XmlNode errorMsg_ERROR = webresDoc.SelectSingleNode("/error/errorMessage");

                if (errorCode_ResultList != null)
                {
                    int eCode = Int32.Parse(errorCode_ResultList.InnerText);
                    if (eCode == 0)
                    {
                        Result = "Pass";
                        Remarks = "NA";

                    }
                    else
                    {
                        Result = "Fail";
                        if (errorMsg_ResultList != null)
                        {
                            Remarks = errorMsg_ResultList.InnerText;
                        }
                        else
                        {
                            Remarks = "";
                        }


                    }
                }
                else if (errorCode__ERROR != null)
                {
                    int eCode = Int32.Parse(errorCode__ERROR.InnerText);
                    if (eCode == 0)
                    {
                        Result = "Pass";
                        Remarks = "NA";

                    }
                    else
                    {
                        Result = "Fail";
                        if (errorMsg_ERROR != null)
                        {
                            Remarks = errorMsg_ERROR.InnerText;
                        }
                        else
                        {
                            Remarks = "";
                        }


                    }
                }
                else
                {
                    Result = "Pass";
                    Remarks = "NA";
                }



                #endregion

            skipvalidation:

                loggerObj.addGreenContent(TestCaseID, ScenarioDescription + " |>File : " + file, API_Invoke_Time, API_Exit_Time, API_Execution_Time_ms, Result, ExpectedResult, Remarks, WebService_Response_Dump);

                Console.WriteLine("\n" + DateTime.Now + " : " + "END: " + TestCaseID);

                #endregion

            }

            if (StudioHome.isPerformanceSuiteRunning)
            {
                StringBuilder TimeData = new StringBuilder();
                foreach (string Record in PerformanceData)
                {
                    TimeData.Append("<" + WebServiceAPI + ">");
                    TimeData.Append(Record);
                    TimeData.Append("</" + WebServiceAPI + ">");
                    Common.UpdateTimeStringBuilder(TimeData.ToString());

                }
            }

            //loggerObj.addRedContent2();
        }

        #endregion

       
    }
}
