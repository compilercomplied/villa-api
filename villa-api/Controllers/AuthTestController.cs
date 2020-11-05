using System.Threading.Tasks;
using infra_http.Auth.OAuth.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace villa_api.Controllers
{
  [Route("auth")]
  [ApiController]
  public class AuthTestController : ControllerBase
  {

    private readonly ILogger<AuthTestController> _logger;
    private readonly IOAuthClient _oauthClient;


    public AuthTestController(
      ILogger<AuthTestController> logger,
      IOAuthClient oauthClient
    )
    {

      _logger = logger;
      _oauthClient = oauthClient;

    }

    [HttpGet]
    [Route("callback")]
    public async Task<IActionResult> Callback([FromQuery] string code)
    {

      var credResponse = await _oauthClient.GetOAuthCreds(code);
      var creds = credResponse.Unwrap();

      return Ok(creds);

    }

  }
}
