﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dal_villa.Context;
using domain_business.Usecases.ProviderSync;
using domain_service.Aggregation;
using infra_http.Client.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace villa_api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AggregationController : ABCController
  {

    private readonly ILogger<AggregationController> _logger;
    private readonly IAggregationProviderClient _client;
    private readonly AggregationService _service;


    public AggregationController(
      ILogger<AggregationController> logger,
      IAggregationProviderClient aggregationProvider,
      AggregationService service
    )
    {

      _logger = logger;
      _client = aggregationProvider;
      _service = service;

    }

    [HttpPost]
    [Route("data")]
    [Authorize]
    public async Task<IActionResult> Sync([FromBody]DataSyncRequest req)
    {

      var user = BuildUser();

      await _service.Sync(req ?? new DataSyncRequest { }, user);

      return Ok();

    }

    [HttpPut]
    [Route("auth/refresh")]
    public async Task<IActionResult> Refresh()
    {

      var credResponse = await _client.RefreshAuth();
      var creds = credResponse.Unwrap();

      return Ok(creds);

    }

    [HttpGet]
    [Route("auth/callback")]
    public async Task<IActionResult> Callback([FromQuery] string code)
    {

      var credResponse = await _client.Authenticate(code);

      credResponse.Unwrap();

      return Ok();

    }

  }

}
