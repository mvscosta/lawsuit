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
    /// <summary>
    /// Get basic law suit informations
    /// ID, UNIFIED PROCESS NUMBER, SITUATIONID
    /// </summary>
    public class GetLawSuitsBasicInformationByResponsibleIdQueryHandler : IQueryHandler<GetLawSuitsBasicInformationByResponsibleIdQuery, List<LawSuit>>
    {
        private readonly DbSet<LawSuitEntity> _lawSuits;
        private readonly IMapper _mapper;

        public GetLawSuitsBasicInformationByResponsibleIdQueryHandler(ApiDbContext context, IMapper mapper)
        {
            _lawSuits = context.Set<LawSuitEntity>();
            _mapper = mapper;
        }

        public async Task<List<LawSuit>> HandleAsync(GetLawSuitsBasicInformationByResponsibleIdQuery query, CancellationToken ct)
        {
            var filter = _lawSuits
                .Include(a => a.LawSuitResponsibles)
                .Where(p => p.LawSuitResponsibles.Any(r => r.PersonId == query.ResponsibleId));

            var result = await filter
                .Select(o => new LawSuit
                {
                    Id = o.Id,
                    UnifiedProcessNumber = o.UnifiedProcessNumber,
                    SituationId = o.SituationId
                })
                .ToListAsync(ct);

            return result;
        }
    }
}
