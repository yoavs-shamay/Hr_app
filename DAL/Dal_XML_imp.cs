using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DS;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace DAL
{
    public class Dal_XML_imp : Idal
    {
        public static XmlSerializer CivicAddressSerializer = new XmlSerializer(typeof(CivicAddress));

        private static string filePath(string name) => XMLSource.DirectoryPath + name;
        private static object desearilizeValue(XElement element, string name, XmlSerializer serializer)
        {
            XElement elementToDesearilize = element.Element(getXName(name));
            var reader = elementToDesearilize.CreateReader();
            return serializer.Deserialize(reader);
        }

        private static XElement searilizeValue<T>(T element, XmlSerializer serializer)
        {
            XDocument document = new XDocument();
            var writer = document.CreateWriter();
            serializer.Serialize(writer, element);
            return document.Root;
        }
        private static XName getXName(string name)
        {
            return new XElement(name).Name;
        }

        private static string getValue(XElement element, string name)
        {
            return element.Element(getXName(name)).Value;
        }

        private XElement getXElement(string name, params string[] value)
        {
            return new XElement(getXName(name), value);
        }

        private void remove(string ID, XElement root)
        {
            foreach (XElement childElement in root.Elements())
            {
                if (childElement.Attribute(getXName("ID")).Value == ID)
                {
                    childElement.Remove();
                    break;
                }
            }
        }

        public void addContract(Contract contract)
        {
            throw new NotImplementedException();
        }

        public void addEmployee(Employee employee)
        {
            XElement root = XMLSource.EmployeesElement;
            XElement employeeElement = new XElement(getXName("employee"));
            employeeElement.SetAttributeValue(getXName("ID"), employee.Id);
            employeeElement.Add(getXElement("firstName",employee.FirstName));
            employeeElement.Add(getXElement("lastName", employee.LastName));
            employeeElement.Add(getXElement("isMale", employee.IsMale.ToString()));
            employeeElement.Add(getXElement("phoneNumber", employee.PhoneNumber.ToString()));
            employeeElement.Add(getXElement("email", employee.Email.ToString()));
            employeeElement.Add(getXElement("birth", employee.Birth.ToString()));
            employeeElement.Add(getXElement("personalEducation", employee.PersonalEducation.ToString()));
            employeeElement.Add(getXElement("armyGraduate", employee.ArmyGraduate.ToString()));
            employeeElement.Add(getXElement("yearsOfExperience", employee.YearsOfExperience.ToString()));
            employeeElement.Add(getXElement("specializationID", employee.EmployeeSpecialization.Id));
            employeeElement.Add(searilizeValue<CivicAddress>(employee.Address, CivicAddressSerializer));
            XElement bankElement = new XElement(getXName("bank"));
            bankElement.SetAttributeValue(getXName("bankAccount"), employee.BankAccount.AccountNumber);
            bankElement.Add(getXElement("bankNumber", employee.BankAccount.BankNumber.ToString()));
            bankElement.Add(getXElement("branchNumber", employee.BankAccount.BranchNumber.ToString()));
            employeeElement.Add(bankElement);
            root.Add(employeeElement);
            root.Save(filePath(XMLSource.EmployeesFile));
            XMLSource.EmployeesElement = root;
        }

        public void addEmployer(Employer employer)
        {
            throw new NotImplementedException();
        }

        public void addSpecialization(Specialization specialization)
        {
            XElement root = XMLSource.SpecializationsElement;
            XElement specializationElement = new XElement(getXName("specialization"));
            specializationElement.SetAttributeValue(getXName("ID"), specialization.Id);
            specializationElement.Add(getXElement("name", specialization.Name));
            specializationElement.Add(getXElement("maxWageForHour", specialization.MaxWageForHour.ToString()));
            specializationElement.Add(getXElement("minWageForHour", specialization.MinWageForHour.ToString()));
            specializationElement.Add(getXElement("degree", specialization.Degree.ToString()));
            specializationElement.Add(getXElement("area", specialization.Area.ToString()));
            root.Add(specializationElement);
            root.Save(filePath(XMLSource.EmployeesFile));
            XMLSource.SpecializationsElement = root;
        }

        public List<Bank> getAllBanks()
        {
            throw new NotImplementedException();
        }

        public List<Contract> getAllContracts()
        {
            throw new NotImplementedException();
        }

        public List<Employee> getAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            XElement root = XMLSource.EmployeesElement;
            foreach (XElement employeeElement in root.Elements())
            {
                Employee employee = new Employee();
                employee.Id = employeeElement.Attribute(new XElement("ID").Name).Value;
                employee.FirstName = getValue(employeeElement, "firstName");
                employee.LastName = getValue(employeeElement, "lastName");
                employee.IsMale = bool.Parse(getValue(employeeElement, "isMale"));
                employee.PhoneNumber = getValue(employeeElement, "phoneNumber");
                employee.Email = getValue(employeeElement, "email");
                employee.Birth = DateTime.Parse(getValue(employeeElement, "birth"));
                employee.PersonalEducation = (Specialization.Education?)Enum.Parse(typeof(Specialization.Education), getValue(employeeElement, "personalEducation"));
                employee.ArmyGraduate = bool.Parse(getValue(employeeElement, "armyGraduate"));
                employee.YearsOfExperience = uint.Parse(getValue(employeeElement, "yearsOfExperience"));
                employee.EmployeeSpecialization = getAllSpecializations().Find(spec => spec.Id == getValue(employeeElement, "specializationID"));
                employee.Address = (CivicAddress)desearilizeValue(employeeElement, "CivicAddress", CivicAddressSerializer);
                XElement bankElement = employeeElement.Element("bank");   
                Bank employeeBank = getAllBanks().Find(b => 
                b.BankNumber == uint.Parse(getValue(bankElement, "bankNumber")) && b.BranchNumber == uint.Parse(getValue(bankElement,"branchNumber")));
                employeeBank.AccountNumber = uint.Parse(bankElement.Attribute(getXName("bankAccount")).Value);
                employee.BankAccount = employeeBank;
                employees.Add(employee);
            }
            return employees;
        }

        public List<Employer> getAllEmployers()
        {
            throw new NotImplementedException();
        }

        public List<Specialization> getAllSpecializations()
        {
            List<Specialization> specs = new List<Specialization>();
            XElement root = XMLSource.SpecializationsElement;
            foreach (XElement specializationElement in root.Elements())
            {
                Specialization spec = new Specialization();
                spec.Id = specializationElement.Attribute(getXName("ID")).Value;
                spec.Name = getValue(specializationElement, "name");
                spec.MaxWageForHour = double.Parse(getValue(specializationElement, "maxWageForHour"));
                spec.MinWageForHour = double.Parse(getValue(specializationElement, "minWageForHour"));
                spec.Degree = (Specialization.Education)Enum.Parse(typeof(Specialization.Education), getValue(specializationElement, "degree"));
                spec.Area = (Proffesion)Enum.Parse(typeof(Proffesion), getValue(specializationElement, "area"));
                specs.Add(spec);
            }
            return specs;
        }

        public void removeContract(Contract contract)
        {
            throw new NotImplementedException();
        }

        public void removeEmployee(Employee employee)
        {
            XElement root = XMLSource.EmployeesElement;
            remove(employee.Id, root);
            XMLSource.EmployeesElement = root;
            root.Save(filePath(XMLSource.EmployeesFile));
        }

        public void removeEmployer(Employer employer)
        {
            throw new NotImplementedException();
        }

        public void removeSpecialization(Specialization specialization)
        {
            XElement root = XMLSource.SpecializationsElement;
            remove(specialization.Id, root);
            XMLSource.SpecializationsElement = root;
            root.Save(filePath(XMLSource.SpecializationsFile));
        }

        public void updateContract(Contract newContract, Contract oldContract)
        {
            throw new NotImplementedException();
        }

        public void updateEmployee(Employee newEmployee, Employee oldEmployee)
        {
            removeEmployee(oldEmployee);
            addEmployee(newEmployee);
        }

        public void updateEmployer(Employer newEmployer, Employer oldEmployer)
        {
            throw new NotImplementedException();
        }

        public void updateSpecialization(Specialization newSpecialization, Specialization oldSpecialization)
        {
            removeSpecialization(oldSpecialization);
            addSpecialization(newSpecialization);
        }
    }
}
