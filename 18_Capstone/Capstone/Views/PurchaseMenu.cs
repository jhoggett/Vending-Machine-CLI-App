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

        List<string> userCartList = new List<string>();

        protected override bool ExecuteSelection(string choice)
        {
            Console.WriteLine($"Your balance is ${MyMachine.UserBalance}");
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Please input your money($1.00, $2.00, $5.00, $10.00): ");
                    string givenMoney = Console.ReadLine();
                    decimal moneyToDecimal = decimal.Parse(givenMoney);
                    //if (moneyToDecimal == 1.00M || moneyToDecimal == 2.00M || moneyToDecimal == 5.00M || moneyToDecimal == 10.00M)
                    try
                    {
                        
                        MyMachine.FeedMoney(moneyToDecimal);
                    }
                    catch(Exception ex) 
                    {
                        Console.WriteLine(ex.Message);
                    }
                    Pause("Want to try another bill?");
                    return true;

                case "2":
                    List<string> listToDisplay = new List<string>();
                    listToDisplay = MyMachine.GiveMenuProductList();
                    foreach (string word in listToDisplay)
                    {
                        Console.WriteLine(word);
                    }
                    Pause("");
                    Console.WriteLine("Please enter the letter and digit code for product location e.g. A4");
                    string userInputLocation = Console.ReadLine();

                    try
                    {
                        MyMachine.VendItem(userInputLocation);

                        MyMachine.AddToUserCart(userInputLocation);
                        Console.WriteLine("");
                        foreach (string item in userCartList)
                        {
                            Console.WriteLine(item);
                        }
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    Pause("");
                    return true;

                case "3":
                    Console.WriteLine("Enjoy your food!");
                    Change change = MyMachine.GiveChange();
                    Console.WriteLine($"{change.Quarters} Quarters, {change.Dimes} Dimes, {change.Nickels} Nickels Returned");

                    string[] sounds = MyMachine.userEats();
                    foreach(string sound in sounds)
                    {
                        Console.WriteLine(sound);
                    }
                    Pause("");
                    return true;
            }
            return true;
        }
    }
}

