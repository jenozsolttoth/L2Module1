
namespace ServiceInterfaces
{
    public interface IProductService
    {
        IServiceResult<IProduct> GetProduct(string name);
    }
}
