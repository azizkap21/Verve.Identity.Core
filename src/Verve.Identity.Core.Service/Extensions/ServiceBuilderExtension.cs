using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Verve.Identity.Core.IdentityContext;
using Verve.Identity.Core.Model;
using Verve.Identity.Core.Service.Abstraction;

namespace Verve.Identity.Core.Service.Extensions
{
    public static class ServiceBuilderExtension
    {
        public static IdentityBuilder AddVerveIdentity<TUser, TRole>(this IServiceCollection services)
            where TUser : VerveUserAccount
            where TRole : VerveRole
        {
            return services.AddIdentity<TUser, TRole>()
                 .AddErrorDescriber<IdentityErrorDescriber>();

        }

        public static IServiceCollection AddVerveIdentityServices<TDbContext, TUser, TRole>(this IServiceCollection services)
            where TUser : VerveUserAccount
            where TRole : VerveRole
            where TDbContext: VerveIdentityDbContext<TUser, TRole>

        {
            services.AddScoped<ILookupNormalizer, NormolizationHandler>();
            services.AddScoped<IPasswordHasher<TUser>, PasswordHasher<TUser>>();
            services.AddScoped<IVerveIdentityService<TUser>, VerveIdentityService<TDbContext, TUser, TRole>>();
            services.AddScoped<IUserPhoneNumberStore<TUser>, VerveIdentityService<TDbContext, TUser, TRole>>();
            services.AddScoped<IUserEmailStore<TUser>, VerveIdentityService<TDbContext, TUser, TRole>>();
            services.AddScoped<IUserSecurityStampStore<TUser>, VerveIdentityService<TDbContext, TUser, TRole>>();
            services.AddScoped<IUserPasswordStore<TUser>, VerveIdentityService<TDbContext, TUser, TRole>>();
            services.AddScoped<IVerveRoleStore<TRole>, VerveRoleStore<TDbContext, TUser, TRole>>();
            services
                .AddScoped<IUserStore<TUser>,
                    VerveIdentityService<TDbContext, TUser, TRole>>();
            services
                .AddScoped<IRoleStore<TRole>, VerveRoleStore<TDbContext, TUser, TRole>>();
            services.AddScoped<UserManager<TUser>>();
            services.AddScoped<IdentityErrorDescriber>();
            return services;
        }
    }
}
