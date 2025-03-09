using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityDataAccess.Interface;
using UniversityModel.Data;
using UniversityModel.Models;

namespace UniversityDataAccess
{
    public class InstructorRepository : IRepository<Instructor>
    {
        private readonly AppDbContext _context;

        public InstructorRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Instructor> GetAll()
        {
            return _context.Instructors.Include(e => e.OfficeAssignment).Include(e => e.CourseAssignments).ToList();
        }

        public Instructor GetById(int id)
        {
            return _context.Instructors.Find(id)!;
        }

        public void Add(Instructor entity)
        {
            _context.Instructors.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Instructor entity)
        {
            var existingInstructor = _context.Instructors
                .Include(i => i.OfficeAssignment)
                .FirstOrDefault(i => i.Id == entity.Id);

            if (existingInstructor != null)
            {
                _context.Entry(existingInstructor).CurrentValues.SetValues(entity);

                if (entity.OfficeAssignment != null)
                {
                    if (existingInstructor.OfficeAssignment == null)
                    {
                        existingInstructor.OfficeAssignment = entity.OfficeAssignment;
                    }
                    else
                    {
                        _context.Entry(existingInstructor.OfficeAssignment).CurrentValues.SetValues(entity.OfficeAssignment);
                    }
                }

                _context.SaveChanges();
            }
        }

        public void Delete(Instructor entity)
        {
            _context.Instructors.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Instructor> Search(string keyword)
        {
            return _context.Instructors
                .Where(i => i.Name.Contains(keyword) || i.LastName.Contains(keyword))
                .ToList();
        }
    }
}
