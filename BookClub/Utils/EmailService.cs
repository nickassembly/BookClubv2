using BookClub.ViewModels;
using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BookClub.Utils
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }
        public List<string> GetEmailRecepients(string connectionString)
        {
            // TODO: add logic to pull friends list from DB and return
            List<string> recepients = new List<string>
            {
                "nickguerra@gmail.com",
                "guerra.joseph@gmail.com"
            };

            return recepients;
        }

        public async Task SendMail(List<string> emailRecepients, EmailDetail emailDetails)
        {
            var host = _config.GetValue<string>("EmailConfiguration:SmtpServer");
            var port = _config.GetValue<int>("EmailConfiguration:Port");

            var sender = new SmtpSender(() => new SmtpClient()
            {
                Host = "localhost",
                Port = 25,
                //Host = host,
                //Port = port,
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
            });

            StringBuilder template = new();
            template.AppendLine("Attn: @Model.UserName,");
            template.AppendLine("<p>This is to notify you that an Author has been added to your list of Authors</p>");
            template.AppendLine("<p><strong>Author Details:</strong>  @Model.AuthorFirstName @Model.AuthorLastName</p>");
            template.AppendLine("<p>Please view your account page for more options</p>");
            template.AppendLine("- BookClub Team");

            Email.DefaultSender = sender;
            Email.DefaultRenderer = new RazorRenderer();

            var email = await Email
                .From("noreply@guerratechinfo.com")
                .To(emailRecepients[0]) 
                .Subject("Change to your List")
                .UsingTemplate(template.ToString(), new 
                {
                    UserName = emailDetails.RecepientName,
                    AuthorFirstName = emailDetails.AuthorDetails.Firstname,
                    AuthorLastName = emailDetails.AuthorDetails.Lastname
                })
                //.Body("Thanks for adding a book")
                .SendAsync();
        }
    }
}
