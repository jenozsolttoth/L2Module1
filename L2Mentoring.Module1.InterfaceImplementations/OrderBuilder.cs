using L2Mentoring.Module1.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;

namespace L2Mentoring.Module1.InterfaceImplementations
{
    public class OrderBuilder : IOrderBuilder
    {
        private readonly IOrderLineBuilder _orderLineBuilder;
        private readonly ILineSeparator _lineSeparator;
        public OrderBuilder(IOrderLineBuilder orderLineBuilder, ILineSeparator lineSeparator)
        {
            _orderLineBuilder = orderLineBuilder;
            _lineSeparator = lineSeparator;
        }

        public Order BuildOrder(string orderList)
        {
            string[] orderLines = _lineSeparator.Separate(orderList);
            Order order = new Order();
            foreach(var orderLine in orderLines)
            {
                OrderLine line = _orderLineBuilder.BuildOrderLine(orderLine);
                order.Lines.Add(line);
            }
            return order;
        }
    }
}
