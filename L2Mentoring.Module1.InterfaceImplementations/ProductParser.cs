using System.Collections.Generic;
using DAL.Entities;
using L2Mentoring.Module1.Interfaces;

namespace L2Mentoring.Module1.InterfaceImplementations
{
    public class ProductParser : IProductParser
    {
        public List<ProductQuantity> ParseProducts(string productList)
        {
            List<ProductQuantity> result = new List<ProductQuantity>();
            string[] products = productList.Split(";");
            foreach ( var a in products )
            {
                string[] productAndQuantity = a.Split(':');
                result.Add(new ProductQuantity() { ProductName = productAndQuantity[0], Quantity = int.Parse(productAndQuantity[1]) });
            }
            return result;
        }
    }
}
