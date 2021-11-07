using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using MilkUp.Data;
using MilkUp.Enums;
using MilkUp.Models;
using MilkUp.Repositories.Interfaces;
using MilkUp.ViewModels.Interfaces;
using MilkUp.ViewModels.SuperUserPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilkUp.ViewModels
{
    public class SuperUserPanelViewModel : ISuperUserPanelViewModel
    {
        readonly UserManager<ApplicationUser> _userManager;
        readonly RoleManager<IdentityRole> _roleManager;
        readonly AuthenticationStateProvider _authenticationStateProvider;
        readonly ApplicationDbContext _applicationDbContext;
        readonly ICompanyRepository _companyRepository;

        public SuperUserPanelViewModel(UserManager<ApplicationUser> userManager,
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

        public List<UserViewModel> UsersViewModels { get; set; }

        public async Task InitializeViewModel()
        {
            UsersViewModels = _applicationDbContext.Users.Select(x => new UserViewModel() { Email = x.Email, UserID = x.Id, CompanyID = x.CompanyID }).ToList();

            foreach (var user in UsersViewModels)
            {
                user.Roles = string.Join(", ", _applicationDbContext.UserRoles.Where(x => x.UserId == user.UserID).Select(x => x.RoleId));
            }
        }


        #region AddUser
        public AddUserViewModel AddUserViewModel { get; set; }
        public List<(string ID, string Name)> Companies { get; set; }
        public List<(string ID, string Name)> Roles {  get; set; } 



        public async Task InitializeAddUser()
        {
            var query = await _companyRepository.GetQuery();
            var data = query.Select(x => new Tuple<int, string>(x.ID, x.Name)).ToList();
            Companies = new List<(string ID, string Name)>();
            foreach (var item in data)
                Companies.Add((item.Item1.ToString(), item.Item2));

            var queryRoles = _applicationDbContext
                .Roles
                .Select(x => new Tuple<string, string>(x.Id, x.Name))
                .AsEnumerable()
                .Select(x => (ID: x.Item1, Name: x.Item2))
                .ToList();
            Roles = new List<(string ID, string Name)>();
            foreach (var item in queryRoles)
                Roles.Add((item.Item1.ToString(), item.Item2));

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
                    await _userManager.AddToRoleAsync(user, nameof(AddUserViewModel.RoleName));
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
        #endregion

        #region HandJobs
        public async Task CreateSuperUser()
        {
            var user = new ApplicationUser();
            user.Email = "puszynski@gmail.com";
            user.UserName = "puszynski@gmail.com";
            user.CompanyID = 0;

            user.EmailConfirmed = true;
            user.ConcurrencyStamp = Guid.NewGuid().ToString();

            try
            {
                var result = await _userManager.CreateAsync(user, "RazDwaTrzy123");
                if (result.Succeeded)
                {
                    //wartość domyślna - czy tak ma być?
                    await _userManager.AddToRoleAsync(user, nameof(EAspNetRole.SuperUser));
                }
                else
                {

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task CreateInitRoles()
        {
            //var role = new IdentityRole();
            //role.Name = nameof(EAspNetRole.Regular);
            //role.ConcurrencyStamp = Guid.NewGuid().ToString();
            //var result = await _roleManager.CreateAsync(role);

            //var role2 = new IdentityRole();
            //role2.Name = nameof(EAspNetRole.Admin);
            //role2.ConcurrencyStamp = Guid.NewGuid().ToString();
            //var result2 = await _roleManager.CreateAsync(role2);

            //var role3 = new IdentityRole();
            //role3.Name = nameof(EAspNetRole.SuperUser);
            //role3.ConcurrencyStamp = Guid.NewGuid().ToString();
            //var result3 = await _roleManager.CreateAsync(role3);
        }
        #endregion

    }
}
