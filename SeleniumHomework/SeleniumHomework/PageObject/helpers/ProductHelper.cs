using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumHomework.PageObject
{
    public class ProductHelper : HelperBase
    {
        public ProductHelper(IWebDriver driver) : base(driver) { }

        public void AddProductToCart(int index)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(productPage.AddToCartButton));
            wait.Until(driver => driver.FindElement(productPage.AddToCartButton).Displayed &&
                driver.FindElement(productPage.AddToCartButton).Enabled);

            if (driver.FindElements(productPage.ProductSize).Count != 0)
            {
                var dropdown = new SelectElement(driver.FindElement(ProductPage.ProductSize));
                dropdown.SelectByIndex(1);
            }

            driver.FindElement(productPage.AddToCartButton).Click();
            wait.Until(driver => driver.FindElement(basePage.CartQuantity).Text.Equals(index.ToString()));
            
            driver.Navigate().Back();
        }
    }
}
