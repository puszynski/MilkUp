using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilkUp.Models
{
    public class CowGroup : EntityBase
    {
        public int ID { get; set; }
        public string Name {  get; set; }
        public ICollection<Cow> Cows { get; set; }
    }
}
