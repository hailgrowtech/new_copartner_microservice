using AdminDashboardService.Dtos;
using CommonLibrary.CommonDTOs;
using Microsoft.AspNetCore.JsonPatch;

namespace AdminDashboardService.Logic
{
    public interface IJoinBusinessProcessor
    {
        Task<ResponseDto> Get();
        Task<ResponseDto> Get(Guid id);
        Task<ResponseDto> Post(JoinCreateDto joins);
        Task<ResponseDto> Put(Guid id, JoinCreateDto joinCreateDto);
        Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<JoinCreateDto> joinDto);
        Task<ResponseDto> Delete(Guid id);  
    }
}
