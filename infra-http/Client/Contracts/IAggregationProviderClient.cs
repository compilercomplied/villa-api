using domain_business.Core.Transaction;
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

    Task<HttpResult<Transaction[], string>> QueryTransactions();


    // --- OAuth flow ----------------------------------------------------------
    Task<HttpResult<OAuthCredentials, string>> Authenticate(string code);
    Task<HttpResult<OAuthCredentials, string>> RefreshAuth();

  }

}
