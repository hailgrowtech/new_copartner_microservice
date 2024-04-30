using AutoMapper;
using CommonLibrary.CommonDTOs;
using ExpertService.Queries;
using ExpertsService.Dtos;
using MediatR;

namespace ExpertsService.Logic
{
    public class RAListingDetailsBusinessProcessor : IRAListingDetailsBusinessProcessor
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public RAListingDetailsBusinessProcessor(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        public async Task<ResponseDto> Get()
        {
            var raListingDetailsList = await _sender.Send(new GetRAListingDetailsQuery());
            var raListingDetailsReadDtoList = _mapper.Map<List<RAListingDetailsReadDto>>(raListingDetailsList);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = raListingDetailsReadDtoList,
            };
        }

        public Task<ResponseDto> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> Put(Guid id, RAListingDetailsDto rAListingDetailsDto)
        {
            throw new NotImplementedException();
        }
    }
}
