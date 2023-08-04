using System;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using Adobe.Localization.Insights.Common;
using DTO = Adobe.Localization.Insights.Common.DataTransferObjects;

namespace Adobe.Localization.Insights.DataLayer.TestStudioComponents
{
    /// <summary>
    /// CreateTestSuiteAPI
    /// </summary>
    class CreateTestSuiteAPI
    {
        #region Public Members

        /// <summary>
        /// CreateTestSuite
        /// </summary>
        /// <param name="loggerObj"></param>
        /// <param name="OptionalParams"></param>
        public DTO.TestSudio CreateTestSuite(DTO.TestSudio ts)
        {
            TestStudioService.TSInfo[] sclist = new TestStudioService.TSInfo[1];
            TestStudioService.TSInfo sc = new TestStudioService.TSInfo();
            TestStudioService.TSEntity[] tsEntity = new TestStudioService.TSEntity[ts.TestCasesCollection.Length];
            sc.title = ts.TestSuiteTitle;
            sc.desc = ts.TestSuiteTitle;

            for (int counter = 0; counter < ts.TestCasesCollection.Length; counter++)
            {
                TestStudioService.TSEntity tsE = new TestStudioService.TSEntity();
                TestStudioService.TempTCInfo tcInfo = new TestStudioService.TempTCInfo();
                tcInfo.tcID = ts.TestCasesCollection[counter][WebConstants.COL_TEST_CASES_ID].ToString();
                tcInfo.id = Convert.ToInt32(ts.TestCasesCollection[counter][WebConstants.COL_TEST_CASES_ID].ToString());
                tsE.tc = tcInfo;
                tsE.type = 1; //for testcases
                tsEntity[counter] = tsE;
            }
            sc.tsEntity = tsEntity;
            sclist[0] = sc;

            TestStudioService.TestStudio service = new TestStudioService.TestStudioClient();

            TestStudioService.createTestSuite logObj = new TestStudioService.createTestSuite();
            logObj.SessionID = ts.SessionID;
            logObj.tsList = sclist;
            logObj.allowDuplicateInSuites = true;

            TestStudioService.createTestSuiteRequest request = new TestStudioService.createTestSuiteRequest(logObj);
            TestStudioService.createTestSuiteResponse response = null;

            try
            {
                response = service.createTestSuite(request);
                ts.TestSuiteID = response.createTestSuiteResponse1[0].ts_displayName;
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
