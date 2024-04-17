using AutoMapper;
using SignInService.Dtos;
using SignInService.Models;

namespace SignInService.Profiles;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Source -> Target
        CreateMap<PotentialCustomer, PotentialCustomerDto>().ReverseMap();
    }

}

