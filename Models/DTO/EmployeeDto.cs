using System.ComponentModel.DataAnnotations;

namespace EmployeeManager.Models.DTO
{
    public class EmployeeDto
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Dob { get; set; }
        [Required]
        public double Salary { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public int ManagerId { get; set; }
    }
}
