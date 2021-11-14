using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using MilkUp.Data;
using MilkUp.Models;
using MilkUp.ViewModels.Interfaces;
using MilkUp.ViewModels.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace MilkUp.ViewModels
{
    public class BaseViewModel : INotification
    {
        public readonly AuthenticationStateProvider _authenticationStateProvider;
        public readonly ApplicationDbContext _applicationDbContext;
        public readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUser _user;
        public int? _userCompanyID;

        public List<NotificationViewModel> Notifications { get; set; }

        public BaseViewModel(AuthenticationStateProvider authenticationStateProvider,
                             ApplicationDbContext applicationDbContext,
                             UserManager<ApplicationUser> userManager)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;

            Notifications = new List<NotificationViewModel>();

            SetUserCompanyID();
        }

        public void SetUserCompanyID()
        {
            var authState = _authenticationStateProvider.GetAuthenticationStateAsync().GetAwaiter().GetResult();

            if (authState.User.Identity.IsAuthenticated)
            {
                var id = authState.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
                _user = _applicationDbContext.Users.Single(x => x.Id == id); // _userManager.FindByIdAsync(id).GetAwaiter().GetResult();  deadlock - why?
                _userCompanyID = _user.CompanyID;
            }
            else
            {
                _user = null;
                _userCompanyID = null;
            }
        }
    }
}
