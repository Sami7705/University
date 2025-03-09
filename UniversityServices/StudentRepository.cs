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
        public void Add(Student entity)
        {
            _context.Students.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Student entity)
        {
          _context.Students.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Student> GetAll()
        {
            return _context.Students.ToList(); 
        }
        

        public Student GetById(int id)
        {
           return  _context.Students.FirstOrDefault(e => e.Id == id)!;
        }

       

        public IEnumerable<Student> Search(string keyword)
        {
            return _context.Students.Where(s => s.Name.ToUpper().Contains(keyword.ToUpper()));

        }

        public void Update(Student entity)
        {
            _context.Students.Update(entity);
            _context.SaveChanges();
        }
    }
}
