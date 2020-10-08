using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Employee
    {
        public string Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime Birth { get; set; }
        public string PhoneNumber { get; set; }
        public CivicAddress Address { get; set; }
        public Specialization.Education PersonalEducation { get; set; }
        public bool ArmyGraduate { get; set; }
        public Bank BankAccount { get; set; }
        public Specialization EmployeeSpecialization { get; set; }
        public bool IsMale { get; set; }
        public string Email { get; set; }
        public uint YearsOfExperience { get; set; }

    }
}
