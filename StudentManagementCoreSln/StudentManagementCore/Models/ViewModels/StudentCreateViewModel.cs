using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementCore.Models.ViewModels
{
    public class StudentCreateViewModel
    {
        public int Id { get; set; }
        [Required, MaxLength(50, ErrorMessage = "name connot exceed 50 charaters")]
        public string StudentName { get; set; }
        [Required]
        [Display(Name = "Student Email")]
        [RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal CourseFee { get; set; }
        [Required]
        public Course? Course { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile Photo { get; set; }
    }
}
