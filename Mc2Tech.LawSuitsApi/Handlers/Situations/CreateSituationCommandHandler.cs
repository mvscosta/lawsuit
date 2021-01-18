using AutoMapper;
using Mc2Tech.LawSuitsApi.DAL;
using Mc2Tech.LawSuitsApi.Model.DALEntity;
using Mc2Tech.LawSuitsApi.ViewModel.Situations;
using SimpleSoft.Mediator;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2Tech.LawSuitsApi.Handlers.Situations
{
    public class CreateSituationCommandHandler : ICommandHandler<CreateSituationCommand, CreateSituationResult>
    {
        private readonly SituationDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CreateSituationCommandHandler(SituationDbContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<CreateSituationResult> HandleAsync(CreateSituationCommand cmd, CancellationToken ct)
        {
            var dbset = _context.Set<SituationEntity>();

            var entity = _mapper.Map<SituationEntity>(cmd.Data);

            await dbset.AddAsync(entity, ct);

            await _mediator.BroadcastAsync(_mapper.Map<CreatedSituationEvent>(entity), ct);

            await _context.SaveChangesAsync(ct);

            return new CreateSituationResult
            {
                Id = entity.Id
            };
        }
    }
}

