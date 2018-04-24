using System;
using ServiceInterfaces;
using Services.CustomerService.Customers;

namespace Services.CustomerService.CustomerParsers
{
    public class NonRegisteredCustomerParser:ICustomerParser
    {
        private readonly IYearCounter _yearCounter;
        private readonly IShoppingCart _shoppingCart;
        public NonRegisteredCustomerParser(IYearCounter yearCounter, IShoppingCart shoppingCart)
        {
            _yearCounter = yearCounter;
            _shoppingCart = shoppingCart;
        }

        public ICustomer ParseCustomer(string name, DateTime? registrationDate)
        {
            return new NonRegisteredCustomer(name, null, _yearCounter, _shoppingCart);
        }

        public bool CanParse(int type)
        {
            return ( CustomerTypes )type == CustomerTypes.NonRegisteredCustomer;
        }
    }
}
