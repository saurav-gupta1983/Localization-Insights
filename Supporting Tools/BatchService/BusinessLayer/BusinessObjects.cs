using System;
using System.Xml;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections.Generic;
using COM = Adobe.OogwayBatch.Common;
using CONST = Adobe.OogwayBatch.Common.Constants;
using DTO = Adobe.OogwayBatch.Common.DataTransferObjects;
using DAO = Adobe.OogwayBatch.DataLayer.DataAccessObjects;
using watson = Adobe.OogwayBatch.DataLayer.Watson;
using Adobe.OogwayBatch.Common;

namespace Adobe.OogwayBatch.BusinessLayer
{
    /// <summary>
    /// BusinessObjects
    /// </summary>
    public class BusinessObjects
    {
        #region Variables

        //Input XML... Specifies which all products and versions information would be fetch from watson
        private static string requestXmlUrlPath = System.Configuration.ConfigurationManager.AppSettings[CONST.LOCATION_REQUEST_XML] + "RequestXML.xml";
        string[] bugsType = new string[] { CONST.DATA_TYPE_ALL, CONST.DATA_TYPE_CORE, CONST.DATA_TYPE_LOC };

        #endregion

        #region Bugs

        /// <summary>
        /// GetBugsData - Get Bugs data for all products from Watson
        /// </summary>
        public void GetBugsData(DTO.Response response)
        {
            response.Products = new List<DTO.Product>();
            response.Status = new List<string>();
            response.ExecutionTime = new List<string>();

            DTO.Request request = COM.Common.GetRequestDetails(requestXmlUrlPath);

            foreach (DTO.Product product in request.Products)  //for each product call watson to fetch bug count
            {

                response.Products.Add(product);
                response.IsSuccess = true;
                string errorMessage = "";

                ExceptionLogger.LogData(String.Format("Processing Product:{0}, Version:{1}", product.Name, COM.Common.GetStringFromList(product.Versions)));

                try
                {
                    foreach (string type in bugsType)
                    {
                        ExceptionLogger.LogData(String.Format("Processing Product:{0}, Version:{1} and Type:{2}", product.Name, COM.Common.GetStringFromList(product.Versions), type));

                        XmlDocument responseXML = new XmlDocument();

                        watson.Operations watsonQuery = new watson.Operations();
                        responseXML = watsonQuery.GetDataFromWatson(watsonQuery.GenerateWatsonRequestXML(product, request.Keywords, type));

                        XmlNodeList groupList = responseXML.GetElementsByTagName("group");

                        if (groupList.Count != 0)  //to handle the case when bug count is 0
                            if (!DAO.DAObjects.DumpDataToDatabase(responseXML, type))
                            {
                                response.IsSuccess = false;
                                errorMessage = String.Format(CONST.ERROR_DATABASE_DUMP_FAILED, product.Name, product.Versions, type);
                            }

                    }

                    DataSet dsResultSet = DAO.DAObjects.ProcessDumpData(response.ExecutionStartTime);
                    if (dsResultSet == null || dsResultSet.Tables.Count == 0)
                    {
                        response.IsSuccess = false;
                        errorMessage = String.Format(CONST.ERROR_WATSON_DATA_PROCESSING_FAILED, product.Name, product.Versions);
                        ExceptionLogger.LogData(errorMessage);
                    }

                    ExceptionLogger.LogData("Processing Completed. \n");
                }
                catch (Exception exception)
                {
                    response.IsSuccess = false;
                    errorMessage = String.Format(CONST.ERROR_PROCESSING_FAILED, product.Name, product.Versions);
                    ExceptionLogger.Log(exception, errorMessage);
                }

                response.ExecutionTime.Add(DateTime.Now.ToString());
                response.ExecutionTime.Add(errorMessage);

                if (response.IsSuccess)
                    response.Status.Add(CONST.STATUS_SUCCESS);
                else
                    response.Status.Add(CONST.STATUS_FAILED);
            }
        }

        #endregion

        #region GenerateXMLs

        /// <summary>
        /// GenerateGraphXMLsForPlotting 
        /// </summary>
        public void GenerateGraphXMLsForPlotting(DTO.Response response)
        {
            response.IsSuccess = true;
            response.Message = "";
            try
            {
                Components.GenerateGraphs graphs = new Components.GenerateGraphs(response);

                foreach (string type in bugsType)
                    graphs.GeneratePlottingGraphXML(type);
            }
            catch (Exception exception)
            {
                response.IsSuccess = false;
                response.Message = CONST.ERROR_GRAPH_GENERATION_FAILED;
                ExceptionLogger.Log(exception, response.Message);
            }
        }

        #endregion

        #region Mail Notifications

        /// <summary>
        /// SendMailNotification
        /// </summary>
        /// <param name="response"></param>
        public void SendMailNotification(DTO.Response response)
        {
            try
            {
                EmailNotifications emailNotify = new EmailNotifications("", "");

                string subject = "{0}: Oogway Batch Process for Watson Data";
                string success = CONST.STATUS_SUCCESS;
                StringBuilder message = new StringBuilder();

                message.AppendFormat("\nProcess Started : {0}\n", response.ExecutionStartTime);
                message.Append("\nProduct\tVersion\tStatus\tErrors");

                for (int listCount = 0; listCount < response.Products.Count; listCount++)
                {
                    if (response.Status[listCount] == CONST.STATUS_SUCCESS)
                        message.AppendFormat("\n{0}\t{1}\t{2}\t{3}", response.Products[listCount].Name, COM.Common.GetStringFromList(response.Products[listCount].Versions), response.Status[listCount], "None");
                    else
                    {
                        success = CONST.STATUS_FAILED;
                        message.AppendFormat("\n{0}\t{1}\t{2}\t{3}", response.Products[listCount].Name, COM.Common.GetStringFromList(response.Products[listCount].Versions), response.Status[listCount], response.Message[listCount]);
                    }
                }

                if (!response.IsSuccess)
                {
                    success = CONST.STATUS_FAILED;
                    message.AppendFormat("\n{0}\n", response.Message);
                }

                message.AppendFormat("\n\nProcess Completed : {0}\n", response.ExecutionEndTime);
                emailNotify.SendEmail(String.Format(subject, success), message.ToString());

            }
            catch (Exception exception)
            {
                ExceptionLogger.Log(exception, "Mail Notification failed");
            }
        }

        #endregion
    }
}
