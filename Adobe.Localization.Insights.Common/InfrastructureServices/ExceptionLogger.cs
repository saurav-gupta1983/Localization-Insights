using System.Diagnostics;
using System;
using System.IO;

namespace Adobe.Localization.Insights.Common
{
    /// <summary>
    /// ExceptionLogger
    /// </summary>
    public class ExceptionLogger
    {
        #region ExceptionLog

        /// <summary>
        /// Log
        /// </summary>
        /// <param name="exception"></param>
        public static void Log(Exception exception, string addInfo)
        {
            try
            {
                string logger = System.Configuration.ConfigurationManager.AppSettings[WebConstants.SETTING_LOGGER].ToString();
                string logPath = System.Configuration.ConfigurationManager.AppSettings[WebConstants.SETTING_LOG_LOCATION].ToString();

                if (logger == "TRUE" && logPath != "")
                {
                    if (!File.Exists(logPath))
                        File.Create(logPath);

                    string newLine = System.Environment.NewLine;
                    string asteriks = "*********************************************";
                    string message = String.Format(newLine + asteriks + newLine + "{0} : {1}" + newLine + newLine + "Additional Information: {2}" + newLine + asteriks, System.DateTime.Now.ToString(), exception.ToString(), addInfo);
                    File.AppendAllText(logPath, message, System.Text.Encoding.Unicode);
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #endregion
    }
}
