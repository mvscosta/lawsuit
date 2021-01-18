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
    public class DeleteLawSuitCommandHandler : ICommandHandler<DeleteLawSuitCommand, DeleteLawSuitResult>
    {
        private readonly ApiDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public DeleteLawSuitCommandHandler(ApiDbContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<DeleteLawSuitResult> HandleAsync(DeleteLawSuitCommand cmd, CancellationToken ct)
        {
            var dbset = _context.Set<LawSuitEntity>();
            var entity = await dbset.FirstAsync(a => a.Id == cmd.Data.LawSuitId);

            dbset.Remove(entity);

            await _mediator.BroadcastAsync(_mapper.Map<DeletedLawSuitEvent>(entity), ct);

            await _context.SaveChangesAsync(ct);

            return new DeleteLawSuitResult
            {
                Id = entity.ExternalReference
            };
        }
    }
}

