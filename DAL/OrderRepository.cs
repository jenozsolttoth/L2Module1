using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ServiceInterfaces;
using Services.OrderService;

namespace DAL
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IConfigurationRepository _configurationRepository;

        public OrderRepository(IConfigurationRepository configurationRepository)
        {
            _configurationRepository = configurationRepository;
        }
        public IServiceResult<IEnumerable<IProduct>> PlaceOrder(IEnumerable<IProduct> products)
        {
            HttpCaller caller = new HttpCaller();
            Task<HttpResponseMessage> mess = caller.GetResponse(_configurationRepository.GetUrl("OrderEndpoint"));
            HttpResponseMessage message = mess.Result;
            if (message.IsSuccessStatusCode)
            {
                return new OrderServiceResult(products, true, "All good.");
            }
            return new OrderServiceResult(null,false, "Error.");
        }
    }
}
