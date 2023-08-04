using System.Diagnostics;
using System;
using System.IO;
using CONST = Adobe.OogwayBatch.Common.Constants;

namespace Adobe.OogwayBatch.Common
{
    /// <summary>
    /// ExceptionLogger
    /// </summary>
    public class ExceptionLogger
    {
        #region Variables

        private static string logger = System.Configuration.ConfigurationManager.AppSettings[CONST.SETTING_LOGGER].ToString();
        private static string logPath = System.Configuration.ConfigurationManager.AppSettings[CONST.SETTING_LOG_LOCATION].ToString();

        #endregion

        #region ExceptionLog

        /// <summary>
        /// Log
        /// </summary>
        /// <param name="exception"></param>
        public static void Log(Exception exception, string addInfo)
        {
            try
            {
                string logger = System.Configuration.ConfigurationManager.AppSettings[CONST.SETTING_LOGGER].ToString();
                string logPath = System.Configuration.ConfigurationManager.AppSettings[CONST.SETTING_LOG_LOCATION].ToString();

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

        /// <summary>
        /// Log
        /// </summary>
        /// <param name="exception"></param>
        public static void LogData(string addInfo)
        {
            try
            {
                if (logger == "TRUE" && logPath != "")
                {
                    if (!File.Exists(logPath))
                        File.Create(logPath);

                    string newLine = System.Environment.NewLine;
                    string message = String.Format(newLine + "{0} : {1}", System.DateTime.Now.ToString(), addInfo);

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
