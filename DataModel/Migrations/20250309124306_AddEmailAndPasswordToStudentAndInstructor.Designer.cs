﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UniversityModel.Data;

#nullable disable

namespace UniversityModel.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250309124306_AddEmailAndPasswordToStudentAndInstructor")]
    partial class AddEmailAndPasswordToStudentAndInstructor
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("UniversityModel.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Credits")
                        .HasColumnType("int");

                    b.Property<int>("DepartmentID")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentID");

                    b.ToTable("Course", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Credits = 3,
                            DepartmentID = 1,
                            Title = "برمجة C#"
                        },
                        new
                        {
                            Id = 2,
                            Credits = 4,
                            DepartmentID = 1,
                            Title = "شبكات الحاسوب"
                        },
                        new
                        {
                            Id = 3,
                            Credits = 3,
                            DepartmentID = 2,
                            Title = "تطوير الويب"
                        });
                });

            modelBuilder.Entity("UniversityModel.Models.CourseAssignment", b =>
                {
                    b.Property<int>("CourseID")
                        .HasColumnType("int");

                    b.Property<int>("InstructorID")
                        .HasColumnType("int");

                    b.HasKey("CourseID", "InstructorID");

                    b.HasIndex("InstructorID");

                    b.ToTable("CourseAssignment", (string)null);

                    b.HasData(
                        new
                        {
                            CourseID = 1,
                            InstructorID = 1
                        },
                        new
                        {
                            CourseID = 2,
                            InstructorID = 1
                        },
                        new
                        {
                            CourseID = 3,
                            InstructorID = 2
                        },
                        new
                        {
                            CourseID = 1,
                            InstructorID = 3
                        });
                });

            modelBuilder.Entity("UniversityModel.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Budget")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("InstructorID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("InstructorID");

                    b.ToTable("Department", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Budget = 1000000m,
                            InstructorID = 1,
                            Name = "قسم تكنولوجيا المعلومات",
                            StartDate = new DateTime(2015, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            Budget = 1500000m,
                            InstructorID = 2,
                            Name = "قسم علوم الحاسوب",
                            StartDate = new DateTime(2010, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("UniversityModel.Models.Enrollment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseID")
                        .HasColumnType("int");

                    b.Property<int?>("Grade")
                        .HasColumnType("int");

                    b.Property<int>("StudentID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseID");

                    b.HasIndex("StudentID");

                    b.ToTable("Enrollment", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CourseID = 1,
                            Grade = 0,
                            StudentID = 1
                        },
                        new
                        {
                            Id = 2,
                            CourseID = 2,
                            Grade = 1,
                            StudentID = 2
                        },
                        new
                        {
                            Id = 3,
                            CourseID = 3,
                            Grade = 2,
                            StudentID = 3
                        });
                });

            modelBuilder.Entity("UniversityModel.Models.Instructor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("HireDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Instructor", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "",
                            HireDate = new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastName = "الزيد",
                            Name = "خالد",
                            Password = ""
                        },
                        new
                        {
                            Id = 2,
                            Email = "",
                            HireDate = new DateTime(2019, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastName = "العتيبي",
                            Name = "فاطمة",
                            Password = ""
                        },
                        new
                        {
                            Id = 3,
                            Email = "",
                            HireDate = new DateTime(2018, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastName = "الدبعي",
                            Name = "سامي",
                            Password = ""
                        });
                });

            modelBuilder.Entity("UniversityModel.Models.OfficeAssignment", b =>
                {
                    b.Property<int>("InstructorID")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InstructorID");

                    b.ToTable("OfficeAssignment", (string)null);

                    b.HasData(
                        new
                        {
                            InstructorID = 1,
                            Location = "مكتب 101 - صنعاء"
                        },
                        new
                        {
                            InstructorID = 2,
                            Location = "مكتب 202 - حدة"
                        });
                });

            modelBuilder.Entity("UniversityModel.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EnrollmentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Student", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "",
                            EnrollmentDate = new DateTime(2022, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "أحمد محمد",
                            Password = ""
                        },
                        new
                        {
                            Id = 2,
                            Email = "",
                            EnrollmentDate = new DateTime(2022, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "سارة علي",
                            Password = ""
                        },
                        new
                        {
                            Id = 3,
                            Email = "",
                            EnrollmentDate = new DateTime(2022, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "محمود إبراهيم",
                            Password = ""
                        });
                });

            modelBuilder.Entity("UniversityModel.Models.Course", b =>
                {
                    b.HasOne("UniversityModel.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("UniversityModel.Models.CourseAssignment", b =>
                {
                    b.HasOne("UniversityModel.Models.Course", "Course")
                        .WithMany("CourseAssignments")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniversityModel.Models.Instructor", "Instructor")
                        .WithMany("CourseAssignments")
                        .HasForeignKey("InstructorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Instructor");
                });

            modelBuilder.Entity("UniversityModel.Models.Department", b =>
                {
                    b.HasOne("UniversityModel.Models.Instructor", "Administrator")
                        .WithMany()
                        .HasForeignKey("InstructorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Administrator");
                });

            modelBuilder.Entity("UniversityModel.Models.Enrollment", b =>
                {
                    b.HasOne("UniversityModel.Models.Course", "Course")
                        .WithMany("Enrollments")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniversityModel.Models.Student", "Student")
                        .WithMany("Enrollments")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("UniversityModel.Models.OfficeAssignment", b =>
                {
                    b.HasOne("UniversityModel.Models.Instructor", "Instructor")
                        .WithOne("OfficeAssignment")
                        .HasForeignKey("UniversityModel.Models.OfficeAssignment", "InstructorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Instructor");
                });

            modelBuilder.Entity("UniversityModel.Models.Course", b =>
                {
                    b.Navigation("CourseAssignments");

                    b.Navigation("Enrollments");
                });

            modelBuilder.Entity("UniversityModel.Models.Instructor", b =>
                {
                    b.Navigation("CourseAssignments");

                    b.Navigation("OfficeAssignment");
                });

            modelBuilder.Entity("UniversityModel.Models.Student", b =>
                {
                    b.Navigation("Enrollments");
                });
#pragma warning restore 612, 618
        }
    }
}
