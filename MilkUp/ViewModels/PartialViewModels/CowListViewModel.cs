﻿namespace MilkUp.ViewModels
{
    public class CowListViewModel
    {
        public int ID { get; set; }//use by DB
        public int NameOnFarm { get; set; }//ID used on farm
        public int FarmID { get; set; }
        public int? ParentID { get; set; }
        public int LactationCount { get; set; }
        public bool IsFemale { get; set; }
    }
}
