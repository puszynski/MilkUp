using MilkUp.Data;
using MilkUp.Models;
using MilkUp.Repositories.Interfaces;

namespace MilkUp.Repositories
{
    public class FarmRepository : DataManager<Farm, ApplicationDbContext>, IFarmRepository
    {
        public FarmRepository(ApplicationDbContext dataContext) : base(dataContext)
        {

        }
    }
}
