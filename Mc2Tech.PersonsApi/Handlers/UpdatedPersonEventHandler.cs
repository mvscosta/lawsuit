using Mc2Tech.PersonsApi.DAL;
using Mc2Tech.PersonsApi.ViewModel.Update;
using SimpleSoft.Mediator;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2Tech.Handlers.LawSuits
{
    public class UpdatedPersonEventHandler : IEventHandler<UpdatedPersonEvent>
    {
        public UpdatedPersonEventHandler()
        {
        }

        public Task HandleAsync(UpdatedPersonEvent evt, CancellationToken ct)
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