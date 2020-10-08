using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    class FactoryBL
    {
        private static IBL bl_instance;

        //To public default constructor will not create
        private FactoryBL() {}

        //Runs on program compliation
        static FactoryBL()
        {
            bl_instance = new BL_imp();
        }

        //property to get the private bl_instance
        public static IBL BL_instance { get { return BL_instance; } }
    }
}
