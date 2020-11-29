using domain_extensions.Core.Result;
using domain_infra.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace http_infra.Middleware.Response
{

  public class ExceptionHandler
  {

    private readonly ILogger<ExceptionHandler> _logger;
    private readonly RequestDelegate _next;

    public ExceptionHandler(ILogger<ExceptionHandler> logger, RequestDelegate next)
    {
      _logger = logger;
      _next = next;
    }
    public async Task Invoke(HttpContext context)
    {
      try
      {
        await _next(context);
      }
      catch (Exception ex)
      {
        await HandleException(context, ex);
      }

    }

    Task HandleException(HttpContext context, Exception ex)
    {
      HttpStatusCode code = HttpStatusCode.InternalServerError;

      bool isCustomEx = (ex is OperationException);

      string message = "Generic error message";  // TODO rsx
      if (isCustomEx)
      {
        _logger.LogInformation("Error result unwrapped", ex);
        message = ex.Message;
      }
      else
      { 
        _logger.LogCritical("Unhandled exception", ex);
      }

      var httpResponse = new ErrorResponse { Message = message };

      string result = JsonConvert.SerializeObject(httpResponse);

      context.Response.ContentType = "application/json";
      context.Response.StatusCode = (int)code;

      return context.Response.WriteAsync(result);

    }

  }


}
