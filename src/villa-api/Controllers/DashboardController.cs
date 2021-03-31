using domain_business.Usecases.Dashboard;
using domain_service.Dashboard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

namespace villa_api.Controllers
{

  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class DashboardController : ABCController
  {

    #region ioc
    private readonly ILogger<AggregationController> _logger;
    private readonly DashboardService _service;
    #endregion ioc

    #region constructor
    public DashboardController(
      ILogger<AggregationController> logger,
      DashboardService service
    )
    {

      _logger = logger;
      _service = service;

    }
    #endregion constructor


    [HttpGet]
    public IActionResult BaseData()
    {

      var user = BuildUser();
      var result = _service.BaseData(user);

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
