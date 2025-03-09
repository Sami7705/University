using UniversityModel.Models;

namespace UniversityModel.Data
{
   
        public static class DbInitializer
        {
            public static void Initialize(AppDbContext context)
            {
                //context.Database.EnsureCreated();
                // البحث عن وجود طلاب مسبقاً.
                if (context.Students.Any())
                {
                    return;   // تم تهيئة قاعدة البيانات مسبقاً
                }

                // إضافة الطلاب
                Student[] students = new Student[]
                {
                    new() { Name = "Sami", EnrollmentDate = DateTime.Parse("2010-09-01") },
                    new() { Name = "reem", EnrollmentDate = DateTime.Parse("2012-09-01") },
                    new() { Name = "Rashad", EnrollmentDate = DateTime.Parse("2013-09-01") },
                    new() { Name = "jeem", EnrollmentDate = DateTime.Parse("2012-09-01") },
                    new() { Name = "Yan", EnrollmentDate = DateTime.Parse("2012-09-01") },
                    new() { Name = "rna", EnrollmentDate = DateTime.Parse("2011-09-01") },
                    new() { Name = "jana", EnrollmentDate = DateTime.Parse("2013-09-01") },
                };

                foreach (Student s in students)
                {
                    _ = context.Students.Add(s);
                }
                _ = context.SaveChanges();

                // إضافة المدربين
                Instructor[] instructors = new Instructor[]
                {
                    new() { Name = "Sami",   LastName = "a", HireDate = DateTime.Parse("1995-03-11") },
                    new() { Name = "Rashad", LastName = "b", HireDate = DateTime.Parse("2002-07-06") },
                    new() { Name = "Ali",    LastName = "c", HireDate = DateTime.Parse("1998-07-01") },
                    new() { Name = "Majed",  LastName = "d", HireDate = DateTime.Parse("2001-01-15") },
                    new() { Name = "Reem",   LastName = "e", HireDate = DateTime.Parse("2004-02-12") }
                };

                foreach (Instructor i in instructors)
                {
                    _ = context.Instructors.Add(i);
                }
                _ = context.SaveChanges();

                // تعديل مصفوفة الأقسام لتتوافق مع الدورات
                Department[] departments = new Department[]
                {
                    new() { Name = "English", Budget = 350000,
                        StartDate = DateTime.Parse("2007-09-01"),
                        InstructorID = instructors.Single(i => i.LastName == "a").Id },
                    new() { Name = "Mathematics", Budget = 100000,
                        StartDate = DateTime.Parse("2007-09-01"),
                        InstructorID = instructors.Single(i => i.LastName == "b").Id },
                    new() { Name = "Engineering", Budget = 350000,
                        StartDate = DateTime.Parse("2007-09-01"),
                        InstructorID = instructors.Single(i => i.LastName == "c").Id },
                    new() { Name = "Economics", Budget = 100000,
                        StartDate = DateTime.Parse("2007-09-01"),
                        InstructorID = instructors.Single(i => i.LastName == "d").Id }
                };

                foreach (Department d in departments)
                {
                    _ = context.Departments.Add(d);
                }
                _ = context.SaveChanges();

                // إضافة الدورات وربطها بالأقسام الصحيحة
                Course[] courses = new Course[]
                {
                    new() { Id = 1050, Title = "Chemistry",
                        Credits = 3,
                        DepartmentID = departments.Single(s => s.Name == "Engineering").Id },
                    new() { Id = 4022, Title = "Microeconomics",
                        Credits = 3,
                        DepartmentID = departments.Single(s => s.Name == "Economics").Id },
                    new() { Id = 4041, Title = "Macroeconomics",
                        Credits = 3,
                        DepartmentID = departments.Single(s => s.Name == "Economics").Id },
                    new() { Id = 1045, Title = "Calculus",
                        Credits = 4,
                        DepartmentID = departments.Single(s => s.Name == "Mathematics").Id },
                    new() { Id = 3141, Title = "Trigonometry",
                        Credits = 4,
                        DepartmentID = departments.Single(s => s.Name == "Mathematics").Id },
                    new() { Id = 2021, Title = "Composition",
                        Credits = 3,
                        DepartmentID = departments.Single(s => s.Name == "English").Id },
                    new() { Id = 2042, Title = "Literature",
                        Credits = 4,
                        DepartmentID = departments.Single(s => s.Name == "English").Id },
                };

                foreach (Course c in courses)
                {
                    _ = context.Courses.Add(c);
                }
                _ = context.SaveChanges();

                // إضافة تعيينات المكاتب للمدربين
                OfficeAssignment[] officeAssignments = new OfficeAssignment[]
                {
                    new() {
                        InstructorID = instructors.Single(i => i.LastName == "a").Id,
                        Location = "Smith 17"
                    },
                    new() {
                        InstructorID = instructors.Single(i => i.LastName == "b").Id,
                        Location = "Gowan 27"
                    },
                    new() {
                        InstructorID = instructors.Single(i => i.LastName == "c").Id,
                        Location = "Thompson 304"
                    },
                };

                foreach (OfficeAssignment o in officeAssignments)
                {
                    _ = context.OfficeAssignments.Add(o);
                }
                _ = context.SaveChanges();

                // إضافة تعيينات الدورات للمدربين مع التأكد من استخدام أسماء موجودة
                CourseAssignment[] courseInstructors = new CourseAssignment[]
                {
                    new() {
                        CourseID = courses.Single(c => c.Title == "Chemistry").Id,
                        InstructorID = instructors.Single(i => i.LastName == "a").Id
                    },
                    new() {
                        CourseID = courses.Single(c => c.Title == "Microeconomics").Id,
                        InstructorID = instructors.Single(i => i.LastName == "b").Id
                    },
                    new() {
                        CourseID = courses.Single(c => c.Title == "Microeconomics").Id,
                        InstructorID = instructors.Single(i => i.LastName == "c").Id
                    },
                    new() {
                        CourseID = courses.Single(c => c.Title == "Macroeconomics").Id,
                        InstructorID = instructors.Single(i => i.LastName == "d").Id
                    },
                    new() {
                        CourseID = courses.Single(c => c.Title == "Calculus").Id,
                        InstructorID = instructors.Single(i => i.LastName == "e").Id
                    },
                    new() {
                        CourseID = courses.Single(c => c.Title == "Trigonometry").Id,
                        // استبدلنا "Harui" بـ "c" وهو متوفر في مصفوفة Instructors
                        InstructorID = instructors.Single(i => i.LastName == "c").Id
                    },
                    new() {
                        CourseID = courses.Single(c => c.Title == "Composition").Id,
                        InstructorID = instructors.Single(i => i.LastName == "a").Id
                    },
                };

                foreach (CourseAssignment ci in courseInstructors)
                {
                    _ = context.CourseAssignments.Add(ci);
                }
                _ = context.SaveChanges();

                // إضافة التسجيلات (Enrollements)
                Enrollment[] enrollments = new Enrollment[]
                {
                    new() {
                        StudentID = students.Single(s => s.Name == "Sami").Id,
                        CourseID = courses.Single(c => c.Title == "Chemistry").Id,
                        Grade = Grade.A
                    },
                    new() {
                        StudentID = students.Single(s => s.Name == "Rashad").Id,
                        CourseID = courses.Single(c => c.Title == "Microeconomics").Id,
                        Grade = Grade.C
                    },
                    new() {
                        StudentID = students.Single(s => s.Name == "reem").Id,
                        CourseID = courses.Single(c => c.Title == "Macroeconomics").Id,
                        Grade = Grade.B
                    },
                    new() {
                        StudentID = students.Single(s => s.Name == "Yan").Id,
                        CourseID = courses.Single(c => c.Title == "Calculus").Id,
                        Grade = Grade.B
                    },
                    new() {
                        StudentID = students.Single(s => s.Name == "jana").Id,
                        CourseID = courses.Single(c => c.Title == "Trigonometry").Id,
                        Grade = Grade.B
                    },
                };

                foreach (Enrollment e in enrollments)
                {
                    Enrollment? enrollmentInDataBase = context.Enrollments
                        .Where(s => s.Student.Id == e.StudentID && s.Course.Id == e.CourseID)
                        .SingleOrDefault();
                    if (enrollmentInDataBase == null)
                    {
                        _ = context.Enrollments.Add(e);
                    }
                }
                _ = context.SaveChanges();
            }
        
        }
}
