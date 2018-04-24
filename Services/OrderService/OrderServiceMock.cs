using System;
using System.Collections.Generic;
using System.Linq;
using ServiceInterfaces;

namespace Services.OrderService
{
    public class OrderServiceMock : IOrderService
    {
        public IServiceResult<IEnumerable<IProduct>> PlaceOrder(List<Tuple<IProduct, int>> products)
        {
            return new OrderServiceResult(products.Select(x=>x.Item1).ToList(), true, "All good");
        }
    }
}
