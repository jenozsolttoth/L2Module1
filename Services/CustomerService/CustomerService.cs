using System.Collections.Generic;
using ServiceInterfaces;

namespace Services.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IEnumerable<ICustomerParser> _customerParsers;
        private const string CannotParseErrorMessage = "Cannot parse customer."; 
        public CustomerService(ICustomerRepository customerRepository, IEnumerable<ICustomerParser> customerParsers)
        {
            _customerRepository = customerRepository;
            _customerParsers = customerParsers;
        }
        public IServiceResult<ICustomer> GetCustomer(string name)
        {
            var response = _customerRepository.GetCustomer(name);
            if (response.Success)
            {
                foreach (var customerParser in _customerParsers)
                {
                    if (customerParser.CanParse(response.Entity.Type))
                    {
                        ICustomer customer = customerParser.ParseCustomer(response.Entity.Name, response.Entity.RegistrationDate);
                        return new GenericServiceResult<ICustomer>(customer, response.Success, response.Message);
                    }
                }
            }
            else
            {
                return new GenericServiceResult<ICustomer>(null, false, response.Message);
            }
            return new GenericServiceResult<ICustomer>(null, false, CannotParseErrorMessage);
        }
    }
}
