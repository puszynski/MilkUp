using MilkUp.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace MilkUp.Models
{
    public class Cow : EntityBase, ICompany
    {        
        public int NameOnFarm { get; set; }//ID used on farm
        public int EarringNumber { get; set; }//Reg1 – to numer kolczyka nadawany jest zaraz po urodzeniu (Ryc. 3).
        public string TransponderNumber { get; set; }
        //public int AllProID { get; set; }//numer transpondera który pozwala programowi AllPro na identyfikację zwierzęcia oraz zapisanie danych dotyczących doju

        public int FarmID { get; set; }
        public Farm Farm {  get; set; } 

        public int? ParentCowID { get; set; }
        public Cow ParentCow { get; set; } 

        public int? ParentBullID { get; set; }
        public Bull ParentBull { get; set; }

        public bool IsMale { get; set; }
        public DateTime BirthDate { get; set; }

        public ICollection<Lactation> Lactations {  get; set; }
        
        public int CowGroupID {  get; set; }
        public CowGroup CowGroup {  get; set; }

        public int CompanyID { get; set; }
        public Company Company { get; set; }

        //wpisujemy z łapy czy program wylicza?
        //public int DailyMilk { get; set; }//wydajnoć dzienna - specka pytanie czy tu czy w laktacji czy osobna tabela?

        //to chyba nie w bazie danych tylko wyliczamy na jakiejś podstawie?
        //public DateTime EstimatedLactationEnd { get; set; }//przewidywana data zasuszenia,
    }
}
