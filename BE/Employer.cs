using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Employer
    {
        public string Id { get; set; }
        public bool IsPrivate { get; set; }
        public string PhoneNumber { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string CompanyName { get; set; }
        public CivicAddress Address { get; set; }
        public Proffesion EmployerProffesion { get; set; }
        public DateTime SetupDate { get; set; }

        public override string ToString()
        {
            return $"Company Name: {CompanyName}, Employer: {LastName} {FirstName}";
        }
    }
}
