using CommonLibrary.CommonDTOs;
using Microsoft.AspNetCore.JsonPatch;
using WalletService.Dtos;

namespace WalletService.Logic;

public interface IWithdrawalBusinessProcessor
{
    Task<ResponseDto> Get(int page, int pageSize,string requestBy);
    Task<ResponseDto> GetWithdrawalMode();
    Task<ResponseDto> Get(Guid id);
    Task<ResponseDto> GetWithdrawalMode(Guid id);
    Task<ResponseDto> GetWithdrawalModeByUserId(Guid id);
    Task<ResponseDto> Post(WithdrawalCreateDto withdrawalMode);
    Task<ResponseDto> PostWithdrawalMode(WithdrawalModeCreateDto withdrawalMode);
    Task<ResponseDto> Put(Guid id, WithdrawalCreateDto withdrawalCreateDto);
    Task<ResponseDto> PutBankUPIDetails(Guid id, WithdrawalModeCreateDto withdrawalModeCreateDto);
}
