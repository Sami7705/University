using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityModel.Models
{
    public class OfficeAssignment
    {
        [Key, ForeignKey("Instructor")]
        public int InstructorID { get; set; }

        [Display(Name = "Office Location")]
        public string Location { get; set; } = string.Empty;

        public Instructor? Instructor { get; set; }
    }
}
