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

        public async Task<IEnumerable<Instructor>> GetAllAsync()
        {
            return await _context.Instructors
                .Include(e => e.OfficeAssignment)
                .Include(e => e.CourseAssignments)
                .ToListAsync();
        }

        public async Task<Instructor> GetByIdAsync(int id)
        {
            return await _context.Instructors.FindAsync(id);
        }

        public async Task AddAsync(Instructor entity)
        {
            await _context.Instructors.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Instructor entity)
        {
            var existingInstructor = await _context.Instructors
                .Include(i => i.OfficeAssignment)
                .FirstOrDefaultAsync(i => i.Id == entity.Id);

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

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Instructor entity)
        {
            _context.Instructors.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Instructor>> SearchAsync(string keyword)
        {
            return await _context.Instructors
                .Where(i => i.Name.Contains(keyword) || i.LastName.Contains(keyword))
                .ToListAsync();
        }
    }
}
