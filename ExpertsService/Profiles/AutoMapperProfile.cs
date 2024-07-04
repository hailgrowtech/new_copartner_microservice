using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using ExpertService.Dtos;
using CommonLibrary.CommonDTOs;
using MigrationDB.Models;
using ExpertsService.Dtos;
using MigrationDB.Model;

namespace ExpertService.Profiles;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Source -> Target
        CreateMap<Experts, ExpertReadDto>().ReverseMap();
        CreateMap<Experts, ExpertsCreateDto>().ReverseMap();
        CreateMap<RAListingDto, RAListingReadDto>().ReverseMap();
        CreateMap<RAListingDataDto, RAListingDataReadDto>().ReverseMap();
        CreateMap<RADetailsDto, RADetailsReadDto>().ReverseMap();
        CreateMap<Experts, JsonPatchDocument<ExpertsCreateDto>>().ReverseMap();
        CreateMap<RAListingDataDto, ResponseDto>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src));
        CreateMap<Experts, ResponseDto>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src)); // Map Subscription entity to ResponseDto's Data property


        // Source -> Target
        CreateMap<WebinarMst, WebinarMstReadDto>().ReverseMap();
        CreateMap<WebinarMst, WebinarMstCreateDto>().ReverseMap();
        CreateMap<WebinarMst, JsonPatchDocument<WebinarMstCreateDto>>().ReverseMap();
        CreateMap<WebinarMst, ResponseDto>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src)); // Map Subscription entity to ResponseDto's Data property


        // Source -> Target
        CreateMap<WebinarBooking, WebinarBookingReadDto>().ReverseMap();
        CreateMap<WebinarBooking, WebinarBookingCreateDto>().ReverseMap();
        CreateMap<WebinarBooking, JsonPatchDocument<WebinarBookingCreateDto>>().ReverseMap();
        CreateMap<WebinarBooking, ResponseDto>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src)); // Map Subscription entity to ResponseDto's Data property

    }

}

