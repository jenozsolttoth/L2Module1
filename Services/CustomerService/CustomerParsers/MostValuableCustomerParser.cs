using System;
using ServiceInterfaces;
using Services.CustomerService.Customers;

namespace Services.CustomerService.CustomerParsers
{
    public class MostValuableCustomerParser : ICustomerParser
    {
        private readonly IYearCounter _yearCounter;
        private readonly IShoppingCart _shoppingCart;

        public int CanParseType { get; }

        public MostValuableCustomerParser(IYearCounter yearCounter, IShoppingCart shoppingCart)
        {
            _yearCounter = yearCounter;
            _shoppingCart = shoppingCart;
            CanParseType = (int) CustomerTypes.MostValuableCustomer;
        }

        public ICustomer ParseCustomer(string name, DateTime? registrationDate)
        {
            return new MostValuableCustomer(name, registrationDate, _yearCounter, _shoppingCart);
        }      
    }
}
