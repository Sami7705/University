using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using UniversityDataAccess;
using UniversityDataAccess.Interface;
using UniversityModel.Data;
using UniversityModel.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register the DbContext before building the app
builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(
builder.Configuration.GetConnectionString("ConnectionDB")));
//builder.Services.AddScoped<typeof(IRepository<>), StudentRepository>();
//builder.Services.AddTransient(typeof(IRepository<>),StudentRepository<>);
//builder.Services.AddTransient(typeof(IRepository<>), typeof(StudentRepository));
builder.Services.AddScoped<IRepository<Student>, StudentRepository>();
builder.Services.AddScoped<IRepository<Enrollment>, EnrollmentRepository>();
builder.Services.AddScoped<IRepository<Course>, CourseRepository>();
builder.Services.AddScoped<IRepository<Department>, DepartmentRepository>();
builder.Services.AddScoped<IRepository<Instructor>, InstructorRepository>();
builder.Services.AddScoped<IRepository<OfficeAssignment>, OfficeAssignmentRepository>();
builder.Services.AddScoped<IRepository<CourseAssignment>, CourseAssignmentRepository>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
    DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
