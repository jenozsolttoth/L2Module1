
using ServiceInterfaces;
using Services;
using System.Collections.Generic;

namespace L2Mentoring.Module1.Interfaces
{
    public interface ICustomerAttendant
    {
        GenericServiceResult<IEnumerable<IProduct>> AttendCustomer(ICustomer customer, string productList);
    }
}
