using System.Net.Http;
using System.Threading.Tasks;
using DAL.Entities;
using Newtonsoft.Json;
using ServiceInterfaces;
using Services.CustomerService;

namespace DAL
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IConfigurationRepository _configurationRepository;

        public CustomerRepository(IConfigurationRepository configurationRepository)
        {
            _configurationRepository = configurationRepository;
        }
        public IServiceResult<ICustomerEntity> GetCustomer(string customerName)
        {
            HttpCaller caller = new HttpCaller();
            Task<HttpResponseMessage> mess = caller.GetResponse(_configurationRepository.GetUrl("CustomerEndpoint") + customerName);
            mess.Start();
            HttpResponseMessage message = mess.Result;
            Customer customer = JsonConvert.DeserializeObject<Customer>(message.Content.ReadAsStringAsync().Result);
            IServiceResult<ICustomerEntity> result = new CustomerEntityServiceResult(customer, message.IsSuccessStatusCode, message.StatusCode.ToString());
            return result;
        }
    }
}
