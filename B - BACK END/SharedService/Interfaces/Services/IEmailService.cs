using System.Net.Mail;

namespace SharedService.Interfaces.Services
{
    public interface IEmailService
    {
        #region Methods

        /// <summary>
        /// Load email configuration from config file.
        /// </summary>
        void ReadConfig();

        /// <summary>
        /// Send email with specific settings.
        /// </summary>
        /// <param name="recipients"></param>
        /// <param name="subject"></param>
        /// <param name="content"></param>
        /// <param name="data"></param>
        /// <param name="bIsHtml"></param>
        /// <param name="bIsErrorSupressed"></param>
        void SendMail(MailAddress[] recipients, string subject, string content, object data, bool bIsHtml, bool bIsErrorSupressed);

        /// <summary>
        /// Send email with specific settings.
        /// </summary>
        /// <param name="recipients"></param>
        /// <param name="carbonCopies"></param>
        /// <param name="subject"></param>
        /// <param name="content"></param>
        /// <param name="data"></param>
        /// <param name="bIsHtml"></param>
        /// <param name="bIsErrorSuppressed"></param>
        void SendMail(MailAddress[] recipients, MailAddress[] carbonCopies, string subject, string content, object data,
            bool bIsHtml, bool bIsErrorSuppressed);

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
        /// <param name="bIsErrorSuppressed">Whether error should be suppressed or not.</param>
        void SendMail(MailAddress[] recipients, MailAddress[] carbonCopies, MailAddress[] blindCarbonCopies, string subject, string content, object data, bool bIsHtml, bool bIsErrorSuppressed);

        #endregion
    }
}
