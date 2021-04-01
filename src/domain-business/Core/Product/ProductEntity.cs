using domain_business.Core.Transaction;
using domain_business.Core.User;
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
    public long ProductID { get; set; }
    public string Description { get; set; }
    public string ProviderID { get; set; }

    public IList<TransactionEntity> Transactions { get; set; }

    public long UserID { get; set; }
    public UserEntity User { get; set; }

  }

}
