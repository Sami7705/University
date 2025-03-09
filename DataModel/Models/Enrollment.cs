using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityModel.Models
{
    public enum Grade
    {
        A,B,C,D,E,F
    }
    public class Enrollment
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Course")]
        public int CourseID { get; set; }
        [ForeignKey("Student")]
        public int StudentID { get; set; }
        [DisplayFormat(NullDisplayText ="No Grade")]
        public Grade? Grade { get; set; }
        public Course? Course { get; set; }
        public Student? Student { get; set; }
    }
}