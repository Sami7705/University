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

        public IEnumerable<OfficeAssignment> GetAll()
        {
            return _context.OfficeAssignments.Include(I => I.Instructor).ToList();
        }

        public OfficeAssignment GetById(int id)
        {
            return _context.OfficeAssignments.Find(id)!;
        }

        public void Add(OfficeAssignment entity)
        {
            _context.OfficeAssignments.Add(entity);
            _context.SaveChanges();
        }

        public void Update(OfficeAssignment entity)
        {
            _context.OfficeAssignments.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(OfficeAssignment entity)
        {
            _context.OfficeAssignments.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<OfficeAssignment> Search(string keyword)
        {
            return _context.OfficeAssignments
                .Where(o => o.Location.Contains(keyword))
                .ToList();
        }
    }
}
