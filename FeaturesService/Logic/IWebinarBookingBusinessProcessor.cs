using CommonLibrary.CommonDTOs;
using FeaturesService.Dtos;
using FeaturesService.Dtos;
using Microsoft.AspNetCore.JsonPatch;

namespace FeaturesService.Logic;
public interface IWebinarBookingBusinessProcessor
{
    Task<ResponseDto> Get(int page = 1, int pageSize = 10);
    Task<ResponseDto> Get(Guid id);
    Task<ResponseDto> Post(WebinarBookingCreateDto webinarBookingCreateDto);
    Task<ResponseDto> Put(Guid id, WebinarBookingCreateDto webinarBookingCreateDto);
    Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<WebinarBookingCreateDto> webinarBookingDto);
    string GenerateToken(string channelName, string uid);
}
