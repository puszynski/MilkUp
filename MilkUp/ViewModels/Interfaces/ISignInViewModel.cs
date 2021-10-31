using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MilkUp.ViewModels.Interfaces
{
    //note: for that moment cant use _signInManager.PasswordSignInAsync using SignalR - use it when changed; The Blazor roadmap suggest this might be fixed in version 5.0.0, but for now you need to develop a solution based on the mixin. I have done this by using the scaffold and setting up navigation between Blazor components and Identity scaffolded items (SignIn and Signout processes). It works great and is the optimal solution for now.
    public interface ISignInViewModel
    {
        string ErrorMessage { get; set; }
        IList<AuthenticationScheme> ExternalLogins { get; set; }
        SignInViewModel.SignInModel Input { get; set; }
        string ReturnUrl { get; set; }
        Task SignIn();
    }
}