using EmployeeManager.Models.Domain;
using EmployeeManager.Models.DTO;
using EmployeeManager.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerRepository _managerRepository;

        public ManagerController(IManagerRepository managerRepository)
        {
            _managerRepository = managerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllManagers()
        {
            var managers = await _managerRepository.GetAllManagersAsync();
            var dtoManagers = new List<ManagerDto>();
            foreach (var manager in managers)
            {
                dtoManagers.Add(new ManagerDto {Id = manager.Id, Name = manager.Name, Location = manager.Location, Employees=manager.Employees });
            }
            return Ok(dtoManagers);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetManagerById(int id)
        {
            var man = await _managerRepository.GetManagerByIdAsync(id);
            if(man == null)
            {
                return NotFound();
            }
            return Ok(man);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UpdateManagerDto updateManagerDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var manager = new Manager
            {
                Name = updateManagerDto.Name,
                Location = updateManagerDto.Location
            };
            var domainManager = await _managerRepository.CreateManagerAsync(manager);
            var newDto = new ManagerDto
            {
                //Id = domainManager.Id,
                Name = domainManager.Name,
                Location = domainManager.Location
            };
            return CreatedAtAction(nameof(GetManagerById), new { id = domainManager.Id }, newDto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateManager(int id, [FromBody] UpdateManagerDto updateManagerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var domainManager = new Manager
            {
                Name = updateManagerDto.Name,
                Location = updateManagerDto.Location
            };

            await _managerRepository.UpdateManagerAsync(id, domainManager);

            var dtoManager = new UpdateManagerDto
            {
                Name = domainManager.Name,
                Location = domainManager.Location,
            };
            return Ok(dtoManager);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteManager(int id)
        {
            var man = await _managerRepository.GetManagerByIdAsync(id);
            if(man != null)
            {
                man = await _managerRepository.DeleteManagerAsync(id);
                return Ok(man); 
            }
            return NotFound();
        }
    }
}
