using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityModel.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }=string.Empty;
        [Range(0 ,5)]
        public int Credits { get; set; }
        [ForeignKey("Department")]
        public int DepartmentID { get; set; }
        public Department? Department { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }
        public ICollection<CourseAssignment>? CourseAssignments { get; set; }

    }
}
