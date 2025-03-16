using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using University.Models;
using University.ViewModels;
using UniversityDataAccess.Interface;
using UniversityModel.Models;

namespace University.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository<Enrollment> _enrollmentRepository;
        private readonly IRepository<Student> _studentRepository;
        private readonly IRepository<Course> _courseRepository;
        private readonly IRepository<Instructor> _instructorRepository;
        private readonly IRepository<CourseAssignment> _courseAssignmentRepository;



        public HomeController(ILogger<HomeController> logger, IRepository<Enrollment> enrollmentRepository,
            IRepository<Course> courseRepository, IRepository<Student> studentRepository,
            IRepository<Instructor> instructorRepository, IRepository<CourseAssignment> courseAssignmentRepository)
        {

            _logger = logger;
            _enrollmentRepository = enrollmentRepository;
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
            _instructorRepository = instructorRepository;
            _courseAssignmentRepository = courseAssignmentRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult StudentLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StudentLogin(StudentAndTeacherLogin studentLogin)
        {
            if (ModelState.IsValid)
            {
                var student = _studentRepository.GetAll().FirstOrDefault(s => s.Email == studentLogin.Email && s.Password == studentLogin.Password);
                if (student != null)
                {
                    var enrollments = _enrollmentRepository.GetAll().Where(e => e.StudentID == student.Id).ToList();
                    return View("StudentEnrollments", enrollments);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }
            return View(studentLogin);
        }
        public ActionResult Devlopers()
        {
            return View();
        }
        public IActionResult TeacherLogin()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public IActionResult TeacherLogin(StudentAndTeacherLogin teacherLogin)
        {
            if (ModelState.IsValid)
            {
                var teacher = _instructorRepository.GetAll().FirstOrDefault(t => t.Email == teacherLogin.Email && t.Password == teacherLogin.Password);
                if (teacher != null)
                {
                    var courses = _courseAssignmentRepository.GetAll()
                        .Where(ca => ca.InstructorID == teacher.Id)
                        .Select(ca => ca.Course)
                        .ToList();

                    var students = _enrollmentRepository.GetAll()
                        .Where(e => courses.Select(c => c.Id).Contains(e.CourseID))
                        .Select(e => e.Student)
                        .Distinct()
                        .ToList();

                    ViewBag.Courses = new SelectList(courses, "Id", "Title");
                    ViewBag.Students = new SelectList(students, "Id", "Name");

                    return RedirectToAction("Create", "Enrollment");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }
            return View(teacherLogin);
        }
    
    }
}
