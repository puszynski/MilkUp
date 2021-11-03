using System;

namespace MilkUp.ViewModels.Cows
{
    public class LactationViewModel
    {
        public DateTime From { get; set; }
        public DateTime? To { get; set; } 
        public int DayOfLactationing { get; set; }
        public int? LitersCollected { get; set; }
    }
}
