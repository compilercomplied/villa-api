using domain_business.Core.Transaction;
using domain_extensions.Http.Result;
using domain_infra.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace http_infra.Contracts
{

  public interface IAggregationProviderClient
  {

    Task<HttpResult<OAuthCredentials, string>> GetOAuthCreds(string code);
    Task<HttpResult<Transaction[], string>> QueryTransactions(string accessToken);

  }

}
