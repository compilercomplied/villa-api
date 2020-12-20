using domain_business.Usecases.Dashboard;
using domain_service.Dashboard;
using http_infra.Middleware.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace villa_api.Controllers
{

  [Route("api/[controller]")]
  [ApiController]
  [GoogleAuthorize]
  public class DashboardController : ControllerBase
  {

    private readonly ILogger<AggregationController> _logger;
    private readonly DashboardService _service;


    public DashboardController(
      ILogger<AggregationController> logger,
      DashboardService service
    )
    {

      _logger = logger;
      _service = service;

    }

    [HttpGet]
    public IActionResult BaseData()
    {

      var result = _service.BaseData();

      return Ok(result.Unwrap());

    }

    [HttpPost]
    [Route("transactions")]
    public IActionResult TransactionListing([FromBody] TransactionListingRequest req)
    {

      var result = _service.QueryTransactions(req);

      return Ok(result.Unwrap());

    }

  }

}
