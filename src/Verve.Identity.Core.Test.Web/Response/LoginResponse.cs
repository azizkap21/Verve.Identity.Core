using System.Collections.Generic;
using System.Security.Claims;
using Verve.Identity.Core.Model;

namespace Verve.Identity.Core.Test.Web.Response
{
    public class LoginResponse
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string AccessToken { get; set; }

        public IList<string> Roles { get; set; }

        public List<Claim> Claims { get; set; }
    }
}