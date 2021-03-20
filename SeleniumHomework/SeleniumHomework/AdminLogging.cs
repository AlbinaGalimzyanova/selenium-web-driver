using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace SeleniumHomework
{
    public class AdminLogging
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
        public void CheckBrowserLogsTest()
        {
            Login("admin", "admin");
            wait.Until(driver => driver.FindElement(By.CssSelector("#app-:nth-of-type(2)")).Displayed &&
                driver.FindElement(By.CssSelector("#app-:nth-of-type(2)")).Enabled);
            driver.FindElement(By.CssSelector("#app-:nth-of-type(2)")).Click();
            wait.Until(driver => driver.FindElement(By.TagName("h1")).Displayed);
            var products = driver.FindElements(By.CssSelector("tr.row td:nth-of-type(3) a[href*=product_id]"));
            for (int i = 0; i < products.Count; i++)
            {
                driver.FindElements(By.CssSelector("tr.row td:nth-of-type(3) a[href*=product_id]"))[i].Click();
                wait.Until(driver => driver.FindElement(By.TagName("h1")).Displayed);
                var logs = new List<LogEntry>();
                foreach (LogEntry l in driver.Manage().Logs.GetLog("browser"))
                {
                    Console.WriteLine(l);
                    logs.Add(l);
                }
                Assert.IsEmpty(logs);
                driver.Navigate().Back();
                wait.Until(driver => driver.FindElement(By.TagName("h1")).Displayed);
            }
        }

        private void Login(string username, string password)
        {
            driver.Url = "http://localhost/litecart/admin/";
            driver.FindElement(By.CssSelector("input[name=username]")).SendKeys(username);
            driver.FindElement(By.CssSelector("input[name=password]")).SendKeys(password);
            driver.FindElement(By.CssSelector("input[name=remember_me]")).Click();
            driver.FindElement(By.CssSelector("button[name=login]")).Click();
            wait.Until(driver => driver.FindElement(By.CssSelector("div.logotype")).Displayed);
        }

        [TearDown]
        public void Stop()
        {
            driver.Quit();
        }
    }
}
