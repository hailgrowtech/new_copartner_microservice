using AutoMapper;
using CommonLibrary.CommonDTOs;
using Copartner;
using Microsoft.AspNetCore.JsonPatch;
using MigrationDB.Models;
using UserService.Dtos;

namespace UserService.Profiles;
public class AutoMapperProfile : Profile
{
    public UserCreateDto ToUserCreateEntity(UserCreatedEvent user)
    {
        var result = new UserCreateDto
        {
            MobileNumber = user.MobileNumber
        };

        return result;
    }
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

