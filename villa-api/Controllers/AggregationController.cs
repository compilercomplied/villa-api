using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using infra_http.Client.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace villa_api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AggregationController : ControllerBase
  {

    private readonly ILogger<AggregationController> _logger;
    private readonly IAggregationProviderClient _client;


    public AggregationController(
      ILogger<AggregationController> logger,
      IAggregationProviderClient aggregationProvider
    )
    {

      _logger = logger;
      _client = aggregationProvider;

    }

    [HttpPost]
    [Route("sync")]
    public async Task<IActionResult> Sync()
    {

      return Ok(new { message = "WIP"});

    }

    [HttpGet]
    [Route("oauth/refresh")]
    public async Task<IActionResult> Refresh()
    {

      var credResponse = await _client.RefreshAuth();
      var creds = credResponse.Unwrap();

      return Ok(creds);

    }

    [HttpGet]
    [Route("oauth/callback")]
    public async Task<IActionResult> Callback([FromQuery] string code)
    {

      var credResponse = await _client.Authenticate(code);
      var creds = credResponse.Unwrap();

      return Ok(creds);

    }

  }

}
