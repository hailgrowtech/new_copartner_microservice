using AffiliatePartnerService.Dtos;
using AffiliatePartnerService.Queries;
using AutoMapper;
using CommonLibrary;
using CommonLibrary.CommonDTOs;
using ExpertService.Queries;
using MediatR;

namespace AffiliatePartnerService.Logic
{
    public class APListingBusinessProcessor : IAPListingBusinessProcessor
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public APListingBusinessProcessor(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        public async Task<ResponseDto> Get(int page = 1, int pageSize = 10)
        {
            var apListingList = await _sender.Send(new GetAPListingQuery(page, pageSize));
            //var apListingReadDtoList = _mapper.Map<List<APListingReadDto>>(apListingList);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = apListingList,
            };
        }

        public async Task<ResponseDto> Get(Guid id, int page = 1, int pageSize = 10)
        {
            var apListingData = await _sender.Send(new GetAPListingByIdQuery(id, page, pageSize));
            if (apListingData == null)
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
                Data = apListingData,
            };
        }

        public Task<ResponseDto> Put(Guid id, APListingDto aPListingDto)
        {
            throw new NotImplementedException();
        }
    }
}
