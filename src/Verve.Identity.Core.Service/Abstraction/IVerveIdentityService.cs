
using Microsoft.AspNetCore.Identity;


namespace Verve.Identity.Core.Service.Abstraction
{
    public interface IVerveIdentityService<TUser> : IUserStore<TUser>,
                                    IUserPasswordStore<TUser>,
                                    IPasswordHasher<TUser>,
                                    IUserSecurityStampStore<TUser>,
                                    IUserEmailStore<TUser>,
                                    IUserPhoneNumberStore<TUser>,
                                    IUserLockoutStore<TUser>,
                                    IUserRoleStore<TUser>

                                    where TUser : class
    {
    }
}
