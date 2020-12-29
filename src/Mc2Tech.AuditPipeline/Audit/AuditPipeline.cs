using Mc2Tech.Pipelines.Audit.DAL;
using Mc2Tech.Pipelines.Audit.Model.Audits;
using Microsoft.EntityFrameworkCore;
using SimpleSoft.Mediator;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2Tech.Pipelines.Audit
{
    public class AuditPipeline : Pipeline
    {
        private readonly AuditDbContext _context;
        private readonly DbSet<CommandEntity> _commands;
        private readonly DbSet<EventEntity> _events;

        public AuditPipeline(AuditDbContext context)
        {
            _commands = context.Set<CommandEntity>();
            _events = context.Set<EventEntity>();
            _context = context;
        }

        public override async Task OnCommandAsync<TCommand>(Func<TCommand, CancellationToken, Task> next, TCommand cmd, CancellationToken ct)
        {
            var command = (await _commands.AddAsync(new CommandEntity
            {
                ExternalReference = cmd.Id,
                Name = typeof(TCommand).Name,
                Payload = JsonSerializer.Serialize(cmd),
                Result = null,
                CreatedOn = cmd.CreatedOn,
                CreatedBy = cmd.CreatedBy,
                ExecutionTime = TimeSpan.Zero
            }, ct)).Entity;

            await _context.SaveChangesAsync();

            using (CommandScope.Begin(command.ExternalReference, command.Id))
            {
                await next(cmd, ct);
            }

            command.ExecutionTime = DateTimeOffset.UtcNow - cmd.CreatedOn;
        }

        public override async Task<TResult> OnCommandAsync<TCommand, TResult>(Func<TCommand, CancellationToken, Task<TResult>> next, TCommand cmd, CancellationToken ct)
        {
            var command = (await _commands.AddAsync(new CommandEntity
            {
                ExternalReference = cmd.Id,
                Name = typeof(TCommand).Name,
                Payload = JsonSerializer.Serialize(cmd),
                Result = null,
                CreatedOn = cmd.CreatedOn,
                CreatedBy = cmd.CreatedBy,
                ExecutionTime = TimeSpan.Zero
            }, ct)).Entity;

            TResult result;

            await _context.SaveChangesAsync();

            using (CommandScope.Begin(command.ExternalReference, command.Id))
            {
                result = await next(cmd, ct);
            }

            command.Result = result == null ? null : JsonSerializer.Serialize(result);
            command.ExecutionTime = DateTimeOffset.UtcNow - cmd.CreatedOn;

            return result;
        }

        public override async Task OnEventAsync<TEvent>(Func<TEvent, CancellationToken, Task> next, TEvent evt, CancellationToken ct)
        {
            await _events.AddAsync(new EventEntity
            {
                CommandId = CommandScope.Current.Id,
                ExternalReference = evt.Id,
                Name = typeof(TEvent).Name,
                Payload = JsonSerializer.Serialize(evt),
                CreatedOn = evt.CreatedOn,
                CreatedBy = evt.CreatedBy,
            }, ct);

            await _context.SaveChangesAsync();

            await next(evt, ct);
        }

        private class CommandScope : IDisposable
        {
            private CommandScope(Guid externalId, Guid id)
            {
                ExternalId = externalId;
                Id = id;

                AsyncLocal.Value = this;
            }

            public Guid ExternalId { get; }

            public Guid Id { get; }

            public void Dispose()
            {
                AsyncLocal.Value = null;
            }

            private static readonly AsyncLocal<CommandScope> AsyncLocal = new AsyncLocal<CommandScope>();

            public static CommandScope Current => AsyncLocal.Value;

            public static IDisposable Begin(Guid externalId, Guid id) => new CommandScope(externalId, id);
        }
    }
}
