using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Bank
    {
        public uint BankNumber { get; set; }
        public CivicAddress BranchAddress { get; set; }
        public string BankName { get; set; }
        public uint BranchNumber { get; set; }
        public uint AccountNumber { get; set; }

        public Bank(string bankName, uint bankNumber, uint branchNumber, CivicAddress branchAddress)
        {
            BankNumber = bankNumber;
            BankName = bankName;
            BranchNumber = branchNumber;
            BranchAddress = branchAddress;
        }

    }
}
