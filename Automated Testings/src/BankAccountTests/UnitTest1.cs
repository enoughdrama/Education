using Microsoft.VisualStudio.TestTools.UnitTesting;
using nBankAccount;

namespace BankTests
{
    [TestClass]
    public class BankAccountTests
    {
        [TestMethod]
        public void Debit_WithValidAmount_UpdatesBalance()
        {
            double initalBalance = 100.00;
            double toBeDebitted = 4.55;
            double validExpectation = 95.45;
            BankAccount account = new BankAccount("Mrs. Simpson", initalBalance);

            account.Debit(toBeDebitted);

            double actual = account.Balance;
            Assert.AreEqual(validExpectation, actual, 0.001, "Account not debited correctly");
        }

        [TestMethod]
        public void Debit_WhenAmountIsLessThanZero()
        {
            double initalBalance = 100.00;
            double toBeDebitted = -100.00;
            BankAccount account = new BankAccount("Mrs. Simpson", initalBalance);

            try
            {
                account.Debit(toBeDebitted);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, BankAccount.DebitAmountLessThanZeroMessage);
                return;
            }

            Assert.Fail("The validExpectation exception was not thrown.");
        }

        [TestMethod]
        public void Debit_WhenAmountIsMoreThanBalance()
        {
            double initalBalance = 100.00;
            double toBeDebitted = 200.0;
            BankAccount account = new BankAccount("Ms. Abby", initalBalance);

            try
            {
                account.Debit(toBeDebitted);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, BankAccount.DebitAmountExceedsBalanceMessage);
                return;
            }

            Assert.Fail("The validExpectation exception was not thrown.");
        }

        [TestMethod]
        public void Credit_WithValidAmount_UpdatesBalance()
        {
            double initalBalance = 100.00;
            double creditAmount = 4.55;
            double validExpectation = 104.55;
            BankAccount account = new BankAccount("Mrs. Simpson", initalBalance);

            account.Credit(creditAmount);

            double actual = account.Balance;
            Assert.AreEqual(validExpectation, actual, 0.001, "Account not credited correctly");
        }

        [TestMethod]
        public void Credit_WhenAmountIsLessThanZero()
        {
            double initalBalance = 100.00;
            double creditAmount = -100.00;
            BankAccount account = new BankAccount("Mrs. Simpson", initalBalance);

            try
            {
                account.Credit(creditAmount);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, BankAccount.CreditAmountLessThanZeroMessage);
                return;
            }

            Assert.Fail("The validExpectation exception was not thrown.");
        }
    }
}