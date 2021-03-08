using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace SeleniumHomework
{
    public class ProductStickers
    {
        private IWebDriver driver;

        [SetUp]
        public void Start()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Test]
        public void ProductStickersTest()
        {
            driver.Url = "http://localhost/litecart/";
            var products = driver.FindElements(By.CssSelector("div.image-wrapper"));

            foreach (IWebElement product in products)
            {
                var stickers = product.FindElements(By.CssSelector("div.sticker"));
                Assert.AreEqual(1, stickers.Count);
            }
        }

        [TearDown]
        public void Stop()
        {
            driver.Quit();
        }
    }
}
