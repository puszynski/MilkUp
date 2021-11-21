using MilkUp.Data;
using MilkUp.Models;
using MilkUp.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace MilkUp.Repositories
{
    public class LactationRepository : DataManager<Lactation, ApplicationDbContext>, ILactationRepository
    {
        public LactationRepository(ApplicationDbContext dataContext) : base(dataContext)
        {
        }

        public async virtual Task<IQueryable<Lactation>> GetCowLactations(int cowID) 
            => await base.GetQuery(x => x.CowID == cowID);
    }
}
