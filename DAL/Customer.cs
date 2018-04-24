using System;
using ServiceInterfaces;

namespace DAL
{
    public class Customer : ICustomerEntity
    {
        public Customer(string name, DateTime registrationDate, int type)
        {
            Name = name;
            RegistrationDate = registrationDate;
            Type = type;
        }
        public string Name { get; }
        public DateTime? RegistrationDate { get; }
        public int Type { get; }

    }
}
