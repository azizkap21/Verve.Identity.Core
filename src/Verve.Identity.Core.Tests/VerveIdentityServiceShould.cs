using System;
using System.Diagnostics;
using Verve.Identity.Core.Test.ApplicationDbContext;
using Xunit;
using AutoFixture;
using Moq;

namespace Verve.Identity.Core.Tests
{
    public class VerveIdentityServiceShould
    {
        private readonly TestApplicationDbContext _testAccountDbContext;

        public VerveIdentityServiceShould()
        {
            _testAccountDbContext = MockDbContext.Create();
            MockDbContext.SeedRoles(_testAccountDbContext);
        }

        [Fact]
        public void CreateNewUser()
        {
           
        }
    }
}
