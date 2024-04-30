using AutoMapper;
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

        public async Task<ResponseDto> Get()
        {
            var raListingList = await _sender.Send(new GetRAListingQuery());
            var raListingReadDtoList = _mapper.Map<List<RAListingReadDto>>(raListingList);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = raListingReadDtoList,
            };
        }

        public Task<ResponseDto> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> Put(Guid id, RAListingDto rAListingDto)
        {
            throw new NotImplementedException();
        }
    }
}
