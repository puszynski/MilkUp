using System;
using System.ComponentModel.DataAnnotations;

namespace MilkUp.ViewModels.AddCow
{
    public class LactationViewModel
    {
        [Required(ErrorMessage = "Pole wymagane")]
        public DateTime? From { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        public DateTime? To { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        public int? LitersCollected { get; set; }
    }
}
