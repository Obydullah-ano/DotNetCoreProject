using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PagedList;
//using PagedList.Core;
using StudentManagementCore.Models;
using StudentManagementCore.Models.Interfaces;
using StudentManagementCore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementCore.Controllers
{
    //[Authorize(Roles = "User")]
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IHostingEnvironment _hostingEnvironment;

        public StudentController(IStudentRepository studentRepository, IHostingEnvironment hostingEnvironment)
        {
            _studentRepository = studentRepository;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Details(int id)
        {
            Student obj = _studentRepository.GetStudentById(id);
            //ViewData["student"] = obj;
            //ViewBag.student = obj;
            return View(obj);
        }
        public IActionResult Index(string SearchString, string CurrentFilter, string sortOrder, int? Page)
        {
            ViewBag.SortNameParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            if (SearchString!=null)
            {
                Page = 1;
            }
            else
            {
                SearchString = CurrentFilter;
            }
            ViewBag.CurrentFilter = SearchString;
            List<Student> list = _studentRepository.GetAllStudent().ToList();
            if (!String.IsNullOrEmpty(SearchString))
            {
                list = list.Where(n => n.StudentName.ToUpper().Contains(SearchString.ToUpper())).ToList();
            }
            switch (sortOrder)
            {
                case "name_desc":
                    list = list.OrderByDescending(n => n.StudentName).ToList();
                    break;
                default:
                    break;
            }
            int PageSize = 3;
            int PageNumber = (Page ?? 1);
            return View("Index", list.ToPagedList(PageNumber, PageSize));            
        }
        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(StudentCreateViewModel viewobj)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadFile(viewobj);
                Student obj = new Student()
                {
                    StudentName = viewobj.StudentName,
                    Email = viewobj.Email,
                    DateOfBirth = viewobj.DateOfBirth,
                    CourseFee = viewobj.CourseFee,
                    Course = viewobj.Course,
                    ImageUrl = uniqueFileName

                };
                Student newstudent = _studentRepository.SaveStudent(obj);
                return RedirectToAction("Details", new { id = newstudent.Id });
            }
            else
            {
                return View();
            }


        }
        [HttpPost]
        public IActionResult Deletestudent(int id)
        {
            Student student = _studentRepository.GetStudentById(id);

            if (student.ImageUrl != null)
            {
                string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Images", student.ImageUrl);
                System.IO.File.Delete(filePath);
            }
            Student deletestudent = _studentRepository.DeleteStudent(id);
            return RedirectToAction("Index");
        }
        public ViewResult Edit(int id)
        {
            Student student = _studentRepository.GetStudentById(id);
            EditStudentViewModel model = new EditStudentViewModel()
            {
                Id = student.Id,
                StudentName = student.StudentName,
                Email = student.Email,
                DateOfBirth = student.DateOfBirth,
                CourseFee = student.CourseFee,
                Course = student.Course,
                ExistingPhotopath = student.ImageUrl

            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(EditStudentViewModel viewobj)
        {
            if (ModelState.IsValid)
            {
                Student student = _studentRepository.GetStudentById(viewobj.Id);
                student.StudentName = viewobj.StudentName;
                student.Email = viewobj.Email;
                student.DateOfBirth = viewobj.DateOfBirth;
                student.CourseFee = viewobj.CourseFee;
                student.Course = viewobj.Course;
                if (student.ImageUrl != null)
                {
                    string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Images", student.ImageUrl);
                    System.IO.File.Delete(filePath);
                }
                student.ImageUrl = ProcessUploadFile(viewobj);
                Student newstudent = _studentRepository.UpdateStudent(student);
                return RedirectToAction("Details", new { id = newstudent.Id });
            }
            else
            {
                return View(viewobj);
            }
        }

        private string ProcessUploadFile(StudentCreateViewModel viewobj)
        {
            string uniqueFileName = null;
            var files = HttpContext.Request.Form.Files;
            foreach (var image in files)
            {
                if (image != null && image.Length > 0)
                {
                    var file = image;
                    var uploadFile = Path.Combine(_hostingEnvironment.WebRootPath, "Images");
                    if (file.Length > 0)
                    {
                        var fileName = Guid.NewGuid().ToString().Replace("_", "") + file.FileName;
                        using (var fileStream = new FileStream(Path.Combine(uploadFile, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                            uniqueFileName = fileName;
                        }
                    }
                }
            }           
            return uniqueFileName;
        }
    }
}
