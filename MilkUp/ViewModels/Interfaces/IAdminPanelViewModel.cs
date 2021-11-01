using MilkUp.ViewModels.SuperAdminPanel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MilkUp.ViewModels.Interfaces
{
    public interface IAdminPanelViewModel
    {
        List<UsersViewModel> UsersViewModels { get; set; }

        Task InitializeViewModel();
    }
}