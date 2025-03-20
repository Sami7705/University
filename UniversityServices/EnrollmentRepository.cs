using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UniversityDataAccess.Interface;
using UniversityModel.Data;
using UniversityModel.Models;

namespace UniversityDataAccess
{
    public class EnrollmentRepository : IRepository<Enrollment>
    {
        private readonly AppDbContext _context;
        public EnrollmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Enrollment entity)
        {
            await _context.Enrollments.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Enrollment entity)
        {
            _context.Enrollments.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Enrollment>> GetAllAsync()
        {
            return await _context.Enrollments
                .Include(e => e.Course)
                .Include(e => e.Student)
                .ToListAsync();
        }

        public async Task<Enrollment> GetByIdAsync(int id)
        {
            return await _context.Enrollments
                .Include(e => e.Course)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Enrollment>> SearchAsync(string keyword)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Enrollment entity)
        {
            _context.Enrollments.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
