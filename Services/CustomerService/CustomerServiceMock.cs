using System;
using System.Collections.Generic;
using ServiceInterfaces;

namespace Services.CustomerService
{
    public class CustomerServiceMock:ICustomerService
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
            foreach ( var a in _customerParsers )
            {
                if ( a.CanParse(type) )
                {
                    return new GenericServiceResult<ICustomer>(a.ParseCustomer(name, regdate), true, "All good."); 
                }
            }
            return new GenericServiceResult<ICustomer>(null, false, CannotParseErrorMessage);
        }
    }
}
