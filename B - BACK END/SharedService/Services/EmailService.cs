using System;
using System.Linq;
using System.Net.Mail;
using System.Text;
using log4net;
using Mustache;
using Newtonsoft.Json;
using SharedService.Interfaces.Services;

namespace SharedService.Services
{
    public class EmailService : IEmailService
    {
        #region Properties

        /// <summary>
        /// Log instance.
        /// </summary>
        private readonly ILog _log;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize email with injectors.
        /// </summary>
        public EmailService()
        {
            _log = LogManager.GetLogger(typeof(EmailService));
        }

        #endregion

        #region Methods

        /// <summary>
        /// Read service configuration.
        /// </summary>
        public void ReadConfig()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Send email.
        /// </summary>
        /// <param name="recipients"></param>
        /// <param name="carbonCopies"></param>
        /// <param name="blindCarbonCopies"></param>
        /// <param name="subject">Subject of email.</param>
        /// <param name="content">Email raw content</param>
        /// <param name="data">Data which should be used for formatting email.</param>
        /// <param name="bIsHtml"></param>
        /// <param name="bIsErrorSuppressed">Whether exception should be suppressed or not.</param>
        public void SendMail(MailAddress[] recipients, MailAddress[] carbonCopies, MailAddress[] blindCarbonCopies, string subject, string content, object data, bool bIsHtml, bool bIsErrorSuppressed)
        {
            var szMessageBuilder = new StringBuilder();
            szMessageBuilder.AppendLine("Sending email ...");
            szMessageBuilder.AppendLine($"Recipients: {string.Join(",", recipients.Select(x => x.Address))}");

            // Carbon copies has been specified.
            if (carbonCopies != null && carbonCopies.Length > 0)
                szMessageBuilder.AppendLine($"Ccs: {carbonCopies.Select(x => x.Address)}");

            // Blind carbon copies list has been defined.
            if (blindCarbonCopies != null && blindCarbonCopies.Length > 0)
                szMessageBuilder.AppendLine($"Blind carbon copies: {string.Join(",", blindCarbonCopies.Select(x => x.Address))}");
            szMessageBuilder.AppendLine($"Subject: {subject}");
            szMessageBuilder.AppendLine($"Content: {content}");

            // Data is defined.
            if (data != null)
                szMessageBuilder.AppendLine($"Data: {JsonConvert.SerializeObject(data)}");
            _log.Info(szMessageBuilder);

            using (var smtpClient = new SmtpClient())
            {
                try
                {
                    var mailMessage = new MailMessage();
                    mailMessage.Subject = subject;


                    //#if DEBUG
                    //                    // In debugging mode. Just send email to a specific person.

                    //                        mailMessage.To.Add(new MailAddress("nguyendacthai1992@gmail.com"));
                    //#else

                    // Add recipients.
                    foreach (var recipient in recipients)
                        mailMessage.To.Add(recipient);

                    // Add carbon copies.
                    if (carbonCopies != null)
                    {
                        foreach (var carbonCopy in carbonCopies)
                            mailMessage.CC.Add(carbonCopy);
                    }

                    // Add blind carbon copy.
                    if (blindCarbonCopies != null)
                    {
                        foreach (var blindCarbonCopy in blindCarbonCopies)
                            mailMessage.Bcc.Add(blindCarbonCopy);
                    }

                    //#endif

                    if (data != null)
                    {
                        var formatCompiler = new FormatCompiler();
                        var generator = formatCompiler.Compile(content);
                        mailMessage.Body = generator.Render(data);
                    }
                    else
                    {
                        mailMessage.Body = content;
                    }
                        
                    mailMessage.IsBodyHtml = bIsHtml;

                    smtpClient.Send(mailMessage);
                }
                catch (Exception exception)
                {
                    _log.Error(exception.Message, exception);
                }

            }
        }

        /// <summary>
        /// Send email
        /// </summary>
        /// <param name="recipients"></param>
        /// <param name="subject"></param>
        /// <param name="content"></param>
        /// <param name="data"></param>
        /// <param name="bIsHtml"></param>
        /// <param name="bIsErrorSupressed"></param>
        public void SendMail(MailAddress[] recipients, string subject, string content, object data, bool bIsHtml, bool bIsErrorSupressed)
        {
            SendMail(recipients, null, null, subject, content, data, bIsHtml, bIsErrorSupressed);
        }

        /// <summary>
        /// Send email
        /// </summary>
        /// <param name="recipients"></param>
        /// <param name="carbonCopies"></param>
        /// <param name="subject"></param>
        /// <param name="content"></param>
        /// <param name="data"></param>
        /// <param name="bIsHtml"></param>
        /// <param name="bIsErrorSuppressed"></param>
        public void SendMail(MailAddress[] recipients, MailAddress[] carbonCopies, string subject, string content, object data, bool bIsHtml, bool bIsErrorSuppressed)
        {
            SendMail(recipients, carbonCopies, null, subject, content, data, bIsHtml, bIsErrorSuppressed);
        }

        #endregion
    }
}