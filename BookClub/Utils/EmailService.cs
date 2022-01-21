using FluentEmail.Core;
using FluentEmail.Smtp;
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

        public async Task SendMail(List<string> emailRecepients)
        {
            var sender = new SmtpSender(() => new SmtpClient("localhost")
            {
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Port = 25
                // DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
                // PickupDirectoryLocation = @"C:\EmailDemos"
            });

            Email.DefaultSender = sender;

            var email = await Email
                .From("noreply@gtech.com")
                .To("nickguerra@gmail.com", "Nick")
                .Subject("Thanks")
                .Body("Thanks for adding a book")
                .SendAsync();
        }
    }
}
