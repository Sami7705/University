using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniversityDataAccess.Interface;
using UniversityModel.Models;

namespace University.Controllers
{
  
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

        public async Task<IActionResult> Index()
        {
            var enrollments = await _enrollmentRepository.GetAllAsync();
            return View(enrollments);
        }

        // GET: EnrollmentController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            selectViewBag();
            var enrollment = await _enrollmentRepository.GetByIdAsync(id);
            return View(enrollment);
        }

        // GET: EnrollmentController/Create
        public IActionResult Create()
        {
            selectViewBag();
            return View();
        }

        // POST: EnrollmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                await _enrollmentRepository.AddAsync(enrollment);
                return RedirectToAction(nameof(Index));
            }

            selectViewBag();
            return View(enrollment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateList(List<Enrollment> enrollments)
        {
            if (ModelState.IsValid)
            {
                foreach (var enrollment in enrollments)
                {
                    var existingEnrollment = (await _enrollmentRepository.GetAllAsync())
                        .FirstOrDefault(e => e.StudentID == enrollment.StudentID && e.CourseID == enrollment.CourseID);

                    if (existingEnrollment != null)
                    {
                        existingEnrollment.Grade = enrollment.Grade;
                        await _enrollmentRepository.UpdateAsync(existingEnrollment);
                    }
                    else
                    {
                        await _enrollmentRepository.AddAsync(enrollment);
                    }
                }
                TempData["SuccessMessage"] = "Enrollments added successfully!";
                return RedirectToAction("Instructor_Screen", "Home");
            }

            selectViewBag();
            return View(enrollments);
        }

        // GET: EnrollmentController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            selectViewBag();
            var enrollment = await _enrollmentRepository.GetByIdAsync(id);
            return View(enrollment);
        }

        // POST: EnrollmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                await _enrollmentRepository.UpdateAsync(enrollment);
                return RedirectToAction(nameof(Index));
            }
            return View(enrollment);
        }

        // GET: EnrollmentController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            selectViewBag();
            if (id is 0 or null)
            {
                return NotFound();
            }
            var enrollment = await _enrollmentRepository.GetByIdAsync(id.Value);
            return View(enrollment);
        }

        // POST: EnrollmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Enrollment enrollment)
        {
            if (enrollment != null)
            {
                await _enrollmentRepository.DeleteAsync(enrollment);
                return RedirectToAction(nameof(Index));
            }
            return View(enrollment);
        }

        public void selectViewBag()
        {
            ViewBag.Student = _studentRepository.GetAllAsync().Result.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name
            }).ToList();

            ViewBag.Course = _courseRepository.GetAllAsync().Result.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Title
            }).ToList();
        }
    }
}
