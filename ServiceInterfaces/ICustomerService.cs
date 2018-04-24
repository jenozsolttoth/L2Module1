
namespace ServiceInterfaces
{
    public interface ICustomerService
    {
        IServiceResult<ICustomer> GetCustomer(string name);
    }
}
