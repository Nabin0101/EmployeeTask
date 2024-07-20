namespace EmployeeTask.Models
{
    public class EmployeePosition
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int PositionId { get; set; }
        public Positions Position { get; set; }
    }
}
