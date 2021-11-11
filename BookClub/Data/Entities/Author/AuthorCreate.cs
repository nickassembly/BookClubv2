using AutoMapper;
using BookClub.Generics;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookClub.Data.Entities
{
    public class AuthorCreate : IRequestHandler<AuthorCreateRequest, AuthorCreateResponse>
    {
        private readonly IRepository<Author> _repo;
        private readonly IMapper _mapper;

        public AuthorCreate(IRepository<Author> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<AuthorCreateResponse> Handle(
            AuthorCreateRequest request, 
            CancellationToken cancellationToken)
        {
            AuthorCreateResponse response = new();

            var newAuthor = _mapper.Map<Author>(request);

            var createdAuthor = await _repo.AddAsync(newAuthor, cancellationToken);

            response.Id = createdAuthor.Id;

            return response;
        }
    }
}
