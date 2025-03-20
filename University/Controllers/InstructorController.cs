using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityDataAccess.Interface;
using UniversityModel.Models;

namespace University.Controllers
{
    [Authorize(Roles = "Admin")]//cookie -Role :Admin
    public class InstructorController : Controller
    {
        private readonly IRepository<Instructor> _instructorRepository;
        private readonly IRepository<OfficeAssignment> _officeAssignmentRepository;

        public InstructorController(IRepository<Instructor> instructorRepository, IRepository<OfficeAssignment> officeAssignmentRepository)
        {
            _instructorRepository = instructorRepository;
            _officeAssignmentRepository = officeAssignmentRepository;
        }

        public async Task<IActionResult> Index()
        {
            var instructors = await _instructorRepository.GetAllAsync();
            return View(instructors);
        }

        // GET: InstructorController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var instructor = await _instructorRepository.GetByIdAsync(id);
            if (instructor == null)
            {
                return NotFound(); // أو يمكنك إعادة توجيه المستخدم إلى صفحة خطأ مخصصة
            }
            return View(instructor);
        }

        // GET: InstructorController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.OfficeAssignments = await _officeAssignmentRepository.GetAllAsync();
            return View();
        }

        // POST: InstructorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                if (instructor.clientFile != null)
                {
                    MemoryStream stream = new MemoryStream();
                    instructor.clientFile.CopyTo(stream);
                    instructor.dbImage = stream.ToArray();
                }
                await _instructorRepository.AddAsync(instructor);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.OfficeAssignments = await _officeAssignmentRepository.GetAllAsync();
            return View(instructor);
        }

        // GET: InstructorController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var instructor = await _instructorRepository.GetByIdAsync(id);
            ViewBag.OfficeAssignments = await _officeAssignmentRepository.GetAllAsync();
            return View(instructor);
        }
        // POST: InstructorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                var existingInstructor = await _instructorRepository.GetByIdAsync(instructor.Id);
                if (existingInstructor == null)
                {
                    return NotFound();
                }

                // Update instructor details
                existingInstructor.Name = instructor.Name;
                existingInstructor.LastName = instructor.LastName;
                existingInstructor.HireDate = instructor.HireDate;
                existingInstructor.Email = instructor.Email;
                existingInstructor.Password = instructor.Password;
                existingInstructor.OfficeAssignment.Location = instructor.OfficeAssignment.Location;

                // Update image if a new file is uploaded
                if (instructor.clientFile != null)
                {
                    using (var stream = new MemoryStream())
                    {
                        await instructor.clientFile.CopyToAsync(stream);
                        existingInstructor.dbImage = stream.ToArray();
                    }
                }

                await _instructorRepository.UpdateAsync(existingInstructor);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.OfficeAssignments = await _officeAssignmentRepository.GetAllAsync();
            return View(instructor);
        }


        // GET: InstructorController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is 0 or null)
            {
                return NotFound();
            }
            var instructor = await _instructorRepository.GetByIdAsync(id.Value);
            return View(instructor);
        }

        // POST: InstructorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Instructor instructor)
        {
            if (instructor != null)
            {
                await _instructorRepository.DeleteAsync(instructor);
                return RedirectToAction(nameof(Index));
            }
            return View(instructor);
        }
    }
}