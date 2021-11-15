using Google.Apis.Books.v1;
using Google.Apis.Books.v1.Data;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookClub.Utils
{
    public class BookSearch
    {
        public Volumes SearchISBN(string id= "")
        {
            string query = $"q=isbn:{id}";
            var result = service.Volumes.List(query).Execute();
            if (result != null && result.Items != null) {
                return result;
            }
            return null;
        }
        public static BooksService service = new BooksService(
           new BaseClientService.Initializer
           {
               ApplicationName = "BookClub",
               ApiKey = "AIzaSyCjqD7OtvMLj-JMh3erdPRh_qWyRJvnvxw"
           });
        }
}
