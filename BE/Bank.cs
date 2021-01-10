using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Bank : INotifyPropertyChanged
    {
        private uint accountNumber;
        public uint AccountNumber { get { return accountNumber; }
            set { accountNumber = value; propertyChanged("AccountNumber"); } }
        private uint branchNumber;
        public uint BranchNumber { get { return branchNumber; }
            set { branchNumber = value; propertyChanged("BranchNumber"); } }
        private string bankName;
        public string BankName { get { return bankName; }
            set { bankName = value;propertyChanged("BankName"); } }
        private CivicAddress branchAddress;
        public CivicAddress BranchAddress { get { return branchAddress; }
            set { branchAddress = value; propertyChanged("BranchAddress"); } }
        private uint bankNumber;
        public uint BankNumber { get { return bankNumber; }
            set { bankNumber = value; propertyChanged("BankNumber"); } }
        public Bank() { }
        public Bank(string bankName, uint bankNumber, uint branchNumber, CivicAddress branchAddress)
        {
            BankNumber = bankNumber;
            BankName = bankName;
            BranchNumber = branchNumber;
            BranchAddress = branchAddress;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void propertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public override bool Equals(object obj)
        {
            Bank other = (Bank)obj;
            return other != null && (AccountNumber == other.AccountNumber) && (BranchNumber == other.BranchNumber) && (BankNumber == other.BankNumber);
        }

        public Bank(Bank other)
        {
            accountNumber = other.accountNumber;
            branchNumber = other.branchNumber;
            bankName = other.bankName;
            branchAddress = other.branchAddress;
            bankNumber = other.bankNumber;
        }
    }
}
