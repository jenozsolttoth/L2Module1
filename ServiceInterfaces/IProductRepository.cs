
namespace ServiceInterfaces
{
    public interface IProductRepository
    {
        IServiceResult<IProduct> GetProduct(string productName);
    }
}
