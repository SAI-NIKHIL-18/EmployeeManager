using EmployeeManager.Models.Domain;
using EmployeeManager.Models.DTO;
using EmployeeManager.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace EmployeeManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var emp = await _employeeRepository.GetAllEmployeesAsync();
            var dtoEmployee = new List<EmployeeDto>();
            foreach (var employee in emp)
            {
                dtoEmployee.Add(new EmployeeDto
                {
                    EmployeeId = employee.EmployeeId,
                    Name = employee.Name,
                    Email = employee.Email,
                    Dob = employee.Dob,
                    Salary = employee.Salary,
                    Location = employee.Location,
                    ManagerId = employee.ManagerId
                });
            }
            return Ok(dtoEmployee);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEmployeeByIdAsync(int id)
        {
            var emp = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (emp == null)
            {
                return NotFound();
            }
            return Ok(emp);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeUpdateDto employeeUpdateDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var emp = new Employee
            {
                Name = employeeUpdateDto.Name,
                Email = employeeUpdateDto.Email,
                Dob = employeeUpdateDto.Dob,
                Salary = employeeUpdateDto.Salary,
                Location = employeeUpdateDto.Location,
                ManagerId = employeeUpdateDto.ManagerId
            };
            emp = await _employeeRepository.CreateEmployeeAsync(emp);

            var dtoEmp = new EmployeeDto
            {
                Name = emp.Name,
                Email = emp.Email,
                Dob = emp.Dob,
                Salary = emp.Salary,
                Location = emp.Location,
                ManagerId = emp.ManagerId
            };
            return Ok(dtoEmp);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeUpdateDto employeeUpdateDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var emp = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (emp == null)
            {
                return NotFound();
            }
            emp = new Employee
            {
                Name = employeeUpdateDto.Name,
                Email = employeeUpdateDto.Email,
                Dob = employeeUpdateDto.Dob,
                Salary = employeeUpdateDto.Salary,
                Location = employeeUpdateDto.Location,
                ManagerId = employeeUpdateDto.ManagerId
            };
            await _employeeRepository.UpdateEmployeeAsync(id, emp);
            var empDto = new EmployeeDto
            {
                Name = emp.Name,
                Email = emp.Email,
                Dob = emp.Dob,
                Salary = emp.Salary,
                Location = emp.Location,
                ManagerId = emp.ManagerId
            };
            return Ok(empDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var emp = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (emp == null)
            {
                return NotFound();
            }
            await _employeeRepository.DeleteEmployeeAsync(id);
            return Ok(emp);
        }

        [HttpGet("employee/testData")]
        public async Task<IActionResult> GetTestDataAsync()
        {
            byte[] fileBytes;

            using (var connection = new SqlConnection("Server=SAI_NIKHIL\\SQLEXPRESS;Database=EmployeeManager;Integrated Security=True;TrustServerCertificate=True;"))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("SELECT TOP 1 ManagerData FROM TestDataInputFile ORDER BY Id DESC", connection))
                {
                    var result = await command.ExecuteScalarAsync();

                    if (result == DBNull.Value || result == null)
                    {
                        return NotFound("No Excel file found in database.");
                    }

                    fileBytes = (byte[])result;
                }
            }

            return new FileContentResult(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = "TestData.xlsx"
            };
        }
    }
}
