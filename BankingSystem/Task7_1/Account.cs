using System;

namespace BankSystem
{
    class Account
    {
        private String _name;
        private decimal _balance;

        public Account(String name, decimal balance)
        {
            this._name = name;
            this._balance = balance;
        }

        public bool Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                return false;
            }
            else
            {
                this._balance += amount;
                return true;
            }
        }

        public bool Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                return false;
            }
            else if (amount > this._balance)
            {
                return false;
            }
            else
            {
                this._balance -= amount;
                return true;
            }
        }

        public string Name
        {
            get => _name;
        }

        public decimal Balance
        {
            get => _balance;
        }

        public void Print()
        {
            Console.WriteLine("Account's name: " + Name);
            Console.WriteLine("Balance: " + this._balance);
        }
    }
}