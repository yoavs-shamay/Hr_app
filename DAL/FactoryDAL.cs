using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FactoryDAL
    {
        private static Idal dal_instance;
        private FactoryDAL() { }
        static FactoryDAL()
        {
            dal_instance = new Dal_XML_imp();
        }
        public static Idal Dal_instance { get { return dal_instance; } }
    }
}
