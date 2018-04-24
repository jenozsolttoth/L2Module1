using System;

namespace ServiceInterfaces
{
    public interface ICustomerParser
    {
        ICustomer ParseCustomer(string name, DateTime? registrationDate);
        bool CanParse(int type);
    }
}
