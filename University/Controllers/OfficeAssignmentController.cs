using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniversityDataAccess.Interface;
using UniversityModel.Models;

namespace University.Controllers
{
    public class OfficeAssignmentController : Controller
    {
        private readonly IRepository<OfficeAssignment> _officeAssignmentRepository;
        private readonly IRepository<Instructor> _instructorRepository;

        public OfficeAssignmentController(IRepository<OfficeAssignment> officeAssignmentRepository, IRepository<Instructor> instructorRepository)
        {
            _officeAssignmentRepository = officeAssignmentRepository;
            _instructorRepository = instructorRepository;
        }

        public IActionResult Index()
        {
            return View(_officeAssignmentRepository.GetAll());
        }

        // GET: OfficeAssignmentController/Details/5
        public IActionResult Details(int id)
        {
            selectViewBag();
            var officeAssignment = _officeAssignmentRepository.GetById(id);
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
        public IActionResult Create(OfficeAssignment officeAssignment)
        {
            if (ModelState.IsValid)
            {
                _officeAssignmentRepository.Add(officeAssignment);
                return RedirectToAction(nameof(Index));
            }

            selectViewBag();
            return View(officeAssignment);
        }

        // GET: OfficeAssignmentController/Edit/5
        public IActionResult Edit(int id)
        {
            selectViewBag();
            var officeAssignment = _officeAssignmentRepository.GetById(id);
            return View(officeAssignment);
        }

        // POST: OfficeAssignmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(OfficeAssignment officeAssignment)
        {
            if (ModelState.IsValid)
            {
                _officeAssignmentRepository.Update(officeAssignment);
                return RedirectToAction(nameof(Index));
            }
            return View(officeAssignment);
        }

        // GET: OfficeAssignmentController/Delete/5
        public IActionResult Delete(int? id)
        {
            selectViewBag();
            if (id is 0 or null)
            {
                return NotFound();
            }
            var officeAssignment = _officeAssignmentRepository.GetById(id.Value);
            return View(officeAssignment);
        }

        // POST: OfficeAssignmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(OfficeAssignment officeAssignment)
        {
            if (officeAssignment != null)
            {
                _officeAssignmentRepository.Delete(officeAssignment);
                return RedirectToAction(nameof(Index));
            }
            return View(officeAssignment);
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
