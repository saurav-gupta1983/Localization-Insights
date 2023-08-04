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
    /// FetchSuiteRunDetailsAPI
    /// </summary>
    class FetchSuiteRunDetailsAPI
    {
        #region Public Members

        /// <summary>
        /// FetchSuiteRunDetails
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public DTO.TestSudio FetchSuiteRunDetails(DTO.TestSudio ts)
        {
            TestStudioService.TestStudio service = new TestStudioService.TestStudioClient();

            TestStudioService.fetchSuiteRunDetails logObj = new TestStudioService.fetchSuiteRunDetails();
            logObj.tsr_id = Convert.ToInt32(ts.TestSuiteRunID.Replace("SID_", ""));
            logObj.treeDepth = 1;
            logObj.SessionID = ts.SessionID;
            logObj.fetchConfig = true;

            TestStudioService.fetchSuiteRunDetailsRequest request = new TestStudioService.fetchSuiteRunDetailsRequest(logObj);
            TestStudioService.fetchSuiteRunDetailsResponse1 response = null;

            try
            {
                response = service.fetchSuiteRunDetails(request);
                ts.TestSuiteID = response.ToString();
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
