using ServiceInterfaces;

namespace Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public IServiceResult<IProduct> GetProduct(string name)
        {
            var response = _productRepository.GetProduct(name);
            if ( response.Success && response.Entity == null )
            {
                response = new GenericServiceResult<IProduct>(null, false, "There is no such a product.");
            }

            return response;
        }
    }
}
