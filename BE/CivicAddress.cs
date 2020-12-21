using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class CivicAddress : INotifyPropertyChanged
    {
        private uint? apartmentNumber;
        public uint? ApartmentNumber { get { return apartmentNumber; }
            set { apartmentNumber = value; propertyChanged("ApartmentNumber"); } }
        private bool isPrivateHouse;
        public bool IsPrivateHouse { get { return isPrivateHouse; }
            set { isPrivateHouse = value; propertyChanged("IsPrivateHouse"); } }
        private uint houseNumber;
        public uint HouseNumber { get { return houseNumber; }
            set { houseNumber = value; propertyChanged("HouseNumber"); } }
        private string streetName;
        public string StreetName { get { return streetName; }
            set { streetName = value; propertyChanged("StreetName"); } }
        private string city;
        public string City { get { return city; }
            set { city = value; propertyChanged("City"); } }

        public override string ToString()
        {
            return $"{City}, Street {StreetName}, Number {HouseNumber}" + ((IsPrivateHouse) ? "" : $", Apartment {ApartmentNumber}");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void propertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
