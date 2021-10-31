using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MilkUp.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MilkUp.ViewModels
{
    //note: for that moment cant use _signInManager.PasswordSignInAsync using SignalR - use it when changed; The Blazor roadmap suggest this might be fixed in version 5.0.0, but for now you need to develop a solution based on the mixin. I have done this by using the scaffold and setting up navigation between Blazor components and Identity scaffolded items (SignIn and Signout processes). It works great and is the optimal solution for now.

    public class SignInViewModel : ISignInViewModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;

        public SignInViewModel(SignInManager<IdentityUser> signInManager,
            ILogger<LoginModel> logger,
            UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;

            Input = new SignInModel();
        }

        [BindProperty]
        public SignInModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class SignInModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task SignIn()
        {
            try
            {
                //błąd - nie uda się tobie tego zrobić via signalR, coś z tokenami => https://forums.asp.net/t/2157944.aspx?The+response+headers+cannot+be+modified+because+the+response+has+already+started+
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                _logger.LogInformation("User logged in.");

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //public async Task OnGetAsync(string returnUrl = null)
        //{
        //    if (!string.IsNullOrEmpty(ErrorMessage))
        //    {
        //        ModelState.AddModelError(string.Empty, ErrorMessage);
        //    }

        //    returnUrl = returnUrl ?? Url.Content("~/");

        //    // Clear the existing external cookie to ensure a clean login process
        //    await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        //    ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

        //    ReturnUrl = returnUrl;
        //}

        //public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        //{
        //    returnUrl = returnUrl ?? Url.Content("~/");

        //    if (ModelState.IsValid)
        //    {
        //        // This doesn't count login failures towards account lockout
        //        // To enable password failures to trigger account lockout, set lockoutOnFailure: true
        //        var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
        //        if (result.Succeeded)
        //        {
        //            _logger.LogInformation("User logged in.");
        //            return LocalRedirect(returnUrl);
        //        }
        //        if (result.RequiresTwoFactor)
        //        {
        //            return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
        //        }
        //        if (result.IsLockedOut)
        //        {
        //            _logger.LogWarning("User account locked out.");
        //            return RedirectToPage("./Lockout");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        //            return Page();
        //        }
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return Page();
        //}
    }
}
