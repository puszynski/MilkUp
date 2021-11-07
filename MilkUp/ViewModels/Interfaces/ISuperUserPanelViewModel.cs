using MilkUp.ViewModels.SuperUserPanel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MilkUp.ViewModels.Interfaces
{
    public interface ISuperUserPanelViewModel
    {        
        List<(string ID, string Name)> Companies { get; set; }
        List<(string ID, string Name)> Roles { get; set; }

        List<UserViewModel> UsersViewModels { get; set; }
        List<CompanyViewModel> CompaniesViewModels { get; set; }

        Task InitializeViewModel();

        AddUserViewModel AddUserViewModel { get; set; }
        Task InitializeAddUser();
        Task CancelAddUser();
        Task AddNewUser();

        AddCompanyViewModel AddCompanyViewModel { get; set; }
        Task InitializeAddCompany();
        Task CancelAddCompany();
        Task AddCompany();

        Task CreateSuperUser();
        Task CreateInitRoles();
    }
}