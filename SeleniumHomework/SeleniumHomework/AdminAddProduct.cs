using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Threading;

namespace SeleniumHomework
{
    public class AdminAddProduct
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
        public void AddNewProductTest()
        {
            var dateTime = DateTime.Now.ToString("HHmmssff");
            var productName = "Pirate Duck " + dateTime;
            var workingDirectory = AppContext.BaseDirectory;
            var projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.Parent.FullName;
            var filePath = Path.Combine(projectDirectory, "/images/duck.jpg");

            Login("admin", "admin");
            Thread.Sleep(1000);
            
            driver.FindElement(By.CssSelector("#app-:nth-of-type(2)")).Click();
            driver.FindElement(By.CssSelector("a.button:nth-of-type(2)")).Click();
            Thread.Sleep(1000);
            
            driver.FindElement(By.CssSelector("input[name=status][value='1']")).Click();
            driver.FindElement(By.CssSelector("input[name='name[en]']")).SendKeys(productName);
            driver.FindElement(By.CssSelector("input[name=code]")).SendKeys("test" + dateTime);
            driver.FindElement(By.CssSelector("input[name='product_groups[]'][value='1-3']")).Click();
            driver.FindElement(By.CssSelector("input[name=quantity]")).Clear();
            driver.FindElement(By.CssSelector("input[name=quantity]")).SendKeys("50");
            driver.FindElement(By.CssSelector("input[type=file]")).SendKeys(filePath);

            driver.FindElement(By.CssSelector("div.tabs li:nth-of-type(2)")).Click();
            Thread.Sleep(1000);
            
            driver.FindElement(By.CssSelector("input[name=keywords]")).SendKeys("pirate");
            driver.FindElement(By.CssSelector("input[name='short_description[en]']")).SendKeys("Litte Pirate Duck");
            driver.FindElement(By.CssSelector("textarea[name='description[en]']")).SendKeys(
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse sollicitudin ante massa, eget ornare libero porta congue.");
            driver.FindElement(By.CssSelector("input[name='head_title[en]']")).SendKeys("Pirate Duck");
            driver.FindElement(By.CssSelector("input[name='meta_description[en]']")).SendKeys("Pirate Duck");

            driver.FindElement(By.CssSelector("div.tabs li:nth-of-type(4)")).Click();
            Thread.Sleep(1000);

            driver.FindElement(By.CssSelector("input[name=purchase_price]")).Clear();
            driver.FindElement(By.CssSelector("input[name=purchase_price]")).SendKeys("15");
            var dropdown = new SelectElement(driver.FindElement(By.CssSelector("select[name=purchase_price_currency_code]")));
            dropdown.SelectByText("US Dollars");
            driver.FindElement(By.CssSelector("input[name='gross_prices[USD]']")).Clear();
            driver.FindElement(By.CssSelector("input[name='gross_prices[USD]']")).SendKeys("30");
            driver.FindElement(By.CssSelector("input[name='gross_prices[EUR]']")).Clear();
            driver.FindElement(By.CssSelector("input[name='gross_prices[EUR]']")).SendKeys("25");

            driver.FindElement(By.CssSelector("button[name=save]")).Click();

            var products = driver.FindElements(By.CssSelector("tr.row"));
            var hasAppeared = false;

            foreach (IWebElement product in products)
            {
                if (product.Text.Equals(productName))
                {
                    hasAppeared = true;
                    break;
                }
            }
        
            Assert.IsTrue(hasAppeared);
        }

        private void Login(string username, string password)
        {
            driver.Url = "http://localhost/litecart/admin/";
            driver.FindElement(By.CssSelector("input[name=username]")).SendKeys(username);
            driver.FindElement(By.CssSelector("input[name=password]")).SendKeys(password);
            driver.FindElement(By.CssSelector("input[name=remember_me]")).Click();
            driver.FindElement(By.CssSelector("button[name=login]")).Click();
        }

        [TearDown]
        public void Stop()
        {
            driver.Quit();
        }
    }
}
