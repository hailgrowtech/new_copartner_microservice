using AdminDashboardService.Dtos;
using AdminDashboardService.Queries;
using AutoMapper;
using CommonLibrary.CommonDTOs;
using MediatR;
using System.Drawing.Printing;

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

        public async Task<ResponseDto> GetUserListing(int page, int pageSize)
        {
            var UserDataList = await _sender.Send(new GetUserDataListingQuery(page, pageSize));
            var UserDataListingList = _mapper.Map<List<UserDataListingDto>>(UserDataList);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = UserDataListingList,
            };
        }

        public async Task<ResponseDto> GetFirstTimePaymentListing(int page, int pageSize)
        {
            var UserFirstPaymentDataList = await _sender.Send(new GetUserFirstTimePaymentListingQuery(page, pageSize));
            var UserFirstPaymentDataListingList = _mapper.Map<List<UserFirstTimePaymentListingDto>>(UserFirstPaymentDataList);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = UserFirstPaymentDataListingList,
            };
        }

        public async Task<ResponseDto> GetSecondTimePaymentListing(int page, int pageSize)
        {
            var UserSecondPaymentDataList = await _sender.Send(new GetUserSecondTimePaymentListingQuery(page, pageSize));
            var UserSecondPaymentDataListingList = _mapper.Map<List<UserSecondTimePaymentListingDto>>(UserSecondPaymentDataList);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = UserSecondPaymentDataListingList,
            };
        }

        
    }
}
