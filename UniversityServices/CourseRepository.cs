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
    public class CourseRepository : IRepository<Course>
    {
        private readonly AppDbContext _context;

        public CourseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Course entity)
        {
            await _context.Courses.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Course entity)
        {
            _context.Courses.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _context.Courses.Include(c => c.Department).ToListAsync();
        }

        public async Task<Course> GetByIdAsync(int id)
        {
            return await _context.Courses.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Course>> SearchAsync(string keyword)
        {
            return await _context.Courses
                .Where(s => s.Title.ToUpper().Contains(keyword.ToUpper()))
                .ToListAsync();
        }

        public async Task UpdateAsync(Course entity)
        {
            _context.Courses.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
