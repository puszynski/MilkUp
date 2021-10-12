namespace MilkUp.ViewModels.PartialViewModels
{
    public class AddCowFormViewModel
    {
        public int ID { get; set; }
        public int NameOnFarm { get; set; }
        public bool IsFarmBorn { get; set; }
        public bool IsFemale { get; set; }
        public int? ParentID { get; set; }
        public int FarmID { get; set; }//todo farm name to display/filter
    }
}
