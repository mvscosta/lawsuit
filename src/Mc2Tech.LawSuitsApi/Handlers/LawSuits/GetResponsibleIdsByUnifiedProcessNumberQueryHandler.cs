using AutoMapper;
using Mc2Tech.LawSuitsApi.DAL;
using Mc2Tech.LawSuitsApi.Model.DALEntity;
using Mc2Tech.LawSuitsApi.ViewModel.LawSuits;
using SimpleSoft.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2Tech.LawSuitsApi.Handlers.LawSuits
{
    public class GetResponsibleIdsByUnifiedProcessNumberQueryHandler : IQueryHandler<GetResponsibleIdsByUnifiedProcessNumberQuery, IEnumerable<Guid>>
    {
        private readonly IQueryable<LawSuitEntity> _lawSuits;
        private readonly IMapper _mapper;

        public GetResponsibleIdsByUnifiedProcessNumberQueryHandler(ApiDbContext context, IMapper mapper)
        {
            _lawSuits = context.Set<LawSuitEntity>();
            _mapper = mapper;
        }

        public async Task<IEnumerable<Guid>> HandleAsync(GetResponsibleIdsByUnifiedProcessNumberQuery query, CancellationToken ct)
        {
            return null;
            //var filter = _lawSuits
            //    .Include(a => a.Responsibles)
            //    .Where(p => p.UnifiedProcessNumber == query.UnifiedProcessNumber);

            //var result = await filter
            //    .SelectMany(a => a.Responsibles.Select(r => r.Id))
            //    .Distinct()
            //    .ToListAsync(ct);

            //return result;
        }
    }
}
