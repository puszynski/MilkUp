using MilkUp.ViewModels.Cows;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MilkUp.ViewModels.Interfaces
{
    public interface ICowsViewModel
    {
        List<CowListViewModel> CowList {  get; set; }
        SelectedCowViewModel SelectedCowViewModel {  get; set; }

        string SearchFilter { get; set; }

        Task InitializeViewModel();        
        Task DisplaySelectedCowViewModel(int cowID);

        Task DeleteCow(int cowID);
    }
}
