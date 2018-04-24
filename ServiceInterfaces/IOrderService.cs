using System;
using System.Collections.Generic;

namespace ServiceInterfaces
{
    public interface IOrderService
    {
        IServiceResult<IEnumerable<IProduct>> PlaceOrder(List<Tuple<IProduct,int>> products);
    }
}
