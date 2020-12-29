using Mc2Tech.LawSuitsApi.DAL;
using Mc2Tech.LawSuitsApi.ViewModel.LawSuits;
using SimpleSoft.Mediator;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2Tech.LawSuitsApi.Handlers.LawSuits
{
    public class CreatedLawSuitEventHandler : IEventHandler<CreatedLawSuitEvent>
    {
        private readonly ApiDbContext _context;

        public CreatedLawSuitEventHandler(ApiDbContext context)
        {
            _context = context;
        }

        public Task HandleAsync(CreatedLawSuitEvent evt, CancellationToken ct)
        {
            //return _context.Set<EventEntity>().AddAsync(new EventEntity
            //{
            //    ExternalReference = evt.ExternalReference,
            //    Name = nameof(CreatedLawSuitEvent),
            //    Payload = JsonSerializer.Serialize(evt),
            //    CreatedOn = evt.CreatedOn,
            //    CreatedBy = evt.CreatedBy
            //}, ct);
            return Task.CompletedTask;
        }
    }
}