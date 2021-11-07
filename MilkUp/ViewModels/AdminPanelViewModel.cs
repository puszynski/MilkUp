﻿using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using MilkUp.Data;
using MilkUp.Enums;
using MilkUp.Models;
using MilkUp.Repositories.Interfaces;
using MilkUp.ViewModels.AdminPanel;
using MilkUp.ViewModels.Interfaces;
using MilkUp.ViewModels.SuperUserPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilkUp.ViewModels
{
    public class AdminPanelViewModel : BaseViewModel, IAdminPanelViewModel
    {
        readonly ICowGroupRepository _cowGroupRepository;
        readonly IFarmRepository _farmRepository;

        public AdminPanelViewModel(ApplicationDbContext applicationDbContext,
                                   UserManager<ApplicationUser> userManager,
                                   AuthenticationStateProvider authenticationStateProvider,
                                   ICowGroupRepository cowGroupRepository,
                                   IFarmRepository farmRepository)
            : base(authenticationStateProvider,
                  applicationDbContext,
                  userManager)
        {
            _cowGroupRepository = cowGroupRepository;
            _farmRepository = farmRepository;
            InitializeViewModel();
        }

        public List<UserViewModel> UsersViewModels { get; set; }
        public List<FarmViewModel> FarmsViewModels { get; set; }
        public List<CowGroupsViewModel> CowGroupsViewModels { get; set; }

        public async Task InitializeViewModel()
        {
            if (_userCompanyID.HasValue)
            {
                UsersViewModels = _applicationDbContext.Users
                    .Where(x => x.CompanyID == _userCompanyID.Value)
                    .Select(x => new UserViewModel() { Email = x.Email, UserID = x.Id, CompanyID = _userCompanyID.Value })
                    .ToList();
                foreach (var userVM in UsersViewModels)
                    userVM.Roles = string.Join(", ", _applicationDbContext.UserRoles.Where(x => x.UserId == userVM.UserID).Select(x => x.RoleId));

                FarmsViewModels = _applicationDbContext.Farms
                    .Where(x => x.CompanyID == _userCompanyID.Value && !x.DateDeleted.HasValue)
                    .Select(x => new FarmViewModel() { ID = x.ID, Name = x.Name, DateAdded = x.DateAdded })
                    .ToList();

                CowGroupsViewModels = _applicationDbContext.CowGroups
                    .Where(x => x.CompanyID == _userCompanyID.Value && !x.DateDeleted.HasValue) //todo add migration and apply code
                    .Select(x => new CowGroupsViewModel() { ID = x.ID, Name = x.Name })
                    .ToList();
            }
        }

        #region Add new user
        public AddUserViewModel AddUserViewModel { get; set; }

        public async Task InitializeAddUser()
        {
            AddUserViewModel = new AddUserViewModel();
        }

        public async Task CancelAddUser()
        {
            if (!_userCompanyID.HasValue)
                return;

            AddUserViewModel = null;
        }

        public async Task AddUser()
        {
            if (!_userCompanyID.HasValue)
                return;

            //todo walidacja

            var user = new ApplicationUser();
            user.Email = AddUserViewModel.Email;
            user.UserName = AddUserViewModel.Email;
            user.CompanyID = _userCompanyID.Value;

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
        #endregion

        #region Add cow group
        public AddCowGroupViewModel AddCowGroupViewModel { get; set; }
        public async Task InitializeAddCowGroup()
        {
            AddCowGroupViewModel = new AddCowGroupViewModel();
        }
        public async Task CancelAddCowGroup()
        {
            AddCowGroupViewModel = null;
        }
        public async Task AddCowGroup()
        {
            if (_userCompanyID.HasValue)
                throw new NotImplementedException();

            var groupToAdd = new CowGroup() { Name = AddCowGroupViewModel.Name, CompanyID = _userCompanyID.Value };
            _cowGroupRepository.Insert(groupToAdd);
            AddCowGroupViewModel = null;
            InitializeViewModel();
        }
        #endregion

        #region Add farm
        public AddFarmViewModel AddFarmViewModel { get; set; }
        public async Task InitializeAddFarm()
        {
            AddFarmViewModel = new AddFarmViewModel();
        }
        public async Task CancelAddFarm()
        {
            AddFarmViewModel = null;
        }
        public async Task AddFarm()
        {
            if (_userCompanyID.HasValue)
                throw new NotImplementedException();

            var farmToAdd = new Farm() { Name = AddCowGroupViewModel.Name, CompanyID = _userCompanyID.Value };
            _farmRepository.Insert(farmToAdd);

            AddFarmViewModel = null;
            InitializeViewModel();
        }
        #endregion
    }
}
