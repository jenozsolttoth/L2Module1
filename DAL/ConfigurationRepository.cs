using System.Collections.Generic;
using ServiceInterfaces;

namespace DAL
{
    public class ConfigurationRepository:IConfigurationRepository
    {
        private readonly Dictionary<string, string> _endpoints;

        public ConfigurationRepository()
        {
            _endpoints = new Dictionary<string, string>();
            _endpoints.Add("CustomerEndpoint", "http://server-endpoint/customers/");
            _endpoints.Add("ProductEndpoint", "http://server-endpoint/products/");
            _endpoints.Add("OrderEndpoint", "http://server-endpoint/orders");
        }
        public string GetUrl(string endpointName)
        {
            return _endpoints[endpointName];
        }
    }
}
