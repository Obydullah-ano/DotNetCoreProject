using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementCore.Models.Interfaces
{
    public interface IStudentRepository
    {
        Student GetStudentById(int id);
        Student SaveStudent(Student obj);
        Student UpdateStudent(Student obj);
        IEnumerable<Student> GetAllStudent();
        Student DeleteStudent(int id);
    }
}
