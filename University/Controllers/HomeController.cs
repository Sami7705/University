using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
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
        public async Task<IActionResult> StudentLogin(StudentAndTeacherLogin studentLogin)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<Student> students = await _studentRepository.GetAllAsync();
                Student? student = students.FirstOrDefault(s => s.Email == studentLogin.Email && s.Password == studentLogin.Password);
                if (student != null)
                {
                    List<Enrollment> enrollments = (await _enrollmentRepository.GetAllAsync()).Where(e => e.StudentID == student.Id).ToList();
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
        public IActionResult ConectUs()
        {
            return View();
        }

        public IActionResult TeacherLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TeacherLogin(StudentAndTeacherLogin teacherLogin)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<Instructor> teachers = await _instructorRepository.GetAllAsync();
                Instructor? teacher = teachers.FirstOrDefault(t => t.Email == teacherLogin.Email && t.Password == teacherLogin.Password);
                if (teacher != null)
                {
                    IEnumerable<CourseAssignment> courseAssignments = await _courseAssignmentRepository.GetAllAsync();
                    List<Course?> courses = courseAssignments
                        .Where(ca => ca.InstructorID == teacher.Id)
                        .Select(ca => ca.Course)
                        .ToList();

                    ViewBag.Courses = new SelectList(courses, "Id", "Title");

                    return RedirectToAction("Instructor_Screen", teacher);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }
            return View(teacherLogin);
        }

        public async Task<IActionResult> Instructor_Screen(Instructor instructor)
        {
            IEnumerable<CourseAssignment> courseAssignments = await _courseAssignmentRepository.GetAllAsync();
            List<Course?> courses = courseAssignments
                .Where(ca => ca.InstructorID == instructor.Id)
                .Select(ca => ca.Course)
                .ToList();

            InstructorScreenViewModel viewModel = new()
            {
                Instructor = instructor,
                Courses = courses
            };

            return View(viewModel);
        }

        public async Task<IActionResult> GetStudentsByCourse(int courseId)
        {
            List<Enrollment> enrollments = (await _enrollmentRepository.GetAllAsync())
                .Where(e => e.CourseID == courseId)
                .ToList();

            return PartialView("_StudentList", enrollments);
        }
    }
}
