using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace SeleniumHomework
{
    public class AdminTabs
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
        public void OpenInNewTabTest()
        {
            Login("admin", "admin");
            driver.Url = "http://localhost/litecart/admin/?app=countries&doc=countries";
            driver.FindElement(By.CssSelector("a.button")).Click();
            var links = driver.FindElements(By.CssSelector("i.fa.fa-external-link"));

            foreach (IWebElement link in links)
            {
                var mainWindow = driver.CurrentWindowHandle;
                var oldWindows = driver.WindowHandles;
                link.Click();
                wait.Until(driver => driver.WindowHandles.Count > oldWindows.Count);
                var newWindows = driver.WindowHandles;
                driver.SwitchTo().Window(newWindows[1]);
                driver.Close();
                driver.SwitchTo().Window(mainWindow);
            }
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
