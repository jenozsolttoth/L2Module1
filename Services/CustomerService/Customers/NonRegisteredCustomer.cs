using System;
using ServiceInterfaces;

namespace Services.CustomerService.Customers
{
    public class NonRegisteredCustomer : ICustomer
    {
        public NonRegisteredCustomer(string name, DateTime? registrationDate, IShoppingCart cart)
        {
            Name = name;
            RegistrationDate = registrationDate;
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
