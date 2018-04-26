using ServiceInterfaces;

namespace DAL.Entities
{
    public class Product : IProduct
    {
        public Product(string id, string name, bool found, decimal price)
        {
            Id = id;
            Name = name;
            Found = found;
            Price = price;
        }
        public string Id { get; }
        public string Name { get; }
        public bool Found { get; }
        public decimal Price { get; }
    }
}
