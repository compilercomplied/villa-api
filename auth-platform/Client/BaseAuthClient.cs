using auth_platform.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace auth_platform.Client
{

  public abstract class BaseAuthClient : IBaseAuthClient
  {

    private readonly ILogger<BaseAuthClient> _logger;
    private readonly IHttpClientFactory _clientFactory;


    public BaseAuthClient(ILogger<BaseAuthClient> logger, IHttpClientFactory clientFactory)
    { 

      _logger = logger;
      _clientFactory = clientFactory;

    }

  }

}
