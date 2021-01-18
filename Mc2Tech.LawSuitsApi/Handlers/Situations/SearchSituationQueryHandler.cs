using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCoreSecondLevelCacheInterceptor;
using Mc2Tech.LawSuitsApi.DAL;
using Mc2Tech.LawSuitsApi.Model.DALEntity;
using Mc2Tech.LawSuitsApi.Model.Situations;
using Mc2Tech.LawSuitsApi.ViewModel.LawSuits;
using Microsoft.EntityFrameworkCore;
using SimpleSoft.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2Tech.LawSuitsApi.Handlers.LawSuits
{
    public class SearchSituationQueryHandler : IQueryHandler<SearchSituationsQuery, IEnumerable<Situation>>
    {
        private readonly IQueryable<SituationEntity> _entityQueryable;
        private readonly IMapper _mapper;

        public SearchSituationQueryHandler(SituationDbContext context, IMapper mapper)
        {
            _entityQueryable = context.Set<SituationEntity>();
            _mapper = mapper;
        }

        public async Task<IEnumerable<Situation>> HandleAsync(SearchSituationsQuery query, CancellationToken ct)
        {
            var filter = _entityQueryable;

            if (!string.IsNullOrWhiteSpace(query.FilterQ))
            {
                var filterQ = query.FilterQ.Trim();

                filter = filter.Where(p =>
                    p.Name.Contains(filterQ)
                );
            }

            var skip = query.Skip ?? 0;
            var take = query.Take ?? 20;

            var result = await filter
                .OrderBy(p => p.Name)
                .Skip(skip)
                .Take(take)
                .Cacheable(CacheExpirationMode.Absolute, TimeSpan.FromHours(12))
                .ProjectTo<Situation>(configuration: _mapper.ConfigurationProvider)
                .ToListAsync(ct);

            return result;
        }
    }
}
