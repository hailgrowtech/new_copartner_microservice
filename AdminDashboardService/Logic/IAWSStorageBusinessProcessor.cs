using CommonLibrary.CommonDTOs;
using Microsoft.AspNetCore.JsonPatch;
using AdminDashboardService.Dtos;

namespace AdminDashboardService.Logic;

public interface IAWSStorageBusinessProcessor
{
    Task<ResponseDto> Get(int page = 1, int pageSize = 10);
    Task<ResponseDto> Get(Guid id);
    Task<ResponseDto> Post(IFormFile file, string? prefix, string bucketName);
    Task<ResponseDto> Put(Guid id, BLogCreateDto blogCreateDto);
    Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<BLogCreateDto> blogDto);
    Task<ResponseDto> Delete(Guid id);
    
}
