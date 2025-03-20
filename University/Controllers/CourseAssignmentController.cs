using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UniversityDataAccess.Interface;
using UniversityModel.Models;

namespace University.Controllers
{
    [Authorize(Roles = "Admin")]//cookie -Role :Admin
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

        public async Task<IActionResult> Index()
        {
            var courseAssignments = await _courseAssignmentRepository.GetAllAsync();
            return View(courseAssignments);
        }

        // GET: CourseAssignmentController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            await selectViewBag();
            var courseAssignment = await _courseAssignmentRepository.GetByIdAsync(id);
            return View(courseAssignment);
        }

        // GET: CourseAssignmentController/Create
        public async Task<IActionResult> Create()
        {
            await selectViewBag();
            return View();
        }

        // POST: CourseAssignmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseAssignment courseAssignment)
        {
            if (ModelState.IsValid)
            {
                await _courseAssignmentRepository.AddAsync(courseAssignment);
                return RedirectToAction(nameof(Index));
            }

            await selectViewBag();
            return View(courseAssignment);
        }

        // GET: CourseAssignmentController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            await selectViewBag();
            var courseAssignment = await _courseAssignmentRepository.GetByIdAsync(id);
            return View(courseAssignment);
        }

        // POST: CourseAssignmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CourseAssignment courseAssignment)
        {
            if (ModelState.IsValid)
            {
                await _courseAssignmentRepository.UpdateAsync(courseAssignment);
                return RedirectToAction(nameof(Index));
            }
            return View(courseAssignment);
        }

        // GET: CourseAssignmentController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            await selectViewBag();
            if (id is 0 or null)
            {
                return NotFound();
            }
            var courseAssignment = await _courseAssignmentRepository.GetByIdAsync(id.Value);
            return View(courseAssignment);
        }

        // POST: CourseAssignmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int courseId, int instructorId)
        {
            var courseAssignments = await _courseAssignmentRepository.GetAllAsync();
            var courseAssignment = courseAssignments.FirstOrDefault(ca => ca.CourseID == courseId && ca.InstructorID == instructorId);

            if (courseAssignment != null)
            {
                await _courseAssignmentRepository.DeleteAsync(courseAssignment);
                return RedirectToAction(nameof(Index));
            }
            return View(courseAssignment);
        }

        public async Task selectViewBag()
        {
            ViewBag.Instructor = (await _instructorRepository.GetAllAsync()).Select(i => new SelectListItem
            {
                Value = i.Id.ToString(),
                Text = i.Name
            }).ToList();

            ViewBag.Course = (await _courseRepository.GetAllAsync()).Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Title
            }).ToList();
        }
    }
}
