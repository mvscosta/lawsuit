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
    public class GetLawSuitByIdQueryHandler : IQueryHandler<GetLawSuitByIdQuery, LawSuit>
    {
        private readonly IQueryable<LawSuitEntity> _lawSuits;
        private readonly IMapper _mapper;

        public GetLawSuitByIdQueryHandler(ApiDbContext context, IMapper mapper)
        {
            _lawSuits = context.Set<LawSuitEntity>();
            _mapper = mapper;
        }

        public async Task<LawSuit> HandleAsync(GetLawSuitByIdQuery query, CancellationToken ct)
        {
            var filter = _lawSuits.Where(p => p.Id == query.LawSuitId);

            var result = await filter
                .ProjectTo<LawSuit>(configuration: _mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(ct);

            return result;
        }
    }
}
