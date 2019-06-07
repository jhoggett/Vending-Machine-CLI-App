using Capstone.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CapstoneTests
{
    [TestClass]
    public class VendingMachineTests
    {
        [DataTestMethod]
        [DataRow (1.00, 1.00)]
        [DataRow (2.00, 2.00)]
        [DataRow(5.00, 5.00)]
        [DataRow(10.00, 10.00)]
        public void WhenFeedingPositiveDollarAmount_ShouldUpdateUserBalance(double givenAmount, double expectedUserBalance)
        {

            VendingMachine testMachine = new VendingMachine();
            Assert.AreEqual((decimal)expectedUserBalance, testMachine.FeedMoney((decimal)givenAmount), "The balance should equal the given amount on a new machine");
        }

        [DataTestMethod]
        [DataRow(3.00)]
        [DataRow(4.00)]
        [DataRow(-1.00)]
        [ExpectedException(typeof(Exception))]
        public void WhenFeedingAnInvalidDollarAmount_ShouldReturnExceptionMessage(double givenAmount)
        {

            VendingMachine testMachine = new VendingMachine();

            // Expect an Exception to be thrown here
            decimal newBalance = testMachine.FeedMoney((decimal)givenAmount);
            //Assert.AreEqual(0.00M, newBalance, "Balance should be zero");
        }
        [DataTestMethod]
        [DataRow (10.00, 20.00)]
        public void WhenFeedingPositiveDollarAmountIntoABalanceThatAlreadyExists_ShouldIncreaseThatAmount(double givenAmount, double expectedUserBalance)
        {
            VendingMachine testMachine = new VendingMachine();
            testMachine.FeedMoney(10.00M);
            Assert.AreEqual((decimal)expectedUserBalance, testMachine.FeedMoney((decimal)givenAmount));
        }
        [TestMethod]
        public void WhenExitingVendingMachine_ShouldReturnChangeInLeastAmountOfCoinsPossible()
        {
            VendingMachine testMachine = new VendingMachine();
            testMachine.FeedMoney((decimal)1.00);
            Change testChange = new Change();
            Change functionResultChange = testMachine.GiveChange();
            testChange.Quarters = 4;
            Assert.AreEqual(testChange.Quarters, functionResultChange.Quarters);

        }
        [TestMethod]
        public void WhenExitingVendingMachine_ShouldReturnChangeInLeastAmountOfCoinsPossible2()
        {
            VendingMachine testMachine = new VendingMachine();
            testMachine.Load(@"Data\vendingmachine.csv");
            testMachine.FeedMoney((decimal)2.00);
            testMachine.VendItem("D1");
            
            Change testChange = new Change();
            Change functionResultChange = testMachine.GiveChange();
            testChange.Quarters = 4;
            testChange.Dimes = 1;
            testChange.Nickels = 1;
            Assert.AreEqual((testChange.Quarters, testChange.Dimes, testChange.Nickels), (functionResultChange.Quarters, functionResultChange.Dimes, functionResultChange.Nickels));

        }



        //[TestMethod]
        //public void WhenVendItemIsCalledSuccesfully_ShouldDecrementQuanityByOne()
        //{
        //    VendingMachine testMachine = new VendingMachine();
        //    //testMachine.Load(@"C:\Users\BGalinas\GitRepos\c-module-1-capstone-team-8\18_Capstone\etc\vendingmachine.csv");

        //    string location = "test";
        //    Product testProduct = new Product("test", 0.00M, "test");
        //    Stock testStock = new Stock(testProduct, "test");
        //    int newQuantity = testStock.Quantity - 1;
        //    testMachine.VendItem(location);
        //    Assert.AreEqual(newQuantity, testStock.Quantity,
        //        "Vend Item should reduce Stock Quantity by 1");
        //}
    }
}
