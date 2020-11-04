using AutoMapper;
using domain_business.Core.Transaction;
using domain_business.Core.Transaction.Providers;
using domain_extensions.Http.Result;
using domain_infra.Auth;
using domain_infra.FixedValues;
using http_infra.Client.Contracts;
using Microsoft.Extensions.Caching.Memory;
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
  using TransactionResponse = HttpResult<Transaction[], string>;

  // ---------------------------------------------------------------------------

  public class TinkClient : IAggregationProviderClient
  {

    #region services
    private readonly ILogger<TinkClient> _logger;
    private readonly TinkSettings _settings;
    private readonly HttpClient _client;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;
    #endregion services

    #region constructor
    public TinkClient(
      ILogger<TinkClient> logger,
      HttpClient client,
      IOptions<TinkSettings> settings,
      IMapper mapper,
      IMemoryCache cache
    )
    {
      _client = client;

      _logger = logger;
      _mapper = mapper;
      _cache = cache;

      _settings = settings.Value;

    }
    #endregion constructor

    public async Task<TransactionResponse> QueryTransactions(bool forceAuthRefresh)
    {
      TransactionResponse result;

      var path = _settings.APIPath.SearchTransactions;

      // -----------------------------------------------------------------------
      // TEST SCAFFOLD
      if (forceAuthRefresh)
        _cache.Remove(TinkFV.ACCESS_TOKEN);

      // -----------------------------------------------------------------------

      // -----------------------------------------------------------------------
      // TODO Move to a decorator.
      /*
      string accessToken = _cache.Get<string>(TinkFV.ACCESS_TOKEN);

      if (string.IsNullOrEmpty(accessToken))
      {
        return TransactionResponse.FAIL(
          HttpError<string>.FromRequest("Expired Tink credentials", System.Net.HttpStatusCode.Unauthorized));
      }
      */
      // -----------------------------------------------------------------------

      var request = new HttpRequestMessage(HttpMethod.Get, path);

      /*
      request.Headers.Authorization = 
        new AuthenticationHeaderValue("Bearer", accessToken);
      */


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

  }

}
