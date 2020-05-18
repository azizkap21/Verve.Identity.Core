# Verve.Identity.Core.Sample
###  What is it
 A set of library to simplify Microsoft.AspNetCore.Identity implementation using Sql Server with the help of Entity Framework Core, in various projects. I am providing it as opensource to help other developers. 
###  Implementation
1. There are three main libraries 
    1. Verve.Identity.Core.Model
    2. Verve.Identity.Core.Service
    3. Verve.Identity.Core.IdentityContext
2. Model contains  `VerveUserAccount` , `VerveRole` and `VerveUserRole` classes.
3. Identity context impliments `DbContext` 
4. Verve.Identity.Core.Service contains an Interface 
```
 public interface IVerveIdentityService<TUser> : IUserStore<TUser>,
                                    IUserPasswordStore<TUser>,
                                    IUserSecurityStampStore<TUser>,
                                    IUserEmailStore<TUser>,
                                    IUserPhoneNumberStore<TUser>,
                                    IUserLockoutStore<TUser>,
                                    IUserRoleStore<TUser>

                                    where TUser : class
    {
    }
```
and a class 
```
 public abstract class VerveIdentityService<TDbContext, TUser, TRole> : IVerveIdentityService<TUser>
        where TDbContext : VerveIdentityDbContext<TUser, TRole>
        where TUser : VerveUserAccount
        where TRole : VerveRole
```
Which provides concrete implementation of the interface. 
### How it works
1. Please see a sample implementation of these libraries [here](https://github.com/verveinfotech/Verve.Identity.Core.Sample)
2. Inherit `Role` from `VerveRole` class if you want to customize your role class.
3. Inherit Application db context from `VerveIdentityDbContext<UserAccount, Role>`. 		
4. Create `ApplicationUserService` class to implement `VerveIdentityService` abstract class.
5. Add below lines in `startup.cs` file
	```
	 public void ConfigureServices(IServiceCollection services)
	 ...
	 ...
	 ...
	 services.AddVerveIdentityServices<ApplicationUserService, TestApplicationDbContext, UserAccount, Role>();
	 services.AddVerveIdentity<UserAccount, Role>()
                .AddDefaultTokenProviders();
	 ...
	 ...        
	```
6. Now in `UserService` class inject `UserManager<UserAccount>` and `SignInManager<UserAccount>`classes to use them for Register, Login, Update user etc. 

