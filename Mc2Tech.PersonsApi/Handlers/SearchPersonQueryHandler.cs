using AutoMapper;
using AutoMapper.QueryableExtensions;
using Mc2Tech.Crosscutting.Interfaces.ServiceClient;
using Mc2Tech.PersonsApi.DAL;
using Mc2Tech.PersonsApi.Model;
using Mc2Tech.PersonsApi.ViewModel.Get;
using Microsoft.EntityFrameworkCore;
using SimpleSoft.Mediator;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2Tech.PersonsApi.Handlers
{
    public class SearchPersonQueryHandler : IQueryHandler<SearchPersonsQuery, IEnumerable<Person>>
    {
        private readonly IQueryable<PersonEntity> _lawSuits;
        private readonly ILawSuitsApiServiceClient _lawSuitsApiServiceClient;
        private readonly IMapper _mapper;

        public SearchPersonQueryHandler(ApiDbContext context, IMapper mapper, ILawSuitsApiServiceClient lawSuitsApiServiceClient)
        {
            _lawSuits = context.Set<PersonEntity>();
            _lawSuitsApiServiceClient = lawSuitsApiServiceClient;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Person>> HandleAsync(SearchPersonsQuery query, CancellationToken ct)
        {
            var filter = _lawSuits;

            if (!string.IsNullOrEmpty(query.Name))
                filter = filter.Where(p => p.Name.Contains(query.Name));

            if (!string.IsNullOrEmpty(query.Cpf))
                filter = filter.Where(p => p.Cpf.Equals(query.Cpf));

            if (!string.IsNullOrEmpty(query.UnifiedProcessNumber))
            {
                var httpPayload = new Crosscutting.Model.ServiceClient.HttpRequestPayloadDto { AccessToken = query.AccessToken };
                var responsibleIds = await _lawSuitsApiServiceClient.GetResponsibleIdsByUnifiedProcessNumberAsync(httpPayload, query.UnifiedProcessNumber, ct);

                filter = filter.Where(p => p.Id.HasValue && responsibleIds.Contains(p.Id.Value));
            }

            var skip = query.Skip ?? 0;
            var take = query.Take ?? 20;

            var result = await filter
                .AsNoTracking()
                .OrderBy(p => p.Name)
                .Skip(skip)
                .Take(take)
                .ProjectTo<Person>(configuration: _mapper.ConfigurationProvider)
                .ToListAsync(ct);

            return result;
        }
    }
}
