using System;
using System.Collections.Generic;
using System.Text;

namespace domain_business.Core.Account.Providers
{

  public class ProviderAccount
  {

    public long AccountID { get; set; }
    public string Number { get; set; }
    public string Description { get; set; }
    public string ProviderID { get; set; }

  }

}
