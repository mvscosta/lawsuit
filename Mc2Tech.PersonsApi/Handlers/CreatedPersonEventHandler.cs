using Mc2Tech.PersonsApi.DAL;
using Mc2Tech.PersonsApi.ViewModel.Create;
using SimpleSoft.Mediator;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2Tech.PersonsApi.Handlers
{
    public class CreatedPersonEventHandler : IEventHandler<CreatedPersonEvent>
    {
        public CreatedPersonEventHandler()
        {
        }

        public Task HandleAsync(CreatedPersonEvent evt, CancellationToken ct)
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