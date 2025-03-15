using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
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

        public HomeController(ILogger<HomeController> logger, IRepository<Enrollment> enrollmentRepository, IRepository<Course> courseRepository, IRepository<Student> studentRepository)
        {
            _logger = logger;
            _enrollmentRepository = enrollmentRepository;
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
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
        public IActionResult StudentLogin(StudentLogin studentLogin)
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
    }
}
