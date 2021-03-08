using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace SeleniumHomework
{
    public class AdminMenu
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
        public void AdminMenuTest()
        {
            Login("admin", "admin");
            var menuItems = driver.FindElements(By.XPath("//*[@id='app-']"));

            for (int i = 1; i <= menuItems.Count; i++)
            {
                Thread.Sleep(1000);
                driver.FindElement(By.XPath("//*[@id='app-'][" + i + "]")).Click();
                Assert.True(driver.FindElement(By.TagName("h1")).Displayed);
                var itemItems = driver.FindElements(By.XPath("//*[contains(@id, 'doc')]"));

                if (itemItems.Count != 0)
                {
                    for (int j = 1; j <= itemItems.Count; j++)
                    {
                        driver.FindElement(By.XPath("//*[contains(@id, 'doc')][" + j + "]")).Click();
                        Assert.True(driver.FindElement(By.TagName("h1")).Displayed);
                    }
                }
            }
        }

        private void Login(string username, string password)
        {
            driver.Url = "http://localhost/litecart/admin/";
            driver.FindElement(By.XPath("//*[@name='username']")).SendKeys(username);
            driver.FindElement(By.XPath("//*[@name='password']")).SendKeys(password);
            driver.FindElement(By.XPath("//*[@name='remember_me']")).Click();
            driver.FindElement(By.XPath("//*[@name='login']")).Click();
        }

        [TearDown]
        public void Stop()
        {
            driver.Quit();
        }
    }
}
