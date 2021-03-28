using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementCore.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
               new Student { Id = 1, StudentName = "Obydullah", Email = "obydullah@gmai.com", Course = Course.Java },
               new Student { Id = 2, StudentName = "Sadia", Email = "sadia@gmai.com", Course = Course.CSharp }
               );
        }
    }
}
