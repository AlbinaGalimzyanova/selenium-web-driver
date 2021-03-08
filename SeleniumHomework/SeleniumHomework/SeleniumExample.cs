using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace SeleniumHomework
{
    public class SeleniumExample
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
        public void SeleniumExampleTest()
        {
            driver.Url = "https://habr.com/ru/";
            driver.FindElement(By.XPath("//*[@class='post__title_link']")).Click();
        }

        [TearDown]
        public void Stop()
        {
            driver.Quit();
        }
    }
}
