using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Configuration;
using System.Net;
using System.IO;
using DTO = Adobe.OogwayBatch.Common.DataTransferObjects;
using CONST = Adobe.OogwayBatch.Common.Constants;
using Adobe.OogwayBatch.Common;

namespace Adobe.OogwayBatch.DataLayer.Watson
{
    /// <summary>
    /// Operations
    /// </summary>
    public class Operations
    {
        #region Variables

        private string watsonServerURL;

        #endregion

        #region Constructor

        /// <summary>
        /// Operations
        /// </summary>
        public Operations()
        {
            watsonServerURL = String.Format("http://{0}/watson/search/bugs/aggregate?appGUID={1}&pagenumber=1&pagesize=5", ConfigurationManager.AppSettings[CONST.WATSON_WEB_SERVER].ToString(), ConfigurationManager.AppSettings[CONST.WATSON_APP_GUID].ToString());
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// GenerateWatsonRequestXML
        /// </summary>
        /// <param name="locKeywords"></param>
        /// <param name="product"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GenerateWatsonRequestXML(DTO.Product product, string[] keywords, string type)
        {
            StringBuilder requestXML = new StringBuilder();

            requestXML.Append("<criteria>");

            #region Include FilterSet

            requestXML.Append("<includeFilterSets><filterSet>");

            requestXML.Append(GetFilterCriteria(CONST.FILTER_CRITERIA_FAMILY, CONST.FILTER_OPERATOR_EQUALS, product.Family));
            requestXML.Append(GetFilterCriteria(CONST.FILTER_CRITERIA_PRODUCT, CONST.FILTER_OPERATOR_EQUALS, product.Name));
            requestXML.Append(GetFilterCriteriaForLists(CONST.FILTER_CRITERIA_VERSIONS, product.Versions));

            if (type == CONST.DATA_TYPE_LOC)
                requestXML.Append(GetFilterCriteriaForLists(CONST.FILTER_CRITERIA_KEYWORDS, keywords));

            requestXML.Append("</filterSet></includeFilterSets>");

            #endregion

            #region Exclude FilterSet

            requestXML.Append("<excludeFilterSets><filterSet>");

            if (type == CONST.DATA_TYPE_CORE)
                requestXML.Append(GetFilterCriteriaForLists(CONST.FILTER_CRITERIA_KEYWORDS, keywords));

            requestXML.Append("</filterSet></excludeFilterSets>");

            #endregion

            #region Grouping

            requestXML.Append("<grouping>");
            requestXML.Append("<groupBy><name>PRODUCT_NAME</name></groupBy>");
            requestXML.Append("<groupBy><name>VERSION_NAME</name></groupBy>");
            requestXML.Append("<groupBy><name>OWNER</name></groupBy>");
            requestXML.Append("<groupBy><name>STATE</name></groupBy>");
            requestXML.Append("<groupBy><name>STATUS</name></groupBy>");
            requestXML.Append("</grouping>");
            requestXML.Append("<aggregation><filterName>BUG_ID</filterName><function>COUNT</function></aggregation>");
            requestXML.Append("<caseSensitive>false</caseSensitive>");
            requestXML.Append("<entityToSearch><name>BUG</name></entityToSearch>");

            #endregion

            requestXML.Append("</criteria>");

            return requestXML.ToString();
        }

        /// <summary>
        /// GetDataFromWatson
        /// </summary>
        /// <param name="url"></param>
        /// <param name="request"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public XmlDocument GetDataFromWatson(string requestXml)
        {
            XmlDocument XMLResponse = null;
            //Declare an HTTP-specific implementation of the WebRequest class.
            HttpWebRequest objHttpWebRequest = (HttpWebRequest)WebRequest.Create(watsonServerURL);
            //Declare an HTTP-specific implementation of the WebResponse class
            HttpWebResponse objHttpWebResponse = null;
            //Declare a generic view of a sequence of bytes
            Stream objRequestStream = null;
            Stream objResponseStream = null;
            //string Response = null;

            //Declare XMLReader
            XmlTextReader objXMLReader;
            try
            {
                //----------------------- Start HttpRequest-----------------------------------------
                //Set HttpWebRequest properties
                byte[] bytes;
                // bytes = System.Text.Encoding.ASCII.GetBytes(v_objXMLDoc.InnerXml);
                bytes = System.Text.Encoding.ASCII.GetBytes(requestXml);
                objHttpWebRequest.Method = "POST";
                objHttpWebRequest.ContentLength = bytes.Length;
                objHttpWebRequest.ContentType = "application/xml";
                // objHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
                //Get Stream object 
                objRequestStream = objHttpWebRequest.GetRequestStream();
                //Writes a sequence of bytes to the current stream 
                objRequestStream.Write(bytes, 0, bytes.Length);
                //Close stream
                objRequestStream.Close();
                //----------------------------- End HttpRequest---------------------------------------
                //Sends the HttpWebRequest, and waits for a response.
                objHttpWebResponse = (HttpWebResponse)objHttpWebRequest.GetResponse();
                //----------------------------- Start HttpResponse-------------------------------------
                if (objHttpWebResponse.StatusCode == HttpStatusCode.OK)
                {
                    //Get response stream
                    objResponseStream = objHttpWebResponse.GetResponseStream();
                    //XML.....
                    //Load response stream into XMLReader
                    objXMLReader = new XmlTextReader(objResponseStream);
                    //Declare XMLDocument
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.Load(objXMLReader);
                    //Set XMLResponse object returned from XMLReader
                    XMLResponse = xmldoc;
                    //Close XMLReader
                    objXMLReader.Close();
                }
                //Close HttpWebResponse
                objHttpWebResponse.Close();
            }
            catch (WebException webException)
            {
                //TODO: Add custom exception handling
                throw new Exception(webException.Message);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                //Close connections
                objRequestStream.Close();
                objResponseStream.Close();
                objHttpWebResponse.Close();
                //Release objects 
                objRequestStream = null;
                objResponseStream = null;
                objHttpWebResponse = null;
                objHttpWebRequest = null;
                //XML Objects
                objXMLReader = null;
            }

            return XMLResponse;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// GetFilterCriteria
        /// </summary>
        /// <param name="filterName"></param>
        /// <param name="filterOp"></param>
        /// <param name="filterValue"></param>
        /// <returns></returns>
        private string GetFilterCriteria(string filterName, string filterOp, string filterValue)
        {
            string filterQuery = "<filter><filterName>{0}</filterName><operator><name>{1}</name></operator>{2}</filter>";
            string filterValueQuery = "<value>{0}</value>";

            if (filterOp == "Equals")
                return String.Format(filterQuery, filterName, filterOp, String.Format(filterValueQuery, filterValue));
            else
                return String.Format(filterQuery, filterName, filterOp, filterValue);
        }

        /// <summary>
        /// GetFilterCriteriaForLists
        /// </summary>
        /// <param name="filterName"></param>
        /// <param name="filterList"></param>
        /// <returns></returns>
        private string GetFilterCriteriaForLists(string filterName, string[] filterList)
        {
            StringBuilder filterValue = new StringBuilder();
            foreach (string value in filterList)
                filterValue.Append("<value>" + value + "</value>");

            return GetFilterCriteria(filterName, CONST.FILTER_OPERATOR_IN, filterValue.ToString());
        }

        #endregion
    }
}
