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
    public class GetPersonBasicInformationByIdQueryHandler : IQueryHandler<GetPersonBasicInformationByIdQuery, PersonBasicInformation>
    {
        private readonly IQueryable<PersonEntity> _persons;
        private readonly IMapper _mapper;

        public GetPersonBasicInformationByIdQueryHandler(ApiDbContext context, IMapper mapper)
        {
            _persons = context.Set<PersonEntity>();
            _mapper = mapper;
        }

        public async Task<PersonBasicInformation> HandleAsync(GetPersonBasicInformationByIdQuery query, CancellationToken ct)
        {
            var filter = _persons
                .Where(p => p.Id == query.PersonId);
                

            var result = await filter
                .ProjectTo<PersonBasicInformation>(configuration: _mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(ct);

            return result;
        }
    }
}
