using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace SeleniumHomework
{
    public class Cart
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void Start()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void CartTest()
        {
            driver.Url = "http://localhost/litecart/";

            AddProductToCart(1);
            AddProductToCart(2);
            AddProductToCart(3);

            driver.FindElement(By.CssSelector("div[id=cart]")).Click();
            var shortcuts = driver.FindElements(By.CssSelector("ul.shortcuts a.inact"));

            foreach(var shortcut in shortcuts)
            {
                var table = driver.FindElement(By.CssSelector("table.dataTable"));
                wait.Until(driver => driver.FindElement(By.CssSelector("button[name=remove_cart_item]")).Displayed);
                driver.FindElement(By.CssSelector("button[name=remove_cart_item]")).Click();
                wait.Until(ExpectedConditions.StalenessOf(table));
            }
        }

        private void AddProductToCart(int index)
        {
            driver.FindElement(By.CssSelector("div.image-wrapper:nth-of-type(1)")).Click();
            wait.Until(driver => driver.FindElement(By.Name("add_cart_product")));
            
            if (driver.FindElements(By.CssSelector("select[name='options[Size]']")).Count != 0)
            {
                var dropdown = new SelectElement(driver.FindElement(By.CssSelector("select[name='options[Size]']")));
                dropdown.SelectByIndex(1);
            }

            driver.FindElement(By.Name("add_cart_product")).Click();
            wait.Until(driver => driver.FindElement(By.CssSelector("span.quantity")).Text.Equals(index.ToString()));
            driver.Navigate().Back();
        }

        [TearDown]
        public void Stop()
        {
            driver.Quit();
        }
    }
}
