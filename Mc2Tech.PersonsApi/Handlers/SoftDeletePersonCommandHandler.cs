using AutoMapper;
using Mc2Tech.PersonsApi.DAL;
using Mc2Tech.PersonsApi.Model;
using Mc2Tech.PersonsApi.ViewModel.Delete;
using Microsoft.EntityFrameworkCore;
using SimpleSoft.Mediator;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2Tech.PersonsApi.Handlers
{
    public class SoftDeletePersonCommandHandler : ICommandHandler<SoftDeletePersonCommand, SoftDeletePersonResult>
    {
        private readonly ApiDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SoftDeletePersonCommandHandler(ApiDbContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<SoftDeletePersonResult> HandleAsync(SoftDeletePersonCommand cmd, CancellationToken ct)
        {
            var dbset = _context.Set<PersonEntity>();
            var entity = await dbset.FirstAsync(a => a.Id == cmd.Data.PersonId);
            entity.Status = Crosscutting.Enums.ObjectStatus.LogicalDeleted;

            dbset.Update(entity);

            await _mediator.BroadcastAsync(_mapper.Map<SoftDeletedPersonEvent>(entity), ct);

            await _context.SaveChangesAsync(ct);

            return new SoftDeletePersonResult
            {
                Id = entity.ExternalReference
            };
        }
    }
}

