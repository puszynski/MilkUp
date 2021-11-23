namespace MilkUp.ViewModels.Cows
{
    public class SelectedCowViewModel
    {
        public int ID { get; set; }
        public string NameOnFarm { get; set; }
        public string FarmName { get; set; }
        public int? ParentID { get; set; }
        public int LactationCount { get; set; }
        public bool IsFemale { get; set; }
    }
}
