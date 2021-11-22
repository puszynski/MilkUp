using MilkUp.ViewModels.AddCow;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MilkUp.ViewModels.Interfaces
{
    public interface IAddCowViewModel
    {
        bool IsFarmBorn { get; set; }
        string NameOnFarm { get; set; }
        string FarmID { get; set; }
        string CowGroupID { get; set; }
        int? EarringNumber { get; set; }
        string TransponderNumber { get; set; }
        DateTime? BirthDate { get; set; }
        List<LactationViewModel> LactationsViewModels { get; set; }
        int? ParentID { get; set; }

        List<(string FarmID, string FarmName)> Farms { get; }
        List<(string ID, string Name)> CowGroups { get; }

        Task AddNewCow();
        Task AddNewCowNewLactation();
    }
}
