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

        public async Task<IEnumerable<CourseAssignment>> GetAllAsync()
        {
            return await _context.CourseAssignments
                .Include(i => i.Instructor)
                .Include(c => c.Course)
                .ToListAsync();
        }

        public async Task<CourseAssignment> GetByIdAsync(int id)
        {
            return await _context.CourseAssignments
                .Include(i => i.Instructor)
                .Include(c => c.Course)
                .FirstOrDefaultAsync(c => c.CourseID == id);
        }

        public async Task AddAsync(CourseAssignment entity)
        {
            await _context.CourseAssignments.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CourseAssignment entity)
        {
            _context.CourseAssignments.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(CourseAssignment entity)
        {
            _context.CourseAssignments.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CourseAssignment>> SearchAsync(string keyword)
        {
            return await _context.CourseAssignments
                .Where(ca => (ca.Course != null && ca.Course.Title.Contains(keyword)) ||
                             (ca.Instructor != null && ca.Instructor.Name.Contains(keyword)))
                .ToListAsync();
        }
    }
}
