using AutoMapper;
using Mc2Tech.LawSuitsApi.ViewModel.LawSuits;
using Mc2Tech.LawSuitsApi.ViewModel.Situations;
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
    /// Situations Endpoints
    /// </summary>
    [ApiVersion("1.0")]
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class SituationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="mapper"></param>
        public SituationsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Base seach method to get situations
        /// </summary>
        /// <param name="filterQ"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<SituationModel>> SearchAsync([FromQuery] string filterQ, [FromQuery] int? skip, [FromQuery] int? take, CancellationToken ct)
        {
            var result = await _mediator.FetchAsync(new SearchSituationsQuery
            {
                FilterQ = filterQ,
                Skip = skip,
                Take = take,
                CreatedBy = User.Identity.Name
            }, ct);

            return _mapper.Map<IEnumerable<SituationModel>>(result);
        }

        /// <summary>
        /// Get situation by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<SituationModel> GetByIdAsync([FromRoute] Guid id, CancellationToken ct)
        {
            var result = await _mediator.FetchAsync(new GetSituationByIdQuery
            {
                SituationId = id,
                CreatedBy = User.Identity.Name
            }, ct);

            return _mapper.Map<SituationModel>(result);
        }

        /// <summary>
        /// Create situation
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<CreateLawSuitResultModel> CreateAsync([FromBody] CreateSituationModel model, CancellationToken ct)
        {
            var command = new CreateSituationCommand()
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
        /// Update situation
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        public async Task UpdateAsync([FromRoute] Guid id, [FromBody] UpdateSituationModel model, CancellationToken ct)
        {
            model.Id = id;
            var command = new UpdateSituationCommand()
            {
                Data = model,
                CreatedBy = User.Identity.Name
            };

            await _mediator.SendAsync(command, ct);
        }

        /// <summary>
        /// Delete situation
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        public async Task DeleteAsync([FromRoute] Guid id, CancellationToken ct)
        {
            await _mediator.SendAsync(new DeleteSituationCommand
            {
                Data = new DeleteSituationModel() { Id = id },
                CreatedBy = User.Identity.Name
            }, ct);
        }
    }
}
