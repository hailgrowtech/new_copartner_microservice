using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using CommonLibrary.CommonDTOs;
using MigrationDB.Models;
using WalletService.Dtos;
using MigrationDB.Model;

namespace WalletService.Profiles;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
       // Source -> Target Withdrwal Mode
        CreateMap<WithdrawalMode, WithdrawalModeReadDto>().ReverseMap();
        CreateMap<WithdrawalMode, WithdrawalModeCreateDto>().ReverseMap();
        CreateMap<WithdrawalMode, JsonPatchDocument<WithdrawalModeCreateDto>>().ReverseMap();
        CreateMap<WithdrawalMode, ResponseDto>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src)); // Map Blog entity to ResponseDto's Data property

        // Source -> Target Withdrwal Request
        CreateMap<Withdrawal, WithdrawalReadDto>().ReverseMap();
        CreateMap<Withdrawal, WithdrawalCreateDto>().ReverseMap();
        CreateMap<Withdrawal, JsonPatchDocument<WithdrawalCreateDto>>().ReverseMap();
        CreateMap<Withdrawal, ResponseDto>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src)); // Map Blog entity to ResponseDto's Data property

        // Source -> Target Withdrwal Request
        CreateMap<Wallet, WalletReadDto>().ReverseMap();
        CreateMap<Wallet, WalletWithdrawalReadDto>().ReverseMap();
        CreateMap<Wallet, ResponseDto>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src)); // Map Blog entity to ResponseDto's Data property

    }

}

