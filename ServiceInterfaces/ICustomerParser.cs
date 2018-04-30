using System;

namespace ServiceInterfaces
{
    public interface ICustomerParser
    {
        ICustomer ParseCustomer(string name, DateTime? registrationDate);
        int CanParseType { get; }
    }
}
