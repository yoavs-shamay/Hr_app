using BE;
using System;
using System.Collections.Generic;
using System.IO;
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
        public static string BanksFile = "Banks.xml";
        public static string EmployersFile = "Employers.xml";
        public static string ContractsFile = "Contracts.xml";
        public static string DirectoryPath = "../../../XMLData/";
        public static XElement EmployeesElement;
        public static XElement SpecializationsElement;
        public static XElement BanksElement;
        public static XElement EmployersElement;
        public static XElement ContractsElement;
        private static XmlSerializer civicAddressSerilizer = new XmlSerializer(typeof(CivicAddress));

        static XMLSource()
        {
            EmployeesElement = loadFile(EmployeesFile);
            SpecializationsElement = loadFile(SpecializationsFile);
            BanksElement = loadFile(BanksFile);
            EmployersElement = loadFile(EmployersFile);
            ContractsElement = loadFile(ContractsFile);
        }

        private static XName getXName(string name)
        {
            return new XElement(name).Name;
        }

        private static XElement loadFile(string name)
        {
            return XElement.Load(filePath(name));
        }

        private static string filePath(string name) => DirectoryPath + name;

        private static XElement getXElement(string name, params string[] value)
        {
            return new XElement(getXName(name), value);
        }

        private static XElement serializeValue<T>(T element, XmlSerializer serializer)
        {
            MemoryStream stream = new MemoryStream();
            serializer.Serialize(stream, element);
            stream.Position = 0;
            XmlReader reader = XmlReader.Create(stream);
            XElement result = XElement.Load(reader);
            reader.Close();
            stream.Close();
            return result;
        }

        public static void downloadBanksXML()
        {
            XElement bankRoot = XElement.Load(@"https://www.boi.org.il/he/BankingSupervision/BanksAndBranchLocations/Lists/BoiBankBranchesDocs/atm.xml");
            XElement localRoot = new XElement(getXName("banks"));
            foreach (XElement bank in bankRoot.Elements())
            {
                XElement localFileElement = new XElement(getXName("Bank"));
                localFileElement.Add(getXElement("BankNumber", bank.Element(getXName("קוד_בנק")).Value));
                string bankAddress = bank.Element("כתובת_ה-ATM").Value;
                string city = bank.Element("ישוב").Value;
                string[] bankAddressSplitted = bankAddress.Replace(city, "").Trim().Split(' ');
                string houseNumber = "";
                string streetName = "";
                for (int i = 0; i < bankAddressSplitted.Length; i++)
                {
                    uint value;
                    if (uint.TryParse(bankAddressSplitted[i],out value))
                    {
                        houseNumber = bankAddressSplitted[i];
                        break;
                    }
                    streetName += bankAddressSplitted[i];
                }
                CivicAddress address = new CivicAddress { City = city, StreetName = streetName, HouseNumber = uint.Parse(houseNumber), IsPrivateHouse = true, ApartmentNumber = 0 };
                XElement addressElement = serializeValue(address, civicAddressSerilizer);
                addressElement.Name = getXName("BranchAddress");
                localFileElement.Add(addressElement);
                localFileElement.Add(getXElement("BankName", bank.Element("שם_בנק").Value.Trim(' ','\n','\r'))); //TODO fix branches
                localFileElement.Add(getXElement("BranchNumber", bank.Element("קוד_סניף").Value));
                localRoot.Add(localFileElement);
            }
            localRoot.Save(filePath(BanksFile));
            BanksElement = localRoot;
        }
    }
}
