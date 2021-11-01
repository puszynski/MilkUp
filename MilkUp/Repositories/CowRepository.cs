using MilkUp.Data;
using MilkUp.Models;
using MilkUp.Repositories.Interfaces;

namespace MilkUp.Repositories
{
    public class CowRepository : DataManager<Cow, ApplicationDbContext>, ICowRepository
    {
        public CowRepository(ApplicationDbContext dataContext) : base(dataContext)
        {
        }

        //todo custom queries can be saved as methods        
    }
}
