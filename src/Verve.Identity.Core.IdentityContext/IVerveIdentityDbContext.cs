using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Verve.Identity.Core.Model;

namespace Verve.Identity.Core.IdentityContext
{
    public interface IVerveIdentityDbContext<TUser, TRole>
        where TUser : VerveUserAccount
        where TRole : VerveRole

    {
        DbSet<TUser> UserAccounts { get; set; }
        DbSet<TRole> Roles { get; set; }
        DbSet<VerveUserRoleMapping> UserRolesMappings { get; set; }

        Task<int> SaveChangesAsync<TEntity>(TEntity entity)
            where TEntity: class;
    }
}