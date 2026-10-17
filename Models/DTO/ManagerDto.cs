using EmployeeManager.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManager.Models.DTO
{
    public class ManagerDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
