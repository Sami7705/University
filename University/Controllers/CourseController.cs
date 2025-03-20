using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniversityDataAccess.Interface;
using UniversityModel.Models;

namespace University.Controllers
{
    [Authorize(Roles = "Admin")]//cookie -Role :Admin
    public class CourseController : Controller
    {
        private readonly IRepository<Course> _courseRepository;
        private readonly IRepository<Department> _departmentRepository;

        public CourseController(IRepository<Course> courseRepository, IRepository<Department> departmentRepository)
        {
            _courseRepository = courseRepository;
            _departmentRepository = departmentRepository;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _courseRepository.GetAllAsync();
            return View(courses);
        }

        // GET: CourseController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            await selectViewBag();
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // GET: CourseController/Create
        public async Task<IActionResult> Create()
        {
            await selectViewBag();
            return View();
        }

        // POST: CourseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {
            if (ModelState.IsValid)
            {
                await _courseRepository.AddAsync(course);
                return RedirectToAction(nameof(Index));
            }
            await selectViewBag();
            return View(course);
        }

        // GET: CourseController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            await selectViewBag();
            return View(course);
        }

        // POST: CourseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Course course)
        {
            if (ModelState.IsValid)
            {
                await _courseRepository.UpdateAsync(course);
                return RedirectToAction(nameof(Index));
            }
            await selectViewBag();
            return View(course);
        }

        // GET: CourseController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            await selectViewBag();
            if (id == null)
            {
                return NotFound();
            }
            var course = await _courseRepository.GetByIdAsync(id.Value);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: CourseController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Course course)
        {
            if (course != null)
            {
                await _courseRepository.DeleteAsync(course);
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        private async Task selectViewBag()
        {
            ViewBag.Department = (await _departmentRepository.GetAllAsync()).Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Name
            }).ToList();
        }
    }
}
