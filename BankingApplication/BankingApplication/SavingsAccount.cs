using System;
using System.Diagnostics.Contracts;

namespace BankingApplication
{
    /// <summary>
    /// Savings Account Class.
    /// </summary>
    public class SavingsAccount : Account
    {

        private  double annualInterestRate;
        private int numberOfDeposits;
        private static int accCount = 1;

        /// <summary>
        ///Creates an account using the name supplied.
        /// </summary>
        /// <param name="name">The name of the account owner.</param>
        public SavingsAccount(string name)
            : base(name, 0.0M)
        {
            accCount++;
            annualInterestRate = 0.0;
        }
        /// <summary>
        /// Creates an account using the name and initial deposit supplied.
        /// </summary>
        /// <param name="name">The name of the account owner.</param>
        /// <param name="initialDeposit">The initial amount deposited into the account.</param>
        public SavingsAccount(string name, decimal initialDeposit)
            : base(name, initialDeposit)
        {
            accCount++;
            annualInterestRate = 0.0; //default interest rate.
        }
        /// <summary>
        /// Creates an account using the name, initial deposit and interest rate supplied.
        /// </summary>
        /// <param name="name">The name of the account owner.</param>
        /// <param name="initialDeposit">The initial amount deposited into the savings account.</param>
        /// <param name="interest">The interest rate for the savings account.</param>
        public SavingsAccount(string name, decimal initialDeposit, double interest)
             : base(name, initialDeposit)
        {
            accCount++;
            annualInterestRate = interest;

        }

        /// <summary>
        /// Implemented abstract method which returns the account type.
        /// </summary>
        public override string GetAccountType
        {
            get
            {
                return "Savings Account";
            }
        }
        /// <summary>
        /// Sets and returns the Annual Interest Rate.
        /// </summary>
        public double InterestRate
        {
            get
            {
                return annualInterestRate;
            }
            set
            {
                if (value >= 0)
                {
                    annualInterestRate = value;
                }

            }
        }

        /// <summary>
        /// Deposits an amount not less than 0 and is a multiple of 50 into an account.
        /// </summary>
        /// <param name="amount">The amount to be deposited.</param>
        /// <returns>Returns a boolean signalling the success or failure of the deposit operation.</returns>
        public override bool Deposit(decimal amount)
        {

            if (amount % 50 != 0 || amount < 0)
                return false;

            Balance += amount;
            numberOfDeposits++;
            if (numberOfDeposits % 2 == 0)
                addIterest();

            return true;


        }

        /// <summary>
        /// Withdraws an amount not less than 0 and is a mutiple of 50 from an account.
        /// </summary>
        /// <param name="amount">The amount to be withdrawn.</param>
        /// <returns>Returns a boolean signalling the success or failure of the withdraw operation.</returns>
        public override bool Withdraw(decimal amount)
         {
            
            if (canNotWithdraw(amount))
                return false;

            Balance -= amount;
            return true;
        }
        /// <summary>
        /// Checks if money can be withdrawn from the account.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns>Returns a boolean signalling if the money can be withderawn from the class.</returns>
        protected override bool canNotWithdraw(decimal amount)
        {
            return (amount < 0 || amount % 50 != 0 || amount > Balance);
        }
        private void addIterest()
        {
            double monthlyInterestRate = ((annualInterestRate * 0.01) / 12);
            balance += Decimal.Round((balance * (decimal)monthlyInterestRate),2);
        }
        /// <summary>
        /// Implemented abstract method which assigns an account number to each savings account.
        /// </summary>
        /// <returns>Returns an account number.</returns>
        protected override string generateAccountNumber()
        {
            return "0"+(100000000 + accCount).ToString();
        }
        /// <summary>
        /// Displays an account's details.
        /// </summary>
        /// <returns>The string representation of the savings account class.</returns>
        public override string ToString()
        {
            return base.ToString() + string.Format("\nAnnual Interest Rate: {0:N}%", annualInterestRate);
        }
    }
}
