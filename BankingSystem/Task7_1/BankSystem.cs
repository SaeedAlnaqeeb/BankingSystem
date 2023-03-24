using System;
using System.Collections.Generic;

namespace BankSystem
{
    public enum MenuOption
    {
        AddNewAccount,
        Withdraw,
        Deposit,
        Transfer,
        PrintAcc,
        PrintTrans,
        Quit
    }

    class BankSystem
    {
        static MenuOption ReadUserInput()
        {
            int option;

            do
            {
                Console.WriteLine("1: Add a new account");
                Console.WriteLine("2: Withdraw");
                Console.WriteLine("3: Deposit");
                Console.WriteLine("4: Transfer");
                Console.WriteLine("5: Print account");
                Console.WriteLine("6: Print transactions history");
                Console.WriteLine("7: Quit");

                Console.WriteLine("Choose an option");
                option = Convert.ToInt32(Console.ReadLine());

            } while (option < 1 || option > 7);

            Console.WriteLine("You chose: " + option);
            return (MenuOption)option;
        }

        static void DoWithdraw(Bank bank)
        {
            decimal amount;
            Account account = FindAccount(bank);
            
            if(account != null)
            {
                Console.WriteLine("How much do you want to withdraw?");
                amount = Convert.ToDecimal(Console.ReadLine());

                
                WithdrawTransaction newWithdraw = new WithdrawTransaction(account, amount);
                try
                {
                    bank.ExecuteTransaction(newWithdraw);
                    newWithdraw.Print();
                }
                catch (InvalidOperationException exception)
                {
                    Console.WriteLine("The following error detected: " + exception.GetType().ToString() + " with message \"" + exception.Message + "\"");
                }
            }
        }

        static void DoDeposit(Bank bank)
        {
            decimal amount;
            Account account = FindAccount(bank);

            if(account != null)
            {
                Console.WriteLine("How much do you want to deposit?");
                amount = Convert.ToDecimal(Console.ReadLine());

                DepositTransaction newDeposit = new DepositTransaction(account, amount);
                try
                {
                    bank.ExecuteTransaction(newDeposit);
                    newDeposit.Print();
                }
                catch (InvalidOperationException exception)
                {
                    Console.WriteLine("The following error detected: " + exception.GetType().ToString() + " with message \"" + exception.Message + "\"");
                }
            }
        }

        static void DoTransfer(Bank bank)
        {
            decimal amount;

            Console.WriteLine("Choose the sender account");
            Account from = FindAccount(bank);
            Console.WriteLine("Choose the receiever account");
            Account to = FindAccount(bank);

            if (from != null && to != null)
            {
                Console.WriteLine("How much do you want to transfer?");
                amount = Convert.ToDecimal(Console.ReadLine());

                TransferTransaction newTransfer = new TransferTransaction(from, to, amount);
                try
                {
                    bank.ExecuteTransaction(newTransfer);
                    newTransfer.Print();
                }
                catch (InvalidOperationException exception)
                {
                    Console.WriteLine("The following error detected: " + exception.GetType().ToString() + " with message \"" + exception.Message + "\"");
                }
            }
        }

        static void DoRollback(Bank bank)
        {
            bank.PrintTransactionHistory();
            var transactions = bank.GetList();

            Console.WriteLine("Do you want to rollback a specific transaction? | type yes or no");
            String answer = Console.ReadLine();
            
            if (answer == "yes")
            {
                Console.WriteLine("Which transaction do you want to rollback? | type its number");
                int trans = Convert.ToInt32(Console.ReadLine()) - 1;

                try
                {
                    bank.RollbackTransaction(transactions[trans]);
                    Console.WriteLine("Rollback was successful");
                }
                catch (InvalidOperationException exception)
                {
                    Console.WriteLine("The following error detected: " + exception.GetType().ToString() + " with message \"" + exception.Message + "\"");
                }
            }
        }

        static void DoPrint(Bank bank)
        {
            Account account = FindAccount(bank);

            if (account != null)
            {
                account.Print();
            }
        }

        private static Account FindAccount(Bank bank)
        {
            String name;

            Console.WriteLine("What is the name of the account?");
            name = Console.ReadLine();

            var acc = bank.GetAccount(name);

            if (acc == null)
            {
                Console.WriteLine("No matches found!");
            }
            return acc;
        }

        static void Main(string[] args)
        {
            Bank bankSystem = new Bank();
            int userOpt;
            String name;
            decimal balance;

            do
            {
                MenuOption opt = ReadUserInput();
                userOpt = (int)opt;

                switch (opt)
                {
                    case (MenuOption)1:
                        Console.WriteLine("What is the name?");
                        name = Console.ReadLine();
                        Console.WriteLine("What is the starting balance?");
                        balance = Convert.ToDecimal(Console.ReadLine());

                        Account newAccount = new Account(name, balance);
                        bankSystem.AddAccount(newAccount);
                        break;

                    case (MenuOption)2:
                        DoWithdraw(bankSystem);
                        break;

                    case (MenuOption)3:
                        DoDeposit(bankSystem);
                        break;

                    case (MenuOption)4:
                        DoTransfer(bankSystem);
                        break;

                    case (MenuOption)5:
                        DoPrint(bankSystem);
                        break;

                    case (MenuOption)6:
                        DoRollback(bankSystem);
                        break;

                    case (MenuOption)7:
                        Console.WriteLine("Out..");
                        break;
                }
                Console.WriteLine();
            } while (userOpt != 7);
        }
    }
}