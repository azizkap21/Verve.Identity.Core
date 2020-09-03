using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Verve.Identity.Core.IdentityContext;
using Verve.Identity.Core.Model;
using Verve.Identity.Core.Service.Abstraction;

namespace Verve.Identity.Core.Service.Extensions
{
    public static class ServiceBuilderExtension
    {
        /// <summary>
        /// IdentityBuilder extension to add identity
        /// </summary>
        /// <typeparam name="TUser">User model inherited from VerveUserAccount or itself</typeparam>
        /// <typeparam name="TRole">Role model inherited from VerveRole or itself</typeparam>
        /// <param name="services">IServiceCollection services</param>
        /// <returns>IServiceCollection services</returns>
        public static IdentityBuilder AddVerveIdentity<TUser, TRole>(this IServiceCollection services)
            where TUser : VerveUserAccount
            where TRole : VerveRole
        {
            return services.AddIdentity<TUser, TRole>()
                 .AddErrorDescriber<IdentityErrorDescriber>();
        }

        /// <summary>
        /// An IserviceCollection Extension to inject required service for Asp.net Core Identity
        /// </summary>
        /// <typeparam name="TDbContext">DbContext which is inherited from VerveIdentityDbContext<TUser, TRole> or itself</typeparam>
        /// <typeparam name="TUser">User model inherited from VerveUserAccount or itself</typeparam>
        /// <typeparam name="TRole">Role model inherited from VerveRole or itself</typeparam>
        /// <typeparam name="TIdentityImpl"></typeparam>
        /// <param name="services">IServiceCollection services</param>
        /// <returns>IServiceCollection services</returns>
        public static IServiceCollection AddVerveIdentityServices<TIdentityImpl, TDbContext, TUser, TRole>(this IServiceCollection services)
            where TIdentityImpl : VerveIdentityService<TDbContext, TUser, TRole>
            where TUser : VerveUserAccount
            where TRole : VerveRole, new()
            where TDbContext : VerveIdentityDbContext<TUser, TRole>

        {
            services.AddScoped<ILookupNormalizer, NormolizationHandler>();
            services.AddScoped<IPasswordHasher<TUser>, PasswordHasher<TUser>>();
            services.AddScoped<IVerveIdentityService<TUser>, TIdentityImpl>();
            services.AddScoped<IUserPhoneNumberStore<TUser>, TIdentityImpl>();
            services.AddScoped<IUserEmailStore<TUser>, TIdentityImpl>();
            services.AddScoped<IUserSecurityStampStore<TUser>, TIdentityImpl>();
            services.AddScoped<IUserPasswordStore<TUser>, TIdentityImpl>();
            services.AddScoped<IVerveRoleStore<TRole>, VerveRoleStore<TDbContext, TUser, TRole>>();
            services
                .AddScoped<IUserStore<TUser>, TIdentityImpl>();
            services
                .AddScoped<IRoleStore<TRole>, VerveRoleStore<TDbContext, TUser, TRole>>();
            services.AddScoped<UserManager<TUser>>();
            services.AddScoped<IdentityErrorDescriber>();
            return services;
        }
    }
}
