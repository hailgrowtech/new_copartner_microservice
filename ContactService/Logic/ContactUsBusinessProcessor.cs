using CommonLibrary.CommonDTOs;
using ContactService.Commands;
using ContactService.Dto;
using ContactUsService.Dto;
using MediatR;

namespace ContactService.Logic
{
    public class ContactUsBusinessProcessor : IContactUsBusinessProcessor
    {
        private readonly ISender _sender;

        public ContactUsBusinessProcessor(ISender sender)
        {
            _sender = sender;
        }

        public async Task<ResponseDto> Post(ContactUsFormDto request)
        {
            var result = await _sender.Send(new SendContactUsFormCommand(request));
            if (result == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    ErrorMessages = new List<string>() { "Failed to send contact us form." }
                };
            }

            return new ResponseDto()
            {
                IsSuccess = true,
                DisplayMessage = "Contact us form submitted successfully."
            };
        }

        
    }
}
