using System;

namespace BankSystem
{
    class TransferTransaction : Transaction
    {
        private Account _fromAccount;
        private Account _toAccount;
        private DepositTransaction _deposit;
        private WithdrawTransaction _withdraw;
        private bool _executed;
        private bool _reversed;

        public TransferTransaction(Account fromAccount, Account toAccount, decimal amount)
            : base(amount)
        {
            _deposit = new DepositTransaction(toAccount, amount);
            _withdraw = new WithdrawTransaction(fromAccount, amount);
            _fromAccount = fromAccount;
            _toAccount = toAccount;
        }

        public override void Execute()
        {
            if (_amount <= 0)
            {
                throw new InvalidOperationException("Amount must be larger than 0");
            }
            else if (_amount > _fromAccount.Balance)
            {
                throw new InvalidOperationException("Insufficient fund");
            }
            else
            {
                _withdraw.Execute();

                if (_withdraw.Success == true)
                {
                    _deposit.Execute();
                    base.Execute();
                    _executed = true;
                    base._success = true;
                }
            }
        }

        public override void Rollback()
        {
            if (Success == false)
            {
                throw new InvalidOperationException("The original transaction was not successful");
            }
            else if (_reversed == true)
            {
                throw new InvalidOperationException("The transaction has been already reversed");
            }
            else if (Success == true)
            {
                if (_amount > _toAccount.Balance)
                {
                    throw new InvalidOperationException("Insufficient fund");
                }
                else
                {
                    try
                    {
                        _deposit.Rollback();
                        _withdraw.Rollback();
                        base.Rollback();
                        _reversed = true;
                    }
                    catch (InvalidOperationException exception)
                    {
                        Console.WriteLine("The following error detected: " + exception.GetType().ToString() + " with message \"" + exception.Message + "\"");
                    }
                }
            }
        }

        public override void Print()
        {
            Console.WriteLine("Transferred " + _amount + " from " + _fromAccount.Name
                + "'s account to " + _toAccount.Name + "'s account");

            _withdraw.Print();
            Console.WriteLine();
            _deposit.Print();
        }

        public override bool Success
        {
            get
            {
                if (_deposit.Success == true && _withdraw.Success == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
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
