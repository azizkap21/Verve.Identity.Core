using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Verve.Identity.Core.Service.Abstraction;
using Verve.Identity.Core.Model;
using Verve.Identity.Core.IdentityContext;
using System.Data.SqlClient;

namespace Verve.Identity.Core.Service

{
    /// <summary>
    /// An abstract implementation of <see cref="IVerveIdentityService{TUser}"/>
    /// </summary>
    /// <typeparam name="TDbContext">
    /// The type used to represent <see cref="VerveIdentityDbContext{TUser,TRole}"/>,
    /// which should provide db sets for <typeparamref name="TUser"/> of type <see cref="VerveUserAccount"/> and <typeparamref name="TRole"/> of type <see cref="VerveRole"/>
    /// </typeparam>
    /// <typeparam name="TUser">The type used to represent <see cref="VerveUserAccount"/></typeparam>
    /// <typeparam name="TRole">The type used to represent <see cref="VerveRole"/></typeparam>
    public abstract class VerveIdentityService<TDbContext, TUser, TRole> : IVerveIdentityService<TUser>
        where TDbContext : VerveIdentityDbContext<TUser, TRole>
        where TUser : VerveUserAccount
        where TRole : VerveRole, new()
    {
        private readonly IVerveRoleStore<TRole> _roleStore;
        private readonly TDbContext _verveIdentityDbContext;
        private readonly IdentityErrorDescriber _identityErrorDescriber;
        private readonly ILogger _logger;

        /// <summary>
        /// Creates a new instance of <see cref="VerveIdentityService{TDbContext,TUser,TRole}"/>
        /// </summary>
        /// <param name="roleStore"></param>
        /// <param name="verveIdentityDbContext"></param>
        /// <param name="identityErrorDescriber"></param>
        /// <param name="logger"></param>
        protected VerveIdentityService(IVerveRoleStore<TRole> roleStore,
                            TDbContext verveIdentityDbContext,
                            IdentityErrorDescriber identityErrorDescriber,
                            ILogger<VerveIdentityService<TDbContext, TUser, TRole>> logger)
        {

            _roleStore = roleStore;
            _verveIdentityDbContext = verveIdentityDbContext;
            _identityErrorDescriber = identityErrorDescriber;

            _logger = logger;
        }

        public virtual Task SetPasswordHashAsync(TUser user, string passwordHash, CancellationToken cancellationToken)
            => Task.FromResult(user.PasswordHash = passwordHash);

        public virtual Task<string> GetPasswordHashAsync(TUser user, CancellationToken cancellationToken)
            => Task.FromResult(user.PasswordHash);

        public virtual Task<bool> HasPasswordAsync(TUser user, CancellationToken cancellationToken)
            => Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));

        public virtual Task SetSecurityStampAsync(TUser user, string stamp, CancellationToken cancellationToken)
            => Task.FromResult(user.SecurityStamp = stamp);

        public virtual Task<string> GetSecurityStampAsync(TUser user, CancellationToken cancellationToken)
            => Task.FromResult(user.SecurityStamp);

        public virtual Task SetPhoneNumberAsync(TUser user, string phoneNumber, CancellationToken cancellationToken)
            => Task.FromResult(user.PhoneNumber = phoneNumber);

        public virtual Task<string> GetPhoneNumberAsync(TUser user, CancellationToken cancellationToken)
            => Task.FromResult(user.PhoneNumber);

        public virtual Task<bool> GetPhoneNumberConfirmedAsync(TUser user, CancellationToken cancellationToken)
            => Task.FromResult(user.PhoneNumberConfirmed);

        public virtual Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed, CancellationToken cancellationToken)
            => Task.FromResult(user.PhoneNumberConfirmed = confirmed);

        public virtual Task<DateTimeOffset?> GetLockoutEndDateAsync(TUser user, CancellationToken cancellationToken)
            => Task.FromResult(user.LockoutEnd);

        public virtual Task SetLockoutEndDateAsync(TUser user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken)
            => Task.FromResult(user.LockoutEnd = lockoutEnd);

        public virtual Task<int> IncrementAccessFailedCountAsync(TUser user, CancellationToken cancellationToken)
            => Task.FromResult(user.AccessFailedCount++);

        public virtual Task ResetAccessFailedCountAsync(TUser user, CancellationToken cancellationToken)
            => Task.FromResult(user.AccessFailedCount = 0);

        public virtual Task<int> GetAccessFailedCountAsync(TUser user, CancellationToken cancellationToken)
            => Task.FromResult(user.AccessFailedCount);

        public virtual Task<bool> GetLockoutEnabledAsync(TUser user, CancellationToken cancellationToken)
            => Task.FromResult(user.LockoutEnabled);

        public virtual Task SetLockoutEnabledAsync(TUser user, bool enabled, CancellationToken cancellationToken)
            => Task.FromResult(user.LockoutEnabled = true);

        public async Task AddToRoleAsync(TUser user, string roleName, CancellationToken cancellationToken)
        {
            var role = await _roleStore.FindByRoleNameAsync(roleName, cancellationToken);
            if (role == null)
            {
                var result = await _roleStore.CreateAsync(new TRole()
                {
                    Name = roleName,
                    NormalizedName = roleName.NormalizedString(),

                }, cancellationToken);

                if (result != IdentityResult.Success)
                {
                    throw new Exception("Not able to create 'User' Role");
                }

                role = await _roleStore.FindByRoleNameAsync(roleName, cancellationToken);
            }
            var userRoleIds = await _verveIdentityDbContext.UserRoleMappings.Where(x => x.UserId == user.Id)
                .AsNoTracking()
                .Select(x => x.RoleId)
                .ToListAsync(cancellationToken);

            if (userRoleIds.Contains(role.Id))
            {
                return;
            }

            _verveIdentityDbContext.Add(new VerveUserRoleMapping
            {
                Id = Guid.NewGuid(),
                RoleId = role.Id,
                UserId = user.Id
            });

            await _verveIdentityDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveFromRoleAsync(TUser user, string roleName, CancellationToken cancellationToken)
        {
            var role = await _roleStore.FindByRoleNameAsync(roleName, cancellationToken);
            var userRole = await _verveIdentityDbContext.UserRoleMappings.FirstOrDefaultAsync(x => x.UserId == user.Id && x.RoleId == role.Id, cancellationToken);

            if (userRole == null)
            {
                return;
            }

            _verveIdentityDbContext.UserRoleMappings.Remove(userRole);

            await _verveIdentityDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IList<string>> GetRolesAsync(TUser user, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException($"{nameof(GetRolesAsync)} is canceled");
            }

            var userRoleIds = await _verveIdentityDbContext.UserRoleMappings.Where(x => x.UserId == user.Id)
                .AsNoTracking()
                .Select(x => x.RoleId)
                .ToListAsync(cancellationToken);

            var roleNames = await _verveIdentityDbContext.Roles.Where(r => userRoleIds.Contains(r.Id))
                .AsNoTracking()
                .Select(n => n.Name)
                .ToListAsync(cancellationToken);

            return roleNames;
        }

        public async Task<bool> IsInRoleAsync(TUser user, string roleName, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException($"{nameof(IsInRoleAsync)} is canceled");
            }
            var role = await _roleStore.FindByRoleNameAsync(roleName, cancellationToken);
            var userRole = await _verveIdentityDbContext.UserRoleMappings.FirstOrDefaultAsync(x => x.UserId == user.Id && x.RoleId == role.Id, cancellationToken);

            return userRole != null;
        }

        public async Task<IList<TUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException($"{nameof(GetUsersInRoleAsync)} is canceled");
            }
            var role = await _roleStore.FindByRoleNameAsync(roleName, cancellationToken);
            var userIds = await _verveIdentityDbContext.UserRoleMappings.Where(x => x.RoleId == role.Id)
                .AsNoTracking()
                .Select(x => x.UserId)
                .ToListAsync(cancellationToken);

            return await _verveIdentityDbContext.UserAccounts.Where(x => userIds.Contains(x.Id))
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public virtual Task<string> GetUserIdAsync(TUser user, CancellationToken cancellationToken)
            => Task.FromResult(user.Id.ToString());

        public virtual Task<string> GetUserNameAsync(TUser user, CancellationToken cancellationToken)
           => Task.FromResult(user.UserName);

        public virtual Task SetUserNameAsync(TUser user, string userName, CancellationToken cancellationToken)
            => Task.FromResult(user.UserName = userName);

        public virtual Task<string> GetNormalizedUserNameAsync(TUser user, CancellationToken cancellationToken)
            => Task.FromResult(user.NormalizedUserName);

        public virtual Task SetNormalizedUserNameAsync(TUser user, string normalizedName, CancellationToken cancellationToken)
            => Task.FromResult(user.NormalizedUserName = normalizedName);

        public virtual async Task<IdentityResult> CreateAsync(TUser user, CancellationToken cancellationToken)
        {
            var existingUserByName = await FindByNameAsync(user.NormalizedUserName, cancellationToken);

            if (existingUserByName != null)
            {
                _logger.LogWarning("User will not be created because this user name '{UserName}' already exists. Normalized User name '{NormalizedUserName}'", user.UserName, user.NormalizedUserName);

                return IdentityResult.Failed(_identityErrorDescriber.DuplicateUserName(user.UserName));
            }
            var existingUserByEmail = await FindByEmailAsync(user.NormalizedEmail, cancellationToken);

            if (existingUserByEmail != null)
            {
                _logger.LogWarning("User can be created because this email '{Email}' already exists. Normalized email '{NormalizedEmail}'", user.NormalizedEmail, user.Email);

                return IdentityResult.Failed(_identityErrorDescriber.DuplicateEmail(user.Email));
            }

            try
            {
                _verveIdentityDbContext.UserAccounts.Attach(user);
                _verveIdentityDbContext.Entry(user).State = EntityState.Added;

                await _verveIdentityDbContext.SaveChangesAsync(cancellationToken);
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError(sqlEx,"An Error occurred while trying to create new user {UserId}, {UserName}", user.Id, user.Email);
                return IdentityResult.Failed(new IdentityError() { Code = "SQL_Error", Description = sqlEx.Message });
            }

            await AddToRoleAsync(user, "User", cancellationToken);

            _logger.LogInformation("New user is created with id '{Id}', user name '{UserName}' and email '{Email}'", user.Id, user.UserName, user.Email);

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(TUser user, CancellationToken cancellationToken)
        {
            var existingUserByName = await FindByNameAsync(user.NormalizedUserName, cancellationToken);

            if (existingUserByName != null && existingUserByName.Id != user.Id)
            {
                _logger.LogWarning("User can not be updated because this user name '{UserName}' already exists. Normalized User name '{NormalizedUserName}'", user.UserName, user.NormalizedUserName);

                return IdentityResult.Failed(_identityErrorDescriber.DuplicateUserName(user.UserName));
            }
            var existingUserByEmail = await FindByEmailAsync(user.NormalizedEmail, cancellationToken);

            if (existingUserByName != null && existingUserByEmail.Id != user.Id)
            {
                _logger.LogWarning("User can not be updated because this email '{Email}' already exists. Normalized email '{NormalizedEmail}'", user.NormalizedEmail, user.Email);
                return IdentityResult.Failed(_identityErrorDescriber.DuplicateEmail(user.Email));
            }

            try
            {
                _verveIdentityDbContext.Update(user);
                _verveIdentityDbContext.Entry(user).State = EntityState.Modified;

                await _verveIdentityDbContext.SaveChangesAsync(cancellationToken);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex,"An Error occurred while trying to update user {UserId}, {UserName}", user.Id, user.Email);

                return IdentityResult.Failed(new IdentityError() { Code = "SQL_Error", Description = ex.Message });
            }
            

            _logger.LogInformation("User updated with id '{Id}', user name '{UserName}' and email '{Email}'", user.Id, user.UserName, user.Email);

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(TUser user, CancellationToken cancellationToken)
        {
            _verveIdentityDbContext.UserAccounts.Remove(user);
            await _verveIdentityDbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("User with id '{Id}', user name '{UserName}' and email '{Email}' has been deleted", user.Id, user.UserName, user.Email);

            return IdentityResult.Success;

        }

        public virtual Task<TUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
            => Guid.TryParse(userId, out var id)
            ? _verveIdentityDbContext.UserAccounts.FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
            : throw new Exception("User Id is not in correct format. Required Guid.");

        public virtual Task<TUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
            => _verveIdentityDbContext.UserAccounts.FirstOrDefaultAsync(x => x.NormalizedUserName == normalizedUserName, cancellationToken);

        public void Dispose()
        {

        }

        public virtual Task SetEmailAsync(TUser user, string email, CancellationToken cancellationToken)
            => Task.FromResult(user.Email = email);

        public virtual Task<string> GetEmailAsync(TUser user, CancellationToken cancellationToken)
            => Task.FromResult(user.Email);

        public virtual Task<bool> GetEmailConfirmedAsync(TUser user, CancellationToken cancellationToken)
            => Task.FromResult(user.EmailConfirmed);

        public virtual Task SetEmailConfirmedAsync(TUser user, bool confirmed, CancellationToken cancellationToken)
            => Task.FromResult(user.EmailConfirmed);

        public virtual Task<TUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
            => _verveIdentityDbContext.UserAccounts.FirstOrDefaultAsync(e => e.NormalizedEmail == normalizedEmail, cancellationToken);

        public virtual Task<string> GetNormalizedEmailAsync(TUser user, CancellationToken cancellationToken)
            => Task.FromResult(user.NormalizedEmail);

        public virtual Task SetNormalizedEmailAsync(TUser user, string normalizedEmail, CancellationToken cancellationToken)
            => Task.FromResult(user.NormalizedEmail = normalizedEmail);
    }
}
