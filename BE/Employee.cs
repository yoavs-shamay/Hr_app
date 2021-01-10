using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Employee : INotifyPropertyChanged
    {
        private uint yearsOfExperience;
        public uint YearsOfExperience { get { return yearsOfExperience; } 
            set { yearsOfExperience = value; propertyChanged("YearsOfExperience"); } }
        private string email;
        public string Email { get { return email; } 
            set { email = value; propertyChanged("Email"); } }
        private bool isMale;
        public bool IsMale { get { return isMale; }
            set { isMale = value; propertyChanged("IsMale"); } }
        private Bank bankAccount;
        public Bank BankAccount { get { return bankAccount; }
            set { bankAccount = value; propertyChanged("BankAccount"); } }
        private bool armyGraduate;
        public bool ArmyGraduate { get { return armyGraduate; }
            set { armyGraduate = value; propertyChanged("ArmyGraduate"); } }
        private Specialization.Education? personalEducation;
        public Specialization.Education? PersonalEducation { get { return personalEducation; }
            set { personalEducation = value; propertyChanged("PersonalEducation"); } }
        private CivicAddress address;
        public CivicAddress Address { get { return address; }
            set { address = value; propertyChanged("Address"); } }
        private string phoneNumber;
        public string PhoneNumber { get { return phoneNumber; }
            set { phoneNumber = value; propertyChanged("PhoneNumber"); } }
        private DateTime birth;
        public DateTime Birth { get { return birth; }
            set { birth = value; propertyChanged("Birth"); } }
        private string firstName;
        public string FirstName { get { return firstName; }
            set { firstName = value; propertyChanged("FirstName"); } }
        private string lastName;
        public string LastName { get { return lastName; }
            set { lastName = value; propertyChanged("LastName"); } }
        private string id;
        public string Id { get { return id; }
            set { id = value; propertyChanged("Id"); } }
        private Specialization employeeSpecialization;
        public Specialization EmployeeSpecialization { get { return employeeSpecialization; }
            set { employeeSpecialization = value; propertyChanged("EmployeeSpecialization"); } }

        public event PropertyChangedEventHandler PropertyChanged;
        private void propertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        public override bool Equals(object obj) //TODO also to other classes
        {
            Employee other = obj as Employee;
            return other != null && other.id == id;
        }
    }
}
