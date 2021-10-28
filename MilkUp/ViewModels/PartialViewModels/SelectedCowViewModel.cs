using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilkUp.ViewModels.PartialViewModels
{
    public class SelectedCowViewModel
    {
        public int ID { get; set; }
        public int NameOnFarm { get; set; }
        public string FarmName { get; set; }
        public int? ParentID { get; set; }
        public int LactationCount { get; set; }
        public bool IsFemale { get; set; }
    }
}
