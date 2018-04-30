
using DAL.Entities;

namespace L2Mentoring.Module1.Interfaces
{
    public interface IOrderBuilder
    {
        Order BuildOrder(string orderList);
    }
}
