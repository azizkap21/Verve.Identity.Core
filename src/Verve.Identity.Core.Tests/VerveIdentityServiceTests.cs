//using Microsoft.AspNetCore.Identity;
//using Microsoft.Extensions.Logging;
//using Moq;
//using System;
//using System.Threading;
//using System.Threading.Tasks;
//using Verve.Identity.Core.IdentityContext;
//using Verve.Identity.Core.Model;
//using Verve.Identity.Core.Service;
//using Verve.Identity.Core.Service.Abstraction;
//using Verve.Identity.Core.Test.ApplicationDbContext;
//using Verve.Identity.Core.Test.Entity;
//using Xunit;

//namespace Verve.Identity.Core.Tests
//{
//    public abstract class VerveIdentityServiceTests<TDbContext, TUser, TRole>
//        where TDbContext: VerveIdentityDbContext<TUser, TRole>
//        where TUser : VerveUserAccount
//        where TRole : VerveRole
//    {
//        private MockRepository mockRepository;

//        private Mock<IVerveRoleStore<VerveRole>> mockVerveRoleStore;
//        private TDbContext mockTDbContext;
//        private Mock<IdentityErrorDescriber> mockIdentityErrorDescriber;
//        private Mock<ILogger<VerveIdentityService<TestApplicationDbContext, UserAccount, VerveRole>>> mockLogger;

//        public VerveIdentityServiceTests()
//        {
//            this.mockRepository = new MockRepository(MockBehavior.Strict);
            
//            this.mockVerveRoleStore = this.mockRepository.Create<IVerveRoleStore<VerveRole>>();
//            this.mockTDbContext = MockDbContext.Create();
//            this.mockIdentityErrorDescriber = this.mockRepository.Create<IdentityErrorDescriber>();
//            this.mockLogger = this.mockRepository.Create<ILogger<VerveIdentityService<TestApplicationDbContext, UserAccount, VerveRole>>>();
//        }

//        private VerveIdentityService CreateService()
//        {
//            return new VerveIdentityService(
//                this.mockVerveRoleStore.Object,
//                this.mockTDbContext,
//                this.mockIdentityErrorDescriber.Object,
//                this.mockLogger.Object);
//        }

//        [Fact]
//        public async Task SetPasswordHashAsync_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            var service = this.CreateService();
//            TUser user = null;
//            string passwordHash = null;
//            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

//            // Act
//            await service.SetPasswordHashAsync(
//                user,
//                passwordHash,
//                cancellationToken);

//            // Assert
//            Assert.True(false);
//            this.mockRepository.VerifyAll();
//        }

//        [Fact]
//        public async Task GetPasswordHashAsync_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            var service = this.CreateService();
//            UserAccount user = null;
//            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

//            // Act
//            var result = await service.GetPasswordHashAsync(
//                user,
//                cancellationToken);

//            // Assert
//            Assert.True(false);
//            this.mockRepository.VerifyAll();
//        }

//        [Fact]
//        public async Task HasPasswordAsync_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            var service = this.CreateService();
//            UserAccount user = null;
//            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

//            // Act
//            var result = await service.HasPasswordAsync(
//                user,
//                cancellationToken);

//            // Assert
//            Assert.True(false);
//            this.mockRepository.VerifyAll();
//        }

//        [Fact]
//        public void HashPassword_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            var service = this.CreateService();
//            TUser user = null;
//            string password = null;

//            // Act
//            var result = service.HashPassword(
//                user,
//                password);

//            // Assert
//            Assert.True(false);
//            this.mockRepository.VerifyAll();
//        }

//        [Fact]
//        public void VerifyHashedPassword_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            var service = this.CreateService();
//            TUser user = null;
//            string hashedPassword = null;
//            string providedPassword = null;

//            // Act
//            var result = service.VerifyHashedPassword(
//                user,
//                hashedPassword,
//                providedPassword);

//            // Assert
//            Assert.True(false);
//            this.mockRepository.VerifyAll();
//        }

//        [Fact]
//        public async Task SetSecurityStampAsync_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            var service = this.CreateService();
//            TUser user = null;
//            string stamp = null;
//            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

//            // Act
//            await service.SetSecurityStampAsync(
//                user,
//                stamp,
//                cancellationToken);

//            // Assert
//            Assert.True(false);
//            this.mockRepository.VerifyAll();
//        }

//        [Fact]
//        public async Task GetSecurityStampAsync_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            var service = this.CreateService();
//            TUser user = null;
//            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

//            // Act
//            var result = await service.GetSecurityStampAsync(
//                user,
//                cancellationToken);

//            // Assert
//            Assert.True(false);
//            this.mockRepository.VerifyAll();
//        }

//        [Fact]
//        public async Task SetPhoneNumberAsync_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            var service = this.CreateService();
//            TUser user = null;
//            string phoneNumber = null;
//            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

//            // Act
//            await service.SetPhoneNumberAsync(
//                user,
//                phoneNumber,
//                cancellationToken);

//            // Assert
//            Assert.True(false);
//            this.mockRepository.VerifyAll();
//        }

//        [Fact]
//        public async Task GetPhoneNumberAsync_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            var service = this.CreateService();
//            TUser user = null;
//            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

//            // Act
//            var result = await service.GetPhoneNumberAsync(
//                user,
//                cancellationToken);

//            // Assert
//            Assert.True(false);
//            this.mockRepository.VerifyAll();
//        }

//        [Fact]
//        public async Task GetPhoneNumberConfirmedAsync_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            var service = this.CreateService();
//            TUser user = null;
//            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

//            // Act
//            var result = await service.GetPhoneNumberConfirmedAsync(
//                user,
//                cancellationToken);

//            // Assert
//            Assert.True(false);
//            this.mockRepository.VerifyAll();
//        }

//        [Fact]
//        public async Task SetPhoneNumberConfirmedAsync_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            var service = this.CreateService();
//            TUser user = null;
//            bool confirmed = false;
//            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

//            // Act
//            await service.SetPhoneNumberConfirmedAsync(
//                user,
//                confirmed,
//                cancellationToken);

//            // Assert
//            Assert.True(false);
//            this.mockRepository.VerifyAll();
//        }

//        [Fact]
//        public async Task GetLockoutEndDateAsync_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            var service = this.CreateService();
//            TUser user = null;
//            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

//            // Act
//            var result = await service.GetLockoutEndDateAsync(
//                user,
//                cancellationToken);

//            // Assert
//            Assert.True(false);
//            this.mockRepository.VerifyAll();
//        }

//        [Fact]
//        public async Task SetLockoutEndDateAsync_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            var service = this.CreateService();
//            TUser user = null;
//            DateTimeOffset? lockoutEnd = null;
//            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

//            // Act
//            await service.SetLockoutEndDateAsync(
//                user,
//                lockoutEnd,
//                cancellationToken);

//            // Assert
//            Assert.True(false);
//            this.mockRepository.VerifyAll();
//        }

//        [Fact]
//        public async Task IncrementAccessFailedCountAsync_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            var service = this.CreateService();
//            TUser user = null;
//            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

//            // Act
//            var result = await service.IncrementAccessFailedCountAsync(
//                user,
//                cancellationToken);

//            // Assert
//            Assert.True(false);
//            this.mockRepository.VerifyAll();
//        }

//        [Fact]
//        public async Task ResetAccessFailedCountAsync_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            var service = this.CreateService();
//            TUser user = null;
//            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

//            // Act
//            await service.ResetAccessFailedCountAsync(
//                user,
//                cancellationToken);

//            // Assert
//            Assert.True(false);
//            this.mockRepository.VerifyAll();
//        }

//        [Fact]
//        public async Task GetAccessFailedCountAsync_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            var service = this.CreateService();
//            TUser user = null;
//            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

//            // Act
//            var result = await service.GetAccessFailedCountAsync(
//                user,
//                cancellationToken);

//            // Assert
//            Assert.True(false);
//            this.mockRepository.VerifyAll();
//        }

//        [Fact]
//        public async Task GetLockoutEnabledAsync_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            var service = this.CreateService();
//            TUser user = null;
//            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

//            // Act
//            var result = await service.GetLockoutEnabledAsync(
//                user,
//                cancellationToken);

//            // Assert
//            Assert.True(false);
//            this.mockRepository.VerifyAll();
//        }

//        [Fact]
//        public async Task SetLockoutEnabledAsync_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            var service = this.CreateService();
//            TUser user = null;
//            bool enabled = false;
//            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

//            // Act
//            await service.SetLockoutEnabledAsync(
//                user,
//                enabled,
//                cancellationToken);

//            // Assert
//            Assert.True(false);
//            this.mockRepository.VerifyAll();
//        }

//        [Fact]
//        public async Task AddToRoleAsync_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            var service = this.CreateService();
//            TUser user = null;
//            string roleName = null;
//            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

//            // Act
//            await service.AddToRoleAsync(
//                user,
//                roleName,
//                cancellationToken);

//            // Assert
//            Assert.True(false);
//            this.mockRepository.VerifyAll();
//        }

//        [Fact]
//        public async Task RemoveFromRoleAsync_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            var service = this.CreateService();
//            TUser user = null;
//            string roleName = null;
//            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

//            // Act
//            await service.RemoveFromRoleAsync(
//                user,
//                roleName,
//                cancellationToken);

//            // Assert
//            Assert.True(false);
//            this.mockRepository.VerifyAll();
//        }

//        [Fact]
//        public async Task GetRolesAsync_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            var service = this.CreateService();
//            TUser user = null;
//            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

//            // Act
//            var result = await service.GetRolesAsync(
//                user,
//                cancellationToken);

//            // Assert
//            Assert.True(false);
//            this.mockRepository.VerifyAll();
//        }

//        [Fact]
//        public async Task IsInRoleAsync_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            var service = this.CreateService();
//            TUser user = null;
//            string roleName = null;
//            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

//            // Act
//            var result = await service.IsInRoleAsync(
//                user,
//                roleName,
//                cancellationToken);

//            // Assert
//            Assert.True(false);
//            this.mockRepository.VerifyAll();
//        }

//        [Fact]
//        public async Task GetUsersInRoleAsync_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            var service = this.CreateService();
//            string roleName = null;
//            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

//            // Act
//            var result = await service.GetUsersInRoleAsync(
//                roleName,
//                cancellationToken);

//            // Assert
//            Assert.True(false);
//            this.mockRepository.VerifyAll();
//        }

//        [Fact]
//        public async Task GetUserIdAsync_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            var service = this.CreateService();
//            TUser user = null;
//            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

//            // Act
//            var result = await service.GetUserIdAsync(
//                user,
//                cancellationToken);

//            // Assert
//            Assert.True(false);
//            this.mockRepository.VerifyAll();
//        }

//        [Fact]
//        public async Task GetUserNameAsync_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            var service = this.CreateService();
//            TUser user = null;
//            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

//            // Act
//            var result = await service.GetUserNameAsync(
//                user,
//                cancellationToken);

//            // Assert
//            Assert.True(false);
//            this.mockRepository.VerifyAll();
//        }

//        [Fact]
//        public async Task SetUserNameAsync_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            var service = this.CreateService();
//            TUser user = null;
//            string userName = null;
//            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

//            // Act
//            await service.SetUserNameAsync(
//                user,
//                userName,
//                cancellationToken);

//            // Assert
//            Assert.True(false);
//            this.mockRepository.VerifyAll();
//        }

//        [Fact]
//        public async Task GetNormalizedUserNameAsync_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            var service = this.CreateService();
//            TUser user = null;
//            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

//            // Act
//            var result = await service.GetNormalizedUserNameAsync(
//                user,
//                cancellationToken);

//            // Assert
//            Assert.True(false);
//            this.mockRepository.VerifyAll();
//        }

//        [Fact]
//        public async Task SetNormalizedUserNameAsync_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            var service = this.CreateService();
//            TUser user = null;
//            string normalizedName = null;
//            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

//            // Act
//            await service.SetNormalizedUserNameAsync(
//                user,
//                normalizedName,
//                cancellationToken);

//            // Assert
//            Assert.True(false);
//            this.mockRepository.VerifyAll();
//        }

//        [Fact]
//        public async Task CreateAsync_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            var service = this.CreateService();
//            TUser user = null;
//            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

//            // Act
//            var result = await service.CreateAsync(
//                user,
//                cancellationToken);

//            // Assert
//            Assert.True(false);
//            this.mockRepository.VerifyAll();
//        }

//        [Fact]
//        public async Task UpdateAsync_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            var service = this.CreateService();
//            TUser user = null;
//            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

//            // Act
//            var result = await service.UpdateAsync(
//                user,
//                cancellationToken);

//            // Assert
//            Assert.True(false);
//            this.mockRepository.VerifyAll();
//        }

//        [Fact]
//        public async Task DeleteAsync_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            var service = this.CreateService();
//            TUser user = null;
//            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

//            // Act
//            var result = await service.DeleteAsync(
//                user,
//                cancellationToken);

//            // Assert
//            Assert.True(false);
//            this.mockRepository.VerifyAll();
//        }

//        [Fact]
//        public async Task FindByIdAsync_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            var service = this.CreateService();
//            string userId = null;
//            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

//            // Act
//            var result = await service.FindByIdAsync(
//                userId,
//                cancellationToken);

//            // Assert
//            Assert.True(false);
//            this.mockRepository.VerifyAll();
//        }

//        [Fact]
//        public async Task FindByNameAsync_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            var service = this.CreateService();
//            string normalizedUserName = null;
//            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

//            // Act
//            var result = await service.FindByNameAsync(
//                normalizedUserName,
//                cancellationToken);

//            // Assert
//            Assert.True(false);
//            this.mockRepository.VerifyAll();
//        }

//        [Fact]
//        public void Dispose_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            var service = this.CreateService();

//            // Act
//            service.Dispose();

//            // Assert
//            Assert.True(false);
//            this.mockRepository.VerifyAll();
//        }

//        [Fact]
//        public async Task SetEmailAsync_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            var service = this.CreateService();
//            TUser user = null;
//            string email = null;
//            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

//            // Act
//            await service.SetEmailAsync(
//                user,
//                email,
//                cancellationToken);

//            // Assert
//            Assert.True(false);
//            this.mockRepository.VerifyAll();
//        }

//        [Fact]
//        public async Task GetEmailAsync_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            var service = this.CreateService();
//            TUser user = null;
//            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

//            // Act
//            var result = await service.GetEmailAsync(
//                user,
//                cancellationToken);

//            // Assert
//            Assert.True(false);
//            this.mockRepository.VerifyAll();
//        }

//        [Fact]
//        public async Task GetEmailConfirmedAsync_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            var service = this.CreateService();
//            TUser user = null;
//            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

//            // Act
//            var result = await service.GetEmailConfirmedAsync(
//                user,
//                cancellationToken);

//            // Assert
//            Assert.True(false);
//            this.mockRepository.VerifyAll();
//        }

//        [Fact]
//        public async Task SetEmailConfirmedAsync_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            var service = this.CreateService();
//            TUser user = null;
//            bool confirmed = false;
//            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

//            // Act
//            await service.SetEmailConfirmedAsync(
//                user,
//                confirmed,
//                cancellationToken);

//            // Assert
//            Assert.True(false);
//            this.mockRepository.VerifyAll();
//        }

//        [Fact]
//        public async Task FindByEmailAsync_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            var service = this.CreateService();
//            string normalizedEmail = null;
//            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

//            // Act
//            var result = await service.FindByEmailAsync(
//                normalizedEmail,
//                cancellationToken);

//            // Assert
//            Assert.True(false);
//            this.mockRepository.VerifyAll();
//        }

//        [Fact]
//        public async Task GetNormalizedEmailAsync_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            var service = this.CreateService();
//            TUser user = null;
//            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

//            // Act
//            var result = await service.GetNormalizedEmailAsync(
//                user,
//                cancellationToken);

//            // Assert
//            Assert.True(false);
//            this.mockRepository.VerifyAll();
//        }

//        [Fact]
//        public async Task SetNormalizedEmailAsync_StateUnderTest_ExpectedBehavior()
//        {
//            // Arrange
//            var service = this.CreateService();
//            TUser user = null;
//            string normalizedEmail = null;
//            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

//            // Act
//            await service.SetNormalizedEmailAsync(
//                user,
//                normalizedEmail,
//                cancellationToken);

//            // Assert
//            Assert.True(false);
//            this.mockRepository.VerifyAll();
//        }
//    }
//}
