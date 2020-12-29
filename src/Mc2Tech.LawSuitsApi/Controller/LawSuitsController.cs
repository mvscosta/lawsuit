using AutoMapper;
using Mc2Tech.LawSuitsApi.ViewModel.LawSuits;
using Microsoft.AspNetCore.Mvc;
using SimpleSoft.Mediator;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2Tech.LawSuitsApi.Controller
{
    /// <summary>
    /// Law Suits endpoints
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class LawSuitsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="mapper"></param>
        public LawSuitsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Basic search method
        /// </summary>
        /// <param name="distributedDateStart"></param>
        /// <param name="distributedDateEnd"></param>
        /// <param name="justiceSecret"></param>
        /// <param name="situationId"></param>
        /// <param name="clientFolder"></param>
        /// <param name="responsibleName"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<LawSuitModel>> SearchAsync(
            [FromQuery] DateTime? distributedDateStart, [FromQuery] DateTime? distributedDateEnd,
            [FromQuery] bool? justiceSecret,
            [FromQuery] Guid? situationId,
            [FromQuery] string clientFolder, [FromQuery] string responsibleName,
            [FromQuery] int? skip, [FromQuery] int? take, CancellationToken ct
        )
        {
            var result = await _mediator.FetchAsync(new SearchLawSuitsQuery
            {
                DistributedDateStart = distributedDateStart,
                DistributedDateEnd = distributedDateEnd,
                JusticeSecret = justiceSecret,
                SituationId = situationId,
                ClientPhysicalFolder = clientFolder,
                ResponsibleName = responsibleName,
                Skip = skip,
                Take = take,
                CreatedBy = User.Identity.Name
            }, ct);

            return _mapper.Map<IEnumerable<LawSuitModel>>(result);
        }

        /// <summary>
        /// Get Law Suits by parent id
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpGet("GetByParentId")]
        public async Task<IEnumerable<LawSuitModel>> GetByParentIdAsync([FromQuery] Guid parentId, [FromQuery] int? skip, [FromQuery] int? take, CancellationToken ct)
        {
            var result = await _mediator.FetchAsync(new GetLawSuitsByParentIdQuery
            {
                ParentId = parentId,
                Skip = skip,
                Take = take,
                CreatedBy = User.Identity.Name
            }, ct);

            return _mapper.Map<IEnumerable<LawSuitModel>>(result);
        }

        /// <summary>
        /// Get Law Suit by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<LawSuitModel> GetByIdAsync([FromRoute] Guid id, CancellationToken ct)
        {
            var result = await _mediator.FetchAsync(new GetLawSuitByIdQuery
            {
                LawSuitId = id,
                CreatedBy = User.Identity.Name
            }, ct);

            return _mapper.Map<LawSuitModel>(result);
        }

        /// <summary>
        /// Get Law Suit by Unified Process Number
        /// </summary>
        /// <param name="unifiedProcessNumber"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpGet("{unifiedProcessNumber}")]
        public async Task<LawSuitModel> GetByUnifiedProcessNumberAsync([FromRoute] string unifiedProcessNumber, CancellationToken ct)
        {
            var result = await _mediator.FetchAsync(new GetLawSuitByUnifiedProcessNumberQuery
            {
                UnifiedProcessNumber = unifiedProcessNumber,
                CreatedBy = User.Identity.Name
            }, ct);

            return _mapper.Map<LawSuitModel>(result);
        }

        /// <summary>
        /// Get Responsible Ids by Unified Process Number
        /// </summary>
        /// <param name="unifiedProcessNumber"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpGet("GetResponsibleIdsByUnifiedProcessNumberAsync/{unifiedProcessNumber}")]
        public async Task<IEnumerable<Guid>> GetResponsibleIdsByUnifiedProcessNumberAsync([FromQuery] string unifiedProcessNumber, CancellationToken ct)
        {
            var result = await _mediator.FetchAsync(new GetResponsibleIdsByUnifiedProcessNumberQuery
            {
                UnifiedProcessNumber = unifiedProcessNumber,
                CreatedBy = User.Identity.Name
            }, ct);

            return _mapper.Map<List<Guid>>(result);
        }

        /// <summary>
        /// Create Law Suit
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ct"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<CreateLawSuitResultModel> CreateAsync([FromBody] CreateLawSuitModel model, CancellationToken ct)
        {
            var command = new CreateLawSuitCommand()
            {
                Data = model,
                CreatedBy = User.Identity.Name
            };

            var result = await _mediator.SendAsync(command, ct);

            return new CreateLawSuitResultModel
            {
                Id = result.Id
            };
        }

        /// <summary>
        /// Update Law Suit
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        public async Task UpdateAsync([FromRoute] Guid id, [FromBody] UpdateLawSuitModel model, CancellationToken ct)
        {
            model.Id = id;
            var command = new UpdateLawSuitCommand()
            {
                Data = model,
                CreatedBy = User.Identity.Name
            };

            await _mediator.SendAsync(command, ct);
        }

        /// <summary>
        /// Delete Law Suit
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        public async Task DeleteAsync([FromRoute] Guid id, CancellationToken ct)
        {
            await _mediator.SendAsync(new DeleteLawSuitCommand
            {
                Data = new DeleteLawSuitModel() { Id = id },
                CreatedBy = User.Identity.Name
            }, ct);
        }
    }
}
