using CommonLibrary.CommonDTOs;
using Microsoft.AspNetCore.JsonPatch;
using AdminDashboardService.Dtos;

namespace AdminDashboardService.Logic;

public interface IAWSStorageBusinessProcessor
{
    Task<ResponseDto> Post(IFormFile file, string? prefix, string bucketName);
    Task<ResponseDto> Delete( string? filePath, string bucketName);
    
}
