using AutoMapper;
using Mc2Tech.BaseApi.Controllers;
using Mc2Tech.Crosscutting.ViewModel.LawSuits;
using Mc2Tech.LawSuitsApi.ViewModel.LawSuits;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using SimpleSoft.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2Tech.LawSuitsApi.Controller
{
    /// <summary>
    /// Law Suits endpoints
    /// </summary>
    [ApiVersion("1.0")]
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class LawSuitsController : Mc2TechControllerBase
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
        [HttpGet(Name = "Search")]
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
                AccessToken = base.GetAccessToken().Parameter,

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
        [HttpGet("{id:guid}", Name = "GetById")]
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
        [HttpGet("{unifiedProcessNumber}", Name = "GetByUnifiedProcessNumber")]
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
        /// Get Responsible Ids by law suit id
        /// </summary>
        /// <param name="lawSuitId"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpGet("GetResponsibleIdsByLawSuitId/{lawSuitId:guid}")]
        public async Task<IEnumerable<Guid>> GetResponsibleIdsByLawSuitIdAsync([FromRoute] Guid lawSuitId, CancellationToken ct)
        {
            var result = await _mediator.FetchAsync(new GetResponsibleIdsByLawSuitIdQuery
            {
                LawSuitId = lawSuitId,
                CreatedBy = User.Identity.Name
            }, ct);

            return _mapper.Map<List<Guid>>(result);
        }

        /// <summary>
        /// Get count of law suits by responsible id
        /// </summary>
        /// <param name="responsibleId"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpGet("GetCountByResponsibleId/{responsibleId:guid}")]
        public async Task<int> GetCountByResponsibleIdAsync([FromRoute] Guid responsibleId, CancellationToken ct)
        {
            var result = await _mediator.FetchAsync(new GetCountByResponsibleIdQuery
            {
                ResponsibleId = responsibleId,
                CreatedBy = User.Identity.Name
            }, ct);

            return result;
        }

        /// <summary>
        /// Get Responsible Ids by Unified Process Number
        /// </summary>
        /// <param name="unifiedProcessNumber"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpGet("GetResponsibleIdsByUnifiedProcessNumber/{unifiedProcessNumber}")]
        public async Task<IEnumerable<Guid>> GetResponsibleIdsByUnifiedProcessNumberAsync([FromRoute] string unifiedProcessNumber, CancellationToken ct)
        {
            var result = await _mediator.FetchAsync(new GetResponsibleIdsByUnifiedProcessNumberQuery
            {
                UnifiedProcessNumber = unifiedProcessNumber,
                CreatedBy = User.Identity.Name
            }, ct);

            return _mapper.Map<List<Guid>>(result);
        }

        /// <summary>
        /// Get a basic law suits information by responsible id
        /// </summary>
        /// <param name="responsibleId"></param>
        /// <param name="ct"></param>
        /// <returns>Returns only Id, UnifiedProcessNumber, SituationId</returns>
        [HttpGet("GetLawSuitsBasicInformationByResponsibleId/{responsibleId}")]
        public async Task<IEnumerable<LawSuitModel>> GetLawSuitsBasicInformationByResponsibleIdAsync([FromRoute] Guid responsibleId, CancellationToken ct)
        {
            var result = await _mediator.FetchAsync(new GetLawSuitsBasicInformationByResponsibleIdQuery
            {
                ResponsibleId = responsibleId,
                CreatedBy = User.Identity.Name
            }, ct);

            return _mapper.Map<List<LawSuitModel>>(result);
        }

        /// <summary>
        /// Create Law Suit
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpPost(Name = "Create")]
        public async Task<CreateLawSuitResultModel> CreateAsync([FromBody] CreateLawSuitModel model, CancellationToken ct)
        {
            var command = new CreateLawSuitCommand()
            {
                AccessToken = base.GetAccessToken().Parameter,

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
        /// <param name="lawSuitId"></param>
        /// <param name="model"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpPut("{lawSuitId:guid}", Name = "Update")]
        public async Task<UpdateLawSuitResult> UpdateAsync([FromRoute] Guid lawSuitId, [FromBody] UpdateLawSuitModel model, CancellationToken ct)
        {
            model.Id = lawSuitId;
            var command = new UpdateLawSuitCommand()
            {
                AccessToken = base.GetAccessToken().Parameter,

                Data = model,
                CreatedBy = User.Identity.Name
            };

            var result = await _mediator.SendAsync(command, ct);

            return result;
        }

        /// <summary>
        /// Delete Law Suit
        /// </summary>
        /// <param name="lawSuitId"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpDelete("{lawSuitId:guid}", Name = "Delete")]
        public async Task DeleteAsync([FromRoute] Guid lawSuitId, CancellationToken ct)
        {
            await _mediator.SendAsync(new DeleteLawSuitCommand
            {
                Data = new DeleteLawSuitModel() { LawSuitId = lawSuitId },
                CreatedBy = User.Identity.Name
            }, ct);
        }
    }
}
