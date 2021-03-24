using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace SeleniumHomework.PageObject
{
    public class HelperBase
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;
        protected BasePage basePage;
        protected MainPage mainPage;
        protected ProductPage productPage;
        protected CartPage cartPage;

        public HelperBase(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            basePage = new BasePage();
            mainPage = new MainPage();
            productPage = new ProductPage();
            cartPage = new CartPage();
        }

        #region Page Constructors

        public BasePage BasePage
        {
            get
            {
                return basePage;
            }
        }

        public MainPage MainPage
        {
            get
            {
                return mainPage;
            }
        }

        public ProductPage ProductPage
        {
            get
            {
                return productPage;
            }
        }

        public CartPage CartPage
        {
            get
            {
                return cartPage;
            }
        }

        #endregion
    }
}
