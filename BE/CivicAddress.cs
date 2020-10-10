using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class CivicAddress
    {
        public string City { get; set; }
        public string StreetName { get; set; }
        public uint HouseNumber { get; set; }
        public bool isPrivateHouse { get; set; }
        public uint? ApartmentNumber { get; set; }
    }
}
