using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeTask.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public int PersonId { get; set; }
        public int PositionId { get; set; }
        public double Salary { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string EmployeeCode { get; set; }
        public bool IsDisable { get; set; }
        public bool IsDeleted { get; set; } = false;

        public People People { get; set; }
        public ICollection<EmployeeJobHistories> EmployeeJobHistories { get; set; }
        public ICollection<EmployeePosition> EmployeePositions { get; set; }

    }
}
