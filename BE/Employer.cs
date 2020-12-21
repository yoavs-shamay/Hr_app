using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Employer : INotifyPropertyChanged
    {
        private DateTime setupDate;
        public DateTime SetupDate { get { return setupDate; }
            set { setupDate = value; propertyChanged("SetupDate"); } }
        private Proffesion employerProffesion;
        public Proffesion EmployerProffesion { get { return employerProffesion; }
            set { employerProffesion = value; propertyChanged("EmployerProffesion"); } }
        private CivicAddress address;
        public CivicAddress Address { get { return address; }
            set { address = value; propertyChanged("Address"); } }
        private string companyName;
        public string CompanyName { get { return companyName; }
            set { companyName = value; propertyChanged("CompanyName"); } }
        private string firstName;
        public string FirstName { get { return firstName; }
            set { firstName = value; propertyChanged("FirstName"); } }
        private string lastName;
        public string LastName { get { return lastName; }
            set { lastName = value; propertyChanged("LastName"); } }
        private string phoneNumber;
        public string PhoneNumber { get { return phoneNumber; }
            set { phoneNumber = value; propertyChanged("PhoneNumber"); } }
        private bool isPrivate;
        public bool IsPrivate { get { return isPrivate; }
            set { isPrivate = value; propertyChanged("IsPrivate"); } }
        private string id;
        public string Id { get { return id; }
            set { id = value; propertyChanged("Id"); } }

        public override string ToString()
        {
            return $"Company Name: {CompanyName}, Employer: {LastName} {FirstName}";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void propertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
