using DAL.Entities;
using Services;

namespace L2Mentoring.Module1.Interfaces
{
    public interface IOrderLineBuilder
    {
        GenericServiceResult<OrderLine> BuildOrderLine(string productQuantity);
    }
}
