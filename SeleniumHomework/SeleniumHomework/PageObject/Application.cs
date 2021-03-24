using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumHomework.PageObject
{
    public class Application
    {
        internal IWebDriver driver;
        internal MainHelper mainHelper;
        internal ProductHelper productHelper;
        internal CartHelper cartHelper;

        public Application()
        {
            driver = new ChromeDriver();
            mainHelper = new MainHelper(driver);
            productHelper = new ProductHelper(driver);
            cartHelper = new CartHelper(driver);
        }

        public void Quit()
        {
            driver.Quit();
        }
    }
}
