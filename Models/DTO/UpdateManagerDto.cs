using System.ComponentModel.DataAnnotations;

namespace EmployeeManager.Models.DTO
{
    public class UpdateManagerDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }
    }
}
