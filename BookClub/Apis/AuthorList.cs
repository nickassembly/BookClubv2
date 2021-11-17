using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookClub.Apis
{
    public class AuthorListHandler : IRequestHandler<AuthorListRequest, AuthorListResponse>
    {
        public AuthorListHandler()
        {

        }
       
        public Task<AuthorListResponse> Handle(AuthorListRequest request,
                                        CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
