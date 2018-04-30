using L2Mentoring.Module1.Interfaces;
using DAL.Entities;

namespace L2Mentoring.Module1.InterfaceImplementations
{
    public class OrderLineBuilder : IOrderLineBuilder
    {
        public OrderLine BuildOrderLine(string productQuantity)
        {
            string[] productAndQuantity = productQuantity.Split(':');
            return new OrderLine() { ProductName = productAndQuantity[0], Quantity = int.Parse(productAndQuantity[1]) };
        }
    }
}
