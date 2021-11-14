using Microsoft.AspNetCore.Components;
using MilkUp.Models;
using MilkUp.Repositories;
using MilkUp.ViewModels.Interfaces;
using MilkUp.ViewModels.Cows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MilkUp.Repositories.Interfaces;
using MilkUp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Components.Authorization;

namespace MilkUp.ViewModels
{
    public class CowsViewModel : BaseViewModel, ICowsViewModel
    {
        readonly ICowRepository _cowRepository;
        readonly ILactationRepository _lactationRepository;
        readonly ApplicationDbContext _dbContext;

        public CowsViewModel(ICowRepository cowRepository,
                             ILactationRepository lactationRepository,
                             ApplicationDbContext applicationDbContext,
                             UserManager<ApplicationUser> userManager,
                             AuthenticationStateProvider authenticationStateProvider) 
            : base(authenticationStateProvider, 
                  applicationDbContext, 
                  userManager)
        {            
            _cowRepository = cowRepository;
            _lactationRepository = lactationRepository;
            InitializeViewModel().GetAwaiter().GetResult();
        }

        public async Task InitializeViewModel()
        {
            if (_userCompanyID == null)
                return;

            var cows = await _cowRepository.GetQuery();

            CowList = cows.Select(x => new CowListViewModel()
                {
                    FarmID = x.FarmID,
                    ID = x.ID,
                    IsFemale = !x.IsMale,
                    LactationCount = x.Lactations.Count(),
                    NameOnFarm = x.NameOnFarm,
                    ParentID = x.ParentCowID,
                    AgeDisplay = (int)(DateTime.UtcNow - x.BirthDate).TotalDays
                })
                .ToList();

            Farms = _applicationDbContext.Farms
                .Where(x => !x.DateDeleted.HasValue)
                .Where(x => x.CompanyID == _userCompanyID.Value)
                .Select(x => new Tuple<string, string>(x.ID.ToString(), x.Name))
                .AsEnumerable()
                .Select(x => (FarmID: x.Item1, FarmName: x.Item2))
                .ToList();

            CowGroups = _applicationDbContext.CowGroups
                .Where(x => !x.DateDeleted.HasValue)
                .Where(x => x.CompanyID == _userCompanyID.Value)
                .Select(x => new Tuple<string, string>(x.ID.ToString(), x.Name))
                .AsEnumerable()
                .Select(x => (ID: x.Item1, Name: x.Item2))
                .ToList();
        }

        public List<CowListViewModel> CowList { get; set; }
        public SelectedCowViewModel SelectedCowViewModel { get; set; }
        
        public AddCowViewModel AddCowViewModel { get; set; }
        public List<(string FarmID, string FarmName)> Farms { get; set; }
        public List<(string ID, string Name)> CowGroups { get; set; }

        public string SearchFilter { get; set; }

        public async Task InitializeNewCowForm()
        {
            await Task.Delay(0);//hack to "run async"

            AddCowViewModel = new AddCowViewModel() 
            { 
                IsFarmBorn = true, 
                LactationsViewModels = new List<LactationViewModel>() 
            };
        }

        public async Task CancelAddCowForm()
        {
            await Task.Delay(0);//hack to "run async"
            AddCowViewModel = null;
        }

        public async Task AddNewCowNewLactation()
        {
            AddCowViewModel.LactationsViewModels.Add(new LactationViewModel() { From = DateTime.UtcNow } );
        }

        public async Task AddNewCow()
        {
            await Task.Delay(0);

            //todo walidacja + walidacja parsowań
              
            var cow = new Cow()
            {
                NameOnFarm = AddCowViewModel.NameOnFarm.Value,
                EarringNumber = AddCowViewModel.EarringNumber.Value,
                TransponderNumber = AddCowViewModel.TransponderNumber,
                BirthDate = AddCowViewModel.BirthDate.Value,
                FarmID = int.Parse(AddCowViewModel.FarmID),
                CowGroupID = int.Parse(AddCowViewModel.CowGroupID),
                CompanyID = _userCompanyID.Value
            };

            var lactationsToAdd = new List<Lactation>();    

            foreach (var lact in AddCowViewModel.LactationsViewModels)
            {
                var lactation = new Lactation()
                {
                    Cow = cow,
                    From = lact.From.Value,
                    To = lact.To,
                    LitersCollected = lact.LitersCollected
                };
                lactationsToAdd.Add(lactation);
                //_lactationRepository.Insert(lactation);
            }

            cow.Lactations = lactationsToAdd;


            try
            {
                var newCow = _cowRepository.Insert(cow).Result;

                //foreach (var lact in AddCowViewModel.LactationsViewModels)
                //{
                //    var lactation = new Lactation()
                //    {
                //        Cow = cow,
                //        From = lact.From.Value,
                //        To = lact.To,
                //        LitersCollected = lact.LitersCollected
                //    };
                //    _lactationRepository.Insert(lactation);
                //}                
                var result = _cowRepository.Save();

                var newCowVM = new CowListViewModel()
                {
                    FarmID = newCow.FarmID,
                    ID = newCow.ID,
                    IsFemale = !newCow.IsMale,
                    LactationCount = AddCowViewModel.LactationsViewModels != null ? AddCowViewModel.LactationsViewModels.Count() : 0,
                    NameOnFarm = newCow.NameOnFarm,
                    ParentID = newCow.ParentCowID,
                    AgeDisplay = (int)(DateTime.UtcNow - newCow.BirthDate).TotalDays
                };

                CowList.Add(newCowVM);
            }
            catch (Exception ex)
            {
                throw ex;
            }


            AddCowViewModel = null;

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