using CommonLibrary.CommonDTOs;
using Microsoft.AspNetCore.JsonPatch;
using WalletService.Dtos;

namespace WalletService.Logic;

public interface IWalletBusinessProcessor
{
    Task<ResponseDto> Get(int page, int pageSize);
    Task<ResponseDto> Get(Guid id, string userType);
    Task<ResponseDto> Post(WithdrawalCreateDto withdrawalMode);
    Task<ResponseDto> Put(Guid id, WithdrawalCreateDto withdrawalCreateDto);
}
