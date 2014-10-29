using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CheckOutKata
{
    [TestClass]
    public class CheckOutTests
    {
        // Product Pricing
        //A - 5
        //B - 10
        //C - 20
        // Discount Rules
        //Buy 3 x A get 1 free
        //Buy 2 x B for 15
        //Buy 2 x C for 35
        //Buy 1 x B and 1 x C for 25
        //Buy 2 x A and 2 x C for 40

        //

        private CheckOut _checkout;

        [TestInitialize]
        public void Setup()
        {
            _checkout = new CheckOut();
        }

        [TestMethod]
        public void TotalCostof1A1B1C_30()
        {
            _checkout
                .Scan(new Product("A"))
                .Scan(new Product("B"))
                .Scan(new Product("C"));

            Assert.AreEqual(new Money(30), _checkout.GetTotal());
        }
        
        [TestMethod]
        public void TotalCostof1A_Is5()
        {
            _checkout
                .Scan(new Product("A"));

            Assert.AreEqual(new Money(5), _checkout.GetTotal());
        }

        [TestMethod]
        public void TotalCostof1B_Is10()
        {
            _checkout
                .Scan(new Product("B"));

            Assert.AreEqual(new Money(10), _checkout.GetTotal());
        }

        [TestMethod]
        public void TotalCostof1C_Is20()
        {
            _checkout
                .Scan(new Product("C"));

            Assert.AreEqual(new Money(20), _checkout.GetTotal());
        }

        [TestMethod]
        public void TotalCostof2A_Is10()
        {
            _checkout
                .Scan(new Product("A"))
                .Scan(new Product("A"));

            Assert.AreEqual(new Money(10), _checkout.GetTotal());
        }

        [TestMethod]
        public void TotalCostof3A_Is10()
        {
            _checkout
                .Scan(new Product("A"))
                .Scan(new Product("A"))
                .Scan(new Product("A"));

            Assert.AreEqual(new Money(10), _checkout.GetTotal());
        }

        [TestMethod]
        public void TotalCostof4A_Is15()
        {
            _checkout
                .Scan(new Product("A"))
                .Scan(new Product("A"))
                .Scan(new Product("A"))
                .Scan(new Product("A"));

            Assert.AreEqual(new Money(15), _checkout.GetTotal());
        }


        [TestMethod]
        public void TotalCostof6A_Is20()
        {
            _checkout
                .Scan(new Product("A"))
                .Scan(new Product("A"))
                .Scan(new Product("A"))
                .Scan(new Product("A"))
                .Scan(new Product("A"))
                .Scan(new Product("A"));

            Assert.AreEqual(new Money(20), _checkout.GetTotal());
        }

        [TestMethod]
        public void TotalCostof1xBand1XC_Is25()
        {
            _checkout
                .Scan(new Product("B"))
                .Scan(new Product("C"));

            Assert.AreEqual(new Money(25), _checkout.GetTotal());
        }
         
        [TestMethod]
        public void TotalCostof2xAand2XC_Is40()
        {
            _checkout
                .Scan(new Product("A"))
                .Scan(new Product("A"))
                .Scan(new Product("C"))
                .Scan(new Product("C"));

            Assert.AreEqual(new Money(40), _checkout.GetTotal());
        }


        [TestMethod]
        public void TotalCostof2B_Is15()
        {
            _checkout
                .Scan(new Product("B"))
                .Scan(new Product("B"));

            Assert.AreEqual(new Money(15), _checkout.GetTotal());
        }


        [TestMethod]
        public void TotalCostof4B_Is30()
        {
            _checkout
                .Scan(new Product("B"))
                .Scan(new Product("B"))
                .Scan(new Product("B"))
                .Scan(new Product("B"));

            Assert.AreEqual(new Money(30), _checkout.GetTotal());
        }

        [TestMethod]
        public void TotalCostof8B_Is60()
        {
            _checkout
                .Scan(new Product("B"))
                .Scan(new Product("B"))
                .Scan(new Product("B"))
                .Scan(new Product("B"))
                .Scan(new Product("B"))
                .Scan(new Product("B"))
                .Scan(new Product("B"))
                .Scan(new Product("B"));

            Assert.AreEqual(new Money(60), _checkout.GetTotal());
        }

        [TestMethod]
        public void TotalCostof2C_Is35()
        {
            _checkout
                .Scan(new Product("C"))
                .Scan(new Product("C"));

            Assert.AreEqual(new Money(35), _checkout.GetTotal());
        }






    }
}
