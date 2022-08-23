namespace BrankTransfer
{
    public interface IBankAccount
    {
        void PayIn(decimal amount);
        bool Withdraw(decimal amount);
        decimal Balance
        {
            get;
        }
    }
    public interface ITransferBankAccount : IBankAccount
    {
        bool TransferTo(IBankAccount destination, decimal amount);
    }
}
_______________________________________________________________

using System;

namespace BrankTransfer
{
    // Oversimplified BankAccounts just to implement interfaces
    public class SaverAccount : IBankAccount
    {
        private decimal balance;
        public void PayIn(decimal amount)
        {
            balance += amount;
        }
        public bool Withdraw(decimal amount)
        {
            if (balance >= amount)
            {
                balance -= amount;
                return true;
            }
            Console.WriteLine("Withdrawal attempt failed.");
            return false;
        }
        public decimal Balance
        {
            get
            {
                return balance;
            }
        }
        public override string ToString()
        {
            return String.Format("Bank Saver: Balance = {0,6:C}", balance);
        }
    }
    public class CurrentAccount : ITransferBankAccount
    {
        private decimal balance;
        public void PayIn(decimal amount)
        {
            balance += amount;
        }
        public bool Withdraw(decimal amount)
        {
            if (balance >= amount)
            {
                balance -= amount;
                return true;
            }
            Console.WriteLine("Withdrawal attempt failed.");
            return false;
        }
        public decimal Balance
        {
            get
            {
                return balance;
            }
        }
        public bool TransferTo(IBankAccount destination, decimal amount)
        {
            bool result;
            if ((result = Withdraw(amount)) == true)
                destination.PayIn(amount);
            return result;
        }
        public override string ToString()
        {
            return String.Format("Bank Current Account: Balance = {0,6:C}", balance);
        }
    }

}
____________________________________________________________________
using System;

namespace BrankTransfer
{
    class Program
    {
        static void Main(string[] args)
        {
            IBankAccount sa = new SaverAccount();
            ITransferBankAccount ca = new CurrentAccount();
            sa.PayIn(200);
            ca.PayIn(500);

            Console.WriteLine("Before transfer operation:");
            Console.WriteLine(sa.ToString());
            Console.WriteLine(ca.ToString());

            ca.TransferTo(sa, 100);

            Console.WriteLine("After transfer operation:");
            Console.WriteLine(sa.ToString());
            Console.WriteLine(ca.ToString());

            Console.ReadLine();
        }
    }
}
