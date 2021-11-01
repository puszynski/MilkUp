using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using MilkUp.Data;
using MilkUp.Enums;
using MilkUp.Models;
using MilkUp.Repositories;
using MilkUp.Repositories.Interfaces;
using MilkUp.ViewModels.Interfaces;
using MilkUp.ViewModels.SuperAdminPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilkUp.ViewModels
{
    public class SuperAdminPanelViewModel : ISuperAdminPanelViewModel
    {
        readonly UserManager<ApplicationUser> _userManager;
        readonly RoleManager<IdentityRole> _roleManager;
        readonly AuthenticationStateProvider _authenticationStateProvider;
        readonly ApplicationDbContext _applicationDbContext;
        readonly ICompanyRepository _companyRepository;

        public SuperAdminPanelViewModel(UserManager<ApplicationUser> userManager,
                                        RoleManager<IdentityRole> roleManager,
                                        AuthenticationStateProvider authenticationStateProvider,
                                        ApplicationDbContext applicationDbContext,
                                        ICompanyRepository companyRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _authenticationStateProvider = authenticationStateProvider;
            _applicationDbContext = applicationDbContext;
            _companyRepository = companyRepository;

            InitializeViewModel().GetAwaiter().GetResult();
        }

        public List<UsersViewModel> UsersViewModels { get; set; }
        public AddUserViewModel AddUserViewModel { get; set; }
        public List<(string ID, string Name)> Companies { get; set; }

        public async Task InitializeViewModel()
        {
            UsersViewModels = _applicationDbContext.Users.Select(x => new UsersViewModel() { Email = x.Email, UserID = x.Id, CompanyID = x.CompanyID }).ToList();

            foreach (var user in UsersViewModels)
            {
                user.Roles = string.Join(", ", _applicationDbContext.UserRoles.Where(x => x.UserId == user.UserID).Select(x => x.RoleId)); 
            }
        }

        public async Task InitializeAddUser()
        {
            var query = await _companyRepository.GetQuery();
            var data = query.Select(x => new Tuple<int, string>(x.ID, x.Name)).ToList();
            Companies = new List<(string ID, string Name)>();
            foreach (var item in data)
                Companies.Add((item.Item1.ToString(), item.Item2));

            AddUserViewModel = new AddUserViewModel();
        }

        public async Task CancelAddUser()
        {
            AddUserViewModel = null;
        }

        public async Task AddNewUser()
        {
            //todo walidacja

            var user = new ApplicationUser();
            user.Email = AddUserViewModel.Email;
            user.UserName = AddUserViewModel.Email;
            user.CompanyID = int.Parse(AddUserViewModel.CompanyID);

            user.EmailConfirmed = true;
            user.ConcurrencyStamp = Guid.NewGuid().ToString();

            try
            {
                var result = await _userManager.CreateAsync(user, AddUserViewModel.Password);
                if (result.Succeeded)
                {
                    //wartość domyślna - czy tak ma być?
                    await _userManager.AddToRoleAsync(user, nameof(EAspNetRole.Regular));
                }
                else
                {

                } 
            }
            catch (Exception ex)
            {

                throw ex;
            }

            

            AddUserViewModel = null;
        }
        

        public async Task InitNewRoleAndAssignLoggedUser()
        {
            //var role = new IdentityRole();
            //role.Name = nameof(EAspNetRole.SuperUser);
            //role.NormalizedName = nameof(EAspNetRole.SuperUser).ToUpper();
            //role.ConcurrencyStamp = Guid.NewGuid().ToString();

            //var result = await _roleManager.CreateAsync(role);

            //if (result.Succeeded)
            //{
            //    var state = _authenticationStateProvider.GetAuthenticationStateAsync();
            //    var user = await _userManager.GetUserAsync(state.Result.User);
            //    var result2 = await _userManager.AddToRoleAsync(user, nameof(EAspNetRole.SuperUser));
            //}
        }

        public async Task CreateNewRole()
        {
            var role = new IdentityRole();
            //role.Name = nameof(EAspNetRole.Regular); //add new EAspNetRole here
            //role.ConcurrencyStamp = Guid.NewGuid().ToString();

            //var result = await _roleManager.CreateAsync(role);
        }
    }
}
