using AutoMapper;
using Mc2Tech.LawSuitsApi.DAL;
using Mc2Tech.LawSuitsApi.Model.DALEntity;
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
    public class GetResponsibleIdsByUnifiedProcessNumberQueryHandler : IQueryHandler<GetResponsibleIdsByUnifiedProcessNumberQuery, IEnumerable<Guid>>
    {
        private readonly DbSet<LawSuitEntity> _lawSuits;
        private readonly IMapper _mapper;

        public GetResponsibleIdsByUnifiedProcessNumberQueryHandler(ApiDbContext context, IMapper mapper)
        {
            _lawSuits = context.Set<LawSuitEntity>();
            _mapper = mapper;
        }

        public async Task<IEnumerable<Guid>> HandleAsync(GetResponsibleIdsByUnifiedProcessNumberQuery query, CancellationToken ct)
        {
            var unifiedProcessNumber = query.UnifiedProcessNumber.Replace(".", string.Empty).Replace("-", string.Empty).Trim();

            var filter = _lawSuits
                .Include(a => a.LawSuitResponsibles)
                .Where(p => p.UnifiedProcessNumber == unifiedProcessNumber);

            var result = await filter
                .SelectMany(a => a.LawSuitResponsibles.Select(r => r.PersonId))
                .Distinct()
                .ToListAsync(ct);

            return result;
        }
    }
}
