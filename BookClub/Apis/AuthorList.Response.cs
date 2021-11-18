using System.Collections.Generic;

namespace BookClub.Apis
{
    public class AuthorListResponse
    {
        public IEnumerable<AuthorListApiModel> Authors { get; set; }
    }
}
