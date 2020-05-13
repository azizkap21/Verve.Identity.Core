using System;

namespace Verve.Identity.Core.Test.Web.Response
{
    public class UserRegisterationResponse
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }

        public string PhoneNo { get; set; }

    }
}