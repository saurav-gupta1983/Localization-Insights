using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Generic;

namespace Adobe.OogwayBatch.Common.DataTransferObjects
{
    [Serializable]
    public class Product
    {
        #region Variables

        private string family;
        private string name;
        private string[] versions;

        #endregion

        #region Public Properties

        /// <summary>
        /// Family
        /// </summary>
        public string Family
        {
            get
            {
                return family;
            }
            set
            {
                family = value;
            }
        }

        /// <summary>
        /// name
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        /// <summary>
        /// versions
        /// </summary>
        public string[] Versions
        {
            get
            {
                return versions;
            }
            set
            {
                versions = value;
            }
        }

        #endregion
    }
}
