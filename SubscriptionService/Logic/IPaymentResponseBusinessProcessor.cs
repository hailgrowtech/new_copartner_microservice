using CommonLibrary.CommonDTOs;
using Microsoft.AspNetCore.JsonPatch;
using SubscriptionService.Dtos;

namespace SubscriptionService.Logic;
public interface IPaymentResponseBusinessProcessor
{
    Task<ResponseDto> Get();
    Task<ResponseDto> Get(string status);
    Task<ResponseDto> Get(Guid id);
    Task<ResponseDto> Put(Guid id, PaymentResponseCreateDto paymentResponseCreateDto);
    Task<ResponseDto> Post(PaymentResponseCreateDto paymentResponseCreateDto);
    Task<ResponseDto> Delete(Guid id);
    Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<PaymentResponseCreateDto> paymentResponseCreateDto);
}
