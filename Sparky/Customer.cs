using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sparky.Customer;

namespace Sparky
{
    public interface ICustomer
    {
         int Discount { get; set; }

         int OrderTotal { get; set; }

         string GreetMessage { get; set; }

         bool IsPlatimun { get; set; }

         string GreetAndCombineNames(string firstName, string lastName);

        CustomerType GetCustomerDetails();
    }

        public class Customer: ICustomer
    {
        public int Discount { get; set; }

        public int OrderTotal { get; set; }

        public string GreetMessage { get; set; }

        public bool IsPlatimun { get; set; }
        int ICustomer.Discount { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Customer()
        {
            Discount = 15;
            IsPlatimun= false;
        }

        public string GreetAndCombineNames(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("Empty First Name");
            }

            GreetMessage = $"Hello, {firstName} {lastName}";
            Discount = 20;
            return GreetMessage;
        }

        public CustomerType GetCustomerDetails ()
        {
            if(OrderTotal < 100)
            {
                return new BasicCustomer();
            }

            return new PlatiumCustomer();
        }

        public class CustomerType { }

        public class BasicCustomer : CustomerType { }

        public class PlatiumCustomer : CustomerType { }
    }
}
