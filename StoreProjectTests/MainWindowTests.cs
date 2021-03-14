using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoreProject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StoreProject.Tests
{
    [TestClass()]
    public class MainWindowTests
    {
        [TestMethod()]
        public void NoItem()
        {
            Product product = new Product();
            Dictionary<Product, int> cart = new Dictionary<Product, int>();

            // create a mock Cart object and substitute it for MainWindows Cart object
            MainWindow.Cart = cart;

            decimal sum = MainWindow.TotalSum();

            Assert.AreEqual(0, sum);
        }

        [TestMethod()]
        public void OneItemMultipleTimes()
        {
            Product product = new Product();
            Dictionary<Product, int> cart = new Dictionary<Product, int>();

            MainWindow.Cart = cart;

            product.Name = "Dator";
            product.Price = "5000";

            cart.Add(product, 2);

            decimal sum = MainWindow.TotalSum();
            Assert.AreEqual(10000, sum);
        }

        [TestMethod()]
        public void TwoItemsMultipleTimes()
        {
            Product productOne = new Product();
            Product productTwo = new Product();
            Dictionary<Product, int> cart = new Dictionary<Product, int>();

            MainWindow.Cart = cart;

            productOne.Name = "Dator";
            productOne.Price = "5000";
            productTwo.Name = "Ratt";
            productTwo.Price = "430";

            cart.Add(productOne, 3);
            cart.Add(productTwo, 2);

            decimal sum = MainWindow.TotalSum();
            Assert.AreEqual(15860, sum);
        }

        [TestMethod()]
        public void OneItemMultipleTimesWithRebate()
        {
            Product product = new Product();
            Rebate currentRebateCode = new Rebate();
            Dictionary<Product, int> cart = new Dictionary<Product, int>();

            MainWindow.Cart = cart;
            MainWindow.currentRebateCode = currentRebateCode;

            product.Name = "Dator";
            product.Price = "5000";
            currentRebateCode.Code = "BCE234";
            currentRebateCode.Percent = "10";

            cart.Add(product, 2);
            decimal sum = MainWindow.TotalSumWithRebate();
            Assert.AreEqual(9000, sum);
        }

        [TestMethod()]
        public void TwoItemsMultipleTimesWithRebate()
        {
            Product productOne = new Product();
            Product pr = new Product();
            Rebate currentRebateCode = new Rebate();
            Dictionary<Product, int> cart = new Dictionary<Product, int>();

            MainWindow.Cart = cart;
            MainWindow.currentRebateCode = currentRebateCode;

            productOne.Name = "Dator";
            productOne.Price = "5000";
            pr.Name = "Ratt";
            pr.Price = "430";
            currentRebateCode.Code = "BCE234";
            currentRebateCode.Percent = "10";

            cart.Add(productOne, 3);
            cart.Add(pr, 2);

            decimal sum = MainWindow.TotalSumWithRebate();
            Assert.AreEqual(14274, sum);
        }

        [TestMethod()]
        public void SaveCartSavesItems()
        {
            Product productOne = new Product();
            Product productTwo = new Product();
            Dictionary<Product, int> cart = new Dictionary<Product, int>();

            productOne.Name = "Dator";
            productOne.Description = "Den har en CPU.";
            productOne.Price = "5000";
            productTwo.Name = "Ratt";
            productTwo.Description = "Den roterar.";
            productTwo.Price = "430";

            cart.Add(productOne, 3);
            cart.Add(productTwo, 2);

            string cartFilePath = @"C:\Windows\Temp\Testcart.csv";

            MainWindow.SaveCart(cart, cartFilePath);

            List<string> cartLines = new List<string>();
            string[] lines = File.ReadAllLines(cartFilePath);
            string[] firstProduct = lines[0].Split(",");
            string[] secondProduct = lines[1].Split(",");
            
            Assert.AreEqual(firstProduct[0], productOne.Name);
            Assert.AreEqual(firstProduct[1], productOne.Description);
            Assert.AreEqual(firstProduct[2], productOne.Price);

            Assert.AreEqual(secondProduct[0], productTwo.Name);
            Assert.AreEqual(secondProduct[1], productTwo.Description);
            Assert.AreEqual(secondProduct[2], productTwo.Price);

            if (File.Exists(cartFilePath))
            {
                File.Delete(cartFilePath);
            }
        }

        [TestMethod()]
        public void LoadCartItems()
        {
            string cartFilePath = @"C:\Windows\Temp\Testcart.csv";

            Product[] product = new Product[2];
            Dictionary<Product, int> cart = new Dictionary<Product, int>();

            product[0] = new Product { Name = "Dator", Description = "Den har en CPU.", Price = "5000" };
            product[1] = new Product { Name = "Ratt", Description = "Den roterar.", Price = "430" };

            List<string> productLine = new List<string>();
            productLine.Add(product[0].Name + "," + product[0].Description + "," + product[0].Price + ",5");
            productLine.Add(product[1].Name + "," + product[1].Description + "," + product[1].Price + ",3");

            File.WriteAllLines(cartFilePath, productLine);

            cart = MainWindow.LoadCart(cartFilePath, product);
            Assert.AreEqual(cart.Count, 2);

            if (File.Exists(cartFilePath))
            {
                File.Delete(cartFilePath);
            }
        }
    }
}