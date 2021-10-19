using MilkUp.ViewModels.Interfaces;
using MilkUp.ViewModels.PartialViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MilkUp.ViewModels
{
    public class CowsViewModel : ICowsViewModel
    {
        public CowsViewModel()
        {
            InitializeViewModel().GetAwaiter().GetResult();
        }

        public async Task InitializeViewModel()
        {
            await Task.Delay(0);

            CowList = new List<CowListViewModel>()
            {
                new CowListViewModel(){ ID = 1, FarmID = 1, NameOnFarm = 203, IsFemale = true, LactationCount = 7 },
                new CowListViewModel(){ ID = 2, FarmID = 1, NameOnFarm = 204, IsFemale = true, ParentID = 1, LactationCount = 0 },
                new CowListViewModel(){ ID = 3, FarmID = 1, NameOnFarm = 205, IsFemale = true, ParentID = 1, LactationCount = 0}
            };
        }

        public List<CowListViewModel> CowList { get; set; }
        public SelectedCowViewModel SelectedCowViewModel { get; set; }        
        public AddCowFormViewModel AddCowFormViewModel { get; set; }

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

            //todo save to db
            CowList.Add(new CowListViewModel() 
            { 
                NameOnFarm = AddCowFormViewModel.NameOnFarm.Value,
                FarmID = int.Parse(AddCowFormViewModel.FarmID),
                LactationCount = 0,
                ParentID = AddCowFormViewModel.ParentID
            });

            AddCowFormViewModel = null;
        }
    }
}