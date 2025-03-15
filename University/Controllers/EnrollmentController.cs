using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniversityDataAccess.Interface;
using UniversityModel.Models;

namespace University.Controllers
{
    [Authorize]
    public class EnrollmentController : Controller
    {
        private readonly IRepository<Enrollment> _enrollmentRepository;
        private readonly IRepository<Student> _studentRepository;
        private readonly IRepository<Course> _courseRepository;
        public EnrollmentController(IRepository<Enrollment> enrollmentRepository, IRepository<Student> studentRepository, IRepository<Course> courseRepository)
        {
            _enrollmentRepository = enrollmentRepository;
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
        }
        public ActionResult Index()
        {
            return View(_enrollmentRepository.GetAll());
        }

        // GET: EnrollmentController/Details/5
        public ActionResult Details(int id)
        {
            selectViewBag();
            var enrollment = _enrollmentRepository.GetById(id);
            
            return View(enrollment);
        }

        // GET: EnrollmentController/Create
        public ActionResult Create()
        {
            selectViewBag();

            return View();
        }
  


        // POST: EnrollmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                _enrollmentRepository.Add(enrollment);
                return RedirectToAction(nameof(Index));
            }

           selectViewBag(); 

            return View(enrollment);
        }

        // GET: EnrollmentController/Edit/5
        public ActionResult Edit(int id)
        {
            selectViewBag();
            return View();
        }

        // POST: EnrollmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Enrollment enrollment)
        {

            if (ModelState.IsValid)
            {
                _enrollmentRepository.Update(enrollment);
                return RedirectToAction(nameof(Index));
            }
            return View(enrollment);
        }

        // GET: EnrollmentController/Delete/5
        public ActionResult Delete(int? id)
        {selectViewBag();
            if (id is 0 or null)
            {
                return NotFound();
            }
            var e = _enrollmentRepository.GetById(id.Value);
           
            return View(e);
        }

        // POST: EnrollmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Enrollment enrollment)
        {
           
            
            if (enrollment != null) 
            {
                _enrollmentRepository.Delete(enrollment);
                return RedirectToAction("Index");
            }

                return View(enrollment);
            

        }
        public void selectViewBag()
        {
            ViewBag.Student = _studentRepository.GetAll().Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name
            }).ToList();

            ViewBag.Course = _courseRepository.GetAll().Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Title
            }).ToList();
        }
    }
}
