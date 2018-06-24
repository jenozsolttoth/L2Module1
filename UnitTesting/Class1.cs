using System;

namespace UnitTesting
{
    using System;
    using NUnit.Framework;
    using Moq;
    using ServiceInterfaces;
    using Services;
    using DAL.Entities;
    using Services.CustomerService.Customers;
    using System.Collections.Generic;
    using Services.CustomerService;
    using FsCheck;
    using System.Linq;
    using System.Collections;
    using L2Mentoring.Module1.InterfaceImplementations;
    using L2Mentoring.Module1.Interfaces;

    namespace Tests
    {
        [TestFixture]
        public class UnitTests
        {
            private static System.Random rnd = new System.Random();
            [Test]
            public void GetCustomer_Should_Return_Valuable_Customer_When_The_Customer_Is_A_Valuable_Customer()
            {
                //ARRANGE
                var testCustomer = new Customer("Anderson", new DateTime(2015, 06, 01), 3);

                var customerRepository = GetCustomerRepository(testCustomer);

                var yearCounter = GetYearCounter();

                var shoppingCart = GetShoppingCart();

                var parsers = new List<ICustomerParser>();

                parsers = GetParsers(testCustomer, shoppingCart.Object, yearCounter.Object);

                var customerService = new CustomerService(customerRepository.Object, parsers);

                //ACT
                var customerServiceResponse = customerService.GetCustomer(testCustomer.Name);


                //ASSERT
                Assert.AreEqual(true, customerServiceResponse.Success);
                Assert.AreEqual("All good.", customerServiceResponse.Message);
                Assert.AreEqual(typeof(ValuableCustomer), customerServiceResponse.Entity.GetType());
            }

            [Test]
            public void GetCustomer_Should_Return_Non_Registered_Customer_When_The_Customer_Is_A_Non_Registered_Customer()
            {
                //ARRANGE
                var testCustomer = new Customer("Anderson", new DateTime(2015, 06, 01), 1);

                var customerRepository = GetCustomerRepository(testCustomer);

                var yearCounter = GetYearCounter();

                var shoppingCart = GetShoppingCart();

                var parsers = new List<ICustomerParser>();

                parsers = GetParsers(testCustomer, shoppingCart.Object, yearCounter.Object);

                var customerService = new CustomerService(customerRepository.Object, parsers);

                //ACT
                var customerServiceResponse = customerService.GetCustomer(testCustomer.Name);

                //ASSERT
                Assert.AreEqual(true, customerServiceResponse.Success);
                Assert.AreEqual("All good.", customerServiceResponse.Message);
                Assert.AreEqual(typeof(NonRegisteredCustomer), customerServiceResponse.Entity.GetType());
            }

            [Test]
            public void GetCustomer_Should_Return_Simple_Customer_When_The_Customer_Is_A_Simple_Customer()
            {
                //ARRANGE
                var testCustomer = new Customer("Anderson", new DateTime(2015, 06, 01), 2);

                var customerRepository = GetCustomerRepository(testCustomer);

                var yearCounter = GetYearCounter();

                var shoppingCart = GetShoppingCart();

                var parsers = new List<ICustomerParser>();

                parsers = GetParsers(testCustomer, shoppingCart.Object, yearCounter.Object);

                var customerService = new CustomerService(customerRepository.Object, parsers);

                //ACT
                var customerServiceResponse = customerService.GetCustomer(testCustomer.Name);

                //ASSERT
                Assert.AreEqual(true, customerServiceResponse.Success);
                Assert.AreEqual("All good.", customerServiceResponse.Message);
                Assert.AreEqual(typeof(SimpleCustomer), customerServiceResponse.Entity.GetType());
            }

            [Test]
            public void GetCustomer_Should_Return_Most_Valuable_Customer_When_The_Customer_Is_A_Most_Valuable_Customer()
            {
                //ARRANGE
                var testCustomer = new Customer("Anderson", new DateTime(2015, 06, 01), 4);

                var customerRepository = GetCustomerRepository(testCustomer);

                var yearCounter = GetYearCounter();

                var shoppingCart = GetShoppingCart();

                var parsers = new List<ICustomerParser>();

                parsers = GetParsers(testCustomer, shoppingCart.Object, yearCounter.Object);

                var customerService = new CustomerService(customerRepository.Object, parsers);

                //ACT
                var customerServiceResponse = customerService.GetCustomer(testCustomer.Name);


                //ASSERT
                Assert.AreEqual(true, customerServiceResponse.Success);
                Assert.AreEqual("All good.", customerServiceResponse.Message);
                Assert.AreEqual(typeof(MostValuableCustomer), customerServiceResponse.Entity.GetType());
            }

            [Test]
            public void OrderLineBuilder_Should_Build_OrderLine_If_The_String_Passed_Is_In_The_Correct_Form()
            {
                //ARRANGE
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                const string nums = "0123456789";

                var numofnamelength = rnd.Next(20);
                var numofpieceslength = rnd.Next(1, 3);

                OrderLineBuilder orderLineBuilder = new OrderLineBuilder();

                var name = new string(Enumerable.Repeat(chars, numofnamelength).Select(s => s[rnd.Next(s.Length)]).ToArray());
                var quantity = new string(Enumerable.Repeat(nums, numofpieceslength).Select(s => s[rnd.Next(s.Length)]).ToArray());
                var teststring = $"{name}:{quantity}";

                //ACT
                var result = orderLineBuilder.BuildOrderLine(teststring);

                //ASSERT
                Assert.AreEqual("All good.", result.Message);
                Assert.AreEqual(true, result.Success);
                Assert.AreEqual(name, result.Entity.ProductName);
                Assert.AreEqual(Int32.Parse(quantity), result.Entity.Quantity);
            }

            [Test]
            public void LineSeparator_Should_Separate_The_Order_Lines_Correctly_If_The_String_Passed_Is_In_The_Correct_Form()
            {
                //ARRANGE
                ProductQuantityTestData testData = GenerateProductQuantityTestData();

                var teststring = testData.TestString;

                var LineSeparator = new LineSeparator();

                //ACT
                var result = LineSeparator.Separate(teststring);

                //ASSERT
                Assert.AreEqual(testData.Names.Count, result.Length);
                for(int i = 0; i < testData.Names.Count; i++)
                {
                    var subresult = result[i].Split(':');
                    Assert.AreEqual(testData.Names[i], subresult[0]);
                    Assert.AreEqual(testData.Quantities[i], int.Parse(subresult[1]));
                }
            }

            [Test]
            public void OrderBuilder_Should_Build_The_Order_Correctly_If_The_String_Passed_Is_In_The_Correct_Form_Unit()
            {
                //ARRANGE
                var testData = GenerateProductQuantityTestData();

                var teststring = testData.TestString;

                var names = testData.Names;
                var quantities = testData.Quantities;

                var orderLineSeparatorMock = GetLineSeparator();

                var orderLineBuilderMock = GetOrderLineBuilder();

                var orderBuilder = new OrderBuilder(orderLineBuilderMock.Object, orderLineSeparatorMock.Object);

                //ACT
                var result = orderBuilder.BuildOrder(teststring);

                //ASSERT
                Assert.AreEqual(true, result.Success);
                Assert.AreEqual("All good.", result.Message);
                Assert.AreEqual(names.Count, result.Entity.Lines.Count);
                for (int i = 0; i < result.Entity.Lines.Count; i++)
                {
                    Assert.AreEqual(result.Entity.Lines[i].ProductName, names[i]);
                    Assert.AreEqual(result.Entity.Lines[i].Quantity, quantities[i], $"ProductName: {result.Entity.Lines[i].ProductName} Should be: {names[i]}, Quantity: {result.Entity.Lines[i].Quantity} Should Be: {quantities[i]}");
                }
            }

            [Test]
            public void OrderBuilder_Should_Build_The_Order_Correctly_If_The_String_Passed_Is_In_The_Correct_Form_Component()
            {
                //ARRANGE
                var testData = GenerateProductQuantityTestData();

                var names = testData.Names;
                var quantities = testData.Quantities;

                string teststring = testData.TestString;

                var orderBuilder = new OrderBuilder(new OrderLineBuilder(), new LineSeparator());

                //ACT
                var result = orderBuilder.BuildOrder(teststring);

                //ASSERT
                Assert.AreEqual(true, result.Success);
                Assert.AreEqual("All good.", result.Message);
                Assert.AreEqual(names.Count, result.Entity.Lines.Count);
                for (int i = 0; i < result.Entity.Lines.Count; i++)
                {
                    Assert.AreEqual(result.Entity.Lines[i].ProductName, names[i]);
                    Assert.AreEqual(result.Entity.Lines[i].Quantity, quantities[i],
                        $"ProductName: {result.Entity.Lines[i].ProductName} Should be: {names[i]}, Quantity: {result.Entity.Lines[i].Quantity} Should Be: {quantities[i]}");
                }
            }

            Mock<ICustomerRepository> GetCustomerRepository(Customer customer)
            {
                Mock<ICustomerRepository> customerRepository = new Mock<ICustomerRepository>();
                customerRepository
                    .Setup(m => m.GetCustomer("Anderson"))
                    .Returns(new GenericServiceResult<ICustomerEntity>(
                        customer, true, "OK"));

                return customerRepository;
            }

            Mock<IYearCounter> GetYearCounter()
            {
                Mock<IYearCounter> yearCounter = new Mock<IYearCounter>();
                yearCounter.Setup(m => m.GetDifference(DateTime.Now, DateTime.Now)).Returns(3);
                return yearCounter;
            }

            Mock<IShoppingCart> GetShoppingCart()
            {
                Mock<IShoppingCart> shoppingCart = new Mock<IShoppingCart>();
                return shoppingCart;
            }

            List<ICustomerParser> GetParsers(Customer testCustomer, IShoppingCart shoppingCart, IYearCounter yearCounter)
            {
                Mock<ICustomerParser> nonRegisteredCustomerParser = new Mock<ICustomerParser>();
                nonRegisteredCustomerParser.SetupGet(m => m.CanParseType).Returns(1);
                nonRegisteredCustomerParser.Setup(x => x.ParseCustomer(testCustomer.Name, testCustomer.RegistrationDate)).Returns(new NonRegisteredCustomer(testCustomer.Name, testCustomer.RegistrationDate, shoppingCart));

                Mock<ICustomerParser> simpleCustomerParser = new Mock<ICustomerParser>();
                simpleCustomerParser.SetupGet(m => m.CanParseType).Returns(2);
                simpleCustomerParser.Setup(x => x.ParseCustomer(testCustomer.Name, testCustomer.RegistrationDate)).Returns(new SimpleCustomer(testCustomer.Name, testCustomer.RegistrationDate, yearCounter, shoppingCart));

                Mock<ICustomerParser> valuableCustomerParser = new Mock<ICustomerParser>();
                valuableCustomerParser.SetupGet(m => m.CanParseType).Returns(3);
                valuableCustomerParser.Setup(x => x.ParseCustomer(testCustomer.Name, testCustomer.RegistrationDate)).Returns(new ValuableCustomer(testCustomer.Name, testCustomer.RegistrationDate, yearCounter, shoppingCart));

                Mock<ICustomerParser> mostValuableCustomerParser = new Mock<ICustomerParser>();
                mostValuableCustomerParser.SetupGet(m => m.CanParseType).Returns(4);
                mostValuableCustomerParser.Setup(x => x.ParseCustomer(testCustomer.Name, testCustomer.RegistrationDate)).Returns(new MostValuableCustomer(testCustomer.Name, testCustomer.RegistrationDate, yearCounter, shoppingCart));

                var parsers = new List<ICustomerParser>();
                parsers.Add(nonRegisteredCustomerParser.Object);
                parsers.Add(simpleCustomerParser.Object);
                parsers.Add(valuableCustomerParser.Object);
                parsers.Add(mostValuableCustomerParser.Object);

                return parsers;
            }

            Mock<ILineSeparator> GetLineSeparator()
            {
                var orderLineSeparatorMock = new Mock<ILineSeparator>();
                orderLineSeparatorMock.Setup(m => m.Separate(It.IsAny<string>())).Returns<string>(a =>
                    {
                        return a.Split(';');
                    }
                );
                return orderLineSeparatorMock;
            }

            Mock<IOrderLineBuilder> GetOrderLineBuilder()
            {
                var orderLineBuilderMock = new Mock<IOrderLineBuilder>();
                orderLineBuilderMock.Setup(m => m.BuildOrderLine(It.IsAny<string>())).Returns<string>(a =>
                {
                    var orderLine = new OrderLine();
                    var productandquantity = a.Split(':');
                    orderLine.ProductName = productandquantity[0];
                    orderLine.Quantity = int.Parse(productandquantity[1]);
                    return new GenericServiceResult<OrderLine>(orderLine, true, "All good.");
                }
                );
                return orderLineBuilderMock;
            }

            ProductQuantityTestData GenerateProductQuantityTestData()
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                const string nums = "0123456789";

                var numofproducts = rnd.Next(300);
                var numofnamelength = rnd.Next(20);
                var numofpieceslength = rnd.Next(1, 3);
                var teststring = "";

                var names = new List<string>();
                var quantities = new List<int>();

                for (int i = 0; i < numofproducts; i++)
                {
                    string name = new string(Enumerable.Repeat(chars, numofnamelength).Select(s => s[rnd.Next(s.Length)]).ToArray());
                    names.Add(name);
                    string count = new string(Enumerable.Repeat(nums, numofpieceslength).Select(s => s[rnd.Next(s.Length)]).ToArray());
                    quantities.Add(Int32.Parse(count));
                    teststring = teststring + $"{name}:{count};";
                }
                teststring = teststring.TrimEnd(';');

                return new ProductQuantityTestData() { TestString = teststring, Names = names, Quantities = quantities };
            }
        }
        public class ProductQuantityTestData
        {
            public string TestString { get; set; }
            public List<string> Names { get; set; }
            public List<int> Quantities { get; set; }
        }
    }
}
