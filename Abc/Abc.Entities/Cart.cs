using System;
using System.Collections.Generic;
using System.Linq;

namespace Abc.Entities
{
    public class Cart
    {
        public Cart()
        {
            //obj. null ref. hatası almamak için.
            CartLines = new List<CartLine>();
        }
        public List<CartLine> CartLines { get; set; }
        public Decimal Total
        {
            get { return CartLines.Sum(c => c.Product.UnitPrice * c.Quantity); }
        }
    }
}
