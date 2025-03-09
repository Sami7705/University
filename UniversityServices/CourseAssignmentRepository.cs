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
    public class CourseAssignmentRepository : IRepository<CourseAssignment>
    {
        private readonly AppDbContext _context;

        public CourseAssignmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<CourseAssignment> GetAll()
        {
            return _context.CourseAssignments.Include(i => i.Instructor).Include(c => c.Course).ToList();
        }

        public CourseAssignment GetById(int id)
        {
            return _context.CourseAssignments
                           .Include(i => i.Instructor)
                           .Include(c => c.Course)
                           .FirstOrDefault(c => c.CourseID == id)!;
        }

        public void Add(CourseAssignment entity)
        {
            _context.CourseAssignments.Add(entity);
            _context.SaveChanges();
        }

        public void Update(CourseAssignment entity)
        {
            _context.CourseAssignments.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(CourseAssignment entity)
        {
            _context.CourseAssignments.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<CourseAssignment> Search(string keyword)
        {
            return _context.CourseAssignments
                .Where(ca => (ca.Course != null && ca.Course.Title.Contains(keyword)) ||
                             (ca.Instructor != null && ca.Instructor.Name.Contains(keyword)))
                .ToList();
        }
    }
}
