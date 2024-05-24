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
public class WithdrawalBusinessProcessor : IWithdrawalBusinessProcessor
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public WithdrawalBusinessProcessor(ISender sender, IMapper mapper)
    {
        this._sender = sender;
        this._mapper = mapper;
    }
    public async Task<ResponseDto> Get(string RequestBy, int page, int pageSize)
    {        
            var withdrawalList = await _sender.Send(new GetWithdrawalQuery(RequestBy, page,pageSize));
            var withdrawalReadDtoList = _mapper.Map<List<WithdrawalReadDto>>(withdrawalList);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = withdrawalReadDtoList,
            };           
    }
    public async Task<ResponseDto> GetWithdrawalMode()
    {
        var withdrawalModeList = await _sender.Send(new GetWithdrawalModeQuery());
        var withdrawalModeDtoList = _mapper.Map<List<WithdrawalModeReadDto>>(withdrawalModeList);
        return new ResponseDto()
        {
            IsSuccess = true,
            Data = withdrawalModeDtoList,
        };
    }
    public async Task<ResponseDto> Get(Guid id)
    {
        var withdrawal = await _sender.Send(new GetWithdrawalByIdQuery(id));
        if (withdrawal == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = null,
                ErrorMessages = new List<string>() { AppConstants.Common_NoRecordFound }
            };
        }
        var withdrawalReadDto = _mapper.Map<WithdrawalDetailsReadDto>(withdrawal);
        return new ResponseDto()
        {
            IsSuccess = true,
            Data = withdrawalReadDto,
        };
    }

    public async Task<ResponseDto> GetWithdrawalMode(Guid id)
    {
        var withdrawalMode = await _sender.Send(new GetWithdrawalModeByIdQuery(id));
        if (withdrawalMode == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = null,
                ErrorMessages = new List<string>() { AppConstants.Common_NoRecordFound }
            };
        }
        var withdrawalModeDto = _mapper.Map<WithdrawalModeReadDto>(withdrawalMode);
        return new ResponseDto()
        {
            IsSuccess = true,
            Data = withdrawalModeDto,
        };
    }

    public async Task<ResponseDto> GetWithdrawalModeByUserId(Guid id,string userType, int page, int pageSize)
    {
        var withdrawalMode = await _sender.Send(new GetWithdrawalModeByUserIdQuery(id, userType, page, pageSize));
        if (withdrawalMode == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = null,
                ErrorMessages = new List<string>() { AppConstants.Common_NoRecordFound }
            };
        }
       // var withdrawalModeDto = _mapper.Map<WithdrawalModeReadDto>(withdrawalMode);
        return new ResponseDto()
        {
            IsSuccess = true,
            Data = withdrawalMode,
        };
    }


    public async Task<ResponseDto> Post(WithdrawalCreateDto request)
    {
        var withdrawal = _mapper.Map<Withdrawal>(request);
        var response = await _sender.Send(new GetWithdrawalByIdQuery(withdrawal.Id));
        if (response != null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<WithdrawalReadDto>(response),
                ErrorMessages = new List<string>() { AppConstants.Common_NoRecordFound }
            };
        }

        var result = await _sender.Send(new CreateWithdrawalCommand(withdrawal));
        if (result == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<WithdrawalReadDto>(response),
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
    public async Task<ResponseDto> PostWithdrawalMode(WithdrawalModeCreateDto request)
    {
        var withdrawalMode = _mapper.Map<WithdrawalMode>(request);

        var response = await _sender.Send(new GetWithdrawalModeByIdQuery(withdrawalMode.Id));
        if (response != null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<WithdrawalModeReadDto>(response),
                ErrorMessages = new List<string>() { AppConstants.Common_NoRecordFound }
            };
        }

        var result = await _sender.Send(new CreateWithdrawalModeCommand(withdrawalMode));
        if (result == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<WithdrawalModeReadDto>(response),
                ErrorMessages = new List<string>() { AppConstants.Common_FailedToCreateNewRecord}
            };
        }

        var resultDto = _mapper.Map<WithdrawalModeReadDto>(result);
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

    public async Task<ResponseDto> PutBankUPIDetails(Guid id, WithdrawalModeCreateDto request)
    {
        var withdrawalMode = _mapper.Map<WithdrawalMode>(request);

        var existingblogs = await _sender.Send(new GetWithdrawalModeByIdQuery(id));
        if (existingblogs == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<WithdrawalModeReadDto>(existingblogs),
                ErrorMessages = new List<string>() { AppConstants.Common_NoRecordFound }
            };
        }
        withdrawalMode.Id = id; // Assigning the provided Id to the experts
        var result = await _sender.Send(new PutWithdrawalModeCommand(withdrawalMode));
        if (result == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<WithdrawalModeReadDto>(existingblogs),
                ErrorMessages = new List<string>() { AppConstants.Common_FailedToCreateNewRecord }
            };
        }

        var resultDto = _mapper.Map<WithdrawalModeReadDto>(result);
        return new ResponseDto()
        {
            Data = resultDto,
            DisplayMessage = AppConstants.Common_RecordCreated
        };
    }

    public async Task<ResponseDto> Delete(Guid id)
    {
        var paymentMode = await _sender.Send(new DeleteBankUPICommand(id));
        var resultDto = _mapper.Map<ResponseDto>(paymentMode);
        return new ResponseDto()
        {
            Data = resultDto,
            DisplayMessage = AppConstants.Common_RecordDeleted
        };
    }

    public async Task<ResponseDto> GetWithdrawalByUserId(Guid id, string userType, int page, int pageSize)
    {
        var withdrawal = await _sender.Send(new GetWithdrawalByUserIdQuery(id, userType,page,pageSize));
        if (withdrawal == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = null,
                ErrorMessages = new List<string>() { AppConstants.Common_NoRecordFound }
            };
        }
        //var withdrawalReadDto = _mapper.Map<WithdrawalDetailsReadDto>(withdrawal);
        return new ResponseDto()
        {
            IsSuccess = true,
            Data = withdrawal,
        };
    }
}

