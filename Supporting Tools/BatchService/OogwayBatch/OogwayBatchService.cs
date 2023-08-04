using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Configuration;
using BL = Adobe.OogwayBatch.BusinessLayer;
using DTO = Adobe.OogwayBatch.Common.DataTransferObjects;
using Adobe.OogwayBatch.Common;

namespace Adobe.OogwayBatch.Service
{
    /// <summary>
    /// OogwayBatchService
    /// </summary>
    public partial class OogwayBatchService : ServiceBase
    {
        #region Variables

        private string time = ConfigurationManager.AppSettings["ServiceExecutionTime"];

        #endregion

        #region Constructor

        /// <summary>
        /// OogwayBatchService
        /// </summary>
        public OogwayBatchService()
        {
            InitializeComponent();
            //Adobe.OogwayBatch.Common.ExceptionLogger.LogData("Oogway Service Started");

            //if (!System.Diagnostics.EventLog.SourceExists("OogwayBatchService"))
            //{
            //    System.Diagnostics.EventLog.CreateEventSource("OogwayBatchService", "Source created for Oogway Batch Service");
            //}

            //eventLog.Source = "OogwayBatchService";
            //eventLog.Log = "Source created for Oogway Batch Service";

            //OnStart(null);
            //InitiateBatchProcess();
        }

        #endregion

        #region Service Events

        /// <summary>
        /// OnStart
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            Adobe.OogwayBatch.Common.ExceptionLogger.LogData("Oogway Batch Service Started.");

            InitiateBatchProcess();

            System.Timers.Timer T1 = new System.Timers.Timer();
            T1.Interval = (300000);
            T1.AutoReset = true;
            T1.Enabled = true;
            T1.Start();
            T1.Elapsed += new System.Timers.ElapsedEventHandler(InitiateProcess);
        }

        /// <summary>
        /// OnStop
        /// </summary>
        protected override void OnStop()
        {
            Adobe.OogwayBatch.Common.ExceptionLogger.LogData("Oogway Batch Service Stopped.");
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// InitiateProcess
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InitiateProcess(object sender, EventArgs e)
        {
            if (System.DateTime.Now.TimeOfDay < TimeSpan.Parse(time) && TimeSpan.Parse(time) < System.DateTime.Now.AddMinutes(5).TimeOfDay)
            {
                Adobe.OogwayBatch.Common.ExceptionLogger.LogData("Batch Process initiated...");
                InitiateBatchProcess();
            }
            else
                Adobe.OogwayBatch.Common.ExceptionLogger.LogData("Batch initiated but no action.");
        }

        /// <summary>
        /// InitiateBatchProcess
        /// </summary>
        private void InitiateBatchProcess()
        {
            ExceptionLogger.LogData("**************Service Operation Started****************");

            DTO.Response response = new DTO.Response();
            response.ExecutionStartTime = DateTime.Now;

            BL.BusinessObjects obj = new BL.BusinessObjects();

            ExceptionLogger.LogData("Watson Bug fetch Initiated");
            obj.GetBugsData(response);
            ExceptionLogger.LogData("Watson Bug fetch Completed");

            ExceptionLogger.LogData("Graph XML generation Initiated");
            obj.GenerateGraphXMLsForPlotting(response);
            ExceptionLogger.LogData("Graph XML generation Completed\n\n\n");

            response.ExecutionEndTime = DateTime.Now;

            obj.SendMailNotification(response);

            ExceptionLogger.LogData("**************Service Operation Completed****************");
        }

        #endregion
    }
}
