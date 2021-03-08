using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace SeleniumHomework
{
    public class AdminLogin
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
        public void AdminLoginTest()
        {
            driver.Url = "http://localhost/litecart/admin/";
            driver.FindElement(By.XPath("//*[@name='username']")).SendKeys("admin");
            driver.FindElement(By.XPath("//*[@name='password']")).SendKeys("admin");
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
