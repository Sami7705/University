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
    public class StudentRepository : IRepository<Student>
    {
        private readonly AppDbContext _context;
        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Student entity)
        {
            await _context.Students.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Student entity)
        {
            _context.Students.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            return await _context.Students.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Student>> SearchAsync(string keyword)
        {
            return await _context.Students
                .Where(s => s.Name.ToUpper().Contains(keyword.ToUpper()))
                .ToListAsync();
        }

        public async Task UpdateAsync(Student entity)
        {
            _context.Students.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
