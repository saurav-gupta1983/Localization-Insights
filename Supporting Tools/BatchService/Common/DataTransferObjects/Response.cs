using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Generic;

namespace Adobe.OogwayBatch.Common.DataTransferObjects
{
    [Serializable]
    public class Response
    {
        #region Variables

        private DateTime executionStartTime;
        private DateTime executionEndTime;
        private List<Product> products;
        private List<string> errorMessages;
        private List<string> status;
        private List<string> executionTime;
        private string message;
        private bool isSuccess;

        #endregion

        #region Public Properties

        /// <summary>
        /// executionStartTime
        /// </summary>
        public DateTime ExecutionStartTime
        {
            get
            {
                return executionStartTime;
            }
            set
            {
                executionStartTime = value;
            }
        }

        /// <summary>
        /// executionEndTime
        /// </summary>
        public DateTime ExecutionEndTime
        {
            get
            {
                return executionEndTime;
            }
            set
            {
                executionEndTime = value;
            }
        }

        /// <summary>
        /// Products
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
        /// errorMessages
        /// </summary>
        public List<string> ErrorMessages
        {
            get
            {
                return errorMessages;
            }
            set
            {
                errorMessages = value;
            }
        }

        /// <summary>
        /// status
        /// </summary>
        public List<string> Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }

        /// <summary>
        /// executionTime
        /// </summary>
        public List<string> ExecutionTime
        {
            get
            {
                return executionTime;
            }
            set
            {
                executionTime = value;
            }
        }

        /// <summary>
        /// message
        /// </summary>
        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
            }
        }

        /// <summary>
        /// isSuccess
        /// </summary>
        public bool IsSuccess
        {
            get
            {
                return isSuccess;
            }
            set
            {
                isSuccess = value;
            }
        }

        #endregion
    }
}
