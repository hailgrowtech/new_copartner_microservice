using AutoMapper;
using CommonLibrary;
using CommonLibrary.CommonDTOs;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using WalletService.Dtos;
using MigrationDB.Model;
using WalletService.Queries;
using WalletService.Commands;

namespace WalletService.Logic;
public class WalletBusinessProcessor : IWalletBusinessProcessor
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public WalletBusinessProcessor(ISender sender, IMapper mapper)
    {
        this._sender = sender;
        this._mapper = mapper;
    }
    public async Task<ResponseDto> Get(int page, int pageSize)
    {        
            var walletList = await _sender.Send(new GetWalletQuery(page, pageSize));
            var walletReadDtoList = _mapper.Map<List<WalletReadDto>>(walletList);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = walletReadDtoList,
            };           
    }
    public async Task<ResponseDto> Get(Guid id, string userType)
    {
        var wallet = await _sender.Send(new GetWalletByIdQuery(id,userType));
        if (wallet == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = null,
                ErrorMessages = new List<string>() { AppConstants.Common_NoRecordFound }
            };
        }
        var walletWithdrawalReadDto = _mapper.Map<WalletWithdrawalReadDto>(wallet);
        return new ResponseDto()
        {
            IsSuccess = true,
            Data = walletWithdrawalReadDto,
        };
    }
    public async Task<ResponseDto> Post(WalletCreateDto request)
    {
        var wallet = _mapper.Map<Wallet>(request);

        //var response = await _sender.Send(new GetWithdrawalByIdQuery(withdrawal.Id));
        //if (response != null)
        //{
        //    return new ResponseDto()
        //    {
        //        IsSuccess = false,
        //        Data = _mapper.Map<WithdrawalReadDto>(response),
        //        ErrorMessages = new List<string>() { AppConstants.Common_NoRecordFound }
        //    };
        //}

        var result = await _sender.Send(new CreateWalletCommand(wallet));
        if (result == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<WalletReadDto>(result),
                ErrorMessages = new List<string>() { AppConstants.Common_FailedToCreateNewRecord }
            };
        }

        var resultDto = _mapper.Map<WalletReadDto>(result);
        return new ResponseDto()
        {
            Data = resultDto,
            DisplayMessage = AppConstants.Common_RecordCreated
        };
    }
   
    public async Task<ResponseDto> Put(Guid id, WithdrawalCreateDto request)
    {
        var withdrawal = _mapper.Map<Withdrawal>(request);

        var existingblogs = await _sender.Send(new GetWithdrawalByIdQuery(id));
        if (existingblogs == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<WithdrawalReadDto>(existingblogs),
                ErrorMessages = new List<string>() { AppConstants.Common_NoRecordFound }
            };
        }
        withdrawal.Id = id; // Assigning the provided Id to the experts
        var result = await _sender.Send(new PutWithdrawalCommand(withdrawal));
        if (result == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<WithdrawalReadDto>(existingblogs),
                ErrorMessages = new List<string>() { AppConstants.Common_FailedToCreateNewRecord }
            };
        }

        var resultDto = _mapper.Map<WithdrawalReadDto>(result);
        return new ResponseDto()
        {
            Data = resultDto,
            DisplayMessage = AppConstants.Common_RecordCreated
        };
    }

}

