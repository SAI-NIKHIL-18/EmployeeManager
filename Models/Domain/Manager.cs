using System.ComponentModel.DataAnnotations;

namespace EmployeeManager.Models.Domain
{
    public class Manager
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
