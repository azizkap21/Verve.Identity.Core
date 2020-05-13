using System;

namespace Verve.Identity.Core.Model
{
    public class VerveUserRoleMapping
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid RoleId { get; set; }
    }
}
