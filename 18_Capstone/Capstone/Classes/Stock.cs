using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Stock
    {
        public int Quantity { get; set; }

        public Product Item { get; set; }

        public string Location { get; set; }

        public Stock(Product item, string location)
        {
            this.Item = item;
            this.Location = location;
            this.Quantity = 5;
        }
    }  
}
