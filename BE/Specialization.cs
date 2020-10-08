using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Specialization
    {
        public enum Education
        {
            CartificateOnly, BA, MA, PHD, Student, None
        }
        public string Id { get; set; }
        public Proffesion Area { get; set; }
        public double MinWageForHour { get; set; } // check if positive
        public double MaxWageForHour { get; set; } // check if positive
        public Education Degree { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, Degree: {Degree.ToString()}, Area: {Area.ToString()}";
        }
    }
}
