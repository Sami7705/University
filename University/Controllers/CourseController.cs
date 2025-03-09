using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniversityDataAccess.Interface;
using UniversityModel.Models;

namespace University.Controllers
{
    public class CourseController : Controller
    {
        private readonly IRepository<Course> _courseRepository;
        private readonly IRepository<Department> _departmentRepository;

        public CourseController(IRepository<Course> courseRepository, IRepository<Department> departmentRepository)
        {
            _courseRepository = courseRepository;
            _departmentRepository = departmentRepository;
        }

        public ActionResult Index()
        {
            return View(_courseRepository.GetAll());
        }

        // GET: CourseController/Details/5
        public ActionResult Details(int id)
        { 
            selectViewBag();
            var course = _courseRepository.GetById(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // GET: CourseController/Create
        public ActionResult Create()
        {
            selectViewBag();
            return View();
        }

        // POST: CourseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Course course)
        {
            if (ModelState.IsValid)
            {
                _courseRepository.Add(course);
                return RedirectToAction(nameof(Index));
            }
            selectViewBag();
            return View(course);
        }

        // GET: CourseController/Edit/5
        public ActionResult Edit(int id)
        {
            var course = _courseRepository.GetById(id);
            if (course == null)
            {
                return NotFound();
            }
            selectViewBag();
            return View(course);
        }

        // POST: CourseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Course course)
        {
            if (ModelState.IsValid)
            {
                _courseRepository.Update(course);
                return RedirectToAction(nameof(Index));
            }
            selectViewBag();
            return View(course);
        }

        // GET: CourseController/Delete/5
        public ActionResult Delete(int? id)
        {
            selectViewBag();
            if (id == null)
            {
                return NotFound();
            }
            var course = _courseRepository.GetById(id.Value);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: CourseController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Course course)
        {
            if (course != null)
            {
                _courseRepository.Delete(course);
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        private void selectViewBag()
        {
            ViewBag.Department = _departmentRepository.GetAll().Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Name
            }).ToList();
        }
    }
}
