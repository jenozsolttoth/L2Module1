using System.Collections.Generic;
using System.Linq;
using L2Mentoring.Module1.Interfaces;
using L2Mentoring.Module1.States;
using ServiceInterfaces;

namespace L2Mentoring.Module1
{
    public class Runner:IRunner
    {
        private readonly ICustomerService _customerService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IArgsVerifier _argsVerifyer;
        private readonly IProductParser _productParser;
        private readonly IOrderBuilder _orderBuilder;

        public Runner(
            ICustomerService customerService, 
            IProductService productService, 
            IOrderService orderService, 
            IArgsVerifier argsVerifyer, 
            IProductParser productParser,
            IOrderBuilder orderBuilder
            )
        {
            _customerService = customerService;
            _productService = productService;
            _orderService = orderService;
            _argsVerifyer = argsVerifyer;
            _productParser = productParser;
            _orderBuilder = orderBuilder;
        }
        public ReturnState Startup(string[] args)
        {
            ReturnState argsVerification = _argsVerifyer.VerifyArgs(args);
            if (argsVerification == ReturnState.Ok)
            {
                var customerResponse = _customerService.GetCustomer(args[0]);
                ICustomer currentCustomer = customerResponse.Entity;

                var products = _productParser.ParseProducts(args[1]);
                var order = _orderBuilder.BuildOrder(args[1]);
                foreach (var product in products)
                {
                    var productResponse = _productService.GetProduct(product.ProductName);
                    if (productResponse.Success)
                    {
                        currentCustomer.AddToCart(productResponse.Entity, product.Quantity);
                    }
                }
                var orderResponse = _orderService.PlaceOrder(currentCustomer.Cart.GetProducts());
                List<IProduct> orderedProducts = new List<IProduct>();
                if (orderResponse.Success)
                {
                    orderedProducts = orderResponse.Entity.ToList();
                }
            }
            else
            {
                return argsVerification;
            }
            return ReturnState.Ready;
        }
    }
}
