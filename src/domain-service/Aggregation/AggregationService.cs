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
using domain_business.Usecases.Auth_flow;
using domain_service.User;
using domain_business.Core.User;

namespace domain_service.Aggregation
{

  public class AggregationService
  {

    #region ioc
    private readonly UserService _users;
    private readonly ILogger<AggregationService> _logger;
    private readonly IAggregationProviderClient _client;
    private readonly IMapper _mapper;
    private readonly VillaContext _context;
    #endregion ioc

    #region constructor
    public AggregationService(
      UserService users,
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
      _users = users;

    }
    #endregion constructor

    public async Task Sync(DataSyncRequest req, UserData userData)
    {

      var user = _users.BySubject(userData.Subject);


      CategoryEntity[] cats = await FetchCategories(req.Master);
      ProductEntity[] accounts = await FetchAccounts(req.Master, user);

      await SyncTransactions(cats, accounts, user, req);

      await _context.SaveChangesAsync();

    }

    #region sync aux

    async Task<CategoryEntity[]> FetchCategories(bool fromProvider)
    { 

      var currentCategories = _context.Categories.ToArray();

      if (!fromProvider)
        return currentCategories;


      var providerCategories = await _client.ListCategories();

      var mappedProviderCats = _mapper
        .Map<CategoryEntity[]>(providerCategories.Unwrap());


      var currentProviderIDs = currentCategories.Select(s => s.ProviderID);

      var missingCategories = mappedProviderCats
        .Where(cat => !currentProviderIDs.Contains(cat.ProviderID));

      if (!missingCategories.Any()) 
        return currentCategories;


      await _context.Categories.AddRangeAsync(missingCategories);


      return _context.Categories.ToArray();

    }

    async Task<ProductEntity[]> FetchAccounts(bool fromProvider, UserEntity user)
    {

      var currentProducts = _context.Products.ToArray();

      if (!fromProvider)
        return currentProducts;


      var providerAccounts = await _client.ListAccounts();

      var mappedAccounts = _mapper.Map<ProductEntity[]>(providerAccounts.Unwrap());


      var currentProductIDs = currentProducts.Select(s => s.ProviderID);

      var missingProducts = mappedAccounts
        .Where(acc => !currentProductIDs.Contains(acc.ProviderID));

      if (!missingProducts.Any())
        return currentProducts;


      foreach (var product in missingProducts) { product.User = user; }

      await _context.Products.AddRangeAsync(missingProducts);


      return _context.Products.ToArray();

    }

    async Task SyncTransactions(
      CategoryEntity[] cats,
      ProductEntity[] products,
      UserEntity user,
      DataSyncRequest req
    )
    {

      var currentTransactions = _context.Transactions
        .Where(tran => tran.Date >= req.From);

      var currentTranProviderIDs = currentTransactions
        .Select(tran => tran.ProviderTransactionID);

      var providerTransactions = await _client.QueryTransactions(req);
      var tran = providerTransactions.Unwrap();


      var tranWithFK = providerTransactions.Unwrap()
        .Where(ptran => !currentTranProviderIDs.Contains(ptran.TransactionID))
        .Join(
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
          Amount = query.tran.Amount,
          Category = query.cat,
          Date = query.tran.Date,
          Description = query.tran.Description,
          Notes = query.tran.Notes,
          Product = prod,
          ProviderTransactionID = query.tran.TransactionID,
          User = user,
        }

      );

      await _context.Transactions.AddRangeAsync(tranWithFK);

    }

    #endregion sync aux



  }

}
