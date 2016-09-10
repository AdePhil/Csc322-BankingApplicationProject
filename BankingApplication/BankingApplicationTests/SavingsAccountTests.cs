using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankingApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.Tests
{
    [TestClass()]
    public class SavingsAccountTests
    {
        [TestMethod()]
        public void SavingsAccountWithdrawTest1()
        {
            //Test for withdrawal of the right amount.
            Account acc1 = new SavingsAccount("Remi martims", 5000);
            Assert.AreEqual(true, acc1.Withdraw(3000));
            Assert.AreEqual(2000, acc1.Balance);
            
        }

        [TestMethod()]

        public void SavingsAccountWithdrawTest2()
        {
            //Test  for the withdral of a larger amount greater than the balance.
            Account acc1 = new SavingsAccount("Remi martims", 5000);
            Assert.AreEqual(false, acc1.Withdraw(6000));
            Assert.AreEqual(5000, acc1.Balance);

        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void SavingsAccountWithdrawTest3()
        {
            //Test for the withdrawal of a negative value.
            Account acc1 = new SavingsAccount("Remi martims", 5000);
            Assert.AreEqual(false, acc1.Withdraw(-6000));
           

        }
        [TestMethod()]
        [ExpectedException(typeof(FormatException))]
        public void SavingsAccountWithdrawTest4()
        {   //Test for the widthral of an amount that is not a multiple of 50.
            Account acc1 = new SavingsAccount("Remi martims", 5000);
            Assert.AreEqual(false, acc1.Withdraw(3030));
            

        }
        [TestMethod()]
        public void SavingsAccountDepositTest1()
        {   //Test for deposit of the right amount.
            Account acc1 = new SavingsAccount("Samson Siasia", 20000);
            Assert.AreEqual(true, acc1.Deposit(10000));
            Assert.AreEqual(30000, acc1.Balance);

        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void SavingsAccountDepositTest2()
        {   //Test for deposit of a negative amount.
            Account acc1 = new SavingsAccount("Samson Siasia", 800);
            Assert.AreEqual(false, acc1.Deposit(-100));
            

        }

        [TestMethod()]
        [ExpectedException(typeof(FormatException))]
        public void SavingsAccountDepositTest3()
        {   //Test for the deposit of an amount that is not a multiple of 50.
            Account acc1 = new SavingsAccount("Samson Siasia", 800);
            Assert.AreEqual(false, acc1.Deposit(501));
        }

        [TestMethod()]
        public void SavingsAccountInterestRateTest()
        {   //Test for the addition of the interest rate.
            Account acc1 = new SavingsAccount("Alicia  keys", 1000,20);
            Assert.AreEqual(true, acc1.Deposit(400));
            Assert.AreEqual(true, acc1.Deposit(600));

            Assert.AreEqual(0,Decimal.Compare(2033.33m, acc1.Balance)); 
        }

        [TestMethod()]
        public void SavingsAccountTransferTest1()
        { //Transfer from a savings account to another savings accouunt.
            Account acc1 = new SavingsAccount("Jon justine", 6000);
            Account acc2 = new SavingsAccount("Bridget kelly", 8000);

            Assert.AreEqual(true, acc1.TransferTo(acc2, 2000));
            Assert.AreEqual(4000, acc1.Balance);
            Assert.AreEqual(10000, acc2.Balance);

        }

        [TestMethod()]
        public void SavingsAccountTransferTest2()
        { //Transfer from a savings account to a current account.
            Account acc1 = new SavingsAccount("Jon justine", 1000);
            Account acc2 = new CurrentAccount("Bridget kelly", 8000);

            Assert.AreEqual(true, acc1.TransferTo(acc2, 500));
            Assert.AreEqual(500, acc1.Balance);
            Assert.AreEqual(8500, acc2.Balance);

        }

    }
}