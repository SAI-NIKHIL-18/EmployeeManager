using EmployeeManager.Models.Domain;

namespace EmployeeManager.Repositories
{
    public interface IManagerRepository
    {
        Task<Manager> CreateManagerAsync(Manager manager);
        Task<List<Manager>> GetAllManagersAsync();
        Task<Manager?> GetManagerByIdAsync(int id);
        Task<Manager?> UpdateManagerAsync(int id, Manager manager);
        Task<Manager?> DeleteManagerAsync(int id);
    }
}
