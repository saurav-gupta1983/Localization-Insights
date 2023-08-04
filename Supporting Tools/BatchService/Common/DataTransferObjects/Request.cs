using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Generic;

namespace Adobe.OogwayBatch.Common.DataTransferObjects
{
    [Serializable]
    public class Request
    {
        #region Variables

        private List<Product> products;
        private string[] keywords;

        #endregion

        #region Public Properties

        /// <summary>
        /// products
        /// </summary>
        public List<Product> Products
        {
            get
            {
                return products;
            }
            set
            {
                products = value;
            }
        }

        /// <summary>
        /// keywords
        /// </summary>
        public string[] Keywords
        {
            get
            {
                return keywords;
            }
            set
            {
                keywords = value;
            }
        }



        #endregion
    }
}
