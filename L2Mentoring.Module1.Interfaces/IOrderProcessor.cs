
using DAL.Entities;
using ServiceInterfaces;
using Services;

namespace L2Mentoring.Module1.Interfaces
{
    public interface IOrderProcessor
    {
        GenericServiceResult<OrderResult> ProcessOrder(ICustomer customer, Order order);
    }
}
