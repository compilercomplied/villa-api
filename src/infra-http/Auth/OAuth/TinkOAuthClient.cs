using AutoMapper;
using domain_extensions.Http.Result;
using domain_infra.Auth;
using domain_infra.FixedValues;
using infra_configuration.Clients;
using infra_http.Auth.OAuth.Contracts;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace infra_http.Auth.OAuth
{
  // --- Type Aliases ----------------------------------------------------------
  using OAuthResponse = HttpResult<OAuthCredentials, string>;

  // ---------------------------------------------------------------------------

  public class TinkOAuthClient : IOAuthClient
  {

    // TODO Move to an env-based config.
    private static readonly string ClientID = Environment
      .GetEnvironmentVariable("TINK_CLIENT_ID") ?? string.Empty;
    private static readonly string ClientSecret = Environment
      .GetEnvironmentVariable("TINK_CLIENT_SECRET") ?? string.Empty;

    private static readonly string[] Scopes = {
      "accounts:read",
      "categories:read",
      "credentials:read",
      "follow:read",
      "identity:read",
      "investments:read",
      "payment:read",
      "payment:write",
      "providers:read",
      "statistics:read",
      "suggestions:read",
      "transactions:read",
      "transfer:execute",
      "transfer:read",
      "user:read",
    };

    #region services
    private readonly ILogger<TinkOAuthClient> _logger;
    private readonly TinkSettings _settings;
    private readonly HttpClient _client;
    private readonly IMemoryCache _cache;
    #endregion services

    #region constructor
    public TinkOAuthClient(
      ILogger<TinkOAuthClient> logger,
      IOptions<TinkSettings> settings,
      HttpClient client,
      IMemoryCache cache)
    {
      _client = client;

      _logger = logger;
      _cache = cache;

      _settings = settings.Value;
    }
    #endregion constructor

    // -------------------------------------------------------------------------

    public async Task<OAuthResponse> Authenticate(string code)
    {
      OAuthResponse result;


      var path = _settings.APIPath.OAuthToken;

      var request = new HttpRequestMessage(HttpMethod.Post, path)
      {
        Content = BuildOAuthBody(code)
      };

      var httpResponse = await _client.SendAsync(request);


      if (httpResponse.IsSuccessStatusCode)
      {

        var credsRaw = await httpResponse.Content.ReadAsStringAsync();
        var creds = JsonConvert.DeserializeObject<OAuthCredentials>(credsRaw);

        CacheCredentials(creds);

        result = OAuthResponse.OK(creds);

      }
      else
      {

        var errorMessage = await httpResponse.Content.ReadAsStringAsync();

        result = OAuthResponse.FAIL(
          HttpError<string>.FromRequest(errorMessage, httpResponse.StatusCode)
        );

      }


      return result;

    }

    HttpContent BuildOAuthBody(string code)
    {

      var body = new List<KeyValuePair<string, string>>
      {
        new KeyValuePair<string, string>("code", code),
        new KeyValuePair<string, string>("client_id", ClientID),
        new KeyValuePair<string, string>("client_secret", ClientSecret),
        new KeyValuePair<string, string>("grant_type", "authorization_code"),
      };

      return new FormUrlEncodedContent(body);

    }

    // -------------------------------------------------------------------------

    public async Task<OAuthResponse> RefreshOAuth()
    {
      OAuthResponse result;


      var path = _settings.APIPath.OAuthToken;

      var refreshToken = _cache.Get<string>(TinkFV.REFRESH_TOKEN);
      var request = new HttpRequestMessage(HttpMethod.Post, path)
      {
        Content = BuildOAuthRefreshBody(refreshToken)
      };

      var httpResponse = await _client.SendAsync(request);


      if (httpResponse.IsSuccessStatusCode)
      {

        var credsRaw = await httpResponse.Content.ReadAsStringAsync();
        var creds = JsonConvert.DeserializeObject<OAuthCredentials>(credsRaw);

        CacheCredentials(creds);  // TODO: XXX

        result = OAuthResponse.OK(creds);

      }
      else
      {

        var errorMessage = await httpResponse.Content.ReadAsStringAsync();

        result = OAuthResponse.FAIL(
          HttpError<string>.FromRequest(errorMessage, httpResponse.StatusCode)
        );

      }

      return result;

    }

    HttpContent BuildOAuthRefreshBody(string refreshToken)
    {

      var body = new List<KeyValuePair<string, string?>>
      {
        new KeyValuePair<string, string?>("refresh_token", refreshToken),
        new KeyValuePair<string, string?>("client_id", ClientID),
        new KeyValuePair<string, string?>("client_secret", ClientSecret),
        new KeyValuePair<string, string?>("grant_type", "refresh_token"),
      };

      return new FormUrlEncodedContent(body);

    }

    // -------------------------------------------------------------------------

    void CacheCredentials(OAuthCredentials creds)
    {
      // both times are measured in hours
      const int accessExpiration = 2;
      const int refreshExpiration = 24;

      var accessOpts = new MemoryCacheEntryOptions()
        .SetAbsoluteExpiration(TimeSpan.FromHours(accessExpiration));

      var refreshOpts = new MemoryCacheEntryOptions()
        .SetAbsoluteExpiration(TimeSpan.FromHours(refreshExpiration));

      _cache.Set(TinkFV.ACCESS_TOKEN, creds.AccessToken, accessOpts);
      _cache.Set(TinkFV.REFRESH_TOKEN, creds.RefreshToken, refreshOpts);

    }

  }

}
