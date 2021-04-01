using domain_business.Core.Product;
using domain_business.Core.Category;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using domain_business.Core.User;

#nullable disable
namespace domain_business.Core.Transaction
{
  [Table("Transactions")]
  public class TransactionEntity
  {

    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long TransactionID { get; set; }

    public string ProviderTransactionID { get; set; }

    public string Description { get; set; }
    public string Notes { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }


    public int CategoryID { get; set; }
    public CategoryEntity Category { get; set; }

    public long ProductID { get; set; }
    public ProductEntity Product { get; set; }

    public long UserID { get; set; }
    public UserEntity User { get; set; }

  }
}
