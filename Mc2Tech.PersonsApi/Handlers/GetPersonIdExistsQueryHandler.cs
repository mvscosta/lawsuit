using AutoMapper;
using Mc2Tech.PersonsApi.DAL;
using Mc2Tech.PersonsApi.Model;
using Mc2Tech.PersonsApi.ViewModel.Get;
using Microsoft.EntityFrameworkCore;
using SimpleSoft.Mediator;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2Tech.PersonsApi.Handlers
{
    public class GetPersonIdExistsQueryHandler : IQueryHandler<GetPersonIdExistsQuery, bool>
    {
        private readonly IQueryable<PersonEntity> _persons;
        private readonly IMapper _mapper;

        public GetPersonIdExistsQueryHandler(ApiDbContext context, IMapper mapper)
        {
            _persons = context.Set<PersonEntity>();
            _mapper = mapper;
        }

        public async Task<bool> HandleAsync(GetPersonIdExistsQuery query, CancellationToken ct)
        {
            var filter = _persons.AnyAsync(p => p.Id == query.PersonId, ct);

            var result = await filter;

            return result;
        }
    }
}
