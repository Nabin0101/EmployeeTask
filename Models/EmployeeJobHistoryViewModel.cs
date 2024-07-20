using System.ComponentModel.DataAnnotations;

namespace EmployeeTask.Models
{
    public class EditEmployeeJobHistoryModel
    {
            [Required]
            public int EmployeeJobHistoryId { get; set; }

            [Required]
            public int PositionId { get; set; }

            [Required]
            [DataType(DataType.Date)]
            public DateTime StartDate { get; set; }

            [Required]
            [DataType(DataType.Date)]
            public DateTime EndDate { get; set; }

            public IEnumerable<Positions> Positions { get; set; }
        }
    }
