using AutoMapper;
using domain_business.Core.Category;
using domain_business.Core.Category.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace domain_mapping.Profiles
{

  public class CategoryProfile : Profile
  {

    public CategoryProfile()
    {

      TinkMaps();

      CreateMap<ProviderCategory, CategoryEntity>()
        .ForMember(dest => dest.CategoryID,
          opt => opt.Ignore())
        .ForMember(dest => dest.Name,
          opt => opt.MapFrom(src => src.Name))
        .ForMember(dest => dest.ProviderID,
          opt => opt.MapFrom(src => src.ProviderCategoryID));

    }

    void TinkMaps()
    {
      CreateMap<TinkCategory, ProviderCategory>()
        .ForMember(dest => dest.ProviderCategoryID,
          opt => opt.MapFrom(src => src.Id))
        .ForMember(dest => dest.GroupID,
          opt => opt.MapFrom(src => src.Parent))
        .ForMember(dest => dest.Name,
          opt => opt.MapFrom(src => src.SecondaryName ?? src.PrimaryName));
    }

  }

}
