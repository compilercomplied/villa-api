using AutoMapper;
using dal_villa.Context;
using domain_business.Core.Product;
using domain_business.Core.Category;
using domain_business.Core.Transaction;
using domain_business.Usecases.ProviderSync;
using infra_http.Client.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain_service.Aggregation
{

  public class AggregationService
  {

    #region ioc
    private readonly ILogger<AggregationService> _logger;
    private readonly IAggregationProviderClient _client;
    private readonly IMapper _mapper;
    private readonly VillaContext _context;
    #endregion ioc

    #region constructor
    public AggregationService(
      ILogger<AggregationService> logger,
      IAggregationProviderClient client,
      IMapper mapper,
      VillaContext context
    )
    {

      _logger = logger;
      _client = client;
      _context = context;
      _mapper = mapper;

    }
    #endregion constructor

    public async Task Sync(SyncRequest req)
    {

      CategoryEntity[] cats;
      ProductEntity[] accounts;

      if (req.Init)
      {
        accounts = await SyncAccounts();
        cats = await SyncCategories();
        _context.SaveChanges();

      }
      else
      {
        accounts = _context.Accounts.ToArray();
        cats = _context.Categories.ToArray();
      }


      await SyncTransactions(cats, accounts, req);

      await _context.SaveChangesAsync();

    }

    #region sync aux

    async Task<ProductEntity[]> SyncAccounts()
    {

      var accResponse = await _client.ListAccounts();
      var acc = accResponse.Unwrap();

      var mapped = _mapper.Map<ProductEntity[]>(acc);

      await _context.Accounts.AddRangeAsync(mapped);

      return mapped;

    }
    async Task<CategoryEntity[]> SyncCategories()
    { 

      var catResponse = await _client.ListCategories();
      var cats = catResponse.Unwrap();

      var catsMap = _mapper.Map<CategoryEntity[]>(cats);
      await _context.Categories.AddRangeAsync(catsMap);

      return catsMap;

    }

    async Task SyncTransactions(
      CategoryEntity[] cats,
      ProductEntity[] products,
      SyncRequest req
    )
    { 

      var tranResponse = await _client.QueryTransactions(req);
      var tran = tranResponse.Unwrap();

      var tranResult = _mapper.Map<TransactionEntity[]>(tran);

      var tranWithFK = tran.AsEnumerable().Join(
        cats.AsEnumerable(),
        tran => tran.CategoryID,
        cat => cat.ProviderID,
        (tran, cat) => new { tran, cat }
      )
      .Join(
        products.AsEnumerable(),
        query => query.tran.ProductID,
        acc => acc.ProviderID,
        (query, prod) => new TransactionEntity
        { 
          ProductID = prod.InternalProductID,
          Amount = query.tran.Amount,
          CategoryID = query.cat.CategoryID,
          Date = query.tran.Date,
          Description = query.tran.Description,
          Notes = query.tran.Notes,
        }

      );

      await _context.Transactions.AddRangeAsync(tranWithFK);

    }

    #endregion sync aux



  }

}
