using System;
using System.ComponentModel.DataAnnotations;

namespace MilkUp.ViewModels.Cows
{
    public class LactationViewModel
    {
        [Required]
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }

        public int? LitersCollected { get; set; }
    }
}
