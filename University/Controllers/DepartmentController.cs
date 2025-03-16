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

        public IActionResult Index()
        {
            return View(_departmentRepository.GetAll());
        }

        // GET: DepartmentController/Details/5
        public IActionResult Details(int id)
        {
            selectViewBag();
            var department = _departmentRepository.GetById(id);
            return View(department);
        }

        // GET: DepartmentController/Create
        public IActionResult Create()
        {
            selectViewBag();
            return View();
        }

        // POST: DepartmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                _departmentRepository.Add(department);
                return RedirectToAction(nameof(Index));
            }

            selectViewBag();
            return View(department);
        }

        // GET: DepartmentController/Edit/5
        public IActionResult Edit(int id)
        {
            selectViewBag();
            var department = _departmentRepository.GetById(id);
            return View(department);
        }

        // POST: DepartmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                _departmentRepository.Update(department);
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: DepartmentController/Delete/5
        public IActionResult Delete(int? id)
        {
            selectViewBag();
            if (id is 0 or null)
            {
                return NotFound();
            }
            var department = _departmentRepository.GetById(id.Value);
            return View(department);
        }

        // POST: DepartmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Department department)
        {
            if (department != null)
            {
                _departmentRepository.Delete(department);
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        public void selectViewBag()
        {
            ViewBag.Instructor = _instructorRepository.GetAll().Select(i => new SelectListItem
            {
                Value = i.Id.ToString(),
                Text = i.Name
            }).ToList();
        }
    }
}
