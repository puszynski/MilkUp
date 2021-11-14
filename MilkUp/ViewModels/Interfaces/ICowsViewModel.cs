﻿using MilkUp.ViewModels.Cows;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MilkUp.ViewModels.Interfaces
{
    public interface ICowsViewModel
    {
        List<CowListViewModel> CowList {  get; set; }

        SelectedCowViewModel SelectedCowViewModel {  get; set; }

        AddCowViewModel AddCowViewModel {  get; set; }
        List<(string FarmID, string FarmName)> Farms { get; set; }
        List<(string ID, string Name)> CowGroups { get; set; }

        string SearchFilter { get; set; }

        Task InitializeViewModel();
        Task InitializeNewCowForm();
        Task CancelAddCowForm();
        Task AddNewCowNewLactation();
        Task AddNewCow();
        Task DisplaySelectedCowViewModel(int cowID);
    }
}
