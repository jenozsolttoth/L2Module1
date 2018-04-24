using ServiceInterfaces;

namespace Services
{
    public class CustomerServiceResult : IServiceResult<ICustomer>
    {
        public CustomerServiceResult(ICustomer entity, bool success, string message)
        {
            Entity = entity;
            Success = success;
            Message = message;
        }
        public ICustomer Entity { get; }
        public bool Success { get; }
        public string Message { get; }
    }
}
