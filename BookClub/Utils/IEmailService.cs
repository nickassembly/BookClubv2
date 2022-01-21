using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClub.Utils
{
    public interface IEmailService
    {
        List<string> GetEmailRecepients(string connectionString);
        Task SendMail(List<string> emailRecepients);
    }
}
