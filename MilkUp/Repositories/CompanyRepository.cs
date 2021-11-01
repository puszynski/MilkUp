using MilkUp.Data;
using MilkUp.Models;
using MilkUp.Repositories.Interfaces;

namespace MilkUp.Repositories
{
    public class CompanyRepository : DataManager<Company, ApplicationDbContext>, ICompanyRepository
    {
        public CompanyRepository(ApplicationDbContext dataContext) : base(dataContext)
        {
        }
    }
}
