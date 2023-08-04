using System;
using System.Net;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.Protocols;
using System.Net.Mail;

namespace Adobe.Localization.Insights.Common
{
    /// <summary>
    /// EmailNotifications
    /// </summary>
    public class EmailNotifications
    {
        #region Variables

        private string defaultEmailID = System.Configuration.ConfigurationManager.AppSettings[WebConstants.SETTING_EMAIL_ID];
        private string defaultEmailName = System.Configuration.ConfigurationManager.AppSettings[WebConstants.SETTING_EMAIL_NAME];
        private string smtpServer = System.Configuration.ConfigurationManager.AppSettings[WebConstants.SETTING_SMTP_SERVER];
        private int smtpPort = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings[WebConstants.SETTING_SMTP_PORT].ToString());

        private string senderList = "";
        private string receiverList = "";
        private string ccList = "";
        private string bccList = "";

        #endregion

        #region Constructor

        /// <summary>
        /// EmailNotifications
        /// </summary>
        /// <param name="toList"></param>
        /// <param name="fromList"></param>
        public EmailNotifications(string toList, string fromList)
        {
            receiverList = toList;
            senderList = fromList;
        }

        /// <summary>
        /// EmailNotifications
        /// </summary>
        /// <param name="toList"></param>
        /// <param name="fromList"></param>
        /// <param name="ccList"></param>
        /// <param name="bccList"></param>
        public EmailNotifications(string toList, string fromList, string ccList, string bccList)
        {
            senderList = fromList;
            receiverList = toList;
            CcList = ccList;
            BccList = bccList;
        }

        #endregion

        #region Properties

        /// <summary>
        /// SenderList
        /// </summary>
        public string SenderList
        {
            get
            {
                return senderList;
            }
            set
            {
                senderList = value;
            }
        }

        /// <summary>
        /// ReceiverList
        /// </summary>
        public string ReceiverList
        {
            get
            {
                return receiverList;
            }
            set
            {
                receiverList = value;
            }
        }

        /// <summary>
        /// BccList
        /// </summary>
        public string BccList
        {
            get
            {
                return bccList;
            }
            set
            {
                bccList = value;
            }
        }

        /// <summary>
        /// CcList
        /// </summary>
        public string CcList
        {
            get
            {
                return ccList;
            }
            set
            {
                ccList = value;
            }
        }

        #endregion

        /// <summary>
        /// SendEmail
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool SendEmail(string subject, string message)
        {
            return SendEmail(subject, message, null, false);
        }

        /// <summary>
        /// SendEmail
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <param name="attachments"></param>
        /// <returns></returns>
        public bool SendEmail(string subject, string message, object attachments)
        {
            return SendEmail(subject, message, attachments, false);
        }

        /// <summary>
        /// SendEmail
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <param name="isBodyHtml"></param>
        /// <returns></returns>
        public bool SendEmail(string subject, string message, bool isBodyHtml)
        {
            return SendEmail(subject, message, null, isBodyHtml);
        }

        /// <summary>
        /// SendEmail
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <param name="attachments"></param>
        /// <param name="isBodyHtml"></param>
        /// <returns></returns>
        public bool SendEmail(string subject, string message, object attachments, bool isBodyHtml)
        {
            SmtpClient smtpClient = new SmtpClient();
            MailMessage mailMessage = new MailMessage();

            try
            {
                smtpClient.Host = smtpServer;
                smtpClient.Port = smtpPort;

                MailAddress defaultMailAddress;
                defaultMailAddress = new MailAddress(defaultEmailID, defaultEmailName);

                if (ReceiverList == "")
                    mailMessage.To.Add(defaultEmailID);
                else
                    mailMessage.To.Add(ReceiverList);

                if (SenderList != "")
                    defaultMailAddress = new MailAddress(SenderList);
                mailMessage.From = defaultMailAddress;

                if (CcList != "")
                    mailMessage.CC.Add(CcList);

                if (BccList != "")
                    mailMessage.Bcc.Add(BccList);

                mailMessage.Subject = subject;

                //Specify true if it  is html message
                mailMessage.IsBodyHtml = isBodyHtml;

                // Message body content
                mailMessage.Body = message;

                // Send SMTP mail
                smtpClient.Send(mailMessage);

                return true;
            }
            catch (Exception ex)
            {
                ExceptionLogger.Log(ex, "Subject: " + subject + " ; Message: " + message);
                return false;
            }
        }
    }
}
