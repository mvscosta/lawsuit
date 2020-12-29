using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    public class GetLawSuitsByParentIdQueryHandler : IQueryHandler<GetLawSuitsByParentIdQuery, IEnumerable<LawSuit>>
    {
        private readonly IQueryable<LawSuitEntity> _lawSuits;
        private readonly IMapper _mapper;

        public GetLawSuitsByParentIdQueryHandler(ApiDbContext context, IMapper mapper)
        {
            _lawSuits = context.Set<LawSuitEntity>();
            _mapper = mapper;
        }

        public async Task<IEnumerable<LawSuit>> HandleAsync(GetLawSuitsByParentIdQuery query, CancellationToken ct)
        {
            var filter = _lawSuits;

            filter = filter.Where(p =>
                p.ParentLawSuitId == query.ParentId
            );

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
