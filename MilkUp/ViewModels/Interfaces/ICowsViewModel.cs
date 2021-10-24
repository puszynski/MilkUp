using MilkUp.ViewModels.PartialViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MilkUp.ViewModels.Interfaces
{
    public interface ICowsViewModel
    {
        List<CowListViewModel> CowList {  get; set; }
        SelectedCowViewModel SelectedCowViewModel {  get; set; }
        AddCowFormViewModel AddCowFormViewModel {  get; set; }
        Action StateHasChangedDelegate { get; set; }
        public string SearchFilter { get; set; }

        Task InitializeNewCowForm();
        Task CancelAddCowForm();
        Task AddNewCow();
    }
}
