using domain_business.Core.Product.Providers;
using domain_business.Core.Category.Providers;
using domain_business.Core.Transaction;
using domain_business.Core.Transaction.Providers;
using domain_business.Usecases.ProviderSync;
using domain_extensions.Http.Result;
using domain_infra.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace infra_http.Client.Contracts
{

  public interface IAggregationProviderClient
  {

    Task<HttpResult<ProviderTransaction[], string>> QueryTransactions(SyncRequest req);
    Task<HttpResult<ProviderCategory[], string>> ListCategories();
    Task<HttpResult<ProviderAccount[], string>> ListAccounts();

    // --- OAuth flow ----------------------------------------------------------
    Task<HttpResult<OAuthCredentials, string>> Authenticate(string code, string state);
    Task<HttpResult<OAuthCredentials, string>> RefreshAuth();

  }

}
