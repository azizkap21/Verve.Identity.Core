
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Verve.Identity.Core.Service.Abstraction
{
    public interface IVerveRoleStore<TRole> : IRoleStore<TRole>
        where TRole : class
    {
        Task<TRole> FindByRoleNameAsync(string roleName, CancellationToken cancellationToken);
    }
}
