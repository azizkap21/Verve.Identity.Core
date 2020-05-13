using System;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Verve.Identity.Core.IdentityContext;
using Verve.Identity.Core.Model;
using Verve.Identity.Core.Test.Entity;

namespace Verve.Identity.Core.Test.ApplicationDbContext
{
    public class TestApplicationDbContext : VerveIdentityDbContext<UserAccount, VerveRole>
    {
        public TestApplicationDbContext(DbContextOptions options):base(options)
        {
                
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasKey(x => x.Id);
            modelBuilder.Entity<UserAccount>().ToTable("UserAccounts");
            modelBuilder.Entity<VerveRole>().ToTable("Roles");

        }
    }
}
