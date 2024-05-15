using AdminDashboardService.Dtos;
using CommonLibrary.CommonDTOs;
using Microsoft.AspNetCore.JsonPatch;

namespace AdminDashboardService.Logic
{
    public interface ISubAdminManagementBusinessProcessor
    {
        Task<ResponseDto> Get();
        Task<ResponseDto> Get(Guid id);
    }
}
