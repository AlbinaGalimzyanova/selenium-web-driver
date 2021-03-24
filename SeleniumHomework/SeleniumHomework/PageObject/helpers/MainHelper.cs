using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace SeleniumHomework.PageObject
{
    public class MainHelper : HelperBase
    {
        public MainHelper(IWebDriver driver) : base(driver) { }

        public void OpenMainPage()
        {
            driver.Url = "http://localhost/litecart/";
        }

        public void OpenFirstProduct()
        {
            driver.FindElement(mainPage.FirstProduct).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(productPage.AddToCartButton));
            wait.Until(driver => driver.FindElement(productPage.AddToCartButton).Displayed &&
                driver.FindElement(productPage.AddToCartButton).Enabled);
        }

        public void OpenCart()
        {
            driver.FindElement(basePage.CartButton).Click();
        }
    }
}
