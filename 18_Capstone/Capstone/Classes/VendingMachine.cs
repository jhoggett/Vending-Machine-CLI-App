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
        private List<string> whatToEat = new List<string>();


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
                if (kvp.Value.Quantity == 0)
                {
                    tempList.Add(kvp.Key + "|" + kvp.Value.Item.ProductName + "|" + kvp.Value.Item.ProductPrice.ToString() + "|" + kvp.Value.Item.Category
                    + "|" + "SOLD OUT!");
                }
                else
                {
                    tempList.Add(kvp.Key + "|" + kvp.Value.Item.ProductName + "|" + kvp.Value.Item.ProductPrice.ToString() + "|" + kvp.Value.Item.Category
                        + "|" + kvp.Value.Quantity);
                }
            }
            return tempList;

        }

        /// <summary>
        /// Takes a decimal number from the user as money, 
        /// </summary>
        /// <param name="givenAmount"></param>
        /// <returns></returns>
        public decimal FeedMoney(decimal givenAmount)
        {
            string whatFunction = "FEED MONEY";
            decimal userInput = givenAmount;
            if (givenAmount == 1.00M || givenAmount == 2.00M || givenAmount == 5.00M || givenAmount == 10.00M)
            {
                decimal userInputAfter = this.UserBalance + givenAmount;
                this.AuditSales(whatFunction, userInput, userInputAfter);
            }
            else
            {
                throw new Exception("Please enter a valid dollar amount e.g. $1.00, $2.00, $5.00, $10.00");
            }
            this.UserBalance = this.UserBalance + givenAmount;
            
            return this.UserBalance;
        }

        public void VendItem(string location)
        {
            if (inventory.ContainsKey(location))
            {
                if(inventory[location].Quantity > 0 && this.UserBalance > inventory[location].Item.ProductPrice)
                {
                    //vars for audit function
                    string whatProduct = inventory[location].Item.ProductName;
                    decimal userStartBalance = this.UserBalance;
                    decimal userEndBalance = this.UserBalance - inventory[location].Item.ProductPrice;

                    //vending functionaliy 
                    inventory[location].Quantity = inventory[location].Quantity - 1;
                    this.UserBalance = this.UserBalance - inventory[location].Item.ProductPrice;

                    this.AuditSales(whatProduct, userStartBalance, userEndBalance);

                }
                else
                {
                    throw new Exception ("Item Sold Out or you have insufficient funds");
                }
                
            }
            else
            {
                throw new Exception ("This is not a valid slot location. please enter another.");
            }
           
        }

        public void AddToUserCart(string slotLocation)
        {
            if (inventory.ContainsKey(slotLocation))
            {
                whatToEat.Add(inventory[slotLocation].Item.Category);
            }
        }

        public string[] userEats()
        {
            List<string> tempSoundList = new List<string>();
            foreach(string food in whatToEat)
            {
                if(food == "Chip")
                {
                    tempSoundList.Add("Crunch Crunch, Yum!"); 
                }
                else if(food == "Candy")
                {
                    tempSoundList.Add("Munch Munch, Yum!");
                }
                else if(food == "Drink")
                {
                    tempSoundList.Add("Glug Glug, Yum!");
                }
                else
                {
                    tempSoundList.Add("Chew Chew, Yum!");
                }
            }
            whatToEat.Clear();
            return tempSoundList.ToArray();
        }

        public Change GiveChange()
        {
            Change change = new Change();

            string whatFunction = "GIVE CHANGE";
            decimal userBalanceStart = this.UserBalance;
            decimal userBalanceEnd = 0.00M;

            change.Quarters = (int)(this.UserBalance/.25M);
            this.UserBalance = this.UserBalance % .25M;

            change.Dimes = (int)(this.UserBalance / .10M);
            this.UserBalance = this.UserBalance % .10M;

            change.Nickels = (int)(this.UserBalance / .05M);
            this.UserBalance = this.UserBalance % .05M;

            this.AuditSales(whatFunction, userBalanceStart, userBalanceEnd);

            return change;
        }

        public void AuditSales(string whatFunction, decimal firstBalance, decimal secondBalance)
        {
            using(StreamWriter sw = new StreamWriter(@"Log.txt", true))
            {
                sw.WriteLine("{0} {1} {2 :C} {3 :C}", DateTime.Now, whatFunction, firstBalance, secondBalance);
            }
        }




        






    }



}
