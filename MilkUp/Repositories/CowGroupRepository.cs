using MilkUp.Data;
using MilkUp.Models;
using MilkUp.Repositories.Interfaces;

namespace MilkUp.Repositories
{
    public class CowGroupRepository : DataManager<CowGroup, ApplicationDbContext>, ICowGroupRepository
    {
        public CowGroupRepository(ApplicationDbContext dataContext) : base(dataContext)
        {
        }

        //todo custom queries can be saved as methods    
    }
}
