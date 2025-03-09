using Microsoft.EntityFrameworkCore;
using UniversityModel.Models;
using System;

namespace UniversityModel.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        public DbSet<CourseAssignment> CourseAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // تحويل أسماء الجداول
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<Instructor>().ToTable("Instructor");
            modelBuilder.Entity<OfficeAssignment>().ToTable("OfficeAssignment");
            modelBuilder.Entity<CourseAssignment>().ToTable("CourseAssignment");

            // المفتاح المركب لجدول CourseAssignment
            modelBuilder.Entity<CourseAssignment>()
                .HasKey(c => new { c.CourseID, c.InstructorID });

            // بيانات افتراضية لجدول Student
            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, Name = "أحمد محمد", EnrollmentDate = new DateTime(2022, 9, 1) },
                new Student { Id = 2, Name = "سارة علي", EnrollmentDate = new DateTime(2022, 9, 1) },
                new Student { Id = 3, Name = "محمود إبراهيم", EnrollmentDate = new DateTime(2022, 9, 1) }
            );

            // بيانات افتراضية لجدول Instructor
            modelBuilder.Entity<Instructor>().HasData(
                new Instructor { Id = 1, Name = "خالد", LastName = "الزيد", HireDate = new DateTime(2020, 1, 15) },
                new Instructor { Id = 2, Name = "فاطمة", LastName = "العتيبي", HireDate = new DateTime(2019, 3, 10) },
                new Instructor { Id = 3, Name = "سامي", LastName = "الدبعي", HireDate = new DateTime(2018, 6, 20) }
            );

            // بيانات افتراضية لجدول OfficeAssignment
            modelBuilder.Entity<OfficeAssignment>().HasData(
                new OfficeAssignment { InstructorID = 1, Location = "مكتب 101 - صنعاء" },
                new OfficeAssignment { InstructorID = 2, Location = "مكتب 202 - حدة" }
            );

            // بيانات افتراضية لجدول Department
            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "قسم تكنولوجيا المعلومات", Budget = 1000000m, StartDate = new DateTime(2015, 8, 15), InstructorID = 1 },
                new Department { Id = 2, Name = "قسم علوم الحاسوب", Budget = 1500000m, StartDate = new DateTime(2010, 9, 1), InstructorID = 2 }
            );

            // بيانات افتراضية لجدول Course
            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, Title = "برمجة C#", Credits = 3, DepartmentID = 1 },
                new Course { Id = 2, Title = "شبكات الحاسوب", Credits = 4, DepartmentID = 1 },
                new Course { Id = 3, Title = "تطوير الويب", Credits = 3, DepartmentID = 2 }
            );

            // بيانات افتراضية لجدول Enrollment
            modelBuilder.Entity<Enrollment>().HasData(
                new Enrollment { Id = 1, CourseID = 1, StudentID = 1, Grade = Grade.A },
                new Enrollment { Id = 2, CourseID = 2, StudentID = 2, Grade = Grade.B },
                new Enrollment { Id = 3, CourseID = 3, StudentID = 3, Grade = Grade.C }
            );

            // بيانات افتراضية لجدول CourseAssignment
            modelBuilder.Entity<CourseAssignment>().HasData(
                new CourseAssignment { CourseID = 1, InstructorID = 1 },
                new CourseAssignment { CourseID = 2, InstructorID = 1 },
                new CourseAssignment { CourseID = 3, InstructorID = 2 },
                new CourseAssignment { CourseID = 1, InstructorID = 3 }
            );
        }
    }
}
