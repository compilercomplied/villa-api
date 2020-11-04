using System.Threading.Tasks;
using http_infra.Client.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace villa_api.Controllers
{

  [Route("provider")]
  [ApiController]
  public class ProviderTestController : ControllerBase
  {
    private readonly ILogger<ProviderTestController> _logger;
    private readonly IAggregationProviderClient _aggProvider;


    public ProviderTestController(
      ILogger<ProviderTestController> logger,
      IAggregationProviderClient aggregationProvider
    )
    {

      _logger = logger;
      _aggProvider = aggregationProvider;

    }

    [HttpGet]
    public async Task<IActionResult> Root([FromQuery] bool forceAuthRefresh = false)
    {

      var tranResponse = await _aggProvider.QueryTransactions(forceAuthRefresh);
      var tran = tranResponse.Unwrap();

      return Ok(tran);

    }

  }

}
