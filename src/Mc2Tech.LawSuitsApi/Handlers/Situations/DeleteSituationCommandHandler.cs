using AutoMapper;
using Mc2Tech.LawSuitsApi.DAL;
using Mc2Tech.LawSuitsApi.Model.DALEntity;
using Mc2Tech.LawSuitsApi.ViewModel.Situations;
using Microsoft.EntityFrameworkCore;
using SimpleSoft.Mediator;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2Tech.LawSuitsApi.Handlers.Situations
{
    public class DeleteSituationCommandHandler : ICommandHandler<DeleteSituationCommand, DeleteSituationResult>
    {
        private readonly SituationDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public DeleteSituationCommandHandler(SituationDbContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<DeleteSituationResult> HandleAsync(DeleteSituationCommand cmd, CancellationToken ct)
        {
            var dbset = _context.Set<SituationEntity>();
            var entity = await dbset.FirstAsync(a=>a.Id == cmd.Data.Id);

            dbset.Remove(entity);

            await _mediator.BroadcastAsync(_mapper.Map<DeletedSituationEvent>(entity), ct);

            await _context.SaveChangesAsync(ct);

            return new DeleteSituationResult
            {
                Id = entity.ExternalReference
            };
        }
    }
}

