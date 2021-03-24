using OpenQA.Selenium;

namespace SeleniumHomework.PageObject
{
    public class BasePage
    {
        public By CartButton => By.CssSelector("div[id=cart]");
        public By CartQuantity => By.CssSelector("span.quantity");
    }
}
