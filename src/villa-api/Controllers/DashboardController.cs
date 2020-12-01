using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using domain_service.Dashboard;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace villa_api.Controllers
{

  [Route("api/[controller]")]
  [ApiController]
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

  }

}
