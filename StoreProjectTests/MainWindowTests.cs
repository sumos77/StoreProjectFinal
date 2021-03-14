using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoreProject;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreProject.Tests
{
    [TestClass()]
    public class MainWindowTests
    {
        [TestMethod()]
        public void NoItem()
        {
            Product p = new Product();
            Dictionary<Product, int> Cart = new Dictionary<Product, int>();

            // create a mock Cart object and substitute it for MainWindows Cart object
            MainWindow.Cart = Cart;

            decimal sum = MainWindow.TotalSum();

            Assert.AreEqual(0, sum);
        }

        [TestMethod()]
        public void OneItemMultipleTimes()
        {
            Product p = new Product();
            Dictionary<Product, int> Cart = new Dictionary<Product, int>();

            MainWindow.Cart = Cart;

            p.Name = "Dator";
            p.Price = "5000";

            Cart.Add(p, 2);

            decimal sum = MainWindow.TotalSum();
            Assert.AreEqual(10000, sum);
        }

        [TestMethod()]
        public void TwoItemsMultipleTimes()
        {
            Product p = new Product();
            Product pr = new Product();
            Dictionary<Product, int> Cart = new Dictionary<Product, int>();

            MainWindow.Cart = Cart;

            p.Name = "Dator";
            p.Price = "5000";
            pr.Name = "Ratt";
            pr.Price = "430";

            Cart.Add(p, 3);
            Cart.Add(pr, 2);

            decimal sum = MainWindow.TotalSum();
            Assert.AreEqual(15860, sum);
        }

        [TestMethod()]
        public void OneItemMultipleTimesWithRebate()
        {
            Product p = new Product();
            Rebate currentRebateCode = new Rebate();
            Dictionary<Product, int> Cart = new Dictionary<Product, int>();

            MainWindow.Cart = Cart;
            MainWindow.currentRebateCode = currentRebateCode;

            p.Name = "Dator";
            p.Price = "5000";
            currentRebateCode.Code = "BCE234";
            currentRebateCode.Percent = "10";

            Cart.Add(p, 2);
            decimal sum = MainWindow.TotalSumWithRebate();
            Assert.AreEqual(9000, sum);
        }

        [TestMethod()]
        public void TwoItemsMultipleTimesWithRebate()
        {
            Product p = new Product();
            Product pr = new Product();
            Rebate currentRebateCode = new Rebate();
            Dictionary<Product, int> Cart = new Dictionary<Product, int>();

            MainWindow.Cart = Cart;
            MainWindow.currentRebateCode = currentRebateCode;

            p.Name = "Dator";
            p.Price = "5000";
            pr.Name = "Ratt";
            pr.Price = "430";
            currentRebateCode.Code = "BCE234";
            currentRebateCode.Percent = "10";

            Cart.Add(p, 3);
            Cart.Add(pr, 2);

            decimal sum = MainWindow.TotalSumWithRebate();
            Assert.AreEqual(14274, sum);
        }
    }
}