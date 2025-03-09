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
        public void Add(Enrollment entity)
        {
            _context.Enrollments.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Enrollment entity)
        {
            _context.Enrollments.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Enrollment> GetAll()
        {
            var enrollments = _context.Enrollments.Include(e => e.Course).Include(e => e.Student).ToList();
            return _context.Enrollments.ToList();
        }

        public Enrollment GetById(int id)
        {
            return _context.Enrollments.FirstOrDefault(e => e.Id == id)!;
        }

        public IEnumerable<Enrollment> Search(string keyword)
        {
            throw new NotImplementedException();
        }

        public void Update(Enrollment entity)
        {
            _context.Enrollments.Update(entity);
            _context.SaveChanges();
        }
        
    }
}
