using CommonLibrary.CommonDTOs;
using ExpertService.Dtos;
using ExpertsService.Dtos;
using Microsoft.AspNetCore.JsonPatch;

namespace ExpertsService.Logic
{
    public interface IStandardQuestionsBusinessProcessor
    {
        Task<ResponseDto> Get(int page = 1, int pageSize = 10);
        Task<ResponseDto> Get(Guid id);
        Task<ResponseDto> Get(Guid id, int page = 1, int pageSize = 10);
        Task<ResponseDto> Post(StandardQuestionsCreateDto standardQuestionsCreateDto);
        Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<StandardQuestionsCreateDto> StandardQuestionsDto);
        Task<ResponseDto> Delete(Guid id);
    }
}
