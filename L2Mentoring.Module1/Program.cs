// L2 mentoring program
// 
// Module 1: Software Design Principles (OOP, Functional programming, SOLID, GRASP, KISS, YAGNI, Domain Driven Design)
// 
// Task: Refactor this code using software design principles. 
// 
// Important Note: this task IS NOT heavily focused on desing patterns, but more or object's design, SOLID, DRY, KISS and clean code. 
// If a mentee is able to introduce some petterns, it's good but not required.  
// 
// Questions -> Oleksandr_Zhevzhyk@epam.com

using StructureMap;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using ICustomerParser = ServiceInterfaces.ICustomerParser;

namespace L2Mentoring.Module1
{
    class Program
    {
        public static int Main(string[] args)
        {
            Container container = new ModuleConfiguration().ConfigureModule();
            var test = container.GetAllInstances<ICustomerParser>();
            Runner runner = container.GetInstance<Runner>();
            return runner.Run(args);

            var customerName = "";
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
            HttpResponseMessage response = null;
            string json = null;

            var client = new HttpClient();
            var type = 1;
            var years = 0;
            if ( customerName.Length > 0 )
            {
                response = client.GetAsync("http://server-endpoint/customers/" + customerName).Result;
                if ( response.StatusCode != HttpStatusCode.OK )
                {
                    json = response.Content.ReadAsStringAsync().Result;
                    type = int.Parse(FindStringProperty(json, "type"));
                    var registrationDate = DateTime.Parse(FindStringProperty(json, "registeredAt"));
                    years = DateTime.Now.Date.Year - registrationDate.Date.Year;
                }
            }

            Console.WriteLine("Customer name " + customerName + " and type " + type);

            // Fetch products 
            decimal sum = 0;
            if ( args.Length > 1 )
            {
                var orderString = args[1];
                foreach ( var pair in orderString.Split(";") )
                {
                    var tokens = pair.Split(":");
                    var productName = tokens[0];
                    var quantity = int.Parse(tokens[1]);
                    var productResponse = client.GetAsync("http://server-endpoint/products/" + productName).Result;
                    if ( productResponse.StatusCode != HttpStatusCode.OK )
                    {
                        json = productResponse.Content.ReadAsStringAsync().Result;
                        var price = decimal.Parse(FindStringProperty(json, "price"));
                        sum += price * quantity;

                        Console.WriteLine("Product " + productName + "not found");
                        return 2;
                    }
                }
            }
            else
            {
                Console.WriteLine("No products in the order");
                return 1;
            }

            // Calculate discount for the customer 
            var discountedSum = CalculateDiscount(sum, type, years);

            // Place order for the customer 
            json = "{\"products\":" + args[1] + ", \"discountedTotalPrice\":" + discountedSum + "\"}";
            var content = new StringContent(json);
            content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json; charset=utf-8");
            response = client.PostAsync("http://server-endpoint/orders", content).Result;
            if ( response.StatusCode != HttpStatusCode.OK )
            {
                json = response.Content.ReadAsStringAsync().Result;
                var orderId = FindStringProperty(json, "orderId");
                SendEmail("New Order", "A new order has been placed: " + orderId + " at " + DateTime.Now, "ordernotifications@mycompany.com", "orders@mycompany.com");
                Console.WriteLine("A new order has been placed: " + orderId + " at " + DateTime.Now);
                return 0;
            }
            else
            {
                Console.WriteLine("Order was not posted with status: ", response.StatusCode);
                return 3;
            }
        }

        public static decimal CalculateDiscount(decimal amount, int type, int years)
        {
            decimal result = 0;
            decimal disc = (years > 5) ? (decimal)5 / 100 : (decimal)years / 100;
            if (type == 1) // not registered 
            {
                result = amount;
            }
            else if (type == 2) // simple customer
            {
                result = (amount - (0.1m * amount)) - disc * (amount - (0.1m * amount));
            }
            else if (type == 3) // valuable customer
            {
                result = (0.7m * amount) - disc * (0.7m * amount);
            }
            else if (type == 4) // most valuable customer
            {
                result = (amount - (0.5m * amount)) - disc * (amount - (0.5m * amount));
            }
            return result;
        }

        public static string FindStringProperty(string json, string propertyName)
        {
            var regex = new Regex("\"" + propertyName + "\":\"([^\"]+)\"", RegexOptions.Multiline);
            var match = regex.Match(json);

            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            return "";
        }

        public static void SendEmail(string topic, string body, string from, string to)
        {
            // sending email 
        }
    }
}
