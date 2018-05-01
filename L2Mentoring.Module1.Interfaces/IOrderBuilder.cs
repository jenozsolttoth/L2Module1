
using DAL.Entities;
using Services;

namespace L2Mentoring.Module1.Interfaces
{
    public interface IOrderBuilder
    {
        GenericServiceResult<Order> BuildOrder(string orderList);
    }
}
