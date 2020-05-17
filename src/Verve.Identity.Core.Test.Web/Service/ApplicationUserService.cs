using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Verve.Identity.Core.Model;
using Verve.Identity.Core.Service;
using Verve.Identity.Core.Service.Abstraction;
using Verve.Identity.Core.Test.ApplicationDbContext;
using Verve.Identity.Core.Test.Entity;

namespace Verve.Identity.Core.Test.Web.Service
{
    public class ApplicationUserService : VerveIdentityService<TestApplicationDbContext, UserAccount, VerveRole>
    {
        public ApplicationUserService(IVerveRoleStore<VerveRole> roleStore, 
            TestApplicationDbContext verveIdentityDbContext, 
            IdentityErrorDescriber identityErrorDescriber, 
            ILogger<VerveIdentityService<TestApplicationDbContext, UserAccount, VerveRole>> logger) : base(roleStore, verveIdentityDbContext, identityErrorDescriber, logger)
        {
        }
    }
}