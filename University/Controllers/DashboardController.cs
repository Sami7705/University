using Microsoft.AspNetCore.Mvc;
using UniversityDataAccess.Interface;
using UniversityModel.Models;
using University.ViewModels;
using System.Threading.Tasks;
using System.Linq;

namespace University.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IRepository<Student> _studentRepository;
        private readonly IRepository<Instructor> _instructorRepository;
        private readonly IRepository<Course> _courseRepository;
        private readonly IRepository<OfficeAssignment> _officeAssignmentRepository;
        private readonly IRepository<CourseAssignment> _courseAssignmentRepository;
        private readonly IRepository<Enrollment> _enrollmentRepository;

        public DashboardController(
            IRepository<Student> studentRepository,
            IRepository<Instructor> instructorRepository,
            IRepository<Course> courseRepository,
            IRepository<OfficeAssignment> officeAssignmentRepository,
            IRepository<CourseAssignment> courseAssignmentRepository,
            IRepository<Enrollment> enrollmentRepository)
        {
            _studentRepository = studentRepository;
            _instructorRepository = instructorRepository;
            _courseRepository = courseRepository;
            _officeAssignmentRepository = officeAssignmentRepository;
            _courseAssignmentRepository = courseAssignmentRepository;
            _enrollmentRepository = enrollmentRepository;
        }

        public async Task<IActionResult> OpenDashboard()
        {
            var dashboardData = new DashboardViewModel
            {
                StudentsCount = (await _studentRepository.GetAllAsync()).Count(),
                InstructorsCount = (await _instructorRepository.GetAllAsync()).Count(),
                CoursesCount = (await _courseRepository.GetAllAsync()).Count(),
                OfficesCount = (await _officeAssignmentRepository.GetAllAsync()).Count(), // عدد المكاتب
                EnrollmentsCount = (await _enrollmentRepository.GetAllAsync()).Count(),  // عدد التسجيلات
                CourseAssignmentsCount = (await _courseAssignmentRepository.GetAllAsync()).Count(), // عدد CourseAssignment
                VisitorsCount = 101523,
                SubscribersCount = 635,
                MessagesCount = 201
            };

            return View(dashboardData);
        }
    }
}
