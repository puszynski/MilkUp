using MilkUp.Models.Interfaces;
using System;

namespace MilkUp.Models
{
    public class Lactation : EntityBase, ICow
    {
        public int CowID { get; set; }
        public Cow Cow { get; set; }

        public DateTime From { get; set; }
        public DateTime? To { get; set; } //null when lactationing/starting

        //public int DayOfLactationing { get; set; } <= wyliczane z dat
        public int? LitersCollected { get; set; } //null when lactationing/starting
    }
}
