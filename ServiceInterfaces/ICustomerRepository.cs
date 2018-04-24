
namespace ServiceInterfaces
{
    public interface ICustomerRepository
    {
        IServiceResult<ICustomerEntity> GetCustomer(string customerName);
    }
}
