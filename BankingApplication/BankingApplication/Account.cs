using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading;
using System.Globalization;


namespace BankingApplication
{
    /// <summary>
    /// An abstract account class. 
    /// </summary>
    public abstract class Account
    {
        private string name;
        private string accountNumber;
        /// <summary>
        /// Account balance.
        /// </summary>
        protected decimal balance;
    

        /// <summary>
        /// A Balance property that can only be set from derived classes.
        /// </summary>
        public decimal Balance
        {
            get
            {
                return this.balance;
            }
            protected set
            {
                this.balance = value;
            }
        }
        
        /// <summary>
        /// Returns the Account Number of the instance of the account.
        /// </summary>
        public string AccountNumber
        {
            get
            {
                return accountNumber;
            }

        }
        /// <summary>
        /// Returns the name of the account owner.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }

        }
        /// <summary>
        /// An abstract property for getting the account type
       ///  which is to be implemented in the sub classs.
        /// </summary>
        public abstract string GetAccountType { get; }
        /// <summary>
        ///Creates an account using the name and initial deposit supplied.
        /// </summary>
        /// <param name="name">The name of the account owner.</param>
        /// <param name="balance">The initial amount to be deposited to the  account.</param>
        public Account(string name, decimal balance)
        {
            this.name = name;
            this.balance = balance;
            accountNumber = generateAccountNumber(); // generates an account number and assigns
                                                     // it to the accountNumber field.
        }/// <summary>
         ///Creates an account using the name supplied.
         /// </summary>
         /// <param name="name">The name of the account owner.</param>
        public Account(string name)
            : this(name, 0.0M)
        {
            accountNumber = generateAccountNumber();

        }

        /// <summary>
        ///  Withdraws an amount not less than 0 and is a mutiple of 50 from an account.
        /// </summary>
        /// <param name="amount">The amount to be withdrawn.</param>
        /// <returns>A boolean signalling the success or failure of the withdraw method.</returns>
        public virtual  bool Withdraw(decimal amount)
        {
            Contract.Requires<FormatException>(amount % 50 == 0, "Invalid Deposit amount");
            Contract.Requires<ArgumentException>(amount > 0, "Invalid withdrawal amount");
            

            if (amount < 0 && amount % 50 != 0)
                return false;

            Balance -= amount;
            return true;
        }
        /// <summary>
        /// Deposits an amount not less than 0 and is a multiple of 50 into the account.
        /// </summary>
        /// <param name="amount">The amount to be deposited into the account.</param>
        /// <returns>Returns a boolean signalling the failure or success of the deposit method.</returns>
        public virtual bool Deposit(decimal amount)
        {
            Contract.Requires<FormatException>(amount % 50 == 0, "Invalid Deposit amount");
            Contract.Requires<ArgumentException>(amount > 0, "Insuffient balance!");
            if (amount > 0)
                return false;

            Balance += amount;
            return true;
        }
        /// <summary>
         /// Transfers money from this account to the account specified.
         /// (Account must not be an instance of it's self.)
         /// </summary>
         /// <param name="toAccount">Account to transfer to.</param>
         /// <param name="amount">Amount to deposit.</param>
         /// <returns>Returns a boolean signalling the failure or success of the transfer method.</returns>
        public virtual bool TransferTo(Account toAccount, decimal amount)
        {
            Contract.Requires(!this.Equals(toAccount), "Cannot transfer to same account");

            if (this.canNotWithdraw(amount) && !this.Equals(toAccount))
                return false;
            this.Withdraw(amount);
            toAccount.Deposit(amount);
            return true;
        }
        /// <summary>
        /// Checks if you can withdraw from the account.
        /// </summary>
        /// <param name="amount">amount to be withdrawn.</param>
        /// <returns>Returns a boolean signalling the success or failure of the canNotWithdraw method.</returns>
        protected virtual bool canNotWithdraw(decimal amount)
        {
            return amount > 0;
        }
        /// <summary>
        /// An abstarct method for generating the account number of a particular type of account. 
        /// </summary>
        /// <returns></returns>
        protected abstract string generateAccountNumber();
        /// <summary>
        /// Display's account's details.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
           
            return string.Format("Name: {0}\nAcount Number: {1}\nBalance: #{2:N}", name, accountNumber, balance);
        }
        
    }
}
