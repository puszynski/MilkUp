using MilkUp.Models;
using MilkUp.Repositories;
using MilkUp.ViewModels.Interfaces;
using MilkUp.ViewModels.PartialViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilkUp.ViewModels
{
    public class CowsViewModel : ICowsViewModel
    {
        private readonly ICowRepository _cowRepository;

        public CowsViewModel(ICowRepository cowRepository)
        {            
            _cowRepository = cowRepository;
            InitializeViewModel().GetAwaiter().GetResult();
        }

        public async Task InitializeViewModel()
        {
            var cows = await _cowRepository.GetQuery();

            CowList = cows.Select(x => new CowListViewModel()
                {
                    FarmID = x.FarmID,
                    ID = x.ID,
                    IsFemale = !x.IsMale,
                    LactationCount = x.Lactations.Count(),
                    NameOnFarm = x.NameOnFarm,
                    ParentID = x.ParentCowID
                })
                .ToList();

            //todo if SelectedCowID from view[parameter] is not null, init SelectedCowViewModel

        }

        public List<CowListViewModel> CowList { get; set; }
        public SelectedCowViewModel SelectedCowViewModel { get; set; }
        
        public AddCowFormViewModel AddCowFormViewModel { get; set; }
        public List<(string FarmID, string FarmName)> Farms { get; set; } = new List<(string FarmID, string FarmName)>() { ("1", "Gdańsk"), ("2", "Puszczykowo") };

        public string SearchFilter { get; set; }

        public async Task InitializeNewCowForm()
        {
            await Task.Delay(0);//hack to "run async"
            AddCowFormViewModel = new AddCowFormViewModel() { IsFarmBorn = true };
        }

        public async Task CancelAddCowForm()
        {
            await Task.Delay(0);//hack to "run async"
            AddCowFormViewModel = null;
        }

        public async Task AddNewCow()
        {
            await Task.Delay(0);

            //todo walidacja parsowania

            var cow = new Cow()
            {
                NameOnFarm = AddCowFormViewModel.NameOnFarm.Value,
                FarmID = int.Parse(AddCowFormViewModel.FarmID),
                BirthDate = new System.DateTime(2010, 10, 1),
                EarringNumber = AddCowFormViewModel.EarringNumber.Value,
                TransponderNumber = AddCowFormViewModel.TransponderNumber.Value,
                CowGroupID = 1
            };

            try
            {
                var newCow = _cowRepository.Insert(cow).Result;
                _cowRepository.Save();

                var newCowVM = new CowListViewModel()
                {
                    FarmID = newCow.FarmID,
                    ID = newCow.ID,
                    IsFemale = !newCow.IsMale,
                    LactationCount = 0, //newCow.Lactations.Count(), powoduje błąd jak nie ma obiektów Lactations
                    NameOnFarm = newCow.NameOnFarm,
                    ParentID = newCow.ParentCowID
                };

                CowList.Add(newCowVM);
            }
            catch (Exception ex)
            {
                throw ex;
            }


            AddCowFormViewModel = null;
            //StateHasChanged(); //use https://itnext.io/mvvm-and-blazor-components-and-statehaschanged-a31be365638b Option 5: EventCallback (Use this one) to fire up StateHasChanged()
        }

        public async Task DisplaySelectedCowViewModel(int cowID)
        {
            var selectedCow = await _cowRepository.GetByID(cowID);

            SelectedCowViewModel = new SelectedCowViewModel() 
            {  
                ID = selectedCow.ID,
                ParentID = selectedCow.ParentCowID,
                FarmName = selectedCow.Farm.Name,
                IsFemale = !selectedCow.IsMale,
                LactationCount = selectedCow.Lactations.Any() ? selectedCow.Lactations.Count() : 0,
                NameOnFarm = selectedCow.NameOnFarm
            };
        }
    }
}