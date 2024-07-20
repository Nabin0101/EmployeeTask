using System.ComponentModel.DataAnnotations;

namespace EmployeeTask.Models
{
    public class EmployeeJobHistories
    {
        [Key]
        public int EmployeeJobHistoryId { get; set; }
        public int EmployeeId { get; set; }
        public int PositionId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Employee Employee { get; set; }
        public Positions Position { get; set; }
    }
}
