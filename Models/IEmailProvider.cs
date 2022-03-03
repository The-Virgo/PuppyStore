using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PuppyStoreFinal.Models
{
    public interface IEmailProvider
    {
        Task SendEmailAsync(int? pupId, EmailProviderSendgrid e);
    }
    public class EmailProviderSendgrid : IEmailProvider
    {
        private readonly IConfiguration Configuration;
        public EmailProviderSendgrid(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Content { get; set; }

        public EmailProviderSendgrid() { }

        public async Task SendEmailAsync(int? pupId, EmailProviderSendgrid e)
        {
            var apiKey = Configuration.GetSection("sendgrid-key").Value;
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("pawpalscustomercontact@gmail.com", "Customer"),
                Subject = "Customer Message: " + e.FirstName,
                PlainTextContent = "Name: " + e.FirstName + " " + e.LastName + "\n"
                                 + "Email: " + e.Email + "\n"
                                 + e.Content,
                HtmlContent = "<strong>Name: </strong>" + e.FirstName + " " + e.LastName + "<br>"
                                 + "<strong>Email: </strong>" + e.Email + "<br>"
                                 + e.Content
            };
            msg.AddTo(new EmailAddress("pawpalspuyallup@gmail.com", "PawPals"));
            //var response =
            await client.SendEmailAsync(msg);
        }
    }
}
