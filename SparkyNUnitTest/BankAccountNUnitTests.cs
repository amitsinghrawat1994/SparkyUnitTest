using Moq;
using NUnit.Framework;
using Sparky;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkyNUnitTest
{
    [TestFixture]
    public class BankAccountNUnitTests
    {
        private BankAccount account;

        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void BankDepositLogFakker_Add100_ReturnTrue()
        {
            account = new(new LogFakker());

            var result = account.Deposit(100);

            Assert.IsTrue(result);
            Assert.That(account.GetBalance, Is.EqualTo(100));
        }

        [Test]
        public void BankDeposit_Add100_ReturnTrue()
        {
            var logMock = new Mock<ILogBook>();
            logMock.Setup(x => x.Message("Deposit invoked"));

            BankAccount bankAccount = new(logMock.Object);

            var result = bankAccount.Deposit(100);

            Assert.IsTrue(result);
            Assert.That(bankAccount.GetBalance, Is.EqualTo(100));
        }
    }
}
