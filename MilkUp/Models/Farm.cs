namespace MilkUp.Models
{
    public class Farm : EntityBase
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int CompanyID { get; set; }
    }
}