using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace MFR.EmailSender
{
    internal class SendMail : ISendMail
    {
        public async Task SendMailAsync(string fromAddress, string toAddress, string subject, string message)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(fromAddress);
            var to = new EmailAddress(toAddress);
            var plainTextContent = message;
            //var htmlContent = "<strong>message</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, null);
            await client.SendEmailAsync(msg);
        }
    }
}
