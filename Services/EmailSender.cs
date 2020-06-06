using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SendgridEmaiWebAPI.Services
{
    public class EmailSender : IEmailSender
    {

        public Task<Response> SendEmailAsync(List<string> to, List<string> cc, string subject, string message)
        {
            return Execute(Environment.GetEnvironmentVariable("SENDEMAILDEMO_ENVIRONMENT_SENDGRID_KEY"), subject, message, cc, to);
        }

        public async Task<Response> Execute(string apiKey, string subject, string message, List<string> to, List<string> cc)
        {
            apiKey = Environment.GetEnvironmentVariable("SENDEMAILDEMO_ENVIRONMENT_SENDGRID_KEY");
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("sijuadebabs@gmail.com", "Benignus Okorie"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };

            foreach (var t in to)
            {
                msg.AddTo(new EmailAddress(t));
            }

            foreach (var c in cc)
            {
                msg.AddCc(new EmailAddress(c));
            }

            var response = await client.SendEmailAsync(msg);
            return response;
        }
    }
}
