//using Moq;

//namespace SparkyXUnit
//{
//    [TestFixture]
//    public class ProductXUnitTests
//    {
//        [Test]
//        public void GetProductPrice_PlatinumCustomer_ReturnPriceWith20Discount()
//        {
//            Product product = new Product() { Price = 50 };

//            var result = product.GetPrice(new Customer()
//            {
//                IsPlatimun = true
//            });

//            Assert.That(result, Is.EqualTo(40));
//        }

//        [Test]
//        public void GetProductPriceMOQAbuse_PlatinumCustomer_ReturnPriceWith20Discount()
//        {
//            var customer = new Mock<ICustomer>();
//            customer.Setup(u => u.IsPlatimun).Returns(true);

//            Product product = new Product() { Price = 50 };

//            var result = product.GetPrice(customer.Object);

//            Assert.That(result, Is.EqualTo(40));
//        }
//    }
//}
