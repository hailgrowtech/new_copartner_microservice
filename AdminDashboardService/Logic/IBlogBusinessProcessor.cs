using CommonLibrary.CommonDTOs;
using Microsoft.AspNetCore.JsonPatch;
using AdminDashboardService.Dtos;

namespace AdminDashboardService.Logic;

public interface IBlogBusinessProcessor
{
    Task<ResponseDto> Get();
    Task<ResponseDto> Get(Guid id);
    Task<ResponseDto> Post(BLogCreateDto blogs);
    Task<ResponseDto> Put(Guid id, BLogCreateDto blogCreateDto);
    Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<BLogCreateDto> blogDto);
    Task<ResponseDto> Delete(Guid id);
    
}
