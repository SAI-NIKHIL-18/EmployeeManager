using EmployeeManager.DBContext;
using EmployeeManager.Models.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ProDbContext _proDbContext;

        public EmployeeRepository(ProDbContext proDbContext)
        {
            _proDbContext = proDbContext;
        }
        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            await _proDbContext.Employees.AddAsync(employee);
            await _proDbContext.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee?> DeleteEmployeeAsync(int id)
        {
            var emp = await _proDbContext.Employees.FindAsync(id);
            if(emp == null)
            {
                return null;
            }
            _proDbContext.Employees.Remove(emp);
            await _proDbContext.SaveChangesAsync();
            return emp;
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            return await _proDbContext.Employees.ToListAsync();
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            if(!string.IsNullOrEmpty(id.ToString())) { return null; }
            return (await _proDbContext.Employees.FindAsync(id));
        }

        public async Task<Employee?> UpdateEmployeeAsync(int id, Employee employee)
        {
            var emp = await _proDbContext.Employees.FindAsync(id);
            if(emp == null)
            {
                return null;
            }
            emp.Name = employee.Name;
            emp.Email = employee.Email;
            emp.Dob = employee.Dob;
            emp.Salary = employee.Salary;
            emp.Location = employee.Location;
            emp.ManagerId = employee.ManagerId;
            await _proDbContext.SaveChangesAsync();
            return emp;
        }
    }
}
