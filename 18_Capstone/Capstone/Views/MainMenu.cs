using Capstone.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Capstone.Views
{
    /// <summary>
    /// The top-level menu in our Market Application
    /// </summary>
    public class MainMenu : CLIMenu
    {
        /// <summary>
        /// Constructor adds items to the top-level menu
        /// </summary>
        public MainMenu(VendingMachine machineReference) : base(machineReference)
        {
            this.Title = "*** Main Menu ***";
            this.menuOptions.Add("1", "Display Inventory");
            this.menuOptions.Add("2", "Go to Purchase menu");
            this.menuOptions.Add("Q", "Quit");
        }

        /// <summary>
        /// The override of ExecuteSelection handles whatever selection was made by the user.
        /// This is where any business logic is executed.
        /// </summary>
        /// <param name="choice">"Key" of the user's menu selection</param>
        /// <returns></returns>
        protected override bool ExecuteSelection(string choice)
        {
            switch (choice)
            {
                case "1":
                    // Display output of file containing Position # and Item
                    List<string> listToDisplay = new List<string>();
                    listToDisplay = MyMachine.GiveMenuProductList();
                    foreach(string word in listToDisplay)
                    {
                        Console.WriteLine(word); 
                    }
                    Pause("");
                        return true;
                case "2":
                    // Sends user to the purchase menu
                    PurchaseMenu menu = new PurchaseMenu(this.MyMachine);
                    menu.Run();
                    return true;
            }
            return true;
        }

    }
}
