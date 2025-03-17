using UniversityModel.Models;

namespace University.ViewModels
{
    public class InstructorScreenViewModel
    {
        public Instructor Instructor { get; set; }
        public IEnumerable<Course> Courses { get; set; }
    }

}
