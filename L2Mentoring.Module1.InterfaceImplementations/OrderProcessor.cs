using L2Mentoring.Module1.Interfaces;
using System;
using DAL.Entities;
using ServiceInterfaces;
using Services;
using System.Collections.Generic;

namespace L2Mentoring.Module1.InterfaceImplementations
{
    public class OrderProcessor : IOrderProcessor
    {
        private readonly IProductService _productService;
        public OrderProcessor(IProductService productService)
        {
            _productService = productService;
        }
        public GenericServiceResult<OrderResult> ProcessOrder(ICustomer customer, Order order)
        {
            OrderResult result = new OrderResult();
            List<IProduct> FoundProducts = new List<IProduct>();
            List<string> NotFoundProducts = new List<string>();
            foreach (var line in order.Lines)
            {
                var serviceResult = _productService.GetProduct(line.ProductName);
                if(serviceResult.Success)
                {
                    FoundProducts.Add(serviceResult.Entity);
                    customer.AddToCart(serviceResult.Entity, line.Quantity);
                }
                else
                {
                    NotFoundProducts.Add(line.ProductName);
                }
            }
            result.FoundProducts = FoundProducts;
            result.NotFoundProducts = NotFoundProducts;
            return new GenericServiceResult<OrderResult>(result,true,"All good.");
        }
    }
}
