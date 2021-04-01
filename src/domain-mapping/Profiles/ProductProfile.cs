using AutoMapper;
using domain_business.Core.Product;
using domain_business.Core.Product.Providers;
using domain_business.Usecases.Dashboard;

namespace domain_mapping.Profiles
{
  public class ProductProfile : Profile
  {
    public ProductProfile()
    { 

      TinkMaps();

      CreateMap<ProviderAccount, ProductEntity>();
      CreateMap<ProductEntity, ProviderAccount>();
      CreateMap<ProductEntity, DashboardProduct>();

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
