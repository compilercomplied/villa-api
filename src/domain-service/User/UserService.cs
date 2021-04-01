using dal_villa.Context;
using domain_business.Core.User;
using domain_extensions.Core.Result;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace domain_service.User
{

  // TODO rsx
  public class UserService
  {

    #region ioc
    private readonly ILogger<UserService> _logger;
    private readonly VillaContext _context;
    #endregion ioc

    #region constructor
    public UserService(
      ILogger<UserService> logger,
      VillaContext context
    )
    {

      _logger = logger;
      _context = context;

    }
    #endregion constructor


    public UserEntity BySubject(string subject)
    { 
      var result = _context.Users.AsQueryable()
        .FirstOrDefault(u => u.Subject == subject);

      if (result == null) 
        throw OperationException.From($"No matching user found with subject {subject}");

      return result;
    }

}
