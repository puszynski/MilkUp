using MilkUp.Models.Interfaces;

namespace MilkUp.Models
{
    public class Farm : EntityBase, ICompany
    {
        public string Name { get; set; }
        public int CompanyID { get; set; }
        public Company Company { get; set; }
    }
}