using Microsoft.AspNetCore.Mvc;
using UniversityDataAccess;
using UniversityDataAccess.Interface;
using UniversityModel.Models;

namespace University.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IRepository<Student> _studentRepository ;
        private readonly IRepository<Instructor> _instructorRepository;
        private readonly IRepository<Course> _courseRepository;
        public DashboardController(IRepository<Student> _studentRepository, IRepository<Instructor> _instructorRepository, IRepository<Course> _courseRepository)
        {
            this._courseRepository = _courseRepository;
            this._instructorRepository = _instructorRepository;
            this._studentRepository = _studentRepository;


        }



        public IActionResult OpenDashboard()
        {
        
           
            List<int> ids = new List<int>();
            int c = _courseRepository.GetAll().Count();
            int i = _instructorRepository.GetAll().Count();
            int s = _studentRepository.GetAll().Count();
            ids.Add(s);
            ids.Add(c);
            ids.Add(i);

            return View(ids);
        }
    }
}
