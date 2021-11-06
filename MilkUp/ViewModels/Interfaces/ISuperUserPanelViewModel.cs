using MilkUp.ViewModels.SuperUserPanel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MilkUp.ViewModels.Interfaces
{
    public interface ISuperUserPanelViewModel
    {
        AddUserViewModel AddUserViewModel { get; set; }
        List<(string ID, string Name)> Companies { get; set; }
        List<UserViewModel> UsersViewModels { get; set; }

        Task InitializeViewModel();
        Task InitializeAddUser();
        Task CancelAddUser();
        Task AddNewUser();
        Task CreateSuperUser();
        Task CreateInitRoles();
    }
}