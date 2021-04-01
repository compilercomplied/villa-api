using domain_business.Core.Transaction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

#nullable disable
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

    public IList<TransactionEntity> Transactions { get; set; }

  }

}
