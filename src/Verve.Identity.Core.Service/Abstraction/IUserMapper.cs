using Verve.Identity.Core.Model;

namespace Verve.Identity.Core.Service.Abstraction
{
    public interface IUserMapper<out TUser>
        where TUser : VerveUserAccount
    {

        TUser ConvertToUser(VerveUserAccount verUserAccount);

    }
}