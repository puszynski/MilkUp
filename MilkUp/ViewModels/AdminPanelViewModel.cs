using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using MilkUp.Data;
using MilkUp.Enums;
using MilkUp.Models;
using MilkUp.Repositories.Interfaces;
using MilkUp.ViewModels.AdminPanel;
using MilkUp.ViewModels.Interfaces;
using MilkUp.ViewModels.Shared;
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
        readonly ICowRepository _cowRepository;
        readonly IFarmRepository _farmRepository;

        public AdminPanelViewModel(ApplicationDbContext applicationDbContext,
                                   UserManager<ApplicationUser> userManager,
                                   AuthenticationStateProvider authenticationStateProvider,
                                   ICowGroupRepository cowGroupRepository,
                                   ICowRepository cowRepository,
                                   IFarmRepository farmRepository)
            : base(authenticationStateProvider,
                  applicationDbContext,
                  userManager)
        {
            _cowGroupRepository = cowGroupRepository;
            _cowRepository = cowRepository;
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
                //HARDCODED! ID of regular role
                UsersViewModels.Add(new UserViewModel() { CompanyID = _userCompanyID.Value, Email = AddUserViewModel.Email, Roles = "ae8ad018-abf8-4723-b5b1-353d4ba77230" });
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
            if (!_userCompanyID.HasValue)
                throw new NotImplementedException();

            var groupToAdd = new CowGroup() { Name = AddCowGroupViewModel.Name, CompanyID = _userCompanyID.Value };
            _cowGroupRepository.Insert(groupToAdd);
            try
            {
                _cowGroupRepository.Save();
                CowGroupsViewModels.Add(new CowGroupsViewModel() { Name = AddCowGroupViewModel.Name });
                AddCowGroupViewModel = null;

                InitializeViewModel();//nie działa, tzn nie ma po przeładowaniu
                //StateHasChanged
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
        #endregion

        #region Delete cow group
        public async Task RemoveCowGroup(int groupID)
        {
            //todo walidacja czy nie jest przypięta
            if (await _cowRepository.Any(x => x.CowGroupID == groupID))
            {
                Notifications.Add(new NotificationViewModel()
                {
                    Message = "Nie można usunąć, do grupy są przypisane krowy. Przypisz je do innej grupy.",
                    NotificationType = ENotificationType.Validation
                });

                return;
            }

            try
            {
                await _cowGroupRepository.Delete(groupID);
                CowGroupsViewModels.Remove(CowGroupsViewModels.Single(x => x.ID == groupID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
            if (!_userCompanyID.HasValue)
                throw new NotImplementedException();

            var farmToAdd = new Farm() { Name = AddFarmViewModel.Name, CompanyID = _userCompanyID.Value };
            _farmRepository.Insert(farmToAdd);

            try
            {
                _farmRepository.Save();
                FarmsViewModels.Add(new FarmViewModel() { Name = AddFarmViewModel.Name });
                AddFarmViewModel = null;
                
                InitializeViewModel();//nie działa, tzn nie ma po przeładowaniu
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Delete farm
        public async Task RemoveFarm(int farmID)
        {
            if (await _cowRepository.Any(x => x.FarmID == farmID))
            {
                Notifications.Add(new NotificationViewModel() 
                { 
                    Message = "Nie można usunąć, do farmy są przypisane krowy. Przypisz je do innej farmy.", 
                    NotificationType = ENotificationType.Validation 
                });

                return;
            }

            try
            {
                await _farmRepository.Delete(farmID);
                FarmsViewModels.Remove(FarmsViewModels.Single(x => x.ID == farmID));
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
        #endregion
    }
}
