using AutoMapper;
using Mc2Tech.LawSuitsApi.DAL;
using Mc2Tech.LawSuitsApi.Model.DALEntity;
using Mc2Tech.LawSuitsApi.ViewModel.LawSuits;
using Microsoft.EntityFrameworkCore;
using SimpleSoft.Mediator;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2Tech.LawSuitsApi.Handlers.LawSuits
{
    public class UpdateLawSuitCommandHandler : ICommandHandler<UpdateLawSuitCommand, UpdateLawSuitResult>
    {
        private readonly ApiDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UpdateLawSuitCommandHandler(ApiDbContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<UpdateLawSuitResult> HandleAsync(UpdateLawSuitCommand cmd, CancellationToken ct)
        {
            var dbset = _context.Set<LawSuitEntity>();
            var entity = await dbset.AsNoTracking().FirstAsync(a => a.Id == cmd.Data.Id);

            var updated = _mapper.Map(cmd.Data, entity);

            dbset.Attach(updated);
            dbset.Update(entity);

            var updatedEvent = _mapper.Map<UpdatedLawSuitEvent>(cmd.Data);
            updatedEvent.AccessToken = cmd.AccessToken;

            await _mediator.BroadcastAsync(updatedEvent, ct);

            await _context.SaveChangesAsync(ct);

            return new UpdateLawSuitResult
            {
                Id = entity.Id
            };
        }
    }
}

