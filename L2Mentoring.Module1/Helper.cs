using System.Collections.Generic;
using L2Mentoring.Module1.Helpers;

namespace L2Mentoring.Module1
{
    public class Helper
    {
        public static List<ProductQuantity> ParseProducts(string productList)
        {
            List<ProductQuantity> result = new List<ProductQuantity>();
            string[] products = productList.Split(";");
            foreach (var a in products)
            {
                string[] productAndQuantity = a.Split(':');
                result.Add(new ProductQuantity(){ ProductName = productAndQuantity[0], Quantity = int.Parse(productAndQuantity[1])});
            }
            return result;
        }
        public static void SendEmail(string topic, string body, string from, string to)
        {
            // sending email 
        }
    }
}
