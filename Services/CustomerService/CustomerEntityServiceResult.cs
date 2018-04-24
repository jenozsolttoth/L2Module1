using ServiceInterfaces;

namespace Services
{
    public class CustomerEntityServiceResult : IServiceResult<ICustomerEntity>
    {
        public CustomerEntityServiceResult(ICustomerEntity entity, bool success, string message)
        {
            Entity = entity;
            Success = success;
            Message = message;
        }
        public ICustomerEntity Entity { get; }
        public bool Success { get; }
        public string Message { get; }
    }
}
