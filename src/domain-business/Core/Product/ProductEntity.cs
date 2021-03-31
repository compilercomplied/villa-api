using domain_business.Core.Transaction;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable
namespace domain_business.Core.Product
{

  [Table("Products")]
  public class ProductEntity
  {

    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long InternalProductID { get; set; }
    public string Description { get; set; }
    public string ProductID { get; set; }
    public string ProviderID { get; set; }

    public IList<TransactionEntity> Transactions { get; set; }

  }

}
