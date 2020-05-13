using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Verve.Identity.Core.Model;

namespace Verve.Identity.Core.IdentityContext
{
    public class VerveIdentityDbContext<TUser, TRole> : DbContext
        where TUser : VerveUserAccount
        where TRole : VerveRole
    {
        public VerveIdentityDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<TUser> UserAccounts { get; set; }

        public DbSet<TRole> Roles { get; set; }

        public DbSet<VerveUserRoleMapping> UserRoleMappings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TUser>().HasKey(x => x.Id);
            modelBuilder.Entity<TRole>().HasKey(x => x.Id);
            modelBuilder.Entity<VerveUserRoleMapping>().HasKey(x => x.Id);
            modelBuilder.Entity<VerveUserRoleMapping>().ToTable("UserRoles");
        }

    }
}
