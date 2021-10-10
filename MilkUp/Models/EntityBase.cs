using System;

namespace MilkUp.Models
{
    public class EntityBase //todo generic - all addings, edits and deletings fill up automaticly dates
    {
        public int ID { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}