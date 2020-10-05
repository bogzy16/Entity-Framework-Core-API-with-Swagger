using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST_API.Models
{
    public partial class Request
    {
        public int employeeNumber { get; set; } = 0;
        public string firstName { get; set; }
        public string lastName { get; set; }
        public decimal temperature { get; set; }
        public int tempStart { get; set; }
        public int tempEnd { get; set; }
        public DateTime? recordStartDate { get; set; } = null;
        public DateTime? recordEntDate { get; set; } = null;
    }

    public static class MappeEntity
    {
        public static EmployeeRecord ToEntity(this Request request) => new EmployeeRecord
        {
            EmployeeNumber = request.employeeNumber,
            FistName = request.firstName,
            LastName = request.lastName,
            Temperature = request.temperature
        };
    }
}
