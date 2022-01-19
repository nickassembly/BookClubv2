using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClub.Utils
{
    public class EmailService : IEmailService
    {
        public List<string> GetEmailRecepients(string connectionString)
        {
            // DB call to get user list or logged in user's friends list
            throw new NotImplementedException();
        }

        public void SendMail(List<string> emailRecepients)
        {
            throw new NotImplementedException();
        }
    }
}
