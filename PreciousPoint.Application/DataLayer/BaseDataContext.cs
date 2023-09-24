using System;
using Microsoft.EntityFrameworkCore;
using PreciousPoint.Models.DataModel.Account;

namespace PreciousPoint.Application.DataLayer
{
  public class BaseDataContext : DbContext
  {
    public BaseDataContext(DbContextOptions<BaseDataContext> options) :base(options)
    {
    }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.Entity<User>()
                    .HasMany(ur => ur.Roles)
                    .WithOne(u => u.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();

      modelBuilder.Entity<Role>()
              .HasMany(ur => ur.UserRoles)
              .WithOne(u => u.Role)
              .HasForeignKey(ur => ur.RoleId)
              .IsRequired();
    }
  }
}

