using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementCore.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required, EmailAddress]
        [Remote(action: "IsEmailInUse", controller: "Account")]
        [ValidEmailDomain(allowedDomain: "gmail.com")]
        public string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password does not match"), DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
