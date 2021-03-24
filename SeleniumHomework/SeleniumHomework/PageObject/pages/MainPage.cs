using OpenQA.Selenium;

namespace SeleniumHomework.PageObject
{
    public class MainPage : BasePage
    {
        public By FirstProduct => By.CssSelector("div.image-wrapper:nth-of-type(1)");
    }
}
