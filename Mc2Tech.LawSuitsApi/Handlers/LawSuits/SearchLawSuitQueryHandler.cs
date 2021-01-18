using AutoMapper;
using AutoMapper.QueryableExtensions;
using Mc2Tech.Crosscutting.Interfaces.ServiceClient;
using Mc2Tech.LawSuitsApi.DAL;
using Mc2Tech.LawSuitsApi.Model.DALEntity;
using Mc2Tech.LawSuitsApi.Model.LawSuits;
using Mc2Tech.LawSuitsApi.ViewModel.LawSuits;
using Microsoft.EntityFrameworkCore;
using SimpleSoft.Mediator;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2Tech.LawSuitsApi.Handlers.LawSuits
{
    public class SearchLawSuitQueryHandler : IQueryHandler<SearchLawSuitsQuery, IEnumerable<LawSuit>>
    {
        private readonly IQueryable<LawSuitEntity> _lawSuits;
        private readonly IPersonsApiServiceClient _personsApiServiceClient;
        private readonly IMapper _mapper;

        public SearchLawSuitQueryHandler(ApiDbContext context, IMapper mapper, IPersonsApiServiceClient personsApiServiceClient)
        {
            _lawSuits = context.Set<LawSuitEntity>();
            _personsApiServiceClient = personsApiServiceClient;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LawSuit>> HandleAsync(SearchLawSuitsQuery query, CancellationToken ct)
        {
            var filter = _lawSuits;

            if (query.DistributedDateStart.HasValue)
                filter = filter.Where(p => p.DistributedDate >= query.DistributedDateStart);

            if (query.DistributedDateEnd.HasValue)
                filter = filter.Where(p => p.DistributedDate <= query.DistributedDateEnd);
            
            if (query.JusticeSecret.HasValue)
                filter = filter.Where(p => p.JusticeSecret == query.JusticeSecret);

            if (query.SituationId.HasValue)
                filter = filter.Where(p => p.SituationId == query.SituationId);

            if (!string.IsNullOrEmpty(query.ClientPhysicalFolder?.Trim()))
                filter = filter.Where(p => p.ClientPhysicalFolder.Contains(query.ClientPhysicalFolder));

            var unifiedProcessNumber = query.UnifiedProcessNumber?.Replace("-", string.Empty).Replace(".", string.Empty).Trim();
            if (!string.IsNullOrEmpty(unifiedProcessNumber))
                filter = filter.Where(p => p.UnifiedProcessNumber.Contains(unifiedProcessNumber));

            if (!string.IsNullOrEmpty(query.ResponsibleName?.Trim()))
            {
                var httpPayload = new Crosscutting.Model.ServiceClient.HttpRequestPayloadDto { AccessToken = query.AccessToken };
                var responsiblesIds = await _personsApiServiceClient.GetPersonIdsByPersonNameAsync(httpPayload, query.ResponsibleName, ct);

                filter = filter
                    .Where(p => p.LawSuitResponsibles
                        .Any(p => responsiblesIds
                            .Any(r => r == p.PersonId)
                        )
                    );
            }

            var skip = query.Skip ?? 0;
            var take = query.Take ?? 20;

            var result = await filter
                .AsNoTracking()
                .OrderBy(p => p.UnifiedProcessNumber)
                .Skip(skip)
                .Take(take)
                .ProjectTo<LawSuit>(configuration: _mapper.ConfigurationProvider)
                .ToListAsync(ct);

            return result;
        }
    }
}
