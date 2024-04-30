using AffiliatePartnerService.Dtos;
using AffiliatePartnerService.Queries;
using AutoMapper;
using CommonLibrary.CommonDTOs;
using MediatR;

namespace AffiliatePartnerService.Logic
{
    public class APListingDetailsBusinessProcessor : IAPListingDetailsBusinessProcessor
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public APListingDetailsBusinessProcessor(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        public async Task<ResponseDto> Get()
        {
            var apListingDetailsList = await _sender.Send(new GetAPListingDetailsQuery());
            var apListingDetialsReadDtoList = _mapper.Map<List<APListingDetailReadDto>>(apListingDetailsList);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = apListingDetialsReadDtoList,
            };
        }

        public Task<ResponseDto> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> Put(Guid id, APListingDetailDto aPListingDetailDto)
        {
            throw new NotImplementedException();
        }
    }
}
