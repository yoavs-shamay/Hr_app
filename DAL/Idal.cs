using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public interface Idal
    {
        public void addSpecialization(Specialization specialization);
        public void addEmployee(Employee employee);
        public void addEmployer(Employer employer);
        public void addContract(Contract contract);

        public void removeSpecialization(Specialization specialization);
        public void removeEmployee(Employee employee);
        public void removeEmployer(Employer employer);
        public void removeContract(Contract contract);

        public void updateSpecialization(Specialization newSpecialization, Specialization oldSpecialization);
        public void updateEmployee(Employee newEmployee, Employee oldEmployee);
        public void updateEmployer(Employer newEmployer, Employer oldEmployer);
        public void updateContract(Contract newContract, Contract oldContract);

        public List<Specialization> getAllSpecializations();
        public List<Employee> getAllEmployees();
        public List<Employer> getAllEmployers();
        public List<Contract> getAllContracts();

        public List<Bank> getAllBanks();
    }
}
