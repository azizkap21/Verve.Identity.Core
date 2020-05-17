using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Verve.Identity.Core.Model;
using Verve.Identity.Core.Service;
using Verve.Identity.Core.Service.Abstraction;
using Verve.Identity.Core.Test.ApplicationDbContext;
using Verve.Identity.Core.Test.Entity;
using Verve.Identity.Core.Test.Web.Service;
using Xunit;

namespace Verve.Identity.Core.Tests.Service
{
    public class ApplicationUserServiceTests
    {
        private readonly MockRepository _mockRepository;

        private readonly Mock<IVerveRoleStore<VerveRole>> _mockVerveRoleStore;
        private readonly TestApplicationDbContext _mockTestApplicationDbContext;
        private readonly Mock<IdentityErrorDescriber> _mockIdentityErrorDescriber;
        private readonly Mock<ILogger<VerveIdentityService<TestApplicationDbContext, UserAccount, VerveRole>>> _mockLogger;

        public ApplicationUserServiceTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);

            _mockVerveRoleStore = this._mockRepository.Create<IVerveRoleStore<VerveRole>>();
            _mockTestApplicationDbContext = MockDbContext.Create();
            _mockIdentityErrorDescriber = this._mockRepository.Create<IdentityErrorDescriber>();
            _mockLogger = this._mockRepository.Create<ILogger<VerveIdentityService<TestApplicationDbContext, UserAccount, VerveRole>>>();
        }

        private ApplicationUserService CreateService()
        {
            return new ApplicationUserService(
                _mockVerveRoleStore.Object,
                _mockTestApplicationDbContext,
                _mockIdentityErrorDescriber.Object,
                _mockLogger.Object);
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var service = this.CreateService();

            // Act


            // Assert
            Assert.True(false);
            this._mockRepository.VerifyAll();
        }
    }
}
