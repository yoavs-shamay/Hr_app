using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Contract : INotifyPropertyChanged
    {
        private double minWorkHoursForMonth;
        public double MaxWorkHoursForMonth { get { return maxWorkHoursForMonth; }
            set { maxWorkHoursForMonth = value; propertyChanged("MaxWorkHoursForMonth"); } } //check if positive
        public double MinWorkHoursForMonth { get { return minWorkHoursForMonth; }
            set { minWorkHoursForMonth = value; propertyChanged("MinWorkHoursForMonth"); } } //check if positive
        private double maxWorkHoursForMonth;
        private DateTime terminateDate;
        public DateTime TerminateDate { get { return terminateDate; }
            set { terminateDate = value; propertyChanged("TerminateDate"); } }
        private DateTime contractEstablishedDate;
        public DateTime ContractEstablishedDate { get { return contractEstablishedDate; }
            set { contractEstablishedDate = value; propertyChanged("ContractEstablishedDate"); } }
        private double netWageForHour;
        public double NetWageForHour { get { return netWageForHour; }
            set { netWageForHour = value; propertyChanged("NetWageForHour"); } }// check if positive
        private double grossWageForHour;
        public double GrossWageForHour { get { return grossWageForHour; }
            set { grossWageForHour = value; propertyChanged("GrossWageForHour"); } } //check if postive
        private bool isFinalized;
        public bool IsFinalized { get { return isFinalized; }
            set { isFinalized = value; propertyChanged("IsFinalized"); } }
        private bool isInterview;
        public bool IsInterview { get { return isInterview; }
            set { isInterview = value; propertyChanged("IsInterview"); } }
        private string employeeId;
        public string EmployeeId { get { return employeeId; }
            set { employeeId = value; propertyChanged("EmployeeId"); } }
        private string employerId;
        public string EmployerId { get { return employerId; }
            set { employerId = value; propertyChanged("EmployerId"); } } 
        private string id;
        public string Id { get { return id; }
            set { id = value; propertyChanged("Id"); } }

        public event PropertyChangedEventHandler PropertyChanged;
        private void propertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
