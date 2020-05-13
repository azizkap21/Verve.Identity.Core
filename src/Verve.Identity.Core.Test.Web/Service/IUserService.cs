using System.Threading.Tasks;
using Verve.Identity.Core.Test.Web.Request;
using Verve.Identity.Core.Test.Web.Response;

namespace Verve.Identity.Core.Test.Web.Service
{
    public interface IUserService
    {
        Task<UserRegisterationResponse> Register(UserRegistrationRequest registrationRequest);
        Task<LoginResponse> LoginAsync(LoginRequest loginRequest);
    }
}