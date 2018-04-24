using System;
using ServiceInterfaces;
using Services.CustomerService.Customers;

namespace Services.CustomerService.CustomerParsers
{
    public class ValuableCustomerParser:ICustomerParser
    {
        private readonly IYearCounter _yearCounter;
        private readonly IShoppingCart _shoppingCart;
        public ValuableCustomerParser(IYearCounter yearCounter, IShoppingCart shoppingCart)
        {
            _yearCounter = yearCounter;
            _shoppingCart = shoppingCart;
        }

        public ICustomer ParseCustomer(string name, DateTime? registrationDate)
        {
            return new ValuableCustomer(name, registrationDate, _yearCounter, _shoppingCart);
        }

        public bool CanParse(int type)
        {
            return (CustomerTypes)type == CustomerTypes.ValuableCustomer;
        }
    }
}
