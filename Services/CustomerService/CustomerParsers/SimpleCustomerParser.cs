﻿using System;
using ServiceInterfaces;
using Services.CustomerService.Customers;

namespace Services.CustomerService.CustomerParsers
{
    public class SimpleCustomerParser : ICustomerParser
    {
        private readonly IYearCounter _yearCounter;
        private readonly IShoppingCart _shoppingCart;

        public int CanParseType { get; }

        public SimpleCustomerParser(IYearCounter yearCounter, IShoppingCart shoppingCart)
        {
            _yearCounter = yearCounter;
            _shoppingCart = shoppingCart;
            CanParseType = (int) CustomerTypes.SimpleCostumer;
        }

        public ICustomer ParseCustomer(string name, DateTime? registrationDate)
        {
            return new SimpleCustomer(name, registrationDate, _yearCounter, _shoppingCart);
        }
    }
}
