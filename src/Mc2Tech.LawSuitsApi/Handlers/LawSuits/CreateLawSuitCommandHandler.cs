using AutoMapper;
using Mc2Tech.LawSuitsApi.DAL;
using Mc2Tech.LawSuitsApi.Model.DALEntity;
using Mc2Tech.LawSuitsApi.ViewModel.LawSuits;
using SimpleSoft.Mediator;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2Tech.LawSuitsApi.Handlers.LawSuits
{
    public class CreateLawSuitCommandHandler : ICommandHandler<CreateLawSuitCommand, CreateLawSuitResult>
    {
        private readonly ApiDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CreateLawSuitCommandHandler(ApiDbContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<CreateLawSuitResult> HandleAsync(CreateLawSuitCommand cmd, CancellationToken ct)
        {
            var dbset = _context.Set<LawSuitEntity>();

            var entity = _mapper.Map<LawSuitEntity>(cmd.Data);

            await dbset.AddAsync(entity, ct);

            await _mediator.BroadcastAsync(_mapper.Map<CreatedLawSuitEvent>(entity), ct);

            await _context.SaveChangesAsync(ct);

            return new CreateLawSuitResult
            {
                Id = entity.Id
            };
        }
    }
}

