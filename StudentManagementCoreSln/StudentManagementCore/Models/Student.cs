using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementCore.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required, MaxLength(50, ErrorMessage = "name connot exceed 50 charaters")]
        public string StudentName { get; set; }
        [Required]
        [Display(Name = "Office Email")]
        [RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateOfBirth { get; set; }
        public decimal CourseFee { get; set; }
        [Required]
        public Course? Course { get; set; }
        public string ImageUrl { get; set; }
    }
}
