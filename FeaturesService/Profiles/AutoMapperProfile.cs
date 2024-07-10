using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using FeaturesService.Dtos;
using CommonLibrary.CommonDTOs;
using MigrationDB.Models;
using FeaturesService.Dtos;
using MigrationDB.Model;

namespace FeaturesService.Profiles;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        


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

