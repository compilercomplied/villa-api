using domain_business.Core.Transaction;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace domain_business.Core.Account
{

  [Table("Accounts")]
  public class AccountEntity
  {

    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long AccountID { get; set; }
    public string Description { get; set; }
    public string Number { get; set; }
    public string ProviderID { get; set; }

    public List<TransactionEntity> Transactions { get; set; }

  }

}
