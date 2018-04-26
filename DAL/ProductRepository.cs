using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ServiceInterfaces;
using Services.ProductService;
using Product = DAL.Entities.Product;

namespace DAL
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConfigurationRepository _configurationRepository;

        public ProductRepository(IConfigurationRepository configurationRepository)
        {
            _configurationRepository = configurationRepository;
        }
        public IServiceResult<IProduct> GetProduct(string productName)
        {
            HttpCaller caller = new HttpCaller();
            Task<HttpResponseMessage> mess = caller.GetResponse(_configurationRepository.GetUrl("ProductEndpoint"));
            HttpResponseMessage message = mess.Result;
            IProduct product = JsonConvert.DeserializeObject<Product>(message.Content.ReadAsStringAsync().Result);
            IServiceResult<IProduct> result = new ProductServiceResult(product, message.IsSuccessStatusCode, message.StatusCode.ToString());
            return result;
        }
    }
}
