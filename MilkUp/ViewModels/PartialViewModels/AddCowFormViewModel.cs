using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MilkUp.ViewModels.PartialViewModels
{
    public class AddCowFormViewModel
    {
        [Required]
        //[Range(typeof(bool), "true", "true", ErrorMessage = "Wybierz tak lub nie")]
        public bool IsFarmBorn { get; set; } = true;

        [Required]
        public int? NameOnFarm { get; set; }

        [Required]
        public string FarmID { get; set; }//todo farm name to display/filter
        public List<(string FarmID, string FarmName)> Farms { get; set; } = new List<(string FarmID, string FarmName)>() { ("1", "Gdańsk"), ("2", "Puszczykowo") };

        #region out of farm
        //[Required] todo
        public int? CowGroupID { get; set; }

        [Required]
        public int? EarringNumber { get; set; }

        [Required]
        public int? TransponderNumber { get; set; }
        #endregion


        #region born on farm
        public int? ParentID { get; set; }
        #endregion
    }
}
