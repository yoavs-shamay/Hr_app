using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Contract
    {
        public string Id { get; set; }
        public string EmployerId { get; set; } 
        public string EmployeeId { get; set; }
        public bool IsInterview { get; set; }
        public bool IsFinalized { get; set; }
        public double GrossWageForHour { get; set; } //check if postive
        public double NetWageForHour { get; set; }// check if positive
        public DateTime ContractEstablishedDate { get; set; }
        public DateTime TerminateDate { get; set; }
        public double MaxWorkHoursForMonth { get; set; } //check if positive
        public double MinWorkHoursForMonth { get; set; } //check if positive
    }
}
