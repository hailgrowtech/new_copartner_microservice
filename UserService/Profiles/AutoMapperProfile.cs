using AutoMapper;
using CommonLibrary.CommonDTOs;
using Microsoft.AspNetCore.JsonPatch;
using MigrationDB.Models;
using UserService.Dtos;

namespace UserService.Profiles;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Source -> Target
        CreateMap<User, UserReadDto>().ReverseMap();
        CreateMap<User, UserCreateDto>().ReverseMap();
        CreateMap<User, JsonPatchDocument<UserCreateDto>>().ReverseMap();
        CreateMap<User, ResponseDto>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src));
    }

}

