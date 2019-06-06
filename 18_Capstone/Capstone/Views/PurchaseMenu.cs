using Capstone.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Views
{
    public class PurchaseMenu : CLIMenu
    {
        public PurchaseMenu(VendingMachine machineReference) : base(machineReference)
        {
            this.Title = "*** Purchasing Menu ***";
            this.menuOptions.Add("1", "Feed Money");
            this.menuOptions.Add("2", "Select Product");
            this.menuOptions.Add("3", "Finish Transaction");
        }



        protected override bool ExecuteSelection(string choice)
        {
            VendingMachine vendingMachine = new VendingMachine();
            
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Please input your money($1.00, $2.00, $5.00, $10.00): ");
                    string givenMoney = Console.ReadLine();
                    decimal moneyToDecimal = decimal.Parse(givenMoney);
                    vendingMachine.FeedMoney(moneyToDecimal);
                    return true;
                    
                    
                    
                //case "2":
                //    select product
                //    case "3":
                //    end transaction

            }
            return true;
        }
    }
}

