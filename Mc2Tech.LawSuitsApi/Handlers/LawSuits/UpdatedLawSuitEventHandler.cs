using Mc2Tech.Crosscutting.Interfaces.Persons;
using Mc2Tech.Crosscutting.Interfaces.ServiceClient;
using Mc2Tech.LawSuitsApi.DAL;
using Mc2Tech.LawSuitsApi.Model.DALEntity;
using Mc2Tech.LawSuitsApi.ViewModel.LawSuits;
using Microsoft.EntityFrameworkCore;
using SimpleSoft.Mediator;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2Tech.LawSuitsApi.Handlers.LawSuits
{
    public class UpdatedLawSuitEventHandler : IEventHandler<UpdatedLawSuitEvent>
    {
        private readonly ApiDbContext _apiDbContext;
        private readonly IPersonsApiServiceClient _personsApiServiceClient;

        public UpdatedLawSuitEventHandler(ApiDbContext apiDbContext, IPersonsApiServiceClient personsApiServiceClient)
        {
            _apiDbContext = apiDbContext;
            _personsApiServiceClient = personsApiServiceClient;
        }

        public async Task HandleAsync(UpdatedLawSuitEvent evt, CancellationToken ct)
        {
            if (evt.LawSuitResponsibles != null && evt.LawSuitResponsibles.Any())
            {
                var httpPayload = new Crosscutting.Model.ServiceClient.HttpRequestPayloadDto { AccessToken = evt.AccessToken };
                StringBuilder sbEmail = new StringBuilder();
                foreach (var responsible in evt.LawSuitResponsibles.Where(p => !p.Id.HasValue))
                {
                    IPersonDto personBasicInformation = await _personsApiServiceClient.GetPersonBasicInformationAsync(httpPayload, responsible.PersonId, ct);

                    var unifiedProcessNumber = await _apiDbContext.Set<LawSuitEntity>().Where(p => p.Id == responsible.LawSuitId).Select(p => p.UnifiedProcessNumber).FirstOrDefaultAsync(ct);

                    sbEmail.AppendLine($"call Send Email {personBasicInformation.Email}");
                    sbEmail.AppendLine($"Você foi cadastrado como envolvido no processo de número ${unifiedProcessNumber}");
                }

                Console.WriteLine(sbEmail.ToString());
            }
            //return await _context.Set<EventEntity>().AddAsync(new EventEntity
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