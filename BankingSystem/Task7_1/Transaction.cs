using System;

namespace BankSystem
{
    abstract class Transaction
    {
        protected decimal _amount;
        protected bool _success;
        private bool _executed;
        private bool _reversed;
        private DateTime _dateStamp;

        public abstract bool Success
        {
            get;
        }
        public bool Executed
        {
            get => _executed;
        }
        public bool Reversed
        {
            get => _reversed;
        }
        public DateTime DateStamp
        {
            get => _dateStamp;
        }

        public Transaction(decimal amount)
        {
            _amount = amount;
        }

        public abstract void Print();

        public virtual void Execute()
        {
            _executed = true;
            _dateStamp = DateTime.Now;
        }

        public virtual void Rollback()
        {
            _reversed = true;
            _dateStamp = DateTime.Now;
        }
    }
}
