using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data;
using System.IO;
using System.Configuration;
using CONST = Adobe.OogwayBatch.Common.Constants;
using DAO = Adobe.OogwayBatch.DataLayer.DataAccessObjects;
using DTO = Adobe.OogwayBatch.Common.DataTransferObjects;
using Adobe.OogwayBatch.Common;

namespace Adobe.OogwayBatch.BusinessLayer.Components
{
    /// <summary>
    /// GenerateGraphs
    /// </summary>
    public class GenerateGraphs
    {
        #region Variables

        private static string graphXMLFolder; //folder where we store xmls from which graphs are ploted
        private DTO.Response response;

        #endregion

        #region Constructor

        /// <summary>
        /// GenerateGraphs
        /// </summary>
        public GenerateGraphs(DTO.Response resp)
        {
            graphXMLFolder = ConfigurationManager.AppSettings[CONST.LOCATION_GRAPH_XML].ToString();
            response = resp;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// CreateXMLForGraph - Create XML for graph plotting.
        /// </summary>
        /// <param name="type"></param>
        public void GeneratePlottingGraphXML(string type)
        {
            DataTable dtProductDetails = DAO.DAObjects.GetProductDetails(response.ExecutionStartTime).Tables[0];

            if(dtProductDetails.Rows.Count == 0)
                ExceptionLogger.LogData("Graphs generation Failed. No Data. \n");
 
            foreach (DataRow drProduct in dtProductDetails.Rows)
            {
                string fileName = graphXMLFolder + drProduct[CONST.COL_PRODUCT].ToString() + "_" + drProduct[CONST.COL_VERSION].ToString() + "_" + type + ".xml";

                XmlDocument xmlGraph = new XmlDocument();

                if (File.Exists(fileName)) //for that product version type..... an xml already exists in the folder                                          
                    xmlGraph.Load(fileName); //load xml                      
                else
                    xmlGraph = CreateXMLForGraph(drProduct, fileName);

                AppendDataToXMLForGraph(xmlGraph, drProduct, type); //append current data into xml.

                xmlGraph.Save(fileName);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// GetExistingXmls - Get all the existing xml file names in the folder.
        /// </summary>
        public string[] GetExistingXmls()
        {
            List<String> list = new List<String>();

            foreach (string filePath in Directory.EnumerateFiles(graphXMLFolder, "*.xml"))
                list.Add(Path.GetFileNameWithoutExtension(filePath));

            return list.ToArray();
        }

        /// <summary>
        /// createXMLforGraph - Create xml file for plotting graph incase there is no xml file for that product and version.....
        /// </summary>
        /// <param name="productVersion"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private XmlDocument CreateXMLForGraph(DataRow drProduct, string fileName)
        {
            StringBuilder xmlList = new StringBuilder();
            xmlList.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?><Content>");
            xmlList.Append("<Product>" + drProduct[CONST.COL_PRODUCT].ToString() + "</Product>");
            xmlList.Append("<Version>" + drProduct[CONST.COL_VERSION].ToString() + "</Version>");

            //XmlDocument stateXml = new XmlDocument();
            //stateXml.Load(stateXmlPath);

            //XmlNodeList stateList = stateXml.GetElementsByTagName("state");
            //foreach (XmlNode state in stateList)
            //{
            //    string state_xml = state.Attributes["state"].Value;
            //    int childCount = state.ChildNodes.Count;
            //    for (int i = 0; i < childCount; i++)
            //    {
            //        string status_xml = state.ChildNodes[i].InnerText;
            //        xmlList.Append("<Series>");
            //        xmlList.Append("<Name>" + state_xml + "-" + status_xml + "</Name>");
            //        xmlList.Append("<Data></Data>");
            //        xmlList.Append("</Series>");
            //    }
            //}

            DataTable dtBugStatusDetails = DAO.DAObjects.GetBugStatusDetails().Tables[0];

            foreach (DataRow dr in dtBugStatusDetails.Rows)
            {
                xmlList.Append("<Series>");
                xmlList.Append("<Name>" + dr[0].ToString() + "-" + dr[1].ToString() + "</Name>");
                xmlList.Append("<Data></Data>");
                xmlList.Append("</Series>");
            }

            xmlList.Append("</Content>");
            string content_xml = xmlList.ToString();
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(content_xml);
            //xml.Save(fileName);
            return xml;
        }

        /// <summary>
        /// AppendDataToXMLForGraph - Append data to the xml file....
        /// </summary>
        /// <param name="xmlDoc"></param>
        private void AppendDataToXMLForGraph(XmlDocument xmlDoc, DataRow drProduct, string type)
        {
            XmlNode node = xmlDoc.SelectSingleNode("Content/Product");
            node = xmlDoc.SelectSingleNode("Content/Version");

            DataSet dsBugsData = DAO.DAObjects.GetBugsCount(drProduct, type);
            DataTable dtBugsData = dsBugsData.Tables[0];

            //XmlDocument xml = new XmlDocument();
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    dsBugsData.WriteXml(ms);
            //    ms.Seek(0, SeekOrigin.Begin);
            //    xml.Load(ms);
            //}

            //XmlNodeList recordList = xml.GetElementsByTagName("Records");
            //if (recordList.Count != 0)
            //{
            //foreach (XmlNode record in recordList)
            foreach (DataRow dr in dtBugsData.Rows)
            {
                string state = dr[0].ToString();
                string status = dr[1].ToString();
                string xml_content = "<Point Day=\"" + dr[2].ToString() + "\" Month =\"" + dr[3].ToString() + "\" Year=\"" + dr[4].ToString() + "\" Count=\"" + dr[5].ToString() + "\"></Point>";
                XmlNodeList seriesList = xmlDoc.GetElementsByTagName("Series");
                foreach (XmlNode series in seriesList)
                    if (series.ChildNodes[0].InnerText == state + "-" + status)
                    {
                        var xfrag = xmlDoc.CreateDocumentFragment();
                        xfrag.InnerXml = xml_content;

                        XmlNode Data = series.ChildNodes[1];
                        if (series.ChildNodes[1].ChildNodes.Count > 0)
                        {
                            XmlNode dataNode = series.ChildNodes[1].ChildNodes[series.ChildNodes[1].ChildNodes.Count - 1];

                            if (dataNode.Attributes["Day"].Value == dr[2].ToString() && dataNode.Attributes["Month"].Value == dr[3].ToString() && dataNode.Attributes["Year"].Value == dr[4].ToString())
                                dataNode.Attributes["Count"].Value = dr[5].ToString();
                            else
                                Data.AppendChild(xfrag);
                        }
                        else
                            Data.AppendChild(xfrag);
                    }
            }
            //}
        }

        #endregion
    }
}
