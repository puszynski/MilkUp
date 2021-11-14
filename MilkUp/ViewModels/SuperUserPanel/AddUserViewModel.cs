using System.ComponentModel.DataAnnotations;

namespace MilkUp.ViewModels.SuperUserPanel
{
    public class AddUserViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        //[Required] for superAdmin
        public string CompanyID { get; set; }

        //[Required] for superAdmin
        public string RoleName { get; set; }
    }
}
