using Microsoft.AspNetCore.Mvc;
using UniversityDataAccess.Interface;
using UniversityModel.Models;

namespace University.Controllers
{
    public class InstructorController : Controller
    {
        private readonly IRepository<Instructor> _instructorRepository;
        private readonly IRepository<OfficeAssignment> _officeAssignmentRepository;

        public InstructorController(IRepository<Instructor> instructorRepository, IRepository<OfficeAssignment> officeAssignmentRepository)
        {
            _instructorRepository = instructorRepository;
            _officeAssignmentRepository = officeAssignmentRepository;
        }
        public IActionResult Index()
        {
            return View(_instructorRepository.GetAll());
        }

        // GET: InstructorController/Details/5
        public IActionResult Details(int id)
        {
            var instructor = _instructorRepository.GetById(id);
            if (instructor == null)
            {
                return NotFound(); // أو يمكنك إعادة توجيه المستخدم إلى صفحة خطأ مخصصة
            }
            return View(instructor);
        }


        // GET: InstructorController/Create
        public IActionResult Create()
        {
            ViewBag.OfficeAssignments = _officeAssignmentRepository.GetAll();
            return View();
        }

        // POST: InstructorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                if (instructor.clientFile != null)
                {
                    MemoryStream stream = new MemoryStream();
                    instructor.clientFile.CopyTo(stream);
                    instructor.dbImage = stream.ToArray();
                }
                _instructorRepository.Add(instructor);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.OfficeAssignments = _officeAssignmentRepository.GetAll();
            return View(instructor);
        }

        // GET: InstructorController/Edit/5
        public IActionResult Edit(int id)
        {
            var instructor = _instructorRepository.GetById(id);
            ViewBag.OfficeAssignments = _officeAssignmentRepository.GetAll();
            return View(instructor);
        }

        // POST: InstructorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                _instructorRepository.Update(instructor);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.OfficeAssignments = _officeAssignmentRepository.GetAll();
            return View(instructor);
        }

        // GET: InstructorController/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id is 0 or null)
            {
                return NotFound();
            }
            var instructor = _instructorRepository.GetById(id.Value);
            return View(instructor);
        }

        // POST: InstructorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Instructor instructor)
        {
            if (instructor != null)
            {
                _instructorRepository.Delete(instructor);
                return RedirectToAction(nameof(Index));
            }
            return View(instructor);
        }
        //GET: InstructorController/GetOfficeAssignments
        //public void selectViewBag()
        //{
        //    ViewBag.OfficeAssignments = _officeAssignmentRepository.GetAll();
        //}
    }
}