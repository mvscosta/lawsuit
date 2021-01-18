using Mc2Tech.LawSuitsApi.DAL;
using Mc2Tech.LawSuitsApi.ViewModel.Situations;
using SimpleSoft.Mediator;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2Tech.LawSuitsApi.Handlers.Situations
{
    public class CreatedSituationEventHandler : IEventHandler<CreatedSituationEvent>
    {
        private readonly SituationDbContext _context;

        public CreatedSituationEventHandler(SituationDbContext context)
        {
            _context = context;
        }

        public Task HandleAsync(CreatedSituationEvent evt, CancellationToken ct)
        {
            //return _context.Set<EventEntity>().AddAsync(new EventEntity
            //{
            //    ExternalReference = evt.ExternalReference,
            //    Name = nameof(CreatedSituationEvent),
            //    Payload = JsonSerializer.Serialize(evt),
            //    CreatedOn = evt.CreatedOn,
            //    CreatedBy = evt.CreatedBy
            //}, ct);
            return Task.CompletedTask;
        }
    }
}