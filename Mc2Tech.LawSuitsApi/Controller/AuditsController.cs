using AutoMapper;
using Mc2Tech.BaseApi.ViewModel.Audits;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleSoft.Mediator;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2Tech.LawSuitsApi.Controller
{
    /// <summary>
    /// Audits Endpoints
    /// </summary>
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AuditsController : Mc2Tech.BaseApi.Controllers.BaseAuditsController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="mapper"></param>
        public AuditsController(IMediator mediator, IMapper mapper)
            : base(mediator)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Search all audits
        /// </summary>
        /// <param name="filterQ"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpGet]
        public override async Task<IEnumerable<AuditSearchItemModel>> SearchAsync([FromQuery] string filterQ, [FromQuery] int? skip, [FromQuery] int? take, CancellationToken ct)
        {
            return await base.SearchAsync(filterQ, skip, take, ct);
        }

        /// <summary>
        /// Get a specific audit by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public override async Task<AuditModel> GetByIdAsync([FromRoute] Guid id, CancellationToken ct)
        {
            return await base.GetByIdAsync(id, ct);
        }
    }
}
