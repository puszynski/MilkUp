using System;

namespace MilkUp.Models
{
    public class Lactation : EntityBase
    {
        public int CowID { get; set; }
        public Cow Cow { get; set; }

        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public int DayOfLactationing { get; set; }
        public int? LitersCollected { get; set; }//null when lactationing
    }
}
