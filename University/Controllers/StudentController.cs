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
       
        public IActionResult Index()
        {
          string n=   User.Identity.Name;
            return View(_studentRepository.GetAll());
        }
        public IActionResult New()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(Student student)
        {
            if (ModelState.IsValid)
            {
                if(student.clientFile != null)
                {
                    MemoryStream stream = new MemoryStream();
                    student.clientFile.CopyTo(stream);
                    student.dbImage = stream.ToArray();
                }
                _studentRepository.Add(student);
                TempData["successData"] = "Stuedent has been added successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(student);
            }
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is 0 or null)
            {
                _ = NotFound();
            }
            Student? student = _studentRepository.GetById(id.Value);
            if (student == null)
            {
                _ = NotFound();
            }
            return View(student);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Student student)
        {

            if (ModelState.IsValid)
            {
                if (student.clientFile != null)
                {
                    MemoryStream stream = new MemoryStream();
                    student.clientFile.CopyTo(stream);
                    student.dbImage = stream.ToArray();
                }
                _studentRepository.Update(student);
                return RedirectToAction("Index");
            }
            return View(student);
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id is null or 0)
            {
                _ = NotFound();
            }
            Student? student = _studentRepository.GetById(id.Value);
            if (student == null)
            {
                _ = NotFound();
            }

            return View(student);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteStudent(int? id)
        {
            if (id is null or 0)
            {
                _ = NotFound();
            }
           var s = _studentRepository.GetById(id.Value);
            if (s != null)
            {
                _studentRepository.Delete(s);
                return RedirectToAction("Index");
            }
            return View(s);


        }
    }
}
