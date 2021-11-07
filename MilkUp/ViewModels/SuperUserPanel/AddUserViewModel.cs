﻿using System.ComponentModel.DataAnnotations;

namespace MilkUp.ViewModels.SuperUserPanel
{
    public class AddUserViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string CompanyID { get; set; }

        [Required]
        public int RoleName { get; set; }
    }
}
