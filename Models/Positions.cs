using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EmployeeTask.Models
{
    public class Positions
    {
        [Key]
        public int PositionId { get; set; }

        [Required]
        public string PositionName { get; set; }

        public ICollection<EmployeeJobHistories> EmployeeJobHistories { get; set; } = new List<EmployeeJobHistories>();
        public ICollection<EmployeePosition> EmployeePositions { get; set; } = new List<EmployeePosition>();
    }
}
