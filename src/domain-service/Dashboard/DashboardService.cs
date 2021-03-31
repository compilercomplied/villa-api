using AutoMapper;
using dal_villa.Context;
using domain_business.Core.Product;
using domain_business.Core.Transaction;
using domain_business.Usecases.Auth_flow;
using domain_business.Usecases.Dashboard;
using domain_extensions.Core.Result;
using domain_service.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace domain_service.Dashboard
{
  public class DashboardService
  {

    const int PAGE_SIZE = 10;


    // -------------------------------------------------------------------------
    #region ioc
    private readonly ILogger<DashboardService> _logger;
    private readonly IMapper _mapper;
    private readonly VillaContext _context;
    private readonly UserService _user;
    #endregion ioc

    #region constructor
    public DashboardService(
      ILogger<DashboardService> logger,
      IMapper mapper,
      VillaContext context,
      UserService user
    )
    {

      _logger = logger;
      _context = context;
      _mapper = mapper;
      _user = user;

    }
    #endregion constructor
    // -------------------------------------------------------------------------


    public Result<DashboardResponse> BaseData(UserData userData)
    {

      var query = _context.Users
        .Where(u => u.Subject == userData.Subject)
        .Select(s => new
        {
          Tran = s.Transactions.Take(PAGE_SIZE),
          Products = s.Products,
        })
        .FirstOrDefault();


      return Result<DashboardResponse>.OK(new DashboardResponse
      {
        Products =  _mapper
          .Map<IEnumerable<DashboardProducts>>(query.Products),
        Transactions = _mapper
          .Map<IEnumerable<QueryListTransaction>>(query.Tran),
      });

    }

    public Result<QueryListTransaction[]> QueryTransactions(
      TransactionListingRequest req
    )
    {
      var tran = _context.Transactions
        .Skip(req.SkipCount)
        .Take(10)
        .AsEnumerable();

      var dashboardList = _mapper.Map<IEnumerable<QueryListTransaction>>(tran);

      return Result<QueryListTransaction[]>.OK(dashboardList.ToArray());

    }

  }

}
