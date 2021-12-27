using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCAG_Repositories.Entities
{
    public class Employee
    {
        public Employee()
        {

        }
        public Employee(string firstName, string lastName, List<string> employeeAccess, List<Gremlin> captureGremlins )
        {
            FirstName = firstName;
            LastName = lastName;
            EmployeeAccess = employeeAccess;
            CapturedGremlins = captureGremlins;
        }
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return $"{FirstName} {LastName}"; } }
        public List<string> EmployeeAccess { get; set; } = new List<string>();
        public List<Gremlin> CapturedGremlins { get; set; } = new List<Gremlin>();
        public decimal TotalEarnings
        { 
            get
            {
                decimal totalEarnings = 0;  
                foreach(var gremlin in CapturedGremlins)
                {
                    totalEarnings += gremlin.GremlinValue;
                }
                return totalEarnings;
            }
        }
       
    }
}
