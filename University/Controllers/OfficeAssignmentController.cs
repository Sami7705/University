using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniversityDataAccess.Interface;
using UniversityModel.Models;

namespace University.Controllers
{
    [Authorize(Roles = "Admin")]//cookie -Role :Admin
    public class OfficeAssignmentController : Controller
    {
        private readonly IRepository<OfficeAssignment> _officeAssignmentRepository;
        private readonly IRepository<Instructor> _instructorRepository;

        public OfficeAssignmentController(IRepository<OfficeAssignment> officeAssignmentRepository, IRepository<Instructor> instructorRepository)
        {
            _officeAssignmentRepository = officeAssignmentRepository;
            _instructorRepository = instructorRepository;
        }

        public async Task<IActionResult> Index()
        {
            var officeAssignments = await _officeAssignmentRepository.GetAllAsync();
            return View(officeAssignments);
        }

        // GET: OfficeAssignmentController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            selectViewBag();
            var officeAssignment = await _officeAssignmentRepository.GetByIdAsync(id);
            return View(officeAssignment);
        }

        // GET: OfficeAssignmentController/Create
        public IActionResult Create()
        {
            selectViewBag();
            return View();
        }

        // POST: OfficeAssignmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OfficeAssignment officeAssignment)
        {
            if (ModelState.IsValid)
            {
                await _officeAssignmentRepository.AddAsync(officeAssignment);
                return RedirectToAction(nameof(Index));
            }

            selectViewBag();
            return View(officeAssignment);
        }

        // GET: OfficeAssignmentController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            selectViewBag();
            var officeAssignment = await _officeAssignmentRepository.GetByIdAsync(id);
            return View(officeAssignment);
        }

        // POST: OfficeAssignmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OfficeAssignment officeAssignment)
        {
            if (ModelState.IsValid)
            {
                await _officeAssignmentRepository.UpdateAsync(officeAssignment);
                return RedirectToAction(nameof(Index));
            }
            return View(officeAssignment);
        }

        // GET: OfficeAssignmentController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            selectViewBag();
            if (id is 0 or null)
            {
                return NotFound();
            }
            var officeAssignment = await _officeAssignmentRepository.GetByIdAsync(id.Value);
            return View(officeAssignment);
        }

        // POST: OfficeAssignmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(OfficeAssignment officeAssignment)
        {
            if (officeAssignment != null)
            {
                await _officeAssignmentRepository.DeleteAsync(officeAssignment);
                return RedirectToAction(nameof(Index));
            }
            return View(officeAssignment);
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
