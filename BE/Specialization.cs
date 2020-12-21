using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Specialization : INotifyPropertyChanged
    {
        public enum Education
        {
            CartificateOnly, BA, MA, PHD, Student, None
        }
        private string name;
        public string Name { get { return name; }
            set { name = value; propertyChanged("Name"); } }
        private Education degree;
        public Education Degree { get { return degree; }
            set { degree = value; propertyChanged("Degree"); } }
        private double maxWageForHour;
        public double MaxWageForHour { get { return maxWageForHour; }
            set { maxWageForHour = value; propertyChanged("MaxWageForHour"); } } // check if positive
        private double minWageForHour;
        public double MinWageForHour { get { return minWageForHour; }
            set { minWageForHour = value; propertyChanged("MinWageForHour"); } } // check if positive
        private Proffesion area;
        public Proffesion Area { get { return area; }
            set { area = value; propertyChanged("Area"); } }
        private string id;
        public string Id { get { return id; }
            set { id = value; propertyChanged("Id"); } }

        public override string ToString()
        {
            return $"Name: {Name}, Degree: {Degree.ToString()}, Area: {Area.ToString()}";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void propertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
