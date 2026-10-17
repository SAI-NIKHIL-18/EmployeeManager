using EmployeeManager.Models.Domain;

namespace EmployeeManager.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Employee> CreateEmployeeAsync(Employee employee);
        Task<List<Employee>> GetAllEmployeesAsync();
        Task<Employee?> GetEmployeeByIdAsync(int id);
        Task<Employee?> UpdateEmployeeAsync(int id, Employee employee);
        Task<Employee?> DeleteEmployeeAsync(int id);
    }
}
