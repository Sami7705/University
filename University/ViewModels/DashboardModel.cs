using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityDataAccess.Interface;
using UniversityModel.Models;
namespace University.ViewModels
{
    public class  DashboardModel : PageModel
    {
        private readonly IRepository<Student> _studentRepository;
        private readonly IRepository<Instructor> _instructorRepository;
        private readonly IRepository<Course> _courseRepository;
        public DashboardModel(IRepository<Student> studentRepository, IRepository<Instructor> repository,
            IRepository<Course> course)
        {
            _courseRepository = course;
            _instructorRepository = repository;
            _studentRepository = studentRepository;
        }



        public int TotalStudents { get; set; }
        public int TotalProfessors { get; set; }
        public int TotalCourses { get; set; }

        public void OnGet()
        {
            // Fetch data from the database
            TotalStudents = _studentRepository.GetAll().Count();
            TotalProfessors = _instructorRepository.GetAll().Count();
            TotalCourses = _courseRepository.GetAll().Count();
        }
    }
}