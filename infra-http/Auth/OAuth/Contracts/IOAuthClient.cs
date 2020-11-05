using domain_extensions.Http.Result;
using domain_infra.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace infra_http.Auth.OAuth.Contracts
{
  public interface IOAuthClient
  {
    Task<HttpResult<OAuthCredentials, string>> GetOAuthCreds(string code);
    Task<HttpResult<OAuthCredentials, string>> RefreshOAuthCreds();
  }
}
