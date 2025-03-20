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
    public class DepartmentRepository : IRepository<Department>
    {
        private readonly AppDbContext _context;
        public DepartmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Department entity)
        {
            await _context.Departments.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Department entity)
        {
            _context.Departments.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _context.Departments.Include(d => d.Administrator).ToListAsync();
        }

        public async Task<Department> GetByIdAsync(int id)
        {
            return await _context.Departments.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<Department>> SearchAsync(string keyword)
        {
            return await _context.Departments
                .Where(d => d.Name.Contains(keyword))
                .Include(d => d.Administrator)
                .ToListAsync();
        }

        public async Task UpdateAsync(Department entity)
        {
            _context.Departments.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}

