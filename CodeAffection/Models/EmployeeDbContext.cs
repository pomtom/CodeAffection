using Microsoft.EntityFrameworkCore;

namespace CodeAffection.Models
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> context) : base(context)
        {

        }

        public DbSet<Employee> Employees { get; set; }
    }
}
