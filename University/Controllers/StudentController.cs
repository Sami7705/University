using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityDataAccess;
using UniversityDataAccess.Interface;
using UniversityModel.Models;

namespace University.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly IRepository<Student> _studentRepository;

        public StudentController(IRepository<Student> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<IActionResult> Index()
        {
            string n = User.Identity.Name;
            var students = await _studentRepository.GetAllAsync();
            return View(students);
        }

        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(Student student)
        {
            if (ModelState.IsValid)
            {
                if (student.clientFile != null)
                {
                    MemoryStream stream = new MemoryStream();
                    student.clientFile.CopyTo(stream);
                    student.dbImage = stream.ToArray();
                }
                await _studentRepository.AddAsync(student);
                TempData["successData"] = "Student has been added successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(student);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is 0 or null)
            {
                return NotFound();
            }
            Student? student = await _studentRepository.GetByIdAsync(id.Value);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                if (student.clientFile != null)
                {
                    MemoryStream stream = new MemoryStream();
                    student.clientFile.CopyTo(stream);
                    student.dbImage = stream.ToArray();
                }
                await _studentRepository.UpdateAsync(student);
                return RedirectToAction("Index");
            }
            return View(student);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null or 0)
            {
                return NotFound();
            }
            Student? student = await _studentRepository.GetByIdAsync(id.Value);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteStudent(int? id)
        {
            if (id is null or 0)
            {
                return NotFound();
            }
            var student = await _studentRepository.GetByIdAsync(id.Value);
            if (student != null)
            {
                await _studentRepository.DeleteAsync(student);
                return RedirectToAction("Index");
            }
            return View(student);
        }
    }
}
