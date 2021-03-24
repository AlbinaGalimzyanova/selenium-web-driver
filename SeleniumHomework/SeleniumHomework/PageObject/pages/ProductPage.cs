using OpenQA.Selenium;

namespace SeleniumHomework.PageObject
{
    public class ProductPage : BasePage
    {
        public By ProductSize => By.CssSelector("select[name='options[Size]']");
        public By AddToCartButton => By.Name("add_cart_product");
    }
}
