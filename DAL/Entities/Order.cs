using System.Collections.Generic;

namespace DAL.Entities
{
    public class Order
    {
        public List<OrderLine> Lines { get; set; }
    }
}
