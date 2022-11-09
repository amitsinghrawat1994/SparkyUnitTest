using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    public class BankAccount
    {
        private readonly ILogBook _logBook;

        public int balance { get; set; }

        public BankAccount(ILogBook logBook)
        {
            balance= 0;
            _logBook = logBook;
        }

        public bool Deposit (int amount)
        {
            _logBook.Message("Deposit invoked");
            balance += amount;
            return true;
        }

        public bool Withdraw(int amount)
        {
            if(amount <= balance)
            {
                balance -= amount;
            }

            return true;    
        }

        public int GetBalance()
        {
            return balance; ;
        }
    }
}
