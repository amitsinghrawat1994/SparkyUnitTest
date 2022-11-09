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
        public void BankDeposit_Add100_ReturnTrue()
        {
            var logMock = new Mock<ILogBook>();
            logMock.Setup(x => x.Message("Deposit invoked"));

            BankAccount bankAccount = new(logMock.Object);

            var result = bankAccount.Deposit(100);

            Assert.IsTrue(result);
            Assert.That(bankAccount.GetBalance, Is.EqualTo(100));
        }

        [Test]
        public void BankWithdraw_WithDraw100With200Balance_ReturnTrue ()
        {
            var logMock = new Mock<ILogBook>();
            logMock.Setup(u => u.LogToDb(It.IsAny<string>())).Returns(true);
            logMock.Setup(u => u.LogBalanceAfterWithdrawal(It.IsAny<int>())).Returns(true);

            BankAccount bankAccount = new(logMock.Object);
            bankAccount.Deposit(200);

            var result = bankAccount.Withdraw(100);
            Assert.IsTrue(result);
        }

        [Test]
        [TestCase(200, 100)]
        [TestCase(200, 150)]
        public void BankWithdraw_WithDraw100With200Balance_ReturnTrue2(int balance, int withdraw)
        {
            var logMock = new Mock<ILogBook>();
            logMock.Setup(u => u.LogToDb(It.IsAny<string>())).Returns(true);
            logMock.Setup(u => u.LogBalanceAfterWithdrawal(It.Is<int>(x => x > 0))).Returns(true);

            BankAccount bankAccount = new(logMock.Object);
            bankAccount.Deposit(balance);
            var result = bankAccount.Withdraw(withdraw);
            Assert.IsTrue(result);
        }


        // redo the code
        [Test]
        [TestCase(200, 300)]
        //[TestCase(200, 150)]
        public void BankWithdraw_WithDraw300With200Balance_ReturnFalse(int balance, int withdraw)
        {
            var logMock = new Mock<ILogBook>();
            //logMock.Setup(u => u.LogToDb(It.IsAny<string>())).Returns(true);
            logMock.Setup(u => u.LogBalanceAfterWithdrawal(It.Is<int>(x => x > 0))).Returns(true);
            //logMock.Setup(u => u.LogBalanceAfterWithdrawal(It.Is<int>(x => x < 0))).Returns(false);
            logMock.Setup(u => u.LogBalanceAfterWithdrawal(It.IsInRange<int>(int.MinValue, -1, Moq.Range.Inclusive))).Returns(false);

            BankAccount bankAccount = new(logMock.Object);
            bankAccount.Deposit(balance);
            var result = bankAccount.Withdraw(withdraw);
            Assert.IsFalse(result);
        }

        [Test]
        public void BankLogDummy_LogMockString_ReturnTrue()
        {
            var logMock = new Mock<ILogBook>();
            string desiredOutput = "hello1";

            logMock.Setup(u => u.MessageWithReturnStr(It.IsAny<string>())).Returns((string str) => str.ToLower());

            Assert.That(logMock.Object.MessageWithReturnStr("HEllo1"), Is.EqualTo(desiredOutput));
        }

        [Test]
        public void BankLogDummy_LogMockStringOutputStr_ReturnTrue()
        {
            var logMock = new Mock<ILogBook>();
            string desiredOutput = "hello";

            logMock.Setup(u => u.LogWithOutputResult(It.IsAny<string>(), out desiredOutput)).Returns(true);
            string result = "";
            Assert.IsTrue(logMock.Object.LogWithOutputResult("Ben", out result));
            Assert.That(result, Is.EqualTo(desiredOutput));
        }

        [Test]
        public void BankLogDummy_LogRefChecker_ReturnTrue()
        {
            var logMock = new Mock<ILogBook>();
            Customer customer = new();
            Customer customerNotUsed = new();

            logMock.Setup(u => u.LogWithRefObj(ref customer)).Returns(true);

            Assert.IsTrue(logMock.Object.LogWithRefObj(ref customer));
            Assert.IsFalse(logMock.Object.LogWithRefObj(ref customerNotUsed));
        }

        [Test]
        public void BankLogDummy_SetAndGetLogTypeAndSeverityMock_ReturnTrue()
        {
            var logMock = new Mock<ILogBook>();
            logMock.SetupAllProperties();
            logMock.Setup(u => u.LogSeverity).Returns(10);
            logMock.Setup(u => u.LogType).Returns("warning");

            logMock.Object.LogSeverity = 100;
            Assert.That(logMock.Object.LogSeverity, Is.EqualTo(10));
            Assert.That(logMock.Object.LogType, Is.EqualTo("warning"));


            // callbacks
            string logTemp = "Hello, ";
            logMock.Setup(u => u.LogToDb(It.IsAny<string>()))
                .Returns(true)
                .Callback((string str) => logTemp += str);

            logMock.Object.LogToDb("Ben");
            Assert.That(logTemp, Is.EqualTo("Hello, Ben"));

            // callbacks
            int counter = 5;
            logMock.Setup(u => u.LogToDb(It.IsAny<string>()))
                .Returns(true)
                .Callback(() => counter++);

            logMock.Object.LogToDb("Ben");
            logMock.Object.LogToDb("Ben");
            Assert.That(counter, Is.EqualTo(7));
        }
    }
}
