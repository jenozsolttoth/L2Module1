using System.Collections.Generic;
using ServiceInterfaces;

namespace Services.OrderService
{
    public class OrderServiceResult : IServiceResult<IEnumerable<IProduct>>
    {
        public OrderServiceResult(IEnumerable<IProduct> entity, bool success, string message)
        {
            Entity = entity;
            Success = success;
            Message = message;
        }
        public IEnumerable<IProduct> Entity { get; }
        public bool Success { get; }
        public string Message { get; }
    }
}
