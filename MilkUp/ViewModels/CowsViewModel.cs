using MilkUp.ViewModels.Interfaces;
using MilkUp.ViewModels.PartialViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MilkUp.ViewModels
{
    public class CowsViewModel : ICowsViewModel/*, INotifyPropertyChanged*/
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

        //public event PropertyChangedEventHandler PropertyChanged;
        //private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        private List<CowListViewModel> _cowList = null;
        public List<CowListViewModel> CowList
        {
            get => _cowList; 
            set { _cowList = value; /*OnPropertyChanged();*/ }
        }

        private SelectedCowViewModel _selectedCowViewModel = null;
        public SelectedCowViewModel SelectedCowViewModel 
        {
            get => _selectedCowViewModel;
            set { _selectedCowViewModel = value; /*OnPropertyChanged();*/ }
        }

        private AddCowFormViewModel _addCowFormViewModel = null;
        public AddCowFormViewModel AddCowFormViewModel
        {
            get => _addCowFormViewModel;
            set {  _addCowFormViewModel = value; /*OnPropertyChanged();*/  }
        }

        public async Task InitializeNewCowForm()
        {
            await Task.Delay(0);//hack to "run async"
            AddCowFormViewModel = new AddCowFormViewModel() { IsFarmBorn = true };
        }

        public async Task AddNewCow()
        {
            await Task.Delay(0);

            //todo save to db
            CowList.Add(new CowListViewModel() 
            { 
                ID = AddCowFormViewModel.ID,
                NameOnFarm = AddCowFormViewModel.NameOnFarm,
                FarmID = AddCowFormViewModel.FarmID,
                IsFemale = AddCowFormViewModel.IsFemale,
                LactationCount = 0,
                ParentID = AddCowFormViewModel.ParentID
            });

            AddCowFormViewModel = null;
        }
    }
}