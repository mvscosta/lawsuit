using AutoMapper;
using Mc2Tech.LawSuitsApi.DAL;
using Mc2Tech.LawSuitsApi.Model.DALEntity;
using Mc2Tech.LawSuitsApi.ViewModel.LawSuits;
using Microsoft.EntityFrameworkCore;
using SimpleSoft.Mediator;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2Tech.LawSuitsApi.Handlers.LawSuits
{
    public class GetCountByResponsibleIdQueryHandler : IQueryHandler<GetCountByResponsibleIdQuery, int>
    {
        private readonly DbSet<LawSuitEntity> _lawSuits;
        private readonly IMapper _mapper;

        public GetCountByResponsibleIdQueryHandler(ApiDbContext context, IMapper mapper)
        {
            _lawSuits = context.Set<LawSuitEntity>();
            _mapper = mapper;
        }

        public async Task<int> HandleAsync(GetCountByResponsibleIdQuery query, CancellationToken ct)
        {
            var filter = _lawSuits
                .Include(a => a.LawSuitResponsibles)
                .Where(p => p.LawSuitResponsibles.Any(r => r.PersonId == query.ResponsibleId));

            var result = await filter
                .CountAsync(ct);

            return result;
        }
    }
}
