using System;
using ServiceInterfaces;

namespace Services.ProductService
{
    public class ProductServiceMock : IProductService
    {
        private static readonly Random Rnd = new Random();
        public IServiceResult<IProduct> GetProduct(string name)
        {
            Product product = new Product(name, name, true, Rnd.Next(200, 20000));
            return new GenericServiceResult<IProduct>(product, true, "All good.");
        }
    }
}
