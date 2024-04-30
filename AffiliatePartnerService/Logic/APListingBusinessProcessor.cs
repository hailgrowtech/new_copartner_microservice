using AffiliatePartnerService.Dtos;
using AffiliatePartnerService.Queries;
using AutoMapper;
using CommonLibrary.CommonDTOs;
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

        public async Task<ResponseDto> Get()
        {
            var apListingList = await _sender.Send(new GetAPListingQuery());
            var apListingReadDtoList = _mapper.Map<List<APListingReadDto>>(apListingList);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = apListingReadDtoList,
            };
        }

        public Task<ResponseDto> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> Put(Guid id, APListingDto aPListingDto)
        {
            throw new NotImplementedException();
        }
    }
}
