using AutoMapper;
using AutoMapper.QueryableExtensions;
using Mc2Tech.LawSuitsApi.DAL;
using Mc2Tech.LawSuitsApi.Model.DALEntity;
using Mc2Tech.LawSuitsApi.Model.LawSuits;
using Mc2Tech.LawSuitsApi.ViewModel.LawSuits;
using Microsoft.EntityFrameworkCore;
using SimpleSoft.Mediator;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2Tech.LawSuitsApi.Handlers.LawSuits
{
    public class GetLawSuitByUnifiedProcessNumberQueryHandler : IQueryHandler<GetLawSuitByUnifiedProcessNumberQuery, LawSuit>
    {
        private readonly IQueryable<LawSuitEntity> _lawSuits;
        private readonly IMapper _mapper;

        public GetLawSuitByUnifiedProcessNumberQueryHandler(ApiDbContext context, IMapper mapper)
        {
            _lawSuits = context.Set<LawSuitEntity>();
            _mapper = mapper;
        }

        public async Task<LawSuit> HandleAsync(GetLawSuitByUnifiedProcessNumberQuery query, CancellationToken ct)
        {
            var unifiedProcessNumber = query.UnifiedProcessNumber.Replace(".", string.Empty).Replace("-", string.Empty);

            var filter = _lawSuits.Where(p => p.UnifiedProcessNumber == unifiedProcessNumber);

            var result = await filter
                .ProjectTo<LawSuit>(configuration: _mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(ct);

            return result;
        }
    }
}
