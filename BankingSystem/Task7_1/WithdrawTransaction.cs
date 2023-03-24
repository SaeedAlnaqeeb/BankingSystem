using System;

namespace BankSystem
{
    class WithdrawTransaction : Transaction
    {
        private Account _account;
        private bool _executed;
        private bool _seccess;
        private bool _reversed;

        public WithdrawTransaction(Account account, decimal amount)
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
                Console.WriteLine("Amount deducted: " + _amount);
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
            else if (_amount > _account.Balance)
            {
                throw new InvalidOperationException("Insufficient fund");
            }
            else
            {
                _account.Withdraw(_amount);
                _seccess = true;
                base._success = true;
            }
        }

        public override void Rollback()
        {
            if (_seccess == false)
            {
                throw new InvalidOperationException("Withdraw was not successful");
            }
            else if (_reversed == true)
            {
                throw new InvalidOperationException("Withdraw has already been reversed");
            }
            else if (_seccess == true && _reversed == false)
            {
                _account.Deposit(_amount);
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
