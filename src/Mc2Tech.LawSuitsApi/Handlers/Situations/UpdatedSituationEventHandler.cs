using Mc2Tech.LawSuitsApi.DAL;
using Mc2Tech.LawSuitsApi.ViewModel.Situations;
using SimpleSoft.Mediator;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2Tech.LawSuitsApi.Handlers.Situations
{
    public class UpdatedSituationEventHandler : IEventHandler<UpdatedSituationEvent>
    {
        private readonly SituationDbContext _context;

        public UpdatedSituationEventHandler(SituationDbContext context)
        {
            _context = context;
        }

        public Task HandleAsync(UpdatedSituationEvent evt, CancellationToken ct)
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