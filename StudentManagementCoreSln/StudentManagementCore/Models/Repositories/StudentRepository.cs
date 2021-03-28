using StudentManagementCore.Context;
using StudentManagementCore.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementCore.Models.Repositories
{
    public class StudentRepository:IStudentRepository
    {
        private readonly Database _context;

        public StudentRepository(Database context)
        {
            _context = context;
        }
        public Student DeleteStudent(int id)
        {
            Student employee = GetStudentById(id);
            if (employee != null)
            {
                _context.Students.Remove(employee);
                _context.SaveChanges();
            }
            return employee;
        }

        public IEnumerable<Student> GetAllStudent()
        {
            return _context.Students;
        }

        public Student GetStudentById(int id)
        {
            Student employee = GetAllStudent().FirstOrDefault(e => e.Id == id);
            return employee;
        }

        public Student SaveStudent(Student obj)
        {
            _context.Students.Add(obj);
            _context.SaveChanges();
            return obj;
        }

        public Student UpdateStudent(Student obj)
        {
            var employee = _context.Students.Attach(obj);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return obj;
        }
    }
}
