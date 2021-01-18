using Mc2Tech.BaseApi.ViewModel.Audits;
using Mc2Tech.Pipelines.Audit.DAL;
using Mc2Tech.Pipelines.Audit.Model.Audits;
using Microsoft.EntityFrameworkCore;
using SimpleSoft.Mediator;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2Tech.BaseApi.Handlers.Audits
{
    public class SearchAuditsQueryHandler : IQueryHandler<SearchAuditsQuery, IEnumerable<AuditSearchItem>>
    {
        private readonly IQueryable<CommandEntity> _commands;

        public SearchAuditsQueryHandler(AuditDbContext context)
        {
            _commands = context.Set<CommandEntity>();
        }

        public async Task<IEnumerable<AuditSearchItem>> HandleAsync(SearchAuditsQuery query, CancellationToken ct)
        {
            var filter = _commands;

            if (!string.IsNullOrWhiteSpace(query.FilterQ))
            {
                var filterQ = query.FilterQ.Trim();

                filter = filter.Where(p =>
                    p.Name.Contains(filterQ)
                );
            }

            var skip = query.Skip ?? 0;
            var take = query.Take ?? 20;

            return await filter
                .OrderByDescending(c => c.CreatedOn)
                .Skip(skip)
                .Take(take)
                .Select(c => new AuditSearchItem
                {
                    Id = c.Id,
                    ExternalReference = c.ExternalReference,
                    Name = c.Name,
                    CreatedOn = c.CreatedOn,
                    CreatedBy = c.CreatedBy,
                    ExecutionTimeInMs = (long)c.ExecutionTime.TotalMilliseconds
                }).ToListAsync(ct);
        }
    }
}