using System.ComponentModel.DataAnnotations;

namespace EmployeeTask.Models
{
    public class EmployeeFormViewModel
    {
        public EmployeeFormViewModel()
        {
            Positions = new List<Positions>(); 
        }
             public int EmployeeID { get; set; }

            public string FirstName { get; set; }

            
            public string MiddleName { get; set; }

            
            public string LastName { get; set; }

            
            public string Address { get; set; }

           
            [EmailAddress]
            public string Email { get; set; }

            
            public double Salary { get; set; }

            
            public string EmployeeCode { get; set; }

            
            public int PositionId { get; set; }

             [DataType(DataType.Date)]
            public DateTime JobStartDate { get; set; }

            [DataType(DataType.Date)]
             public DateTime JobEndDate { get; set; }

            
            public bool IsDisable { get; set; }

            public List<Positions> Positions { get; set; }  
        }
    }
