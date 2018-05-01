using System.Collections.Generic;
using L2Mentoring.Module1.Interfaces;
using ServiceInterfaces;
using Services;

namespace L2Mentoring.Module1.InterfaceImplementations
{
    public class CustomerAttendant : ICustomerAttendant
    {
        private readonly IOrderBuilder _orderBuilder;
        private readonly IOrderProcessor _orderProcessor;
        private readonly IOrderService _orderService;
        public CustomerAttendant(
            IOrderBuilder orderBuilder,
            IOrderProcessor orderProcessor,
            IOrderService orderService)
        {
            _orderBuilder = orderBuilder;
            _orderProcessor = orderProcessor;
            _orderService = orderService;
        }
        public GenericServiceResult<IEnumerable<IProduct>> AttendCustomer(ICustomer customer, string productList)
        {
            var order = _orderBuilder.BuildOrder(productList);
            var processedOrder = _orderProcessor.ProcessOrder(customer, order.Entity);
            var orderResponse = _orderService.PlaceOrder(customer.Cart.GetProducts());
            if (orderResponse.Success)
            {
                return new GenericServiceResult<IEnumerable<IProduct>>(orderResponse.Entity, true, "All good.");
            }
            return new GenericServiceResult<IEnumerable<IProduct>>(null, false, "All bad.");
        }
    }
}
