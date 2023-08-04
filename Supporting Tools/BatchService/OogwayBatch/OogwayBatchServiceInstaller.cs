using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;

namespace Adobe.OogwayBatch.Service
{
    [RunInstaller(true)]
    public partial class OogwayBatchServiceInstaller : System.Configuration.Install.Installer
    {
        #region Variables

        private readonly ServiceProcessInstaller processInstaller;
        private readonly System.ServiceProcess.ServiceInstaller i;

        #endregion

        #region Constructor

        /// <summary>
        /// ServiceInstaller
        /// </summary>
        public OogwayBatchServiceInstaller()
        {
            InitializeComponent();

            //processInstaller = new ServiceProcessInstaller();
            //serviceInstaller = new System.ServiceProcess.ServiceInstaller();

            //// Service will run under system account
            //processInstaller.Account = ServiceAccount.LocalService;

            //// Service will have Start Type of Manual
            //serviceInstaller.StartType = ServiceStartMode.Automatic;

            //serviceInstaller.ServiceName = "OogwayBatchService";

            //Installers.Add(serviceInstaller);
            //Installers.Add(processInstaller);
            //serviceInstaller.AfterInstall += ServiceInstaller_AfterInstall;
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// ServiceInstaller_AfterInstall
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ServiceInstaller_AfterInstall(object sender, InstallEventArgs e)
        {
            ServiceController sc = new ServiceController("OogwayBatchService");
            sc.Start();
        }

        #endregion
    }
}
