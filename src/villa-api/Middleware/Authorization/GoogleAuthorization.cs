using domain_constants.Env;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace http_infra.Middleware.Authorization
{

  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
  public class GoogleAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
  {
    readonly static string CLIENTID = 
      Environment.GetEnvironmentVariable(GoogleEnv.ID);

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {

      string auth = context.HttpContext.Request.Headers["Authorization"];
      if (string.IsNullOrEmpty(auth))
      {
        context.Result = new UnauthorizedResult();
        return;
      }

      string jwt = ParseAuth(auth);


      var settings = BuildSettings();
      GoogleJsonWebSignature.Payload payload;

      try
      {
        payload = await GoogleJsonWebSignature.ValidateAsync(jwt, settings);
      }
      // No need to do anything with either exception. Two catch blocks are 
      // written to separate between a standard behaviour (throw InvalidJwtEx if
      // the token is not valid) from something unexpected like the token being
      // malformed or any kind of possible 500 from Google's backend.
      catch (InvalidJwtException)
      {
        context.Result = new UnauthorizedResult();
        return;
      }
      catch (Exception)
      {
        context.Result = new UnauthorizedResult();
        return;
      }


      if (!Valid(payload))
      { 
        context.Result = new UnauthorizedResult();
        return;
      }

    }

    // Trim the 'Bearer ' declaration.
    string ParseAuth(string rawHeader) => rawHeader[7..];

    GoogleJsonWebSignature.ValidationSettings BuildSettings() =>
      new GoogleJsonWebSignature.ValidationSettings
      {
        Audience = new []{ CLIENTID },
      };

    bool Valid(GoogleJsonWebSignature.Payload payload)
    {
      bool valid = false;

      if (!payload.EmailVerified) return valid;

      valid = true; 

      return valid;

    }

  }

}
