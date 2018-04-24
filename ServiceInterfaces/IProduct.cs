
namespace ServiceInterfaces
{
    public interface IProduct
    {
        string Id { get; }
        string Name { get; }
        bool Found { get; }
        decimal Price { get; }
    }
}
