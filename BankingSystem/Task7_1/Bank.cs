using System;
using System.Collections.Generic;

namespace BankSystem
{
    class Bank
    {
        private List<Account> _accounts = new List<Account>();
        private List<Transaction> _transactions = new List<Transaction>();

        public Bank()
        {
        }

        public void AddAccount(Account account)
        {
            _accounts.Add(account);
        }

        public Account GetAccount(String name)
        {
            for (var i = 0; i < _accounts.Count; i++)
            {
                if (_accounts[i].Name == name)
                {
                    return _accounts[i];
                }
            }
            return null;
        }

        public void ExecuteTransaction(Transaction transaction)
        {
            transaction.Execute();
            _transactions.Add(transaction);
        }

        public void RollbackTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);
            transaction.Rollback();
        }

        public void PrintTransactionHistory()
        {
            Console.WriteLine();
            int num = 1;

            for (int i = 0; i < _transactions.Count; i++)
            {
                Console.WriteLine("Transaction number: " + num);

                _transactions[i].Print();

                Console.WriteLine("Time of the transaction: " + _transactions[i].DateStamp);
                Console.WriteLine();
                num += 1;
            }
        }

        public List<Transaction> GetList()
        {
            return _transactions;
        }
    }
}