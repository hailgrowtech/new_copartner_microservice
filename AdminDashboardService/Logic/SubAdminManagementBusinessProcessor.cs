using AdminDashboardService.Dtos;
using AdminDashboardService.Queries;
using AutoMapper;
using CommonLibrary;
using CommonLibrary.CommonDTOs;
using MediatR;

namespace AdminDashboardService.Logic
{
    public class SubAdminManagementBusinessProcessor : ISubAdminManagementBusinessProcessor
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public SubAdminManagementBusinessProcessor(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        public async Task<ResponseDto> Get()
        {
            var subadminsList = await _sender.Send(new GetSubAdminManagementQuery());
            var subadminsListReadDtoList = _mapper.Map<List<SubAdminManagementReadDto>>(subadminsList);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = subadminsListReadDtoList,
            };
        }

        public async Task<ResponseDto> Get(Guid id)
        {
            var subadmins = await _sender.Send(new GetSubAdminManagementByIdQuery(id));
            if (subadmins == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = null,
                    ErrorMessages = new List<string>() { AppConstants.Expert_ExpertNotFound }
                };
            }
            var subadminsReadDto = _mapper.Map<SubAdminManagementReadDto>(subadmins);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = subadminsReadDto,
            };
        }
    }
}
