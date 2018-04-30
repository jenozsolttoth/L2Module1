using System;
using System.Collections.Generic;
using ServiceInterfaces;
using System.Linq;

namespace Services.CustomerService
{
    public class CustomerServiceMock : ICustomerService
    {
        private readonly IEnumerable<ICustomerParser> _customerParsers;
        private const string CannotParseErrorMessage = "Cannot parse customer.";
        public static readonly Random Rnd = new Random();
        public CustomerServiceMock(IEnumerable<ICustomerParser> customerParsers)
        {
            _customerParsers = customerParsers;
        }
        public IServiceResult<ICustomer> GetCustomer(string name)
        {
            int type = Rnd.Next(1, 4);
            DateTime regdate = new DateTime(2014, 12, 6);
            ICustomerParser parser = _customerParsers.Where(x => x.CanParseType == type).SingleOrDefault();
            
            if(parser != null)
            {
                ICustomer customer = null;
                customer = parser.ParseCustomer(name, regdate);
                return new GenericServiceResult<ICustomer>(customer, true, "All good.");
            }
            return new GenericServiceResult<ICustomer>(null, false, CannotParseErrorMessage);
        }
    }
}
