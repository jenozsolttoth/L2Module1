using System.Collections.Generic;
using DAL.Entities;

namespace L2Mentoring.Module1.Interfaces
{
    public interface IProductParser
    {
        List<ProductQuantity> ParseProducts(string productList);
    }
}
