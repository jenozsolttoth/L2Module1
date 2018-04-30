using System;
using ServiceInterfaces;
using Services.CustomerService.Customers;

namespace Services.CustomerService.CustomerParsers
{
    public class NonRegisteredCustomerParser : ICustomerParser
    {
        private readonly IYearCounter _yearCounter;
        private readonly IShoppingCart _shoppingCart;

        public int CanParseType { get; }

        public NonRegisteredCustomerParser(IYearCounter yearCounter, IShoppingCart shoppingCart)
        {
            _yearCounter = yearCounter;
            _shoppingCart = shoppingCart;
            CanParseType = (int) CustomerTypes.NonRegisteredCustomer;
        }

        public ICustomer ParseCustomer(string name, DateTime? registrationDate)
        {
            return new NonRegisteredCustomer(name, null, _shoppingCart);
        }
    }
}
