using OpenQA.Selenium;

namespace SeleniumHomework.PageObject
{
    public class CartPage : BasePage
    {
        public By Table => By.CssSelector("ul.shortcuts a.inact");
        public By RemoveButton => By.CssSelector("button[name=remove_cart_item]");
    }
}
