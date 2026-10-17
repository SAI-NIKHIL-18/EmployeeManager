using EmployeeManager.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.DBContext
{
    public class ProDbContext : DbContext
    {
        public ProDbContext(DbContextOptions<ProDbContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Manager> Managers { get; set; }
    }
}
