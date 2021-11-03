using System.Collections.Generic;

namespace MilkUp.Models
{
    public class Company : EntityBase
    {
        public string Name { get; set; }

        public ICollection<Farm> Farms { get; set; }
        public ICollection<CompanyPayment> CompanyPayments {  get; set; }
        public ICollection<CowGroup> CowGroups { get; set; }
        public ICollection<Cow> Cows { get; set; }
    }
}
