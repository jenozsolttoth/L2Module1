using System;

namespace ServiceInterfaces
{
    public interface ICustomer
    {
        
        string Name { get; }
        DateTime? RegistrationDate { get; }
        IShoppingCart Cart { get; }
        void AddToCart(IProduct product, int quantity);
        decimal CalculateDiscount();
    }
}
