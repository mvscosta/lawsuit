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
    public class GetResponsibleIdsByLawSuitIdQueryHandler : IQueryHandler<GetResponsibleIdsByLawSuitIdQuery, IEnumerable<Guid>>
    {
        private readonly DbSet<LawSuitEntity> _lawSuits;
        private readonly IMapper _mapper;

        public GetResponsibleIdsByLawSuitIdQueryHandler(ApiDbContext context, IMapper mapper)
        {
            _lawSuits = context.Set<LawSuitEntity>();
            _mapper = mapper;
        }

        public async Task<IEnumerable<Guid>> HandleAsync(GetResponsibleIdsByLawSuitIdQuery query, CancellationToken ct)
        {
            var filter = _lawSuits
                .Include(a => a.LawSuitResponsibles)
                .Where(p => p.Id == query.LawSuitId);

            var result = await filter
                .SelectMany(a => a.LawSuitResponsibles.Select(r => r.PersonId))
                .Distinct()
                .ToListAsync(ct);

            return result;
        }
    }
}
