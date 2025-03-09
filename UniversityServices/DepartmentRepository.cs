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

        public void Add(Department entity)
        {
            _context.Departments.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Department entity)
        {
            _context.Departments.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Department> GetAll()
        {
            return _context.Departments.Include(d => d.Administrator).ToList();
        }

        public Department GetById(int id)
        {
            return _context.Departments.FirstOrDefault(d => d.Id == id)!;
        }

        public IEnumerable<Department> Search(string keyword)
        {
            return _context.Departments
                .Where(d => d.Name.Contains(keyword))
                .Include(d => d.Administrator)
                .ToList();
        }

        public void Update(Department entity)
        {
            _context.Departments.Update(entity);
            _context.SaveChanges();
        }
    }
}
