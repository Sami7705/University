using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniversityDataAccess.Interface;
using UniversityModel.Models;

namespace University.Controllers
{
    [Authorize(Roles = "Admin")]//cookie -Role :Admin
    public class DepartmentController : Controller
    {
        private readonly IRepository<Department> _departmentRepository;
        private readonly IRepository<Instructor> _instructorRepository;

        public DepartmentController(IRepository<Department> departmentRepository, IRepository<Instructor> instructorRepository)
        {
            _departmentRepository = departmentRepository;
            _instructorRepository = instructorRepository;
        }

        public async Task<IActionResult> Index()
        {
            var departments = await _departmentRepository.GetAllAsync();
            return View(departments);
        }

        // GET: DepartmentController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            await selectViewBag();
            var department = await _departmentRepository.GetByIdAsync(id);
            return View(department);
        }

        // GET: DepartmentController/Create
        public async Task<IActionResult> Create()
        {
            await selectViewBag();
            return View();
        }

        // POST: DepartmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                await _departmentRepository.AddAsync(department);
                return RedirectToAction(nameof(Index));
            }

            await selectViewBag();
            return View(department);
        }

        // GET: DepartmentController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            await selectViewBag();
            var department = await _departmentRepository.GetByIdAsync(id);
            return View(department);
        }

        // POST: DepartmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                await _departmentRepository.UpdateAsync(department);
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: DepartmentController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            await selectViewBag();
            if (id is 0 or null)
            {
                return NotFound();
            }
            var department = await _departmentRepository.GetByIdAsync(id.Value);
            return View(department);
        }

        // POST: DepartmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Department department)
        {
            if (department != null)
            {
                await _departmentRepository.DeleteAsync(department);
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        public async Task selectViewBag()
        {
            ViewBag.Instructor = (await _instructorRepository.GetAllAsync()).Select(i => new SelectListItem
            {
                Value = i.Id.ToString(),
                Text = i.Name
            }).ToList();
        }
    }
}
