using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication
{/// <summary>
/// The account database.
/// </summary>
    public class AccountDatabase
    {
        private static readonly Dictionary<string, Account>  db = new Dictionary<string, Account>();
        static AccountDatabase mydb = null;
        private AccountDatabase()
        {
            var acc1 = new SavingsAccount("Rasheed Salvador", 500,25);
            var acc2 = new SavingsAccount("Ade Samuel", 5000);
            var acc3 = new SavingsAccount("Mafe Kayode", 10000, 20);
            var acc4 = new CurrentAccount("Mathew mark", 3000);
            var acc5 = new CurrentAccount("Sultan Mubarak", 5000);

            db.Add(acc1.AccountNumber, acc1);
            db.Add(acc2.AccountNumber, acc2);
            db.Add(acc3.AccountNumber, acc3);
            db.Add(acc4.AccountNumber, acc4);
            db.Add(acc5.AccountNumber, acc5);
        }
        /// <summary>
        /// Returns an instance of the AcountDatabase class.
        /// </summary>
        /// <returns></returns>
        public static AccountDatabase GetInstance()
        {
            if (mydb == null)
            {
                mydb = new AccountDatabase();
            }

            return mydb;
        }/// <summary>
        /// Adds an account to the account database.
        /// </summary>
        /// <param name="acc">Account to be added</param>
        public  void AddAccount(Account acc)
        {
            db.Add(acc.AccountNumber, acc);
        }
        /// <summary>
        /// Returns the account with a unique accountNumber.
        /// </summary>
        /// <param name="accountNumber">Account number of tthe account.</param>
        /// <returns></returns>
        public  Account GetAccount(string accountNumber)
        {
            return db[accountNumber];
        }
        /// <summary>
        /// Checks if the account with specified account number is in the database.
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns>Returns a boolean signalling the success or failure of the IsAccountInDb method </returns>
        public bool IsAccountInDb(string accountNumber)
        {
            return db.ContainsKey(accountNumber);
        }
        /// <summary>
        /// Converts the Account database to an array and returns the array.
        /// </summary>
        /// <returns>Returns  an array of the accounts in the database.</returns>
        public Account[] GetAccounts()
        {
            return db.Values.ToArray();
        }
        /// <summary>
        /// Deletes an account from the database.
        /// </summary>
        /// <param name="accountNumber">Account number of the account to be deleted from the database.</param>
        public void RemoveAccount(string accountNumber)
        {
            if (IsAccountInDb(accountNumber))
                db.Remove(accountNumber);
        }

    }
}
