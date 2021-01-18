using AutoMapper;
using Mc2Tech.LawSuitsApi.DAL;
using Mc2Tech.LawSuitsApi.Model.DALEntity;
using Mc2Tech.LawSuitsApi.ViewModel.Situations;
using Microsoft.EntityFrameworkCore;
using SimpleSoft.Mediator;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2Tech.LawSuitsApi.Handlers.Situations
{
    public class UpdateSituationCommandHandler : ICommandHandler<UpdateSituationCommand, UpdateSituationResult>
    {
        private readonly SituationDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UpdateSituationCommandHandler(SituationDbContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<UpdateSituationResult> HandleAsync(UpdateSituationCommand cmd, CancellationToken ct)
        {
            var dbset = _context.Set<SituationEntity>();
            var entity = await dbset.AsNoTracking().FirstAsync(a=>a.Id == cmd.Data.Id);

            _mapper.Map(cmd.Data, entity);

            dbset.Update(entity);

            await _mediator.BroadcastAsync(_mapper.Map<UpdatedSituationEvent>(entity), ct);

            await _context.SaveChangesAsync(ct);

            return new UpdateSituationResult
            {
                Id = entity.ExternalReference
            };
        }
    }
}

