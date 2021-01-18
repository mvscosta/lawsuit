using Mc2Tech.BaseApi.ViewModel.Audits;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleSoft.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2Tech.BaseApi.Controllers
{
    /// <summary>
    /// API Audits
    /// </summary>
    //[Authorize]
    //[ApiVersion("1.0")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    //[ApiController]
    public class BaseAuditsController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator"></param>
        public BaseAuditsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Search all audits added in the API
        /// </summary>
        /// <param name="filterQ"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<AuditSearchItemModel>> SearchAsync([FromQuery] string filterQ, [FromQuery] int? skip, [FromQuery] int? take, CancellationToken ct)
        {
            var result = await _mediator.FetchAsync(new SearchAuditsQuery
            {
                FilterQ = filterQ,
                Skip = skip,
                Take = take,
                CreatedBy = User.Identity.Name
            }, ct);

            return result.Select(r => new AuditSearchItemModel
            {
                Id = r.Id,
                Name = r.Name,
                CreatedOn = r.CreatedOn,
                CreatedBy = r.CreatedBy,
                ExecutionTimeInMs = r.ExecutionTimeInMs
            });
        }

        /// <summary>
        /// Get a specific audit by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public virtual async Task<AuditModel> GetByIdAsync([FromRoute] Guid id, CancellationToken ct)
        {
            var result = await _mediator.FetchAsync(new GetAuditByIdQuery
            {
                AuditId = id,
                CreatedBy = User.Identity.Name
            }, ct);

            return new AuditModel
            {
                Id = result.Id,
                Name = result.Name,
                Payload = result.Payload,
                Result = result.Result,
                CreatedOn = result.CreatedOn,
                CreatedBy = result.CreatedBy,
                ExecutionTimeInMs = result.ExecutionTimeInMs,
                Events = result.Events.Select(e => new AuditEventModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    Payload = e.Payload,
                    CreatedOn = e.CreatedOn
                })
            };
        }
    }
}
