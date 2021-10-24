using MilkUp.Data;
using MilkUp.Models;
using MilkUp.Repositories;
using MilkUp.ViewModels.Interfaces;
using MilkUp.ViewModels.PartialViewModels;
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

            //await Task.Delay(0);

            //CowList = new List<CowListViewModel>()
            //{
            //    new CowListViewModel(){ ID = 1, FarmID = 1, NameOnFarm = 203, IsFemale = true, LactationCount = 7 },
            //    new CowListViewModel(){ ID = 2, FarmID = 1, NameOnFarm = 204, IsFemale = true, ParentID = 1, LactationCount = 0 },
            //    new CowListViewModel(){ ID = 3, FarmID = 1, NameOnFarm = 205, IsFemale = true, ParentID = 1, LactationCount = 0}
            //};
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
            var cow = new Cow()
            {
                NameOnFarm = AddCowFormViewModel.NameOnFarm.Value,
                FarmID = int.Parse(AddCowFormViewModel.FarmID),
                BirthDate = new System.DateTime(2010, 10, 1),
                EarringNumber = AddCowFormViewModel.EarringNumber.Value,
                TransponderNumber = AddCowFormViewModel.TransponderNumber.Value
            };

            _cowRepository.Insert(cow);
            _cowRepository.Save();

            AddCowFormViewModel = null;
        }
    }
}