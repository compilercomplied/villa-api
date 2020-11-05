using domain_infra.FixedValues;
using infra_http.Auth.OAuth.Contracts;
using infra_http.Middleware.Contracts;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace infra_http.Middleware
{

  public class TinkOAuthHandler : IExternalOAuthHandler
  {

    private readonly ILogger<TinkOAuthHandler> _logger;
    private readonly IMemoryCache _cache;
    private readonly IOAuthClient _oauth;

    public TinkOAuthHandler(
      ILogger<TinkOAuthHandler> logger, 
      IMemoryCache cache,
      IOAuthClient oauth
      )
      : base()
    {

      _logger = logger;
      _cache = cache;
      _oauth = oauth;

    }

    protected override async Task<HttpResponseMessage> SendAsync(
      HttpRequestMessage request,
      CancellationToken cancellationToken)
    {

      var accessToken = _cache.Get<string>(TinkFV.ACCESS_TOKEN);

      if (string.IsNullOrEmpty(accessToken))
      {

        bool refreshAuth = await RefreshOAuthCreds();

        if (!refreshAuth)
        { 
            return new HttpResponseMessage(HttpStatusCode.Unauthorized)
            {
              // TODO move to rsx
              Content = new StringContent("Tink authentication expired")
            };
        }

        accessToken = _cache.Get<string>(TinkFV.ACCESS_TOKEN);

      }

      request.Headers.Authorization = 
        new AuthenticationHeaderValue("Bearer", accessToken);

      return await base.SendAsync(request, cancellationToken);

    }

    async Task<bool> RefreshOAuthCreds()
    { 

      var refreshToken = _cache.Get<string>(TinkFV.REFRESH_TOKEN);

      if (string.IsNullOrEmpty(refreshToken)) return false;

      var req = await _oauth.RefreshOAuthCreds();

      req.Unwrap();

      return true;

    }

  }

}
