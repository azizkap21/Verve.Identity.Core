using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Verve.Identity.Core.Service.Abstraction;
using Verve.Identity.Core.IdentityContext;
using Verve.Identity.Core.Model;

namespace Verve.Identity.Core.Service
{
    public class VerveRoleStore<TDbContext, TUser, TRole> : IVerveRoleStore<TRole>
        where TDbContext: VerveIdentityDbContext<TUser, TRole>
        where TUser: VerveUserAccount
        where TRole : VerveRole
    {
        private readonly TDbContext _verveIdentityDbContext;
        private readonly IdentityErrorDescriber _identityErrorDescriber;
        private readonly ILogger<VerveRoleStore<TDbContext, TUser, TRole>> _logger;
        
        public VerveRoleStore(TDbContext verveIdentityDbContext,
                    IdentityErrorDescriber identityErrorDescriber,
                    ILogger<VerveRoleStore<TDbContext, TUser, TRole>> logger)
        {
            _verveIdentityDbContext = verveIdentityDbContext;
            _identityErrorDescriber = identityErrorDescriber;
            _logger = logger;
        }

        public async Task<IdentityResult> CreateAsync(TRole role, CancellationToken cancellationToken)
        {
            var existingRole = await _verveIdentityDbContext.Roles.FirstOrDefaultAsync(x => x.NormalizedName == role.NormalizedName, cancellationToken);
            if (existingRole != null)
            {
                _logger.LogWarning("Attempting to add new role with the normalized role name: '{NormalizedRoleName}' already exists in db.", role.NormalizedName);
                return IdentityResult.Failed(new IdentityError[] { _identityErrorDescriber.DuplicateRoleName(role.Name) });
            }

            _verveIdentityDbContext.Roles.Add(role);

            await _verveIdentityDbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("New role has been added with normalized role name: '{NormalizedRoleName}' and id: '{Id}'", role.NormalizedName, role.Id);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(TRole role, CancellationToken cancellationToken)
        {
            var existingRole = await _verveIdentityDbContext.Roles.FirstOrDefaultAsync(x => x.NormalizedName == role.NormalizedName, cancellationToken);

            if (existingRole != null && existingRole.Id != role.Id)
            {
                _logger.LogWarning("Attempting to update new normalized role name with the '{NormalizedRoleName}' already exists in db.", role.NormalizedName);
                return IdentityResult.Failed(new IdentityError[] { _identityErrorDescriber.DuplicateRoleName(role.Name) });
            }

            _verveIdentityDbContext.Update(role);
            _verveIdentityDbContext.Entry(role).State = EntityState.Modified;
            await _verveIdentityDbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("New role has been updated with normalized role name: '{NormalizedRoleName}' and id: '{Id}'", role.NormalizedName, role.Id);

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(TRole role, CancellationToken cancellationToken)
        {
            _verveIdentityDbContext.Roles.Remove(role);
            await _verveIdentityDbContext.SaveChangesAsync(cancellationToken);
            return IdentityResult.Success;
        }

        public Task<string> GetRoleIdAsync(TRole role, CancellationToken cancellationToken)
            => Task.FromResult(role.Id.ToString());

        public Task<string> GetRoleNameAsync(TRole role, CancellationToken cancellationToken)
            => Task.FromResult(role.Name);

        public Task SetRoleNameAsync(TRole role, string roleName, CancellationToken cancellationToken)
            => Task.FromResult(role.Name = roleName);

        public Task<string> GetNormalizedRoleNameAsync(TRole role, CancellationToken cancellationToken)
            => Task.FromResult(role.NormalizedName);

        public Task SetNormalizedRoleNameAsync(TRole role, string normalizedName, CancellationToken cancellationToken)
            => Task.FromResult(role.NormalizedName = normalizedName);

        public async Task<TRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
            => Guid.TryParse(roleId, out var id)
                    ? await _verveIdentityDbContext.Roles.FirstOrDefaultAsync(r => r.Id == id, cancellationToken)
                    : throw (new Exception($"{roleId} is not a Guid"));
        public async Task<TRole> FindByRoleNameAsync(string roleName, CancellationToken cancellationToken)
        {
            var normalizedRoleName = roleName.NormalizedString();
            return await FindByNameAsync(normalizedRoleName, cancellationToken);
        }
        public async Task<TRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
            => await _verveIdentityDbContext.Roles.FirstOrDefaultAsync(r => r.NormalizedName == normalizedRoleName, cancellationToken);

      
        public void Dispose()
        {
           
        }
    }
}
