using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using CommonLibrary.CommonDTOs;
using WalletService.Dtos;
using MigrationDB.Model;
using Copartner;

namespace WalletService.Profiles;
public class AutoMapperProfile : Profile
{
    public WalletCreateDto ToWalletCreateEntity(WalletEvent wallet)
    {
        var result = new WalletCreateDto
        {
            SubscriberId = wallet.SubscriberId,
            AffiliatePartnerId = wallet.AffiliatePartnerId,
            ExpertsId = wallet.ExpertsId,
            RAAmount = wallet.RAAmount,
            APAmount = wallet.APAmount,
            CPAmount = wallet.CPAmount,
            TransactionDate = DateTime.Now
        };

        return result;
    }
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
        CreateMap<Wallet, WalletCreateDto>().ReverseMap();
        CreateMap<Wallet, ResponseDto>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src)); // Map Blog entity to ResponseDto's Data property

    }

}

