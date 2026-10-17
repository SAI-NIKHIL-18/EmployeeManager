using System.ComponentModel.DataAnnotations;

namespace EmployeeManager.Models.Domain
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Dob { get; set; }
        public double Salary { get; set; }
        public string Location { get; set; }
        public int ManagerId { get; set; }
        //public Manager Manager { get; set; }
    }
}
