using AutoMapper;
using Mc2Tech.BaseApi.Controllers;
using Mc2Tech.Crosscutting.Interfaces.Persons;
using Mc2Tech.PersonsApi.ViewModel.Create;
using Mc2Tech.PersonsApi.ViewModel.Delete;
using Mc2Tech.PersonsApi.ViewModel.Get;
using Mc2Tech.PersonsApi.ViewModel.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleSoft.Mediator;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2Tech.PersonsApi.Controller
{
    /// <summary>
    /// Persons Endpoints
    /// </summary>
    [ApiVersion("1.0")]
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PersonsController : Mc2TechControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="mapper"></param>
        public PersonsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Search Persons paginated
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cpf"></param>
        /// <param name="unifiedProcessNumber"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpGet(Name = "Search")]
        public async Task<IEnumerable<IPersonDto>> SearchAsync(
            [FromQuery] string name, [FromQuery] string cpf, [FromQuery] string unifiedProcessNumber,
            [FromQuery] int? skip, [FromQuery] int? take, CancellationToken ct
        )
        {
            var result = await _mediator.FetchAsync(new SearchPersonsQuery
            {
                Name = name,
                Cpf = cpf,
                UnifiedProcessNumber = unifiedProcessNumber,
                Skip = skip,
                Take = take,
                CreatedBy = User.Identity.Name,
                AccessToken = base.GetAccessToken().Parameter
            }, ct);

            return _mapper.Map<IEnumerable<IPersonDto>>(result);
        }

        /// <summary>
        /// Get persons Id by contains person name 
        /// </summary>
        /// <param name="personName"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpGet("GetPersonIdsByPersonName/{personName}")]
        public async Task<IEnumerable<Guid>> GetPersonIdsByPersonNameAsync([FromRoute] string personName, CancellationToken ct)
        {
            var result = await _mediator.FetchAsync(new GetPersonIdsByPersonNameQuery
            {
                PersonName = personName,
                CreatedBy = User.Identity.Name,
                AccessToken = base.GetAccessToken().Parameter
            }, ct);

            return _mapper.Map<IEnumerable<Guid>>(result);
        }

        /// <summary>
        /// Get a person by PersonId
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpGet("GetPersonIdExists/{personId}")]
        public async Task<bool> GetPersonIdExistsAsync([FromRoute] Guid personId, CancellationToken ct)
        {
            var result = await _mediator.FetchAsync(new GetPersonIdExistsQuery
            {
                PersonId = personId,
                CreatedBy = User.Identity.Name,
                AccessToken = base.GetAccessToken().Parameter
            }, ct);

            return result;
        }

        /// <summary>
        /// Get Basic Information of a Person by PersonId
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="ct"></param>
        /// <returns>Return only Id, Name, Cpf, Email</returns>
        [HttpGet("GetPersonBasicInformation/{personId}")]
        public async Task<IPersonDto> GetPersonBasicInformationAsync([FromRoute] Guid personId, CancellationToken ct)
        {
            var result = await _mediator.FetchAsync(new GetPersonBasicInformationByIdQuery
            {
                PersonId = personId,
                CreatedBy = User.Identity.Name
            }, ct);

            return _mapper.Map<IPersonDto>(result);
        }

        /// <summary>
        /// Get Person Photo by PersonId
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpGet("GetPersonPhoto/{personId}")]
        public async Task<byte[]> GetPersonPhotoAsync([FromRoute] Guid personId, CancellationToken ct)
        {
            var result = await _mediator.FetchAsync(new GetPersonPhotoByIdQuery
            {
                PersonId = personId,
                CreatedBy = User.Identity.Name
            }, ct);

            return result;
        }

        /// <summary>
        /// Get a Person by PersonId
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpGet("{personId:guid}", Name = "GetById")]
        public async Task<IPersonDto> GetByIdAsync([FromRoute] Guid personId, CancellationToken ct)
        {
            var result = await _mediator.FetchAsync(new GetPersonByIdQuery
            {
                PersonId = personId,
                CreatedBy = User.Identity.Name
            }, ct);

            return _mapper.Map<IPersonDto>(result);
        }

        /// <summary>
        /// Create new Person
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpPost(Name = "Create")]
        public async Task<CreatePersonResultModel> CreateAsync([FromBody] CreatePersonModel model, CancellationToken ct)
        {
            var command = new CreatePersonCommand()
            {
                Data = model,
                CreatedBy = User.Identity.Name
            };

            var result = await _mediator.SendAsync(command, ct);

            return new CreatePersonResultModel
            {
                Id = result.Id
            };
        }

        /// <summary>
        /// Update a person
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="model"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpPut("{personId:guid}", Name = "Update")]
        public async Task UpdateAsync([FromRoute] Guid personId, [FromBody] UpdatePersonModel model, CancellationToken ct)
        {
            model.Id = personId;
            var command = new UpdatePersonCommand()
            {
                Data = model,
                CreatedBy = User.Identity.Name
            };

            await _mediator.SendAsync(command, ct);
        }

        /// <summary>
        /// Delete a person
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpDelete("{personId:guid}", Name = "Delete")]
        public async Task DeleteAsync([FromRoute] Guid personId, CancellationToken ct)
        {
            await _mediator.SendAsync(new DeletePersonCommand
            {
                AccessToken = base.GetAccessToken().Parameter,

                Data = new DeletePersonModel() { PersonId = personId },
                CreatedBy = User.Identity.Name
            }, ct);
        }
    }
}
