using MilkUp.ViewModels.SuperAdminPanel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MilkUp.ViewModels.Interfaces
{
    public interface ISuperAdminPanelViewModel
    {
        AddUserViewModel AddUserViewModel { get; set; }
        List<(string ID, string Name)> Companies { get; set; }
        List<UsersViewModel> UsersViewModels { get; set; }

        Task InitializeViewModel();
        Task InitializeAddUser();
        Task CancelAddUser();
        Task AddNewUser();
        Task InitNewRoleAndAssignLoggedUser();
        Task CreateNewRole();
    }
}