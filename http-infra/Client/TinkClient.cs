using AutoMapper;
using domain_business.Core.Transaction;
using domain_business.Core.Transaction.Providers;
using domain_extensions.Http.Result;
using domain_infra.Auth;
using http_infra.Contracts;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using villa_configuration.Clients;

namespace http_infra.Client
{
  // --- Type Aliases ----------------------------------------------------------
  using OAuthResponse = HttpResult<OAuthCredentials, string>;
  using TransactionResponse = HttpResult<Transaction[], string>;

  // ---------------------------------------------------------------------------

  public class TinkClient : IAggregationProviderClient
  {

    // TODO Move to an env-based config.
    private static string ClientID = Environment.GetEnvironmentVariable("TINK_CLIENT_ID");
    private static string ClientSecret = Environment.GetEnvironmentVariable("TINK_CLIENT_SECRET");

    private static string[] Scopes = {
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
    private readonly ILogger<TinkClient> _logger;
    private readonly TinkSettings _settings;
    private readonly HttpClient _client;
    private readonly IMapper _mapper;
    #endregion services

    #region constructor
    public TinkClient(
      ILogger<TinkClient> logger,
      HttpClient client,
      IOptions<TinkSettings> settings,
      IMapper mapper
    )
    {
      _logger = logger;
      _client = client;
      _mapper = mapper;
      _settings = settings.Value;
    }
    #endregion constructor

    public async Task<OAuthResponse> GetOAuthCreds(string code)
    {
      OAuthResponse result;


      var path = _settings.APIPath.OAuthToken;

      var request = new HttpRequestMessage(HttpMethod.Post, path);

      var body = new List<KeyValuePair<string, string>>
      { 
        new KeyValuePair<string, string>("code", code),
        new KeyValuePair<string, string>("client_id", ClientID),
        new KeyValuePair<string, string>("client_secret", ClientSecret),
        new KeyValuePair<string, string>("grant_type", "authorization_code"),
      };

      var content = new FormUrlEncodedContent(body);
      request.Content = content;

      var httpResponse = await _client.SendAsync(request);


      if (httpResponse.IsSuccessStatusCode)
      {

        var credsRaw = await httpResponse.Content.ReadAsStringAsync();
        var creds = JsonConvert.DeserializeObject<OAuthCredentials>(credsRaw);

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

    // TODO abstract away auth.
    public async Task<TransactionResponse> QueryTransactions(string accessToken)
    {
      TransactionResponse result;

      var path = _settings.APIPath.SearchTransactions;


      var searchRequest = new HttpRequestMessage(HttpMethod.Get, path);

      searchRequest.Headers.Authorization = 
        new AuthenticationHeaderValue("Bearer", accessToken);


      var searchResponse = await _client.SendAsync(searchRequest);


      if (searchResponse.IsSuccessStatusCode)
      {

        var raw = await searchResponse.Content.ReadAsStringAsync();
        var payload = JsonConvert.DeserializeObject<TinkTransactionResponse>(raw);
        var transactionResponse = payload.Results.Select(r => r.Transaction);

        var transactions = _mapper.Map<Transaction[]>(transactionResponse);

        result = TransactionResponse.OK(transactions);

      }
      else
      {

        var errorMessage = await searchResponse.Content.ReadAsStringAsync();

        result = TransactionResponse.FAIL(
          HttpError<string>.FromRequest(errorMessage, searchResponse.StatusCode)
        );

      }

      return result;

    }

  }

}
