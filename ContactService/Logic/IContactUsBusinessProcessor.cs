using CommonLibrary.CommonDTOs;
using ContactService.Dto;
using ContactUsService.Dto;

namespace ContactService.Logic;

public interface IContactUsBusinessProcessor
{
    Task<ResponseDto> Post(ContactUsFormDto form);
}