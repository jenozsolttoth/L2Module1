using System.Collections.Generic;

namespace DAL.Entities
{
    public class Order
    {
        public Order()
        {
            Lines = new List<OrderLine>();
        }
        public List<OrderLine> Lines { get; set; }
    }
}
