using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    public class GetPersonBasicInformationByIdQueryHandler : IQueryHandler<GetPersonBasicInformationByIdQuery, Person>
    {
        private readonly IQueryable<PersonEntity> _lawSuits;
        private readonly IMapper _mapper;

        public GetPersonBasicInformationByIdQueryHandler(ApiDbContext context, IMapper mapper)
        {
            _lawSuits = context.Set<PersonEntity>();
            _mapper = mapper;
        }

        public async Task<Person> HandleAsync(GetPersonBasicInformationByIdQuery query, CancellationToken ct)
        {
            var filter = _lawSuits
                .Where(p => p.Id == query.PersonId);
                

            var result = await filter
                .ProjectTo<Person>(configuration: _mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(ct);

            return result;
        }
    }
}
