using EmployeeManager.DBContext;
using EmployeeManager.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.Repositories
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly ProDbContext _proDbContext;

        public ManagerRepository(ProDbContext proDbContext)
        {
            _proDbContext = proDbContext;
        }
        public async Task<Manager> CreateManagerAsync(Manager manager)
        {
            await _proDbContext.Managers.AddAsync(manager);
            await _proDbContext.SaveChangesAsync();
            return manager;
        }

        public async Task<Manager?> DeleteManagerAsync(int id)
        {
            var man = await _proDbContext.Managers.FirstOrDefaultAsync(manager => manager.Id == id);
            if (man != null)
            {
                _proDbContext.Managers.Remove(man);
                await _proDbContext.SaveChangesAsync();
                return man;
            }
            return null;
        }

        public async Task<List<Manager>> GetAllManagersAsync()
        {
            return await _proDbContext.Managers.Include(x=>x.Employees).ToListAsync();
        }

        public async Task<Manager?> GetManagerByIdAsync(int id)
        {
            var man = await _proDbContext.Managers.Include("Employees").FirstOrDefaultAsync(x=>x.Id==id);
            if (man == null)
            {
                return null;
            }
            return man;
        }

        public async Task<Manager?> UpdateManagerAsync(int id, Manager manager)
        {
            var man = await _proDbContext.Managers.FindAsync(id);
            if(man == null)
            {
                return null;
            }

            man.Name = manager.Name;
            man.Location = manager.Location;

            await _proDbContext.SaveChangesAsync();
            return man;
        }
    }
}
