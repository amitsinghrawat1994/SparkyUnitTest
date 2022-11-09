using NUnit.Framework;
using Sparky;
using static Sparky.Customer;

namespace SparkyNUnitTest
{
    [TestFixture]
    public class CustomerNUnitTest
    {
        private Customer customer;
        [SetUp]
        public void Setup()
        {
            customer = new Customer();
        }

        [Test]
        public void CombineName_InputFirstAndLastName_ReturnFullName()
        {
            customer.GreetAndCombineNames("Ben", "Spark");

            //
            Assert.Multiple(() =>
            {
                Assert.AreEqual(customer.GreetMessage, "Hello, Ben Spark");
                Assert.That(customer.GreetMessage, Is.EqualTo("Hello, Ben Spark"));
                Assert.That(customer.GreetMessage, Does.Contain(","));
                Assert.That(customer.GreetMessage, Does.StartWith("Hello,"));
                Assert.That(customer.GreetMessage, Does.EndWith("Spark"));
                Assert.That(customer.GreetMessage, Does.Match("Hello, [A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+"));

            });
          }
       

        [Test]
        public void GreetMessage_NotGreeted_ReturnNull()
        {
            // arrange

            // act

            // assert
            Assert.IsNull(customer.GreetMessage);
        }

        [Test]
        public void DiscountCheck_DefaultCustomer_ReturnDiscountInRange()
        {
            int result = customer.Discount;

            Assert.That(result, Is.InRange(10, 25));
        }

        [Test]
        public void GreetMessage_GreetedWithoutLastName_ReturnNotNull()
        {
            customer.GreetAndCombineNames("ben", "");

            Assert.IsNotNull(customer.GreetMessage);

            Assert.IsFalse(string.IsNullOrEmpty(customer.GreetMessage));
        }

        [Test]
        public void GreetChecker_EmptyFirstName_ThrowsException()
        {
            var excetionDetails = Assert.Throws<ArgumentException>(() =>
            {
                customer.GreetAndCombineNames("", "Spark");
            });

            //Assert.AreEqual("Empty First Name", excetionDetails.Message);
            
            //Assert.That(() => customer.GreetAndCombineNames("", "spark")
            //, Throws.ArgumentException.With.Message.EqualTo("Empty First Name")
            //    );

            Assert.Throws<ArgumentException>(() =>
            {
                customer.GreetAndCombineNames("", "Spark");
            });

            Assert.That(() => customer.GreetAndCombineNames("", "spark")
          , Throws.ArgumentException
              );
        }

        [Test]
        public void CustomerType_CreateCustomerwithLessThen100Order_ReturnBasicCustomer ()
        {
            customer.OrderTotal = 10;

            var result = customer.GetCustomerDetails();

            Assert.That(result, Is.TypeOf<BasicCustomer>());
        }

        [Test]
        public void CustomerType_CreateCustomerwithMoreThen100Order_ReturnBasicCustomer()
        {
            customer.OrderTotal = 110;

            var result = customer.GetCustomerDetails();

            Assert.That(result, Is.TypeOf<PlatiumCustomer>());
        }
    }
}
