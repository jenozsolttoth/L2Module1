using ServiceInterfaces;

namespace Services.ProductService
{
    public class ProductServiceResult : IServiceResult<IProduct>
    {
        public IProduct Entity { get; }
        public bool Success { get; }
        public string Message { get; }

        public ProductServiceResult(IProduct entity, bool success, string message)
        {
            Entity = entity;
            Success = success;
            Message = message;
        }
    }
}
