using AutoMapper;
using CommonLibrary;
using CommonLibrary.CommonDTOs;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using MigrationDB.Model;
using SubscriptionService.Commands;
using SubscriptionService.Dtos;
using SubscriptionService.Queries;
using static MassTransit.ValidationResultExtensions;

namespace SubscriptionService.Logic;
public class PaymentResponseBusinessProcessor : IPaymentResponseBusinessProcessor
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    public PaymentResponseBusinessProcessor(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    public async Task<ResponseDto> Get()
    {
        var paymentMstList = await _sender.Send(new GetPaymentResponseQuery());
        var paymentMstReadDtoList = _mapper.Map<List<PaymentResponseReadDto>>(paymentMstList);
        return new ResponseDto()
        {
            IsSuccess = true,
            Data = paymentMstReadDtoList,
        };
    }

    public async Task<ResponseDto> Get(Guid id)
    {
        var payment = await _sender.Send(new GetPaymentResponseByIdQuery(id));
        if (payment == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = null,
                ErrorMessages = new List<string>() { AppConstants.Expert_ExpertNotFound }
            };
        }
        var paymentMstsReadDto = _mapper.Map<PaymentResponseReadDto>(payment);
        return new ResponseDto()
        {
            IsSuccess = true,
            Data = paymentMstsReadDto,
        };
    }
    public async Task<ResponseDto> Get(string paymentStatus)
    {
        var paymentMsts = await _sender.Send(new GetPaymentResponseByStatusQuery(paymentStatus));
        if (paymentMsts == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = null,
                ErrorMessages = new List<string>() { AppConstants.Common_NoRecordFound }
            };
        }

        //var paymentMstsReadDto = _mapper.Map<PaymentResponseReadDto>(paymentMsts);

        return new ResponseDto()
        {
            IsSuccess = true,
            Data = paymentMsts,
        };
    }
    public async Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<PaymentResponseCreateDto> request)
    {           
        var existingpaymentMsts = await _sender.Send(new GetPaymentResponseByIdQuery(Id));
        if (existingpaymentMsts == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                //   Data = _mapper.Map<ExpertsReadDto>(existingExperts),
                ErrorMessages = new List<string>() { AppConstants.Expert_ExpertNotFound }
            };
        }
        var paymentMsts = _mapper.Map<PaymentResponse>(existingpaymentMsts);
        var result = await _sender.Send(new PatchPaymentResponseCommand(Id, request, paymentMsts));
        if (result == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<PaymentResponseReadDto>(paymentMsts),
                ErrorMessages = new List<string>() { AppConstants.Expert_FailedToUpdateExpert }
            };
        }

        return new ResponseDto()
        {
            Data = _mapper.Map<PaymentResponseReadDto>(result),
            DisplayMessage = AppConstants.Expert_ExpertUpdated
        };
    }
    public async Task<ResponseDto> Post(PaymentResponseCreateDto request)
    {
        var payment = _mapper.Map<PaymentResponse>(request);

        var existingpayment = await _sender.Send(new GetPaymentResponseByIdQuery(payment.Id));
        if (existingpayment != null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<PaymentResponseReadDto>(existingpayment),
                ErrorMessages = new List<string>() { AppConstants.Common_AlreadyExistsRecord}
            };
        }

        var result = await _sender.Send(new CreatePaymentResponseCommand(payment));
        if (result == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<PaymentResponseReadDto>(existingpayment),
                ErrorMessages = new List<string>() { AppConstants.Common_FailedToCreateNewRecord }
            };
        }

        var resultDto = _mapper.Map<PaymentResponseReadDto>(result);
        return new ResponseDto()
        {
            Data = resultDto,
            DisplayMessage = AppConstants.Common_RecordCreated
        };
    }
    public async Task<ResponseDto> Put(Guid id, PaymentResponseCreateDto request)
    {
        var payment = _mapper.Map<PaymentResponse>(request);

        var existingPayment = await _sender.Send(new GetPaymentResponseByIdQuery(id));
        if (existingPayment == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<PaymentResponseReadDto>(existingPayment),
                ErrorMessages = new List<string>() { AppConstants.Common_NoRecordFound }
            };
        }
        payment.Id = id; // Assigning the provided Id to the subscription
        var result = await _sender.Send(new PutPaymentResponseCommand(payment));
        if (result == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<PaymentResponseReadDto>(existingPayment),
                ErrorMessages = new List<string>() { AppConstants.Common_FailedToCreateNewRecord}
            };
        }

        var resultDto = _mapper.Map<PaymentResponseReadDto>(result);
        return new ResponseDto()
        {
            Data = resultDto,
            DisplayMessage = AppConstants.Common_RecordCreated
        };
    }
    public async Task<ResponseDto> Delete(Guid Id)
    {
        var payment = await _sender.Send(new DeletePaymentResponseCommand(Id));
        var paymentReadDto = _mapper.Map<ResponseDto>(payment);
        if (paymentReadDto == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                ErrorMessages = new List<string>() { AppConstants.Common_NoRecordFound }
            };
        }
        return new ResponseDto()
        {
            Data = paymentReadDto,
            DisplayMessage = AppConstants.Common_RecordDeleted
        };
    }
}
