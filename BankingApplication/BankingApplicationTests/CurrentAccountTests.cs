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
    public class CurrentAccountTests
    {
        [TestMethod()]
        public void CurrentAccountWithdrawTest1()
        {
            //Test for withdrawal of the right amount.
            Account acc1 = new CurrentAccount("Adebayo Tunji", 7000);
            Assert.AreEqual(true, acc1.Withdraw(3000));
            Assert.AreEqual(3950, acc1.Balance);

        }

        [TestMethod()]
        
        public void CurrentAccounttWithdrawTest2()
        {
            //Test  for the withdral of a larger amount than the balance.
            Account acc1 = new CurrentAccount("Adebayo Tunji", 3000);
            Assert.AreEqual(false, acc1.Withdraw(6000));
            Assert.AreEqual(3000, acc1.Balance);

        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void CurrentAccountWithdrawTest3()
        {
            //Test for the withdrawal of a negative value.
            Account acc1 = new CurrentAccount("Adebayo Tunji", 4000);
            Assert.AreEqual(false, acc1.Withdraw(-6000));
            Assert.AreEqual(4000, acc1.Balance);


        }
        [TestMethod()]
        [ExpectedException(typeof(FormatException))]
        public void CurrentAccountWithdrawTest4()
        {   //Test for the widthral of an amount that is not a multiple of 50.
            Account acc1 = new CurrentAccount("Adebayo Tunji", 5000);
            Assert.AreEqual(false, acc1.Withdraw(3030));
            Assert.AreEqual(5000, acc1.Balance);


        }
        [TestMethod()]
        public void CurrentAccountDepositTest1()
        {   //Test for deposit of the right amount.
            Account acc1 = new SavingsAccount("Olusegun taiwo", 20000);
            Assert.AreEqual(true, acc1.Deposit(10000));
            Assert.AreEqual(30000, acc1.Balance);

        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void CurrentAccountDepositTest2()
        {   //Test for deposit of a negative amount.
            Account acc1 = new CurrentAccount("Olusegun taiwo", 1000);
            Assert.AreEqual(false, acc1.Deposit(-100));
            Assert.AreEqual(1000, acc1.Balance);

        }

        [TestMethod()]
        [ExpectedException(typeof(FormatException))]
        public void CurrentAccountDepositTest3()
        {   //Test for the deposit of an amount that is not a multiple of 50.
            Account acc1 = new CurrentAccount("Olusegun taiwo", 1000);
            Assert.AreEqual(false, acc1.Deposit(501));
            Assert.AreEqual(1000, acc1.Balance);
        }

        [TestMethod()]
        public void CurrentAccountTransferTest1()
        { // Transfer from a current account to a current account.
            Account acc1 = new CurrentAccount("Martin Luke", 6000);
            Account acc2 = new CurrentAccount("Judit love",8000);

            Assert.AreEqual(true, acc1.TransferTo(acc2, 2000));
            Assert.AreEqual(3950, acc1.Balance);
            Assert.AreEqual(10000, acc2.Balance);

        }

        [TestMethod()]
        public void CurrentAccountTransferTest2()
        { //Transfer from a current account to a savings account.
            Account acc1 = new CurrentAccount("Martin Luke", 6000);
            Account acc2 = new SavingsAccount("Judit love", 8000);

            Assert.AreEqual(true, acc1.TransferTo(acc2, 1000));
            Assert.AreEqual(4950, acc1.Balance);
            Assert.AreEqual(9000, acc2.Balance);

        }
    }
}