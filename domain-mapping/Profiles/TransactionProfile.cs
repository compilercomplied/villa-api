using AutoMapper;
using domain_business.Core.Transaction;
using domain_business.Core.Transaction.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace domain_mapping.Profiles
{

  public class TransactionProfile : Profile
  {

    public TransactionProfile()
    {

      TinkMaps();

      CreateMap<ProviderTransaction, TransactionEntity>()
        .ForMember(dest => dest.TransactionID,
          opt => opt.Ignore())
        .ForMember(dest => dest.CategoryID,
          opt => opt.Ignore())
        .ForMember(dest => dest.AccountID,
          opt => opt.Ignore());

    }

    void TinkMaps()
    {

      CreateMap<TinkTransaction, ProviderTransaction>()
        .ForMember(dest => dest.Amount,
          opt => opt.MapFrom(src => src.Amount))
        .ForMember(dest => dest.CategoryID,
          opt => opt.MapFrom(src => src.CategoryId))
        .ForMember(dest => dest.Date,
          opt => opt.MapFrom(src => DateTime.UnixEpoch.AddMilliseconds(src.Date)))
        .ForMember(dest => dest.Description,
          opt => opt.MapFrom(src => src.Description))
        .ForMember(dest => dest.Notes,
          opt => opt.MapFrom(src => src.Notes))
        .ForMember(dest => dest.ProductID,
          opt => opt.MapFrom(src => src.AccountId))
        .ForMember(dest => dest.TransactionID,
          opt => opt.MapFrom(src => src.Id));

    }

  }

}
