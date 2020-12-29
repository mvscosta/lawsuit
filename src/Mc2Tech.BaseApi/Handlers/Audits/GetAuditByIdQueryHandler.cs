using Mc2Tech.BaseApi.ViewModel.Audits;
using Mc2Tech.Pipelines.Audit.DAL;
using Mc2Tech.Pipelines.Audit.Model.Audits;
using Microsoft.EntityFrameworkCore;
using SimpleSoft.Mediator;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2Tech.BaseApi.Handlers.Audits
{
    public class GetAuditByIdQueryHandler : IQueryHandler<GetAuditByIdQuery, Audit>
    {
        private readonly IQueryable<CommandEntity> _commands;
        private readonly IQueryable<EventEntity> _events;

        public GetAuditByIdQueryHandler(AuditDbContext context)
        {
            _commands = context.Set<CommandEntity>();
            _events = context.Set<EventEntity>();
        }

        public async Task<Audit> HandleAsync(GetAuditByIdQuery query, CancellationToken ct)
        {
            var command = await _commands.SingleOrDefaultAsync(c => c.ExternalReference == query.AuditId, ct);

            if (command == null)
            {
                throw new InvalidOperationException($"Command audit '{query.AuditId}' not found");
            }

            var events = await _events.Where(e => e.CommandId == command.Id).ToListAsync(ct);

            return new Audit
            {
                Id = command.ExternalReference,
                Name = command.Name,
                Payload = JsonSerializer.Deserialize<dynamic>(command.Payload),
                Result = string.IsNullOrWhiteSpace(command.Result)
                    ? null
                    : JsonSerializer.Deserialize<dynamic>(command.Result),
                CreatedOn = command.CreatedOn,
                CreatedBy = command.CreatedBy,
                ExecutionTimeInMs = (long)command.ExecutionTime.TotalMilliseconds,
                Events = events.Select(e => new AuditEvent
                {
                    Id = e.ExternalReference,
                    Name = e.Name,
                    Payload = JsonSerializer.Deserialize<dynamic>(e.Payload),
                    CreatedOn = e.CreatedOn
                }).ToList()
            };
        }
    }
}