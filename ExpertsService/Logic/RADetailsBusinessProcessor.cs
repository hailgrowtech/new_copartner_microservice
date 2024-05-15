using AutoMapper;
using CommonLibrary.CommonDTOs;
using ExpertService.Queries;
using ExpertsService.Dtos;
using MediatR;

namespace ExpertsService.Logic
{
    public class RADetailsBusinessProcessor : IRADetailsBusinessProcessor
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public RADetailsBusinessProcessor(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        public async Task<ResponseDto> Get(bool isCoPartner, int page = 1, int pageSize = 10)
        {
            var raListingDetailsList = await _sender.Send(new GetRADetailsQuery(isCoPartner,page, pageSize));
            var raListingDetailsReadDtoList = _mapper.Map<List<RADetailsReadDto>>(raListingDetailsList);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = raListingDetailsReadDtoList,
            };
        }

        public Task<ResponseDto> Get(Guid id, int page = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> Put(Guid id, RADetailsDto rAListingDetailsDto)
        {
            throw new NotImplementedException();
        }
    }
}
