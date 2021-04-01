using domain_business.Core.Product;
using domain_business.Core.Transaction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

#nullable disable
namespace domain_business.Core.User
{

  [Table("Users")]
  public class UserEntity
  {

    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long UserID { get; set; }
    public string Subject { get; set; }
    public string Email { get; set; }


    public IList<ProductEntity> Products { get; set; }

    public IList<TransactionEntity> Transactions { get; set; }

  }

}
