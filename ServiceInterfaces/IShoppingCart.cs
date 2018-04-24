using System;
using System.Collections.Generic;

namespace ServiceInterfaces
{
    public interface IShoppingCart
    {
        bool AddProduct(IProduct product, int pieces);
        decimal SumProductPrice();
        List<Tuple<IProduct, int>> GetProducts();
    }
}
