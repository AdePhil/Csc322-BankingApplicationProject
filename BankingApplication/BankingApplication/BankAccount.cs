using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication
{
    class BankAccount
    {
        //Main menu options.
        enum MainMenu { Quit,NewAccount, ManageAccount , ViewAllAccts}
        //Account menu options.
        enum AccountTypeOptions { Back, Savings, Current }
        //Manage account options 
        enum ManageAccountOptions { Back,Withdraw,Deposit,Tranfer}
        //confirmation dailog options
        enum Confirmation { No, Yes }

        //Main menu
        public static void Menu()
        {
            int choice;
            bool justContinue;
            do
            {
                Console.WriteLine("\nMain menu\n1: Create new account\n2: Manage Account\n3: View Accounts\n0: Quit\n");
                Console.Write("Enter a choice: ");
                justContinue = (int.TryParse(Console.ReadLine(), out choice));
                Console.WriteLine();
                Console.Clear();

                if (choice < 0 || choice > 3 || !justContinue)
                    Console.WriteLine("Invalid Input!");

            } while (choice < 0 || choice > 3   || !justContinue);

            switch ((MainMenu)choice)
            {
                case MainMenu.Quit:
                    Console.WriteLine("Exiting...");
                    return;
                case MainMenu.NewAccount:
                    CreateAccount();
                    break;
                case MainMenu.ManageAccount:
                    ManageAccount();
                    Menu();
                    break;
                case MainMenu.ViewAllAccts:
                    ViewAllAccounts();
                    Menu();
                    break;
            }
        }
        //create an account
        private static void CreateAccount()
        {
            int choice;
            bool justContinue;
            do
            {
                Console.WriteLine("Select Account Type\n1: Savings Account\n2: Current Account\n0: Back\n");
                Console.Write("Enter a choice: ");
                justContinue = (int.TryParse(Console.ReadLine(), out choice));
                Console.WriteLine();
                Console.Clear();
                if (choice < 0 || choice > 2 || !justContinue)
                    Console.WriteLine("Invalid Input!");

            } while (choice < 0 || choice > 2 || !justContinue);

            switch ((AccountTypeOptions)choice)
            {
                case AccountTypeOptions.Back:
                    Menu();
                    return;
                case AccountTypeOptions.Savings:
                    CreateSavingsAccount();
                    Menu();
                    break;
                case AccountTypeOptions.Current:
                    CreateCurrentAccount();
                    Menu();
                    break;
            }

        }

       
        //create a savings account.
        private static void CreateSavingsAccount()
        {
            Console.Write("Enter your Name[-1 to cancel]: ");
            string accountName = Console.ReadLine();
            if (accountName == "-1")
                return;


            int defaulBalancechoice = ConfirmationMenu("Do you want to deposit now?");
            
            bool isNumber;
            double rate;
            decimal amount;
            SavingsAccount theAccount;

            switch ((Confirmation)defaulBalancechoice)
            {
                case Confirmation.No:
                    theAccount = new SavingsAccount(accountName);
                    AccountDatabase.GetInstance().AddAccount(theAccount);
                    Console.WriteLine("Account has been created\n");
                    Console.WriteLine(theAccount);
                    break;

                case Confirmation.Yes:
                    string input;

                    do
                    {
                        Console.Write("Enter an amount[-1]: ");
                        input = Console.ReadLine();
                        if (input == "-1")
                            return;
                        isNumber = Decimal.TryParse(input, out amount);

                        if (!isNumber)
                            Console.WriteLine("Invalid amount, must be a number.");
                        if (amount % 50 != 0)
                            Console.WriteLine("Invalid amount, must be a multiple of 50.");
                        if (amount <= 0)
                            Console.WriteLine("Invalid amount, must not a negative value.");

                        Console.WriteLine();
                    }
                    while (!isNumber || amount % 50 != 0 || amount <= 0);
                   

                    int addInterestratechoice = ConfirmationMenu("Do you want to add InterestRate? ");
                    if (addInterestratechoice == -1)
                        return;

                    switch ((Confirmation)addInterestratechoice)
                    {
                        case Confirmation.No:
                                theAccount = new SavingsAccount(accountName, amount);
                                AccountDatabase.GetInstance().AddAccount(theAccount);
                                Console.WriteLine("\nAccount has been created");
                                Console.WriteLine(theAccount);
                            break;
                        case Confirmation.Yes:
                            do
                            {
                                Console.Write("Enter Interest Rate[-1 to cancel]: ");
                                string ratestring = Console.ReadLine();
                                if (ratestring == "-1")
                                    return;
                                isNumber = Double.TryParse(ratestring, out rate);

                                if(rate < 0)
                                Console.WriteLine("Invalid interest rate.");
                                Console.WriteLine();
                                
                            }
                            while (rate < 0);

                            theAccount = new SavingsAccount(accountName, amount, rate);
                            AccountDatabase.GetInstance().AddAccount(theAccount);
                            Console.WriteLine("\nAccount has been created.");
                            Console.WriteLine(theAccount);
                            break;
                    }
                    break;


            }

        }

       
        //create a current account
        private static void CreateCurrentAccount()
        {
            Console.Write("Enter your Name[-1 to cancel]: ");
            string accountName = Console.ReadLine();

            if (accountName == "-1") return;


            bool isNumber;
            decimal amount;
            CurrentAccount theAccount;
            string input;
         

            do
            {
                Console.Write("Enter an amount to deposit{1000 minimum}[-1 to cancel]: ");
                input = Console.ReadLine();
                if (input == "-1")
                    return;
                isNumber = Decimal.TryParse(input, out amount);

                if (!isNumber)
                    Console.WriteLine("Invalid amount, Amount must be a Number.");

                if (amount % 50 != 0)
                    Console.WriteLine("Invalid amount, must be a multiple of 50.");

                if (amount < 1000)
                    Console.WriteLine("Minimum balance must be 1000.");
                Console.WriteLine();

                
            }
            while (!isNumber || amount % 50 != 0 || amount < 1000);
           
            theAccount = new CurrentAccount(accountName, amount);
            AccountDatabase.GetInstance().AddAccount(theAccount);
            Console.WriteLine("\nAccount has been created");
            Console.WriteLine(theAccount);
            


        }
        //Ask a yes or no question
        private static int ConfirmationMenu(string message)
        {
            int choice;
            bool justContinue;
            string input;
            do
            {
                Console.WriteLine(message + "\n1: Yes\n0: No\n");
                Console.Write("Enter a choice[-1 to cancel]: ");
                input = Console.ReadLine();
                if (input == "-1")
                    return -1;
                justContinue = (int.TryParse(input, out choice));
                Console.WriteLine();
                Console.Clear();

                if (choice < 0 || choice > 1 || !justContinue)
                    Console.WriteLine("Invalid Input!");

            } while (choice < 0 || choice > 1 || !justContinue);

            return choice;
        }

        //Display all accounts
        private static void ViewAllAccounts()
        {
            var i = 0;
            foreach (var account in AccountDatabase.GetInstance().GetAccounts())
            {
                Console.WriteLine("{0}. {1}\n", i++, account);
            }
        }
        //manage accounts
        private static void ManageAccount()
        {
            Console.Write("Enter account number[-1 to cancel]: ");
            string accountNumber = Console.ReadLine();
            if (accountNumber == "-1") return;

            if (AccountDatabase.GetInstance().IsAccountInDb(accountNumber))
            {
                Console.WriteLine("Successful! Login");
                Console.WriteLine();
                Console.WriteLine(AccountDatabase.GetInstance().GetAccount(accountNumber));

            }
            else
            {
                Console.WriteLine("Invalid account number!");
                Menu();
            }

            int choice;
            bool justContinue;
            do
            {
                Console.WriteLine("\nSelect Operation\n1: Withdraw\n2: Deposit\n3: Transfer\n0: Back\n");
                Console.Write("Enter a choice: ");
                justContinue = (int.TryParse(Console.ReadLine(), out choice));
                Console.WriteLine();

                if (choice < 0 || choice > 3 || !justContinue)
                    Console.WriteLine("Invalid Input!");

            } while (choice < 0 || choice > 3 || !justContinue);

            switch ((ManageAccountOptions)choice)
            {
                case ManageAccountOptions.Back:
                    Menu();
                    return;
                case ManageAccountOptions.Withdraw:
                    Withdraw(AccountDatabase.GetInstance().GetAccount(accountNumber));
                    break;
                case ManageAccountOptions.Deposit:
                    Deposit(AccountDatabase.GetInstance().GetAccount(accountNumber));
                    break;
                case ManageAccountOptions.Tranfer:
                    Transfer(AccountDatabase.GetInstance().GetAccount(accountNumber));
                    break;
            }
        }
        //transfer to another account
        private static void Transfer(Account fromAcc)
        {
            Console.Write("Enter account number to transfer to[-1 to cancel]: ");
            string accountNumber = Console.ReadLine();
            if (accountNumber == "-1") return;

            if (AccountDatabase.GetInstance().IsAccountInDb(accountNumber))
            {
                Console.WriteLine("Valid Account Number!");
                Console.WriteLine();

            }
            else
            {
                Console.WriteLine("Invalid account number!");
                Menu();
            }

            Account toAccount = AccountDatabase.GetInstance().GetAccount(accountNumber);
            Console.Write("Enter amount to Transfer[-1 to cancel]: ");
            string amount = Console.ReadLine();
            if (amount == "-1") return;

            Decimal realAmount;
            if (Decimal.TryParse(amount, out realAmount) && realAmount % 50 == 0)
            {
                int confirm = ConfirmationMenu("Do you want to Transfer now?");
                switch ((Confirmation)confirm)
                {
                    case Confirmation.No:
                        return;
                    case Confirmation.Yes:
                        if (!fromAcc.TransferTo(toAccount,realAmount))
                        {
                            Console.WriteLine("Could not transfer!, Insufficient Funds.");
                        }
                        Console.WriteLine(fromAcc);
                        Menu();
                        break;
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid amount!, must be a multiple of 50 and must be a number.");
                Console.WriteLine();
                ManageAccount();
            }
        }
        //deposit to the the account
        private static void Deposit(Account acc)
        {
            Console.Write("Enter amount to deposit[-1 to cancel]: ");
            string amount = Console.ReadLine();
            if (amount == "-1") return;

            Decimal realAmount;
            if (Decimal.TryParse(amount, out realAmount) && realAmount % 50 == 0)
            {
                int confirm = ConfirmationMenu("Do you want to deposit now?");
                switch ((Confirmation)confirm)
                {
                    case Confirmation.No:
                        return;
                    case Confirmation.Yes:
                        if (!acc.Deposit(realAmount))
                        {
                            Console.WriteLine("Could not deposit!, Insufficient Funds.");
                        }
                        Console.WriteLine(acc);
                        Menu();
                        break;
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid amount!, must be a multiple of 50 and must be a number.");
                ManageAccount();
            }
        }

        //withdraw from the account
        private static void Withdraw(Account acc)
        {
            Console.Write("Enter amount to withdraw[-1 to cancel]: ");
            string amount = Console.ReadLine();
            if (amount == "-1") return;

            Decimal realAmount;
            if (Decimal.TryParse(amount, out realAmount) && realAmount % 50 == 0)
            {
                int confirm = ConfirmationMenu("Do you want to withdraw now?");
                if (confirm == -1)
                    return;
                switch ((Confirmation)confirm)
                {
                    case Confirmation.No:
                        return;
                    case Confirmation.Yes:
                        if (!acc.Withdraw(realAmount))
                        {
                            Console.WriteLine("Insufficient funds!");
                        }
                        Console.WriteLine(acc);
                        Menu();
                        break;
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid amount!, must be a multiple of 50 and must be a number.");
                ManageAccount();
            }


        }
    }
}
        

        
