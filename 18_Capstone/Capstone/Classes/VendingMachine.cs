using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Capstone.Classes
{
    public class VendingMachine
    {
        /// <summary>
        /// Represents inventory, dictionary whose key is the location in the machine,
        /// and the value is the stock object(This contains a product)
        /// </summary>
        private Dictionary<string, Stock> inventory;
        //Holds the users money balance
        public decimal UserBalance { get; set; } = 0.00M;


        public void Load(string path)
        {
            inventory = new Dictionary<string, Stock>();
            using(StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] productCreator = line.Split("|");
                    Product product1 = new Product(productCreator[1], decimal.Parse(productCreator[2]), productCreator[3]);
                    Stock stock = new Stock(product1, productCreator[0]);
                    inventory.Add(stock.Location, stock);
                  
                }
            }
        }


        public List<string> GiveMenuProductList()
        {
            List<string> tempList = new List<string>();

            foreach (KeyValuePair<string, Stock> kvp in inventory)
            {
                // move to main menu
                tempList.Add(kvp.Key + "|" + kvp.Value.Item.ProductName + "|" + kvp.Value.Item.ProductPrice.ToString() + "|" + kvp.Value.Item.Category);               
            }
            return tempList;

        }




        public VendingMachine (/*string path*/)
        {
            //@"C:\Users\JHoggett\Git\c-module-1-capstone-team-8\18_Capstone\etc\vendingmachine.csv";

            this.Load(@"C:\Users\JHoggett\Git\c-module-1-capstone-team-8\18_Capstone\etc\vendingmachine.csv");
            


        }
        


        /// <summary>
        /// Takes a decimal number from the user as money, 
        /// </summary>
        /// <param name="givenAmount"></param>
        /// <returns></returns>
        public decimal FeedMoney(decimal givenAmount)
        {
            try
            {
                if ((givenAmount == 1.00M) || (givenAmount == 2.00M) || (givenAmount == 5.00M) || (givenAmount == 10.00M))
                {
                    this.UserBalance = this.UserBalance + givenAmount;                 
                }
                return this.UserBalance;
            }
            catch (Exception ex)
            {
                if (ex != null)
                {
                    Console.WriteLine("Enter a valid money amount:");
                }
                throw;
            }
        }
    }
}
