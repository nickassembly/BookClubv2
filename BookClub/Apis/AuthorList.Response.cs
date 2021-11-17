using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClub.Apis
{
    public class AuthorListResponse
    {
        public IEnumerable<AuthorListApiModel> Authors { get; set; }
    }
}
