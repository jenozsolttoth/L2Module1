using System;
using ServiceInterfaces;

namespace Services.CustomerService.Customers
{
    class NonRegisteredCustomer : ICustomer
    {
        private readonly IYearCounter _yearCounter;
        public NonRegisteredCustomer(string name, DateTime? registrationDate, IYearCounter yearCounter, IShoppingCart cart)
        {
            Name = name;
            RegistrationDate = registrationDate;
            _yearCounter = yearCounter;
            Cart = cart;
        }
        public string Name { get; }
        public DateTime? RegistrationDate { get; }
        public IShoppingCart Cart { get; }

        public void AddToCart(IProduct product, int quantity)
        {
            Cart.AddProduct(product, quantity);
        }

        public decimal CalculateDiscount()
        {
            decimal amount = Cart.SumProductPrice();
            return amount;
        }
    }
}
