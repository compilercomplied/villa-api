using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace domain_business.Core.Product.Providers
{

  public class ProviderAccount
  {

    public long AccountID { get; set; }
    public string Number { get; set; }
    public string Description { get; set; }
    public string ProviderID { get; set; }

  }

}
