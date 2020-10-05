using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST_API.Models
{
    public partial class EmployeeRecord
    {
        public int EmployeeNumber { get; set; }
        public string FistName { get; set; }
        public string LastName { get; set; }
        public decimal Temperature { get; set; }
        public bool isActive { get; set; }
        public DateTime RecordDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
