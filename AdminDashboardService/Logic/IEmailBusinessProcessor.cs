using CommonLibrary.CommonDTOs;
using Microsoft.AspNetCore.JsonPatch;
using AdminDashboardService.Dtos;

namespace AdminDashboardService.Logic;

public interface IEmailBusinessProcessor
{
    Task<ResponseDto> Post(EmailCreateDto emails);
    
}
