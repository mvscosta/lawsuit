using Mc2Tech.Crosscutting.Interfaces.Persons;
using Mc2Tech.Crosscutting.Interfaces.ServiceClient;
using Mc2Tech.LawSuitsApi.DAL;
using Mc2Tech.LawSuitsApi.Model.DALEntity;
using Mc2Tech.LawSuitsApi.ViewModel.LawSuits;
using SimpleSoft.Mediator;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2Tech.LawSuitsApi.Handlers.LawSuits
{
    public class CreatedLawSuitEventHandler : IEventHandler<CreatedLawSuitEvent>
    {
        private readonly ApiDbContext _context;
        private readonly IPersonsApiServiceClient _personsApiServiceClient;

        public CreatedLawSuitEventHandler(ApiDbContext context, IPersonsApiServiceClient personsApiServiceClient)
        {
            _context = context;
            _personsApiServiceClient = personsApiServiceClient;
        }

        public async Task HandleAsync(CreatedLawSuitEvent evt, CancellationToken ct)
        {
            var dbset = _context.Set<LawSuitResponsibleEntity>();
            var httpPayload = new Crosscutting.Model.ServiceClient.HttpRequestPayloadDto { AccessToken = evt.AccessToken };
            StringBuilder sbEmail = new StringBuilder();
            foreach (var responsibleId in evt.LawSuitResponsibles)
            {
                dbset.Add(new LawSuitResponsibleEntity
                {
                    LawSuitId = evt.LawSuitId,
                    PersonId = responsibleId,
                    Status = Crosscutting.Enums.ObjectStatus.Active
                });

                IPersonDto personBasicInformation = await _personsApiServiceClient.GetPersonBasicInformationAsync(httpPayload, responsibleId, ct);

                sbEmail.AppendLine($"call Send Email {personBasicInformation.Email}");
                sbEmail.AppendLine($"Você foi cadastrado como envolvido no processo de número ${evt.UnifiedProcessNumber}");
            }

            await _context.SaveChangesAsync(ct);

            Console.WriteLine(sbEmail.ToString());
            //return _context.Set<EventEntity>().AddAsync(new EventEntity
            //{
            //    ExternalReference = evt.ExternalReference,
            //    Name = nameof(CreatedLawSuitEvent),
            //    Payload = JsonSerializer.Serialize(evt),
            //    CreatedOn = evt.CreatedOn,
            //    CreatedBy = evt.CreatedBy
            //}, ct);
        }
    }
}