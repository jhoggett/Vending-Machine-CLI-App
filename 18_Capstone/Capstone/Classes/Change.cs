using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Change
    {
        public int Quarters { get; set; }

        public int Dimes { get; set; }

        public int Nickels { get; set; }

        public decimal TotalChange
        {
            get
            {
                return (Quarters * .25M) + (Dimes * .10M) + (Nickels * .05M);
            }
        }
    
    }
}
