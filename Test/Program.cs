using BE;
using DAL;
using System;
using System.Text;

namespace Test
{
    class Program
    {
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
        static void Main(string[] args)
        {
            Dal_XML_imp XML_imp_object = new Dal_XML_imp();
            Dal_XML_imp.DownloadBanksXML();
            Bank bank = XML_imp_object.getAllBanks()[0];
            Console.WriteLine(bank.BankName + ";" + bank.BankNumber + ";" + bank.BranchAddress + ";" + bank.BranchNumber);
            Console.ReadLine();
        }
    }
}
