using System.Collections.Generic;
using ServiceInterfaces;
using System.Linq;

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
                ICustomerParser parser = _customerParsers.Where(x => x.CanParseType == response.Entity.Type).SingleOrDefault();
                if (parser != null)
                {
                    ICustomer customer = null;
                    customer = parser.ParseCustomer(name, response.Entity.RegistrationDate);
                    return new GenericServiceResult<ICustomer>(customer, true, "All good.");
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
