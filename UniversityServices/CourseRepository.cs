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
        public void Add(Course entity)
        {
          _context.Courses.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Course entity)
        {
            _context.Courses.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Course> GetAll()

        {  var courses = _context.Courses.Include(c => c.Department).ToList();
            return _context.Courses.ToList();
        }

        public Course GetById(int id)
        {
         return _context.Courses.SingleOrDefault( c => c.Id == id)!;
        }

        public IEnumerable<Course> Search(string keyword)
        {
            return _context.Courses.Where(s => s.Title.ToUpper().Contains(keyword.ToUpper()));

        }

        public void Update(Course entity)
        {
          _context.Courses.Update(entity);
            _context.SaveChanges();
        }
    }
}
