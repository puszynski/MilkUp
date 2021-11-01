using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using MilkUp.Data;
using MilkUp.Models;
using MilkUp.ViewModels.Interfaces;
using MilkUp.ViewModels.SuperAdminPanel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilkUp.ViewModels
{
    public class AdminPanelViewModel : IAdminPanelViewModel
    {
        readonly ApplicationDbContext _applicationDbContext;
        readonly AuthenticationStateProvider _authenticationStateProvider;
        readonly UserManager<ApplicationUser> _userManager;
        int _companyID = 1;

        public AdminPanelViewModel(ApplicationDbContext applicationDbContext,
            UserManager<ApplicationUser> userManager,
            AuthenticationStateProvider authenticationStateProvider)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
            _authenticationStateProvider = authenticationStateProvider;

            InitializeViewModel2().GetAwaiter().GetResult();
        }

        public List<UsersViewModel> UsersViewModels { get; set; }

        public async Task InitializeViewModel2()
        {
            _companyID = 1; //HACK! TODO BUG - GET FROM USER!
            //var state = _authenticationStateProvider.GetAuthenticationStateAsync();
            //var user = _userManager.GetUserAsync(state.Result.User);
            //_companyID = user.Result.CompanyID;

            //if (_companyID == 0) //means y are SuperUser
            //    _companyID = 1;

            UsersViewModels = _applicationDbContext.Users
                .Where(x => x.CompanyID == _companyID)
                .Select(x => new UsersViewModel() { Email = x.Email, UserID = x.Id, CompanyID = _companyID })
                .ToList();

            foreach (var userVM in UsersViewModels)
            {
                userVM.Roles = string.Join(", ", _applicationDbContext.UserRoles.Where(x => x.UserId == userVM.UserID).Select(x => x.RoleId));
            }
        }
        public async Task InitializeViewModel()
        {
            var state = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = await _userManager.GetUserAsync(state.User);
            _companyID = user.CompanyID;

            if (_companyID == 0) //means y are SuperUser
                _companyID = 1;

            UsersViewModels = _applicationDbContext.Users
                .Where(x => x.CompanyID == _companyID)
                .Select(x => new UsersViewModel() { Email = x.Email, UserID = x.Id, CompanyID = _companyID })
                .ToList();

            foreach (var userVM in UsersViewModels)
            {
                userVM.Roles = string.Join(", ", _applicationDbContext.UserRoles.Where(x => x.UserId == userVM.UserID).Select(x => x.RoleId));
            }
        }
    }
}
