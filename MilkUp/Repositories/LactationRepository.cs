using MilkUp.Data;
using MilkUp.Models;
using MilkUp.Repositories.Interfaces;

namespace MilkUp.Repositories
{
    public class LactationRepository : DataManager<Lactation, ApplicationDbContext>, ILactationRepository
    {
        public LactationRepository(ApplicationDbContext dataContext) : base(dataContext)
        {
        }

        //todo custom queries can be saved as methods  
    }
}
