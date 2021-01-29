using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DS
{
    public static class XMLSource
    {
        public static string EmployeesFile = "Employees.xml";
        public static string SpecializationsFile = "Specializations.xml";
        public static string DirectoryPath = "../../../../XMLData/";
        public static XElement EmployeesElement;
        public static XElement SpecializationsElement;

        static XMLSource()
        {
            EmployeesElement = loadFile(EmployeesFile);
            SpecializationsElement = loadFile(SpecializationsFile);
        }

        private static XElement loadFile(string name)
        {
            return XElement.Load(filePath(name));
        }

        private static string filePath(string name) => DirectoryPath + name;

    }
}
