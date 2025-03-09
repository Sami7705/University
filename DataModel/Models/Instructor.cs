using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityModel.Models
{
    public class Instructor
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }=string.Empty;
        public string LastName {  get; set; }=string.Empty;
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name ="Hire Date")]
        public DateTime HireDate { get; set; }
        public ICollection<CourseAssignment>? CourseAssignments { get; set; }
  
   
        public OfficeAssignment? OfficeAssignment { get; set; }

    }
}
