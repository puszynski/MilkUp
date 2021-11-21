using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using MilkUp.Data;
using MilkUp.Models;
using MilkUp.Repositories.Interfaces;
using MilkUp.Services;
using MilkUp.ViewModels.Cows;
using MilkUp.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilkUp.ViewModels
{
    public class CowsViewModel : BaseViewModel, ICowsViewModel
    {
        readonly ICowRepository _cowRepository;
        readonly ILactationRepository _lactationRepository;
        readonly ApplicationDbContext _dbContext;
        readonly ApplicationStateService _applicationStateService;    
        readonly CowDeleteService _cowDeleteService;

        public CowsViewModel(ICowRepository cowRepository,
                             ILactationRepository lactationRepository,
                             ApplicationDbContext applicationDbContext,
                             UserManager<ApplicationUser> userManager,
                             AuthenticationStateProvider authenticationStateProvider,
                             CowDeleteService cowDeleteService, 
                             ApplicationStateService applicationStateService)
            : base(authenticationStateProvider,
                  applicationDbContext,
                  userManager)
        {
            _cowRepository = cowRepository;
            _lactationRepository = lactationRepository;
            InitializeViewModel().GetAwaiter().GetResult();
            _cowDeleteService = cowDeleteService;
            _applicationStateService = applicationStateService;
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
        }

        public List<CowListViewModel> CowList { get; set; }
        public SelectedCowViewModel SelectedCowViewModel { get; set; }
        
        public string SearchFilter { get; set; }

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

        public async Task DeleteCow(int cowID)
        {
            _applicationStateService.AddNotification(await _cowDeleteService.Delete(cowID)); 
            
            InitializeViewModel(); 
        }
    }
}