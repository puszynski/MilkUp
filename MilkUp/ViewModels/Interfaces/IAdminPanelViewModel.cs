using MilkUp.ViewModels.AdminPanel;
using MilkUp.ViewModels.SuperUserPanel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MilkUp.ViewModels.Interfaces
{
    public interface IAdminPanelViewModel
    {
        List<UserViewModel> UsersViewModels { get; set; }
        List<FarmViewModel> FarmsViewModels { get; set; }
        List<CowGroupsViewModel> CowGroupsViewModels { get; set; }

        Task InitializeViewModel();

        AddUserViewModel AddUserViewModel { get; set; }
        Task InitializeAddUser();
        Task CancelAddUser();
        Task AddNewUser();
    }
}