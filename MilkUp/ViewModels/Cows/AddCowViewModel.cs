﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MilkUp.ViewModels.Cows
{
    public class AddCowViewModel
    {
        [Required]
        //[Range(typeof(bool), "true", "true", ErrorMessage = "Wybierz tak lub nie")]
        public bool IsFarmBorn { get; set; } = true;

        [Required(ErrorMessage = "Pole wymagane")]
        public int? NameOnFarm { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        public string FarmID { get; set; }//todo farm name to display/filter


        #region out of farm
        [Required(ErrorMessage = "Pole wymagane")]
        public string CowGroupID { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        public int? EarringNumber { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        public int? TransponderNumber { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        public DateTime? BirthDate { get; set; }

        public List<LactationViewModel> LactationsViewModels { get; set; }
        #endregion


        #region born on farm
        public int? ParentID { get; set; }
        #endregion
    }
}
