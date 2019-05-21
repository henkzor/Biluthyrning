using Biluthyrning.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biluthyrning.Models
{
    public class AccountService
    {
        UserManager<MyIdentityUser> userManager;
        SignInManager<MyIdentityUser> signInManager;
        IHttpContextAccessor httpContextAccessor;
        public AccountService(
            UserManager<MyIdentityUser> userManager,
            SignInManager<MyIdentityUser> signInManager,
            IHttpContextAccessor httpContextAccessor
            )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.httpContextAccessor = httpContextAccessor;
        }


        public async Task<IdentityResult> TryRegisterAsync(AccountCreateVM accountCreateInput)
        {
            var result = await userManager.CreateAsync(
                new MyIdentityUser
                {
                    FirstName = accountCreateInput.FirstName,
                    LastName = accountCreateInput.LastName,
                    UserName = accountCreateInput.FirstName
                }, accountCreateInput.Password);

            return result;
        }

        public async Task<SignInResult> TryLoginAsync(AccountLoginVM ALVM)
        {
            return await signInManager.PasswordSignInAsync(
                ALVM.FirstName,
                ALVM.Password,
                isPersistent: true,
                lockoutOnFailure: false
                );
        }

        public async Task TryLogOutAsync()
        {
            await signInManager.SignOutAsync();
        }

        

    }
}
