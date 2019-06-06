using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Product
    {
        public string ProductName { get; set; }

        public decimal ProductPrice { get; set; }

        public string Category { get; set; }

        public Product(string productName, decimal productPrice, string category)
        {
            this.ProductName = productName;
            this.ProductPrice = productPrice;
            this.Category = category;
        }
    }
}
