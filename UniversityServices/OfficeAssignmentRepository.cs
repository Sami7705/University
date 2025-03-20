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
    public class OfficeAssignmentRepository : IRepository<OfficeAssignment>
    {
        private readonly AppDbContext _context;

        public OfficeAssignmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OfficeAssignment>> GetAllAsync()
        {
            return await _context.OfficeAssignments.Include(I => I.Instructor).ToListAsync();
        }

        public async Task<OfficeAssignment> GetByIdAsync(int id)
        {
            return await _context.OfficeAssignments.FindAsync(id);
        }

        public async Task AddAsync(OfficeAssignment entity)
        {
            await _context.OfficeAssignments.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(OfficeAssignment entity)
        {
            _context.OfficeAssignments.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(OfficeAssignment entity)
        {
            _context.OfficeAssignments.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<OfficeAssignment>> SearchAsync(string keyword)
        {
            return await _context.OfficeAssignments
                .Where(o => o.Location.Contains(keyword))
                .ToListAsync();
        }
    }
}
