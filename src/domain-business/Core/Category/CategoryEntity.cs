using domain_business.Core.Transaction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace domain_business.Core.Category
{

  [Table("Categories")]
  public class CategoryEntity
  {

    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CategoryID { get; set; }
    public string Name { get; set; }
    public bool Hidden { get; set; }
    public string ProviderID { get; set; }

    public long TransactionID { get; set; }
    public List<TransactionEntity> Transactions { get; set; }

  }

}
