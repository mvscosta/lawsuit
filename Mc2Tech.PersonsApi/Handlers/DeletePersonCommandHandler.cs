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
    public class DeletePersonCommandHandler : ICommandHandler<DeletePersonCommand, DeletePersonResult>
    {
        private readonly ApiDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public DeletePersonCommandHandler(ApiDbContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<DeletePersonResult> HandleAsync(DeletePersonCommand cmd, CancellationToken ct)
        {
            var dbset = _context.Set<PersonEntity>();
            var entity = await dbset.FirstAsync(a => a.Id == cmd.Data.PersonId);

            dbset.Remove(entity);

            await _mediator.BroadcastAsync(_mapper.Map<DeletedPersonEvent>(entity), ct);

            await _context.SaveChangesAsync(ct);

            return new DeletePersonResult
            {
                Id = entity.ExternalReference
            };
        }
    }
}

