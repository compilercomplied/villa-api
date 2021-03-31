using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using domain_business.Usecases.Auth_flow;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace villa_api.Controllers
{

  public abstract class ABCController : ControllerBase
  {

    protected UserData BuildUser()
    {

      var user = new UserData 
      {
        Subject = User.Identity.Name ?? string.Empty,
      };

      return user;

    }

  }

}
