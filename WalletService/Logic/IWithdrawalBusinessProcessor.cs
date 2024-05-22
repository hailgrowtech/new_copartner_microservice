using CommonLibrary.CommonDTOs;
using Microsoft.AspNetCore.JsonPatch;
using WalletService.Dtos;

namespace WalletService.Logic;

public interface IWithdrawalBusinessProcessor
{
    Task<ResponseDto> Get(string requestBy, int page, int pageSize);
    Task<ResponseDto> GetWithdrawalMode();
    Task<ResponseDto> Get(Guid id);
    Task<ResponseDto> GetWithdrawalMode(Guid id);
    Task<ResponseDto> GetWithdrawalModeByUserId(Guid id, string userType, int page, int pageSize);
    Task<ResponseDto> GetWithdrawalByUserId(Guid id, string userType, int page, int pageSize);
    Task<ResponseDto> Post(WithdrawalCreateDto withdrawalMode);
    Task<ResponseDto> PostWithdrawalMode(WithdrawalModeCreateDto withdrawalMode);
    Task<ResponseDto> Put(Guid id, WithdrawalCreateDto withdrawalCreateDto);
    Task<ResponseDto> PutBankUPIDetails(Guid id, WithdrawalModeCreateDto withdrawalModeCreateDto);
    Task<ResponseDto> Delete(Guid id);
}
