using AdminDashboardService.Dtos;
using CommonLibrary.CommonDTOs;
using Microsoft.AspNetCore.JsonPatch;

namespace AdminDashboardService.Logic
{
    public interface ITelegramMessageBusinessProcessor
    {
        Task<ResponseDto> Get(int page = 1, int pageSize = 10);
        Task<ResponseDto> Get(Guid id);
        Task<ResponseDto> Get(Guid id, string userType, int page = 1, int pageSize = 10);
        Task<ResponseDto> Post(TelegramMessageCreateDto telegramMessage);
        Task<ResponseDto> Put(Guid id, TelegramMessageCreateDto telegramMessageCreateDto);
        Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<TelegramMessageCreateDto> telegramMessageDto);
        Task<ResponseDto> Delete(Guid id);
    }
}
