using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

        public double GetPrice(Customer customer)
        {
            if(customer.IsPlatimun)
            {
                return Price * .8;
            }

            return Price;
        }

        public double GetPrice(ICustomer customer)
        {
            if (customer.IsPlatimun)
            {
                return Price * .8;
            }

            return Price;
        }
    }
}
