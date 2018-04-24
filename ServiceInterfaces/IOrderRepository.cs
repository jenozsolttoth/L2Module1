using System.Collections.Generic;

namespace ServiceInterfaces
{
    public interface IOrderRepository
    {
        IServiceResult<IEnumerable<IProduct>> PlaceOrder(IEnumerable<IProduct> products);
    }
}
