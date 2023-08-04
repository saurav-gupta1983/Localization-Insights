using System;
using System.Data;
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
    /// FetchTestSuiteRunDetailsExAPI
    /// </summary>
    class FetchTestSuiteRunDetailsExAPI
    {
        #region Public Members

        /// <summary>
        /// FetchTestSuiteRunDetailsEx
        /// </summary>
        /// <param name="sessionID"></param>
        /// <param name="suiteID"></param>
        public DTO.TestSudio FetchTestSuiteRunDetailsEx(DTO.TestSudio ts)
        {
            TestStudioService.TestStudio service = new TestStudioService.TestStudioClient();

            TestStudioService.fetchTestSuiteDetailsEx logObj = new TestStudioService.fetchTestSuiteDetailsEx();
            logObj.ts_id = Convert.ToInt32(ts.TestSuiteID.Replace("TS_", ""));
            logObj.fetchChildTC = true;
            logObj.fetchChildTS = false;
            logObj.fetchTCConfig = true;
            logObj.pageNumber = 1;
            logObj.pageSize = ts.TestCasesCount + 10;
            logObj.SessionID = ts.SessionID;

            TestStudioService.fetchTestSuiteDetailsExRequest request = new TestStudioService.fetchTestSuiteDetailsExRequest(logObj);
            TestStudioService.fetchTestSuiteDetailsExResponse1 response = null;

            try
            {
                response = service.fetchTestSuiteDetailsEx(request);

                DataTable dtTestCaseDetails = new DataTable();
                dtTestCaseDetails.Columns.Add(new DataColumn(WebConstants.COL_TEST_CASES_ID, Type.GetType(WebConstants.STR_SYSTEM_INT32)));
                dtTestCaseDetails.Columns.Add(new DataColumn(WebConstants.COL_TEST_CASES_PRODUCT_AREA, Type.GetType(WebConstants.STR_SYSTEM_STRING)));
                dtTestCaseDetails.Columns.Add(new DataColumn(WebConstants.COL_TEST_CASES_SUB_AREA, Type.GetType(WebConstants.STR_SYSTEM_STRING)));
                dtTestCaseDetails.Columns.Add(new DataColumn(WebConstants.COL_TEST_CASES_PRIORITY, Type.GetType(WebConstants.STR_SYSTEM_STRING)));
                dtTestCaseDetails.Columns.Add(new DataColumn(WebConstants.COL_TEST_CASES_VERSION, Type.GetType(WebConstants.STR_SYSTEM_STRING)));

                foreach (TestStudioService.TSEntity data in response.fetchTestSuiteDetailsExResponse.@return.list)
                {
                    if (data.tc != null)
                    {
                        DataRow drTCDetails = dtTestCaseDetails.NewRow();
                        drTCDetails[0] = data.tc.id;
                        drTCDetails[3] = data.tc.priority.id;

                        if (data.tc.product.Length > 0)
                        {
                            foreach (TestStudioService.TCVersion version in data.tc.product[0].versionList)
                            {
                                drTCDetails[4] = version.name;
                                if (version.isActive && data.tc.product[0].versionList[0].subArea != null)
                                {
                                    if (data.tc.product[0].versionList[0].subArea.Length > 1)
                                    {
                                        drTCDetails[1] = data.tc.product[0].versionList[0].subArea[0].name;
                                        drTCDetails[2] = data.tc.product[0].versionList[0].subArea[1].name;
                                    }
                                    else if (data.tc.product[0].versionList[0].subArea.Length == 1)
                                        drTCDetails[1] = data.tc.product[0].versionList[0].subArea[0].name;
                                }
                            }
                        }
                        dtTestCaseDetails.Rows.Add(drTCDetails);
                    }
                }

                DataView dvTestCases = dtTestCaseDetails.DefaultView;
                string sortingCriteria;

                sortingCriteria = String.Format("{0} ASC, {1} ASC, {2} DESC", WebConstants.COL_TEST_CASES_PRODUCT_AREA, WebConstants.COL_TEST_CASES_SUB_AREA, WebConstants.COL_TEST_CASES_PRIORITY);
                if (ts.ProductAreaDistributionCriteria && !ts.PriorityDistributionCriteria)
                    sortingCriteria = String.Format("{0} ASC, {1} ASC", WebConstants.COL_TEST_CASES_PRODUCT_AREA, WebConstants.COL_TEST_CASES_SUB_AREA);
                else if (!ts.ProductAreaDistributionCriteria && ts.PriorityDistributionCriteria)
                    sortingCriteria = String.Format("{0} DESC", WebConstants.COL_TEST_CASES_PRIORITY);

                dvTestCases.Sort = sortingCriteria;


                ts.TestSuiteDetails = dvTestCases.ToTable();
                ts.TestCasesCount = dtTestCaseDetails.Rows.Count;
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
