using AutoMapper;
using BookClub.Data.Entities;
using BookClub.Generics;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BookClub.Apis
{
    public class AuthorListHandler : IRequestHandler<AuthorListRequest, AuthorListResponse>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Author> _repo;

        public AuthorListHandler(IMapper mapper,
            IRepository<Author> repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<AuthorListResponse> Handle(AuthorListRequest request,
                                        CancellationToken cancellationToken)
        {
            var authors = await _repo.ListAsync(cancellationToken);

            return _mapper.Map<AuthorListResponse>(authors);
        }
    }
}
