using System;
using System.Diagnostics.Contracts;

namespace BankingApplication
{
    /// <summary>
    /// A current account class.
    /// </summary>
    public class CurrentAccount : Account
    {
        private  static decimal fixedCharge = 50;
        private static int accCount = 1;
        /// <summary>
        /// Gets the  minimum balance of the current account class.
        /// </summary>
        public static int MinimumBalance { get; } = 1000;
        /// <summary>
        ///Gets and sets the fixed charge.
        /// </summary>
        public static decimal FixedCharge
        {
            get
            {
                return fixedCharge;
            }
            set
            {
                fixedCharge = value;
            }
        }

        

        /// <summary>
        ///Creates an account using the name and initial deposit supplied.
        ///Initial deposit must be greater than or equal to 1000.
        /// </summary>
        /// <param name="name">The name of the account owner.</param>
        /// <param name="initialDeposit">The initial amount to be deposited into the account.</param>
        public CurrentAccount(string name, decimal initialDeposit)
             : base(name, initialDeposit)
        {
            Contract.Requires<ArgumentException>(initialDeposit >= MinimumBalance);
            accCount++; //the number of current accounts created.
           
        }
       
        /// <summary>
        /// Implemented abstract method which returns the account type.
        /// </summary>
        public override string GetAccountType
        {
            get
            {
                return "Current Account"; //returns string Current Account.
            }
        }
        
        /// <summary>
        /// Deposits an amount not less than 0 and is a mutiple of 50 into an the account.
        /// </summary>
        /// <param name="amount">The amount to be deposited.</param>
        /// <returns>Returns a boolean signalling the success or failure of the deposit operation.</returns>
        public override bool Deposit(decimal amount)
        {

            if (amount < 0 && amount % 50 != 0)
                return false; // returns false if the deposit amount is negative and not a multiple of 50.
            Balance += amount;
            return true;
        }
        /// <summary>
        /// Withdraws an amount not less than the withdrawal limit and is a mutiple of 50 into an account.
        /// It also withdraws the fixed charge on the current account.
        /// </summary>
        /// <param name="amount">The amount to be withdrawn.</param>
        /// <returns>Returns a boolean signalling the success or failure of the withdraw operation.</returns>
        public override bool Withdraw(decimal amount)
        {
            decimal availableBalance = balance - MinimumBalance;

            if ((canNotWithdraw(amount)))
            {
                return false;
            }
           
                availableBalance -= amount;
                availableBalance -= fixedCharge;
                balance = availableBalance + MinimumBalance;
                return true;
            
        }
         /// <summary>
         /// Checks if money can be withdrawn from the account.
         /// </summary>
         /// <param name="amount">Amount to be withdrawn.</param>
         /// <returns>Returns a boolean signalling if the money can be withderawn from the account.</returns>
        protected override bool canNotWithdraw(decimal amount)
        {
            decimal availableBalance = balance - MinimumBalance;
            return (amount % 50 != 0) || amount + fixedCharge > availableBalance || amount < MinimumBalance;
        }
        /// <summary>
        /// Implemented abstract method which assigns an account number to each current account.
        /// </summary>
        /// <returns>Returns an account number.</returns>
        protected override string generateAccountNumber()
        {
            return "0"+(200000000 + accCount).ToString();
        }
        /// <summary>
        ///  Displays an account's details.
        /// </summary>
        /// <returns>Returns a string representation of the savings account class.</returns>
        public override string ToString()
        {
            return base.ToString()+string.Format("\nFixed Charge: #{0:N}",fixedCharge);
        }
        
        
    }
}
