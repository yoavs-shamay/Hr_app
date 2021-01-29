using BE;
using DAL;
using System;

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
            Specialization spec = XML_imp_object.getAllSpecializations()[0];
            Specialization newSpec = new Specialization();
            copyObject<Specialization>(spec, newSpec);
            newSpec.Id = "10001";
            newSpec.Name += "a";
            XML_imp_object.addSpecialization(newSpec);
            XML_imp_object.removeSpecialization(spec);
        }
    }
}
