using MilkUp.Models.Interfaces;
using System.Collections.Generic;

namespace MilkUp.Models
{
    public class CowGroup : EntityBase, ICompany
    {
        public string Name {  get; set; }
        public ICollection<Cow> Cows { get; set; }


        public int CompanyID { get; set; }
        public Company Company { get; set; }
    }
}
