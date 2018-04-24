using System;
using System.Collections.Generic;
using System.Linq;
using ServiceInterfaces;

namespace L2Mentoring.Module1
{
    public class Runner:IRunner
    {
        private readonly ICustomerService _customerService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;

        public Runner(ICustomerService customerService, IProductService productService, IOrderService orderService)
        {
            _customerService = customerService;
            _productService = productService;
            _orderService = orderService;
        }
        public int Run(string[] args)
        {
            int argsVerification = VerifyArgs(args);
            if (argsVerification == 1)
            {
                var customerResponse = _customerService.GetCustomer(args[0]);
                ICustomer currentCustomer = customerResponse.Entity;

                var products = Helper.ParseProducts(args[1]);
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
            return 0;
        }

        private int VerifyArgs(string[] args)
        {
            string customerName = "";
            if ( args.Length > 0 )
            {
                customerName = args[0];
                if ( customerName == "help" )
                {
                    Console.WriteLine("L2Mentoring.Module1.exe usage:");
                    Console.WriteLine("\tL2Mentoring.Module1.exe \"Oleksandr Zhevzhyk\" \"ProductA:2;ProductB:1;ProductC:1\"");
                    Console.WriteLine("\tL2Mentoring.Module1.exe \"\" \"ProductA:1;ProductB:2\"");
                    return 0;
                }
            }
            else
            {
                Console.WriteLine("No parameters provided.");
                Console.WriteLine("Run L2Mentoring.Module1.exe help");
                return 4;
            }
            return 1;
        }
    }
}
