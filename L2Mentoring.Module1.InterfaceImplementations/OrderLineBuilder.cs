using L2Mentoring.Module1.Interfaces;
using DAL.Entities;
using Services;

namespace L2Mentoring.Module1.InterfaceImplementations
{
    public class OrderLineBuilder : IOrderLineBuilder
    {
        public GenericServiceResult<OrderLine> BuildOrderLine(string productQuantity)
        {
            string[] productAndQuantity = productQuantity.Split(':');
            OrderLine orderLine = new OrderLine() { ProductName = productAndQuantity[0], Quantity = int.Parse(productAndQuantity[1]) };
            return new GenericServiceResult<OrderLine>(orderLine, true, "All good.");
        }
    }
}
