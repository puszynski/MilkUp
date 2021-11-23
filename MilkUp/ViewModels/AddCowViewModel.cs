using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using MilkUp.Data;
using MilkUp.Models;
using MilkUp.Repositories.Interfaces;
using MilkUp.Services;
using MilkUp.ViewModels.AddCow;
using MilkUp.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MilkUp.ViewModels
{
    public class AddCowViewModel : BaseViewModel, IAddCowViewModel
    {
        readonly ICowRepository _cowRepository;
        readonly NavigationManager _navigationManager;
        readonly ApplicationStateService _applicationStateService;

        public AddCowViewModel(ICowRepository cowRepository,
                               NavigationManager navigationManager,
                               ApplicationStateService applicationStateService,
                               AuthenticationStateProvider authenticationStateProvider,
                               ApplicationDbContext applicationDbContext,
                               UserManager<ApplicationUser> userManager)
            : base(authenticationStateProvider,
                   applicationDbContext,
                   userManager)
        {
            _cowRepository = cowRepository;
            _navigationManager = navigationManager;
            _applicationStateService = applicationStateService;

            Farms = applicationDbContext.Farms
                .Where(x => !x.DateDeleted.HasValue)
                .Where(x => x.CompanyID == _userCompanyID.Value)
                .Select(x => new Tuple<string, string>(x.ID.ToString(), x.Name))
                .AsEnumerable()
                .Select(x => (FarmID: x.Item1, FarmName: x.Item2))
                .ToList();

            CowGroups = applicationDbContext.CowGroups
                .Where(x => !x.DateDeleted.HasValue)
                .Where(x => x.CompanyID == _userCompanyID.Value)
                .Select(x => new Tuple<string, string>(x.ID.ToString(), x.Name))
                .AsEnumerable()
                .Select(x => (ID: x.Item1, Name: x.Item2))
                .ToList();
        }

        [Required]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Wybierz tak lub nie")]
        public bool IsFarmBorn { get; set; } = true;

        [Required(ErrorMessage = "Pole wymagane")]
        public string NameOnFarm { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        public string FarmID { get; set; }//todo farm name to display/filter


        #region out of farm
        [Required(ErrorMessage = "Pole wymagane")]
        public string CowGroupID { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        public int? EarringNumber { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        [RegularExpression(@"^[A-Za-z]{2}[0-9]{12}", ErrorMessage = "Pole transpondera musi składać sie z 2 liter oraz 12 cyfr")]
        public string TransponderNumber { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        public DateTime? BirthDate { get; set; }

        public List<LactationViewModel> LactationsViewModels { get; set; } = new List<LactationViewModel>();
        #endregion

        #region born on farm
        public int? ParentID { get; set; }
        #endregion


        public List<(string FarmID, string FarmName)> Farms { get; }
        public List<(string ID, string Name)> CowGroups { get; }

        public async Task AddNewCowNewLactation()
        {
            LactationsViewModels.Add(new LactationViewModel() { From = DateTime.UtcNow });
        }

        public async Task AddNewCow()
        {
            await Task.Delay(0);

            //todo walidacja + walidacja parsowań

            var cow = new Cow()
            {
                NameOnFarm = NameOnFarm,
                EarringNumber = EarringNumber.Value,
                TransponderNumber = TransponderNumber,
                BirthDate = BirthDate.Value,
                FarmID = int.Parse(FarmID),
                CowGroupID = int.Parse(CowGroupID),
                CompanyID = _userCompanyID.Value
            };

            var lactationsToAdd = new List<Lactation>();

            foreach (var lact in LactationsViewModels)
            {
                var lactation = new Lactation()
                {
                    Cow = cow,
                    From = lact.From.Value,
                    To = lact.To,
                    LitersCollected = lact.LitersCollected
                };
                lactationsToAdd.Add(lactation);
            }

            cow.Lactations = lactationsToAdd;

            try
            {
                var newCow = _cowRepository.Insert(cow).Result;
                await _cowRepository.Save();
            }
            catch (Exception ex)
            {
                //todo - wyświetl coś ładnego + dodaj loga
                _applicationStateService.AddNotification(ex.Message, Enums.ENotificationType.Error);
            }

            _applicationStateService.AddNotification("Dodano nową krowe", Enums.ENotificationType.Success);
            //todo trzeba wyczyścić dane bo po ponownym wejściu są te same dane w formularzu
            _navigationManager.NavigateTo("/cows");
        }
    }
}
