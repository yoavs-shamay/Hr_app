using BE;
using DAL;
using DS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL
{
    public class Dal_imp : Idal
    {

        /// <summary>
        /// A generic function to add an item to list if it is not already exists
        /// </summary>
        /// <typeparam name="T">The generic item type</typeparam>
        /// <param name="list">The list to add the item to</param>
        /// <param name="element">The item to add</param>
        void addToList<T>(List<T> list, T element)
        {
            if (list.Contains(element))
            {
                throw new Exception(typeof(T).Name + " already exists");
            }
            list.Add(element);
        }

        /// <summary>
        /// A generic function to remove an item from list if its exists
        /// </summary>
        /// <typeparam name="T">The item type</typeparam>
        /// <param name="list">The list to add the item to</param>
        /// <param name="element">The item to add</param>
        void RemoveItem<T>(List<T> list, T element)
        {
            if (!list.Contains(element))
            {
                throw new Exception(typeof(T).Name + " not exists");
            }
            list.Remove(element);
        }

        /// <summary>
        /// A generic function to update an item in list, if its exists
        /// </summary>
        /// <typeparam name="T">The item type</typeparam>
        /// <param name="list">The list to update the item inside it</param>
        /// <param name="newItem">The new updated item</param>
        /// <param name="oldItem">The old item to update</param>
        void UpdateItem<T>(List<T> list, T newItem, T oldItem) where T : class
        {
            RemoveItem<T>(list, oldItem);
            addToList<T>(list, newItem);
        }

        /// <summary>
        /// Adds a contract to the database
        /// </summary>
        /// <param name="contract">The contract to add</param>
        public void addContract(Contract contract)=>addToList<Contract>(DataSource.contractList, contract);

        /// <summary>
        /// Adds an employee to the database
        /// </summary>
        /// <param name="employee">The employee to add</param>
        public void addEmployee(Employee employee) => addToList<Employee>(DataSource.employeeList, employee);
        /// <summary>
        /// Adds an employer to the database
        /// </summary>
        /// <param name="employer">The employer to add</param>
        public void addEmployer(Employer employer) => addToList<Employer>(DataSource.employerList, employer);

        /// <summary>
        /// Adds a specialization to the database
        /// </summary>
        /// <param name="specialization">The specialization to add</param>
        public void addSpecialization(Specialization specialization) =>
            addToList<Specialization>(DataSource.specList,specialization);

        /// <summary>
        /// Get all the banks in the database
        /// </summary>
        /// <returns>List of all the banks in the database</returns>
        public List<Bank> getAllBanks()
        {
            return DataSource.bankList;
        }

        /// <summary>
        /// Get all the contracts in the database
        /// </summary>
        /// <returns>List of all the contracts in the database</returns>
        public List<Contract> getAllContracts()
        {
            return DataSource.contractList;
        }

        /// <summary>
        /// Get all the employees in the database
        /// </summary>
        /// <returns>List of all the employees in the database</returns>
        public List<Employee> getAllEmployees()
        {
            return DataSource.employeeList;
        }

        /// <summary>
        /// Get all the employers in the database
        /// </summary>
        /// <returns>List of all the employers in the database</returns>
        public List<Employer> getAllEmployers()
        {
            return DataSource.employerList;
        }

        /// <summary>
        /// Get all the specializations in the database
        /// </summary>
        /// <returns>List of all the specializations in the database</returns>
        public List<Specialization> getAllSpecializations()
        {
            return DataSource.specList;
        }

        /// <summary>
        /// Removes a contract from the database
        /// </summary>
        /// <param name="contract">The contract to remove</param>
        public void removeContract(Contract contract) => RemoveItem<Contract>(DataSource.contractList, contract);

        /// <summary>
        /// Removes an employee from the database
        /// </summary>
        /// <param name="employee">The employee to remove</param>
        public void removeEmployee(Employee employee) => RemoveItem<Employee>(DataSource.employeeList, employee);

        /// <summary>
        /// Removes an employer from the database
        /// </summary>
        /// <param name="employer">The employer to remove</param>
        public void removeEmployer(Employer employer) => RemoveItem<Employer>(DataSource.employerList, employer);

        /// <summary>
        /// Removes a specialization from the database
        /// </summary>
        /// <param name="specialization">The specialization to remove</param>
        public void removeSpecialization(Specialization specialization) =>
            RemoveItem<Specialization>(DataSource.specList, specialization);

        /// <summary>
        /// Updates a contract in the database
        /// </summary>
        /// <param name="newContract">the new updated contract</param>
        /// <param name="oldContract">the old contract to update</param>
        public void updateContract(Contract newContract, Contract oldContract) => 
            UpdateItem<Contract>(DataSource.contractList, newContract, oldContract);

        /// <summary>
        /// Updates an employee in the database
        /// </summary>
        /// <param name="newEmployee">the new updated employee</param>
        /// <param name="oldEmployee">the old employee to update</param>
        public void updateEmployee(Employee newEmployee, Employee oldEmployee) => 
            UpdateItem<Employee>(DataSource.employeeList, newEmployee, oldEmployee);

        /// <summary>
        /// Updates an employer in the database
        /// </summary>
        /// <param name="newEmployer">the new updated employer</param>
        /// <param name="oldEmployer">the old employer to update</param>
        public void updateEmployer(Employer newEmployer, Employer oldEmployer) => 
            UpdateItem<Employer>(DataSource.employerList, newEmployer, oldEmployer);

        /// <summary>
        /// Updates a specizliation in the database
        /// </summary>
        /// <param name="newSpecialization">the new updated specialization</param>
        /// <param name="oldSpecialization">the old specialization to update</param>
        public void updateSpecialization(Specialization newSpecialization, Specialization oldSpecialization) => 
            UpdateItem<Specialization>(DataSource.specList, newSpecialization, oldSpecialization);
    }
}
