using AutoMapper;
using AutoMapper.QueryableExtensions;
using Mc2Tech.LawSuitsApi.DAL;
using Mc2Tech.LawSuitsApi.Model.DALEntity;
using Mc2Tech.LawSuitsApi.Model.Situations;
using Mc2Tech.LawSuitsApi.ViewModel.LawSuits;
using Microsoft.EntityFrameworkCore;
using SimpleSoft.Mediator;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2Tech.LawSuitsApi.Handlers.LawSuits
{
    public class GetSituationByIdQueryHandler : IQueryHandler<GetSituationByIdQuery, Situation>
    {
        private readonly IQueryable<SituationEntity> _entityQueryable;
        private readonly IMapper _mapper;

        public GetSituationByIdQueryHandler(SituationDbContext context, IMapper mapper)
        {
            _entityQueryable = context.Set<SituationEntity>();
            _mapper = mapper;
        }

        public async Task<Situation> HandleAsync(GetSituationByIdQuery query, CancellationToken ct)
        {
            var filter = _entityQueryable.Where(p => p.Id == query.Id);

            var result = await filter
                .ProjectTo<Situation>(configuration: _mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(ct);

            return result;
        }
    }
}
