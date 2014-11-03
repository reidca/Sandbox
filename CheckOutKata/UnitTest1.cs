using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CheckOutKata
{
    [TestClass]
    public class CheckOutTests : ICheckoutListener
    {
        // Product Pricing
        //A - 5
        //B - 10
        //C - 20
        //D - 50
        // Discount Rules
        //Buy 3 x A get 1 free
        //Buy 2 x B for 15
        //Buy 2 x C for 35
        //Buy 1 x B and 1 x C for 25
        //Buy 2 x A and 2 x C for 40

        //

        public Money Total { get; set; }

        private CheckOut _checkout;

        [TestInitialize]
        public void Setup()
        {
            _checkout = new CheckOut();
            _checkout.ItemScanned += _checkout_ItemScanned;
            _checkout.TotalCalculated += _checkout_TotalCalculated;
        }

        void _checkout_ItemScanned(object sender, ItemScannedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Price of scanned item {0} is {1}, sub total after scan  is {2}", e.ScannedProductName, e.ScannedProductPrice, e.Total);
        }

        void _checkout_TotalCalculated(object sender, TotalCalculatedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Final total is {0}", e.Total);
            this.Total = e.Total;
        }
        

        [TestMethod]
        public void TotalCostof1A1B1C_30()
        {
            _checkout
                .Scan(new Product("A"))
                .Scan(new Product("B"))
                .Scan(new Product("C"));

            _checkout.CalculateTotal();
            Assert.AreEqual(new Money(30), this.Total);
        }

        [TestMethod]
        public void TotalCostof1A_Is5()
        {
            _checkout
                .Scan(new Product("A"));
            _checkout.CalculateTotal();
            Assert.AreEqual(new Money(5), this.Total);
        }

        [TestMethod]
        public void TotalCostof1B_Is10()
        {
            _checkout
                .Scan(new Product("B"));
            _checkout.CalculateTotal();
            Assert.AreEqual(new Money(10), this.Total);
        }

        [TestMethod]
        public void TotalCostof1C_Is20()
        {
            _checkout
                .Scan(new Product("C"));
            _checkout.CalculateTotal();
            Assert.AreEqual(new Money(20), this.Total);
        }

        [TestMethod]
        public void TotalCostof2A_Is10()
        {
            _checkout
                .Scan(new Product("A"))
                .Scan(new Product("A"));
            _checkout.CalculateTotal();
            Assert.AreEqual(new Money(10), this.Total);
        }

        [TestMethod]
        public void TotalCostof3A_Is10()
        {
            _checkout
                .Scan(new Product("A"))
                .Scan(new Product("A"))
                .Scan(new Product("A"));
            _checkout.CalculateTotal();
            Assert.AreEqual(new Money(10), this.Total);
        }

        [TestMethod]
        public void TotalCostof4A_Is15()
        {
            _checkout
                .Scan(new Product("A"))
                .Scan(new Product("A"))
                .Scan(new Product("A"))
                .Scan(new Product("A"));
            _checkout.CalculateTotal();
            Assert.AreEqual(new Money(15), this.Total);
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
            _checkout.CalculateTotal();
            Assert.AreEqual(new Money(20), this.Total);
        }

        [TestMethod]
        public void TotalCostof1xBand1XC_Is25()
        {
            _checkout
                .Scan(new Product("B"))
                .Scan(new Product("C"));
            _checkout.CalculateTotal();
            Assert.AreEqual(new Money(25), this.Total);
        }

        [TestMethod]
        public void TotalCostof2xAand2XC_Is40()
        {
            _checkout
                .Scan(new Product("A"))
                .Scan(new Product("A"))
                .Scan(new Product("C"))
                .Scan(new Product("C"));
            _checkout.CalculateTotal();
            Assert.AreEqual(new Money(40), this.Total);
        }


        [TestMethod]
        public void TotalCostof2B_Is15()
        {
            _checkout
                .Scan(new Product("B"))
                .Scan(new Product("B"));
            _checkout.CalculateTotal();
            Assert.AreEqual(new Money(15), this.Total);
        }


        [TestMethod]
        public void TotalCostof4B_Is30()
        {
            _checkout
                .Scan(new Product("B"))
                .Scan(new Product("B"))
                .Scan(new Product("B"))
                .Scan(new Product("B"));
            _checkout.CalculateTotal();
            Assert.AreEqual(new Money(30), this.Total);
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
            _checkout.CalculateTotal();
            Assert.AreEqual(new Money(60), this.Total);
        }

        [TestMethod]
        public void TotalCostof2C_Is35()
        {
            _checkout
                .Scan(new Product("C"))
                .Scan(new Product("C"));
            _checkout.CalculateTotal();
            Assert.AreEqual(new Money(35), this.Total);
        }

        [TestMethod]
        public void NoDiscountAppliedTo_ProductD()
        {
            int numberOfItemsInBasket = 100;

            Money totalCost = PricingEngine.PriceList[new Product("D")] * numberOfItemsInBasket;


            for (int i = 1; i <= numberOfItemsInBasket; i++)
            {
                _checkout
                    .Scan(new Product("D"));
            }
            _checkout.CalculateTotal();
            Assert.AreEqual(totalCost, this.Total);

        }









    }
}
