using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using DTO = Adobe.OogwayBatch.Common.DataTransferObjects;

namespace Adobe.OogwayBatch.Common
{
    /// <summary>
    /// Common
    /// </summary>
    public static class Common
    {
        /// <summary>
        /// GetLocKeywordsList
        /// </summary>
        /// <param name="XML_URL"></param>
        /// <returns></returns>
        public static string[] GetLocKeywordsList(string requestXmlUrlPath)
        {
            string[] keywords;
            XmlDocument doc = new XmlDocument();

            doc.Load(requestXmlUrlPath);
            XmlNodeList Keyword_list = doc.GetElementsByTagName("Keyword");
            int Keywords_count = Keyword_list.Count;
            keywords = new string[Keywords_count];
            int i = 0;
            foreach (XmlNode Key in Keyword_list)
            {
                keywords[i] = Key.InnerText;
                i++;
            }

            return keywords;
        }

        /// <summary>
        /// GetProductsList
        /// </summary>
        /// <param name="XML_URL"></param>
        /// <returns></returns>
        public static List<DTO.Product> GetProductsList(string requestXmlUrlPath)
        {
            XmlDocument doc = new XmlDocument();
            List<DTO.Product> products = new List<DTO.Product>();
            doc.Load(requestXmlUrlPath);
            XmlNodeList prod_list = doc.GetElementsByTagName("Product");
            foreach (XmlNode prod in prod_list)
            {
                DTO.Product product = new DTO.Product();
                product.Family = prod.Attributes["Family"].Value;
                product.Name = prod.Attributes["Name"].Value;
                int i = 0;
                XmlNode Version = prod.SelectSingleNode("Versions");
                XmlNodeList version_list = Version.ChildNodes;
                int version_count = version_list.Count;
                product.Versions = new string[version_count];
                foreach (XmlNode version_v in version_list)
                {
                    product.Versions[i] = version_v.InnerText;
                    i++;
                }

                products.Add(product);
            }

            return products;
        }

        /// <summary>
        /// GetRequestDetails
        /// </summary>
        /// <param name="requestXmlUrlPath"></param>
        /// <returns></returns>
        public static DTO.Request GetRequestDetails(string requestXmlUrlPath)
        {
            XmlDocument doc = new XmlDocument();

            DTO.Request request = new DTO.Request();

            request.Products = GetProductsList(requestXmlUrlPath);
            request.Keywords = GetLocKeywordsList(requestXmlUrlPath);

            return request;
        }
        
        /// <summary>
        /// GetStringFromList
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string GetStringFromList(string[] list)
        {
            StringBuilder singleString = new StringBuilder();

            if (list.Length > 0)
            {
                singleString.Append(list[0]);

                for(int count =1; count < list.Length; count++)
                    singleString.Append("," + list[count]);
            }

            return singleString.ToString();
            
        }

        /// <summary>
        /// GetStringFromList
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string GetStringFromList(List<string> list)
        {
            StringBuilder singleString = new StringBuilder();

            if (list.Count > 0)
            {
                singleString.Append(list[0]);

                for (int count = 1; count < list.Count; count++)
                    singleString.Append("," + list[count]);
            }

            return singleString.ToString();

        }
    }
}
