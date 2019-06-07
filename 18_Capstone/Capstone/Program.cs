using Capstone.Classes;
using Capstone.Views;
using System;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            // If you want to use the CLI menu, you can create an instance in Main, and 
            // Run it.  You can customize the Main menu, and create other menus in the Views folder.
            // If you do not want to use the CLI menu, you can delete the files from the Views folder.

            VendingMachine vendingMachine = new VendingMachine();
            vendingMachine.Load(@"C:\Users\BGalinas\GitRepos\c-module-1-capstone-team-8\18_Capstone\etc\vendingmachine.csv");

            MainMenu menu = new MainMenu(vendingMachine);
            menu.Run();
        }
    }
}
