using System;

namespace ServiceInterfaces
{
    public interface ICustomerEntity
    {
        string Name { get; }
        DateTime? RegistrationDate { get; }
        int Type { get; }
    }
}
