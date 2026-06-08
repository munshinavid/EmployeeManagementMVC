using EmployeeManagementMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementMVC.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed default admin accounts
            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    AdminId = 1,
                    Username = "admin",
                    Password = "admin123",
                    Email = "admin@company.com"
                },
                new Admin
                {
                    AdminId = 2,
                    Username = "manager",
                    Password = "manager123",
                    Email = "manager@company.com"
                }
            );

            // Seed sample employee records
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    FirstName = "Navid",
                    LastName = "Munshi",
                    Position = "Software Engineer",
                    Salary = 75000.00m
                },
                new Employee
                {
                    Id = 2,
                    FirstName = "Rafiq",
                    LastName = "Ahmed",
                    Position = "Project Manager",
                    Salary = 95000.00m
                },
                new Employee
                {
                    Id = 3,
                    FirstName = "Fatima",
                    LastName = "Rahman",
                    Position = "UI/UX Designer",
                    Salary = 65000.00m
                },
                new Employee
                {
                    Id = 4,
                    FirstName = "Kamal",
                    LastName = "Hossain",
                    Position = "Database Administrator",
                    Salary = 80000.00m
                },
                new Employee
                {
                    Id = 5,
                    FirstName = "Nusrat",
                    LastName = "Jahan",
                    Position = "QA Engineer",
                    Salary = 60000.00m
                }
            );
        }
    }
}
