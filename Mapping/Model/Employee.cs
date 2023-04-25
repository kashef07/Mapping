using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Mapping.Model
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string? Name { get; set; }

        //Navigation Properties
        public virtual Address Address { get; set; }
    }
}
