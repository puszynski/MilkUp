using MilkUp.ViewModels.PartialViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MilkUp.ViewModels.Interfaces
{
    public interface ICowsViewModel
    {
        List<CowListViewModel> CowList {  get; set; }
        SelectedCowViewModel SelectedCowViewModel {  get; set; }
        AddCowFormViewModel AddCowFormViewModel {  get; set; }
        Task InitializeNewCowForm();
        Task AddNewCow();
    }
}
