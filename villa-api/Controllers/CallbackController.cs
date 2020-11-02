using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using http_infra.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace villa_api.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class CallbackController : ControllerBase
  {

    private readonly ILogger<CallbackController> _logger;
    private readonly IAggregationProviderClient _aggProvider;


    public CallbackController(
      ILogger<CallbackController> logger,
      IAggregationProviderClient aggregationProvider
    )
    {

      _logger = logger;
      _aggProvider = aggregationProvider;

    }


    [HttpGet]
    public async Task<IActionResult> Root([FromQuery] string code, [FromQuery] string credentialsId)
    {

      var oauthResponse = await _aggProvider.GetOAuthCreds(code);
      var oauth = oauthResponse.Unwrap();

      var tranResponse = await _aggProvider.QueryTransactions(oauth.AccessToken);
      var tran = tranResponse.Unwrap();

      return Ok(tran);

    }

  }

}
