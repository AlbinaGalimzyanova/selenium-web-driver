using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace SeleniumHomework.PageObject
{
    public class CartHelper : HelperBase
    {
        public CartHelper(IWebDriver driver) : base(driver) { }

        public void EmptyCart()
        {
            var shortcuts = driver.FindElements(cartPage.Table);

            foreach (var shortcut in shortcuts)
            {
                var table = driver.FindElement(cartPage.Table);
                wait.Until(driver => driver.FindElement(cartPage.RemoveButton).Displayed);
                driver.FindElement(cartPage.RemoveButton).Click();
                wait.Until(ExpectedConditions.StalenessOf(table));
            }
        }
    }
}
