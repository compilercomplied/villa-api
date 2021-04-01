using domain_business.Core.Product;
using domain_business.Core.Category;
using domain_business.Core.Transaction;
using Microsoft.EntityFrameworkCore;
using System;
using domain_business.Core.User;

#nullable disable
namespace dal_villa.Context
{

  public class VillaContext : DbContext
  {

    public VillaContext(DbContextOptions<VillaContext> options)
      : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductEntity>();
        modelBuilder.Entity<TransactionEntity>();
        modelBuilder.Entity<CategoryEntity>();
        modelBuilder.Entity<UserEntity>();
    }

    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<TransactionEntity> Transactions { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<UserEntity> Users { get; set; }

  }

}
