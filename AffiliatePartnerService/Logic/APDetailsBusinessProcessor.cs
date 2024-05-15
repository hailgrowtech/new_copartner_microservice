using AffiliatePartnerService.Dtos;
using AffiliatePartnerService.Queries;
using AutoMapper;
using CommonLibrary.CommonDTOs;
using MediatR;

namespace AffiliatePartnerService.Logic
{
    public class APListingDetailsBusinessProcessor : IAPDetailsBusinessProcessor
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public APListingDetailsBusinessProcessor(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        public async Task<ResponseDto> Get(int page = 1, int pageSize = 10)
        {
            var apListingDetailsList = await _sender.Send(new GetAPDetailsQuery());
            var apListingDetialsReadDtoList = _mapper.Map<List<APDetailReadDto>>(apListingDetailsList);
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

        public Task<ResponseDto> Put(Guid id, APDetailDto aPListingDetailDto)
        {
            throw new NotImplementedException();
        }
    }
}
