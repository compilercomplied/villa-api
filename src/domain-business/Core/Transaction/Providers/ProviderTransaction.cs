using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace domain_business.Core.Transaction.Providers
{

  public class ProviderTransaction
  {

    public string ProductID { get; set; }
    public string TransactionID { get; set; }
    public string Description { get; set; }
    public string Notes { get; set; }
    public decimal Amount { get; set; }
    public string CategoryID { get; set; }
    public DateTime Date { get; set; }

  }

}
