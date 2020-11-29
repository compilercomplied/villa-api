using AutoMapper;
using domain_business.Core.Account;
using domain_business.Core.Account.Providers;

namespace domain_mapping.Profiles
{
  public class AccountProfile : Profile
  {
    public AccountProfile()
    { 

      TinkMaps();

      CreateMap<ProviderAccount, AccountEntity>();
      CreateMap<AccountEntity, ProviderAccount>();

    }

    void TinkMaps()
    {

      CreateMap<TinkAccount, ProviderAccount>()
        .ForMember(dest => dest.AccountID,
          opt => opt.Ignore())
        .ForMember(dest => dest.ProviderID,
          opt => opt.MapFrom(src => src.Id))
        .ForMember(dest => dest.Description,
          opt => opt.MapFrom(src => src.AccountNumber))
        .ForMember(dest => dest.Number,
          opt => opt.MapFrom(src => src.AccountNumber));
    }
  }
}
