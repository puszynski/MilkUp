using System.Collections.Generic;

namespace MilkUp.Models
{
    public class Company : EntityBase
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ICollection<Farm> Farms { get; set; }
        public ICollection<CompanyPayment> CompanyPayments {  get; set; }
    }
}
