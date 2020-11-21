using domain_business.Core.Account;
using domain_business.Core.Category;
using domain_business.Core.Transaction;
using Microsoft.EntityFrameworkCore;
using System;

namespace dal_villa.Context
{

  public class VillaContext : DbContext
  {

    public VillaContext(DbContextOptions<VillaContext> options) 
      : base(options) 
    {
      Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountEntity>();
        modelBuilder.Entity<TransactionEntity>();
        modelBuilder.Entity<CategoryEntity>();
    }

    public DbSet<AccountEntity> Accounts { get; set; }
    public DbSet<TransactionEntity> Transactions { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }

  }

}
