using AutoMapper;
using domain_business.Core.Transaction;
using domain_business.Core.Transaction.Providers;
using domain_extensions.Http.Result;
using domain_infra.Auth;
using domain_infra.FixedValues;
using infra_configuration.Clients;
using infra_http.Auth.OAuth.Contracts;
using infra_http.Client.Contracts;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace infra_http.Client
{
  // --- Type Aliases ----------------------------------------------------------
  using TransactionResponse = HttpResult<Transaction[], string>;
  using OAuthResponse = HttpResult<OAuthCredentials, string>;

  // ---------------------------------------------------------------------------

  public class TinkClient : IAggregationProviderClient
  {

    #region services
    private readonly ILogger<TinkClient> _logger;
    private readonly TinkSettings _settings;
    private readonly HttpClient _client;
    private readonly IOAuthClient _oauth;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;
    #endregion services

    #region constructor
    public TinkClient(
      ILogger<TinkClient> logger,
      HttpClient client,
      IOptions<TinkSettings> settings,
      IOAuthClient oauth,
      IMapper mapper,
      IMemoryCache cache
    )
    {
      _oauth = oauth;

      _client = client;

      _logger = logger;
      _mapper = mapper;
      _cache = cache;

      _settings = settings.Value;

    }
    #endregion constructor

    public async Task<TransactionResponse> QueryTransactions()
    {
      TransactionResponse result;

      var path = _settings.APIPath.SearchTransactions;
      var request = new HttpRequestMessage(HttpMethod.Get, path);

      var searchResponse = await _client.SendAsync(request);


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


    // --- OAuth flow ----------------------------------------------------------
    public async Task<OAuthResponse> Authenticate(string code)
      => await _oauth.Authenticate(code);

    public async Task<OAuthResponse> RefreshAuth()
      => await _oauth.RefreshOAuth();

  }

}
