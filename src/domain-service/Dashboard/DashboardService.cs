using AutoMapper;
using dal_villa.Context;
using domain_business.Core.Transaction.Providers;
using domain_business.Usecases.Dashboard;
using domain_extensions.Core.Result;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace domain_service.Dashboard
{
  public class DashboardService
  {

    #region ioc
    private readonly ILogger<DashboardService> _logger;
    private readonly IMapper _mapper;
    private readonly VillaContext _context;
    #endregion ioc

    #region constructor
    public DashboardService(
      ILogger<DashboardService> logger,
      IMapper mapper,
      VillaContext context
    )
    {

      _logger = logger;
      _context = context;
      _mapper = mapper;

    }
    #endregion constructor

    public Result<DashboardResponse> BaseData()
    {
      var tran = _context.Transactions.Take(10).AsEnumerable();
      var dashboardList = _mapper.Map<IEnumerable<QueryListTransaction>>(tran);

      return Result<DashboardResponse>.OK(
        new DashboardResponse { Transactions = dashboardList }
      );

    }

  }

}
