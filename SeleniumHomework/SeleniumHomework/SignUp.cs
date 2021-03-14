using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace SeleniumHomework
{
    public class SignUp
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
        public void SignUpTest()
        {
            var dateTime = DateTime.Now.ToString("HHmmssff");

            driver.Url = "http://localhost/litecart/";
            driver.FindElement(By.CssSelector("#box-account-login a")).Click();

            Thread.Sleep(1000);
            
            driver.FindElement(By.CssSelector("input[name=tax_id]")).SendKeys("test");
            driver.FindElement(By.CssSelector("input[name=company]")).SendKeys("test");
            driver.FindElement(By.CssSelector("input[name=firstname]")).SendKeys("Albina");
            driver.FindElement(By.CssSelector("input[name=lastname]")).SendKeys("Galimzyanova");
            driver.FindElement(By.CssSelector("input[name=address1]")).SendKeys("test");
            driver.FindElement(By.CssSelector("input[name=address2]")).SendKeys("test");
            driver.FindElement(By.CssSelector("input[name=postcode]")).SendKeys("12345");
            driver.FindElement(By.CssSelector("input[name=city]")).SendKeys("Kazan");
            driver.FindElement(By.CssSelector("span[class=select2-selection__rendered]")).Click();
            driver.FindElement(By.CssSelector("li[id*='US']")).Click();
            driver.FindElement(By.CssSelector("input[name=email]")).SendKeys("test_" + dateTime + "@gmail.com");
            driver.FindElement(By.CssSelector("input[name=phone]")).SendKeys("+1234567890");
            driver.FindElement(By.CssSelector("input[name=password]")).SendKeys("qwerty");
            driver.FindElement(By.CssSelector("input[name=confirmed_password]")).SendKeys("qwerty");
            driver.FindElement(By.CssSelector("button[name=create_account]")).Click();

            driver.FindElement(By.CssSelector("#box-account li:nth-of-type(4) a")).Click();

            driver.FindElement(By.CssSelector("input[name=email]")).SendKeys("test_" + dateTime + "@gmail.com");
            driver.FindElement(By.CssSelector("input[name=password]")).SendKeys("qwerty");
            driver.FindElement(By.CssSelector("input[name=remember_me]")).Click();
            driver.FindElement(By.CssSelector("button[name=login]")).Click();

            driver.FindElement(By.CssSelector("#box-account li:nth-of-type(4) a")).Click();
        }

        [TearDown]
        public void Stop()
        {
            driver.Quit();
        }
    }
}
