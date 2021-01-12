using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Security.Cryptography;
using System.Diagnostics;
using System.ComponentModel.Design;
using System.Reflection;

namespace BL
{
    public class BL_imp : IBL
    {
        Idal DalObject = FactoryDAL.Dal_instance;
        const int TotalDaysInYear = 365;

        /// <summary>
        /// Adds a contract to the database
        /// </summary>
        /// <param name="contract">The contract to add</param>
        public void addContract(Contract contract)
        {
            CheckPropertyExists<Contract>(DalObject.getAllContracts(), "Id", contract);
            #region check if emplyee and employer exists
            if (DalObject.getAllEmployees().Count(x => x.Id == contract.EmployeeId) == 0)
            {
                throw new Exception("Employee not exists");
            }
            if (DalObject.getAllEmployers().Count(x => x.Id == contract.EmployerId) == 0)
            {
                throw new Exception("Employer not exists");
            }
            #endregion

            Employer employer = DalObject.getAllEmployers().Find(x => (x.Id == contract.EmployerId));
            // check if the employer setup date is at least before 1 year
            if (DateTime.Today.Subtract(employer.SetupDate).TotalDays < TotalDaysInYear)
            {
                throw new Exception("Employer exists less than 1 year");
            }

            Employee employee = DalObject.getAllEmployees().Find(x => (x.Id == contract.EmployeeId));
            // check if gross wage is in employee specialization range
            if (contract.GrossWageForHour > employee.EmployeeSpecialization.MaxWageForHour || 
                contract.GrossWageForHour < employee.EmployeeSpecialization.MinWageForHour)
            {
                throw new Exception($"Contract's gross wage isn't in employee's specialization range. it has to be minimum {employee.EmployeeSpecialization.MinWageForHour} and maximum {employee.EmployeeSpecialization.MaxWageForHour}");
            }

            // calculate how much employee and employer contracts already exists
            int numberOfEmployeeContracts = 0;
            int numberOfEmployerContracts = 0;
            foreach (Contract c in DalObject.getAllContracts())
            {
                if (c.EmployeeId == contract.EmployeeId)
                {
                    numberOfEmployeeContracts++;
                }
                if (c.EmployerId == contract.EmployerId)
                {
                    numberOfEmployerContracts++;
                }
            }

            // calculate comision using numberOfEmployeeContracts and numberOfEmployerContracts
            double Commision = 10 - numberOfEmployeeContracts;
            if (Commision < 3)
            {
                Commision = 3;
            }
            if (numberOfEmployerContracts < 50)
            {
                Commision += 10 - (numberOfEmployerContracts * .2);
            }

            //Enter net wage to the contract object
            contract.NetWageForHour = contract.GrossWageForHour * (1-(Commision/100));

            DalObject.addContract(contract);
        }

        /// <summary>
        /// Checks if ID is valid
        /// </summary>
        /// <param name="id">The ID to check</param>
        /// <returns>true if the ID is valid, false if not</returns>
        private bool checkID(string id)
        {
            int weight = 1;
            int sum = 0;

            foreach (char digit in id)
            {
                int digitNum = int.Parse(digit.ToString());
                int addToSum = digitNum * weight;
                if (addToSum > 9)
                {
                    addToSum = addToSum % 10 + ((int)addToSum / 10);
                }
                sum += addToSum;
                weight = 3 - weight;
            }
            return sum % 10 == 0;
        }

        /// <summary>
        /// Adds an employee to the database
        /// </summary>
        /// <param name="employee">The employee to add</param>
        public void addEmployee(Employee employee)
        {
            CheckPropertyExists(DalObject.getAllEmployees(), "BankAccount", employee);
            CheckPropertyExists(DalObject.getAllEmployees(), "Email", employee);
            CheckPropertyExists(DalObject.getAllEmployees(), "Id", employee);
            CheckPropertyExists(DalObject.getAllEmployees(), "PhoneNumber", employee);
            if (DalObject.getAllEmployees().Count(x => (x.FirstName == employee.FirstName) && (x.LastName == employee.LastName)) > 0)
            {
                throw new Exception("Employee with this name already exists");
            }

            if (DateTime.Today.Subtract(employee.Birth).TotalDays < TotalDaysInYear*18)
            {
                throw new Exception("Employee is less than 18 years old");
            }

            if (!checkID(employee.Id))
            {
                throw new Exception("Invalid ID");
            }
            DalObject.addEmployee(employee);
        }

        /// <summary>
        /// throw exception if an item in the list with a equal property of the new item already exists
        /// </summary>
        /// <typeparam name="T">the type of the item</typeparam>
        /// <param name="list">the list of itemsin database</param>
        /// <param name="variableName">the property name</param>
        /// <param name="newItem">the new item</param>
        private void CheckPropertyExists<T>(List<T> list, string variableName, T newItem)
        {
            Type type = typeof(T);
            PropertyInfo property = type.GetProperty(variableName);
            if (list.Count(x => (property.GetValue(newItem).Equals(property.GetValue(x)))) > 0)
            {
                throw new Exception($"{type.Name} with this {variableName} already exists");
            }
        }

        /// <summary>
        /// Adds an employer to the database
        /// </summary>
        /// <param name="employer">The employer to add</param>
        public void addEmployer(Employer employer)
        {
            if (employer.IsPrivate && employer.CompanyName != employer.FirstName)
            {
                throw new Exception("Company name doesn't equals to first name");
            }
            List<Employer> employers = DalObject.getAllEmployers();
            CheckPropertyExists<Employer>(employers, "CompanyName", employer);
            CheckPropertyExists<Employer>(employers, "Id", employer);
            CheckPropertyExists<Employer>(employers, "PhoneNumber", employer);
            if (!employer.IsPrivate)
            {
                CheckPropertyExists<Employer>(employers, "Address", employer);
            }
            if (employers.Count(x => (x.FirstName == employer.FirstName) && (x.LastName == employer.LastName)) > 0)
            {
                throw new Exception("Employer with this name already exists");
            }
            if (!checkID(employer.Id))
            {
                throw new Exception("Invalid ID");
            }
            DalObject.addEmployer(employer);
        }

        /// <summary>
        /// Adds a specialization to the database
        /// </summary>
        /// <param name="specialization">The specialization to add</param>
        public void addSpecialization(Specialization specialization)
        {
            CheckPropertyExists(DalObject.getAllSpecializations(),"Id",specialization);
            CheckPropertyExists(DalObject.getAllSpecializations(), "Name", specialization);
            if (DalObject.getAllSpecializations().Count(x => (x.Degree == specialization.Degree) && (x.Area == specialization.Area)) > 0)
            {
                throw new Exception("Specialization with this degree and area already exists");
            }
            if (specialization.MaxWageForHour < specialization.MinWageForHour)
            {
                throw new Exception("The max wage per hour is smaller then the min wage per hour");
            }
            DalObject.addSpecialization(specialization);
        }

        /// <summary>
        /// Get all banks in the database
        /// </summary>
        /// <returns>List of all the banks in the database</returns>
        public List<Bank> getAllBanks()
        {
            return DalObject.getAllBanks();
        }

        /// <summary>
        /// Get all contracts in the database
        /// </summary>
        /// <returns>List of all contracts in the database</returns>
        public List<Contract> getAllContracts()
        {
            return DalObject.getAllContracts();
        }

        /// <summary>
        /// Get all employees in the database
        /// </summary>
        /// <returns>List of all employees in the database</returns>
        public List<Employee> getAllEmployees()
        {
            return DalObject.getAllEmployees();
        }

        /// <summary>
        /// Get all employers in the database
        /// </summary>
        /// <returns>List of all employers in the database</returns>
        public List<Employer> getAllEmployers()
        {
            return DalObject.getAllEmployers();
        }

        /// <summary>
        /// Get all specializations in the database
        /// </summary>
        /// <returns>List of all specializations in the database</returns>
        public List<Specialization> getAllSpecializations()
        {
            return DalObject.getAllSpecializations();
        }

        /// <summary>
        /// Removes a contract from the database
        /// </summary>
        /// <param name="contract">The contract to remove</param>
        public void removeContract(Contract contract)
        {
            DalObject.removeContract(contract);
        }

        /// <summary>
        /// Removes an employee from the database
        /// </summary>
        /// <param name="employee">The employee to remove</param>
        public void removeEmployee(Employee employee)
        {
            if (DalObject.getAllContracts().Count(x => x.EmployeeId == employee.Id) > 0)
            {
                throw new Exception("Employee has active contract");
            }
            DalObject.removeEmployee(employee);
        }

        /// <summary>
        /// removes an employer from the database
        /// </summary>
        /// <param name="employer">The employer to remove</param>
        public void removeEmployer(Employer employer)
        {
            if (DalObject.getAllContracts().Count(x => x.EmployerId == employer.Id) > 0)
            {
                throw new Exception("Employer has active contract");
            }
            DalObject.removeEmployer(employer);
        }

        /// <summary>
        /// Removes a specialization from the database
        /// </summary>
        /// <param name="specialization">The specialization to remove</param>
        public void removeSpecialization(Specialization specialization)
        {
            if (DalObject.getAllEmployees().Count(x => x.EmployeeSpecialization.Id == specialization.Id) > 0)
            {
                throw new Exception("There are employees with this specialization");
            }
            DalObject.removeSpecialization(specialization);
        }

        private static void copyObject<T>(T source, T target)
        {
            var props = typeof(T).GetProperties();
            foreach (var p in props)
            {
                if (p.PropertyType == typeof(CivicAddress))
                {
                    CivicAddress instance = new CivicAddress();
                    CivicAddress propertySource = p.GetValue(source) as CivicAddress;
                    copyObject<CivicAddress>(propertySource, instance);
                    p.SetValue(target, instance);
                }
                else if (p.PropertyType == typeof(Bank))
                {
                    Bank instance = new Bank();
                    Bank propertySource = p.GetValue(source) as Bank;
                    copyObject<Bank>(propertySource, instance);
                    p.SetValue(target, instance);
                }
                else
                {
                    p.SetValue(target, p.GetValue(source));
                }
            }
        }

        /// <summary>
        /// Updates a contract in the database
        /// </summary>
        /// <param name="newContract">The new updated contract</param>
        /// <param name="oldContract">The old contract to update</param>
        public void updateContract(Contract newContract, Contract oldContract)
        {
            Contract oldContractCopy = new Contract();
            copyObject<Contract>(oldContract, oldContractCopy);
            removeContract(oldContract);
            try
            {
                addContract(newContract);
            }
            catch (Exception err)
            {
                addContract(oldContractCopy);
                throw err;
            }
        }

        /// <summary>
        /// Updates an employee in the database
        /// </summary>
        /// <param name="newEmployee">The new updated employee</param>
        /// <param name="oldEmployee">The old employee to update</param>
        public void updateEmployee(Employee newEmployee, Employee oldEmployee)
        {
            Employee oldEmployeeCopy = new Employee();
            copyObject<Employee>(oldEmployee, oldEmployeeCopy);
            removeEmployee(oldEmployee);
            try
            {
                addEmployee(newEmployee);
            }
            catch (Exception err)
            {
                addEmployee(oldEmployeeCopy);
                throw err;
            }
        }

        /// <summary>
        /// Updates an employer in the database
        /// </summary>
        /// <param name="newEmployer">The new updated employer</param>
        /// <param name="oldEmployer">The old employer to update</param>s
        public void updateEmployer(Employer newEmployer, Employer oldEmployer)
        {
            Employer oldEmployerCopy = new Employer();
            copyObject<Employer>(oldEmployer, oldEmployerCopy);
            removeEmployer(oldEmployer);
            try
            {
                addEmployer(newEmployer);
            }
            catch (Exception err)
            {
                addEmployer(oldEmployerCopy);
                throw err;
            }
        }

        /// <summary>
        /// Update a specialization in the database
        /// </summary>
        /// <param name="newSpecialization">The new updated specialization</param>
        /// <param name="oldSpecialization">The old specialization to update</param>
        public void updateSpecialization(Specialization newSpecialization, Specialization oldSpecialization)
        {
            Specialization oldSpecilaizationCopy = new Specialization();
            copyObject<Specialization>(oldSpecialization, oldSpecilaizationCopy);
            removeSpecialization(oldSpecialization);
            try
            {
                addSpecialization(newSpecialization);
            }
            catch (Exception err)
            {
                addSpecialization(oldSpecilaizationCopy);
                throw err;
            }
        }

        /// <summary>
        /// Get all contracts that matches a condition
        /// </summary>
        /// <param name="condition">The condition to match</param>
        /// <returns>IEnumerable of contracts that match the condition</returns>
        public IEnumerable<Contract> getAllContractsWithCondition(Predicate<Contract> condition)
        {
            var result = from c in DalObject.getAllContracts() where condition(c) select c;
            return result;
        }

        /// <summary>
        /// Gets all contracts that matches a condition and the count of them
        /// </summary>
        /// <param name="condition">The condition to match</param>
        /// <param name="count">The variable that after the function run will store the count of matching conditions</param>
        /// <returns>IEnumerable of contracts that match the condition</returns>
        public IEnumerable<Contract> getAllContractsWithCondition(Predicate<Contract> condition, out int count)
        {
            var result = from c in DalObject.getAllContracts() where condition(c) select c;
            count = result.Count();
            return result;
        }

        /// <summary>
        /// Returns contracts groups by the specialization of them
        /// Returns IEnumerable of ContractGroupContainers so there will be no use of template and
        /// select with let's will be able to use in querys, ContractGroupContainer contains Key - object and Value - Contract
        /// </summary>
        /// <param name="sorted">If sort the output</param>
        /// <returns>An IEnumerable of ContractGroupContainer of Specialization and Contract</returns>
        public IEnumerable<ContractGroupContainer> getAllCountractsGroupedBySpecialization(bool sorted = false)
        {
            var value = from c in DalObject.getAllContracts()
                        let contractSpecialization = DalObject.getAllEmployees().Find(e => e.Id == c.EmployeeId).EmployeeSpecialization
                        orderby (sorted)? contractSpecialization.Name : null
                        select new ContractGroupContainer { Key = contractSpecialization, Value = c };
            return value;
        }

        /// <summary>
        /// Returns contracts grouped by the employee address
        /// Returns IEnumerable of ContractGroupContainers so there will be no use of template and
        /// select with let's will be able to use in querys, ContractGroupContainer contains Key - object and Value - Contract
        /// </summary>
        /// <param name="sorted">If sort the output</param>
        /// <returns>An IEnumerable of ContractGroupContainer of CivicAddress and Contract</returns>
        public IEnumerable<ContractGroupContainer> getAllCountractsGroupedByAddress(bool sorted = false)
        {
            var value = from c in DalObject.getAllContracts()
                        let contractAddress = DalObject.getAllEmployees().Find(e => e.Id == c.EmployeeId).Address
                        orderby (sorted) ? contractAddress.City : null,
                        (sorted)? contractAddress.StreetName : null,
                        (sorted)? contractAddress.HouseNumber : 0
                        select new ContractGroupContainer { Key = contractAddress, Value = c };
            return value;
        }

        /// <summary>
        /// Returns all contracts grouped by the contract established date
        /// Returns IEnumerable of ContractGroupContainers so there will be no use of template and
        /// select with let's will be able to use in querys, ContractGroupContainer contains Key - object and Value - Contract
        /// </summary>
        /// <param name="sorted">If sort the output</param>
        /// <returns>An IEnumerable of ContractGroupContainer of DateTime and Contract</returns>
        public IEnumerable<ContractGroupContainer> getAllCountractsGroupedByTime(bool sorted = false)
        {
            var value = from c in DalObject.getAllContracts()
                        let time = c.ContractEstablishedDate
                        orderby (sorted)? time : DateTime.Now
                        select new ContractGroupContainer { Key = time, Value = c };
            return value;
        }
    }
}
