using DAL.Entities;

namespace L2Mentoring.Module1.Interfaces
{
    public interface IOrderLineBuilder
    {
        OrderLine BuildOrderLine(string productQuantity);
    }
}
