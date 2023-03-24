using System;

namespace BankSystem
{
    class DepositTransaction : Transaction
    {
        private Account _account;
        private bool _executed;
        private bool _seccess;
        private bool _reversed;

        public DepositTransaction(Account account, decimal amount)
            : base(amount)
        {
            _account = account;
        }

        public override void Print()
        {
            Console.WriteLine("Execution: " + _executed);
            Console.WriteLine("Success: " + _seccess);
            Console.WriteLine("Reversion: " + _reversed);

            if (_seccess == true)
            {
                Console.WriteLine("Amount deposited: " + _amount);
            }
        }

        public override void Execute()
        {
            base.Execute();
            _executed = true;

            if (_amount <= 0)
            {
                throw new InvalidOperationException("Amount must be larger than 0");
            }
            else
            {
                _account.Deposit(_amount);
                _seccess = true;
                base._success = true;
            }
        }

        public override void Rollback()
        {
            if (_seccess == false)
            {
                throw new InvalidOperationException("Deposit was not successful");
            }
            else if (_reversed == true)
            {
                throw new InvalidOperationException("Deposit has already been reversed");
            }
            else if (_seccess == true && _reversed == false)
            {
                _account.Withdraw(_amount);
                base.Rollback();
                _reversed = true;
            }
        }

        public override bool Success
        {
            get => _seccess;
        }

        public new bool Executed
        {
            get => base.Executed;
        }

        public new bool Reversed
        {
            get => base.Reversed;
        }
    }
}
