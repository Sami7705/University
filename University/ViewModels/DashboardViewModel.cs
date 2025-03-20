using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityDataAccess.Interface;
using UniversityModel.Models;
namespace University.ViewModels
{
    public class DashboardViewModel
    {
        public int StudentsCount { get; set; }
        public int InstructorsCount { get; set; }
        public int CoursesCount { get; set; }
        public int OfficesCount { get; set; }  // عدد المكاتب
        public int EnrollmentsCount { get; set; }  // عدد التسجيلات
        public int CourseAssignmentsCount { get; set; } // عدد CourseAssignment
        public int VisitorsCount { get; set; }
        public int SubscribersCount { get; set; }
        public int MessagesCount { get; set; }
    }


}