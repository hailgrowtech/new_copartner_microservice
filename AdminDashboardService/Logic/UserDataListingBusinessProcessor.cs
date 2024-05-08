using AdminDashboardService.Dtos;
using AdminDashboardService.Queries;
using AutoMapper;
using CommonLibrary.CommonDTOs;
using MediatR;

namespace AdminDashboardService.Logic
{
    public class UserDataListingBusinessProcessor : IUserDataListingBusinessProcessor
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public UserDataListingBusinessProcessor(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        public async Task<ResponseDto> GetUserListing()
        {
            var UserDataList = await _sender.Send(new GetUserDataListingQuery());
            var UserDataListingList = _mapper.Map<List<UserDataListingDto>>(UserDataList);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = UserDataListingList,
            };
        }

        public async Task<ResponseDto> GetFirstTimePaymentListing()
        {
            var UserFirstPaymentDataList = await _sender.Send(new GetUserFirstTimePaymentListingQuery());
            var UserFirstPaymentDataListingList = _mapper.Map<List<UserFirstTimePaymentListingDto>>(UserFirstPaymentDataList);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = UserFirstPaymentDataListingList,
            };
        }

        public async Task<ResponseDto> GetSecondTimePaymentListing()
        {
            var UserSecondPaymentDataList = await _sender.Send(new GetUserSecondTimePaymentListingQuery());
            var UserSecondPaymentDataListingList = _mapper.Map<List<UserSecondTimePaymentListingDto>>(UserSecondPaymentDataList);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = UserSecondPaymentDataListingList,
            };
        }

        
    }
}
