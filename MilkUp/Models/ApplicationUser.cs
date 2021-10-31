using Microsoft.AspNetCore.Identity;

namespace MilkUp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int CompanyID { get; set; }
    }
}
