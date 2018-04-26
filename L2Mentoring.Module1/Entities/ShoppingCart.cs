using System;
using System.Collections.Generic;
using ServiceInterfaces;

namespace L2Mentoring.Module1.Entities
{
    public class ShoppingCart : IShoppingCart
    {
        private readonly List<Tuple<IProduct, int>> _products;
        private decimal _sumProductPrice;
        public ShoppingCart()
        {
            _products = new List<Tuple<IProduct, int>>();
        }
        public bool AddProduct(IProduct product, int pieces)
        {
            _products.Add(new Tuple<IProduct, int>(product, pieces));
            if ( product.Found )
            {
                _sumProductPrice = _sumProductPrice + (product.Price * pieces);
            }
            return true;
        }

        public decimal SumProductPrice()
        {
            return _sumProductPrice;
        }

        List<Tuple<IProduct, int>> IShoppingCart.GetProducts()
        {
            return _products;
        }

        public List<Tuple<IProduct, int>> GetProducts()
        {
            return _products;
        }
    }
}
