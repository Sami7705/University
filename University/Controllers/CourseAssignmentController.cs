using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UniversityDataAccess.Interface;
using UniversityModel.Models;

namespace University.Controllers
{
    public class CourseAssignmentController : Controller
    {
        private readonly IRepository<CourseAssignment> _courseAssignmentRepository;
        private readonly IRepository<Instructor> _instructorRepository;
        private readonly IRepository<Course> _courseRepository;

        public CourseAssignmentController(IRepository<CourseAssignment> courseAssignmentRepository, IRepository<Instructor> instructorRepository, IRepository<Course> courseRepository)
        {
            _courseAssignmentRepository = courseAssignmentRepository;
            _instructorRepository = instructorRepository;
            _courseRepository = courseRepository;
        }

        public IActionResult Index()
        {
            return View(_courseAssignmentRepository.GetAll());
        }

        // GET: CourseAssignmentController/Details/5
        public IActionResult Details(int id)
        {
            selectViewBag();
            var courseAssignment = _courseAssignmentRepository.GetById(id);
            return View(courseAssignment);
        }

        // GET: CourseAssignmentController/Create
        public IActionResult Create()
        {
            selectViewBag();
            return View();
        }

        // POST: CourseAssignmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CourseAssignment courseAssignment)
        {
            if (ModelState.IsValid)
            {
                _courseAssignmentRepository.Add(courseAssignment);
                return RedirectToAction(nameof(Index));
            }

            selectViewBag();
            return View(courseAssignment);
        }

        // GET: CourseAssignmentController/Edit/5
        public IActionResult Edit(int id)
        {
            selectViewBag();
            var courseAssignment = _courseAssignmentRepository.GetById(id);
            return View(courseAssignment);
        }

        // POST: CourseAssignmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CourseAssignment courseAssignment)
        {
            if (ModelState.IsValid)
            {
                _courseAssignmentRepository.Update(courseAssignment);
                return RedirectToAction(nameof(Index));
            }
            return View(courseAssignment);
        }

        // GET: CourseAssignmentController/Delete/5
        public IActionResult Delete(int? id)
        {
            selectViewBag();
            if (id is 0 or null)
            {
                return NotFound();
            }
            var courseAssignment = _courseAssignmentRepository.GetById(id.Value);
            return View(courseAssignment);
        }

        // ...

        // POST: CourseAssignmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int courseId, int instructorId)
        {
            var courseAssignment = _courseAssignmentRepository
                .GetAll()
                .FirstOrDefault(ca => ca.CourseID == courseId && ca.InstructorID == instructorId);

            if (courseAssignment != null)
            {
                _courseAssignmentRepository.Delete(courseAssignment);
                return RedirectToAction(nameof(Index));
            }
            return View(courseAssignment);
        }

        public void selectViewBag()
        {
            ViewBag.Instructor = _instructorRepository.GetAll().Select(i => new SelectListItem
            {
                Value = i.Id.ToString(),
                Text = i.Name
            }).ToList();

            ViewBag.Course = _courseRepository.GetAll().Select(c => new SelectListItem
            {
                Value = c.Id.ToString(), 
                Text = c.Title
            }).ToList();
        }
    }
}
