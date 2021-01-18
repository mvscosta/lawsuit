using AutoMapper;
using Mc2Tech.PersonsApi.DAL;
using Mc2Tech.PersonsApi.Model;
using Mc2Tech.PersonsApi.ViewModel.Create;
using SimpleSoft.Mediator;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2Tech.PersonsApi.Handlers
{
    public class CreatePersonCommandHandler : ICommandHandler<CreatePersonCommand, CreatePersonResult>
    {
        private readonly ApiDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CreatePersonCommandHandler(ApiDbContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<CreatePersonResult> HandleAsync(CreatePersonCommand cmd, CancellationToken ct)
        {
            var dbset = _context.Set<PersonEntity>();

            var entity = _mapper.Map<PersonEntity>(cmd.Data);

            await dbset.AddAsync(entity, ct);

            await _mediator.BroadcastAsync(_mapper.Map<CreatedPersonEvent>(entity), ct);

            await _context.SaveChangesAsync(ct);

            return new CreatePersonResult
            {
                Id = entity.Id
            };
        }
    }
}

