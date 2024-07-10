using AutoMapper;
using CommonLibrary;
using CommonLibrary.CommonDTOs;
using ExpertService.Queries;
using ExpertsService.Dtos;
using MediatR;

namespace ExpertsService.Logic
{
    public class RAListingBusinessProcessor : IRAListingBusinessProcessor
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public RAListingBusinessProcessor(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        public async Task<ResponseDto> Get(int page = 1, int pageSize = 10)
        {
            var raListingList = await _sender.Send(new GetRAListingQuery(page, pageSize));
            var raListingReadDtoList = _mapper.Map<List<RAListingReadDto>>(raListingList);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = raListingReadDtoList,
            };
        }

        public async Task<ResponseDto> Get(Guid id, int page = 1, int pageSize = 10)
        {
            var raListingData = await _sender.Send(new GetRAListingByIdQuery(id, page, pageSize));
            if (raListingData == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = null,
                    ErrorMessages = new List<string>() { AppConstants.Common_NoRecordFound }
                };
            }
           // var raListingReadDto = _mapper.Map<RAListingDataReadDto>(raListingData);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = raListingData,
            };
        }

        public Task<ResponseDto> Put(Guid id, RAListingDto rAListingDto)
        {
            throw new NotImplementedException();
        }
        public async Task<ResponseDto> GetInvoice(Guid id, int page = 1, int pageSize = 10)
        {
            var raListingData = await _sender.Send(new GetRAListingByIdQuery(id, page, pageSize));
            if (raListingData == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = null,
                    ErrorMessages = new List<string>() { AppConstants.Common_NoRecordFound }
                };
            }
            // var raListingReadDto = _mapper.Map<RAListingDataReadDto>(raListingData);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = raListingData,
            };
        }
    }
}
