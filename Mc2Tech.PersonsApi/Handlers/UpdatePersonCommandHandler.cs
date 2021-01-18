using AutoMapper;
using Mc2Tech.PersonsApi.DAL;
using Mc2Tech.PersonsApi.Model;
using Mc2Tech.PersonsApi.ViewModel.Update;
using Microsoft.EntityFrameworkCore;
using SimpleSoft.Mediator;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2Tech.PersonsApi.Handlers
{
    public class UpdatePersonCommandHandler : ICommandHandler<UpdatePersonCommand, UpdatePersonResult>
    {
        private readonly ApiDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UpdatePersonCommandHandler(ApiDbContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<UpdatePersonResult> HandleAsync(UpdatePersonCommand cmd, CancellationToken ct)
        {
            var dbset = _context.Set<PersonEntity>();
            var entity = await dbset.AsNoTracking().FirstAsync(a=>a.Id == cmd.Data.Id);

            _mapper.Map(cmd.Data, entity);

            dbset.Update(entity);

            await _mediator.BroadcastAsync(_mapper.Map<UpdatedPersonEvent>(entity), ct);

            await _context.SaveChangesAsync(ct);

            return new UpdatePersonResult
            {
                Id = entity.ExternalReference
            };
        }
    }
}

