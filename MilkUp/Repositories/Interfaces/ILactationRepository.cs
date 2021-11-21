using MilkUp.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MilkUp.Repositories.Interfaces
{
    public interface ILactationRepository : IRepository<Lactation>
    {
        Task<IQueryable<Lactation>> GetCowLactations(int cowID);
    }    
}
