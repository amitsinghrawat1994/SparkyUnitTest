

using Sparky;
using Xunit;
using static Sparky.Customer;

namespace SparkyXUnit
{
    public class CustomerXUnitTest
    {
        private Customer customer;
       
        public  CustomerXUnitTest()
        {
            customer = new Customer();
        }

        [Fact]
        public void CombineName_InputFirstAndLastName_ReturnFullName()
        {
            customer.GreetAndCombineNames("Ben", "Spark");

            Assert.Equal("Hello, Ben Spark", customer.GreetMessage );
            Assert.Contains("ben Spark".ToLower() , customer.GreetMessage.ToLower());
            Assert.StartsWith("Hello,", customer.GreetMessage);
            Assert.EndsWith("Spark", customer.GreetMessage);
            Assert.Matches("Hello, [A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", customer.GreetMessage);
        }


        [Fact]
        public void GreetMessage_NotGreeted_ReturnNull()
        {
            // arrange

            // act

            // assert
            Assert.Null(customer.GreetMessage);
        }

        [Fact]
        public void DiscountCheck_DefaultCustomer_ReturnDiscountInRange()
        {
            int result = customer.Discount;

            Assert.InRange(result, 10, 25);
        }

        [Fact]
        public void GreetMessage_GreetedWithoutLastName_ReturnNotNull()
        {
            customer.GreetAndCombineNames("ben", "");

            Assert.NotNull(customer.GreetMessage);

            Assert.False(string.IsNullOrEmpty(customer.GreetMessage));
        }

        [Fact]
        public void GreetChecker_EmptyFirstName_ThrowsException()
        {
            var excetionDetails = Assert.Throws<ArgumentException>(() =>
            {
                customer.GreetAndCombineNames("", "Spark");
            });

            Assert.Equal("Empty First Name", excetionDetails.Message);

            //Assert.That(() => customer.GreetAndCombineNames("", "spark")
            //, Throws.ArgumentException.With.Message.EqualTo("Empty First Name")
            //    );

            Assert.Throws<ArgumentException>(() =>
            {
                customer.GreetAndCombineNames("", "Spark");
            });
        }

        [Fact]
        public void CustomerType_CreateCustomerwithLessThen100Order_ReturnBasicCustomer()
        {
            customer.OrderTotal = 10;

            var result = customer.GetCustomerDetails();

            Assert.IsType<BasicCustomer>(result);
        }

        [Fact]
        public void CustomerType_CreateCustomerwithMoreThen100Order_ReturnBasicCustomer()
        {
            customer.OrderTotal = 110;

            var result = customer.GetCustomerDetails();

            Assert.IsType<PlatiumCustomer>(result);
        }
    }
}
