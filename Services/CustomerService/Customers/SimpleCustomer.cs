using System;
using ServiceInterfaces;

namespace Services.CustomerService.Customers
{
    class SimpleCustomer : ICustomer
    {
        private readonly IYearCounter _yearCounter;
        public SimpleCustomer(string name, DateTime? registrationDate, IYearCounter yearCounter, IShoppingCart cart)
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
            int years = _yearCounter.GetDifference(RegistrationDate ?? DateTime.Now, DateTime.Now);
            decimal disc = (years > 5) ? ( decimal )5 / 100 : ( decimal )years / 100;
            return (amount - (0.1m * amount)) - disc * (amount - (0.1m * amount));
        }
    }
}
