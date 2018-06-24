using System;
using NUnit.Framework;
using Moq;
using ServiceInterfaces;
using Services;
using DAL.Entities;
using Services.CustomerService.Customers;
using System.Collections.Generic;
using Services.CustomerService;

namespace Tests
{
    public class UnitTests
    {
        public void GetCustomer_Should_Get_Customer()
        {
            //ARRANGE
            var testCustomer = new Customer("Anderson", new DateTime(2015, 06, 01), 3);

            Mock<ICustomerRepository> customerRepository = new Mock<ICustomerRepository>();
            customerRepository
                .Setup(m => m.GetCustomer("Anderson"))
                .Returns(new GenericServiceResult<ICustomerEntity>(
                    new Customer("Anderson", new DateTime(2015, 06, 01), 3), true, "OK"));

            Mock<IYearCounter> yearCounter = new Mock<IYearCounter>();
            yearCounter.Setup(m => m.GetDifference(DateTime.Now, DateTime.Now)).Returns(3);

            Mock<IShoppingCart> shoppingCart = new Mock<IShoppingCart>();

            Mock<ICustomerParser> nonRegisteredCustomerParser = new Mock<ICustomerParser>();
            nonRegisteredCustomerParser.SetupGet(m => m.CanParseType).Returns(1);
            nonRegisteredCustomerParser.Setup(x => x.ParseCustomer(testCustomer.Name, testCustomer.RegistrationDate)).Returns(new NonRegisteredCustomer(testCustomer.Name, testCustomer.RegistrationDate, shoppingCart.Object));

            Mock<ICustomerParser> simpleCustomerParser = new Mock<ICustomerParser>();
            simpleCustomerParser.SetupGet(m => m.CanParseType).Returns(2);
            simpleCustomerParser.Setup(x => x.ParseCustomer(testCustomer.Name, testCustomer.RegistrationDate)).Returns(new SimpleCustomer(testCustomer.Name, testCustomer.RegistrationDate, yearCounter.Object, shoppingCart.Object));

            Mock<ICustomerParser> valuableCustomerParser = new Mock<ICustomerParser>();
            valuableCustomerParser.SetupGet(m => m.CanParseType).Returns(3);
            valuableCustomerParser.Setup(x => x.ParseCustomer(testCustomer.Name, testCustomer.RegistrationDate)).Returns(new ValuableCustomer(testCustomer.Name, testCustomer.RegistrationDate, yearCounter.Object, shoppingCart.Object));

            Mock<ICustomerParser> mostValuableCustomerParser = new Mock<ICustomerParser>();
            mostValuableCustomerParser.SetupGet(m => m.CanParseType).Returns(4);
            mostValuableCustomerParser.Setup(x=>x.ParseCustomer(testCustomer.Name, testCustomer.RegistrationDate)).Returns(new MostValuableCustomer(testCustomer.Name, testCustomer.RegistrationDate, yearCounter.Object, shoppingCart.Object));

            var parsers = new List<ICustomerParser>();
            parsers.Add(nonRegisteredCustomerParser.Object);
            parsers.Add(simpleCustomerParser.Object);
            parsers.Add(valuableCustomerParser.Object);
            parsers.Add(mostValuableCustomerParser.Object);

            //ACT
            var customerService = new CustomerService(customerRepository.Object, parsers);


            //ASSERT
            var customerServiceResponse = customerService.GetCustomer("Anderson");
            Assert.AreEqual(customerServiceResponse.Entity.GetType(), typeof(ValuableCustomer));
        }
    }
}
