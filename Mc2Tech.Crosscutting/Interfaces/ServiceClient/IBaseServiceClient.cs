using Mc2Tech.Crosscutting.Model.ServiceClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mc2Tech.Crosscutting.Interfaces.ServiceClient
{
    public interface IBaseServiceClient<TDto> where TDto : class
    {
        Task<SearchResultDto<TDto>> GetAllAsync(HttpRequestPayloadDto httpRequestPayload, SearchRequestDto request);

        Task<int> GetCountAsync(HttpRequestPayloadDto payload, SearchRequestDto request);
        Task<TDto> GetByExternalReferenceAsync(HttpRequestPayloadDto payload, string reference);
        Task<TDto> GetByIdAsync(HttpRequestPayloadDto httpRequestPayload, int id);
        Task<ResultDto<string>> AddAsync(HttpRequestPayloadDto httpRequestPayload, TDto dto);
        Task<ResultDto<bool>> DeleteAsync(HttpRequestPayloadDto httpRequestPayload, string externalReference);
        Task<ResultDto<bool>> DeleteManyAsync(HttpRequestPayloadDto httpRequestPayload, IList<string> externalReferences);
        Task<ResultDto<bool>> ModifyAsync(HttpRequestPayloadDto httpRequestPayload, TDto dto);
        Task<ResultDto<bool>> EnableAsync(HttpRequestPayloadDto httpRequestPayload, string externalReference);
        Task<ResultDto<bool>> EnableManyAsync(HttpRequestPayloadDto httpRequestPayload, IList<string> externalReferences);
        Task<ResultDto<bool>> DisableAsync(HttpRequestPayloadDto httpRequestPayload, string externalReference);
        Task<ResultDto<bool>> DisableManyAsync(HttpRequestPayloadDto httpRequestPayload, IList<string> externalReferences);
    }
}
