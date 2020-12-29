using Mc2Tech.BaseApi.Handlers.Audits;
using Mc2Tech.BaseApi.ViewModel.Audits;
using Mc2Tech.Pipelines.Audit.Model.Audits;
using Microsoft.Extensions.DependencyInjection;
using SimpleSoft.Mediator;
using System.Collections.Generic;

namespace Mc2Tech.BaseApi
{
    public static class Mc2TechBaseApiInjectionsExtension
    {
        public static IServiceCollection Mc2TechBaseApiInjections(this IServiceCollection services)
        {
            services.AddTransient<IQueryHandler<SearchAuditsQuery, IEnumerable<AuditSearchItem>>, SearchAuditsQueryHandler>();
            services.AddTransient<IQueryHandler<GetAuditByIdQuery, Audit>, GetAuditByIdQueryHandler> ();
            return services;
        }
    }
}
